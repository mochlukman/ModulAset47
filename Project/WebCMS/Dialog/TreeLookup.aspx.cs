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

public partial class TreeLookup : System.Web.UI.Page
{
  WindowDebug WindowDebug1 = null;
  WindowAdmin[] WindowAdmin1 = new WindowAdmin[] {
    new WindowAdmin(0,BOSysUtils.ID_COL_TREE),
  };
  protected void Page_Load(object sender, EventArgs e)
  {
    if (GlobalAsp.CekSessionTree(Page))
    {
      if (!Page.IsPostBack && !X.IsAjaxRequest)
      {
        int id = GlobalExt.GetRequestI();
        WindowDebug1 = new WindowDebug(id, GlobalAsp.GetURLSessionKey());
        int target = int.Parse(Request["target"]);
        IDataControlUI dcCaller = UtilityUI.GetDataControl(id);
        IDataControlUI dc = UtilityUI.GetDataControlLookup(id);
        dc.SetFilterKey((BaseBO)dcCaller);
        //if (dc == null)
        //{
        //  string cname = ((ViewListProperties)dcCaller.GetProperties()).LookupDC;
        //  dc = (IDataControlUI)UtilityUI.Create(cname);
        //  ((ViewListProperties)dc.GetProperties()).ViewLabelQuery = ((ViewListProperties)dcCaller.GetProperties()).LookupLabelQuery;
        //}
        LoadTreePanel();
        #region Add Lookup Toolbar
        Ext.Net.Button btnExpand = new Ext.Net.Button();
        btnExpand.Text = "Expand All";
        btnExpand.IconCls = "icon-expand-all2";
        string msg = string.Empty;
        if (ConstantDictExt.IDLOCALE == ConstantDictExt.EN)
        {
          msg = "It needs more time to load more data";
        }
        else
        {
          msg = "Apakah anda yakin? Perlu waktu lebih lama untuk menampilkan semua data";
        }
        btnExpand.Listeners.Click.Handler = @"
          if(confirm('" + msg + @"'))
          {
            " + TPLookup1.ClientID + @".expandAll();
          }";
        TBLookup1.Add(btnExpand);

        Ext.Net.Button btnCollapse = new Ext.Net.Button();
        btnCollapse.Text = "Collapse All";
        btnCollapse.IconCls = "icon-collapse-all2";
        btnCollapse.Listeners.Click.Handler = TPLookup1.ClientID + ".collapseAll();";
        TBLookup1.Add(btnCollapse);

        //"#{TPLookup1}.submitNodes();" +
        //"CoreNET.SubmitNodes(Ext.encode(atrsel));" +//ngga kompatibel dengan TreePanel? klo TreeGrid bisa..masa?
        //"#{TPLookup1}.submitNodes();" +pake enumerasi checkboc, parent.Attributes jd Json object
        //"#{TPLookup1}.submitNodes();" +
        Ext.Net.Button btnSave = new Ext.Net.Button() { ID = GlobalExt.BTN_SAVE1, Icon = Icon.Disk };
        btnSave.Text = ConstantDictExt.Translate(btnSave.ID);
        /*.getChecked();//.getSelectedNodes()*/
        //+ ((target == ExtGridPanelFilter.TREE) ? "parent.refreshTree();" : "parent.refreshData();")
        btnSave.Listeners.Click.Handler = @"
          var atrsel = #{TPLookup1}.getSelectedNodes().attributes;
          #{TPLookup1}.submitNodes();
          if(!!parent.WindowLookup1)
          {
            parent.WindowLookup1.hide(this);
          }
        ";
        TBLookup1.Add(btnSave);
        //if (((ViewListProperties)dc.GetProperties()).IsModeAdd() || ((ViewListProperties)dc.GetProperties()).IsModeEdit())
        //{
        //}
        Ext.Net.Button btnBack = new Ext.Net.Button() { ID = GlobalExt.BTN_BACK1, Icon = Icon.Reload };
        btnBack.Text = ConstantDictExt.Translate(btnBack.ID);
        btnBack.Listeners.Click.Handler = @"
          if(!!parent.WindowLookup1)
          {
            parent.WindowLookup1.hide(this);
          }
        ";
        TBLookup1.Add(btnBack);

        Ext.Net.Button btnRefresh = new Ext.Net.Button() { ID = GlobalExt.BTN_REFRESH1, Icon = Icon.ArrowRefresh };
        btnRefresh.Text = ConstantDictExt.Translate(btnRefresh.ID);
        btnRefresh.Listeners.Click.Handler = @"" +
          "CoreNET.LoadTreePanel(); ";
        TBLookup1.Add(btnRefresh);

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

          TBLookup1.Add(btnMenu);
        }
        #endregion

        Ext.Net.Label lblWait = new Ext.Net.Label() { ID = "lblWait" };
        lblWait.Html = @"<div id='loading-mask' style='display:none'><div id='loading'><div class='loading-indicator'>Loading...</div></div></div>";
        TBLookup1.Add(lblWait);

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
  public void LoadTreePanel()
  {
    #region Add Tree Lookup
    int id = GlobalExt.GetRequestI();
    int target = int.Parse(Request["target"]);
    IDataControlUI dcCaller = UtilityUI.GetDataControl(id);
    IDataControlTreeGrid3 dc = (IDataControlTreeGrid3)UtilityUI.GetDataControlLookup(id);
    if (dc == null)
    {
      string cname = ((ViewListProperties)dcCaller.GetProperties()).LookupDC;
      try
      {
        dc = (IDataControlTreeGrid3)UtilityUI.Create(cname);
      }
      catch (Exception ex)
      {
        UtilityBO.Log(ex);
        X.Msg.Alert("Error", ex.Message + "=" + cname).Show();
        return;
      }
    }
    if (dc != null)
    {
      TreePanel tree = new TreePanel();
      tree.Hidden = false;

      //dclookup.SetTreeGridColumns(TPLookup1.Columns);
      string label = ((ViewListProperties)dcCaller.GetProperties()).LookupLabelQuery;
      dc.SetPageKey();
      IList list = null;
      if (list == null)
      {
        list = dc.View(label);
        Session["Lookup" + id] = list;
      }

      dc.SetPageKey();
      //Ext.Net.TreeNode root = dclookup.CreateRoot(list, ExtTreePanelUtil.TYPE_TREEGRID, ((ViewListProperties)dclookup.GetProperties()).HasTreeRoot);// util.CreateTree("Lookup" + id, list, Keys, new string[] { "Roleid", "." }, new string[] { "Role", ">" }, true);
      Ext.Net.TreeNode root = dc.CreateRoot(list, ExtTreePanelUtil.TYPE_TREELOOKUP, ((ViewListProperties)dc.GetProperties()).HasTreeRoot);// util.CreateTree("Lookup" + id, list, Keys, new string[] { "Roleid", "." }, new string[] { "Role", ">" }, true);
      root.Text = ((ViewListProperties)dc.GetProperties()).TitleFilter;
      TPLookup1.Root.Clear();
      TPLookup1.Root.Add(root);

      MultiSelectionModel multiSelectionModel = new MultiSelectionModel();
      tree.SelectionModel.Add(multiSelectionModel);
    }
    else
    {
      Ext.Net.TreeNode root = new Ext.Net.TreeNode();
      root.Text = GlobalExt.ROOT;
      TPLookup1.Root.Clear();
      TPLookup1.Root.Add(root);
    }
    TPLookup1.LoadContent();
    #endregion
  }

  //object sender, SubmitEventArgs e
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void SubmitNodes(string json)
  {
    int id = GlobalExt.GetRequestI();
    int target = int.Parse(Request["target"]);
    IDataControlUI dcCaller = UtilityUI.GetDataControl(id);
    IDataControlUI dc = (IDataControlUI)UtilityUI.GetDataControlLookup(id);
    if (dc == null)
    {
      string cname = ((ViewListProperties)dcCaller.GetProperties()).LookupDC;
      try
      {
        dc = (IDataControlTreeGrid3)UtilityUI.Create(cname);
      }
      catch (Exception ex)
      {
        UtilityBO.Log(ex);
        X.Msg.Alert("Error", ex.Message + "=" + cname).Show();
        return;
      }
    }

    if (!json.Equals(string.Empty) && !json.Equals("[null]"))
    {
      Dictionary<string, object>[] datas = JSON.Deserialize<Dictionary<string, object>[]>(json);

      foreach (Dictionary<string, object> row in datas)
      {
        UtilityExt.SetValue(dc, row);
        dcCaller.SetPageKey();
        dcCaller.SetPrimaryKey();
        dcCaller.SetFilterKey((BaseBO)dc);
        try
        {
          dcCaller.Insert();
        }
        catch (Exception ex)
        {
          UtilityBO.Log(ex);

        }
      }
    }
    if (target == ExtGridPanelFilter.TREE)
    {
      X.Js.Call("parent.refreshTree");
    }
    else
    {
      X.Js.Call("parent.refreshData");
    }
  }

  public void SubmitNodes(object sender, SubmitEventArgs e)
  {
    int id = GlobalExt.GetRequestI();
    IDataControlUIEntry dcCall = (IDataControlUIEntry)UtilityUI.GetDataControl(id);

    SubmittedNode root = e.RootNode;
    string msg = EnumerateNode(root);
    dcCall.AfterInsert();    

    if (!string.IsNullOrEmpty(msg))
    {
      X.Js.Alert(msg);
    }
    int target = int.Parse(Request["target"]);
    if (target == ExtGridPanelFilter.TREE)
    {
      X.Js.Call("parent.refreshTree");
    }
    else
    {
      X.Js.Call("parent.refreshData");
    }

  }

  private string EnumerateNode(SubmittedNode parent)
  {
    int id = GlobalExt.GetRequestI();
    IDataControlUI dcCaller = UtilityUI.GetDataControl(id);
    IDataControlUI dc = (IDataControlUI)UtilityUI.GetDataControlLookup(id);
    if (dc == null)
    {
      string cname = ((ViewListProperties)dcCaller.GetProperties()).LookupDC;
      try
      {
        dc = (IDataControlTreeGrid3)UtilityUI.Create(cname);
      }
      catch (Exception ex)
      {
        UtilityBO.Log(ex);
        X.Msg.Alert("Error", ex.Message + "=" + cname).Show();
        return "Error";
      }
    }

    string msg = "";
    if (parent.Checked)
    {
      UtilityExt.SetValue(dc, parent.Attributes);//parent.Attributes Json Object\
      dcCaller.SetPageKey();
      dcCaller.SetPrimaryKey();
      dcCaller.SetFilterKey((BaseBO)dc);
      try
      {
        if (((ViewListProperties)dc.GetProperties()).DetilLookupOnly)
        {
          if (parent.Attributes["Type"].Equals("D"))
          {
            dcCaller.Insert();
          }
        }
        else
        {
          dcCaller.Insert();
        }
      }
      catch (Exception ex)
      {
        msg = ex.Message;
        return msg;
        //if (MasterAppConstants.Instance.StatusTesting)
        //{
        //  msg += ex.Message;
        //}
      }
    }
    for (int i = 0; i < parent.Children.Count; i++)
    {
      SubmittedNode node = parent.Children[i];
      msg += EnumerateNode(node);
    }
    return msg;
  }
  protected void LoadPages(object sender, NodeLoadEventArgs e)
  {
    try
    {
      if (!string.IsNullOrEmpty(e.NodeID))
      {
        int id = GlobalExt.GetRequestI();
        IDataControlUI dcCaller = UtilityUI.GetDataControl(id);
        IDataControlTreeGrid3 dclookup = (IDataControlTreeGrid3)UtilityUI.GetDataControlLookup(id);
        if (dclookup == null)
        {
          string cname = ((ViewListProperties)dcCaller.GetProperties()).LookupDC;
          try
          {
            dclookup = (IDataControlTreeGrid3)UtilityUI.Create(cname);
          }
          catch (Exception ex)
          {
            UtilityBO.Log(ex);
            X.Msg.Alert("Error", ex.Message + "=" + cname).Show();
            return;
          }
        }
        IList list = (IList)Session["Lookup" + id];
        if (typeof(IDataControlTreeGrid3).IsInstanceOfType(dclookup))
        {
          ((IDataControlTreeGrid3)dclookup).LoadPages(list, e.NodeID, e.Nodes, ExtTreePanelUtil.TYPE_TREELOOKUP);
        }
      }
    }
    catch (Exception ex) {UtilityBO.Log(ex);
}

  }

}
