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

public partial class FilterTabular : System.Web.UI.Page
{
    private WindowDebug WindowDebug1 = null;
    private WindowAdmin[] WindowAdmin1 = new WindowAdmin[] {
    new WindowAdmin(0,BOSysUtils.ID_COL_GRID),
  };

    private IDataControlUIEntry GetDataControl()
    {
        int id = GlobalExt.GetRequestI();
        string url = Request.Url.AbsoluteUri;
        IDataControlUIEntry dc = null;
        if (Request["lookup"] == "1")
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
        int id = GlobalExt.GetRequestI();
        string winid = Page.Request["winid"];
        IDataControlUI cCtrl = null;
        try
        {
            cCtrl = GetDataControl();
        }
        catch (Exception ex)
        {
            UtilityBO.Log(ex);
        }
        if (GlobalAsp.CekSessionGrid(Page) && (cCtrl != null))
        {
            #region Inisialisasi
            int mode = int.Parse(Page.Request["mode"]);
            WindowDebug1 = new WindowDebug(GlobalAsp.GetRequestI(), GlobalAsp.GetURLSessionKey());
            string key = Page.Request["key"];
            string[] keys = key.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            int target = int.Parse(Page.Request["target"]);
            string form = Page.Request["form"];
            #endregion
            #region Bind Store
            HashTableofParameterRow hps = null;
            try
            {
                if (mode == ExtWindows.MODE_FILTER)
                {
                    cCtrl = GetDataControl();
                    hps = cCtrl.GetFilters();
                }
                else
                {
                    cCtrl = (IDataControlUIEntry)GetDataControl();
                    hps = ((IDataControlUIEntry)cCtrl).GetEntries();
                }

                if (MasterAppConstants.Instance.StatusTesting)
                {
                    HttpContext.Current.Session[GlobalAsp.GetURLSessionKey()] = cCtrl;
                }
            }
            catch (Exception ex)
            {
                UtilityBO.Log(ex);
            }

            ParameterRow pr = (ParameterRow)hps[key];
            string[] targetfields = null;

            if ((pr != null) && (pr.TargetLookupFields != null) && (pr.TargetLookupFields.Length > 0))
            {
                targetfields = pr.TargetLookupFields;
            }
            string[] targets = new string[targetfields.Length];
            string[] fields = new string[targetfields.Length];
            for (int i = 0; i < targetfields.Length; i++)
            {
                string fieldmap = targetfields[i];
                string[] mapfields = fieldmap.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                targets[i] = mapfields[0];//punya si caller,yang benar Idunit1=Idunit
                fields[i] = (mapfields.Length > 1) ? mapfields[1] : mapfields[0];
            }

            IDataControlUI dc = (IDataControlUI)pr.DCLookup;
            ExtStore.SettingStore(Store1, dc, Page);

            string label = pr.LabelQuery;
            dc.SetFilterKey((BaseBO)cCtrl);
            dc.SetValue("ModePage", ViewListProperties.MODE_TABULAR);
            if (!Page.IsPostBack)
            {
                ExtStore.BindStore(Store1, dc, false);
            }
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
            GridPanel gridPanel = new ExtGridPanelFilter(dc, pr, winid, form, target)
            {
                StoreID = "Store1"
            };
            //ImageCommandColumn col = new ImageCommandColumn() { Width = new Unit(20, UnitType.Pixel), Align = Alignment.Center };
            //ImageCommand cmd = null;
            //cmd = new ImageCommand() { CommandName = "Select", Icon = Icon.Accept };
            //cmd.ToolTip.Text = ConstantDict.Translate("LBL_SELECT");
            //col.Commands.Add(cmd);
            //gridPanel.ColumnModel.Columns.Insert(0, col);

            PanelCenter.Items.Add(gridPanel);
            #endregion
            #region Add GridPanel PagingToolbar
            PagingToolbar pageTB = new PagingToolbar
            {
                HideLabels = true,
                HideRefresh = false,
                ID = "PageTBFilter" + winid
            };
            Ext.Net.Button btnSelect = new Ext.Net.Button() { ID = GlobalExt.BTN_SELECT1, Icon = Icon.Accept };
            btnSelect.Text = ConstantDictExt.Translate(btnSelect.ID);
            btnSelect.ToolTip = ConstantDictExt.TranslateTabTip(btnSelect);

            RowSelectionModel rowSelectionModel = new RowSelectionModel { SingleSelect = true };
            gridPanel.SelectionModel.Add(rowSelectionModel);

            string strset = "";
            for (int i = 0; i < targets.Length; i++)
            {
                if (true)//(i <= 2)
                {
                    if (targets[i] != "Path")
                    {
                        if (mode == ExtWindows.MODE_FILTER)
                        {
                            string c = "   parent.textFilter" + targets[i] + ".setValue(sel.data." + fields[i] + ");";
                            strset += "   if(!!parent.textFilter" + targets[i] + "){" + c + "}";
                        }
                        else
                        {
                            string c = "   parent.textEntry" + targets[i] + ".setValue(sel.data." + fields[i] + ");";
                            strset += "   if(!!parent.textEntry" + targets[i] + "){" + c + "}";
                        }
                    }
                    else
                    {
                        strset += "   parent.textEntry" + targets[i] + ".setValue(path);";
                    }
                }
            }
            /*tidak sesuai kebutuhan, ada field yg ketimpa nantinya*/
            //parent." + form + ".getForm().loadRecord(#{" + gridPanel.ID + @"}.getSelectionModel().getSelected());
            btnSelect.Listeners.Click.Handler = @"
        if(!!#{" + gridPanel.ID + @"}.getSelectionModel().getSelected())
        {
          sel=#{" + gridPanel.ID + "}.getSelectionModel().getSelected();" +
                     strset +
                "CoreNET.btnSelect(Ext.encode(#{" + gridPanel.ID + @"}.getRowsValues({selectedOnly : true})));" +
                ((target == ExtGridPanelFilter.ENTRY) ?
                    "" :
                    ((target == ExtGridPanelFilter.GRID) ?
                      "parent.refreshData();" :
                      "parent.refreshTree();")) +
                  "parent." + winid + @".hide();
        }else
        {
          Ext.Msg.alert('" + ConstantDictExt.Translate(GlobalAsp.LBL_INFO) + "','" + ConstantDictExt.Translate("LBL_ERROR_DATA_NOT_SELECTED") + @"');
        }
      ";

            rowSelectionModel.Listeners.RowSelect.Handler = btnSelect.Listeners.Click.Handler;
            //gridPanel.Listeners.Command.Handler = @"
            //  parent." + winid + @".hide();
            //  if(command.indexOf('Select') != -1){
            //    var sel=record;
            //    " + strset + @"
            //    CoreNET.btnSelect(Ext.encode(record.data)); " +
            //    ((target == ExtGridPanelFilter.ENTRY) ?
            //         "" :
            //         ((target == ExtGridPanelFilter.GRID) ?
            //           "parent.refreshData();" :
            //           "parent.refreshTree();")) + @"
            //  }
            //  parent." + winid + @".hide();
            //";
            //pageTB.Add(btnSelect);

            string comp = "";
            for (int i = 0; i < targets.Length; i++)
            {
                if (target == ExtGridPanelFilter.ENTRY)
                {
                    comp += "parent.textEntry" + targets[i] + ".setValue('');";
                }
                else
                {
                    comp += "parent.textFilter" + targets[i] + ".setValue('');";
                }
            }
            Ext.Net.Button btnReset = new Ext.Net.Button() { ID = GlobalExt.BTN_RESET1, Icon = Icon.Reload };
            btnReset.Text = ConstantDictExt.Translate(btnReset.ID);
            btnReset.ToolTip = ConstantDictExt.TranslateTabTip(btnReset);
            btnReset.Listeners.Click.Handler =
              comp +
              "CoreNET.btnReset('" + form + "');" +
              ((target == ExtGridPanelFilter.ENTRY) ?
                  string.Empty :
                    ((target == ExtGridPanelFilter.GRID) ?
                      "parent.refreshData();" :
                      "parent.refreshTree();")) +
              "parent.filterclick();" +
              "parent." + winid + ".hide();";
            pageTB.Add(btnReset);

            Ext.Net.Button btnClose = new Ext.Net.Button() { ID = GlobalExt.BTN_CLOSE1, Icon = Icon.DoorOpen };
            btnClose.Text = ConstantDictExt.Translate(btnClose.ID);
            btnClose.ToolTip = ConstantDictExt.TranslateTabTip(btnClose);
            btnClose.Listeners.Click.Handler = "parent." + winid + ".hide();";
            pageTB.Add(btnClose);

            Ext.Net.Button btnPreview = new Ext.Net.Button() { ID = GlobalExt.BTN_PREVIEW_TREE, Icon = Icon.ReportMagnify };
            btnPreview.Text = ConstantDictExt.Translate(btnPreview.ID);
            btnPreview.ToolTip = ConstantDictExt.TranslateTabTip(btnPreview);

            btnPreview.Listeners.Click.Handler =
                @"CoreNET.RefreshPreview();";
            pageTB.Add(btnPreview);

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

                pageTB.Add(btnMenu);
            }
            #endregion
            Ext.Net.Label lblWait = new Ext.Net.Label() { ID = "lblWait" };
            lblWait.Html = @"<div id='loading-mask' style='display:none'><div id='loading'><div class='loading-indicator'>Loading...</div></div></div>";
            pageTB.Add(lblWait);


            gridPanel.TopBar.Add(pageTB);
            #endregion
        }
        else
        {
            PanelCenter.Layout = "Fit";
            UtilityExt.LoadUrl(PanelCenter, "../blank.aspx");
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
        HashTableofParameterRow hps = null;
        IDataControlUI cCtrl = null;
        int mode = int.Parse(Page.Request["mode"]);
        int id = GlobalExt.GetRequestI();
        string key = Page.Request["key"];
        if (mode == ExtWindows.MODE_FILTER)
        {
            cCtrl = GetDataControl();
            hps = cCtrl.GetFilters();
        }
        else
        {
            cCtrl = (IDataControlUIEntry)GetDataControl();
            hps = ((IDataControlUIEntry)cCtrl).GetEntries();
        }
        ParameterRow pr = (ParameterRow)hps[key];
        IDataControlUI dc = (IDataControlUI)pr.DCLookup;

        Store store = (Store)sender;
        FormPanel formFilter = ControlUtils.FindControl<FormPanel>(Page.Form, "FormFilter1");
        CoreNETCompositeField.GetValues(formFilter, dc);
        dc.SetPageKey();
        try
        {
            ExtStore.BindStore(store, dc, e);
        }
        catch (Exception ex)
        {
            WindowDebug.ShowMessage(Page, ConstantDictExt.TranslateException(ex, MethodBase.GetCurrentMethod().Name));
        }

    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void btnReset(string form)
    {
        int id = GlobalExt.GetRequestI();
        string key = Page.Request["key"];
        int mode = int.Parse(Page.Request["mode"]);

        IDataControlUI dc = GetDataControl();
        HashTableofParameterRow hps = null;
        if (mode == ExtWindows.MODE_FILTER)
        {
            hps = dc.GetFilters();
        }
        else
        {
            hps = ((IDataControlUIEntry)dc).GetEntries();
        }
        ParameterRow pr = (ParameterRow)hps[key];

        string[] keys = key.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < keys.Length; i++)
        {
            try
            {
                dc.GetProperty(keys[i]).SetValue(dc, "", null);
            }
            catch (Exception ex)
            {
                UtilityBO.Log(ex);
            }
        }
        dc.FilterClick(key);
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void btnSelect(string newjson)
    {
        /*salah satu aja, nanti kereset*/

        int id = GlobalExt.GetRequestI();
        string key = Page.Request["key"];
        int mode = int.Parse(Page.Request["mode"]);

        string json = newjson;
        if (!newjson.StartsWith("[{"))
        {
            json = "[" + newjson + "]";
        }


        IDataControlUI dc = GetDataControl();
        HashTableofParameterRow hps = null;
        if (mode == ExtWindows.MODE_FILTER)
        {
            hps = dc.GetFilters();
        }
        else
        {
            hps = ((IDataControlUIEntry)dc).GetEntries();
        }
        ParameterRow pr = (ParameterRow)hps[key];

        string[] keys = key.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        string[] targetfields = pr.TargetLookupFields;

        if (!json.Equals(string.Empty))
        {
            Dictionary<string, string>[] datas = JSON.Deserialize<Dictionary<string, string>[]>(json);

            foreach (Dictionary<string, string> row in datas)
            {
                for (int i = 0; i < targetfields.Length; i++)
                {
                    string fieldmap = targetfields[i];
                    string[] mapfields = fieldmap.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                    string fieldvalue = (mapfields.Length > 1) ? mapfields[1] : mapfields[0];
                    try
                    {
                        dc.SetValue(mapfields[0], row[fieldvalue]);
                        //dc.GetProperty(mapfields[0]).SetValue(dc, row[fieldvalue], null);
                    }
                    catch (Exception ex)
                    {
                        UtilityBO.Log(ex);
                        dc.GetProperty(mapfields[0]).SetValue(dc, UtilityBO.GetDefault(dc.GetProperty(keys[i]).PropertyType), null);
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < keys.Length; i++)
            {
                try
                {
                    dc.GetProperty(keys[i]).SetValue(dc, UtilityBO.GetDefault(dc.GetProperty(keys[i]).PropertyType), null);
                }
                catch (Exception ex)
                {
                    UtilityBO.Log(ex);
                }
            }
        }
        dc.FilterClick(key);
        //@todo Bikin kereset formnya
        //kasus 1: perlu, klo generate
        //X.Js.Call("reloadForm");//move ke dc.FilterClick(key)
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void RefreshPreview()
    {
        int id = GlobalExt.GetRequestI();
        IDataControlUIEntry dc = (IDataControlUIEntry)GetDataControl();
        string url = Request.UrlReferrer.OriginalString;
        if (typeof(IDataControlTreeGrid3).IsInstanceOfType(dc))
        {
            url = url.Replace("FilterTabular.aspx", "FilterTreePanel.aspx");
            dc.SetValue("ModePage", ViewListProperties.MODE_TREE);
        }
        Response.Redirect(url);
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void SetComboValue(string key, string value)
    {
        int id = GlobalExt.GetRequestI();
        IDataControlUI dc = GetDataControl();
        FormPanel formFilter = ControlUtils.FindControl<FormPanel>(Page, "FormFilter1");
        if (formFilter != null)
        {
            dc.SetValue(key, value);
            dc.FilterClick(key);
            CoreNETCompositeField.SetValues(formFilter, dc);
        }
    }
}
