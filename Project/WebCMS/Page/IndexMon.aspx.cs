using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreNET.Common.Base;
using CoreNET.Common.BO;
using CoreNET.Common;
using Ext.Net;
using System.Reflection;

public partial class IndexMon : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    #region !X.IsAjaxRequest
    string script = AssemblyUtils.ReadFile("CoreNET.Common.Js.menu.js");
    Page.ClientScript.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), script, true);
    if (!X.IsAjaxRequest)
    {
      try
      {

        Ss00appMonControl dcMenu = new Ss00appMonControl();
        GlobalExt.SetSessionMenu(dcMenu);
        Session[GlobalAsp.SESSION_OBJECT_FOR_LIST1] = dcMenu;

        CreateNode(TreePanel1);

      }
      catch (Exception ex)
      {
        if (MasterAppConstants.Instance.StatusTesting)
        {
          throw new Exception(ex.Message + " at " + ex.StackTrace);
        }
        else
        {
          TreePanel1.Hidden = true;
          WindowDebug.ShowMessage(Page, ConstantDictExt.TranslateException(ex, MethodBase.GetCurrentMethod().Name));
          return;
        }
      }
    }
    #endregion
  }
  private void CreateNode(TreeGrid tree)
  {
    tree.Icon = Icon.BookOpen;
    tree.Title = ConstantDictExt.Translate("LIST_MENU");
    tree.AutoScroll = true;

    StatusBar statusBar = new StatusBar();
    statusBar.AutoClear = 1000;
    tree.BottomBar.Add(statusBar);

    tree.Listeners.Click.Handler = statusBar.ClientID + ".setStatus({text: 'Node Selected: <b>' + node.text + '</b>', clear: true});";
    tree.Listeners.ExpandNode.Handler = statusBar.ClientID + ".setStatus({text: 'Node Expanded: <b>' + node.text + '</b>', clear: true});";
    tree.Listeners.ExpandNode.Delay = 30;
    tree.Listeners.CollapseNode.Handler = statusBar.ClientID + ".setStatus({text: 'Node Collapsed: <b>' + node.text + '</b>', clear: true});";


    IList list = null;
    Ext.Net.TreeNode root = null;
    Ss00appissueControl cMenu = new Ss00appissueControl();// (IDataControlMenu)GlobalExt.GetSessionMenu();
    cMenu.SetTreeGridColumns(tree.Columns);
    try
    {
      list = (IList)GlobalExt.GetSessionListMenu();
      if (list == null)
      {
        cMenu.SetPageKey();
        list = (IList)cMenu.View();
      }
      UtilityUI.SetDataControl(1, cMenu);
      GlobalExt.SetSessionListMenu(list);

      if (list != null && list.Count > 0)
      {
        root = ((IDataControlMenu)cMenu).CreateMenu(list, ExtTreePanelUtil.TYPE_TREEMENU, ((ViewListProperties)cMenu.GetProperties()).HasTreeRoot);
      }
      else
      {
        root = new Ext.Net.TreeNode();
      }
      tree.Root.Add(root);
      tree.Listeners.Click.Handler = "if (node.attributes.href) { CoreNET.SaveSessionPage(node.attributes.href,node.attributes.id,node.getPath('id','/'));e.stopEvent(); loadNode(#{Pages}, node,node.getPath('id','/')); }";
    }
    catch (Exception ex)
    {
      try
      {
        throw new Exception(ex.Message + " at " + ex.StackTrace);
      }
      catch (Exception ex1)
      {
        UtilityBO.Log(ex1);;
        if (MasterAppConstants.Instance.StatusTesting)
        {
          throw new Exception(ConstantDict.Translate("LBL_ERROR_CREATE_MENU"));
        }
        Response.Redirect(GlobalExt.GetLogoutURL());
      }
    }

  }
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void SaveSessionPage(string url, string roleid, string path)
  {
    UtilityUI.SetSessionMenuPage(url, roleid, true);
  }
}



