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
    #region Usadi.Valid49.BO.InvnotaControl, Usadi.Valid49.Aset.MAT
    [Serializable]
    public class InvnotaControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
    {
        #region Properties 
        public string Asetkey { get; set; }
        public string Kdaset { get; set; }
        public string Nmaset { get; set; }
        public DateTime Datecreate { get; set; }
        public DateTime Dateupdate { get; set; }
        public long Id { get; set; }
        public string Kdsatuan { get; set; }
        public string Nmsatuan { get; set; }
        public string Ket { get; set; }
        public string Nonpb { get; set; }
        public int Qty { get; set; }
        public string Unitkey { get; set; }
        public string Unitkey2 { get; set; }
        public string Kdunit { get; set; }
        public string Nmunit { get; set; }
        public string Kdkib { get; set; }
        public string Untuk { get; set; }
        public string Untuk2 { get; set; }
        #endregion Properties 

        #region Methods 


        public InvnotaControl()
        {
            XMLName = ConstantTablesAsetMAT.XMLINVNOTA;
        }
        public new void SetPageKey()
        {
            Kdkib = ConstantTablesAsetMAT.KDKIBH;
        }
        public new IProperties GetProperties()
        {
            ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
            cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
            cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Nonpb" };
            cViewListProperties.IDKey = "Id";//IDKey for ID Notes
            cViewListProperties.IDProperty = "Id";//UniqueKey in gridview
            cViewListProperties.ReadOnlyFields = new String[] { "Unitkey", "Kdunit", "Nmunit" };//Key in GetFilters should put here
            cViewListProperties.SortFields = new String[] { "Nonpb" };//
            cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_EDIT_DEL;
            cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
            cViewListProperties.AllowMultiDelete = true;
            return cViewListProperties;
        }
        public new void SetFilterKey(BaseBO bo)
        {
            if (typeof(IDataControlMenu).IsInstanceOfType(bo))
            {
                Unitkey = (string)GlobalAsp.GetSessionUser().GetValue("Unitkey");
                Kdunit = (string)GlobalAsp.GetSessionUser().GetValue("Kdunit");
                Nmunit = (string)GlobalAsp.GetSessionUser().GetValue("Nmunit");

                Last_by = !string.IsNullOrEmpty(Last_by) ? Last_by : GlobalAsp.GetSessionUser().GetUserID();
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
            List<InvnotaControl> ListData = new List<InvnotaControl>();
            foreach (InvnotaControl dc in list)
            {
                //dc.Valid = (dc.Tglvalid != new DateTime());
                ListData.Add(dc);
            }
            return ListData;
        }
        public new HashTableofParameterRow GetFilters()
        {
            bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
            HashTableofParameterRow hpars = new HashTableofParameterRow();
            hpars.Add(DaftunitLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetAllowEmpty(false));
            return hpars;
        }
        public override DataControlFieldCollection GetColumns()
        {
            bool enable = true;

            DataControlFieldCollection columns = new DataControlFieldCollection();
            //columns.Add(ExtFields.GetStatusField());
            //columns.Add(ExtFields.GetRatingField());
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center).SetVisible(enable));
            //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Id"), typeof(long), 10, HorizontalAlign.Left).SetEditable(true));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nonpb=Nota Penerimaan Barang"), typeof(string), 30, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 30, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 30, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Qty=Kuantity"), typeof(int), 10, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmsatuan=Satuan"), typeof(string), 10, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Untuk=Keperluan"), typeof(string), 30, HorizontalAlign.Left));
            //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket"), typeof(string), 10, HorizontalAlign.Left).SetEditable(true));
            //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Datecreate"), typeof(DateTime), 13, HorizontalAlign.Center));
            //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Dateupdate"), typeof(DateTime), 13, HorizontalAlign.Center));
            return columns;
        }
        //Unuk ParameterLookup2, pastikan parameter entry is true
        public override HashTableofParameterRow GetEntries()
        {
            bool enable = true;
            bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
            HashTableofParameterRow hpars = new HashTableofParameterRow();
            //hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Id"), false, 30).SetEnable(enable));
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nonpb=Nomor NPB"), true, 80).SetEnable(enable));
            //hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Asetkey"), true, 100).SetEnable(enable));
            hpars.Add(DaftasetKibLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enableFilter).SetAllowEmpty(false));
            hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Qty=Kuantity"), true, 30).SetEnable(enable));
            hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdsatuan=Satuan"), GetList(new SatuanLookupControl()), "Kdsatuan=Nmsatuan", 50).SetAllowRefresh(false).SetEnable(enable).SetAllowEmpty(false));
            hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Untuk=Keperluan"), true, 3).SetEnable(enable).SetAllowEmpty(true));
            hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Ket=Keterangan"), true, 3).SetEnable(enable).SetAllowEmpty(true));
            //hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Datecreate"), true).SetEnable(enable));
            //hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Dateupdate"), true).SetEnable(enable));
            //hpars.Add(DmtahunLookupControl.Instance.GetLookupParameterRow(this, true).SetEnable(enable));
            //hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdlevel"),
            //  DmlevelLookupControl.GetListDataSingleton(), "Kdlevel=Nmlevel", 30).SetAllowRefresh(false).SetEnable(enable));
            //hpars.Add(new ParameterRowType(this, true));
            //hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitleEntry("Status"),
            //  DmstatusLookupControl.GetListDataSingleton(), "Idstatus=Nmstatus", 30).SetAllowRefresh(false).SetEnable(enable));
            //hpars.Add(new ParameterRowCek(this, true));
            //hpars.Add(new ParameterRowUploadFile(this, true));
            //hpars.Add(new ParameterRowHelp(this, true));
            //hpars.Add(new ParameterRowForum(this, true));
            return hpars;
        }

        #endregion Methods 
    }
    #endregion Invnota
}

