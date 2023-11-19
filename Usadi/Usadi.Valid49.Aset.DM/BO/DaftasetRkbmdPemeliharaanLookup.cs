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
  #region Usadi.Valid49.BO.DaftasetRkbmdPemeliharaanLookupControl, Usadi.Valid49.Aset.DM
  [Serializable]
  public class DaftasetRkbmdPemeliharaanLookupControl : BaseDataControlAsetDM, IDataControlLookup, IDataControlTreeGrid3, IHasJSScript
  {
    #region Properties
    public long Id { get; set; }
    public string Asetkey { get; set; }
    public string Kdaset { get; set; }
    public string Nmaset { get; set; }
    public int Jmlaset { get; set; }
    public string Unitkey { get; set; }
    #endregion Properties 

    #region Methods
    #region Singleton
    private static DaftasetRkbmdPemeliharaanLookupControl _Instance = null;
    public static DaftasetRkbmdPemeliharaanLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new DaftasetRkbmdPemeliharaanLookupControl();
        }
        return _Instance;
      }
    }
    #endregion
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Asetkey" };
      cViewListProperties.IDKey = "Id";
      cViewListProperties.IDProperty = "Id";
      cViewListProperties.ReadOnlyFields = new String[] {  };
      cViewListProperties.SortFields = new String[] { "Kdaset" };
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      cViewListProperties.PageSize = 20;
      cViewListProperties.RefreshParent = true;
      return cViewListProperties;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Jmlaset=Jumlah Barang"), typeof(int), 20, HorizontalAlign.Center));
      return columns;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      if (typeof(IDataControlMenu).IsInstanceOfType(bo))
      {
        Last_by = ((BaseBO)bo).Userid;
      }
      else if (bo.GetProperty("Unitkey") != null)
      {
        Unitkey = bo.GetValue("Unitkey").ToString();
      }
    }

    public new IList View()
    {
      string sql = @"
        exec [dbo].[WSP_RKBMD_DAFTASET]
		    @UNITKEY = N'{0}'
      ";

      sql = string.Format(sql, Unitkey);
      string[] fields = new string[] { "Id", "Kdlevel", "Asetkey", "Kdaset", "Nmaset", "Type", "Jmlaset" };
      List<IDataControl> list = BaseDataAdapter.GetListDC(this, sql, fields);
      List<DaftasetRkbmdPemeliharaanLookupControl> ListData = new List<DaftasetRkbmdPemeliharaanLookupControl>();

      foreach (DaftasetRkbmdPemeliharaanLookupControl dc in list)
      {
        ListData.Add(dc);
      }
      return ListData;
    }
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {

      DaftasetRkbmdPemeliharaanLookupControl dclookup = new DaftasetRkbmdPemeliharaanLookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys = new String[] { "Kdaset", "Nmaset", "Asetkey"};
      string[] targets = new String[] { "Kdaset", "Nmaset", "Asetkey", "Jumlahsah=Jmlaset" };
      ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 20, 75, 0 }, targets)
      {
        Label = "Kode Barang",
        VisibleControls = new bool[] { true, true, !entry },
        AllowRefresh = !entry,
        DCLookup = dclookup,
        IsTree = true,
        SelectionCriteria = ParameterRow.SELECTION_CRITERIA_TYPE,
        SelectionType = "D"
      };
      return par;
    }
    public string GetFieldValueMap()
    {
      return "Kdaset=Nmaset";
    }
    #endregion Methods 
    #region IDataControlTreeGrid3
    public string ParentAsetkey { get { return ParentKode(Asetkey); } set { } }

    public static bool IsRootCondition(DaftasetRkbmdPemeliharaanLookupControl dc)
    {
      return (dc.Kdlevel == 2);
    }
    public Icon GetIcon()
    {
      return Icon.Table;
    }
    /*pastikan GetKeys-> ...Urmenu,URL,Kdlevel,Type */
    public new string[] GetKeys()
    {
      return new string[] { "Id", "Asetkey", "Kdaset", "Nmaset", "Jmlaset", "Status", "Statusicon", "Kdlevel", "Type" };
    }
    public new void SetTreeGridColumns(TreeGridColumnCollection Columns)
    {

      Columns.Add(new TreeGridColumn { Header = ConstantDict.Translate("Kdaset=Kode Barang"), Width = 250, DataIndex = "Kdaset", Align = Ext.Net.TextAlign.Left });
      Columns.Add(new TreeGridColumn { Header = ConstantDict.Translate("Nmaset=Nama Barang"), Width = 400, DataIndex = "Nmaset", Align = Ext.Net.TextAlign.Left });
      Columns.Add(new TreeGridColumn { Header = ConstantDict.Translate("Jmlaset=Jumlah Barang"), Width = 150, DataIndex = "Jmlaset", Align = Ext.Net.TextAlign.Center });
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
      List<DaftasetRkbmdPemeliharaanLookupControl> localList = (List<DaftasetRkbmdPemeliharaanLookupControl>)list;
      List<DaftasetRkbmdPemeliharaanLookupControl> roots = localList.FindAll(i => IsRootCondition(i));

      Ext.Net.TreeNode root = new Ext.Net.TreeNode("Root", "Root", GetIcon());
      Ext.Net.TreeNodeCollection nodes = root.Nodes;
      foreach (DaftasetRkbmdPemeliharaanLookupControl ctrl in roots)
      {
        nodes.Add(CreateNodeWithOutChildren((List<DaftasetRkbmdPemeliharaanLookupControl>)list, ctrl, typetree, delim_menu));
      }
      return root;
    }
    public void LoadPages(IList inList, string nodeid, Ext.Net.TreeNodeCollection nodes, int typetree)
    {
      string delim_menu = ">";
      List<DaftasetRkbmdPemeliharaanLookupControl> list = (List<DaftasetRkbmdPemeliharaanLookupControl>)inList;
      if (list != null && list.Count > 0)
      {
        DaftasetRkbmdPemeliharaanLookupControl parent = list.Find(o => o.Asetkey.Equals(nodeid));
        if ((parent != null) && (parent.Type.Equals("H")))
        {
          List<DaftasetRkbmdPemeliharaanLookupControl> children = GetChildren(list, parent);
          foreach (DaftasetRkbmdPemeliharaanLookupControl ctrl in children)
          {
            nodes.Add(CreateNodeWithOutChildren(list, ctrl, typetree, delim_menu));
          }
        }
      }
    }
    public static List<DaftasetRkbmdPemeliharaanLookupControl> GetChildren(List<DaftasetRkbmdPemeliharaanLookupControl> domainset, DaftasetRkbmdPemeliharaanLookupControl parent)
    {
      List<DaftasetRkbmdPemeliharaanLookupControl> children = domainset.FindAll(o => o.Kdaset.StartsWith(parent.Kdaset) && (o.Kdlevel == parent.Kdlevel + 1));
      return children;
    }
    protected TreeNodeBase CreateNodeWithOutChildren(List<DaftasetRkbmdPemeliharaanLookupControl> list, DaftasetRkbmdPemeliharaanLookupControl parent, int typetree, string delim_menu)
    {
      TreeNodeBase treeNode;
      List<DaftasetRkbmdPemeliharaanLookupControl> children = GetChildren(list, parent);
      if (children != null && children.Count > 0)
      {
        treeNode = (AsyncTreeNode)ExtTreeNode.GetExtTreeNode(parent.Asetkey,
          ExtTreePanelUtil.GetUraian(parent.Asetkey, new string[] { "Asetkey", delim_menu }),
          typetree, GetKeys(), parent, true, Icon.Folder);
      }
      else
      {
        treeNode = ExtTreeNode.GetExtTreeNode(parent.Asetkey,
          ExtTreePanelUtil.GetUraian(parent.Asetkey, new string[] { "Asetkey", delim_menu }),
          typetree, GetKeys(), parent, false, Icon.Table);
      }
      return treeNode;
    }
    #endregion IDataControlTreeGrid3
  }

  #endregion DaftasetRkbmdLookup
}

