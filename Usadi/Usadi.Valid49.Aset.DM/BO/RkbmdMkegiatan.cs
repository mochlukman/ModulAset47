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
  #region Usadi.Valid49.BO.RkbmdMkegiatanControl, Usadi.Valid49.Aset.DM
  [Serializable]
  public class RkbmdMkegiatanControl : BaseDataControlAsetDM, IDataControlUIEntry,IDataControlTreeGrid3, IHasJSScript
  {
    #region Properties
    public long Id { get; set; }
    public string Idprgrm { get; set; }
    public string Nuprgrm { get; set; }
    public string Nmprgrm { get; set; }
    public string Kdperspektif { get; set; }
    public string Nukeg { get; set; }
    public string Nmkegunit { get; set; }
    public string Nunmkeg { get { return Nukeg + " " + Nmkegunit; } }
    public int Levelkeg { get; set; }
    public string Kdkeg_induk { get; set; }
    public string Uruskey { get; set; }
    public string Kdurus { get; set; }
    public string Nmurus { get; set; }
    public string Kdunit { get; set; }
    public string Nmunit { get; set; }
    public string Unitkey { get; set; }
    public string Thang { get; set; }
    public string Kdkegunit { get; set; }
    #endregion Properties

    #region Methods
    public RkbmdMkegiatanControl()
    {
      XMLName = ConstantTablesAsetDM.XMLRKBMD_MKEGIATAN;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Kdkegunit", "Thang" };
      cViewListProperties.IDKey = "Id";//IDKey for ID Notes
      cViewListProperties.IDProperty = "Id";//UniqueKey in gridview
      cViewListProperties.ReadOnlyFields = new String[] { "Uruskey", "Idprgrm" };//Key in GetFilters should put here
      cViewListProperties.SortFields = new String[] { "Nukeg"};//Key in GetFilters should put here
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      cViewListProperties.PageSize = 30;
      return cViewListProperties;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nukeg=No Kegiatan"), typeof(string), 30, HorizontalAlign.Left).SetEditable(false));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmkegunit=Nama Kegiatan"), typeof(string), 50, HorizontalAlign.Left).SetEditable(false));
      return columns;
    }
    public new void SetPageKey()
    {
    }
    public new void SetFilterKey(BaseBO bo)
    {
      if (typeof(RkbmdMpgrmControl).IsInstanceOfType(bo))
      {
        Uruskey = ((RkbmdMpgrmControl)bo).Uruskey;
        Kdurus = ((RkbmdMpgrmControl)bo).Kdurus;
        Nmurus = ((RkbmdMpgrmControl)bo).Nmurus;
        Idprgrm = ((RkbmdMpgrmControl)bo).Idprgrm;
        Nuprgrm = ((RkbmdMpgrmControl)bo).Nuprgrm;
        Nmprgrm = ((RkbmdMpgrmControl)bo).Nmprgrm;
      }
    }
    public new void SetPrimaryKey()
    {
      Idprgrm = Idprgrm;
      Kdkegunit = Guid.NewGuid().ToString();
      UtilityUI.GetNoUrut(this, "Nukeg", 3, BaseDataControl.KEY, Nukeg, ".");
    }
    public new int Delete()
    {
      Status = -1;
      int n = ((BaseDataControlUI)this).Delete(BaseDataControl.DEFAULT);
      return n;
    }
    public new HashTableofParameterRow GetFilters()
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      //hpars.Add(DaftunitUrusLookupControl.Instance.GetLookupParameterRow(this, true).SetAllowRefresh(false).SetEnable(enableFilter));
      //hpars.Add(RkbmdMpgrmLookupControl.Instance.GetLookupParameterRow(this, true).SetAllowRefresh(false).SetEnable(enableFilter));
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
      List<RkbmdMkegiatanControl> ListData = new List<RkbmdMkegiatanControl>();
      foreach (RkbmdMkegiatanControl dc in list)
      {
        dc.Idprgrm = Idprgrm;
        dc.Nuprgrm = Nuprgrm;
        dc.Nmprgrm = Nmprgrm;
        ListData.Add(dc);
      }
      //Update(ListData);
      return ListData;
    }
    public override HashTableofParameterRow GetEntries()
    {
      bool enable = true;
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nukeg=No"), false, 30).SetEnable(enable));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nmkegunit=Nama Kegiatan"), false, 90).SetEnable(enable));
      //hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdperspektif"),
      //GetList(new JperspektifLookupControl()), "Kdperspektif=Nmperspektif", 50).SetAllowRefresh(false).SetEnable(enable));
      return hpars;
    }


    #endregion Methods
    #region IDataControlTreeGrid3
    public string ParentNukeg { get { return ParentKode(Nukeg); } set { } }
    //public IList View()
    //{
    //  IList list = base.View();
    //  List<RkbmdMkegiatanControl> ListData = new List<RkbmdMkegiatanControl>();
    //  foreach(RkbmdMkegiatanControl dc in list)
    //  {
    //    ListData.Add(dc);
    //  }
    //  return ListData;
    //}
    public static bool IsRootCondition(RkbmdMkegiatanControl dc)
    {
      return (dc.Kdlevel == 1);
    }
    public Icon GetIcon()
    {
      return Icon.Table;
    }
    /*pastikan GetKeys-> ...Urmenu,URL,Kdlevel,Type */
    public new string[] GetKeys()
    {
      return new string[] { "Id", "Kdkegunit", "Idprgrm", "Kdperspektif", "Nukeg", "Nmkegunit", "Nunmkeg", "Kdkeg_induk"
        , "Statusicon",  "Url", "Kdlevel", "Type" };
    }
    public new void SetTreeGridColumns(TreeGridColumnCollection Columns)
    {
      Columns.Add(new TreeGridColumn { Header = ConstantDict.Translate("Nukeg=No Kegiatan"), Width = 150, DataIndex = "Nukeg", Align = Ext.Net.TextAlign.Left });
      Columns.Add(new TreeGridColumn { Header = ConstantDict.Translate("Nmkegunit=Uraian"), Width = 500, DataIndex = "Nmkegunit", Align = Ext.Net.TextAlign.Left });
    }
  
    public Ext.Net.TreeNode CreateRoot(IList list, int typetree, bool withroot)
    {
      string delim_menu = GlobalAsp.DELIMITER_MENU;
      List<RkbmdMkegiatanControl> localList = (List<RkbmdMkegiatanControl>)list;
      List<RkbmdMkegiatanControl> roots = localList.FindAll(i => IsRootCondition(i));

      Ext.Net.TreeNode root = new Ext.Net.TreeNode("Root", "Root", GetIcon());
      Ext.Net.TreeNodeCollection nodes = root.Nodes;
      foreach (RkbmdMkegiatanControl ctrl in roots)
      {
        nodes.Add(CreateNodeWithOutChildren((List<RkbmdMkegiatanControl>)list, ctrl, typetree, delim_menu));
      }
      return root;
    }
    public void LoadPages(IList inList, string nodeid, Ext.Net.TreeNodeCollection nodes, int typetree)
    {
      string delim_menu = ">";
      List<RkbmdMkegiatanControl> list = (List<RkbmdMkegiatanControl>)inList;
      if (list != null && list.Count > 0)
      {
        RkbmdMkegiatanControl parent = list.Find(o => o.Nukeg.Equals(nodeid));
        if ((parent != null) && (parent.Type.Equals("H")))
        {
          List<RkbmdMkegiatanControl> children = GetChildren(list, parent);
          foreach (RkbmdMkegiatanControl ctrl in children)
          {
            nodes.Add(CreateNodeWithOutChildren(list, ctrl, typetree, delim_menu));
          }
        }
      }
    }
    public static List<RkbmdMkegiatanControl> GetChildren(List<RkbmdMkegiatanControl> domainset, RkbmdMkegiatanControl parent)
    {
      List<RkbmdMkegiatanControl> children = domainset.FindAll(o => o.Nukeg.StartsWith(parent.Nukeg) && (o.Kdlevel == parent.Kdlevel + 1));
      return children;
    }
    protected TreeNodeBase CreateNodeWithOutChildren(List<RkbmdMkegiatanControl> list, RkbmdMkegiatanControl parent, int typetree, string delim_menu)
    {
      TreeNodeBase treeNode;
      List<RkbmdMkegiatanControl> children = GetChildren(list, parent);
      if (children != null && children.Count > 0)
      {
        treeNode = (AsyncTreeNode)ExtTreeNode.GetExtTreeNode(parent.Nukeg,
          ExtTreePanelUtil.GetUraian(parent.Nunmkeg, new string[] { "Nunmkeg", delim_menu }),
          typetree, GetKeys(), parent, true, Icon.Folder);
      }
      else
      {
        treeNode = ExtTreeNode.GetExtTreeNode(parent.Nukeg,
          ExtTreePanelUtil.GetUraian(parent.Nunmkeg, new string[] { "Nunmkeg", delim_menu }),
          typetree, GetKeys(), parent, false, Icon.Table);
      }
      return treeNode;
    }
    #endregion IDataControlTreeGrid3
    #region Sum All
    public static void Update(List<RkbmdMkegiatanControl> domainset)
    {
      List<RkbmdMkegiatanControl> roots = domainset.FindAll(i => IsRootCondition(i));
      if (roots.Count > 0)
      {
        foreach (RkbmdMkegiatanControl r in roots)
        {
          UpdatedRecFields bobotProgress = SumChildren(domainset, r);
          r.SetValue(bobotProgress);
        }
      }
    }
    static UpdatedRecFields SumChildren(List<RkbmdMkegiatanControl> domainsetParent, RkbmdMkegiatanControl parent)
    {
      UpdatedRecFields localBobotProgress = new UpdatedRecFields();
      List<RkbmdMkegiatanControl> localDomainset = domainsetParent.FindAll(o => o.Nukeg.StartsWith(parent.Nukeg));

      List<RkbmdMkegiatanControl> children = GetChildren(localDomainset, parent);
      int localNumChildren = 0;
      if ((children.Count > 0) && (parent.Type == "H"))
      {
        localBobotProgress.Nchild = children.Count;
        foreach (RkbmdMkegiatanControl o in children)
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

      public void SetValue(RkbmdMkegiatanControl rec)
      {
        Nchild = rec.Nchild;
      }
    }
    #endregion
  }

  #endregion RkbmdMkegiatan
}

