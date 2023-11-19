using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Diagnostics;
using CoreNET.Common.Base;

public partial class System_PrintPdf : System.Web.UI.Page
{


  private void Page_Load(object sender, System.EventArgs e)
  {
    string htmlStr = (string)Session["WORDDOC_STR"];
    string progPath = Server.MapPath("~");
    Byte[] bytes = ASCIIEncoding.UTF8.GetBytes(htmlStr);
    using (MemoryStream strm = new MemoryStream())
    {
      strm.Write(bytes, 0, bytes.Length);
      strm.Position = 0;
      this.Response.Clear();
      this.Response.Buffer = true;
      this.Response.ContentType = "application/pdf";
      using (StreamReader sr = new StreamReader(strm))
      {
        Printer.GeneratePdf(progPath, sr, this.Response.OutputStream, new Size(false, 280, 200));
        this.Response.End();
      }
    }
  }
}

