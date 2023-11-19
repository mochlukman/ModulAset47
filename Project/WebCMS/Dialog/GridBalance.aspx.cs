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
using CoreNET.Common.BO;
using Ext.Net;
using Ext.Net.Utilities;

public partial class GridBalance : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (GlobalAsp.CekSessionGrid(Page))
    {
      #region Inisialisasi
      UtilityUI.SetSessionExtPage(UtilityUI.GRID_BALANCE);
      int id = GlobalExt.GetRequestI();
      IDataControlUIEntry dc = (IDataControlUIEntry)UtilityUI.GetDataControl(id);
      try
      {
        ((BaseBO)dc).ModePage = ViewListProperties.MODE_TABULAR;
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
      }
      catch (Exception ex)
      {
        WindowDebug.ShowMessage(Page, ConstantDictExt.TranslateException(ex, MethodBase.GetCurrentMethod().Name));
        return;
      }
      Page.Title = ((ViewListProperties)dc.GetProperties()).TitleList;
      #endregion
      #region Custom JS Script
      if (typeof(IHasJSScript).IsInstanceOfType(dc))
      {
        IHasJSScript thisdc = (IHasJSScript)dc;
        string[] scripts = thisdc.GetScript();
        if (scripts != null)
        {
          for (int i = 0; i < scripts.Length; i++)
          {
            ClientScript.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), scripts[i], true);
          }
        }
      }
      #endregion
      #region Add Grid Panel
      ExtStore.SettingStore(Store1, dc, Page);
      GridPanel gridPanel = new ExtGridPanel(id, dc, GridMenu, Title);
      PagingToolbar TopBar1 = ExtGridPanel.BuildPagingToolbarDefault(1);
      Ext.Net.Button btnSaveWin = new Ext.Net.Button() { ID = GlobalExt.BTN_SAVE_WIN1, Icon = Icon.Disk };
      btnSaveWin.Text = ConstantDictExt.Translate(btnSaveWin.ID);
      btnSaveWin.Listeners.Click.Handler = @"
          CoreNET.btnSaveClick();
          ";
      TopBar1.Add(btnSaveWin);
      gridPanel.TopBar.Add(TopBar1);
      PanelCenter.Items.Add(gridPanel);
      #endregion
      #region Add Form Entry
      ExtFormDetail.SetToolbarForm(TBDialog1, true);
      PanelBase FormDetail = ExtFormDetail.GetPanelDetil();
      PanelFormEntry.Items.Add(FormDetail);
      #endregion
      #region Bottom Toolbar
      if (typeof(IExtDataControl).IsInstanceOfType(dc))
      {
        Toolbar tbbtm = (Toolbar)gridPanel.BottomBar[0];
        ((IExtDataControl)dc).SetTotalFields(tbbtm);
      }
      #endregion
      #region !X.IsAjaxRequest
      if (!X.IsAjaxRequest)
      {
        //X.Js.Call(@"(function() {Store1.reload();})");/*efeknya ngga langsung paging*/
        ExtStore.BindStore(Store1, dc, true, (Request["passlist"] == "1"));//simpan list ke session
        if ((dc != null) && typeof(IExtDataControl).IsInstanceOfType(dc))
        {
          ((IExtDataControl)dc).SetTotal(Page.Form);
        }
        ExtGridPanel.SetRegisteredIcons(ResourceManager1);
      }
      #endregion
      #region Locale and Culture
      Page.UICulture = "id-ID";
      ResourceManager1.Locale = "id-ID";
      #endregion
    }
    else
    {
      X.Js.Call("home");
    }
  }
  public void store_RefreshData(object sender, StoreRefreshDataEventArgs e)
  {
    int id = GlobalExt.GetRequestI();
    Store store = (Store)sender;
    IDataControlUI dc = UtilityUI.GetDataControl(id);
    FormPanel formFilter = ControlUtils.FindControl<FormPanel>(Page.Form, "FormFilter1");
    try
    {
      CoreNETCompositeField.GetValues(formFilter, dc);
      dc.SetPageKey();
      ExtStore.BindStore(store, dc, e);
      try
      {
        if (MasterAppConstants.Instance.StatusTesting)
        {
          string msg = @"
            [PageTabular]<ul>
            <li>IList list = dc.View();//" + DateTime.Now.ToString() + @"</li>
            <li>HttpContext.Current.Session[" + GlobalExt.SESSION_LIST_ROWS + id + @"] = list;</li>
            </ul>[/PageTabular]<br/>
        ";
          dc.SetValue("Debug", msg);
        }
      }
      catch (Exception ex)
      {
        UtilityBO.Log(ex);
      }
    }
    catch (Exception ex)
    {
      WindowDebug.ShowMessage(Page, ConstantDictExt.TranslateException(ex, MethodBase.GetCurrentMethod().Name));
      return;
    }
    #region SetTotal

    if ((dc != null) && typeof(IExtDataControl).IsInstanceOfType(dc))
    {
      ((IExtDataControl)dc).SetTotal(Page.Form);
    }
    #endregion

  }
  #region DirectMethods
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void btnSaveClick()
  {
    int id = GlobalExt.GetRequestI();
    IDataControlUIEntry dc = null;
    dc = (IDataControlUIEntry)UtilityUI.GetDataControl(id);
    IList list = dc.View();
    decimal debet = 0;
    decimal kredit = 0;
    foreach(IDataControl ctr in list)
    {
      debet += (decimal)ctr.GetValue("Debet");
      kredit += (decimal)ctr.GetValue("Kredit");
    }

    if (debet == kredit)
    {
      X.Js.Call("closewin");
    }
    else
    {
      X.Msg.Alert(ConstantDictExt.Translate(GlobalExt.LBL_INFO), ConstantDict.Translate("LBL_INVALID_POSTING")).Show();
    }

  }

  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void FilterClick(string key)
  {
    int id = GlobalExt.GetRequestI();
    IDataControlUIEntry dc = (IDataControlUIEntry)UtilityUI.GetDataControl(id);
    dc.FilterClick(key);
    FormPanel formFilter = ControlUtils.FindControl<FormPanel>(Page.Form, "FormFilter1");
    CoreNETCompositeField.SetValues(formFilter, dc);
  }
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void RefreshPreview()
  {
    int id = GlobalExt.GetRequestI();
    IDataControlUIEntry dc = (IDataControlUIEntry)UtilityUI.GetDataControl(id);

    string url = Request.UrlReferrer.OriginalString;
    if (typeof(IDataControlTreeGrid3).IsInstanceOfType(dc))
    {
      url = url.Replace("PageTabular.aspx", "PageTreeGrid.aspx");
    }
    if (!url.Contains("passdc"))
    {
      url += "&passdc=1";
      dc.SetColumnsNull();
    }
    Response.Redirect(url);
  }
  #endregion
}
