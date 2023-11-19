using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.HtmlControls;
using Ext.Net;
using Ext.Net.Utilities;
using System.Linq;
using System.Reflection;
using System.Text;
using CoreNET.Common.Base;
using CoreNET.Common.BO;

public partial class PageMenu : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (GlobalAsp.CekSessionTree(Page))
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
      #region Add Grid Filter
      ExtGridFilter formFilter = new ExtGridFilter(id, ExtGridPanelFilter.GRID);
      if (formFilter.RowCount > 0)
      {
        Viewport1.Items.Add(formFilter);
        if (!X.IsAjaxRequest)
        {
          CoreNETCompositeField.SetValues(formFilter, dc);
        }
      }
      #endregion
      #region Add TreeGrid
      ExtTreePageUtils.BuildTreeGrid(TreeGrid1, null, true);
      if (!X.IsAjaxRequest && !Page.IsPostBack)
      {
        Ext.Net.TreeNode treeRoot = Methods2.LoadRoot();/*Khusus TreePanel*/
        TreeGrid1.Root.Add(treeRoot);
        TreeGrid1.RootVisible = true;
      }
      TreeGrid1.Listeners.Click.Handler = "if (node.attributes.href) { CoreNET.SaveSessionPage(node.attributes.href,node.attributes.id,node.getPath('id','/'));e.stopEvent(); loadNode(#{Pages}, node,node.getPath('id','/')); }";//
      string script = @"
            if(#{Pages}.activeTab!=null){
              CoreNET.ExpandTreePanel(#{Pages}.activeTab.id);
              var title=#{Pages}.activeTab.id
              var obj = document.getElementById(title+'_IFrame')
              if(!!obj){
                if(!!obj.contentWindow.refreshDataWithSelection){
                  obj.contentWindow.refreshDataWithSelection();
                }else{
                  obj.contentWindow.location.reload();
                }
              }
            }
          ";
      Pages.Listeners.TabChange.Handler = script;
      #endregion

      #region !X.IsAjaxRequest
      if (!X.IsAjaxRequest)
      {
        Title = (string)GlobalAsp.GetSessionAppValue(MasterAppConstants.APPTITLE);
        TreeGrid1.Title = Title;
        //try
        //{
        //  CreateNode(TreeGrid1);
        //}
        //catch (Exception ex)
        //{
        //  TreeGrid1.Hidden = true;
        //  WindowDebug.ShowMessage(Page, ConstantDictExt.TranslateException(ex, MethodBase.GetCurrentMethod().Name));
        //  return;
        //}
      }
      #endregion
    }
    else
    {
      X.Js.Call("home");
      string urlout = GlobalAsp.GetLogoutURL() + "?local=" + Request["local"] + "&kdapp=" + GlobalAsp.GetRequestKdapp()
        + "&sub=" + GlobalAsp.GetRequestSub();
      Response.Redirect(urlout);
    }
  }
  //private void CreateNode(TreePanel tree)
  //{
  //  tree.Icon = Icon.BookOpen;
  //  tree.Title = ConstantDictExt.Translate("LIST_MENU");
  //  tree.AutoScroll = true;

  //  StatusBar statusBar = new StatusBar();
  //  statusBar.AutoClear = 1000;
  //  tree.BottomBar.Add(statusBar);

  //  tree.Listeners.Click.Handler = statusBar.ClientID + ".setStatus({text: 'Node Selected: <b>' + node.text + '</b>', clear: true});";
  //  tree.Listeners.ExpandNode.Handler = statusBar.ClientID + ".setStatus({text: 'Node Expanded: <b>' + node.text + '</b>', clear: true});";
  //  tree.Listeners.ExpandNode.Delay = 30;
  //  tree.Listeners.CollapseNode.Handler = statusBar.ClientID + ".setStatus({text: 'Node Collapsed: <b>' + node.text + '</b>', clear: true});";

  //  IList list = null;
  //  Ext.Net.TreeNode root = null;
  //  IDataControlAppuserUI cUserdb = GlobalExt.GetSessionUser();
  //  int i = GlobalAsp.GetRequestI();
  //  IDataControlMenu cMenu = (IDataControlMenu)UtilityUI.GetDataControl(i);// (IDataControlMenu)GlobalExt.GetSessionMenu();
  //  try
  //  {
  //    //Beda dengan MainMenu.aspx, ada query string qs menu
  //    list = (IList)GlobalExt.GetSessionListSubMenu();
  //    if (list == null)
  //    {
  //      cMenu.SetPageKey();
  //      list = (IList)cMenu.View();//polimorphisme using qs menu=
  //    }
  //    if (list != null && list.Count > 0)
  //    {
  //      GlobalExt.SetSessionListSubMenu(list);

  //      root = ((IDataControlMenu)cMenu).CreateMenu(list, ExtTreePanelUtil.TYPE_TREESUBMENU, ((ViewListProperties)cMenu.GetProperties()).HasTreeRoot);
  //    }
  //    else
  //    {
  //      root = new Ext.Net.TreeNode();
  //    }
  //    tree.Root.Add(root);
  //    tree.Listeners.Click.Handler = "if (node.attributes.href) { CoreNET.SaveSessionPage(node.attributes.href,node.attributes.id,node.getPath('id','/'));e.stopEvent(); loadNode(#{Pages}, node,node.getPath('id','/')); }";
  //  }
  //  catch (Exception ex)
  //  {
  //    if (MasterAppConstants.Instance.StatusTesting)
  //    {
  //      throw new Exception(ex.Message + " at " + ex.StackTrace);
  //    }
  //    else
  //    {
  //      TreeGrid1.Hidden = true;
  //      WindowDebug.ShowMessage(Page, ConstantDictExt.TranslateException(ex, MethodBase.GetCurrentMethod().Name));
  //      return;
  //    }
  //  }

  //}

  protected void LoadPages(object sender, NodeLoadEventArgs e)
  {
    try
    {
      if (!string.IsNullOrEmpty(e.NodeID))
      {
        int id = GlobalExt.GetRequestI();
        IDataControlTreeGrid3 dc = (IDataControlTreeGrid3)UtilityUI.GetDataControl(id);
        IList list = GlobalAsp.GetSessionListRows();
        if (list != null)
        {
          if (typeof(IDataControlTreeGrid3).IsInstanceOfType(dc))
          {
            ((IDataControlTreeGrid3)dc).LoadPages(list, e.NodeID, e.Nodes, ExtTreePanelUtil.TYPE_TREEGRID);
          }
        }
      }
    }
    catch (Exception ex)
    {
      UtilityBO.Log(ex);
    }
  }

  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void ExpandTreePanel(string tree, string path)
  {
    //Ext.Net.TreePanel TreeGrid1 = Ext.Net.Utilities.ControlUtils.FindControl<Ext.Net.TreePanel>(PnlWest, tree);
    TreeGrid1.SelectPath(path);
  }
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void SaveSessionPage(string url, string roleid, string path)
  {
    UtilityUI.SetSessionMenuPage(url, roleid, false);
  }
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void ExpandRightFrame()
  {
    TreeGrid1.Collapse();
  }
}
