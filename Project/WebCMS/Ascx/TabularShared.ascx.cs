using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System.Xml.Xsl;
using System.Xml;
using System.Linq;
using System.IO;
using System.Text;
using System.Globalization;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CoreNET.Common.Base;
using Ext.Net;
using Ext.Net.Utilities;

public partial class Ascx_TabularShared : System.Web.UI.UserControl
{
  Hashtable WinFilters;
  Hashtable WinEntries;

  IDataControlUIEntry GetDataControl()
  {
    int id = GlobalExt.GetRequestI();
    string url = Request.Url.AbsoluteUri;
    IDataControlUIEntry dc = null;
    if (url.ToLower().Contains("dialog"))
    {
      dc = (IDataControlUIEntry)UtilityUI.GetDataControlLookup(id);
    }
    else
    {
      dc = (IDataControlUIEntry)UtilityUI.GetDataControl(id);
    }
    return dc;
  }
  protected void Page_Load(object sender, EventArgs e)
  {
    if (GlobalAsp.CekSessionGrid(Page))
    {
      int id = GlobalExt.GetRequestI();
      IDataControlUIEntry dc = (IDataControlUIEntry)GetDataControl();
      string url = Request.Url.AbsoluteUri;
      bool lookup = url.ToLower().Contains("dialog");
      WinFilters = ExtWindows.CreateLookupFilterWindow(Page, id, dc, ExtGridPanelFilter.GRID,lookup);
      WinEntries = ExtWindows.CreateLookupEntryWindow(Page, id);
    }
    else
    {
      X.Js.Call("home");
    }
  }
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void btnLookup_DirectClick(int mode, string par)
  {
    if (GlobalAsp.CekSessionGrid(Page))
    {
      if (mode == GlobalExt.FILTER)
      {
        ((Window)WinFilters[par]).Render(Page.Form);
        ((Window)WinFilters[par]).Show();
      }
      else
      {
        ((Window)WinEntries[par]).Render(Page.Form);
        ((Window)WinEntries[par]).Show();
      }
    }
  }

}