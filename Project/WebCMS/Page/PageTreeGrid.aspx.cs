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
using Ext.Net;
using Ext.Net.Utilities;

public partial class PageTreeGrid : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    CoreNET.Common.Base.AssemblyUtils.WriteBeginLog();
    if (GlobalAsp.CekSessionTree(Page))
    {
      #region Inisialisasi
      UtilityUI.SetSessionExtPage(UtilityUI.PAGE_TREE_GRID);
      Methods2.TreeGrid1 = TreeGrid1;
      Methods2.BottomBar1 = BottomBar1;
      IDataControlTreeGrid3 dc = null;
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
      dc = (IDataControlTreeGrid3)UtilityUI.GetDataControl(id);
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

      ((BaseBO)dc).ModePage = ViewListProperties.MODE_TREE;
      Page.Title = ((ViewListProperties)dc.GetProperties()).TitleList;
      #endregion
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
      #region Add Grid Filter
      ExtGridFilter formFilter = new ExtGridFilter(id, ExtGridPanelFilter.TREE);
      if (formFilter.RowCount > 0)
      {
        PanelCenter.Items.Add(formFilter);
        if (!X.IsAjaxRequest)
        {
          CoreNETCompositeField.SetValues(formFilter, dc);
        }
      }
      #endregion
      #region Add TreeGrid
      ExtTreePageUtils.BuildTreeGrid(TreeGrid1, GridMenu, true);
      #endregion
      #region Add Form Entry
      ExtFormDetail.SetToolbarForm(TBDialog1, true);
      PanelBase FormDetail = ExtFormDetail.GetPanelDetil();
      PanelFormEntry.Items.Add(FormDetail);
      //TreePanel tree = new TreePanel() { ID = "TreePanel2", Height = 75, Margins = "5 5 5 5", Width = 300, AutoScroll = true, Split = true };
      //Ext.Net.TreeNode root = new Ext.Net.TreeNode(GlobalExt.ROOT, Icon.Page);
      //tree.Root.Add(root);
      //tree.RootVisible = false;
      //tree.Collapsible = true;
      //tree.Region = Region.North;
      //PanelFormEntry.Add(tree);
      if (typeof(TabPanel).IsInstanceOfType(FormDetail))
      {
        bool vistree = ((ViewListProperties)dc.GetProperties()).VisibleTreeView;
        TabPanel tp = (TabPanel)FormDetail;
        if (vistree && typeof(FormPanel).IsInstanceOfType(tp.Items[0]))
        {
          TreePanel tree = new TreePanel() { ID = "TreePanel2", Height = 75, Margins = "5 5 5 5", Width = 300, AutoScroll = true, Split = true };
          Ext.Net.TreeNode root = new Ext.Net.TreeNode(GlobalExt.ROOT, Icon.Page);
          tree.Root.Add(root);
          tree.RootVisible = false;
          tree.Collapsible = true;
          tree.Region = Region.North;
          PanelFormEntry.Add(tree);
        }
      }
      PanelFormEntry.Add(FormDetail);
      #endregion
      #region Bottom Toolbar
      if (typeof(IExtDataControl).IsInstanceOfType(dc) && ((ViewListProperties)dc.GetProperties()).VisibleBottomBar)
      {
        Toolbar tbbtm = (Toolbar)TreeGrid1.BottomBar[0];
        ((IExtDataControl)dc).SetTotalFields(tbbtm);
      }
      #endregion
      #region !X.IsAjaxRequest
      if (!X.IsAjaxRequest && !Page.IsPostBack)
      {
        SetResourceHidenValues();
        X.Js.Call("refreshData");
      }
      #endregion
    }
    CoreNET.Common.Base.AssemblyUtils.WriteEndLog();
  }
  protected void LoadPages(object sender, NodeLoadEventArgs e)
  {
    int id = GlobalExt.GetRequestI();
    IDataControlTreeGrid3 dc = (IDataControlTreeGrid3)UtilityUI.GetDataControl(id);
    try
    {
      if (!string.IsNullOrEmpty(e.NodeID))
      {
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
      UtilityBO.Log(dc, ex);
    }
  }
  public void SetResourceHidenValues()
  {
    this.Hidden1.Value = ResourceManager.GetInstance().GetIconUrl(Icon.Lorry);
    this.Hidden2.Value = ResourceManager.GetInstance().GetIconUrl(Icon.LorryGo);
    this.Hidden3.Value = ResourceManager.GetInstance().GetIconUrl(Icon.LorryError);
    this.Hidden4.Value = ResourceManager.GetInstance().GetIconUrl(Icon.HouseGo);
    this.Hidden5.Value = ResourceManager.GetInstance().GetIconUrl(Icon.House);
    this.Hidden6.Value = ResourceManager.GetInstance().GetIconUrl(Icon.Money);
    this.Hidden7.Value = ResourceManager.GetInstance().GetIconUrl(Icon.TableGo);
    this.Hidden8.Value = ResourceManager.GetInstance().GetIconUrl(Icon.Exclamation);
    this.Hidden9.Value = ResourceManager.GetInstance().GetIconUrl(Icon.Accept);
  }

  #region DirectMethods
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void RefreshPreview()
  {
    int id = GlobalExt.GetRequestI();
    IDataControlUIEntry dc = (IDataControlUIEntry)UtilityUI.GetDataControl(id);

    string url = Request.UrlReferrer.OriginalString;
    if (typeof(IDataControlUI).IsInstanceOfType(dc))
    {
      url = url.Replace("PageTreeGrid.aspx", "PageTabular.aspx");
    }
    if (!url.Contains("passdc"))
    {
      url += "&passdc=1";
      dc.SetColumnsNull();
    }
    Response.Redirect(url);
  }
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void RefreshPreview2()
  {
    int id = GlobalExt.GetRequestI();
    IDataControlUIEntry dc = (IDataControlUIEntry)UtilityUI.GetDataControl(id);

    string url = Request.UrlReferrer.OriginalString;
    if (typeof(IDataControlUI).IsInstanceOfType(dc))
    {
      url = url.Replace("PageTreeGrid.aspx", "PageTreeGridDetil.aspx");
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
