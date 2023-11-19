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
  #region Usadi.Valid49.BO.DaftunitControl, Usadi.Valid49.Aset.DM
  [Serializable]
  public class DaftunitControl : BaseDataControlAsetDM, IDataControlTreeGrid3, IHasJSScript
  {
    #region Properties 
    public long Id { get; set; }
    public string Kdunit { get; set; }
    public string Nmunit { get; set; }
    public string Kdnmunit { get; set; }
    public string Akrounit { get; set; }
    public string Alamat { get; set; }
    public string Telepon { get; set; }
    public int Kdlevelmin { get; set; }
    public string Uruskey { get; set; }
    public string Kdtahap { get; set; }
    public string Kdgroup { get; set; }
    public string Unitkey { get; set; }
    public string Unitkey2 { get; set; }
    public string Kdunit2 { get; set; }
    public string Nmunit2 { get; set; }
        #endregion Properties 


        #region Methods 
        public DaftunitControl()
    {
      XMLName = ConstantTablesAsetDM.XMLDAFTUNIT;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Unitkey" };
      cViewListProperties.IDKey = "Unitkey";
      cViewListProperties.IDProperty = "Unitkey";
      cViewListProperties.SortFields = new String[] { "Kdunit" };
      cViewListProperties.ReadOnlyFields = new String[] { };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_EDIT_DEL;
      cViewListProperties.PageSize = 30;
      return cViewListProperties;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdunit"), typeof(string), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmunit"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Akrounit"), typeof(string), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Alamat"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Telepon"), typeof(string), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdlevel"), typeof(string), 10, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Type"), typeof(string), 10, HorizontalAlign.Center));

      return columns;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      if (typeof(IDataControlMenu).IsInstanceOfType(bo))
      {
        Last_by = ((BaseBO)bo).Userid;
      }
    }
    public new void SetPrimaryKey()
    {
      Kdlevel = 4;
      Type = "D";
    }
    public new HashTableofParameterRow GetFilters()
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
      HashTableofParameterRow hpars = new HashTableofParameterRow();

      return hpars;
    }
    public new IList View()
    {
      IList list = this.View(BaseDataControl.ALL);
      return list;
    }
    public new IList View(string label)
    {
      IList list = ((BaseDataControl)this).View(label);
      List<DaftunitControl> ListData = new List<DaftunitControl>();
      foreach (DaftunitControl dc in list)
      {
        ListData.Add(dc);
      }
      //Update(ListData);
      return ListData;
    }
    public new int Delete()
    {
      Status = -1;
      int n = ((BaseDataControlUI)this).Delete(BaseDataControl.DEFAULT);
      return n;
    }
    public override HashTableofParameterRow GetEntries()
    {
      bool enable = true;
      HashTableofParameterRow hpars = new HashTableofParameterRow();

      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Kdunit=Kode Unit"), false, 50).SetEnable(enable));
      hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Nmunit=Nama Unit"), true, 3).SetEnable(enable));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Akrounit"), true, 50).SetEnable(enable));
      hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Alamat"), true, 3).SetEnable(enable).SetAllowEmpty(true));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Telepon"), true, 50).SetEnable(enable));
      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdlevel=Level"),
      GetList(new StruunitLookupControl()), "Level=Nmlevel", 50).SetAllowRefresh(false).SetEnable(enable).SetEditable(false));
      hpars.Add(new ParameterRowType(this, false));

      return hpars;
    }

    public new void Insert()
    {
      if (IsValid())
      {
        bool unik = true;
        DaftunitControl cUnit = new DaftunitControl();
        cUnit.Kdunit = Kdunit;
        IList list = cUnit.View("Kdunit");
        unik = (list.Count == 0);
        if (!unik)
        {
          throw new Exception("Gagal menyimpan data : Kode urusan/unit harus unik!");
        }
        else
        {
          Unitkey = Kdunit.Replace(".", "");

          base.Insert();
        }
      }
      else
      {
        throw new Exception(ConstantDict.Translate("LBL_INVALID_INSERT"));
      }
    }
    private bool IsValid()
    {
      bool valid = true;
      return valid;
    }
    #endregion Methods 

    #region IDataControlTreeGrid3
    public string ParentUnitkey { get { return ParentKode(Unitkey); } set { } }

    public static bool IsRootCondition(DaftunitControl dc)
    {
      //DaftunitControl cDaftunitceklevel = new DaftunitControl();
      //cDaftunitceklevel.Load("Kdlevelmin");
   
      return (dc.Kdlevel == 1);
    }
    public Icon GetIcon()
    {
      return Icon.Table;
    }
    /*pastikan GetKeys-> ...Urmenu,URL,Kdlevel,Type */
    public new string[] GetKeys()
    {
      return new string[] { "Id", "Unitkey", "Kdunit", "Nmunit", "Akrounit", "Alamat", "Telepon", "Status", "Statusicon", "Kdlevel", "Type" };
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
      List<DaftunitControl> localList = (List<DaftunitControl>)list;
      List<DaftunitControl> roots = localList.FindAll(i => IsRootCondition(i));

      Ext.Net.TreeNode root = new Ext.Net.TreeNode("Root", "Root", GetIcon());
      Ext.Net.TreeNodeCollection nodes = root.Nodes;
      foreach (DaftunitControl ctrl in roots)
      {
        nodes.Add(CreateNodeWithOutChildren((List<DaftunitControl>)list, ctrl, typetree, delim_menu));
      }
      return root;
    }
    public void LoadPages(IList inList, string nodeid, Ext.Net.TreeNodeCollection nodes, int typetree)
    {
      string delim_menu = ">";
      List<DaftunitControl> list = (List<DaftunitControl>)inList;
      if (list != null && list.Count > 0)
      {
        DaftunitControl parent = list.Find(o => o.Unitkey.Equals(nodeid));
        if ((parent != null) && (parent.Type.Equals("H")))
        {
          List<DaftunitControl> children = GetChildren(list, parent);
          foreach (DaftunitControl ctrl in children)
          {
            nodes.Add(CreateNodeWithOutChildren(list, ctrl, typetree, delim_menu));
          }
        }
      }
    }
    public static List<DaftunitControl> GetChildren(List<DaftunitControl> domainset, DaftunitControl parent)
    {
      List<DaftunitControl> children = domainset.FindAll(o => o.Kdunit.StartsWith(parent.Kdunit) && (o.Kdlevel == parent.Kdlevel + 1));
      return children;
    }
    protected TreeNodeBase CreateNodeWithOutChildren(List<DaftunitControl> list, DaftunitControl parent, int typetree, string delim_menu)
    {
      TreeNodeBase treeNode;
      List<DaftunitControl> children = GetChildren(list, parent);
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
    #region Sum All
    public static void Update(List<DaftunitControl> domainset)
    {
      List<DaftunitControl> roots = domainset.FindAll(i => IsRootCondition(i));
      if (roots.Count > 0)
      {
        foreach (DaftunitControl r in roots)
        {
          UpdatedRecFields bobotProgress = SumChildren(domainset, r);
          r.SetValue(bobotProgress);
        }
      }
    }
    static UpdatedRecFields SumChildren(List<DaftunitControl> domainsetParent, DaftunitControl parent)
    {
      UpdatedRecFields localBobotProgress = new UpdatedRecFields();
      List<DaftunitControl> localDomainset = domainsetParent.FindAll(o => o.Unitkey.StartsWith(parent.Unitkey));

      List<DaftunitControl> children = GetChildren(localDomainset, parent);
      int localNumChildren = 0;
      if ((children.Count > 0) && (parent.Type == "H"))
      {
        localBobotProgress.Nchild = children.Count;
        foreach (DaftunitControl o in children)
        {
          localNumChildren++;
          UpdatedRecFields childrenBobotProgress = SumChildren(localDomainset, o);
          localBobotProgress.Sum(childrenBobotProgress);
        }
        parent.SetValue(localBobotProgress);
        return localBobotProgress;
      }
      else
      {
        localBobotProgress.SetValue(parent);
        localBobotProgress.Nchild = 0;
        return localBobotProgress;
      }
    }
    void SetValue(UpdatedRecFields rec)
    {
      Nchild = rec.Nchild;
    }
    class UpdatedRecFields
    {
      public int Nchild = 0;

      public void Sum(UpdatedRecFields rec)
      {
        Nchild += rec.Nchild;
      }

      public void SetValue(DaftunitControl rec)
      {
        Nchild = rec.Nchild;
      }
    }
    #endregion
  }
  #endregion Daftunit
}

