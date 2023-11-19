using CoreNET.Common.Base;
using System;
using System.Configuration;

public partial class Default : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    string url = ConfigurationManager.AppSettings["URLHome"];
    string key = Guid.NewGuid().ToString();
    string script = "$('#content').load(\"" + url + "\");";
    PanelCenter.Border = false;
    UtilityExt.LoadUrl(PanelCenter, url);
  }
}