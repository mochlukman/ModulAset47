using CoreNET.Common.Base;
using CoreNET.Common.BO;
using Ext.Net;
using Ext.Net.Utilities;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;

public partial class TextEditor : System.Web.UI.Page
{
  private WindowDebug WindowDebug1 = null;
  private WindowLookupText WindowLookupText1 = new WindowLookupText();

  public IDataControlUIEntry GetDataControlForText()
  {
    IDataControlUIEntry dc = null;

    if (!string.IsNullOrEmpty(Request["debug"]) || !string.IsNullOrEmpty(Request["key"]))
    {
      string url = HttpContext.Current.Request.UrlReferrer.OriginalString;
      string sessionkey = Request["key"];
      dc = (IDataControlUIEntry)HttpContext.Current.Session[sessionkey];
    }

    int id = GlobalExt.GetRequestI();
    if (dc == null)
    {
      dc = (IDataControlUIEntry)UtilityUI.GetDataControl(id);
      if (dc == null)
      {
        UtilityUI.ProcessInfo();
        dc = (IDataControlUIEntry)UtilityUI.GetDataControl(id);
      }
    }
    ((BaseBO)dc).ModePage = ViewListProperties.MODE_TEXT_EDITOR;
    dc.SetPageKey();
    IDataControlUI dcprev = (IDataControlUIEntry)UtilityUI.GetDataControlPrev(id);
    if (dcprev == null)
    {
      dcprev = GlobalExt.GetSessionMenu();
    }
    if (dcprev != null)
    {
      dcprev.SetPageKey();
      dc.SetFilterKey((BaseBO)dcprev);
    }

    //PropertyInfo prop = dc.GetProperty(Request["pk"]);
    //if (prop != null)
    //{
    //  prop.SetValue(dc, Request["val"], null);
    //}
    //if (dc.Load() == null)
    //{
    //  dc.Insert();
    //}
    return dc;
  }

  protected void Page_Load(object sender, EventArgs e)
  {
    if (!string.IsNullOrEmpty(Request["msg"]))
    {
      string sesname = Request["msg"];
      string msg = Session[sesname].ToString();
      string content = Printer.CheckBase64Encoding(msg);
      PnlText.Html = content;
      HtmlEditor1.Text = PnlText.Html;
      Toolbar1.Visible = false;
    }
    else
    {
      if (GlobalAsp.CekSessionPage(Page))
      {
        #region Inisialisasi
        try
        {
          WindowDebug1 = new WindowDebug(GlobalAsp.GetRequestI(), GlobalAsp.GetURLSessionKey());
        }
        catch (Exception ex)
        {
          UtilityBO.Log(ex);
        }
        IDataControlUIEntry dc = GetDataControlForText();
        string path = ((BaseBO)dc).Path;
        PanelLink.AutoLoad.Url = UtilityExt.ValidateURL(this, string.Format("FileBrowser.aspx?local=" + HttpContext.Current.Request["local"] + "&app=" + GlobalAsp.GetRequestApp() + "&roleid=" + HttpContext.Current.Request["roleid"] + "&id=" + GlobalExt.GetRequestId() + "&title=Cross Referensi&current=1&path=" + path + "&kkp=" + Request["kkp"] + "&tbfolder=0&lcfolder=1&mode=copylink"));
        UtilityExt.SetIFrameAutoLoad(PanelLink.AutoLoad);
        PanelLink.LoadContent();
        btnCancel1.Text = ConstantDictExt.Translate("BTN_CANCEL1");
        if (Request["parentsave"] == "1")
        {
          //Langsung Nutup
          //btnSave1.Listeners.Click.Handler = @"
          //  if(!!parent.CoreNET.Methods1)
          //  {
          //    parent.CoreNET.Methods1.btnCancelClick();
          //  }
          //";
          btnCancel1.Listeners.Click.Handler = @"
            if(!!parent.CoreNET.Methods1)
            {
              parent.CoreNET.Methods1.btnCancelClick();
            }
            CoreNET.NormalizeFrame();
          ";
        }

        if (Request["mode"] == null)
        {
          btnSave1.Listeners.Click.Handler += @"
            this.setDisabled(true);
            #{btnEdit1}.setDisabled(false);
            #{BtnLink}.setDisabled(false);
            #{PanelEditor}.completeEdit();
            #{Toolbar2}.setVisible(false);
            #{PnlText}.setVisible(true);
            CoreNET.SaveText(Ext.util.Format.htmlEncode(HtmlEditor1.getValue()));
            #{PnlText}.update();
            //HtmlEditor1.update(HtmlEditor1.getValue(),true);
            ";
        }
        else
        {
          btnSave1.Listeners.Click.Handler += @"
            this.setDisabled(true);
            #{btnEdit1}.setDisabled(false);
            #{BtnLink}.setDisabled(false);
            #{PanelEditor}.completeEdit();
            #{Toolbar2}.setVisible(false);
            #{PnlText}.setVisible(true);
            CoreNET.SaveText(Ext.util.Format.htmlEncode(HtmlEditor1.getValue()));
            #{PnlText}.update();
            //HtmlEditor1.update(HtmlEditor1.getValue(),true);
            ";
        }
        string url = Request.UrlReferrer.OriginalString;
        bool istree = url.ToLower().Contains("tree");
        btnSave1.Listeners.Click.Handler += @"
            if(!!parent.CoreNET.Methods1)
            {" +
              (istree ? "parent.refreshTree(); " : "parent.refreshData(); ")
            + @"}
          ";

        #endregion
        #region !X.IsAjaxRequest
        if (!X.IsAjaxRequest)
        {
          Title = Request["title"];

          string sessionname = "TextEditing" + GlobalExt.GetRequestId();
          Session[sessionname] = string.Empty;

          WindowLink.Listeners.Hide.Handler = "NormalizeFrame();";

          btnWord.Text = ConstantDictExt.Translate("BTN_WORD1");

          Toolbar1.Visible = Request["tb"] == "1";
          if (Request["tb"] == "1")
          {
            /*Override this method on DC to make it dinamically enable-disable*/
            Toolbar1.Visible = dc.IsEditable();
          }
          if (Request["mode"] == "print")
          {
            Toolbar1.Visible = true;
          }
          if (MasterAppConstants.Instance.StatusAdmin || ConfigurationManager.AppSettings["EditHelp"] == "1")
          {
            Toolbar1.Visible = true;
          }
          if (dc != null)
          {
            string text = string.Empty;
            try
            {
              if (!string.IsNullOrEmpty(Request["pname"]))
              {
                string pname = Request["pname"];
                if (pname.ToLower().Contains("debug"))
                {
                  /*Dipake di WindowDebug*/
                  //text = dc.GetDefaultDebug();//Untuk Debug
                  text = (string)dc.GetValue(pname);//Efeknya sama
                }
                else
                {
                  text = (string)dc.GetValue(pname);
                }
              }
              else/*Dipake di WindowHelp*/
              {
                /*Dipake di WindowHelp*/
                text = NotesUIControl.LoadHTML(dc, int.Parse(Request["type"]));
              }
            }
            catch (Exception ex)
            {
              UtilityBO.Log(ex);
            }

            string content = Printer.CheckBase64Encoding(text);
            PnlText.Html = content;
            HtmlEditor1.Text = PnlText.Html;

            if (!string.IsNullOrEmpty(Request["pname"]) || (Request["preview"] == "1"))
            {
              if (Request["pname"].Contains(",") || (Request["preview"] == "1"))
              {
                btnEdit1.Hide();
                btnSave1.Hide();
                btnWord.Listeners.Click.Handler = @"CoreNET.ExportToWord(Ext.util.Format.htmlEncode(HtmlEditor1.getValue()));";
                btnWord.Show();
              }
            }

            if (((Request["ro"] != null) && (Request["ro"] != "0")) || (Request["enable"] == "0"))
            {
              btnEdit1.Hide();
              btnSave1.Hide();
            }
          }
          else
          {
            PnlText.Disabled = true;
          }
        }
        int id = GlobalExt.GetRequestI();
        if (Request["mode"] == "help")
        {
          btnEdit1.Listeners.Click.Handler =
            " this.setDisabled(true);#{btnSave1}.setDisabled(false);#{btnCancel1}.setDisabled(false);#{BtnLink}.setDisabled(true);#{PanelEditor}.startEdit(#{PnlText}.getBody());#{Toolbar2}.setVisible(true);#{PnlText}.setVisible(false);" +
            "";
        }
        else
        {
          btnEdit1.Listeners.Click.Handler = @"
            if (!!parent.GridPanel1){
              if (!!parent.GridPanel1.getSelectionModel().getSelected() || !!parent.GridPanel1.record) {
               this.setDisabled(true);#{btnSave1}.setDisabled(false);#{btnCancel1}.setDisabled(false);#{BtnLink}.setDisabled(true);#{PanelEditor}.startEdit(#{PnlText}.getBody());#{Toolbar2}.setVisible(true);#{PnlText}.setVisible(false);
              }else{
                alert('Error data selection');
              }
            }else if (!!parent.TreeGrid1){
              if (!!parent.TreeGrid1.getSelectedNodes()) {
               this.setDisabled(true);#{btnSave1}.setDisabled(false);#{btnCancel1}.setDisabled(false);#{BtnLink}.setDisabled(true);#{PanelEditor}.startEdit(#{PnlText}.getBody());#{Toolbar2}.setVisible(true);#{PnlText}.setVisible(false);
              }else{
                alert('Error data selection');
              }
            }else{
              this.setDisabled(true);#{btnSave1}.setDisabled(false);#{btnCancel1}.setDisabled(false);#{BtnLink}.setDisabled(true);#{PanelEditor}.startEdit(#{PnlText}.getBody());#{Toolbar2}.setVisible(true);#{PnlText}.setVisible(false);
            }";

        }

        #region Conflict Resolution
        if ((HttpContext.Current.Request["lookup"] == "1") || (HttpContext.Current.Request["cr"] == "1"))
        {
          Ext.Net.Button btnCR = new Ext.Net.Button() { ID = "btnCR1", Icon = Icon.Magnifier };
          btnCR.Text = ConstantDictExt.Translate(btnCR.ID);
          btnCR.ToolTip = ConstantDictExt.TranslateTabTip(btnCR);
          btnCR.Listeners.Click.Handler = "CoreNET.ShowCRDialog();";
          Toolbar1.Add(btnCR);
        }
        #endregion
        #region Debugging
        if (MasterAppConstants.Instance.StatusTesting)
        {
          Ext.Net.Button btnDebug = new Ext.Net.Button() { ID = "btnDebug" + Toolbar1.ID, Text = "Debugging", Icon = Icon.PageLightning };
          btnDebug.DirectClick += new ComponentDirectEvent.DirectEventHandler(btnDebug_DirectClick);
          Toolbar1.Add(btnDebug);
        }
        Ext.Net.Button btnPreview = new Ext.Net.Button() { ID = "btnPreview" + Toolbar1.ID, Text = "Preview", Icon = Icon.PageWorld };
        btnPreview.Listeners.Click.Handler = "CoreNET.Preview(HtmlEditor1.getValue());";
        Toolbar1.Add(btnPreview);
        #endregion

        #endregion
      }
      else
      {
        X.Js.Call("home");
      }
    }
  }

  private string StripAbsoluteLink(string content)
  {
    if (content != null)
    {
      string partern = @"(http([\s\S]*?)://([\s\S]*?)/SiAPDocs)";

      string replacement = "../../SiAPDocs";
      content = Regex.Replace(content, partern, replacement, RegexOptions.Multiline | RegexOptions.IgnoreCase);
      return content;
    }
    return null;
  }

  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void Preview(string text)
  {
    string datadir = GlobalAsp.GetDataDir();
    string path = "Tmp/report.htm";
    string filename = datadir + path;
    StreamWriter SW = File.CreateText(filename);
    SW.WriteLine(text);
    SW.WriteLine("<br/><br/><hr/><br/>");
    SW.WriteLine(text);
    SW.Close();

    string url = GlobalAsp.GetDataURL() + path;

    //string url = Request.Url.AbsoluteUri.Replace("tb=1", "tb=0");
    X.Js.Call("open", url);
  }
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void ShowCRDialog()
  {
    Ext.Net.Panel Parent = ControlUtils.FindControl<Ext.Net.Panel>(this, "PanelFormEntry");
    if (Parent != null)
    {
      WindowLookupText1.Render(Parent);
    }
    else
    {
      WindowLookupText1.Render(Form);
    }
    WindowLookupText1.Show();

    string pname = "Col1";
    if (Request["pname"] != null)
    {
      pname = Request["pname"];
    }
    IDataControlUI dc = GetDataControlForText();


  }

  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void SetText(string text)
  {
    PnlText.Html = StripAbsoluteLink(HttpUtility.HtmlDecode(text));
  }
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  private void btnDebug_DirectClick(object sender, DirectEventArgs e)
  {
    WindowDebug1.Render(Form);
    WindowDebug1.Show();
  }
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void CopyLink()
  {
    X.Msg.Confirm(ConstantDictExt.Translate(GlobalExt.LBL_CONFIRM),
      ConstantDictExt.Translate(GlobalExt.LBL_CONFIRM + MethodBase.GetCurrentMethod().Name), new MessageBoxButtonsConfig
      {
        Yes = new MessageBoxButtonConfig
        {
          Handler = "CoreNET.GenerateTabelReferensi();",
          Text = ConstantDictExt.Translate(GlobalExt.LBL_YES)
        },
        No = new MessageBoxButtonConfig
        {
          Handler = "CoreNET.ExpandFrame();WindowLink.show();",
          Text = ConstantDictExt.Translate(GlobalExt.LBL_NO)
        }
      }).Show();
  }

  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void PastePic(string html)
  {
    HtmlEditor1.Text += html;
  }

  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void GenerateTabelReferensi()
  {
    int id = GlobalExt.GetRequestI();
    btnEdit1.FireEvent("click", null);
    //IDataControl dc = GetDataControlForText();
    IDataControl dc = GetDataControlForText();// (IDataControlUI)UtilityUI.GetDataControl(1);

    if (typeof(IReferences).IsInstanceOfType(dc))
    {
      #region File References
      List<FileReferences> data = ((IReferences)dc).GetListReferensi();

      string html;
      html = @"
                <p style=""font-size: large"">Your text here ... </p>
                <p style=""font-size: large"">Daftar referensi KKP yang ada di folder ini sbb.</p>
                <table border=""1"" style=""border-style: groove; border-width: thin"">
                  <tr>
                    <td style=""width:5%"">No</td>
                    <td style=""width:25%"">Nama file</td>
                    <td style=""width:70%"">Keterangan</td>
                  </tr>
                ";

      for (int i = 0; i < data.Count; i++)
      {
        string url = HttpUtility.HtmlDecode(data[i].Url);
        html += @"
                  <tr>
                    <td>" + (i + 1) + @"</td>
                    <td><a href='#' onclick=""javascript:window.open('" + url + "')\" >" + data[i].FileName + @"</a></td>
                    <td>" + data[i].FileName + @"</td>
                  </tr>
                ";
      }
      html += @"</table>";
      html += @"<p style=""font-size: large"">Your text here ... </p>";

      HtmlEditor1.Text += html;
      #endregion
    }
  }
  protected void BtnEditClick(object sender, DirectEventArgs e)
  {
    btnEdit1.Disabled = true;
    btnSave1.Disabled = false;
    HtmlEditor1.ReadOnly = false;
    HtmlEditor1.EnableSourceEdit = true;
  }

  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void ExpandFrame()
  {
    X.Js.Call(@"(function() {
      if (!!parent) 
      {				
        if (!!parent.parent) 
        {
          if(!!parent.parent.MyMethods){
						if(parent.name == 'PnlDetil_IFrame'){
                parent.parent.MyMethods.ExpandBotFrame();								
						}
            else {
							parent.parent.MyMethods.ExpandTopFrame();
            }       
          }
        }
      }
    })");
  }
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void NormalizeFrame()
  {
    X.Js.Call(@"(function() {
      if (!!parent) 
      {
        if (!!parent.parent) 
        {
          if(!!parent.parent.MyMethods){
            parent.parent.MyMethods.NormalizeFrame();
          }
          if(!!parent.WindowHelp){
            parent.WindowHelp.hide();
          }
          if(!!parent.WindowDebug){
            parent.WindowDebug.hide();
          }
        }
      }
    })");
  }

  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void SaveText(string text)
  {
    if ((Request["ro"] == null) || (Request["ro"] == "0"))
    {
      IDataControlUI dc = GetDataControlForText();
      try
      {
        if (!string.IsNullOrEmpty(Request["pname"]))
        {
          dc.SetValue(Request["pname"], text);
        }
        NotesUIControl.SaveHTML(dc, text, int.Parse(Request["type"]));
        PnlText.Html = Printer.CheckBase64Encoding(text);
        PnlText.Update(PnlText.Html);
        PnlText.UpdateContent();
        //PnlText.LoadContent();
        //PnlText.Reload();
        X.Js.Call(@"(function () {
          if (parent.CoreNET.GridReload) {
              parent.CoreNET.GridReload();
          }
        })");
        //Close/Hide JENDELA
        //NormalizeFrame();
      }
      catch (Exception ex)
      {
        UtilityBO.Log(ex);
      }
      //HtmlEditor1.ReadOnly = true;
      //HtmlEditor1.EnableSourceEdit = false;
    }
    else
    {
      X.Msg.Alert(ConstantDictExt.Translate(GlobalExt.LBL_INFO), ConstantDictExt.TranslateException(null, MethodBase.GetCurrentMethod().Name)).Show();
    }
  }

  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void AddLink(string link)
  {
    HtmlEditor1.Text += "<a href='" + link + "' target='_blank'>link</a>";
  }

  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void ExportToWord(string text)
  {
    IDataControlUI dc = GetDataControlForText();
    if (dc != null)
    {
      dc.Load();
    }
    try
    {
      IPrintable dcprint = (IPrintable)dc;
      dcprint.Print(0);
      X.Msg.Alert(ConstantDictExt.Translate(GlobalExt.LBL_INFO), ConstantDictExt.Translate("INFO_EXPORT_SUCCESS")).Show();
    }
    catch (Exception ex)
    {
      WindowDebug.ShowMessage(Page, ConstantDictExt.TranslateException(ex, MethodBase.GetCurrentMethod().Name));
    }

  }

  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void BtnWindowUploadClick(string text)
  {
    Button4.Disabled = true;
    if (FileUploadField1.HasFile)
    {
      IDataControlUI dc = GetDataControlForText();
      string CurrentDir = ((BaseBO)dc).Path;
      string path = GlobalAsp.GetDataDir() + CurrentDir;
      if (!Directory.Exists(path))
      {
        Directory.CreateDirectory(path);
      }

      HttpPostedFile file = FileUploadField1.PostedFile;
      string fullpath = path + "/" + Path.GetFileName(file.FileName);
      try
      {
        if (File.Exists(fullpath))
        {
          File.Delete(fullpath);
          //throw new Exception(string.Format(ConstantLanguages.TxtLblFileIsExists, file.FileName));
        }
        if (file.ContentLength > 30000000) //Batasi ukuran file 30MB
        {
          X.Msg.Alert(ConstantDictExt.Translate(GlobalExt.LBL_INFO), ConstantDictExt.Translate("ERROR_30MB_FILE_SIZE")).Show();
          return;
        }

        file.SaveAs(fullpath);

        string url = CurrentDir.Replace("\\", "/");
        string docurl = GlobalAsp.GetDataURL();

        string fullurl = docurl + url + Path.GetFileName(file.FileName);
        string[] strs = docurl.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
        string baserelativeurl = strs[strs.Length - 1] + "/";
        string relativeurl = "../../" + baserelativeurl + url + Path.GetFileName(file.FileName);

        string sessionname = "TextEditing" + GlobalExt.GetRequestId();
        string currenttext = (string)Session[sessionname];
        if (string.IsNullOrEmpty(currenttext))
        {
          currenttext = HtmlEditor1.Text;
        }
        HtmlEditor1.Text = text + string.Format("\n<br/><a href='{0}' target='_blank'>{1}</a>", relativeurl, Path.GetFileName(file.FileName));
        Session[sessionname] = HtmlEditor1.Text;
      }
      catch (Exception ex)
      {
        WindowDebug.ShowMessage(Page, ConstantDictExt.TranslateException(ex, MethodBase.GetCurrentMethod().Name));
      }
      finally
      {
        Button4.Disabled = false;
      }
    }
    Toolbar1.Enabled = true;
    PanelText.Enabled = true;
    X.Get("loading-mask-upload").SetStyle("display", "none");
    Button4.Disabled = false;
  }
}