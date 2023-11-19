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
    #region Usadi.Valid49.BO.DaftgudangControl, Usadi.Valid49.DM
    [Serializable]
    public class DaftgudangControl : BaseDataControlAsetDM, IDataControlUIEntry, IHasJSScript, IExtLoadCsv
    {
        #region Properties 
        public DateTime Datecreate { get; set; }
        public DateTime Dateupdate { get; set; }
        public string Gudangkey { get; set; }
        public long Id { get; set; }
        public string Kdgudang { get; set; }
        public string Keterangan { get; set; }
        public string Nama { get; set; }
        public string Namauser { get; set; }
        public string Nip { get; set; }
        public string Nmgudang { get; set; }
        public string Unitkey { get; set; }
        public string Kdunit { get; set; }
        public string Nmunit { get; set; }
        #endregion Properties 

        #region Methods 
        public DaftgudangControl()
        {
            XMLName = ConstantTablesAsetDM.XMLDAFTGUDANG;
        }
        public new IProperties GetProperties()
        {
            ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
            cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
            cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Kdgudang" };
            cViewListProperties.IDKey = "Id";//IDKey for ID Notes
            cViewListProperties.IDProperty = "Id";//UniqueKey in gridview
            cViewListProperties.ReadOnlyFields = new String[] { "Unitkey" };//Key in GetFilters should put here
            cViewListProperties.SortFields = new String[] { "Kdgudang" };//
            cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
            cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_EDIT_DEL;
            cViewListProperties.PageSize = 30;
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
            }
        }
        public new void SetPrimaryKey()
        {
            Unitkey = Unitkey;
        }
        public new HashTableofParameterRow GetFilters()
        {
            bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
            HashTableofParameterRow hpars = new HashTableofParameterRow();
            hpars.Add(DaftunitLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetAllowEmpty(false));

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
            List<DaftgudangControl> ListData = new List<DaftgudangControl>();
            foreach (DaftgudangControl dc in list)
            {
                ListData.Add(dc);
            }
            //Update(ListData);
            return ListData;
        }

        public override DataControlFieldCollection GetColumns()
        {
            DataControlFieldCollection columns = new DataControlFieldCollection();
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdgudang=Kode"), typeof(string), 20, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmgudang=Uraian"), typeof(string), 40, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nip=Nip"), typeof(string), 25, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nama=Nama Penanggung Jawab"), typeof(string), 40, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket"), typeof(string), 30, HorizontalAlign.Left));
            return columns;
        }
        //Unuk ParameterLookup2, pastikan parameter entry is true
        public override HashTableofParameterRow GetEntries()
        {
            bool enable = true;
            HashTableofParameterRow hpars = new HashTableofParameterRow();
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Kdgudang=Kode Gudang"), false, 30).SetEnable(enable));
            hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Nmgudang=Uraian"), true, 3).SetEnable(enable));
            //hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nip=Penanggung Jawab"), true, 100).SetEnable(enable));
            hpars.Add(PegawaiLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(true).SetAllowEmpty(false));
            //hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nama"), true, 100).SetEnable(enable));
            hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Keterangan"), true, 100).SetEnable(enable));
            hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdlevel=Level"),
           GetList(new StruruangLookupControl()), "Level=Nmlevel", 50).SetAllowRefresh(false).SetEnable(enable).SetEditable(false));
            hpars.Add(new ParameterRowType(this, false));
            return hpars;
        }
 
        #endregion Methods 
    }
    #endregion Daftgudang
}