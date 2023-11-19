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
    #region Usadi.Valid49.BO.DaftlokasiControl, Usadi.Valid49.DM
    [Serializable]
    public class DaftlokasiControl : BaseDataControlAsetDM, IDataControlTreeGrid3, IHasJSScript
    {
        #region Properties 
        public long Id { get; set; }
        public string Kdjlok { get; set; }
        public string Uraijlok { get; set; }
        public string Kdlokasi { get; set; }
        public string Nmlokasi { get; set; }
        public int Kdjlokmin { get; set; }
        public string Lokasikey { get; set; }
        #endregion Properties 

        #region Methods 
        public DaftlokasiControl()
        {
            XMLName = ConstantTablesAsetDM.XMLDAFTLOKASI;
        }
        public new IProperties GetProperties()
        {
            ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
            cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
            cViewListProperties.PrimaryKeys = new String[] { "Lokasikey" };
            cViewListProperties.IDKey = "Lokasikey";
            cViewListProperties.IDProperty = "Lokasikey";
            cViewListProperties.ReadOnlyFields = new String[] { };
            cViewListProperties.SortFields = new String[] { "Kdlokasi" };
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
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdlokasi=Kode Lokasi"), typeof(string), 30, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmlokasi=Lokasi"), typeof(string), 40, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdjlok=Jenis"), typeof(string), 10, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Uraijlok=Jenis Lokasi"), typeof(string), 20, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Type=Tipe"), typeof(string), 10, HorizontalAlign.Center));
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
        //public new void SetPrimaryKey()
        //{
        //    Kdjlok = 4;
        //    Type = "D";
        //}
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
            List<DaftlokasiControl> ListData = new List<DaftlokasiControl>();
            foreach (DaftlokasiControl dc in list)
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
            bool unik = true;
            DaftlokasiControl cUnit = new DaftlokasiControl();
            cUnit.Kdlokasi = Kdlokasi;
            IList list = cUnit.View("Kdlokasi");
            unik = (list.Count == 0);
            if (!unik)
            {
                throw new Exception("Gagal menyimpan data : Kode Lokasi harus unik!");
            }
            else
            {
                Lokasikey = Kdlokasi.Replace(".", "");
                base.Insert();
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
                        new DaftlokasiControl
                        {
                            Lokasikey = this.Lokasikey,
                            Kdlokasi = this.Kdlokasi,
                            Nmlokasi = this.Nmlokasi,
                            Kdjlok = this.Kdjlok,
                            Type = this.Type
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
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Kdlokasi=Kode Lokasi"), false, 50).SetEnable(enable));
            hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Nmlokasi=Nama Lokasi"), true, 3).SetEnable(enable));
            hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdlevel=Jenis"),
            GetList(new JlokasiLookupControl()), "Jenis=Uraijlok", 50).SetAllowRefresh(false).SetEnable(enable).SetEditable(false));
            hpars.Add(new ParameterRowType(this, false));

            return hpars;
        }
        #endregion Methods 
        #region IDataControlTreeGrid3
        public string ParentLokasikey { get { return ParentKode(Lokasikey); } set { } }

        public static bool IsRootCondition(DaftlokasiControl dc)
        {
            return (dc.Kdlevel == 1);
        }
        public Icon GetIcon()
        {
            return Icon.Table;
        }
        /*pastikan GetKeys-> ...Urmenu,URL,Kdjlok,Type */
        public new string[] GetKeys()
        {
            return new string[] { "Id", "Lokasikey", "Kdlokasi", "Nmlokasi",  "Statusicon", "Kdlevel", "Type" };
        }
        public new void SetTreeGridColumns(TreeGridColumnCollection Columns)
        {

            Columns.Add(new TreeGridColumn { Header = ConstantDict.Translate("Kdlokasi=Kode Lokasi"), Width = 250, DataIndex = "Kdlokasi", Align = Ext.Net.TextAlign.Left });
            Columns.Add(new TreeGridColumn { Header = ConstantDict.Translate("Nmlokasi=Lokasi"), Width = 400, DataIndex = "Nmlokasi", Align = Ext.Net.TextAlign.Left });
            Columns.Add(new TreeGridColumn { Header = ConstantDict.Translate("Kdlevel"), Width = 70, DataIndex = "Kdlevel", Align = Ext.Net.TextAlign.Center });
            Columns.Add(new TreeGridColumn { Header = ConstantDict.Translate("Type=Tipe"), Width = 70, DataIndex = "Type", Align = Ext.Net.TextAlign.Center });

        }
        /*Create Root Static, not asynchronus, pastikan 4 kolom terakhir GetKeys-> ...Urmenu,URL,Kdjlok,Type */
        /*public Ext.Net.TreeNode CreateRoot(IList list, int typetree, bool withroot)
        {
          string[] Keys = GetKeys();
          ExtTreePanelUtil util = new ExtTreePanelUtil();
          return util.CreateTree(ConstantDict.Translate("Daftar Menu"), list, Keys, new string[] { "Unitkey", "." }, new string[] { "Unitkey", ">" }, typetree, withroot);
        }*/
        public Ext.Net.TreeNode CreateRoot(IList list, int typetree, bool withroot)
        {
            string delim_menu = GlobalAsp.DELIMITER_MENU;
            List<DaftlokasiControl> localList = (List<DaftlokasiControl>)list;
            List<DaftlokasiControl> roots = localList.FindAll(i => IsRootCondition(i));

            Ext.Net.TreeNode root = new Ext.Net.TreeNode("Root", "Root", GetIcon());
            Ext.Net.TreeNodeCollection nodes = root.Nodes;
            foreach (DaftlokasiControl ctrl in roots)
            {
                nodes.Add(CreateNodeWithOutChildren((List<DaftlokasiControl>)list, ctrl, typetree, delim_menu));
            }
            return root;
        }
        public void LoadPages(IList inList, string nodeid, Ext.Net.TreeNodeCollection nodes, int typetree)
        {
            string delim_menu = ">";
            List<DaftlokasiControl> list = (List<DaftlokasiControl>)inList;
            if (list != null && list.Count > 0)
            {
                DaftlokasiControl parent = list.Find(o => o.Lokasikey.Equals(nodeid));
                if ((parent != null) && (parent.Type.Equals("H")))
                {
                    List<DaftlokasiControl> children = GetChildren(list, parent);
                    foreach (DaftlokasiControl ctrl in children)
                    {
                        nodes.Add(CreateNodeWithOutChildren(list, ctrl, typetree, delim_menu));
                    }
                }
            }
        }
        public static List<DaftlokasiControl> GetChildren(List<DaftlokasiControl> domainset, DaftlokasiControl parent)
        {
            List<DaftlokasiControl> children = domainset.FindAll(o => o.Kdlokasi.StartsWith(parent.Kdlokasi) && (o.Kdjlok == parent.Kdjlok + 1));
            return children;
        }
        protected TreeNodeBase CreateNodeWithOutChildren(List<DaftlokasiControl> list, DaftlokasiControl parent, int typetree, string delim_menu)
        {
            TreeNodeBase treeNode;
            List<DaftlokasiControl> children = GetChildren(list, parent);
            if (children != null && children.Count > 0)
            {
                treeNode = (AsyncTreeNode)ExtTreeNode.GetExtTreeNode(parent.Lokasikey,
                  ExtTreePanelUtil.GetUraian(parent.Lokasikey, new string[] { "Lokasikey", delim_menu }),
                  typetree, GetKeys(), parent, true, Icon.Folder);
            }
            else
            {
                treeNode = ExtTreeNode.GetExtTreeNode(parent.Lokasikey,
                  ExtTreePanelUtil.GetUraian(parent.Lokasikey, new string[] { "Lokasikey", delim_menu }),
                  typetree, GetKeys(), parent, false, Icon.Table);
            }
            return treeNode;
        }
        #endregion IDataControlTreeGrid3
        
    }
    #endregion
}

