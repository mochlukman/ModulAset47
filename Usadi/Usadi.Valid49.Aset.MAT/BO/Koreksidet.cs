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
    #region Usadi.Valid49.BO.KoreksidetControl, Usadi.Valid49.Aset.MAT
    [Serializable]
    public class KoreksidetControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
    {
        #region Properties
        public long Id { get; set; }
        public string Asetkey { get; set; }
        public string Kdaset { get; set; }
        public string Nmaset { get; set; }
        public decimal Nilai { get; set; }
        public decimal Nilaikoreksi { get; set; }
        public decimal Nilaikoreksiganda { get; set; }
        public decimal Umekokoreksi { get; set; }
        public string Spesifikasi { get; set; }
        public DateTime Tglvalid { get; set; }
        public string Kdkib { get; set; }
        public string Nmkib { get; set; }
        public string Unitkey { get; set; }
        public string Nobakoreksi { get; set; }
        public string Idbrg { get; set; }
        public string Blokid { get; set; }
        public string Kdtans { get; set; }

        #endregion Properties 

        #region Methods 
        public KoreksidetControl()
        {
            XMLName = ConstantTablesAsetMAT.XMLKOREKSIDET;
        }
        public new IProperties GetProperties()
        {
            ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
            cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
            cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Nobakoreksi", "Idbrg" };
            cViewListProperties.IDKey = "Id";
            cViewListProperties.IDProperty = "Id";
            cViewListProperties.ReadOnlyFields = new String[] { };
            cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_LOOKUP;
            cViewListProperties.LookupDC = " Usadi.Valid49.BO.ViewasetKoreksiControl, Usadi.Valid49.Aset.MAT";
            cViewListProperties.LookupLabelQuery = "";
            cViewListProperties.PageSize = 20;
            cViewListProperties.RefreshFilter = true;

            if (Tglvalid != new DateTime() || Blokid == "1")
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
            bool enable = true;

            if (Tglvalid != new DateTime() || Blokid == "1")
            {
                enable = false;
            }

            DataControlFieldCollection columns = new DataControlFieldCollection();
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center).SetVisible(enable));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 25, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 50, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai=Nilai Perolehan"), typeof(decimal), 25, HorizontalAlign.Left));
            if (Kdtans == "228")
            {
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilaikoreksiganda=Nilai Koreksi"), typeof(decimal), 25, HorizontalAlign.Left));
            }
            else
            {
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilaikoreksi=Nilai Koreksi"), typeof(decimal), 25, HorizontalAlign.Left));
            }

            return columns;
        }
        public new void SetFilterKey(BaseBO bo)
        {
            if (typeof(IDataControlMenu).IsInstanceOfType(bo))
            {
                Last_by = ((BaseBO)bo).Userid;
            }
            else if (typeof(KoreksiControl).IsInstanceOfType(bo))
            {
                Unitkey = ((KoreksiControl)bo).Unitkey;
                Nobakoreksi = ((KoreksiControl)bo).Nobakoreksi;
                Tglvalid = ((KoreksiControl)bo).Tglvalid;
                Blokid = ((KoreksiControl)bo).Blokid;
                Kdtans = ((KoreksiControl)bo).Kdtans;
            }
            else if (typeof(ViewasetKoreksiControl).IsInstanceOfType(bo))
            {
                Idbrg = ((ViewasetKoreksiControl)bo).Idbrg;
                Asetkey = ((ViewasetKoreksiControl)bo).Asetkey;
                Nilai = ((ViewasetKoreksiControl)bo).Nilai;
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
            List<KoreksidetControl> ListData = new List<KoreksidetControl>();
            foreach (KoreksidetControl dc in list)
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
        public override HashTableofParameterRow GetEntries()
        {
            bool enable = true;
            HashTableofParameterRow hpars = new HashTableofParameterRow();
            //if (Kdtans != "228")
            //{
            //    hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Nilaikoreksi=Nilai Koreksi"), true, 35).SetEnable(enable).SetEditable(true));
            //}
            hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Nilaikoreksi=Nilai Koreksi"), true, 35).SetEnable(enable).SetEditable(true));
            hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Spesifikasi=Keterangan"), true, 3).SetEnable(false).SetAllowEmpty(true));
            return hpars;
        }

        #endregion Methods 
    }
    #endregion Koreksidet
}

