using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System.IO;
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

public partial class GridBalance1 : System.Web.UI.Page
{
  WindowDebug WindowDebug1 = null;
  WindowAdmin[] WindowAdmin1 = new WindowAdmin[] {
    new WindowAdmin(0,BOSysUtils.ID_COL_GRID),
  };
  Hashtable WinEntries;
  protected void Page_Load(object sender, EventArgs e)
  {
    if (GlobalAsp.CekSessionGrid(Page))
    {
      #region Inisialisasi
      int id = GlobalExt.GetRequestI();
      WindowDebug1 = new WindowDebug(id, GlobalAsp.GetURLSessionKey());
      IDataControlUIEntry dc = null;
      dc = (IDataControlUIEntry)UtilityUI.GetDataControl(id);
      Page.Title = ((ViewListProperties)dc.GetProperties()).TitleList;
      ExtStore.SettingStore(Store1, dc, Page);
      #endregion
      #region Add Grid Panel
      GridPanel gridPanel = new ExtGridPanel(id, dc, false, true);
      gridPanel.Width = Unit.Pixel(300);
      gridPanel.AutoWidth = false;
      gridPanel.Title = Title;
      gridPanel.StoreID = "Store1";
      PanelCenter.Items.Add(gridPanel);
      #endregion
      #region Add Grid Panel PagingToolbar
      PagingToolbar pagetoolbar = ExtGridPanel.BuildPagingToolbarEmpty(dc, 1);
      gridPanel.TopBar.Add(pagetoolbar);
      Ext.Net.Button btnAdd = new Ext.Net.Button() { ID = GlobalExt.BTN_ADD1, Icon = Icon.Add };
      btnAdd.Text = ConstantDictExt.Translate(btnAdd.ID);
      btnAdd.Listeners.Click.Handler = @"
          CoreNET.Methods1.btnAddClick();
          var form = #{FPFormEntry1};
          form.reset();
          for(var i in form.items.items){
            if(form.items.items.hasOwnProperty(i)){
              var composite = form.items.items[i];
              if(composite.items){
                for(var j in composite.items.items){
                  if(composite.items.items.hasOwnProperty(j)){
                    var input = composite.items.items[j].el.dom;
                    input.hasAttribute = input.hasAttribute || function(){return false;};
                    if(input.hasAttribute('readonly')){
                      input.removeAttribute('readonly');
                    }
                  }
                }
              }
            }
          }";
      Ext.Net.Button btnDel = new Ext.Net.Button() { ID = GlobalExt.BTN_DEL1, Icon = Icon.Delete };
      btnDel.Text = ConstantDictExt.Translate(btnDel.ID);
      btnDel.Listeners.Click.Handler = @"
            sels = (#{GridPanel1}.getRowsValues({selectedOnly : true}))
            CoreNET.Methods1.btnDeleteClick(sels);";
      Ext.Net.Button btnSaveWin = new Ext.Net.Button() { ID = GlobalExt.BTN_SAVE_WIN1, Icon = Icon.Disk };
      btnSaveWin.Text = ConstantDictExt.Translate(btnSaveWin.ID);
      btnSaveWin.Listeners.Click.Handler = @"
          CoreNET.Methods1.btnSaveClick();
          ";
      pagetoolbar.Add(btnAdd);
      pagetoolbar.Add(btnDel);
      pagetoolbar.Add(btnSaveWin);
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
      Toolbar tbbtm = (Toolbar)gridPanel.BottomBar[0];
      tbbtm.Add(new ToolbarFill());
      tbbtm.Add(new DisplayField() { ID = "DfTotalDebet", Text = "Debet" });
      tbbtm.Add(new ToolbarSeparator());
      tbbtm.Add(new DisplayField() { ID = "DfTotalKredit", Text = "Kredit" });
      //tbbtm.Add(new ToolbarSeparator());
      //tbbtm.Add(new DisplayField() { ID = "DfTotalJrdebet", Text = "JR Debet" });
      //tbbtm.Add(new ToolbarSeparator());
      //tbbtm.Add(new DisplayField() { ID = "DfTotalJrkredit", Text = "JR Kredit" });
      //tbbtm.Add(new ToolbarSeparator());
      #endregion
      #region Add Form Entry
      WindowFormJurnal.Width = Unit.Pixel(ExtWindows.DEFAULT_WIDTH);
      WindowFormJurnal.Height = Unit.Pixel(ExtWindows.DEFAULT_HEIGHT);
      FormPanel FPFormEntry1 = new FormPanel
      {
        ID = "FPFormEntry1",
        Region = Region.Center,
        Padding = 5,
        Margins = "0 0 5 5",
        AutoDoLayout = true,
        AutoScroll = true
      };
      HashTableofParameterRow hps = dc.GetEntries();
      string[] keys = hps.GetOrderKeys();
      ArrayList arkeys = new ArrayList();
      foreach (string key in keys)
      {
        string[] fields = key.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        arkeys.AddRange(fields);
        CoreNETCompositeField cf = new CoreNETCompositeField((ParameterRow)hps[key], ExtGridPanelFilter.GRID);
        FPFormEntry1.Items.Add(cf);
      }
      string[] pks = ((ViewListProperties)dc.GetProperties()).PrimaryKeys;
      foreach (string key in pks)
      {
        if (!arkeys.Contains(key))
        {
          TextField textField = new TextField();
          textField.Hidden = true;
          textField.DataIndex = key;
          textField.AnchorHorizontal = "95%";
          FPFormEntry1.Items.Add(textField);
        }
      }
      WindowFormJurnal.Add(FPFormEntry1);
      #endregion
      #region Add Lookup Entry Window
      WinEntries = ExtWindows.CreateLookupEntryWindow(Page, id);
      #endregion
      #region Add Form Entry Toolbar
      Ext.Net.Button btnSave = new Ext.Net.Button() { ID = GlobalExt.BTN_SAVE1, Icon = Icon.Disk };
      btnSave.Text = ConstantDictExt.Translate(btnSave.ID);
      btnSave.Listeners.Click.Handler = @"
        CoreNET.Methods1.btnSaveForm();
        #{WindowFormJurnal}.hide(this);
        if (!!parent) 
        {
          if(!!parent.MyMethods){
            parent.MyMethods.NormalizeFrame();
          }
        }
        //update 17/09/2019
        parent.refreshDataWithSelection();
        //update 07/06/2019
        //deprecated
      ";
      TBDialog1.Add(btnSave);
      Ext.Net.Button btnBack = new Ext.Net.Button() { ID = GlobalExt.BTN_BACK1, Icon = Icon.Reload };
      btnBack.Text = ConstantDictExt.Translate(btnBack.ID);
      btnBack.Listeners.Click.Handler =
      WindowFormJurnal.Listeners.Hide.Handler = @"
        #{WindowFormJurnal}.hide(this);
        if (!!parent) 
        {
          if(!!parent.MyMethods){
            parent.MyMethods.NormalizeFrame();
          }
        }
      ";
      TBDialog1.Add(btnBack);
      #endregion
      #region !X.IsAjaxRequest
      if (!X.IsAjaxRequest)
      {
        ExtStore.BindStore(Store1, dc);
        SetTotal(dc);
      }
      #endregion
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
    Store store = (Store)sender;
    int id = GlobalExt.GetRequestI();
    IDataControlUIEntry dc = null;
    dc = (IDataControlUIEntry)UtilityUI.GetDataControl(id);
    FormPanel formFilter = ControlUtils.FindControl<FormPanel>(Page.Form, "FormFilter1");
    CoreNETCompositeField.GetValues(formFilter, dc);
    ExtStore.BindStore(store, dc);
    SetTotal(dc);

  }
  public void SetTotal(IDataControl dc)
  {
    IList list = ((BaseDataControl)dc).View(BaseDataControl.JURNAL);
    if (list.Count > 0)
    {
      dc = (IDataControlUIEntry)list[0];
      DisplayField dfjpd = ControlUtils.FindControl<DisplayField>(Page.Form, "DfTotalDebet");
      DisplayField dfjpk = ControlUtils.FindControl<DisplayField>(Page.Form, "DfTotalKredit");
      //DisplayField dfjrd = ControlUtils.FindControl<DisplayField>(Page.Form, "DfTotalJrdebet");
      //DisplayField dfjrk = ControlUtils.FindControl<DisplayField>(Page.Form, "DfTotalJrkredit");
      dfjpd.Text = "Debet = " + ((decimal)(dc.GetValue("Debet"))).ToString("#,##0.00");
      dfjpk.Text = "Kredit = " + ((decimal)(dc.GetValue("Kredit"))).ToString("#,##0.00");
      //dfjrd.Text = "JR Debet = " + ((decimal)(dc.GetProperty("Jrdebet").GetValue(dc, null))).ToString("#,##0.00");
      //dfjrk.Text = "JR Kredit = " + ((decimal)(dc.GetProperty("Jrkredit").GetValue(dc, null))).ToString("#,##0.00");
    }
  }
  #region DirectMethods

  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void GridDeleteSelected()
  {
    int id = GlobalExt.GetRequestI();
    ExtGridPanel gridPanel = ControlUtils.FindControl<ExtGridPanel>(this.PanelCenter, "GridPanel1");
    try
    {
      ((RowSelectionModel)gridPanel.GetSelectionModel()).ClearSelections();
    }
    catch (Exception) { }
  }
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void btnSaveClick()
  {
    int id = GlobalExt.GetRequestI();
    IDataControlUIEntry dc = null;
    dc = (IDataControlUIEntry)UtilityUI.GetDataControl(id);
    Exception ex = null;// dc.ValidateTotal();
    IList list = ((BaseDataControl)dc).View(BaseDataControl.JURNAL);
    if (list.Count > 0)
    {
      dc = (IDataControlUIEntry)list[0];
      decimal saldo = ((decimal)(dc.GetValue("Saldo")));
      if (saldo != 0)
      {
        ex = new Exception(ConstantDict.Translate("LBL_INVALID_POSTING"));
      }
    }
    if (ex == null)
    {
      X.Js.Call("closewin");
    }
    else
    {
      WindowDebug.ShowMessage(Page, ConstantDictExt.TranslateException(ex, MethodBase.GetCurrentMethod().Name));
    }

  }
  #endregion
}
