using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CoreNET.Common.Base;
using CoreNET.Common.BO;
using Ext.Net;
using Ext.Net.Utilities;

public partial class PageMasterDetil : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (GlobalAsp.CekSessionPage(Page))
    {
      int id = GlobalExt.GetRequestI();
      if ((!Page.IsPostBack) && (!X.IsAjaxRequest))
      {
        try
        {
          UtilityUI.ProcessInfo();
        }
        catch (Exception ex)
        {
          if (MasterAppConstants.Instance.StatusTesting)
          {
            throw new Exception(ex.Message + " at " + ex.StackTrace);
          }
          else
          {
            WindowDebug.ShowMessage(Page, ConstantDictExt.TranslateException(ex, MethodBase.GetCurrentMethod().Name));
            return;
          }
        }
      }
      IDataControlUI dcmaster = null;
      try
      {
        dcmaster = (IDataControlUI)UtilityUI.GetDataControl(id);
      }
      catch (Exception ex)
      {
        WindowDebug.ShowMessage(Page, ConstantDictExt.TranslateException(ex, MethodBase.GetCurrentMethod().Name));
        return;
      }
      Dictionary<string, object> Props = ((IPageMaster)dcmaster).GetPageMasterProperties();

      string key = PageMasterProperties.MODE;
      string mode = !Props.ContainsKey(key) ? string.Empty : (string)Props[key];
      if (mode.Equals(PageMasterProperties.MODE_LEFT_RIGHT))
      {
        Panel1.Region = Region.West;
        TabPanel1.Region = Region.Center;
      }
      if (!X.IsAjaxRequest)
      {
        string url = UtilityExt.ValidateURL(this, (string)Props[PageMasterProperties.URLMASTER]);
        if (!url.Contains("passdc"))
        {
          url += "&passdc=1";
          dcmaster.SetColumnsNull();
        }
        Panel1.AutoLoad.Url = url;
        UtilityExt.SetIFrameAutoLoad(Panel1.AutoLoad);
        Panel1.LoadContent();
      }

      string idmaster = "1";
      int nchild = PageMasterProperties.GetNChilds((IPageMaster)dcmaster);
      if (nchild > 0)
      {
        for (int n = 1; n <= nchild; n++)
        {
          string i = idmaster + n;
          id = int.Parse(i);
          IDataControlUIEntry dcdetil = (IDataControlUIEntry)UtilityUI.GetDataControl(id);
          if (dcdetil == null)
          {
            X.Msg.Alert(ConstantDictExt.Translate(GlobalExt.LBL_INFO), ConstantDictExt.Translate("LBL_ERROR_MAP_CLASS")).Show();
            return;
          }

          string url = UtilityExt.ValidateURL(this, ((string[])Props[PageMasterProperties.URLDETILS])[n - 1]);
          if (!url.Contains("passdc"))
          {
            url += "&passdc=1";
            dcdetil.SetColumnsNull();
          }

          string title = ((ViewListProperties)dcdetil.GetProperties()).TitleList;

          Ext.Net.Panel pnl = new Ext.Net.Panel() { Region = Region.Center };
          pnl.ID = "PnlDetil" + i;
          pnl.ToolTip = title;
          pnl.Title = title;
          pnl.AutoLoad.Url = UtilityExt.ValidateURL(this, url);
          UtilityExt.SetIFrameAutoLoad(pnl.AutoLoad);
          //pnl.AutoLoad.TriggerEvent = "show";
          TabPanel1.Add(pnl);

        }
        /*Jangan refresh detil disini*/
      }
    }
    else
    {
      X.Js.Call("home");
    }
  }
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void ExpandBotFrame()
  {
    Panel1.Collapse();
  }

  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void ExpandTopFrame()
  {
    Panel1.Height = 600;
    TabPanel1.Hide();
  }

  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void NormalizeFrame()
  {
    Panel1.Height = 250;
    Panel1.Expand();
    TabPanel1.Show();
  }
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void RefreshDetilPage(string json)
  {
    IDataControlUI dcmaster = null;
    int idmaster = GlobalExt.GetRequestI();
    try
    {
      dcmaster = UtilityUI.GetDataControl(idmaster);
    }
    catch (Exception ex)
    {
      WindowDebug.ShowMessage(Page, ConstantDictExt.TranslateException(ex, MethodBase.GetCurrentMethod().Name));
    }
    Dictionary<string, object> Props = ((IPageMaster)dcmaster).GetPageMasterProperties();
    if (!string.IsNullOrEmpty(json))
    {
      UtilityExt.SetValue(dcmaster, json);
    }
    else
    {
      UtilityUI.ResetFields(dcmaster);
    }

    Ext.Net.TabPanel TabPanel1 = ControlUtils.FindControl<Ext.Net.TabPanel>(Viewport1, "TabPanel1");
    int nchild = PageMasterProperties.GetNChilds((IPageMaster)dcmaster);
    for (int n = 1; n <= nchild; n++)
    {
      string i = idmaster.ToString() + n;
      int id = int.Parse(i);
      IDataControlUIEntry dcdetil = (IDataControlUIEntry)UtilityUI.GetDataControl(id);
      Ext.Net.Panel panel = (Ext.Net.Panel)TabPanel1.Items[n - 1];
      panel.Title = ((ViewListProperties)dcdetil.GetProperties()).TitleList;

      //Jangan lupa, dcmaster dan dcdetil, passdc=1
      //Contoh [CoreNET.Common.BO.TrpurchaseControl, Diasoft.AKM]
      dcdetil.SetFilterKey((BaseBO)dcmaster);

      if (((ViewListProperties)dcmaster.GetProperties()).CanUpdateDetilState)
      {
        panel.LoadContent();
      }
      else
      {
        X.Js.Call(panel.ID + @".getBody().refreshDataWithSelection");
      }
    }
    NormalizeFrame();
  }
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void RefreshMasterPage()
  {
    IDataControlUI dcdetil = null;
    int iddetil = GlobalExt.GetRequestI();
    try
    {
      dcdetil = UtilityUI.GetDataControl(iddetil);
    }
    catch (Exception ex)
    {
      WindowDebug.ShowMessage(Page, ConstantDictExt.TranslateException(ex, MethodBase.GetCurrentMethod().Name));
    }
    if (((ViewListProperties)dcdetil.GetProperties()).RefreshMaster)
    {
      X.Js.Call(@"(function() {
        Panel1.getBody().refreshDataWithSelection();
      })");
    }
    else
    {
      X.Js.Call(@"(function() {
        Panel1.getBody().refreshDataWithSelection();
      })");
    }


  }
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void RedirectSingle()
  {
    IPageMaster dc = (IPageMaster)UtilityUI.GetDataControl(GlobalExt.GetRequestI());
    Dictionary<string, object> props = dc.GetPageMasterProperties();
    Response.Redirect((string)props[PageMasterProperties.URLMASTER]);
  }
}