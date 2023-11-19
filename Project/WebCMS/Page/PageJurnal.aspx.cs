using CoreNET.Common.Base;
using CoreNET.Common.BO;
using Ext.Net;
using Ext.Net.Utilities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PageJurnal : System.Web.UI.Page
{
  #region Dialog Windows
  private Window WindowJurnal = new Window
  {
    ID = "WindowJurnal",
    Width = Unit.Pixel(ExtWindows.DEFAULT_WIDTH + 100),
    Height = Unit.Pixel(ExtWindows.DEFAULT_HEIGHT + 50),
    Modal = true,
    Resizable = false,
    Maximizable = false,
    Closable = false,
    Hidden = true,
    AutoLoad =
    {
      Url = "~/Dialog/GridBalance.aspx?app=" + GlobalAsp.GetRequestApp() + "&roleid=" + HttpContext.Current.Request["roleid"] + "&id=" + GlobalExt.GetRequestId() + "&mode=jurnal" +
      "&i=" + GlobalExt.GetRequestI() + "&win=" + HttpContext.Current.Request["win"],
      Mode = LoadMode.IFrame
    },
    Listeners =
    {
      Activate =
      {
        Handler = "this.loadContent();"
      }
    }
  };
  #endregion
  protected void Page_Load(object sender, EventArgs e)
  {
    if (GlobalAsp.CekSessionGrid(Page))
    {
      #region Inisialisasi
      UtilityUI.SetSessionExtPage(UtilityUI.PAGE_JURNAL);
      IDataControlUIEntry dc = null;
      #region Inisialisasi Data Control
      if (string.IsNullOrEmpty(Request["back"]) && string.IsNullOrEmpty(Request["passdc"]))
      {
        try
        {
          if (!Page.IsPostBack)
          {
            UtilityUI.ProcessInfo();
          }
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
      int id = GlobalExt.GetRequestI();
      int idprev = GlobalExt.GetRequestIPrev();
      dc = (IDataControlUIEntry)UtilityUI.GetDataControl(id);
      if (!Page.IsPostBack)
      {
        try
        {
          dc.SetPageKey();
          IDataControlUI dcprev = null;
          if (idprev == 0)
          {
            if (id.ToString().Length == 2)//GetMasterDC
            {
              idprev = id / 10;
              dcprev = (IDataControlUIEntry)UtilityUI.GetDataControlPrev(idprev);
            }
            else
            {
              dcprev = (IDataControlUIEntry)UtilityUI.GetDataControlPrev(id);
            }
          }
          else
          {
            dcprev = (IDataControlUIEntry)UtilityUI.GetDataControlPrev(idprev);
          }
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
      }
      #endregion

      if (!string.IsNullOrEmpty(Request["win"]))
      {
        BaseBO parent = (BaseBO)UtilityUI.GetDataControl(GlobalExt.GetRequestI());
        dc.SetFilterKey(parent);
      }
      ((BaseBO)dc).ModePage = ViewListProperties.MODE_TABULAR;
      Title = ((ViewListProperties)dc.GetProperties()).TitleList;
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
            ClientScript.RegisterStartupScript(typeof(string), "dcscript" + i, scripts[i], true);
          }
        }
      }
      #endregion
      #region Add Grid Filter
      ExtGridFilter formFilter = new ExtGridFilter(id, ExtGridPanelFilter.GRID);
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
      ExtStore.SettingStore(Store1, dc, Page);
      GridPanel gridPanel = new ExtGridPanel(id, dc, Title);
      PanelCenter.Items.Add(gridPanel);
      #endregion
      #region Add Grid Panel PagingToolbar
      PagingToolbar pagetoolbar = ExtGridPanel.BuildPagingToolbarEmpty(dc, 1);
      gridPanel.TopBar.Add(pagetoolbar);
      //if (((ViewListProperties)dc.GetProperties()).IsModeAdd())
      //{
      //  Ext.Net.Button btnAdd = new Ext.Net.Button() { ID = GlobalExt.BTN_ADD1, Icon = Icon.Add };
      //  btnAdd.Text = ConstantDictExt.Translate(btnAdd.ID);
      //  btnAdd.DirectClick += new ComponentDirectEvent.DirectEventHandler(btnAdd_DirectClick);
      //  pagetoolbar.Add(btnAdd);
      //}
      if (((ViewListProperties)dc.GetProperties()).IsModeAdd() || ((ViewListProperties)dc.GetProperties()).IsModeEdit())
      {
        Ext.Net.Button btnEdit = new Ext.Net.Button() { ID = "btnEdit" + gridPanel.ID, Text = "Edit", Icon = Icon.Pencil };
        btnEdit.DirectClick += new ComponentDirectEvent.DirectEventHandler(btnEdit_DirectClick);
        btnEdit.DirectEvents.Click.ExtraParams.Add(new Ext.Net.Parameter("Values", "#{GridPanel1}.getRowsValues({selectedOnly:true})", ParameterMode.Raw, true));
        pagetoolbar.Add(btnEdit);
      }
      WindowJurnal.Listeners.BeforeHide.Handler = @"
          CoreNET.btnDeleteNotBalance();
          ";

      #endregion
      #region Bottom Toolbar
      if (typeof(IExtDataControl).IsInstanceOfType(dc))
      {
        Toolbar tbbtm = (Toolbar)gridPanel.BottomBar[0];
        ((IExtDataControl)dc).SetTotalFields(tbbtm);
      }
      #endregion
      #region WindowHelp
      if (HttpContext.Current.Request["enable"] != "0")
      {
        Ext.Net.Button btnHelp = new Ext.Net.Button() { ID = GlobalExt.BTN_HELP1, Icon = Icon.PageWorld };
        btnHelp.Text = ConstantDictExt.Translate(btnHelp.ID);
        btnHelp.ToolTip = ConstantDictExt.TranslateTabTip(btnHelp);
        btnHelp.Listeners.Click.Handler = "CoreNET.Methods1.btnHelpClick()";
        pagetoolbar.Add(btnHelp);
      }
      #endregion
      #region Debugging
      if (MasterAppConstants.Instance.StatusTesting)
      {
        Ext.Net.Button btnDebug = new Ext.Net.Button() { ID = GlobalExt.BTN_DEBUG1, Icon = Icon.PageLightning };
        btnDebug.Text = ConstantDictExt.Translate(btnDebug.ID);
        btnDebug.ToolTip = ConstantDictExt.TranslateTabTip(btnDebug);
        btnDebug.Listeners.Click.Handler = "CoreNET.Methods1.btnDebugClick()";
        pagetoolbar.Add(btnDebug);
      }
      #endregion
      #region !X.IsAjaxRequest
      if (!X.IsAjaxRequest)
      {
        X.Js.Call(@"(function() {Store1.reload();})");
        //ExtStore.BindStore(Store1, dc, true);
        //if (typeof(IExtDataControl).IsInstanceOfType(dc))
        //{
        //  dc.SetPageKey();
        //  SetTotal((IExtDataControl)dc);
        //}
      }
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
            <ul>
            <li>IList list = dc.View();//" + DateTime.Now.ToString() + @"</li>
            <li>HttpContext.Current.Session[" + GlobalExt.SESSION_LIST_ROWS + id + @"] = list;</li>
            </ul>
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
  public void btnDeleteNotBalance()
  {
    int id = GlobalExt.GetRequestI();
    IDataControlUIEntry dc = null;
    dc = (IDataControlUIEntry)UtilityUI.GetDataControl(id);
    try
    {
      Exception ex = dc.ValidateTotal();
      if (ex != null)
      {
        dc.Delete("Jurnal");
        try
        {
          X.Js.Call("refreshData");
        }
        catch (Exception ex1) { UtilityBO.Log(ex1);; }
      }
    }
    catch (Exception ex)
    {
      UtilityBO.Log(ex);
    }
  }
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  private void btnAdd_DirectClick(object sender, DirectEventArgs e)
  {
    UtilityExt.SetModeEditable(UtilityExt.MODE_INSERT);
    //Session[ExtGridPanel.SESSION_MODE] = ExtGridPanel.MODE_INSERT;
    int id = GlobalExt.GetRequestI();
    IDataControlUIEntry dc = (IDataControlUIEntry)UtilityUI.GetDataControl(id);
    UtilityUI.ResetEntry(dc);
    dc.SetPageKey();
    dc.SetPrimaryKey();
    WindowJurnal.Title = ((ViewListProperties)dc.GetProperties()).TitleList + " - Add";
    WindowJurnal.Render(Form);
    WindowJurnal.Show();
    try
    {
      X.Js.Call("refreshData");
    }
    catch (Exception ex)
    {
      UtilityBO.Log(ex);
    }
  }

  private void btnEdit_DirectClick(object sender, DirectEventArgs e)
  {
    UtilityExt.SetModeEditable(UtilityExt.MODE_EDIT);
    //Session[ExtGridPanel.SESSION_MODE] = ExtGridPanel.MODE_EDIT;
    int id = GlobalExt.GetRequestI();
    IDataControlUIEntry dc = null;
    dc = (IDataControlUIEntry)UtilityUI.GetDataControl(id);
    string json = e.ExtraParams["Values"];

    #region from grid
    //Dictionary<string, string>[] datas = JSON.Deserialize<Dictionary<string, string>[]>(json);
    //foreach (Dictionary<string, string> row in datas)
    //{
    //  foreach (string key in row.Keys)
    //  {
    //    try
    //    {
    //      dc.GetProperty(key).SetValue(dc, row[key], null);
    //    }
    //    catch (Exception ex)
    //    {
    //      UtilityBO.Log(ex);
    //    }
    //  }
    //}
    #endregion
    //if (datas.Length > 0)
    //{
    WindowJurnal.Title = ((ViewListProperties)dc.GetProperties()).TitleList + " - Edit";
    WindowJurnal.Render(Form);
    WindowJurnal.Show();
    try
    {
      X.Js.Call("refreshData");
    }
    catch (Exception ex)
    {
      UtilityBO.Log(ex);
    }
    //}
    //else
    //{
    //  X.MessageBox.Alert(ConstantDictExt.Translate(GlobalExt.LBL_INFO), ConstantDictExt.Translate("LBL_ERROR_DATA_NOT_SELECTED")).Show();
    //}
  }
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  private void btnDelete_DirectClick(object sender, DirectEventArgs e)
  {
    int id = GlobalExt.GetRequestI();
    IDataControlUIEntry dc = null;
    dc = (IDataControlUIEntry)UtilityUI.GetDataControl(id);
    string json = e.ExtraParams["Values"];

    #region from grid
    Dictionary<string, string>[] datas = JSON.Deserialize<Dictionary<string, string>[]>(json);

    foreach (Dictionary<string, string> row in datas)
    {
      foreach (string key in row.Keys)
      {
        try
        {
          dc.GetProperty(key).SetValue(dc, row[key], null);
        }
        catch (Exception ex)
        {
          UtilityBO.Log(ex);
        }
      }
    }
    #endregion
    dc.Delete("Jurnal");
    try
    {
      X.Js.Call("refreshData");
    }
    catch (Exception ex)
    {
      UtilityBO.Log(ex);
    }
  }
  #endregion
}
