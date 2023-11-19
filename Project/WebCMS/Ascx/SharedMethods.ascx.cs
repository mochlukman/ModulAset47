using CoreNET.Common.Base;
using CoreNET.Common.BO;
using Ext.Net;
using Ext.Net.Utilities;
using System;
using System.Collections;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Xsl;

public partial class Ascx_SharedMethods : System.Web.UI.UserControl
{
    #region Property
    private string CmdName = string.Empty;
    private Window WindowLoadCSV = null;
    private WindowDebug WindowDebug1 = null;
    private WindowPrint WindowPrint1 = null;
    private WindowSearch WindowSearch1 = new WindowSearch();
    private WindowHelp WindowHelp1 = new WindowHelp();
    private WindowForum WindowForum1 = new WindowForum(false);
    private WindowForum WindowForum2 = new WindowForum(true);
    private WindowAdmin[] WindowAdmin1 = new WindowAdmin[] {
    new WindowAdmin(0,BOSysUtils.ID_FORM_ENTRY),
    new WindowAdmin(1,BOSysUtils.ID_COL_GRID),
    new WindowAdmin(2,BOSysUtils.ID_COL_TREE),
    new WindowAdmin(3,BOSysUtils.ID_FILTERS),
    new WindowAdmin(4,BOSysUtils.ID_KEYS,true),
    new WindowAdmin(5,BOSysUtils.ID_SET_URL,true)
  };
    private Window WindowLookup1 = new Window
    {
        ID = "WindowLookup1",
        Width = Unit.Pixel(ExtWindows.DEFAULT_WIDTH),
        Height = Unit.Pixel(ExtWindows.DEFAULT_HEIGHT),
        Modal = true,
        Resizable = true,
        Maximizable = true,
        Hidden = true,
        AutoLoad =
    {
      Url = "~/Dialog/TreeLookup.aspx?app=" + GlobalAsp.GetRequestApp() + "&roleid=" + HttpContext.Current.Request["roleid"] + "&id=" + GlobalExt.GetRequestId() + "&enable=" + HttpContext.Current.Request["enable"] + "&at=" + HttpContext.Current.Request["at"] + "&kt=" + HttpContext.Current.Request["kt"] + "&pt=" + HttpContext.Current.Request["pt"] + "&pj=" + HttpContext.Current.Request["pj"] + "&i=" +
      GlobalExt.GetRequestI() + "&target=" + ExtGridPanelFilter.GRID,
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
        if (GlobalAsp.CekSession())
        {
            WindowDetil1.Width = Unit.Pixel(ExtWindows.DEFAULT_WIDTH);
            WindowDetil1.Height = Unit.Pixel(ExtWindows.DEFAULT_HEIGHT - 50);
            WindowView1.AnchorHorizontal = "0";
            WindowView1.AnchorVertical = "0";
            WindowView1.MinHeight = WindowView1.Height;
            WindowView1.MinWidth = WindowView1.Width;
            WindowView1.AutoDoLayout = true;
            WindowView1.Resizable = false;
            WindowView1.Minimizable = false;
            WindowView1.Maximizable = false;
            WindowView1.Layout = "Accordion";

            //WindowHelp1.AnchorHorizontal = "0";
            //WindowHelp1.AnchorVertical = "0";
            //WindowHelp1.MinHeight = WindowView1.Height;
            //WindowHelp1.MinWidth = WindowView1.Width;
            //WindowHelp1.AutoDoLayout = true;
            //WindowHelp1.Resizable = false;
            //WindowHelp1.Minimizable = false;
            //WindowHelp1.Maximizable = false;

            int id = GlobalExt.GetRequestI();
            WindowDebug1 = new WindowDebug(id, GlobalAsp.GetURLSessionKey());
            IDataControlUI dc = (IDataControlUI)UtilityUI.GetDataControl(id);
            if (typeof(IExtLoadCsv).IsInstanceOfType(dc))
            {
                IExtLoadCsv dcLoadCsv = (IExtLoadCsv)dc;
                WindowLoadCSV = dcLoadCsv.GetLoadCsvWindow();
            }
            if (!X.IsAjaxRequest)
            {
                bool editing = UtilityExt.IsModeEdit() || UtilityExt.IsModeAdd();
                SetModeComponentBefore(false, false);
                SetModeComponentAfter(false, string.Empty);
            }
            lblWaitLink.Html = @"<div id='loading-mask-link' style='display:none'><div id='loading'><div class='loading-indicator'>Loading...</div></div></div>";
        }
        else
        {
            X.Js.Call("home");
        }
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void ChangeModePreview()
    {
        int id = GlobalExt.GetRequestI();
        IDataControlUIEntry dc = (IDataControlUIEntry)UtilityUI.GetDataControl(id);

        ((BaseBO)dc).ChangeModePreviewIndex();
        string url = HttpContext.Current.Request.UrlReferrer.OriginalString;
        if (!url.Contains("passdc"))
        {
            url += "&passdc=1";
            dc.SetColumnsNull();
        }
        HttpContext.Current.Response.Redirect(url);
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void GridDeleteSelected()
    {
        int id = GlobalExt.GetRequestI();
        ExtGridPanel gridPanel = ControlUtils.FindControl<ExtGridPanel>(Page, "GridPanel1");
        try
        {
            ((RowSelectionModel)gridPanel.GetSelectionModel()).ClearSelections();
        }
        catch (Exception ex)
        {
            UtilityBO.Log(ex);
        }
    }

    //  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    //  public void RefreshDetilPage(string json)
    //  {
    //    X.Js.Call(@"
    //      if(!!parent){
    //        if(!!parent.CoreNET){
    //          parent.CoreNET.RefreshDetilPage(" + json + @");
    //        }
    //      }
    //      ");
    //  }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void SetMasterDetilPage()
    {
        Response.Redirect(GlobalExt.GetSessionMenu().GetURLReal().Replace("Page/", string.Empty));
    }
    //[DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    //public void RefreshMasterPage()
    //{
    //  int i = GlobalAsp.GetRequestI();
    //  IDataControlUI dc = UtilityUI.GetDataControl(i);

    //  if (((ViewListProperties)dc.GetProperties()).RefreshMaster)
    //  {
    //    //if (i.ToString().Length == 2)
    //    //{
    //      X.Js.Call(@"(function() {
    //        if (!!parent) 
    //        {
    //          if(!!parent.CoreNET){
    //            try
    //            {
    //              parent.CoreNET.RefreshMasterPage();
    //            }catch(Exception ex)
    //            {
    //              UtilityBO.Log(ex);
    //            }
    //          }
    //        }
    //      })");
    //    //}
    //  }
    //}
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void FilterClick(string key)
    {
        int id = GlobalExt.GetRequestI();
        IDataControlUI dc = UtilityUI.GetDataControl(id);
        FormPanel formFilter = ControlUtils.FindControl<FormPanel>(Page, "FormFilter1");
        if (formFilter != null)
        {
            dc.FilterClick(key);
            CoreNETCompositeField.SetValues(formFilter, dc);
        }
        ResetFormEntries();
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void SelectClick(string key, int target)
    {
        int id = GlobalExt.GetRequestI();
        IDataControlUI dc = UtilityUI.GetDataControl(id);
        FormPanel formFilter = ControlUtils.FindControl<FormPanel>(Page, "FormFilter1");
        if (formFilter != null)
        {
            dc.FilterClick(key);
            CoreNETCompositeField.SetValues(formFilter, dc);
        }
        ResetFormEntries();

        string strRefresh =
        ((target == ExtGridPanelFilter.GRID) ?
          "  Store1.reload();" :/*analisis dulu: gimana klo asinkronus?trus id and displaytext?*/
          "  refreshTree();");
        X.Js.Call(@"(function() {
    " + strRefresh + @"
    })");
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void EntryClick(string key)
    {
        int id = GlobalExt.GetRequestI();
        IDataControlUI dc = UtilityUI.GetDataControl(id);
        FormPanel FPFormEntry1 = ControlUtils.FindControl<FormPanel>(Page, "FPFormEntry1");
        if (FPFormEntry1 != null)
        {
            try
            {
                CoreNETCompositeField.GetValues(FPFormEntry1, dc);
                dc.FilterClick(key);
            }
            catch (Exception ex)
            {
                X.Js.Alert(ex.Message);
            }
            RefreshFormEntries();
        }
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void SetComboValue(string key, string value)
    {
        int id = GlobalExt.GetRequestI();
        IDataControlUI dc = UtilityUI.GetDataControl(id);
        FormPanel formFilter = ControlUtils.FindControl<FormPanel>(Page, "FormFilter1");
        if (formFilter != null)
        {
            dc.SetValue(key, value);
            dc.FilterClick(key);
            CoreNETCompositeField.SetValues(formFilter, dc);
        }
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void SetModeComponentBefore(bool editing, bool editclick)
    {
        if (GlobalAsp.CekSession())
        {
            int id = GlobalExt.GetRequestI();
            IDataControlUIEntry dc = (IDataControlUIEntry)UtilityUI.GetDataControl(id);
            dc.SetColumnsNull();
            dc.SetValue("Editable", editing);

            Ext.Net.PanelBase PanelFormEntry = ControlUtils.FindControl<Ext.Net.PanelBase>(Page, "PanelFormEntry");
            Ext.Net.TabPanel tp = ControlUtils.FindControl<Ext.Net.TabPanel>(PanelFormEntry, "TabPanel1");
            if (tp != null)
            {
                tp.SetActiveTab(0);//Biar Form Terisi
            }
            else
            {
                ////Ada PanelHelp
                //((Ext.Net.Panel)PanelFormEntry.Items[0]).LoadContent();
                //PanelFormEntry.LoadContent();
            }

            GridPanel GridPanel1 = ControlUtils.FindControl<GridPanel>(Page, "GridPanel1");
            if (GridPanel1 != null)
            {
                GridPanel1.Disabled = editing;
                ToolbarBase toolbar = ControlUtils.FindControl<ToolbarBase>(Page, "TopBar1");
                toolbar.Disabled = editing;
            }


            TreePanel TreeGrid1 = ControlUtils.FindControl<TreePanel>(Page, "TreeGrid1");
            if (TreeGrid1 != null)
            {
                ToolbarBase toolbar = ControlUtils.FindControl<ToolbarBase>(TreeGrid1, "TopBar1");
                TreeGrid1.Disabled = editing;//TreeGrid1.TopBar[0]; //
                if (toolbar != null)
                {
                    toolbar.Disabled = editing;
                }
            }

            Toolbar TBDialog1 = ControlUtils.FindControl<Toolbar>(Page, "TBDialog1");
            if (TBDialog1 != null)
            {
                //TBDialog1.Hidden = !editing;
                TBDialog1.Disabled = false; //!editing;

                try
                {
                    Ext.Net.Button btnAdd = ControlUtils.FindControl<Ext.Net.Button>(TBDialog1, "BTN_ADD2"); //(Ext.Net.Button)TBDialog1.Items[0];
                    if (btnAdd != null)
                    {
                        btnAdd.Disabled = editing || !((ViewListProperties)dc.GetProperties()).IsModeAdd();
                    }
                    Ext.Net.Button btnEdit = ControlUtils.FindControl<Ext.Net.Button>(TBDialog1, "BTN_EDIT2"); //(Ext.Net.Button)TBDialog1.Items[1];
                    if (btnEdit != null)
                    {
                        btnEdit.Disabled = editing || !((ViewListProperties)dc.GetProperties()).IsModeEdit();
                        if (UtilityExt.IsModeAddFromDialogForm())
                        {
                            btnEdit.Disabled = editing;
                        }
                    }
                    Ext.Net.Button btnSave = ControlUtils.FindControl<Ext.Net.Button>(TBDialog1, "BTN_SAVE1"); //(Ext.Net.Button)TBDialog1.Items[2];
                    if (btnSave != null)
                    {
                        btnSave.Disabled = !editing;
                    }
                    Ext.Net.Button btnCancel = ControlUtils.FindControl<Ext.Net.Button>(TBDialog1, "BTN_BACK1"); //(Ext.Net.Button)TBDialog1.Items[3];
                    if (btnCancel != null)
                    {
                        if (!UtilityUI.GetSessionMenuPage().Equals(UtilityUI.PAGE_FORM))
                        {
                            btnCancel.Disabled = false;
                        }
                        else
                        {
                            btnCancel.Disabled = !editing;
                        }
                    }
                }
                catch (Exception ex)
                {
                    UtilityBO.Log(ex);
                }
            }
            Ext.Net.Component c1 = ControlUtils.FindControl<Ext.Net.Component>(Page, "FormFilter1");
            if (c1 != null)
            {
                ArrayList array = new ArrayList(dc.GetFilters().GetAllComponentsIDKeyForSetComponent());
                if (editing)
                {
                    CoreNETCompositeField.SetBoolProperty(array, ((Ext.Net.FormPanel)c1), dc.GetFilters(), true);
                }
                else
                {
                    CoreNETCompositeField.SetBoolProperty(array, ((Ext.Net.FormPanel)c1), dc.GetFilters(), false);
                    array = dc.GetFilters().GetDisableKeys();
                    CoreNETCompositeField.SetBoolProperty(array, ((Ext.Net.FormPanel)c1), dc.GetFilters(), true);
                }
            }
        }
        else
        {
            //X.Js.Call("home");
        }
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void SetModeComponentAfter(bool editing, string cmdname)
    {
        int id = GlobalExt.GetRequestI();
        IDataControlUIEntry dc = (IDataControlUIEntry)UtilityUI.GetDataControl(id);
        //dc.SetColumnsNull();
        dc.SetValue("Editable", editing);

        Ext.Net.PanelBase PanelFormEntry = ControlUtils.FindControl<Ext.Net.PanelBase>(Page, "PanelFormEntry");
        Ext.Net.TabPanel tp = ControlUtils.FindControl<Ext.Net.TabPanel>(PanelFormEntry, "TabPanel1");
        int activetab = 0;
        string title = cmdname.Replace("Edit", string.Empty);
        int i = 0;
        if (tp != null)
        {
            PanelFormEntry.Reload();
            tp.Reload();

            if (!string.IsNullOrEmpty(title))
            {
                foreach (Component tab in tp.Items)
                {
                    if (typeof(FormPanel).IsInstanceOfType(tab))
                    {
                        FormPanel fp = ((FormPanel)tab);
                        if (fp.Title.ToLower().Equals(title.ToLower()))
                        {
                            activetab = i;
                        }
                    }
                    else if (typeof(Ext.Net.Panel).IsInstanceOfType(tab))
                    {
                        Ext.Net.Panel fp = ((Ext.Net.Panel)tab);
                        UtilityExt.SetIFrameAutoLoad(fp.AutoLoad);
                        if (fp.ID.ToLower().Contains(title.ToLower()))
                        {
                            activetab = i;
                        }
                        //fp.ClearContent();
                        //fp.RemoveAll();
                        //fp.LoadContent();//Untuk mereload URL baru, misal Request["tb"] berubah
                    }
                    i++;
                }
            }
            /*berfungsi klo single page*/
            tp.SetActiveTab(activetab);
        }
        else
        {
            if (PanelFormEntry != null)
            {
                foreach (Component tab in PanelFormEntry.Items)
                {
                    if (typeof(FormPanel).IsInstanceOfType(tab))
                    {
                        FormPanel fp = ((FormPanel)tab);
                        if (fp.Title.ToLower().Equals(title.ToLower()))
                        {
                            activetab = i;
                        }
                    }
                    else if (typeof(Ext.Net.Panel).IsInstanceOfType(tab))
                    {
                        Ext.Net.Panel fp = ((Ext.Net.Panel)tab);
                        fp.LoadContent();
                    }
                    i++;
                }
            }
        }
        if (PanelFormEntry != null)
        {
            PanelFormEntry.LoadContent();
        }
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void btnAddFromDlgLookup(string json)
    {
        UtilityExt.SetModeEditable(UtilityExt.MODE_INSERT);
        //Session[ExtGridPanel.SESSION_MODE] = ExtGridPanel.MODE_INSERT;
        int id = GlobalExt.GetRequestI();
        IDataControlUI dc = (IDataControlUI)UtilityUI.GetDataControl(id);

        IDataControlUI dclookup = (IDataControlUI)UtilityUI.GetDataControlLookup(id);
        if (dclookup == null)
        {
            string cname = ((ViewListProperties)dc.GetProperties()).LookupDC;
            try
            {
                dclookup = (IDataControlUI)UtilityUI.Create(cname);
            }
            catch (Exception ex)
            {
                UtilityBO.Log(ex);
                X.Msg.Alert("Error", ex.Message + "=" + cname).Show();
                return;
            }
            UtilityUI.SetDataControlLookup(id, dclookup);
        }
        dclookup.SetFilterKey((BaseBO)dc);
        if (dclookup != null)
        {
            if (((ViewListProperties)dc.GetProperties()).EntryStyle == ViewListProperties.ENTRY_STYLE_TREE)
            {
                WindowLookup1.AutoLoad.Url = "~/Dialog/TreeLookup.aspx?app=" + GlobalAsp.GetRequestApp() + "&roleid=" + HttpContext.Current.Request["roleid"] + "&id=" + GlobalExt.GetRequestId() + "&enable=" + HttpContext.Current.Request["enable"] + "&i=" +
                GlobalExt.GetRequestI() + "&target=" + ExtGridPanelFilter.GRID;
            }
            else if (((ViewListProperties)dc.GetProperties()).EntryStyle == ViewListProperties.ENTRY_STYLE_LOOKUP)
            {
                WindowLookup1.AutoLoad.Url = "~/Dialog/GridLookup.aspx?app=" + GlobalAsp.GetRequestApp() + "&roleid=" + HttpContext.Current.Request["roleid"] + "&id=" + GlobalExt.GetRequestId() + "&enable=" + HttpContext.Current.Request["enable"] + "&i=" +
                GlobalExt.GetRequestI() + "&target=" + ExtGridPanelFilter.GRID;
            }
            else if (((ViewListProperties)dc.GetProperties()).EntryStyle == ViewListProperties.ENTRY_STYLE_LOOKUP_ENTRY)
            {
                string cname = ((ViewListProperties)dc.GetProperties()).LookupDC;
                string dynamic_idmenu = Guid.NewGuid().ToString();
                int i = GlobalExt.GetRequestI();
                WindowLookup1.AutoLoad.Url = "~/Page/PageTabular.aspx?app=" + GlobalAsp.GetRequestApp() + "&roleid=" + HttpContext.Current.Request["roleid"]
                  + "&id=" + dynamic_idmenu + "&enable=" + HttpContext.Current.Request["enable"]
                  + "&idprev=" + GlobalExt.GetRequestId()
                  + "&i=1"
                  + "&dc=" + cname
                  + "&target=" + ExtGridPanelFilter.GRID;
            }
            else if (((ViewListProperties)dc.GetProperties()).EntryStyle == ViewListProperties.ENTRY_STYLE_TREE_ENTRY)
            {
                int i = GlobalExt.GetRequestI();
                WindowLookup1.AutoLoad.Url = "~/Page/PageTreeGrid.aspx?passdc=1&app=" + GlobalAsp.GetRequestApp() + "&roleid=" + HttpContext.Current.Request["roleid"]
                  + "&id=" + GlobalExt.GetRequestId() + "&enable=" + HttpContext.Current.Request["enable"]
                  + "&idprev=" + GlobalExt.GetRequestId()
                  + "&i=" + (i + 1)
                  + "&target=" + ExtGridPanelFilter.GRID;
            }
            WindowLookup1.Title = ((ViewListProperties)dc.GetProperties()).TitleList + " - Add";
            WindowLookup1.Render(Page.Form);
            WindowLookup1.Show();

            WindowLookup1.Maximize();
        }
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    /*
     * EntryStyle == ViewListPropertiesPM.ENTRY_STYLE_LOOKUP_FORM
     * */
    public void btnAddFromFormEntry(string json)
    {
        UtilityExt.SetModeEditable(UtilityExt.MODE_INSERT);
        int id = GlobalExt.GetRequestI();
        IDataControlUIEntry dc = (IDataControlUIEntry)UtilityUI.GetDataControl(id);
        dc.SetColumnsNull();

        Ext.Net.Panel PanelFormEntry = ControlUtils.FindControl<Ext.Net.Panel>(Page, "PanelFormEntry");
        //PanelBase FormDetail = ExtFormDetail.GetPanelDetil();
        //PanelFormEntry.Items.Add(FormDetail);
        PanelFormEntry.Expand();

        Ext.Net.FormPanel PanelFormFilter1 = ControlUtils.FindControl<Ext.Net.FormPanel>(Page, "FormFilter1");
        if (PanelFormFilter1 != null)
        {
            PanelFormFilter1.Collapse();
        }

        SetModeComponentBefore(true, true);

        FormPanel FPFormEntry1 = ControlUtils.FindControl<FormPanel>(PanelFormEntry, "FPFormEntry1");
        UtilityExt.PrepareForAdd(FPFormEntry1, json);
        SetModeComponentAfter(true, string.Empty);
        //try
        //{
        //  UtilityExt.SetModeEditable(UtilityExt.MODE_INSERT);
        //  //Session[ExtGridPanel.SESSION_MODE] = ExtGridPanel.MODE_INSERT;
        //  Ext.Net.Panel PanelFormEntry = ControlUtils.FindControl<Ext.Net.Panel>(Page, "PanelFormEntry");
        //  Ext.Net.TabPanel c1 = ControlUtils.FindControl<Ext.Net.TabPanel>(PanelFormEntry, "TabPanel1");
        //  if (c1 != null)
        //  {
        //    c1.SetActiveTab(0);
        //  }
        //  PanelFormEntry.Show();
        //  PanelFormEntry.Expand();
        //  SetModeComponentBefore(true, false);

        //  int id = GlobalExt.GetRequestI();
        //  IDataControlUIEntry dc = (IDataControlUIEntry)UtilityUI.GetDataControl(id);
        //  //dc.SetColumnsNull();
        //  FormPanel FPFormEntry1 = ControlUtils.FindControl<FormPanel>(PanelFormEntry, "FPFormEntry1");
        //  UtilityExt.PrepareForAdd(FPFormEntry1, json);
        //  SetModeComponentAfter(true, string.Empty);
        //}
        //catch (Exception ex)
        //{
        //  string msg = string.Empty;
        //  if (MasterAppConstants.Instance.StatusTesting)
        //  {
        //    msg = string.Format(@"Testing: Error executing '{0}'. Cause: {1}; In: {2} ", MethodBase.GetCurrentMethod().Name, ex.Message, ex.StackTrace);
        //  }
        //  else
        //  {
        //    msg = ConstantDictExt.TranslateException(ex, MethodBase.GetCurrentMethod().Name);
        //  }
        //  X.Msg.Alert(ConstantDictExt.Translate(GlobalExt.LBL_INFO), msg).Show();
        //}
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    /*
     * EntryStyle == ViewListPropertiesPM.ENTRY_STYLE_FORM
     * */
    public void btnAddDlgFormEntry(string json)
    {
        UtilityExt.SetModeEditable(UtilityExt.MODE_INSERTPACK);
        int id = GlobalExt.GetRequestI();
        IDataControlUIEntry dc = (IDataControlUIEntry)UtilityUI.GetDataControl(id);
        dc.SetColumnsNull();
        string url = Request.Url.AbsoluteUri;
        string fname = System.IO.Path.GetFileName(url);
        string[] strs = fname.Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
        url = url.Replace(strs[0], "PageForm.aspx");
        if (!url.Contains("passdc"))
        {
            url += "&passdc=1";
        }
        //PanelBase FormDetail = ExtFormDetail.GetPanelDetil();
        //WindowDetil1.Items.Add(FormDetail);
        //WindowDetil1.Render(Page.Form);
        //WindowDetil1.Show();
        WindowDetil1.Title = ConstantDict.Translate((string)dc.GetValue("XMLName")) + " - " + ConstantDict.Translate("BTN_ADD1");
        WindowDetil1.AutoLoad.Url = url + "&me=add";
        WindowDetil1.LoadContent();
        WindowDetil1.Show();
        //Ext.Net.Panel PanelFormEntry = ControlUtils.FindControl<Ext.Net.Panel>(Page, "PanelFormEntry");
        ////PanelBase FormDetail = ExtFormDetail.GetPanelDetil();
        ////PanelFormEntry.Items.Add(FormDetail);
        //PanelFormEntry.Expand();

        //Ext.Net.FormPanel PanelFormFilter1 = ControlUtils.FindControl<Ext.Net.FormPanel>(Page, "FormFilter1");
        //if (PanelFormFilter1 != null)
        //{
        //  PanelFormFilter1.Collapse();
        //}

        //SetModeComponentBefore(true, true);

        //FormPanel FPFormEntry1 = ControlUtils.FindControl<FormPanel>(PanelFormEntry, "FPFormEntry1");
        //UtilityExt.PrepareForAdd(FPFormEntry1, json);
        //SetModeComponentAfter(true, string.Empty);
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void RefreshDetilInfo(string json)
    {
        if (GlobalAsp.CekSession())
        {
            try
            {
                SetModeComponentBefore(false, false);
                int id = GlobalExt.GetRequestI();
                IDataControlUIEntry dc = (IDataControlUIEntry)UtilityUI.GetDataControl(id);

                Ext.Net.PanelBase PanelFormEntry = ControlUtils.FindControl<Ext.Net.PanelBase>(Page, "PanelFormEntry");
                FormPanel FPFormEntry1 = ControlUtils.FindControl<FormPanel>(PanelFormEntry, "FPFormEntry1");
                UtilityExt.PrepareForReview(FPFormEntry1, json);
                SetModeComponentAfter(false, CmdName);
            }
            catch (Exception ex)
            {
                WindowDebug.ShowMessage(Page, ConstantDictExt.TranslateException(ex, MethodBase.GetCurrentMethod().Name));

            }
        }
        else
        {
            //X.Js.Call("home");
        }
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void btnRadioClick()
    {
        UtilityExt.SetModeEditable(UtilityExt.MODE_EDIT);
        //HttpContext.Current.Session[ExtGridPanel.SESSION_MODE] = ExtGridPanel.MODE_EDIT;
        int id = GlobalExt.GetRequestI();
        IDataControlUIEntry dc = (IDataControlUIEntry)UtilityUI.GetDataControl(id);
        FormPanel FPFormEntry1 = ControlUtils.FindControl<FormPanel>(Page, "FPFormEntry1");
        UtilityExt.RefreshForEdit(FPFormEntry1);
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void btnEditClick(string json, string cmdname)
    {
        CmdName = cmdname;
        if (cmdname.Contains("Edit"))
        {
            UtilityExt.SetModeEditable(UtilityExt.MODE_EDIT);
        }
        //Session[ExtGridPanel.SESSION_MODE] = ExtGridPanel.MODE_EDIT;
        int id = GlobalExt.GetRequestI();
        IDataControlUIEntry dc = (IDataControlUIEntry)UtilityUI.GetDataControl(id);
        dc.SetColumnsNull();

        Ext.Net.Panel PanelFormEntry = ControlUtils.FindControl<Ext.Net.Panel>(Page, "PanelFormEntry");
        PanelFormEntry.Expand();

        Ext.Net.FormPanel PanelFormFilter1 = ControlUtils.FindControl<Ext.Net.FormPanel>(Page, "FormFilter1");
        if (PanelFormFilter1 != null)
        {
            PanelFormFilter1.Collapse();
        }

        SetModeComponentBefore(true, true);

        FormPanel FPFormEntry1 = ControlUtils.FindControl<FormPanel>(PanelFormEntry, "FPFormEntry1");
        UtilityExt.PrepareForEdit(FPFormEntry1, json);
        SetModeComponentAfter(true, cmdname);
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void btnCopyBarisClick(string json)
    {
        UtilityExt.SetModeEditable(UtilityExt.MODE_INSERTCOPY);
        //Session[ExtGridPanel.SESSION_MODE] = ExtGridPanel.MODE_INSERT;
        Ext.Net.Panel PanelFormEntry = ControlUtils.FindControl<Ext.Net.Panel>(Page, "PanelFormEntry");
        PanelFormEntry.Expand();
        SetModeComponentBefore(true, true);

        int id = GlobalExt.GetRequestI();
        IDataControlUIEntry dc = (IDataControlUIEntry)UtilityUI.GetDataControl(id);
        FormPanel FPFormEntry1 = ControlUtils.FindControl<FormPanel>(PanelFormEntry, "FPFormEntry1");
        UtilityExt.PrepareForCopy(FPFormEntry1, json);
        SetModeComponentAfter(true, string.Empty);
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void btnViewClick(string json, string cmdname)
    {
        int id = GlobalExt.GetRequestI();
        IDataControlUIEntry dc = (IDataControlUIEntry)UtilityUI.GetDataControl(id);
        //dc.SetColumnsNull();

        Window WindowView1 = ControlUtils.FindControl<Ext.Net.Window>(Page, "WindowView1");
        WindowView1.Title = ((ViewListProperties)dc.GetProperties()).TitleList + " - Detil";
        try
        {
            UtilityExt.PrepareViewWindow(id, WindowView1, dc, json, cmdname);
        }
        catch (Exception ex)
        {
            WindowDebug.ShowMessage(Page, ConstantDictExt.TranslateException(ex, MethodBase.GetCurrentMethod().Name));
            return;
        }
        if (!cmdname.StartsWith(GlobalExt.LINK))
        {
            WindowView1.Maximize();
            WindowView1.Show();
        }
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void btnDeleteClick(string json)
    {
        try
        {
            SetModeComponentBefore(false, false);
            int id = GlobalExt.GetRequestI();
            IDataControlUI dc = (IDataControlUI)UtilityUI.GetDataControl(id);
            UtilityExt.DeleteMultiple(json);
            X.Js.Call("refreshData");
            ExtGridPanel GridPanel1 = ControlUtils.FindControl<ExtGridPanel>(Page, "GridPanel1");
            if (GridPanel1 != null)
            {
                ((RowSelectionModel)GridPanel1.GetSelectionModel()).ClearSelections();
            }
            SetModeComponentAfter(false, string.Empty);
            //RefreshMasterPage();//Pindah di store1.refresh()
        }
        catch (Exception ex)
        {
            string msg = string.Empty;
            if (MasterAppConstants.Instance.StatusTesting)
            {
                msg = string.Format(@"Testing: Error executing '{0}'. Cause: {1}; In: {2} ", MethodBase.GetCurrentMethod().Name, ex.Message, ex.StackTrace);
                WindowDebug.ShowMessage(Page, msg);
            }
            else
            {
                msg = ConstantDictExt.Translate(ex.Message);
                X.Msg.Alert(GlobalAsp.GetConfigLabelInfo(), msg).Show();
            }
        }
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void RefreshFormEntries()
    {
        int id = GlobalExt.GetRequestI();
        IDataControlUIEntry dc = (IDataControlUIEntry)UtilityUI.GetDataControl(id);
        Ext.Net.Panel PanelFormEntry = ControlUtils.FindControl<Ext.Net.Panel>(Page, "PanelFormEntry");
        FormPanel FPFormEntry1 = ControlUtils.FindControl<FormPanel>(PanelFormEntry, "FPFormEntry1");
        CoreNETCompositeField.SetValues(FPFormEntry1, dc);
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void ResetFormEntries()
    {
        int id = GlobalExt.GetRequestI();
        IDataControlUIEntry dc = (IDataControlUIEntry)UtilityUI.GetDataControl(id);
        Ext.Net.Panel PanelFormEntry = ControlUtils.FindControl<Ext.Net.Panel>(Page, "PanelFormEntry");
        FormPanel FPFormEntry1 = ControlUtils.FindControl<FormPanel>(PanelFormEntry, "FPFormEntry1");
        if (FPFormEntry1 != null) FPFormEntry1.Reset();
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void btnCancelClick()
    {
        SetModeComponentBefore(false, false);
        Ext.Net.Panel PanelFormEntry = ControlUtils.FindControl<Ext.Net.Panel>(Page, "PanelFormEntry");
        FormPanel FPFormEntry1 = ControlUtils.FindControl<FormPanel>(PanelFormEntry, "FPFormEntry1");
        UtilityExt.PrepareAfterInsert(FPFormEntry1);
        SetModeComponentAfter(false, CmdName);
        if (PanelFormEntry.Region != Region.Center)
        {
            PanelFormEntry.Collapse();
            Ext.Net.FormPanel PanelFormFilter1 = ControlUtils.FindControl<Ext.Net.FormPanel>(Page, "FormFilter1");
            if (PanelFormFilter1 != null)
            {
                PanelFormFilter1.Expand();
            }
        }
        int id = GlobalExt.GetRequestI();
        IDataControlUIEntry dc = (IDataControlUIEntry)UtilityUI.GetDataControl(id);
        dc.SetColumnsNull();
        //UtilityExt.SetModeEditable(UtilityExt.MODE_NORMAL);
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void btnExpandClick()
    {
        Ext.Net.Panel PanelFormEntry = ControlUtils.FindControl<Ext.Net.Panel>(Page, "PanelFormEntry");
        FormPanel FPFormEntry1 = ControlUtils.FindControl<FormPanel>(PanelFormEntry, "FPFormEntry1");
        foreach (Component v in FPFormEntry1.Items)
        {
            if (typeof(FormPanel).IsInstanceOfType(v))
            {
                ((FormPanel)v).Expand();
            }
        }
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void btnCollapseClick()
    {
        Ext.Net.Panel PanelFormEntry = ControlUtils.FindControl<Ext.Net.Panel>(Page, "PanelFormEntry");
        FormPanel FPFormEntry1 = ControlUtils.FindControl<FormPanel>(PanelFormEntry, "FPFormEntry1");
        foreach (Component v in FPFormEntry1.Items)
        {
            if (typeof(FormPanel).IsInstanceOfType(v))
            {
                ((FormPanel)v).Collapse();
            }
        }
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void btnCloseDialogClick()
    {
        int id = GlobalExt.GetRequestI();
        IDataControlUIEntry dc = (IDataControlUIEntry)UtilityUI.GetDataControl(id);
        Window WindowView1 = ControlUtils.FindControl<Ext.Net.Window>(Page, "WindowView1");
        if (WindowView1 != null)
        {
            WindowView1.Hide();
        }
        //WindowLookup1.Close();
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void btnCloseDialogClick1()
    {
        int id = GlobalExt.GetRequestI();
        IDataControlUIEntry dc = (IDataControlUIEntry)UtilityUI.GetDataControl(id);
        Window WindowView1 = ControlUtils.FindControl<Ext.Net.Window>(Page, "WindowView1");
        if (WindowView1 != null)
        {
            WindowView1.Hide();
        }
        X.Js.Call(@"(function() {
      try{
        parent.WindowView1.hide();
        parent.WindowLookup1.hide();
        parent.WindowLoadCSV.hide();
        parent.WindowDebug1.hide();
        parent.WindowPrint1.hide();
        parent.WindowSearch1.hide();
        parent.WindowHelp1.hide();
        parent.WindowForum1.hide();
        parent.WindowForum2.hide();
      }catch(Exception ex) 
      {
      }
    })");
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void btnSaveForm()
    {
        try
        {
            //string mode = ((string)HttpContext.Current.Session[ExtGridPanel.SESSION_MODE]);

            SetModeComponentBefore(false, false);
            int id = GlobalExt.GetRequestI();
            IDataControlUIEntry dc = (IDataControlUIEntry)UtilityUI.GetDataControl(id);
            Ext.Net.Panel PanelFormEntry = ControlUtils.FindControl<Ext.Net.Panel>(Page, "PanelFormEntry");
            UtilityExt.SaveFormPanel(id, PanelFormEntry, "FPFormEntry1", dc);//Include Refresh, menyalahi SOLID, terlalu banyak beban

            PanelFormEntry.LoadContent();
            FormPanel FPFormEntry1 = ControlUtils.FindControl<FormPanel>(PanelFormEntry, "FPFormEntry1");
            UtilityExt.PrepareAfterInsert(FPFormEntry1);
            UtilityExt.SetModeEditable(UtilityExt.GetModeEditable() + 10);//UtilityExt.MODE_NORMAL
                                                                          //Session[ExtGridPanel.SESSION_MODE] = ExtGridPanel.MODE_NORMAL;
            SetModeComponentAfter(false, CmdName);

            string url = Request.UrlReferrer.OriginalString;
            if ((HttpContext.Current.Request["debug"] == null)
              && (UtilityUI.GetSessionExtPage() != UtilityUI.PAGE_FORM)
              && (UtilityUI.GetSessionExtPage() != UtilityUI.PAGE_TREE_PANEL_DETIL)
              && (!url.Contains("Form"))
              )//not windows debug and not form
            {
                //if (mode.Equals(ExtGridPanel.MODE_EDIT))
                //bukan page form
                if (!UtilityUI.GetModePage().Equals(UtilityUI.PAGE_FORM))
                {
                    if (!((ViewListProperties)dc.GetProperties()).AllowKeepFormExpanded)
                    {
                        PanelFormEntry.Collapse();
                        Ext.Net.FormPanel PanelFormFilter1 = ControlUtils.FindControl<Ext.Net.FormPanel>(Page, "FormFilter1");
                        if (PanelFormFilter1 != null)
                        {
                            PanelFormFilter1.Expand();
                        }
                    }
                }
            }

            //RefreshMasterPage();//Pindah di refreshTree(07/06/2019:bikin parent reload ketika baru buka child)
        }
        catch (Exception ex)
        {
            string msg = string.Empty;
            if (MasterAppConstants.Instance.StatusTesting)
            {
                msg = string.Format(@"Testing: Error executing '{0}'. Cause: {1}; In: {2} ", MethodBase.GetCurrentMethod().Name, ex.Message, ex.StackTrace);
                WindowDebug.ShowMessage(Page, msg);
            }
            else
            {
                msg = ConstantDictExt.Translate(ex.Message);
                X.Msg.Alert(GlobalAsp.GetConfigLabelInfo(), msg).Show();
            }
        }
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void btnSaveEditingCell(string field, string value, string json)
    {
        /*Opsi untuk Commit All Data/Save Multiple Row (lihat DeleteMultiple)*/
        try
        {
            UtilityExt.SetModeEditable(UtilityExt.MODE_EDIT);
            int id = GlobalExt.GetRequestI();
            IDataControlUIEntry dc = (IDataControlUIEntry)UtilityUI.GetDataControl(id);/*value set from row selected event*/
            dc.SetPageKey();
            UtilityExt.SetValue(dc, json);/*set value from editing row*/

            //IDataControlUIEntry prev = (IDataControlUIEntry)((BaseBO)dc).Clone();/*make a prev object*/
            IDataControlUIEntry prev = (IDataControlUIEntry)BaseBO.Clone(dc);
            prev.SetValue(field, value);/*set the original value*/
            GlobalExt.SetEditingObject(prev);/*save original value as prev.object*/

            dc.Update();

            string scriptRefreshMasterPage = string.Empty;
            if (((ViewListProperties)dc.GetProperties()).RefreshMaster)//Ketika RefreshMaster true
            {
                scriptRefreshMasterPage = "refreshMasterPage();";
            }
            string scriptRefresh = string.Empty;
            //Klo terlalu banyak kolom sebaiknya false, biar
            //refreshnya manual saja, agar ngga selalu reload
            if (((ViewListProperties)dc.GetProperties()).AutoRefresh)
            {
                scriptRefresh = "refreshDataWithSelection();";
            }
            X.Js.Call(@"(function() {
        " + scriptRefresh + @"
        " + scriptRefreshMasterPage + @"
      })");

            UtilityExt.SetModeEditable(UtilityExt.GetModeEditable() + 10);//UtilityExt.MODE_NORMAL
                                                                          //Session[ExtGridPanel.SESSION_MODE] = ExtGridPanel.MODE_NORMAL;
                                                                          //RefreshMasterPage();//Pindah di store1.refresh()
        }
        catch (Exception ex)
        {
            WindowDebug.ShowMessage(Page, ConstantDictExt.TranslateException(ex, MethodBase.GetCurrentMethod().Name));
            return;
        }
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void OpenTab(string title, string versi)
    {
        int id = GlobalExt.GetRequestI();
        IDataControlUIEntry dc = (IDataControlUIEntry)UtilityUI.GetDataControl(id);
        string url = ((IDataControlUIEntry)dc).GetURLFile(title, versi);
        X.Js.Call("opentab", title, url);
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public string GetIcon(string status)
    {
        return "page_white_word";
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void btnHelpClick()
    {
        //int id = GlobalAsp.GetRequestI();
        //string title = GlobalAsp.GetConfigLabelInfo();
        //string url = UtilityExt.ValidateURL(null, string.Format("~/Page/TextEditor.aspx?local=" + HttpContext.Current.Request["local"]
        //  + "&app=" + GlobalAsp.GetRequestApp()
        //  + "&id=" + GlobalAsp.GetRequestId()
        //  + "&i=" + GlobalAsp.GetRequestI()
        //  + "&tb=1"
        //  + "&type=" + NotesControl.RELEASE_NOTES
        //  + "&parentsave=0"
        //  ));

        //WindowHelp1.Title = title;
        //WindowHelp1.AutoLoad.Url = url;
        //WindowHelp1.LoadContent();
        //WindowHelp1.Show();
        //WindowHelp1.Maximize();

        WindowHelp1.Render(Page.Form);
        WindowHelp1.LoadContent();
        WindowHelp1.Show();
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void btnForumClick()
    {
        WindowForum1.Render(Page.Form);
        WindowForum1.LoadContent();
        WindowForum1.Show();
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void btnErrorClick()
    {
        WindowForum2.Render(Page.Form);
        WindowForum2.LoadContent();
        WindowForum2.Show();
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void btnDebugClick()
    {
        WindowDebug1.Render(Page.Form);
        WindowDebug1.LoadContent();
        WindowDebug1.Show();
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void btnAdminClick(int idx)
    {
        WindowAdmin1[idx].Render(Page.Form);
        WindowAdmin1[idx].LoadContent();
        WindowAdmin1[idx].Show();
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void ShowDialogLink()
    {
        FileDownloadLink1.Disabled = true;
        WindowLink.Show();
        X.Js.Call("loadingmasklink");
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void btnSaveCsvClick()
    {
        ShowDialogLink();

        int id = GlobalExt.GetRequestI();
        IDataControlUI dc = (IDataControlUI)UtilityUI.GetDataControl(id);
        IList list = GlobalAsp.GetSessionListRows();
        string[] key = null;
        if (typeof(ICsv).IsInstanceOfType(dc))
        {
            key = ((ICsv)dc).GetCsvColumns(MyDocumentFormat.CSV);
        }
        else
        {
            key = dc.GetKeys();
        }
        #region Export Data
        if (list != null)
        {
            string fullpath = GlobalAsp.GetDataDir();
            string dir = "Tmp\\";
            string csvname = string.Format("file{0}{1}.csv", GlobalAsp.GetSessionUser().GetUserID(), GlobalAsp.GetRequestId());
            if (typeof(IEksporable).IsInstanceOfType(dc))
            {
                csvname = ((IEksporable)dc).GetFileName(MyDocumentFormat.CSV);
            }
            PropertyInfo prop = dc.GetProperty("FolderRef");
            if (prop != null)
            {
                dir = (string)prop.GetValue(dc, null);
            }
            string fname = dir + csvname;
            fullpath += fname;
            if (typeof(IEksporable).IsInstanceOfType(dc))
            {
                fullpath = ((IEksporable)dc).GetFilePath(MyDocumentFormat.CSV);
            }
            try
            {
                string[] mappings = ((ICsv)dc).GetCsvColumns(0);

                Hashtable Maps = new Hashtable();
                for (int i = 0; i < mappings.Length; i++)
                {
                    string[] maps = mappings[i].Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);

                    string colname = maps[0];
                    Maps[maps[0]] = maps[0];
                    if (maps.Length > 1)
                    {
                        colname = maps[1];
                        Maps[maps[0]] = maps[1];
                    }
                }

                CsvConverter.ExportCsv(list, fullpath, ((ViewListProperties)dc.GetProperties()).Delimiter, key);
                FileDownloadLink1.Text = "Download File CSV";
                string url = GlobalAsp.GetDataURL() + fname;
                FileDownloadLink1.NavigateUrl = url;
            }
            catch (Exception ex)
            {
                WindowDebug.ShowMessage(Page, ConstantDictExt.TranslateException(ex, MethodBase.GetCurrentMethod().Name));
            }

        }
        else
        {
            X.Msg.Alert(ConstantDictExt.Translate(GlobalExt.LBL_INFO), ConstantDictExt.Translate("ERROR_FITUR_NOT_IMPLEMENTED")).Show();
        }
        X.Get("loading-mask-link").SetStyle("display", "none");
        FileDownloadLink1.Disabled = false;
        FileDownloadLink1.Hidden = false;
        #endregion

    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void btnSaveWordClick()
    {
        ShowDialogLink();
        try
        {
            int id = GlobalExt.GetRequestI();
            IPrintable dc = (IPrintable)UtilityUI.GetDataControl(id);
            dc.Print(0);

            FileDownloadLink1.Text = ConstantDictExt.Translate("LBL_FILE_CAN_BE_DOWNLOADED");
            string csvname = "Faktur.docx";// ((IEksporable)dc).GetFileName(MyDocumentFormat.DOC);
            string dir = "Tmp\\";
            PropertyInfo prop = dc.GetType().GetProperty("FolderRef");
            if (prop != null)
            {
                dir = (string)prop.GetValue(dc, null);
            }
            string fname = dir + csvname;

            string url = GlobalAsp.GetDataURL() + fname;
            FileDownloadLink1.NavigateUrl = url;
        }
        catch (Exception ex)
        {
            WindowDebug.ShowMessage(Page, ConstantDictExt.TranslateException(ex, MethodBase.GetCurrentMethod().Name));
        }
        X.Get("loading-mask-link").SetStyle("display", "none");
        FileDownloadLink1.Disabled = false;
        FileDownloadLink1.Hidden = false;
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void btnPrintClick(string title, int n)
    {

        string url = string.Empty;
        int id = GlobalAsp.GetRequestI();
        IDataControlUI dc = UtilityUI.GetDataControl(id);
        FormPanel FPFormEntry1 = ControlUtils.FindControl<FormPanel>(Page, "FPFormEntry1");
        if (FPFormEntry1 != null)
        {
            try
            {
                CoreNETCompositeField.GetValues(FPFormEntry1, dc);
            }
            catch (Exception ex)
            {
                X.Js.Alert(ex.Message);
            }
        }
        IPrintable dcprint = null;
        if (typeof(IPrintable).IsInstanceOfType(dc))
        {
            dcprint = (IPrintable)dc;
            try
            {
                url = dcprint.GetURLReport(n);
            }
            catch (Exception ex)
            {
                X.Js.Alert(ex.Message);
            }
        }
        if (!string.IsNullOrEmpty(url))
        {
            X.Js.Call("loadPageByID", new string[] { "Pages", title, title, url });
        }
        else
        {
            X.Js.Call("loadingmasklink");
            try
            {
                WindowPrint1 = new WindowPrint(n);

                dcprint.Print(0);

                WindowPrint1.Render(Page.Form);
                WindowPrint1.LoadContent();
                WindowPrint1.Show();

            }
            catch (Exception ex)
            {
                WindowDebug.ShowMessage(Page, ConstantDictExt.TranslateException(ex, MethodBase.GetCurrentMethod().Name));
            }
            X.Get("loading-mask-link").SetStyle("display", "none");
        }
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void btnSaveExcelClick(object sender, StoreSubmitDataEventArgs e)
    {
        string format = "xls";// this.FormatType.Value.ToString();

        XmlNode xml = e.Xml;

        Response.Clear();

        switch (format)
        {
            case "xml":
                string strXml = xml.OuterXml;
                Response.AddHeader("Content-Disposition", "attachment; filename=submittedData.xml");
                Response.AddHeader("Content-Length", strXml.Length.ToString());
                Response.ContentType = "application/xml";
                Response.Write(strXml);
                break;

            case "xls":
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment; filename=submittedData.xls");
                XslCompiledTransform xtExcel = new XslCompiledTransform();
                xtExcel.Load(Server.MapPath("Excel.xsl"));
                xtExcel.Transform(xml, null, Response.OutputStream);

                break;

            case "csv":
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=submittedData.csv");
                XslCompiledTransform xtCsv = new XslCompiledTransform();
                xtCsv.Load(Server.MapPath("Csv.xsl"));
                xtCsv.Transform(xml, null, Response.OutputStream);

                break;
        }
        Response.End();
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void btnLoadCsv()
    {
        WindowLoadCSV.Render(Page.Form);
        WindowLoadCSV.Show();
    }
    //  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    //  public void NormalizeFrame()
    //  {
    //    X.Js.Call(@"(function() {
    //      if (!!parent) 
    //      {
    //        if(!!parent.CoreNET){
    //          parent.CoreNET.NormalizeFrame();
    //        }
    //      }
    //    })");
    //  }
    //  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    //  public void ExpandFrame()
    //  {
    //    X.Js.Call(@"(function() {
    //      if (!!parent) 
    //      {
    //        if(!!parent.CoreNET){
    //          if (location.search.indexOf('child') !== -1) {
    //            parent.CoreNET.ExpandTopFrame();
    //          } else {
    //            parent.CoreNET.ExpandBotFrame();
    //          }
    //        }
    //      }
    //    })");
    //  }
    //  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    //  public void ClearDetilPage()
    //  {
    //    X.Js.Call(@"
    //      if(!!parent){
    //        if(!!parent.CoreNET){
    //          parent.CoreNET.RefreshDetilPage('');
    //        }
    //      }
    //    ");
    //  }  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]

}