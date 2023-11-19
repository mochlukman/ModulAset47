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
  #region Usadi.Valid49.BO.MatangnrcControl, Usadi.Valid49.Aset.DM
  [Serializable]
  public class MatangnrcControl : BaseDataControlAsetDM, IDataControlTreeGrid3, IHasJSScript
  {
    #region Properties 
    public long Id { get; set; }
    public string Kdper { get; set; }
    public string Nmper { get; set; }
    public int Jnsrek { get; set; }
    public int Mtglevel { get; set; }
    public string Kdnmper { get { return Kdper + " " + Nmper; } }
    public string Thang { get; set; }
    public string Mtgkey { get; set; }
    public string Kdgroup { get; set; }
    #endregion Properties 

    #region Methods 
    public MatangnrcControl()
    {
      XMLName = ConstantTablesAsetDM.XMLMATANGNRC;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Mtgkey" };
      cViewListProperties.IDKey = "Mtgkey";
      cViewListProperties.IDProperty = "Mtgkey";
      cViewListProperties.ReadOnlyFields = new String[] { };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      cViewListProperties.PageSize = 30;

      //MatangnrcControl cMatangnrcGetkdgroup = new MatangnrcControl();
      //cMatangnrcGetkdgroup.Userid = GlobalAsp.GetSessionUser().GetUserID();
      //cMatangnrcGetkdgroup.Load("Getkdgroup");

      //if (cMatangnrcGetkdgroup.Kdgroup == "01")
      //{
      //  cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD;
      //}
      //else
      //{
      //  cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      //}

      return cViewListProperties;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdper=Kode Rekening"), typeof(string), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmper=Nama Rekening"), typeof(string), 70, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdlevel=Level"), typeof(int), 10, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Type=Tipe"), typeof(int), 10, HorizontalAlign.Center));
      return columns;
    }
    public new void SetPageKey()
    {
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
      PemdaControl cPemda = new PemdaControl();
      cPemda.Configid = "cur_thang";
      cPemda.Load("PK");

      Thang = cPemda.Configval;
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
      List<MatangnrcControl> ListData = new List<MatangnrcControl>();
      foreach (MatangnrcControl dc in list)
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
    public new void Insert()
    {

      string sql = @"
            exec [dbo].[WSP_GETMASTER_REKNRC]
            @THANG = N'{0}'
            ";
      sql = string.Format(sql, Thang);
      BaseDataAdapter.ExecuteCmd(this, sql);
    }
    private bool IsValid()
    {
      bool valid = true;
      return valid;
    }
    public override HashTableofParameterRow GetEntries()
    {
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Thang=Tahun Anggaran"), false, 15).SetEnable(false));
      return hpars;
    }
    #endregion Methods 
    #region IDataControlTreeGrid3
    public string ParentKdper { get { return ParentKode(Kdper); } set { } }
    public static bool IsRootCondition(MatangnrcControl dc)
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
      return new string[] { "Id", "Mtglevel", "Mtgkey", "Kdper","Kdnmper", "Nmper"
                            , "Status","Statusicon", "Mtgkey", "Url", "Kdlevel", "Type" };
    }
    public new void SetTreeGridColumns(TreeGridColumnCollection Columns)
    {
      Columns.Add(new TreeGridColumn { Header = ConstantDict.Translate("Kdper=Kode Rekening"), Width = 200, DataIndex = "Kdper", Align = Ext.Net.TextAlign.Left });
      Columns.Add(new TreeGridColumn { Header = ConstantDict.Translate("Nmper=Rekening"), Width = 400, DataIndex = "Nmper", Align = Ext.Net.TextAlign.Left });
      Columns.Add(new TreeGridColumn { Header = ConstantDict.Translate("Kdlevel"), Width = 100, DataIndex = "Kdlevel", Align = Ext.Net.TextAlign.Center });
      Columns.Add(new TreeGridColumn { Header = ConstantDict.Translate("Type=Tipe"), Width = 100, DataIndex = "Type", Align = Ext.Net.TextAlign.Center });
    }
    public Ext.Net.TreeNode CreateRoot(IList list, int typetree, bool withroot)
    {
      string delim_menu = GlobalAsp.DELIMITER_MENU;
      List<MatangnrcControl> localList = (List<MatangnrcControl>)list;
      List<MatangnrcControl> roots = localList.FindAll(i => IsRootCondition(i));

      Ext.Net.TreeNode root = new Ext.Net.TreeNode("Root", "Root", GetIcon());
      Ext.Net.TreeNodeCollection nodes = root.Nodes;
      foreach (MatangnrcControl ctrl in roots)
      {
        nodes.Add(CreateNodeWithOutChildren((List<MatangnrcControl>)list, ctrl, typetree, delim_menu));
      }
      return root;
    }
    public void LoadPages(IList inList, string nodeid, Ext.Net.TreeNodeCollection nodes, int typetree)
    {
      string delim_menu = ">";
      List<MatangnrcControl> list = (List<MatangnrcControl>)inList;
      if (list != null && list.Count > 0)
      {
        MatangnrcControl parent = list.Find(o => o.Kdper.Equals(nodeid));
        if ((parent != null) && (parent.Type.Equals("H")))
        {
          List<MatangnrcControl> children = GetChildren(list, parent);
          foreach (MatangnrcControl ctrl in children)
          {
            nodes.Add(CreateNodeWithOutChildren(list, ctrl, typetree, delim_menu));
          }
        }
      }
    }
    public static List<MatangnrcControl> GetChildren(List<MatangnrcControl> domainset, MatangnrcControl parent)
    {
      List<MatangnrcControl> children = domainset.FindAll(o => o.Kdper.StartsWith(parent.Kdper) && (o.Kdlevel == parent.Kdlevel + 1));
      return children;
    }
    protected TreeNodeBase CreateNodeWithOutChildren(List<MatangnrcControl> list, MatangnrcControl parent, int typetree, string delim_menu)
    {
      TreeNodeBase treeNode;
      List<MatangnrcControl> children = GetChildren(list, parent);
      if (children != null && children.Count > 0)
      {
        treeNode = (AsyncTreeNode)ExtTreeNode.GetExtTreeNode(parent.Kdper,
          ExtTreePanelUtil.GetUraian(parent.Kdnmper, new string[] { "Kdnmper", delim_menu }),
          typetree, GetKeys(), parent, true, Icon.Folder);
      }
      else
      {
        treeNode = ExtTreeNode.GetExtTreeNode(parent.Kdper,
          ExtTreePanelUtil.GetUraian(parent.Kdnmper, new string[] { "Kdnmper", delim_menu }),
          typetree, GetKeys(), parent, false, Icon.Table);
      }
      return treeNode;
    }
    #endregion IDataControlTreeGrid3
    #region Sum All
    public static void Update(List<MatangnrcControl> domainset)
    {
      List<MatangnrcControl> roots = domainset.FindAll(i => IsRootCondition(i));
      if (roots.Count > 0)
      {
        foreach (MatangnrcControl r in roots)
        {
          UpdatedRecFields bobotProgress = SumChildren(domainset, r);
          r.SetValue(bobotProgress);
        }
      }
    }
    static UpdatedRecFields SumChildren(List<MatangnrcControl> domainsetParent, MatangnrcControl parent)
    {
      UpdatedRecFields localBobotProgress = new UpdatedRecFields();
      List<MatangnrcControl> localDomainset = domainsetParent.FindAll(o => o.Mtgkey.StartsWith(parent.Mtgkey));

      List<MatangnrcControl> children = GetChildren(localDomainset, parent);
      int localNumChildren = 0;
      if ((children.Count > 0) && (parent.Type == "H"))
      {
        localBobotProgress.Nchild = children.Count;
        foreach (MatangnrcControl o in children)
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

      public void SetValue(MatangnrcControl rec)
      {
        Nchild = rec.Nchild;
      }
    }
    #endregion
  }
  #endregion
}

