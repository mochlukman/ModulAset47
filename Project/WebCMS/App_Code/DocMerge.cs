//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using DocumentFormat.OpenXml.Packaging;
//using DocumentFormat.OpenXml.Wordprocessing;
//using DocumentFormat.OpenXml;
//using DocumentFormat.OpenXml.CustomProperties;
//using DocumentFormat.OpenXml.VariantTypes;
//using System.IO;
//using NotesFor.HtmlToOpenXml;
////using System.Xml.Linq;


//public class DocMerge
//{

//  static void Main(string[] args)
//  {
//    string[] filepaths = new[] { @"D:\Merged1.docx", @"C:\inetpub\Docs\Pustaka\Template\Lapming.docx", @"C:\inetpub\Docs\Pustaka\Template\TemplateP2.docx" };
//    Join(filepaths);
//  }

//  public static void Join(params string[] filepaths)
//  {
//    if (filepaths != null && filepaths.Length > 1)
//    {
//      //first document is the merged document
//      using (WordprocessingDocument myDoc = WordprocessingDocument.Create(@filepaths[0], WordprocessingDocumentType.Document))
//      {
//        SectionProperties SecPro = new SectionProperties();
//        PageSize PSize = new PageSize();
//        PSize.Width = 11906;
//        PSize.Height = 16838;
//        SecPro.Append(PSize);

//        MainDocumentPart mainPart = myDoc.MainDocumentPart;
//        if (mainPart == null)
//        {
//          mainPart = myDoc.AddMainDocumentPart();
//          Body body = new Body();
//          body.Append(SecPro);
//          new Document(body).Save(mainPart);
//        }

//        for (int i = 1; i < filepaths.Length; i++)
//        {
//          string altChunkId = "AltChunkId" + i;
//          AlternativeFormatImportPart chunk = mainPart.AddAlternativeFormatImportPart(
//              AlternativeFormatImportPartType.WordprocessingML, altChunkId);
//          using (FileStream fileStream = File.Open(@filepaths[i], FileMode.Open))
//          {
//            chunk.FeedData(fileStream);
//          }
//          DocumentFormat.OpenXml.Wordprocessing.AltChunk altChunk = new DocumentFormat.OpenXml.Wordprocessing.AltChunk();
//          altChunk.Id = altChunkId;
//          //new page, if you like it...
//          //mainPart.Document.Body.AppendChild(new Paragraph(new Run(new Break() { Type = BreakValues.Page })));
//          mainPart.Document.Body.AppendChild(new Paragraph());
//          //next document
//          mainPart.Document.Body.InsertAfter(altChunk, mainPart.Document.Body.Elements<Paragraph>().Last());
//        }
//        mainPart.Document.Save();
//        myDoc.Close();
//      }
//    }
//  }
//}
