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
    #region Usadi.Valid49.BO.BapkirdetControl, Usadi.Valid49.Aset.MAT
    [Serializable]
    public class BapkirdetControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
    {
        #region Properties
        public long Id { get; set; }
        public string Unitkey { get; set; }
        public string Ruangkey { get; set; }
        public string Kdruang { get; set; }
        public string Nmruang { get; set; }
        public string Nobapkir { get; set; }
        public string Kdbapkir { get; set; }
        public string Nmbapkir { get; set; }
        public string Asetkey { get; set; }
        public string Kdaset { get; set; }
        public string Nmaset { get; set; }
        public string Noreg { get; set; }
        public string Idbrg { get; set; }
        public string Kdkib { get; set; }
        public string Nmkib { get; set; }
        public string Blokid { get; set; }
        #endregion Properties 

        #region Methods 
        public BapkirdetControl()
        {
            XMLName = ConstantTablesAsetMAT.XMLBAPKIRDET;
        }
        public new IProperties GetProperties()
        {
            ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
            cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
            cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Ruangkey", "Nobapkir", "Kdbapkir", "Asetkey", "Tahun", "Noreg" };
            cViewListProperties.IDKey = "Id";
            cViewListProperties.IDProperty = "Id";
            cViewListProperties.ReadOnlyFields = new String[] { };
            cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_LOOKUP;
            cViewListProperties.LookupDC = " Usadi.Valid49.BO.ViewasetBapkirControl, Usadi.Valid49.Aset.MAT ";
            cViewListProperties.LookupLabelQuery = "";
            cViewListProperties.PageSize = 30;

            if (Blokid == "1")
            {
                cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
            }
            else
            {
                cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_DEL;
                cViewListProperties.AllowMultiDelete = true;
            }

            return cViewListProperties;
        }
        public override DataControlFieldCollection GetColumns()
        {
            DataControlFieldCollection columns = new DataControlFieldCollection();

            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 25, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 50, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun=Tahun Perolehan"), typeof(string), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreg=No Register"), typeof(string), 15, HorizontalAlign.Center));
            return columns;
        }
        public new void SetFilterKey(BaseBO bo)
        {
            if (typeof(IDataControlMenu).IsInstanceOfType(bo))
            {
                Last_by = ((BaseBO)bo).Userid;
            }
            else if (typeof(BapkirControl).IsInstanceOfType(bo))
            {
                Unitkey = ((BapkirControl)bo).Unitkey;
                Ruangkey = ((BapkirControl)bo).Ruangkey;
                Nobapkir = ((BapkirControl)bo).Nobapkir;
                Kdbapkir = ((BapkirControl)bo).Kdbapkir;
                Blokid = ((BapkirControl)bo).Blokid;
            }
            else if (typeof(ViewasetBapkirControl).IsInstanceOfType(bo))
            {
                Asetkey = ((ViewasetBapkirControl)bo).Asetkey;
                Tahun = ((ViewasetBapkirControl)bo).Tahun;
                Noreg = ((ViewasetBapkirControl)bo).Noreg;
                Idbrg = ((ViewasetBapkirControl)bo).Idbrg;
            }
        }
        public new IList View()
        {
            IList list = this.View(BaseDataControl.ALL);
            return list;
        }
        public new IList View(string label)
        {
            IList list = ((BaseDataControl)this).View(label);
            List<BapkirdetControl> ListData = new List<BapkirdetControl>();
            foreach (BapkirdetControl dc in list)
            {
                ListData.Add(dc);
            }
            return ListData;
        }
        public new int Delete()
        {
            Status = -1;
            int n = ((BaseDataControlUI)this).Delete(BaseDataControl.DEFAULT);
            return n;
        }
        #endregion Methods 
    }
    #endregion Hapusskdet
}

