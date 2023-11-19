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

public partial class GridLookup : System.Web.UI.Page
{
  WindowDebug WindowDebug1 = null;
  WindowAdmin[] WindowAdmin1 = new WindowAdmin[] {
    new WindowAdmin(0,BOSysUtils.ID_COL_GRID),
  };
  protected void Page_Load(object sender, EventArgs e)
  {
    if (GlobalAsp.CekSessionGrid(Page))
    {
      int id = GlobalExt.GetRequestI();
      WindowDebug1 = new WindowDebug(id, GlobalAsp.GetURLSessionKey());

      int target = int.Parse(Request["target"]);
      IDataControlUI dcCaller = UtilityUI.GetDataControl(id);
      IDataControlUI dc = UtilityUI.GetDataControlLookup(id);
      //if (dc == null)
      //{
      //  string cname = ((ViewListProperties)dcCaller.GetProperties()).LookupDC;
      //  dc = (IDataControlUI)UtilityUI.Create(cname);
      //  dc.SetFilterKey((BaseBO)dcCaller);
      //  ((ViewListProperties)dc.GetProperties()).ViewLabelQuery = ((ViewListProperties)dcCaller.GetProperties()).LookupLabelQuery;
      //}
      #region Custom JS Script
      if (typeof(IHasJSScript).IsInstanceOfType(dc))
      {
        IHasJSScript thisdc = (IHasJSScript)dc;
        string[] scripts = thisdc.GetScript();
        if (scripts != null)
        {
          for (int i = 0; i < scripts.Length; i++)
          {
            ClientScript.RegisterStartupScript(typeof(string), "dcscript" + i, scripts[i], true);
          }
        }
      }
      #endregion
      #region Add Grid Filter
      ExtGridFilter formFilter = new ExtGridFilter(id, dc, ExtGridPanelFilter.GRID);
      if (formFilter.RowCount > 0)
      {
        PanelCenter.Items.Add(formFilter);
        if (!X.IsAjaxRequest)
        {
          CoreNETCompositeField.SetValues(formFilter, dc);
        }
      }
      #endregion
      #region Add Grid Panel
      Title = ((ViewListProperties)dc.GetProperties()).TitleFilter;
      ExtStore.SettingStore(Store1, dc, Page);
      GridPanel gridPanel = new ExtGridPanel(id, dc, ExtGridPanel.MODE_PAGE_GRIDLOOKUP);
      gridPanel.Title = Title;
      gridPanel.StoreID = "Store1";
      PanelCenter.Items.Add(gridPanel);
      #endregion
      #region Add Grid Panel PagingToolbar
      PagingToolbar pagetoolbar = ExtGridPanel.BuildPagingToolbarEmpty(dc, 1);
      gridPanel.TopBar.Add(pagetoolbar);
      string strid = id.ToString();
      string parentid = (strid.Substring(0, strid.Length - 1)).ToString();

      Ext.Net.Button btnSave = new Ext.Net.Button() { ID = GlobalExt.BTN_SAVE1, Icon = Icon.Disk };
      btnSave.Text = ConstantDictExt.Translate(btnSave.ID);
      //((target == ExtGridPanelFilter.TREE) ? "parent.refreshTree();" : "parent.refreshData();") + 
      btnSave.Listeners.Click.Handler = @"
        CoreNET.btnSaveForm(" + target + @",Ext.encode(#{GridPanel1}.getRowsValues({selectedOnly : true})));
        if(!!parent.WindowLookup1)
        {
          parent.WindowLookup1.hide(this);
        }else
        {
          parent.PanelFormEntry.hide(this);
        }
      ";
      pagetoolbar.Add(btnSave);
      Ext.Net.Button btnBack = new Ext.Net.Button() { ID = GlobalExt.BTN_BACK1, Icon = Icon.Reload };
      btnBack.Text = ConstantDictExt.Translate(btnBack.ID);
      btnBack.Listeners.Click.Handler = @"
        if(!!parent.WindowLookup1)
        {
          parent.WindowLookup1.hide(this);
        }else
        {
          parent.PanelFormEntry.hide(this);
        }";
      pagetoolbar.Add(btnBack);

      #region Debugging
      if (MasterAppConstants.Instance.StatusTesting)
      {
        Ext.Net.Button btnMenu = new Ext.Net.Button() { ID = GlobalAsp.BTN_MENU1, Icon = Icon.Wrench };
        btnMenu.Text = ConstantDictExt.Translate(btnMenu.ID);
        btnMenu.ToolTip = ConstantDictExt.TranslateTabTip(btnMenu);

        Ext.Net.Menu menuDebug = new Ext.Net.Menu() { };
        btnMenu.Menu.Add(menuDebug);

        string typename = UtilityBO.GetClassLibName(dc);
        Ext.Net.MenuItem mnInfo = new Ext.Net.MenuItem() { ID = GlobalAsp.MENU_INFO1, Icon = Icon.PageLightning };
        mnInfo.Text = ConstantDictExt.Translate(mnInfo.ID);
        mnInfo.Listeners.Click.Handler = string.Format("CoreNET.btnDebugClick('{0}')", typename);
        menuDebug.Items.Add(mnInfo);
        menuDebug.Items.Add(new Ext.Net.MenuSeparator());

        Ext.Net.MenuItem mngrid = new Ext.Net.MenuItem() { ID = GlobalAsp.MENU_GRID1, Text = "Info", Icon = Icon.Table };
        mngrid.Text = ConstantDictExt.Translate(mngrid.ID);
        mngrid.Listeners.Click.Handler = string.Format("CoreNET.btnAdminClick(0,'{0}')", typename);
        menuDebug.Items.Add(mngrid);

        pagetoolbar.Add(btnMenu);
      }
      #endregion

      Ext.Net.Label lblWait = new Ext.Net.Label() { ID = "lblWait" };
      lblWait.Html = @"<div id='loading-mask' style='display:none'><div id='loading'><div class='loading-indicator'>Loading...</div></div></div>";
      pagetoolbar.Add(lblWait);

      #endregion
      if (!X.IsAjaxRequest)
      {
        HttpContext.Current.Session[GlobalExt.SESSION_LIST_LOOKUP_ROWS + id] = null;
        ExtStore.BindStore(Store1, dc, false, false);//@todo HttpContext.Current.Session[GlobalExt.SESSION_LIST_LOOKUP_ROWS + id] diset
        if (typeof(IExtDataControl).IsInstanceOfType(dc))
        {
          SetTotal((IExtDataControl)dc);
        }
      }
    }
    else
    {
      X.Js.Call("home");
    }
  }
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void btnDebugClick(string typename)
  {
    WindowDebug1.AutoLoad.Url += "&type=" + typename;
    WindowDebug1.Render(Page.Form);
    WindowDebug1.LoadContent();
    WindowDebug1.Show();
  }
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void btnAdminClick(int idx, string typename)
  {
    WindowAdmin1[idx].AutoLoad.Url += "&type=" + typename;
    WindowAdmin1[idx].Render(Page.Form);
    WindowAdmin1[idx].LoadContent();
    WindowAdmin1[idx].Show();
  }
  public void store_RefreshData(object sender, StoreRefreshDataEventArgs e)
  {
    int id = GlobalExt.GetRequestI();
    Store store = (Store)sender;
    IDataControlUI dcCaller = UtilityUI.GetDataControl(id);
    IDataControlUI dc = (IDataControlUI)UtilityUI.GetDataControlLookup(id);
    if (dc == null)
    {
      string cname = ((ViewListProperties)dcCaller.GetProperties()).LookupDC;
      try
      {
        dc = (IDataControlUI)UtilityUI.Create(cname);
      }
      catch (Exception ex)
      {
        UtilityBO.Log(ex);
        X.Msg.Alert("Error", ex.Message + "=" + cname).Show();
        return;
      }
    }
    //dc.SetFilterKey((BaseBO)dcCaller);
    FormPanel formFilter = ControlUtils.FindControl<FormPanel>(Page.Form, "FormFilter1");
    CoreNETCompositeField.GetValues(formFilter, dc);
    dc.SetPageKey();
    ExtStore.BindStore(store, dc, e, false);
    #region SetTotal
    if (typeof(IExtDataControl).IsInstanceOfType(dc))
    {
      SetTotal((IExtDataControl)dc);
    }
    #endregion

  }
  public void SetTotal(IExtDataControl dc)
  {
    if (dc != null)
    {
      dc.SetTotal(Page.Form);
    }
  }
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void btnSaveForm(int target, string json)
  {
    int id = GlobalExt.GetRequestI();
    IDataControlUIEntry dcCaller = (IDataControlUIEntry)UtilityUI.GetDataControl(id);

    IDataControlUI dc = (IDataControlUI)UtilityUI.GetDataControlLookup(id);
    if (dc == null)
    {
      string cname = ((ViewListProperties)dcCaller.GetProperties()).LookupDC;
      try
      {
        dc = (IDataControlUI)UtilityUI.Create(cname);
      }
      catch (Exception ex)
      {
        UtilityBO.Log(ex);
        X.Msg.Alert("Error", ex.Message + "=" + cname).Show();
        return;
      }
    }
    Dictionary<string, object>[] data = JSON.Deserialize<Dictionary<string, object>[]>(json);

    foreach (Dictionary<string, object> row in data)
    {
      UtilityExt.SetValue(dc, row);
      dcCaller.SetPageKey();
      dcCaller.SetPrimaryKey();
      dcCaller.SetFilterKey((BaseBO)dc);//Jangan pake Idproperty, klo UniqueID bermasalah
      try
      {
        dcCaller.Insert();
      }
      catch (Exception ex)
      {
        UtilityBO.Log(ex);
      }
    }
    dcCaller.AfterInsert();
    if (target == ExtGridPanelFilter.TREE)
    {
      X.Js.Call("parent.refreshTree");
    }
    else
    {
      X.Js.Call("parent.refreshData");
    }
  }

}
