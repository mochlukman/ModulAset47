
using CoreNET.Common.Base;
using CoreNET.Common.BO;
using Ext.Net;
using Ext.Net.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using System.Web.UI;

public partial class FilterTreePanel : System.Web.UI.Page
{
  private WindowDebug WindowDebug1 = null;
  private WindowAdmin[] WindowAdmin1 = new WindowAdmin[] {
    new WindowAdmin(0,BOSysUtils.ID_COL_TREE),
  };
  IDataControlUIEntry GetDataControl()
  {
    int id = GlobalExt.GetRequestI();
    string url = Request.Url.AbsoluteUri;
    IDataControlUIEntry dc = null;
    if (Request["lookup"]=="1")
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
    if (GlobalAsp.CekSessionTree(Page))
    {
      int id = GlobalExt.GetRequestI();
      WindowDebug1 = new WindowDebug(id, GlobalAsp.GetURLSessionKey());
      if (!Page.IsPostBack && !X.IsAjaxRequest)
      {
        #region Page Request
        int mode = int.Parse(Page.Request["mode"]);
        string winid = Page.Request["winid"];
        string key = Page.Request["key"];

        WindowDebug1 = new WindowDebug(id, GlobalAsp.GetURLSessionKey());
        HashTableofParameterRow hps = null;
        IDataControlUI cCtrl = null;
        ParameterRow pr = null;
        string[] targetfields = null;
        if (mode == ExtWindows.MODE_FILTER)
        {
          cCtrl = GetDataControl();
          hps = cCtrl.GetFilters();
          pr = (ParameterRow)hps[key];
        }
        else if (mode == ExtWindows.MODE_ENTRY)
        {
          cCtrl = GetDataControl();
          hps = ((IDataControlUIEntry)cCtrl).GetEntries();
          pr = (ParameterRow)hps[key];
        }
        else
        {
          cCtrl = (IDataControlUIEntry)GetDataControl();
          hps = cCtrl.GetFilters();
          pr = (ParameterRow)hps[key];
        }
        try
        {
          if (MasterAppConstants.Instance.StatusTesting)
          {
            HttpContext.Current.Session[GlobalAsp.GetURLSessionKey()] = cCtrl;
          }
        }
        catch (Exception ex)
        {
          UtilityBO.Log(ex);
        }

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
          targets[i] = mapfields[0];//punya si caller
          fields[i] = (mapfields.Length > 1) ? mapfields[1] : mapfields[0];
        }



        int target = int.Parse(Page.Request["target"]);
        string form = Page.Request["form"];
        #endregion
        #region Add TreeGrid
        string type = string.Empty;// pr.DCLookup.GetType().AssemblyQualifiedName;
        string label = string.Empty;//pr.LabelQuery;
        if (pr != null)
        {
          type = pr.DCLookup.GetType().AssemblyQualifiedName;
          label = pr.LabelQuery;
        }
        else
        {
          type = Request["DCLookup"];
          label = Request["LabelQuery"];
        }

        IDataControlTreeGrid3 dc = null;
        if (pr != null)
        {
          dc = (IDataControlTreeGrid3)pr.DCLookup;
        }
        else
        {
          try
          {
            dc = (IDataControlTreeGrid3)UtilityUI.Create(type);
          }
          catch (Exception ex)
          {
            UtilityBO.Log(ex);
            X.Msg.Alert("Error", ex.Message + "=" + type).Show();
            return;
          }
        }
        #region Custom JS Script
        IHasJSScript thisdc = null;
        if (typeof(IHasJSScript).IsInstanceOfType(dc))
        {
          thisdc = (IHasJSScript)dc;
        }
        if (thisdc != null)
        {
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
        #region Hidden
        Hidden1.Value = ResourceManager.GetInstance().GetIconUrl(Icon.Lorry);
        Hidden2.Value = ResourceManager.GetInstance().GetIconUrl(Icon.LorryGo);
        Hidden3.Value = ResourceManager.GetInstance().GetIconUrl(Icon.LorryError);
        Hidden4.Value = ResourceManager.GetInstance().GetIconUrl(Icon.HouseGo);
        #endregion
        Title = ((ViewListProperties)dc.GetProperties()).TitleFilter;
        DefaultSelectionModel selectionModel = new DefaultSelectionModel(); //MultiSelectionModel();//DefaultSelectionModel
        TreeGrid1.SelectionModel.Add(selectionModel);
        dc.SetTreeGridColumns(TreeGrid1.Columns);
        dc.SetPageKey();
        dc.SetFilterKey((BaseBO)cCtrl);
        dc.SetValue("ModePage", ViewListProperties.MODE_TREE);

        IList list = null;
        list = dc.View();
        Session[winid] = list;
        Ext.Net.TreeNode root = dc.CreateRoot(list, ExtTreePanelUtil.TYPE_TREEGRID, ((ViewListProperties)dc.GetProperties()).HasTreeRoot);
        TreeGrid1.Root.Add(root);
        #endregion
        #region Topbar
        Ext.Net.Button btnExpand = new Ext.Net.Button
        {
          ID = "BTN_EXPAND1"
        };
        btnExpand.Text = ConstantDictExt.Translate(btnExpand.ID);
        btnExpand.ToolTip = ConstantDictExt.TranslateTabTip(btnExpand);
        btnExpand.IconCls = "icon-expand-all2";
        btnExpand.Listeners.Click.Handler = @"
                  if(confirm('" + ConstantDictExt.Translate("LBL_CONFIRM_EXPAND") + @"'))
                  {
                    #{TreeGrid1}.expandAll();
                  }";
        TopBar1.Add(btnExpand);

        Ext.Net.Button btnCollapse = new Ext.Net.Button
        {
          ID = "BTN_COLLAPSE1"
        };
        btnCollapse.Text = ConstantDictExt.Translate(btnCollapse.ID);
        btnCollapse.ToolTip = ConstantDictExt.TranslateTabTip(btnCollapse);
        btnCollapse.Text = ConstantDictExt.Translate(btnCollapse.ID);
        btnCollapse.IconCls = "icon-collapse-all2";
        btnCollapse.Listeners.Click.Handler = "#{TreeGrid1}.collapseAll();";
        TopBar1.Add(btnCollapse);

        Ext.Net.Button btnSelect = new Ext.Net.Button() { ID = GlobalExt.BTN_SELECT1, Icon = Icon.Accept };
        btnSelect.Text = ConstantDictExt.Translate(btnSelect.ID);
        btnSelect.ToolTip = ConstantDictExt.TranslateTabTip(btnSelect);

        string strlvl = "";
        string lvl = "";

        if (pr != null)
        {
          lvl = pr.SelectionLevel;
          if (pr.SelectionCriteria == ParameterRow.SELECTION_CRITERIA_LEVEL)//Level
          {
            string[] lvls = lvl.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            strlvl = "if((sel.Kdlevel=='" + lvl + "') ";
            for (int i = 0; i < lvls.Length; i++)
            {
              strlvl += "|| (sel.Kdlevel=='" + lvls[i] + "')";
            }
            strlvl += "){";
          }
          else if (pr.SelectionCriteria == ParameterRow.SELECTION_CRITERIA_TYPE)
          {
            strlvl = "if(sel.Type=='" + pr.SelectionType.Trim() + "'){ ";
          }
        }

        string strset = "";
        for (int i = 0; i < targets.Length; i++)
        {
          if (targets[i] != "Path")
          {
            if (mode == ExtWindows.MODE_FILTER)
            {
              strset += "   parent.textFilter" + targets[i] + ".setValue(sel." + fields[i] + ");";
            }
            else
            {
              strset += "   parent.textEntry" + targets[i] + ".setValue(sel." + fields[i] + ");";
            }
          }
          else
          {
            strset += "   parent.textEntry" + targets[i] + ".setValue(path);";
          }
        }
        string msgerror = "";
        if (strlvl != "")
        {
          msgerror = pr.ErrorSelectionMsg;
          if (string.IsNullOrEmpty(msgerror))
          {
            if (pr.SelectionCriteria == ParameterRow.SELECTION_CRITERIA_LEVEL)//Level
            {
              msgerror = string.Format(ConstantDictExt.Translate("LBL_ERROR_SELECTION_LEVEL"), lvl);
            }
            else if (pr.SelectionCriteria == ParameterRow.SELECTION_CRITERIA_TYPE)//Type
            {
              msgerror = string.Format(ConstantDictExt.Translate("LBL_ERROR_SELECTION_TYPE"), pr.SelectionType);
            }
          }
          msgerror = "  }else" +
          "  {" +
          "    Ext.Msg.alert('" + ConstantDictExt.Translate(GlobalExt.LBL_INFO) + "','" + msgerror + "');" +
          "  }";
        }

        ///*
        //alert('tes'); !!#{TreeGrid1}.tes();
        btnSelect.Listeners.Click.Handler = @"
          if(!!#{TreeGrid1}.getSelectedNodes()){
            var sel = #{TreeGrid1}.getSelectedNodes().attributes;" + strlvl + @"
            var path = #{TreeGrid1}.selModel.selNode.getPath('text','/');
            " + strset + @"
             CoreNET.btnSelect(Ext.encode(sel),path);" +
          ((target == ExtGridPanelFilter.ENTRY) ?//Mungkinkah dipindahin ke btnSelect(sel,path), karena klo JS asinkronus
              "" : "try{" +
                ((target == ExtGridPanelFilter.GRID) ?
                "  parent.refreshData();" :
                "  parent.refreshTree();") +"}catch(exception){}" ) +
          "  parent." + winid + ".hide();" + msgerror +
          "}"
          ;
        //*/
        TreeGrid1.Listeners.BeforeClick.Handler = "node.select();";
        TreeGrid1.Listeners.Click.Handler = btnSelect.Listeners.Click.Handler;
        //TreeGrid1.Listeners.Click.Handler = @"
        //    var sel = node.attributes;
        //      alert(node.text);
        //      alert(Ext.encode(sel));"
        //    + strlvl + @"
        //    var path = node.getPath('text','/');
        //    " + strset + @"
        //     alert(Ext.encode(sel)+path);
        //     CoreNET.btnSelect(Ext.encode(sel),path);
        //     try{" +
        //  ((target == ExtGridPanelFilter.ENTRY) ?//Mungkinkah dipindahin ke btnSelect(sel,path), karena klo JS asinkronus
        //      "" :
        //      ((target == ExtGridPanelFilter.GRID) ?
        //        "  parent.refreshData();" :
        //        "  parent.refreshTree();")) +
        //   @"}catch(exception){}
        //     parent." + winid + ".hide();" + msgerror
        //  ;
        //TopBar1.Add(btnSelect);

        string comp = "";
        for (int i = 0; i < targets.Length; i++)
        {
          if (mode == ExtWindows.MODE_ENTRY)
          {
            comp += "parent.textEntry" + targets[i] + ".reset();";
          }
          else if (mode == ExtWindows.MODE_FILTER)
          {
            comp += "parent.textFilter" + targets[i] + ".reset();";
          }
        }
        Ext.Net.Button btnReset = new Ext.Net.Button() { ID = GlobalExt.BTN_RESET1, Icon = Icon.Reload };
        btnReset.Text = ConstantDictExt.Translate(btnReset.ID);
        btnReset.ToolTip = ConstantDictExt.TranslateTabTip(btnReset);
        btnReset.Listeners.Click.Handler =
          "CoreNET.btnReset('" + form + "');" + comp +
          ((target == ExtGridPanelFilter.ENTRY) ?
              string.Empty :
              ((target == ExtGridPanelFilter.GRID) ?
                string.Empty +
                "parent.refreshData();" :
                "parent.refreshTree();")) +
          "parent." + winid + ".hide();";
        TopBar1.Add(btnReset);

        Ext.Net.Button btnCancel = new Ext.Net.Button() { ID = GlobalExt.BTN_CANCEL1, Icon = Icon.Cancel };
        btnCancel.Text = ConstantDictExt.Translate(btnCancel.ID);
        btnCancel.ToolTip = ConstantDictExt.TranslateTabTip(btnCancel);
        btnCancel.Listeners.Click.Handler =
          "CoreNET.btnSelect(\"\");" +
          ((target == ExtGridPanelFilter.ENTRY) ?
              string.Empty :
              ((target == ExtGridPanelFilter.GRID) ?
                "parent.refreshData();" :
                "parent.refreshTree();")) +
          "parent." + winid + ".hide();";
        //pageTB.Add(btnCancel);

        Ext.Net.Button btnRefresh = new Ext.Net.Button() { ID = GlobalExt.BTN_REFRESH1, Icon = Icon.ArrowRefresh };
        btnRefresh.Text = ConstantDictExt.Translate(btnRefresh.ID);
        btnRefresh.ToolTip = ConstantDictExt.TranslateTabTip(btnRefresh);
        btnRefresh.Listeners.Click.Handler = @"" +
          "refreshTree(); " +
          "";
        //TopBar1.Add(btnRefresh);

        Ext.Net.Button btnClose = new Ext.Net.Button() { ID = GlobalExt.BTN_CLOSE1, Icon = Icon.DoorOpen };
        btnClose.Text = ConstantDictExt.Translate(btnClose.ID);
        btnClose.ToolTip = ConstantDictExt.TranslateTabTip(btnClose);
        btnClose.Listeners.Click.Handler = "parent." + winid + ".hide();";
        TopBar1.Add(btnClose);

        Ext.Net.Button btnPreview = new Ext.Net.Button() { ID = GlobalExt.BTN_PREVIEW_TABLE, Icon = Icon.ReportMagnify };
        btnPreview.Text = ConstantDictExt.Translate(btnPreview.ID);
        btnPreview.ToolTip = btnPreview.Text;
        btnPreview.Listeners.Click.Handler =
            @"CoreNET.RefreshPreview();";
        TopBar1.Add(btnPreview);

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

          TopBar1.Add(btnMenu);
        }
        #endregion

        Ext.Net.Label lblWait = new Ext.Net.Label() { ID = "lblWait" };
        lblWait.Html = @"<div id='loading-mask' style='display:none'><div id='loading'><div class='loading-indicator'>Loading...</div></div></div>";
        TopBar1.Add(lblWait);

        #endregion
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
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void RefreshPreview()
  {
    int id = GlobalExt.GetRequestI();
    IDataControlUIEntry dc = (IDataControlUIEntry)GetDataControl();
    string url = Request.UrlReferrer.OriginalString;
    if (url.Contains("FilterTreePanel.aspx"))
    {
      url = url.Replace("FilterTreePanel.aspx", "FilterTabular.aspx");
      dc.SetValue("ModePage", ViewListProperties.MODE_TABULAR);
    }
    Response.Redirect(url);
  }
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void btnReset(string form)
  {
    int id = GlobalExt.GetRequestI();
    string key = Page.Request["key"];
    string[] temps = key.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
    string[] keys = new string[temps.Length];
    string[] sels = new string[temps.Length];
    for (int i = 0; i < temps.Length; i++)
    {
      string[] fields = temps[i].Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
      keys[i] = fields[0];
      sels[i] = fields[0];
      if (fields.Length > 1)
      {
        sels[i] = fields[1];
      }
    }
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
    if (mode == ExtWindows.MODE_FILTER)
    {
      dc.FilterClick(key);
    }
  }
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void btnSelect(string json, string path)
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
    string[] targetfields = null;

    if ((pr != null) && (pr.TargetLookupFields != null) && (pr.TargetLookupFields.Length > 0))
    {
      targetfields = pr.TargetLookupFields;
    }
    if (!json.Equals(string.Empty))
    {
      string newjson = json;
      if (!json.Contains("["))
      {
        newjson = "[" + json + "]";
      }
      Dictionary<string, string>[] datas = JSON.Deserialize<Dictionary<string, string>[]>(newjson);

      foreach (Dictionary<string, string> row in datas)
      {
        for (int i = 0; i < targetfields.Length; i++)
        {
          string fieldmap = targetfields[i];
          string[] mapfields = fieldmap.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
          string targetlookupfield = (mapfields.Length > 1) ? mapfields[1] : mapfields[0];
          try
          {
            PropertyInfo prop = dc.GetProperty(targetlookupfield);
            if (prop != null && prop.CanWrite)
            {
              prop.SetValue(dc, row[mapfields[0]], null);
            }
          }
          catch (Exception ex)
          {
            UtilityBO.Log(ex);
            dc.GetProperty(targetlookupfield).SetValue(dc, UtilityBO.GetDefault(dc.GetProperty(keys[i]).PropertyType), null);
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
    dc.GetProperty("Path").SetValue(dc, path, null);//Path propertynya ada di BaseBO
    if (mode == ExtWindows.MODE_FILTER)
    {
      dc.FilterClick(key);
    }
  }
  protected void LoadPages(object sender, NodeLoadEventArgs e)
  {
    try
    {
      if (!string.IsNullOrEmpty(e.NodeID))
      {
        int mode = int.Parse(Page.Request["mode"]);
        string key = Page.Request["key"];
        int id = GlobalExt.GetRequestI();

        HashTableofParameterRow hps = null;
        IDataControlUI cCtrl = null;
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
        if (pr == null)
        {
          pr = null;
        }
        string type = string.Empty;// pr.DCLookup.GetType().AssemblyQualifiedName;
        string label = string.Empty;//pr.LabelQuery;
        if (pr != null)
        {
          type = pr.DCLookup.GetType().AssemblyQualifiedName;
          label = pr.LabelQuery;
        }
        else
        {
          type = Request["DCLookup"];
          label = Request["LabelQuery"];
        }

        IDataControlTreeGrid3 dc = null;
        if (pr != null)
        {
          dc = (IDataControlTreeGrid3)pr.DCLookup;
        }
        else
        {
          try
          {
            dc = (IDataControlTreeGrid3)UtilityUI.Create(type);
          }
          catch (Exception ex)
          {
            UtilityBO.Log(ex);
            X.Msg.Alert("Error", ex.Message + "=" + type).Show();
            return;
          }
        }

        string winid = Page.Request["winid"];
        IList list = (IList)Session[winid];

        if (typeof(IDataControlTreeGrid3).IsInstanceOfType(dc))
        {
          ((IDataControlTreeGrid3)dc).LoadPages(list, e.NodeID, e.Nodes, ExtTreePanelUtil.TYPE_TREEGRID);
        }
      }
    }
    catch (Exception ex)
    {
      UtilityBO.Log(ex);
    }

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
