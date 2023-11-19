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
  #region Usadi.Valid49.BO.RkbmdMpgrmControl, Usadi.Valid49.Aset.DM
  [Serializable]
  public class RkbmdMpgrmControl : BaseDataControlAsetDM, IDataControlUIEntry, IDataControlTreeGrid3, IHasJSScript
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
    public ImageCommand[] Cmds
    {
      get
      {
        ImageCommand cmd1 = new ImageCommand()
        {
          CommandName = "ViewRkbmdKegiatan",
          Icon = Icon.PageCopy
        };
        cmd1.ToolTip.Text = "Daftar kegiatan";
        return new ImageCommand[] { cmd1 };
      }
    }
    public string ViewRkbmdKegiatan
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
    public RkbmdMpgrmControl()
    {
      XMLName = ConstantTablesAsetDM.XMLRKBMD_MPGRM;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Idprgrm", "Thang" };
      cViewListProperties.IDKey = "Id";//IDKey for ID Notes
      cViewListProperties.IDProperty = "Id";//UniqueKey in gridview
      cViewListProperties.ReadOnlyFields = new String[] { "Uruskey","Unitkey" };//Key in GetFilters should put here
      cViewListProperties.SortFields = new String[] { "Nuprgrm" };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_EDIT_DEL;
      cViewListProperties.PageSize = 30;
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

    public new void SetFilterKey(BaseBO bo)
    {
      if (typeof(IDataControlMenu).IsInstanceOfType(bo))
      {
        Last_by = ((BaseBO)bo).Userid;
      }
    }
    public new void SetPrimaryKey()
    {
      Unitkey = Uruskey;
      Idprgrm = Idprgrm;
      UtilityUI.GetNoUrut(this, "Idprgrm", 5, BaseDataControl.LAST, Idprgrm, ".");
      UtilityUI.GetNoUrut(this, "Nuprgrm", 2, BaseDataControl.KEY, Nuprgrm, ".");
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
      List<RkbmdMpgrmControl> ListData = new List<RkbmdMpgrmControl>();
      foreach (RkbmdMpgrmControl dc in list)
      {
        ListData.Add(dc);
      }
      //Update(ListData);
      return ListData;
    }
    public override HashTableofParameterRow GetEntries()
    {
      bool enable = true;
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nuprgrm=No."), true, 30).SetEnable(enable));
      hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Nmprgrm=Nama Program"), true, 50).SetEnable(enable));
      return hpars;
    }
    public new void Insert()
    {
      PemdaControl cPemda = new PemdaControl();
      cPemda.Configid = "cur_thang";
      cPemda.Load("PK");
      Thang = cPemda.Configval.Trim();
      //Thang = (Int32.Parse(cPemda.Configval.Trim())+1).ToString();
      base.Insert();
    }

    #endregion Methods
    #region IDataControlTreeGrid3
    public string ParentNuprgrm { get { return ParentKode(Nuprgrm); } set { } }
    
    public static bool IsRootCondition(RkbmdMpgrmControl dc)
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
      List<RkbmdMpgrmControl> localList = (List<RkbmdMpgrmControl>)list;
      List<RkbmdMpgrmControl> roots = localList.FindAll(i => IsRootCondition(i));

      Ext.Net.TreeNode root = new Ext.Net.TreeNode("Root", "Root", GetIcon());
      Ext.Net.TreeNodeCollection nodes = root.Nodes;
      foreach (RkbmdMpgrmControl ctrl in roots)
      {
        nodes.Add(CreateNodeWithOutChildren((List<RkbmdMpgrmControl>)list, ctrl, typetree, delim_menu));
      }
      return root;
    }
    public void LoadPages(IList inList, string nodeid, Ext.Net.TreeNodeCollection nodes, int typetree)
    {
      string delim_menu = ">";
      List<RkbmdMpgrmControl> list = (List<RkbmdMpgrmControl>)inList;
      if (list != null && list.Count > 0)
      {
        RkbmdMpgrmControl parent = list.Find(o => o.Nuprgrm.Equals(nodeid));
        if ((parent != null) && (parent.Type.Equals("H")))
        {
          List<RkbmdMpgrmControl> children = GetChildren(list, parent);
          foreach (RkbmdMpgrmControl ctrl in children)
          {
            nodes.Add(CreateNodeWithOutChildren(list, ctrl, typetree, delim_menu));
          }
        }
      }
    }
    public static List<RkbmdMpgrmControl> GetChildren(List<RkbmdMpgrmControl> domainset, RkbmdMpgrmControl parent)
    {
      List<RkbmdMpgrmControl> children = domainset.FindAll(o => o.Nuprgrm.StartsWith(parent.Nuprgrm) && (o.Kdlevel == parent.Kdlevel + 1));
      return children;
    }
    protected TreeNodeBase CreateNodeWithOutChildren(List<RkbmdMpgrmControl> list, RkbmdMpgrmControl parent, int typetree, string delim_menu)
    {
      TreeNodeBase treeNode;
      List<RkbmdMpgrmControl> children = GetChildren(list, parent);
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
    public static void Update(List<RkbmdMpgrmControl> domainset)
    {
      List<RkbmdMpgrmControl> roots = domainset.FindAll(i => IsRootCondition(i));
      if (roots.Count > 0)
      {
        foreach (RkbmdMpgrmControl r in roots)
        {
          UpdatedRecFields bobotProgress = SumChildren(domainset, r);
          r.SetValue(bobotProgress);
        }
      }
    }
    static UpdatedRecFields SumChildren(List<RkbmdMpgrmControl> domainsetParent, RkbmdMpgrmControl parent)
    {
      UpdatedRecFields localBobotProgress = new UpdatedRecFields();
      List<RkbmdMpgrmControl> localDomainset = domainsetParent.FindAll(o => o.Nuprgrm.StartsWith(parent.Nuprgrm));

      List<RkbmdMpgrmControl> children = GetChildren(localDomainset, parent);
      int localNumChildren = 0;
      if ((children.Count > 0) && (parent.Type == "H"))
      {
        localBobotProgress.Nchild = children.Count;
        foreach (RkbmdMpgrmControl o in children)
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

      public void SetValue(RkbmdMpgrmControl rec)
      {
        Nchild = rec.Nchild;
      }
    }
    #endregion
  }
  #endregion RkbmdMpgrm
}

