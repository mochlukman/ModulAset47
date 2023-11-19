using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Generic;
using Ext.Net;
using Ext.Net.Utilities;
using CoreNET.Common.Base;
using CoreNET.Common.BO;

namespace Usadi.Valid49.BO
{
  #region Usadi.Valid49.BO.DaftunitUserskpdControl, Usadi.Valid49.Aset.DM
  [Serializable]
  public class DaftunitUserskpdControl : BaseDataControlAsetDM, IDataControlTreeGrid3, IHasJSScript
  {
    #region Properties
    public long Id { get; set; }
    public string Kdunit { get; set; }
    public string Nmunit { get; set; }
    public string Kdnmunit { get; set; }
    public string Akrounit { get; set; }
    public string Alamat { get; set; }
    public string Telepon { get; set; }
    public string Unitkey { get; set; }

    #endregion Properties 

    #region Methods 
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      return cViewListProperties;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdunit"), typeof(string), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmunit"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdlevel"), typeof(string), 10, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Type"), typeof(string), 10, HorizontalAlign.Center));

      return columns;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      Userid = (string)bo.GetValue("Userid");
    }
    public new IList View(string label)
    {
      string sql = @"
        exec [dbo].[TREEVIEWUNIT_USERSKPD]
		    @USERID = N'{0}'
      ";
      sql = string.Format(sql, Userid);
      string[] fields = new string[] { "Id", "Unitkey", "Kdlevel", "Kdunit", "Nmunit", "Akrounit", "Alamat", "Telepon", "Type", "Kdnmunit" };
      List<IDataControl> list = BaseDataAdapter.GetListDC(this, sql, fields);
      List<DaftunitUserskpdControl> ListData = new List<DaftunitUserskpdControl>();

      foreach (DaftunitUserskpdControl dc in list)
      {
        ListData.Add(dc);
      }
      return ListData;
    }
    //public new IList View(string label)
    //{
    //  IList list = ((BaseDataControl)this).View(label);
    //  List<DaftunitUserskpdControl> ListData = new List<DaftunitUserskpdControl>();
    //  foreach (DaftunitUserskpdControl dc in list)
    //  {
    //    ListData.Add(dc);
    //  }
    //  //Update(ListData);
    //  return ListData;
    //}
    #endregion Methods 

    #region IDataControlTreeGrid3
    public string ParentUnitkey { get { return ParentKode(Unitkey); } set { } }

    public static bool IsRootCondition(DaftunitUserskpdControl dc)
    {
      //DaftunitUserskpdControl cDaftunitceklevel = new DaftunitUserskpdControl();
      //cDaftunitceklevel.Load("Kdlevelmin");

      return (dc.Kdlevel == 3);
    }
    public Icon GetIcon()
    {
      return Icon.Table;
    }
    /*pastikan GetKeys-> ...Urmenu,URL,Kdlevel,Type */
    public new string[] GetKeys()
    {
      return new string[] { "Id", "Unitkey", "Kdunit", "Nmunit", "Kdnmunit", "Akrounit", "Alamat", "Telepon", "Status", "Statusicon", "Kdlevel", "Type" };
    }
    public new void SetTreeGridColumns(TreeGridColumnCollection Columns)
    {

      Columns.Add(new TreeGridColumn { Header = ConstantDict.Translate("Kdunit=Kode Unit"), Width = 200, DataIndex = "Kdunit", Align = Ext.Net.TextAlign.Left });
      Columns.Add(new TreeGridColumn { Header = ConstantDict.Translate("Nmunit=Nama Unit"), Width = 450, DataIndex = "Nmunit", Align = Ext.Net.TextAlign.Left });
      Columns.Add(new TreeGridColumn { Header = ConstantDict.Translate("Kdlevel"), Width = 100, DataIndex = "Kdlevel", Align = Ext.Net.TextAlign.Center });
      Columns.Add(new TreeGridColumn { Header = ConstantDict.Translate("Type"), Width = 100, DataIndex = "Type", Align = Ext.Net.TextAlign.Center });
      //Columns.Add(new TreeGridColumn { Header = ConstantDict.Translate("Stricon"), Width = 50, DataIndex = "Kdunit", Align = Ext.Net.TextAlign.Left });

    }
    /*Create Root Static, not asynchronus, pastikan 4 kolom terakhir GetKeys-> ...Urmenu,URL,Kdlevel,Type */
    /*public Ext.Net.TreeNode CreateRoot(IList list, int typetree, bool withroot)
    {
      string[] Keys = GetKeys();
      ExtTreePanelUtil util = new ExtTreePanelUtil();
      return util.CreateTree(ConstantDict.Translate("Daftar Menu"), list, Keys, new string[] { "Unitkey", "." }, new string[] { "Unitkey", ">" }, typetree, withroot);
    }*/
    public Ext.Net.TreeNode CreateRoot(IList list, int typetree, bool withroot)
    {
      string delim_menu = GlobalAsp.DELIMITER_MENU;
      List<DaftunitUserskpdControl> localList = (List<DaftunitUserskpdControl>)list;
      List<DaftunitUserskpdControl> roots = localList.FindAll(i => IsRootCondition(i));

      Ext.Net.TreeNode root = new Ext.Net.TreeNode("Root", "Root", GetIcon());
      Ext.Net.TreeNodeCollection nodes = root.Nodes;
      foreach (DaftunitUserskpdControl ctrl in roots)
      {
        nodes.Add(CreateNodeWithOutChildren((List<DaftunitUserskpdControl>)list, ctrl, typetree, delim_menu));
      }
      return root;
    }
    public void LoadPages(IList inList, string nodeid, Ext.Net.TreeNodeCollection nodes, int typetree)
    {
      string delim_menu = ">";
      List<DaftunitUserskpdControl> list = (List<DaftunitUserskpdControl>)inList;
      if (list != null && list.Count > 0)
      {
        DaftunitUserskpdControl parent = list.Find(o => o.Unitkey.Equals(nodeid));
        if ((parent != null) && (parent.Type.Equals("H")))
        {
          List<DaftunitUserskpdControl> children = GetChildren(list, parent);
          foreach (DaftunitUserskpdControl ctrl in children)
          {
            nodes.Add(CreateNodeWithOutChildren(list, ctrl, typetree, delim_menu));
          }
        }
      }
    }
    public static List<DaftunitUserskpdControl> GetChildren(List<DaftunitUserskpdControl> domainset, DaftunitUserskpdControl parent)
    {
      List<DaftunitUserskpdControl> children = domainset.FindAll(o => o.Kdunit.StartsWith(parent.Kdunit) && (o.Kdlevel == parent.Kdlevel + 1));
      return children;
    }
    protected TreeNodeBase CreateNodeWithOutChildren(List<DaftunitUserskpdControl> list, DaftunitUserskpdControl parent, int typetree, string delim_menu)
    {
      TreeNodeBase treeNode;
      List<DaftunitUserskpdControl> children = GetChildren(list, parent);
      if (children != null && children.Count > 0)
      {
        treeNode = (AsyncTreeNode)ExtTreeNode.GetExtTreeNode(parent.Unitkey,
          ExtTreePanelUtil.GetUraian(parent.Kdnmunit, new string[] { "Kdnmunit", delim_menu }),
          typetree, GetKeys(), parent, true, Icon.Folder);
      }
      else
      {
        treeNode = ExtTreeNode.GetExtTreeNode(parent.Unitkey,
          ExtTreePanelUtil.GetUraian(parent.Kdnmunit, new string[] { "Kdnmunit", delim_menu }),
          typetree, GetKeys(), parent, false, Icon.Table);
      }
      return treeNode;
    }
    #endregion IDataControlTreeGrid3
  }
  #endregion DaftunitUserskpd

}

