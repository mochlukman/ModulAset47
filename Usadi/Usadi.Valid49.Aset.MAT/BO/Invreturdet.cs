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
    #region Usadi.Valid49.BO.InvreturdetControl, Usadi.Valid49.Aset.MAT
    [Serializable]
    public class InvreturdetControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
    {
        #region Properties 
        public DateTime Datecreate { get; set; }
        public DateTime Dateupdate { get; set; }
        public long Id { get; set; }
        public string Idbrg { get; set; }
        public string Asetkey { get; set; }
        public string Kdaset { get; set; }
        public string Nmaset { get; set; }
        public string Kdsatuan { get; set; }
        public string Nmsatuan { get; set; }
        public string Merk { get; set; }
        public decimal Nilai { get; set; }
        public string Noretur { get; set; }
        public string Nospb { get; set; }
        public string Nusp { get; set; }
        public string Spesifikasi { get; set; }
        public int Stexpr { get; set; }
        public DateTime Tglexpr { get; set; }
        public string Unitkey { get; set; }
        public string Unitkey2 { get; set; }
        #endregion Properties 

        #region Methods 
        public InvreturdetControl()
        {
            XMLName = ConstantTablesAsetMAT.XMLINVRETURDET;
        }
        public new IProperties GetProperties()
        {
            ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
            cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
            cViewListProperties.PrimaryKeys = new String[] { "Unitkey2", "Noretur", "Idbrg" };
            cViewListProperties.IDKey = "Id";//IDKey for ID Notes
            cViewListProperties.IDProperty = "Id";//UniqueKey in gridview
            cViewListProperties.ReadOnlyFields = new String[] { };//Key in GetFilters should put here
            cViewListProperties.SortFields = new String[] { "Idbrg" };//
            cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
            cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_EDIT_DEL;
            cViewListProperties.AllowMultiDelete = true;
            return cViewListProperties;
        }
        public new void SetFilterKey(BaseBO bo)
        {
            if (typeof(InvspbControl).IsInstanceOfType(bo))
            {
                Noretur = ((InvreturControl)bo).Noretur;
                Unitkey = ((InvreturControl)bo).Unitkey;
                Unitkey2 = ((InvreturControl)bo).Unitkey2;
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
            List<InvreturdetControl> ListData = new List<InvreturdetControl>();
            foreach (InvreturdetControl dc in list)
            {
                ListData.Add(dc);
            }

            return ListData;
        }
        public new HashTableofParameterRow GetFilters()
        {
            bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
            HashTableofParameterRow hpars = new HashTableofParameterRow();
            return hpars;
        }
        public override DataControlFieldCollection GetColumns()
        {
            bool enable = true;

            DataControlFieldCollection columns = new DataControlFieldCollection();
            columns.Add(ExtFields.GetStatusField());
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center).SetVisible(enable));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 20, HorizontalAlign.Left).SetEditable(true));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 25, HorizontalAlign.Left).SetEditable(true));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nusp=Nomor Registrasi"), typeof(string), 20, HorizontalAlign.Left).SetEditable(true));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Spesifikasi"), typeof(string), 20, HorizontalAlign.Left).SetEditable(true));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmsatuan=Satuan"), typeof(string), 10, HorizontalAlign.Center).SetEditable(true));
            //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Merk"), typeof(string), 10, HorizontalAlign.Left).SetEditable(true));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai"), typeof(decimal), 15, HorizontalAlign.Right));
            return columns;
        }
        //Unuk ParameterLookup2, pastikan parameter entry is true
        public override HashTableofParameterRow GetEntries()
        {
            bool enable = true;
            HashTableofParameterRow hpars = new HashTableofParameterRow();
            //hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Id"), false, 30).SetEnable(enable));
            hpars.Add(KibhLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enable).SetEditable(false));
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nusp=Nomor Registrasi"), true, 60).SetEnable(enable).SetAllowRefresh(true));
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Spesifikasi"), true, 100).SetEnable(enable));
            //hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdsatuan=Satuan"), GetList(new SatuanLookupControl()), "Kdsatuan=Nmsatuan", 50).SetAllowRefresh(false).SetEnable(enable).SetAllowEmpty(false));
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Kdsatuan=Kode Satuan"), true, 100).SetVisible(false).SetEnable(enable));
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nmsatuan=Satuan"), true, 60).SetEnable(enable));
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Merk"), true, 100).SetEnable(enable));
            hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Nilai"), true, 30).SetEnable(enable));
            return hpars;
        }
        #endregion Methods 
    }
    #endregion Invreturdet
}