using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System.Xml.Xsl;
using System.Xml;
using System.Linq;
using System.IO;
using System.Text;
using System.Globalization;
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

public partial class Ascx_TreeShared : System.Web.UI.UserControl
{
  #region Property TreeGrid1
  private TreePanel _TreeGrid1;
  public TreePanel TreeGrid1
  {
    get { return _TreeGrid1; }
    set { _TreeGrid1 = value; }
  }
  #endregion
  #region Property BottomBar1
  private Toolbar _BottomBar1;
  public Toolbar BottomBar1
  {
    get { return _BottomBar1; }
    set { _BottomBar1 = value; }
  }
  #endregion

  Hashtable WinFilters;
  Hashtable WinEntries;
  IDataControlUIEntry GetDataControl()
  {
    int id = GlobalExt.GetRequestI();
    string url = Request.Url.AbsoluteUri;
    IDataControlUIEntry dc = null;
    if (url.ToLower().Contains("dialog"))
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
    if (GlobalAsp.CekSession())
    {
      int id = GlobalExt.GetRequestI();
      IDataControlTreeGrid3 dc = (IDataControlTreeGrid3)GetDataControl();
      string url = Request.Url.AbsoluteUri;
      bool lookup = url.ToLower().Contains("dialog");
      WinFilters = ExtWindows.CreateLookupFilterWindow(Page, id, dc, ExtGridPanelFilter.TREE,lookup);
      WinEntries = ExtWindows.CreateLookupEntryWindow(Page, id);
    }
    else
    {
      X.Js.Call("home");
    }
  }
  public void SetTotal(IExtDataControl dc)
  {
    if (dc != null)
    {
      ((IExtDataControl)dc).SetTotalFields(BottomBar1);

      int id = GlobalExt.GetRequestI();
      IList list = GlobalAsp.GetSessionListRows();
      if (list == null)
      {
        FormPanel FPFormFilter1 = ControlUtils.FindControl<FormPanel>(Page.Form, "FormFilter1 ");
        CoreNETCompositeField.GetValues(FPFormFilter1, dc);
        list = dc.View();
        GlobalAsp.SetSessionListRows(id, list);
      }
      dc.SetTotal(BottomBar1);
    }
  }

  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void btnLookup_DirectClick(int mode, string par)
  {
    if (GlobalAsp.CekSessionGrid(Page))
    {
      if (mode == GlobalExt.FILTER)
      {
        ((Window)WinFilters[par]).Render(Page.Form);
        ((Window)WinFilters[par]).Show();
      }
      else
      {
        ((Window)WinEntries[par]).Render(Page.Form);
        ((Window)WinEntries[par]).Show();
      }
    }
  }
  #region DirectMethods
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void ExpandTree(string path)
  {
    TreeGrid1.SelectPath(path);
  }
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void TreeDeleteSelected()
  {
    TreeGrid1.SelectPath(string.Empty);
    try
    {
      //not work untuk upgrade ext.net
      ((DefaultSelectionModel)TreeGrid1.SelectionModel[0]).ClearSelections();
    }
    catch (Exception ex)
    {
      UtilityBO.Log(ex);
    }
  }
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public string RefreshTree()
  {
    if (GlobalAsp.CekSession())
    {
      int id = GlobalExt.GetRequestI();
      GlobalAsp.SetSessionListRows(id, null);
      return LoadTree();
    }
    else
    {
      //X.Js.Call("home");
      return string.Empty;
    }
  }
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public string LoadTree()
  {
    string json = string.Empty;
    Ext.Net.TreeNode root = null;
    LoadTreeDualMode(out json, out root);
    return json;
  }
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public Ext.Net.TreeNode LoadRoot()
  {
    string json = string.Empty;
    Ext.Net.TreeNode root = null;
    LoadTreeDualMode(out json, out root);
    return root;
  }
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  private void LoadTreeDualMode(out string json, out Ext.Net.TreeNode root)
  {
    #region Add TreeGrid

    int id = GlobalExt.GetRequestI();
    IDataControlTreeGrid3 dc = (IDataControlTreeGrid3)UtilityUI.GetDataControl(id);
    IList list = GlobalAsp.GetSessionListRows();
    if (list == null)
    {
      string formpanel = "FormFilter1";
      FormPanel FPFormFilter1 = ControlUtils.FindControl<FormPanel>(Page.Form, formpanel);
      CoreNETCompositeField.GetValues(FPFormFilter1, dc);
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
      list = dc.View();
      GlobalAsp.SetSessionListRows(id, list);
    }
    if (UtilityUI.GetModePage() != UtilityUI.PAGE_MENU)
    {
      root = dc.CreateRoot(list, ExtTreePanelUtil.TYPE_TREEGRID, ((ViewListProperties)dc.GetProperties()).HasTreeRoot);
    }
    else
    {
      root = ((IDataControlMenu)dc).CreateMenu(list, ExtTreePanelUtil.TYPE_TREESUBMENU, ((ViewListProperties)dc.GetProperties()).HasTreeRoot);
    }

    Ext.Net.TreeNodeCollection nodes = new Ext.Net.TreeNodeCollection();
    if (root != null)
    {
      nodes.Add(root);
    }
    #endregion
    #region SetTotal
    if (typeof(IExtDataControl).IsInstanceOfType(dc) && ((ViewListProperties)dc.GetProperties()).VisibleBottomBar)
    {
      SetTotal((IExtDataControl)dc);
    }
    #endregion
    #region SortTree
    //X.Js.Call("SortTreeGrid", new string[] { "2" });
    //if (((ViewListProperties)dc.GetProperties()).SortFields.Length > 0)
    //{
    //  if (((ViewListProperties)dc.GetProperties()).SortDirection == ViewListProperties.DESC)
    //  {
    //    X.Js.Call("SortTreeGridByColumn", new string[] { ((ViewListProperties)dc.GetProperties()).SortFields[0] });
    //  }
    //}
    #endregion
    #region unloading mask
    X.Get("loading-mask").SetStyle("display", "none");
    #endregion
    json = nodes.ToJson();
  }

  #endregion

}