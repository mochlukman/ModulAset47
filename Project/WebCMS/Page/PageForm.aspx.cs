using CoreNET.Common.Base;
using Ext.Net;
using Ext.Net.Utilities;
using System;
using System.Reflection;
using System.Web.UI;

public partial class PageForm : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (GlobalAsp.CekSessionPage(Page))
    {
      #region Inisialisasi
      UtilityUI.SetSessionExtPage(UtilityUI.PAGE_FORM);
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
      ((BaseBO)dc).ModePage = ViewListProperties.MODE_FORM;
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
      if (string.IsNullOrEmpty(Request["me"]))
      {
        ExtGridFilter formFilter = new ExtGridFilter(id, ExtGridPanelFilter.GRID);
        if (formFilter.RowCount > 0)
        {
          PanelCenter.Items.Add(formFilter);
          if (!X.IsAjaxRequest)
          {
            CoreNETCompositeField.SetValues(formFilter, dc);
          }
        }
      }
      #endregion
      #region Add ExtStore
      ExtStore.SettingStore(Store1, dc, Page);
      #endregion
      #region Add Form Entry
      ExtFormDetail.SetToolbarForm(TBDialog1, true);
      PanelBase FormDetail = ExtFormDetail.GetPanelDetil();
      PanelFormEntry.Items.Add(FormDetail);
      #endregion
      #region !X.IsAjaxRequest
      if (!X.IsAjaxRequest)
      {
        //ExtStore.BindStore(Store1, dc, true, (Request["passlist"] == "1"));//simpan list ke session

        FormPanel FPFormEntry1 = ControlUtils.FindControl<FormPanel>(PanelFormEntry, "FPFormEntry1");
        dc.SetPageKey();
        dc.Load();
        //CoreNETCompositeField.SetValues(FPFormEntry1, dc);
        UtilityExt.PrepareForReview(FPFormEntry1, dc);
        if (((ViewListProperties)dc.GetProperties()).ModeToolbar == ViewListProperties.MODE_TOOLBAR_PRINT)
        {
          X.Js.Call("CoreNET.Methods1.btnEditClick('', 'EditForm')");
        }

        ExtGridPanel.SetRegisteredIcons(ResourceManager1);
        //X.Js.Call("");
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
    FormPanel FPFormEntry1 = ControlUtils.FindControl<FormPanel>(PanelFormEntry, "FPFormEntry1");
    try
    {
      CoreNETCompositeField.GetValues(formFilter, dc);
      dc.SetPageKey();
      dc.Load();
      CoreNETCompositeField.SetValues(FPFormEntry1, dc);
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
