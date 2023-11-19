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
    #region Usadi.Valid49.BO.DaftasetControl, Usadi.Valid49.DM
    [Serializable]
    public class DaftasetControl : BaseDataControlAsetDM, IDataControlTreeGrid3, IHasJSScript, IDataControlUIEntry
    {
        #region Properties 
        public long Id { get; set; }
        public string Mtglevel { get; set; }
        public string Kdaset { get; set; }
        public string Nmaset { get; set; }
        public decimal Umeko { get; set; }
        public decimal Nklas { get; set; }
        public string Kdkib { get; set; }
        public string Golkib { get; set; }
        public int Kdlevelmin { get; set; }
        public string Mtgkey { get; set; }
        public string Kddana { get; set; }
        public string Kdnmaset { get { return Golkib + " - " + Kdaset + " - " + Nmaset; } }
        public string Asetkey { get; set; }
        #endregion Properties 

        #region Methods 
        public DaftasetControl()
        {
            XMLName = ConstantTablesAsetDM.XMLDAFTASET;
        }
        public new IProperties GetProperties()
        {
            ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
            cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
            cViewListProperties.PrimaryKeys = new String[] { "Asetkey" };
            cViewListProperties.IDKey = "Asetkey";
            cViewListProperties.IDProperty = "Asetkey";
            cViewListProperties.ReadOnlyFields = new String[] { };
            cViewListProperties.SortFields = new String[] { "Kdaset" };
            cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
            cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_EDIT_DEL;
            cViewListProperties.PageSize = 30;
            cViewListProperties.AllowMultiDelete = true;
            return cViewListProperties;
        }
        public override DataControlFieldCollection GetColumns()
        {
            DataControlFieldCollection columns = new DataControlFieldCollection();
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Stricon"), typeof(string), EditCmd, 5, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 30, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Uraian"), typeof(string), 50, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Mtglevel=Level"), typeof(string), 10, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Type=Tipe"), typeof(string), 10, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Umeko=Umur Ekonomis"), typeof(decimal), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nklas=Nilai Klasifikasi"), typeof(decimal), 20, HorizontalAlign.Right));
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
            Kdlevel = 7;
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
            List<DaftasetControl> ListData = new List<DaftasetControl>();
            foreach (DaftasetControl dc in list)
            {
                ListData.Add(dc);
            }
            //Update(ListData);
            return ListData;
        }

        private bool IsValid()
        {
            bool valid = true;
            return valid;
        }
        public new void Insert()
        {
            if (IsValid())
            {
                bool unik = true;
                DaftasetControl cUnit = new DaftasetControl();
                cUnit.Kdaset = Kdaset;
                IList list = cUnit.View("Kdaset");
                unik = (list.Count == 0);
                if (!unik)
                {
                    throw new Exception("Gagal menyimpan data : Kode barang harus unik!");
                }
                else
                {
                    Asetkey = Kdaset.Replace(".", "");
                    base.Insert();
                }
            }
            else
            {
                throw new Exception(ConstantDict.Translate("LBL_INVALID_INSERT"));
            }
        }
        public new int Update()
        {
            int n = 0;
            string ErrMsg = string.Empty;
            try
            {
                if (Type.Equals("H"))
                {
                    try
                    {
                        new DaftasetControl
                        {
                            Asetkey = this.Asetkey,
                            Kdaset = this.Kdaset,
                            Nmaset = this.Nmaset,
                            Kdlevel = this.Kdlevel,
                            Type = this.Type,
                            Nklas = this.Nklas,
                            Umeko = this.Umeko
                        }.Update("Header");
                    }
                    catch (Exception exception)
                    {
                        throw exception;
                    }
                }
                else
                {
                    base.Update();
                }
            }
            catch (Exception ex)
            {
                ErrMsg = ex.Message;
                throw new Exception(ErrMsg + " Error )!", new Exception("-1"));
            }
            return n;
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
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), false, 50).SetEnable(enable));
            hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), true, 3).SetEnable(enable));
            hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdlevel=Level"),
            GetList(new StruasetLookupControl()), "Level=Nmlevel", 50).SetAllowRefresh(false).SetEnable(enable).SetEditable(false));
            hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Umeko=Umur Ekonomis"), true, 50).SetEnable(enable));
            hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Nklas=Nilai Klasifikasi"), true, 50).SetEnable(enable));
            hpars.Add(new ParameterRowType(this, false));

            return hpars;
        }
        #endregion Methods 
        #region IDataControlTreeGrid3
        public string ParentAsetkey { get { return ParentKode(Asetkey); } set { } }

        public static bool IsRootCondition(DaftasetControl dc)
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
            return new string[] { "Id", "Asetkey", "Kdaset", "Nmaset", "Umeko", "Nklas", "Status", "Statusicon", "Kdlevel", "Type", "Golkib" };
        }
        public new void SetTreeGridColumns(TreeGridColumnCollection Columns)
        {

            Columns.Add(new TreeGridColumn { Header = ConstantDict.Translate("Kdaset=Kode Barang"), Width = 250, DataIndex = "Kdaset", Align = Ext.Net.TextAlign.Left });
            Columns.Add(new TreeGridColumn { Header = ConstantDict.Translate("Nmaset=Nama Barang"), Width = 400, DataIndex = "Nmaset", Align = Ext.Net.TextAlign.Left });
            Columns.Add(new TreeGridColumn { Header = ConstantDict.Translate("Kdlevel"), Width = 70, DataIndex = "Kdlevel", Align = Ext.Net.TextAlign.Center });
            Columns.Add(new TreeGridColumn { Header = ConstantDict.Translate("Type=Tipe"), Width = 70, DataIndex = "Type", Align = Ext.Net.TextAlign.Center });
            Columns.Add(new TreeGridColumn { Header = ConstantDict.Translate("Umeko=Umur Ekonomis"), Width = 150, DataIndex = "Umeko", Align = Ext.Net.TextAlign.Right });
            Columns.Add(new TreeGridColumn { Header = ConstantDict.Translate("Nklas=Nilai Klasifikasi"), Width = 150, DataIndex = "Nklas", Align = Ext.Net.TextAlign.Right });

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
            List<DaftasetControl> localList = (List<DaftasetControl>)list;
            List<DaftasetControl> roots = localList.FindAll(i => IsRootCondition(i));

            Ext.Net.TreeNode root = new Ext.Net.TreeNode("Root", "Root", GetIcon());
            Ext.Net.TreeNodeCollection nodes = root.Nodes;
            foreach (DaftasetControl ctrl in roots)
            {
                nodes.Add(CreateNodeWithOutChildren((List<DaftasetControl>)list, ctrl, typetree, delim_menu));
            }
            return root;
        }
        public void LoadPages(IList inList, string nodeid, Ext.Net.TreeNodeCollection nodes, int typetree)
        {
            string delim_menu = ">";
            List<DaftasetControl> list = (List<DaftasetControl>)inList;
            if (list != null && list.Count > 0)
            {
                DaftasetControl parent = list.Find(o => o.Asetkey.Equals(nodeid));
                if ((parent != null) && (parent.Type.Equals("H")))
                {
                    List<DaftasetControl> children = GetChildren(list, parent);
                    foreach (DaftasetControl ctrl in children)
                    {
                        nodes.Add(CreateNodeWithOutChildren(list, ctrl, typetree, delim_menu));
                    }
                }
            }
        }
        public static List<DaftasetControl> GetChildren(List<DaftasetControl> domainset, DaftasetControl parent)
        {
            List<DaftasetControl> children = domainset.FindAll(o => o.Kdaset.StartsWith(parent.Kdaset) && (o.Kdlevel == parent.Kdlevel + 1));
            return children;
        }
        protected TreeNodeBase CreateNodeWithOutChildren(List<DaftasetControl> list, DaftasetControl parent, int typetree, string delim_menu)
        {
            TreeNodeBase treeNode;
            List<DaftasetControl> children = GetChildren(list, parent);
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
        //#region Sum All
        //public static void Update(List<DaftasetControl> domainset)
        //{
        //  List<DaftasetControl> roots = domainset.FindAll(i => IsRootCondition(i));
        //  if (roots.Count > 0)
        //  {
        //    foreach (DaftasetControl r in roots)
        //    {
        //      UpdatedRecFields bobotProgress = SumChildren(domainset, r);
        //      r.SetValue(bobotProgress);
        //    }
        //  }
        //}
        //static UpdatedRecFields SumChildren(List<DaftasetControl> domainsetParent, DaftasetControl parent)
        //{
        //  UpdatedRecFields localBobotProgress = new UpdatedRecFields();
        //  List<DaftasetControl> localDomainset = domainsetParent.FindAll(o => o.Asetkey.StartsWith(parent.Asetkey));

        //  List<DaftasetControl> children = GetChildren(localDomainset, parent);
        //  int localNumChildren = 0;
        //  if ((children.Count > 0) && (parent.Type == "H"))
        //  {
        //    localBobotProgress.Nchild = children.Count;
        //    foreach (DaftasetControl o in children)
        //    {
        //      localNumChildren++;
        //      UpdatedRecFields childrenBobotProgress = SumChildren(localDomainset, o);
        //      localBobotProgress.Sum(childrenBobotProgress);
        //    }
        //    parent.SetValue(localBobotProgress);
        //    return localBobotProgress;
        //  }
        //  else
        //  {
        //    localBobotProgress.SetValue(parent);
        //    localBobotProgress.Nchild = 0;
        //    return localBobotProgress;
        //  }
        //}
        //void SetValue(UpdatedRecFields rec)
        //{
        //  Nchild = rec.Nchild;
        //}
        //class UpdatedRecFields
        //{
        //  public int Nchild = 0;

        //  public void Sum(UpdatedRecFields rec)
        //  {
        //    Nchild += rec.Nchild;
        //  }

        //  public void SetValue(DaftasetControl rec)
        //  {
        //    Nchild = rec.Nchild;
        //  }
        //}
        //#endregion

    }
    #endregion
}

