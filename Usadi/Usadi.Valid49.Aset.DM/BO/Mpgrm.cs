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
  #region Usadi.Valid49.BO.MpgrmControl, Usadi.Valid49.Aset.DM
  [Serializable]
  public class MpgrmControl : BaseDataControlAsetDM, IDataControlUIEntry, IDataControlTreeGrid3, IHasJSScript
  {
    #region Properties
    public long Id { get; set; }
    public string Unitkey { get; set; }
    public string Uruskey { get; set; }
    public string Kdurus { get; set; }
    public string Nmurus { get; set; }
    public string Nmprgrm { get; set; }
    public string Nuprgrm { get; set; }
    public string Kdkegunit { get; set; }
    public string Nukeg { get; set; }
    public string Nmkegunit { get; set; }
    public string Sifat{ get; set; }
    public string Kdunit { get; set; }
    public string Nmunit { get; set; }
    public string Nunmprog { get { return Nuprgrm + " " + Nmprgrm; } }
    public string Thang { get; set; }
    public string Idprgrm { get; set; }
    public string Kdgroup { get; set; }
    public ImageCommand[] Cmds
    {
      get
      {
        ImageCommand cmd1 = new ImageCommand()
        {
          CommandName = "ViewKegiatan",
          Icon = Icon.PageCopy
        };
        cmd1.ToolTip.Text = "Daftar kegiatan";
        return new ImageCommand[] { cmd1 };
      }
    }
    public string ViewKegiatan
    {
      get
      {
        string app = GlobalAsp.GetRequestApp();
        string id = GlobalAsp.GetRequestId();
        string idprev = GlobalAsp.GetRequestId();
        string kode = GlobalAsp.GetRequestKode();
        string idx = GlobalAsp.GetRequestIndex();
        string strenable = "&enable=" + ((Status == 0) ? 1 : 0);
        string url = string.Format("PageTabular.aspx?passdc=1&app={0}&i={1}&id={2}&idprev={3}&kode={4}&idx={5}" + strenable, app, 11, id, idprev, kode, idx);
        return "Daftar Kegiatan :"+url;
      }
    }
    #endregion Properties

    #region Methods
    public MpgrmControl()
    {
      XMLName = ConstantTablesAsetDM.XMLMPGRM;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Idprgrm", "Thang" };
      cViewListProperties.IDKey = "Idprgrm";//IDKey for ID Notes
      cViewListProperties.IDProperty = "Idprgrm";//UniqueKey in gridview
      cViewListProperties.ReadOnlyFields = new String[] { "Uruskey","Unitkey" };//Key in GetFilters should put here
      cViewListProperties.SortFields = new String[] { "Nuprgrm" };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      cViewListProperties.PageSize = 30;

      //MpgrmControl cMpgrmGetkdgroup = new MpgrmControl();
      //cMpgrmGetkdgroup.Userid = GlobalAsp.GetSessionUser().GetUserID();
      //cMpgrmGetkdgroup.Load("Getkdgroup");

      //if (cMpgrmGetkdgroup.Kdgroup == "01")
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
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Stricon"), typeof(CommandColumn), Cmds, 5, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nuprgrm=No Program"), typeof(string), 30, HorizontalAlign.Left).SetEditable(false));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmprgrm=Uraian Program"), typeof(string), 100, HorizontalAlign.Left).SetEditable(false));

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
      hpars.Add(DaftunitUrusLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enableFilter));
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
      List<MpgrmControl> ListData = new List<MpgrmControl>();
      foreach (MpgrmControl dc in list)
      {
        ListData.Add(dc);
      }
      //Update(ListData);
      return ListData;
    }
    public override HashTableofParameterRow GetEntries()
    {
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Thang=Tahun Anggaran"), false, 15).SetEnable(false));
      return hpars;
    }
    public new void Insert()
    {

      string sql = @"
            exec [dbo].[WSP_GETMASTER_PROKEG]
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

    #endregion Methods
    #region IDataControlTreeGrid3
    public string ParentNuprgrm { get { return ParentKode(Nuprgrm); } set { } }
    
    public static bool IsRootCondition(MpgrmControl dc)
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
      return new string[] { "Unitkey", "Idprgrm", "Nuprgrm",  "Statusicon", "Nmprgrm", "Nunmprog", "Url", "Kdlevel", "Type" };
    }
    public new void SetTreeGridColumns(TreeGridColumnCollection Columns)
    {
      Columns.Add(new TreeGridColumn { Header = ConstantDict.Translate("Nuprgrm=No"), Width = 150, DataIndex = "Nuprgrm", Align = Ext.Net.TextAlign.Left });
      Columns.Add(new TreeGridColumn { Header = ConstantDict.Translate("Nmprgrm=Uraian"), Width = 500, DataIndex = "Nmprgrm", Align = Ext.Net.TextAlign.Left });

    }
   
    public Ext.Net.TreeNode CreateRoot(IList list, int typetree, bool withroot)
    {
      string delim_menu = GlobalAsp.DELIMITER_MENU;
      List<MpgrmControl> localList = (List<MpgrmControl>)list;
      List<MpgrmControl> roots = localList.FindAll(i => IsRootCondition(i));

      Ext.Net.TreeNode root = new Ext.Net.TreeNode("Root", "Root", GetIcon());
      Ext.Net.TreeNodeCollection nodes = root.Nodes;
      foreach (MpgrmControl ctrl in roots)
      {
        nodes.Add(CreateNodeWithOutChildren((List<MpgrmControl>)list, ctrl, typetree, delim_menu));
      }
      return root;
    }
    public void LoadPages(IList inList, string nodeid, Ext.Net.TreeNodeCollection nodes, int typetree)
    {
      string delim_menu = ">";
      List<MpgrmControl> list = (List<MpgrmControl>)inList;
      if (list != null && list.Count > 0)
      {
        MpgrmControl parent = list.Find(o => o.Nuprgrm.Equals(nodeid));
        if ((parent != null) && (parent.Type.Equals("H")))
        {
          List<MpgrmControl> children = GetChildren(list, parent);
          foreach (MpgrmControl ctrl in children)
          {
            nodes.Add(CreateNodeWithOutChildren(list, ctrl, typetree, delim_menu));
          }
        }
      }
    }
    public static List<MpgrmControl> GetChildren(List<MpgrmControl> domainset, MpgrmControl parent)
    {
      List<MpgrmControl> children = domainset.FindAll(o => o.Nuprgrm.StartsWith(parent.Nuprgrm) && (o.Kdlevel == parent.Kdlevel + 1));
      return children;
    }
    protected TreeNodeBase CreateNodeWithOutChildren(List<MpgrmControl> list, MpgrmControl parent, int typetree, string delim_menu)
    {
      TreeNodeBase treeNode;
      List<MpgrmControl> children = GetChildren(list, parent);
      if (children != null && children.Count > 0)
      {
        treeNode = (AsyncTreeNode)ExtTreeNode.GetExtTreeNode(parent.Nuprgrm,
          ExtTreePanelUtil.GetUraian(parent.Nunmprog, new string[] { "Nunmprog", delim_menu }),
          typetree, GetKeys(), parent, true, Icon.Folder);
      }
      else
      {
        treeNode = ExtTreeNode.GetExtTreeNode(parent.Nuprgrm,
          ExtTreePanelUtil.GetUraian(parent.Nunmprog, new string[] { "Nunmprog", delim_menu }),
          typetree, GetKeys(), parent, false, Icon.Table);
      }
      return treeNode;
    }
    #endregion IDataControlTreeGrid3
    #region Sum All
    public static void Update(List<MpgrmControl> domainset)
    {
      List<MpgrmControl> roots = domainset.FindAll(i => IsRootCondition(i));
      if (roots.Count > 0)
      {
        foreach (MpgrmControl r in roots)
        {
          UpdatedRecFields bobotProgress = SumChildren(domainset, r);
          r.SetValue(bobotProgress);
        }
      }
    }
    static UpdatedRecFields SumChildren(List<MpgrmControl> domainsetParent, MpgrmControl parent)
    {
      UpdatedRecFields localBobotProgress = new UpdatedRecFields();
      List<MpgrmControl> localDomainset = domainsetParent.FindAll(o => o.Nuprgrm.StartsWith(parent.Nuprgrm));

      List<MpgrmControl> children = GetChildren(localDomainset, parent);
      int localNumChildren = 0;
      if ((children.Count > 0) && (parent.Type == "H"))
      {
        localBobotProgress.Nchild = children.Count;
        foreach (MpgrmControl o in children)
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

      public void SetValue(MpgrmControl rec)
      {
        Nchild = rec.Nchild;
      }
    }
    #endregion
  }
  #endregion Mpgrm
}

