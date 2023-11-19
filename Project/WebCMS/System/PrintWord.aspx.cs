using CoreNET.Common.Base;
using CoreNET.Common.BO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.CustomProperties;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.VariantTypes;
using DocumentFormat.OpenXml.Wordprocessing;
using Ext.Net;
using NotesFor.HtmlToOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;


public partial class System_PrintWord : System.Web.UI.Page
{
  private string html = "";
  private string template = "";
  private string filename = "";

  protected void Page_Load(object sender, EventArgs e)
  {
    if (GlobalAsp.CekSessionPage(Page))
    {
      if (!Page.IsPostBack)
      {
        //if you want it streamed
        //btnStream_Click(null, null);

        //if you want it saved
        btnSave_Click(null, null);
        //if (File.Exists(filename))
        //{
        //  System.Diagnostics.Process.Start(filename);
        //}
      }
    }
    else
    {
      X.Js.Call("home");
    }
  }

  protected void btnSave_Click(object sender, EventArgs e)
  {
    //try
    //{
    int id = string.IsNullOrEmpty(HttpContext.Current.Request["i"]) ? 1 : int.Parse(HttpContext.Current.Request["i"]);
    IPrintable dc = null;
    try
    {
      dc = (IPrintable)UtilityUI.GetDataControl(id);
      if (dc == null)
      {
        //label1.Text = ConstantLang.TxtLblGetDcIsFailed + " : app=" + GlobalAsp.GetRequestApp() + "&roleid=" + HttpContext.Current.Request["roleid"] + "&id=" + Request["id"] + " dan i=" + id;
        return;
      }
    }
    catch (Exception ex)
    {
      UtilityBO.Log(ex);
      //label1.Text = ConstantLang.TxtLblGetDcIsFailed + " : id=" + id;
      return;
    }

    Hashtable Htmls = new Hashtable();
    try
    {
      //Htmls = dc.GetData();
      template = ((IEksporable)dc).GetTemplateFilePath();
      filename = ((IEksporable)dc).GetFilePath(1); //Path.Combine(fpath, fname);
    }
    catch (Exception ex)
    {
      UtilityBO.Log(ex);
      //label1.Text = ConstantLang.TxtLblGetDcIsFailed + "  : Htmls=" + Htmls.Count + ";Template=" + template + ";FileName=" + filename + ";classname=" + dc.ToString();
      return;
    }

    if (File.Exists(filename))
    {
      File.Delete(filename);
    }

    try
    {
      File.Copy(template, filename, true);
    }
    catch (Exception ex)
    {
      UtilityBO.Log(ex);
      //X.MessageBox.Alert(ConstantLang.INFORMATION, "Copy template gagal").Show();
      //label1.Text = ConstantLang.TxtLblCopyTemplateIsFailed;
      return;
    }

    try
    {
      using (WordprocessingDocument package = WordprocessingDocument.Open(filename, true))
      {
        MainDocumentPart mainPart = package.MainDocumentPart;
        if (mainPart == null)
        {
          mainPart = package.AddMainDocumentPart();
          new Document(new Body()).Save(mainPart);
        }

        HtmlConverter converter = new HtmlConverter(mainPart);
        Body body = mainPart.Document.Body;

        List<Paragraph> headings = new List<Paragraph>();
        headings = body
            .OfType<Paragraph>()
            .Where(p => p.ParagraphProperties != null &&
                        p.ParagraphProperties.ParagraphStyleId != null &&
                        p.ParagraphProperties.ParagraphStyleId.Val.Value.Contains("Heading1")).ToList();

        foreach (Paragraph p in headings)
        {
          string html = (string)Htmls[p.InnerText];
          try
          {
            IList<OpenXmlCompositeElement> paragraphs = converter.Parse(html);
            for (int i = paragraphs.Count - 1; i >= 0; i--)
            {
              body.InsertAfter<OpenXmlCompositeElement>(paragraphs[i], p);
            }
          }
          catch (Exception ex)
          {
            UtilityBO.Log(ex);
          }
        }

        mainPart.Document.Save();
      }
    }
    catch (Exception ex)
    {
      UtilityBO.Log(ex);
      //label1.Text = ConstantLang.TxtLblPrintReportEror + "  " + ex.Message;
      return;
    }

    foreach (string key in dc.GetDocProperties().Keys)
    {
      string html = (string)dc.GetDocProperties()[key];
      try
      {
        SetCustomProperty(filename, key, html, PropertyTypes.Text);
      }
      catch (Exception ex)
      {
        UtilityBO.Log(ex);
      }
    }
    //X.MessageBox.Alert(ConstantLang.INFORMATION, "Cetak berhasil. Silahkan lihat di halaman referensi").Show();
    //label1.Text = ConstantLang.TxtLblPrintSuccesed;
    //}
    //catch (Exception ex)
    //{
    //  //X.MessageBox.Alert(ConstantLang.INFORMATION, "Gagal karena " + ex.Message).Show();
    //  label1.Text = "Gagal karena " + ex.Message;
    //}
  }

  protected void btnStream_Click(object sender, EventArgs e)
  {
    try
    {
      string contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
      html = (string)Session["WORDDOC_STR"];
      filename = (string)Session["WORDDOC_FNAME"];

      using (MemoryStream generatedDocument = new MemoryStream())
      {
        using (WordprocessingDocument package = WordprocessingDocument.Create(generatedDocument, WordprocessingDocumentType.Document))
        {
          MainDocumentPart mainPart = package.MainDocumentPart;
          if (mainPart == null)
          {
            mainPart = package.AddMainDocumentPart();
            new Document(new Body()).Save(mainPart);
          }

          HtmlConverter converter = new HtmlConverter(mainPart)
          {

            //http://html2openxml.codeplex.com/wikipage?title=ImageProcessing&referringTitle=Documentation
            //to process an image you must provide a base url
            BaseImageUrl = new Uri(Request.Url.Scheme + "://" + Request.Url.Authority)
          };

          Body body = mainPart.Document.Body;

          IList<OpenXmlCompositeElement> paragraphs = converter.Parse(html);
          for (int i = 0; i < paragraphs.Count; i++)
          {
            body.Append(paragraphs[i]);
          }

          mainPart.Document.Save();
        }

        byte[] bytesInStream = generatedDocument.ToArray(); // simpler way of converting to array
        generatedDocument.Close();

        Response.Clear();
        Response.ContentType = contentType;
        Response.AddHeader("content-disposition", "attachment;filename=" + filename);

        //this will generate problems
        Response.BinaryWrite(bytesInStream);
        Session["WORDDOC_STR"] = null;
        Session["WORDDOC_FNAME"] = null;
        Session["WORDDOC_PATH"] = null;
        try
        {
          Response.End();
        }
        catch (Exception ex)
        {
          UtilityBO.Log(ex);
          //Response.End(); generates an exception. if you don't use it, you get some errors when Word opens the file...
        }
      }
    }
    catch (Exception ex)
    {
      UtilityBO.Log(ex);
    }
  }

  public enum PropertyTypes : int
  {
    YesNo,
    Text,
    DateTime,
    NumberInteger,
    NumberDouble
  }

  public static string SetCustomProperty(
      string fileName,
      string propertyName,
      object propertyValue,
      PropertyTypes propertyType)
  {
    // Given a document name, a property name/value, and the property type, 
    // add a custom property to a document. The method returns the original
    // value, if it existed.

    string returnValue = null;

    CustomDocumentProperty newProp = new CustomDocumentProperty();
    bool propSet = false;

    // Calculate the correct type.
    switch (propertyType)
    {
      case PropertyTypes.DateTime:

        // Be sure you were passed a real date, 
        // and if so, format in the correct way. 
        // The date/time value passed in should 
        // represent a UTC date/time.
        if ((propertyValue) is DateTime)
        {
          newProp.VTFileTime =
              new VTFileTime(string.Format("{0:s}Z",
                  Convert.ToDateTime(propertyValue)));
          propSet = true;
        }

        break;

      case PropertyTypes.NumberInteger:
        if ((propertyValue) is int)
        {
          newProp.VTInt32 = new VTInt32(propertyValue.ToString());
          propSet = true;
        }

        break;

      case PropertyTypes.NumberDouble:
        if (propertyValue is double)
        {
          newProp.VTFloat = new VTFloat(propertyValue.ToString());
          propSet = true;
        }

        break;

      case PropertyTypes.Text:
        newProp.VTLPWSTR = new VTLPWSTR(propertyValue.ToString());
        propSet = true;

        break;

      case PropertyTypes.YesNo:
        if (propertyValue is bool)
        {
          // Must be lowercase.
          newProp.VTBool = new VTBool(
            Convert.ToBoolean(propertyValue).ToString().ToLower());
          propSet = true;
        }
        break;
    }

    if (!propSet)
    {
      // If the code was not able to convert the 
      // property to a valid value, throw an exception.
      throw new InvalidDataException("propertyValue");
    }

    // Now that you have handled the parameters, start
    // working on the document.
    newProp.FormatId = "{D5CDD505-2E9C-101B-9397-08002B2CF9AE}";
    newProp.Name = propertyName;

    using (WordprocessingDocument document = WordprocessingDocument.Open(fileName, true))
    {
      CustomFilePropertiesPart customProps = document.CustomFilePropertiesPart;
      if (customProps == null)
      {
        // No custom properties? Add the part, and the
        // collection of properties now.
        customProps = document.AddCustomFilePropertiesPart();
        customProps.Properties =
            new DocumentFormat.OpenXml.CustomProperties.Properties();
      }

      Properties props = customProps.Properties;
      if (props != null)
      {
        // This will trigger an exception if the property's Name 
        // property is null, but if that happens, the property is damaged, 
        // and probably should raise an exception.
        OpenXmlElement prop =
            props.Where(
            p => ((CustomDocumentProperty)p).Name.Value
                == propertyName).FirstOrDefault();

        // Does the property exist? If so, get the return value, 
        // and then delete the property.
        if (prop != null)
        {
          returnValue = prop.InnerText;
          prop.Remove();
        }

        // Append the new property, and 
        // fix up all the property ID values. 
        // The PropertyId value must start at 2.
        props.AppendChild(newProp);
        int pid = 2;
        foreach (CustomDocumentProperty item in props)
        {
          item.PropertyId = pid++;
        }
        props.Save();
      }
    }
    return returnValue;
  }


}