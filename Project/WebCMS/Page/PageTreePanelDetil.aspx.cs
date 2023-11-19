using CoreNET.Common.Base;
using CoreNET.Common.BO;
using Ext.Net;
using System;
using System.Collections;
using System.Reflection;
using System.Web.UI;

public partial class PageTreePanelDetil : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    CoreNET.Common.Base.AssemblyUtils.WriteBeginLog();
    if (GlobalAsp.CekSessionTree(Page))
    {
      #region Inisialisasi
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
      if (!X.IsAjaxRequest && !Page.IsPostBack)
      {
        Ext.Net.TreeNode treeRoot = Methods2.LoadRoot();/*Khusus TreePanel*/
        TreeGrid1.Root.Add(treeRoot);
      }
      #endregion
      #region Add Form Entry
      ExtFormDetail.SetToolbarForm(TBDialog1, true);
      PanelBase FormDetail = ExtFormDetail.GetPanelDetil();
      PanelFormEntry.Items.Add(FormDetail);
      //if (typeof(TabPanel).IsInstanceOfType(FormDetail))
      //{
      //  bool vistree = ((ViewListProperties)dc.GetProperties()).VisibleTreeView;
      //  TabPanel tp = (TabPanel)FormDetail;
      //  if (vistree && typeof(FormPanel).IsInstanceOfType(tp.Items[0]))
      //  {
      //    TreePanel tree = new TreePanel() { ID = "TreePanel2", Height = 75, Margins = "5 5 5 5", Width = 300, AutoScroll = true, Split = true };
      //    Ext.Net.TreeNode root = new Ext.Net.TreeNode(GlobalExt.ROOT, Icon.Page);
      //    tree.Root.Add(root);
      //    tree.RootVisible = false;
      //    tree.Collapsible = true;
      //    tree.Region = Region.North;
      //    PanelFormEntry.Add(tree);
      //  }
      //}
      //PanelFormEntry.Add(FormDetail);
      #endregion
      #region Bottom Toolbar
      if (typeof(IExtDataControl).IsInstanceOfType(dc))
      {
        Toolbar tbbtm = (Toolbar)TreeGrid1.BottomBar[0];
        ((IExtDataControl)dc).SetTotalFields(tbbtm);
      }
      #endregion
      #region !X.IsAjaxRequest
      if (!X.IsAjaxRequest && !Page.IsPostBack)
      {
        SetResourceHidenValues();
        X.Js.Call("refreshTree");
      }
      #endregion
    }
    CoreNET.Common.Base.AssemblyUtils.WriteEndLog();
  }
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
  public void SetResourceHidenValues()
  {
    Hidden1.Value = ResourceManager.GetInstance().GetIconUrl(Icon.Lorry);
    Hidden2.Value = ResourceManager.GetInstance().GetIconUrl(Icon.LorryGo);
    Hidden3.Value = ResourceManager.GetInstance().GetIconUrl(Icon.LorryError);
    Hidden4.Value = ResourceManager.GetInstance().GetIconUrl(Icon.HouseGo);
    Hidden5.Value = ResourceManager.GetInstance().GetIconUrl(Icon.House);
    Hidden6.Value = ResourceManager.GetInstance().GetIconUrl(Icon.Money);
    Hidden7.Value = ResourceManager.GetInstance().GetIconUrl(Icon.TableGo);
    Hidden8.Value = ResourceManager.GetInstance().GetIconUrl(Icon.Exclamation);
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
      url = url.Replace("PageTreePanelDetil.aspx", "PageTabular.aspx");
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
      url = url.Replace("PageTreePanelDetil.aspx", "PageTreeGrid.aspx");
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