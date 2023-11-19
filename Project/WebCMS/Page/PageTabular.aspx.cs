using CoreNET.Common.Base;
using CoreNET.Common.BO;
using Ext.Net;
using Ext.Net.Utilities;
using System;
using System.Reflection;
using System.Web.UI;

public partial class PageTabular : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    CoreNET.Common.Base.AssemblyUtils.WriteBeginLog();
    if (GlobalAsp.CekSessionGrid(Page))
    {
      #region Inisialisasi
      UtilityUI.SetSessionExtPage(UtilityUI.PAGE_TABULAR);
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
      ((BaseBO)dc).ModePage = ViewListProperties.MODE_TABULAR;
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
      GridPanel gridPanel = new ExtGridPanel(id, dc, GridMenu, Title);
      PagingToolbar TopBar1 = ExtGridPanel.BuildPagingToolbarDefault(1);
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
        tbbtm.Cls = "bottom-text";
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
    CoreNET.Common.Base.AssemblyUtils.WriteEndLog();
  }

  private void BtnAdd_DirectClick(object sender, DirectEventArgs e)
  {
    PanelBase FormDetail = ExtFormDetail.GetPanelDetil();
    PanelFormEntry.Items.Add(FormDetail);
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
  //[DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  //public void RefreshMasterPage()
  //{
  //  X.Js.Call(@"(function() {
  //    refreshDataWithSelection();
  //  })");

  //}
  #endregion
}
