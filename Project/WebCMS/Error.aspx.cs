using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreNET.Common.Base;

public partial class Error : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    string msg = @"Setting aplikasi tidak sesuai, hubungi helpdesk!";
    if (MasterAppConstants.Instance.StatusTesting)
    {
      msg += " Error terjadi karena " + GlobalExt.CurrentException.Message + " pada " + GlobalExt.CurrentException.StackTrace;
      Panel1.Html = "";

      //link ke halaman utama
    }
  }
}