using CoreNET.Common.Base;
using CoreNET.Common.BO;
using Ext.Net;
using System;
using System.Collections;
using System.Reflection;
using System.Web.UI;

public partial class PageMenuTreeGrid : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (GlobalAsp.CekSessionTree(Page))
    {
      #region Inisialisasi
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
      if (!Page.IsPostBack && !X.IsAjaxRequest)
      {
        GlobalAsp.SetSessionListRows(id, null);
      }
      IDataControlTreeGrid3 dc = (IDataControlTreeGrid3)UtilityUI.GetDataControl(id);
      dc.SetPageKey();
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
      btnCollapse.Text = "Collapse All";
      btnCollapse.IconCls = "icon-collapse-all2";
      btnCollapse.Listeners.Click.Handler = "#{TreeGrid1}.collapseAll();";
      TopBar1.Add(btnCollapse);

      Ext.Net.Label lblWait = new Ext.Net.Label() { ID = "lblWait" };
      lblWait.Html = @"<div id='loading-mask' style='display:none'><div id='loading'><div class='loading-indicator'>Loading...</div></div></div>";
      TopBar1.Add(lblWait);

      #endregion
      #region Handler
      DefaultSelectionModel selectionModel = new DefaultSelectionModel();
      selectionModel.Listeners.SelectionChange.Handler = @"
        if(#{TreeGrid1}.getSelectedNodes())
        {
          var atrsel = #{TreeGrid1}.getSelectedNodes().attributes;
          CoreNET.RefreshPanelCenter(Ext.encode(atrsel));
        }else
        {
          Ext.Msg.alert('" + ConstantDictExt.Translate(GlobalExt.LBL_INFO) + "','" + ConstantDictExt.Translate("LBL_ERROR_DATA_NOT_SELECTED") + @"');
        }";
      TreeGrid1.SelectionModel.Add(selectionModel);
      #endregion
      #region !X.IsAjaxRequest
      ExtTreePageUtils.BuildTreeGrid(TreeGrid1);
      if (!X.IsAjaxRequest)
      {
        Ext.Net.TreeNode treeRoot = Methods2.LoadRoot();/*Khusus TreePanel*/
        TreeGrid1.Root.Add(treeRoot);
        UtilityExt.SetIFrameAutoLoad(PanelCenter.AutoLoad);
        UtilityExt.LoadUrl(PanelCenter, GlobalExt.GetBlankURL());
      }
      #endregion
    }
    else
    {
      X.Js.Call("home");
    }
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
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void RefreshPanelCenter(string json)
  {
    int imaster = GlobalExt.GetRequestI();
    IDataControlUI dcMaster = UtilityUI.GetDataControl(imaster);
    UtilityExt.SetValue(dcMaster, json);
    int idetil = imaster * 10 + 1;
    IDataControlUI dcDetil = UtilityUI.GetDataControl(idetil);
    dcDetil.SetFilterKey((BaseBO)dcMaster);

    string url = GlobalExt.GetBlankURL();
    if (typeof(IDataControlTreeGrid3).IsInstanceOfType(dcDetil))
    {
      url = string.Format(@"PageTreeGrid.aspx?i={0}&passdc=1&id={1}", idetil.ToString(), GlobalExt.GetRequestId());
    }
    else
    {
      url = string.Format(@"PageTabular.aspx?i={0}&passdc=1&id={1}", idetil.ToString(), GlobalExt.GetRequestId());
    }
    UtilityExt.LoadUrl(PanelCenter, url);
  }

}
