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
    #region Usadi.Valid49.BO.HistrenovdetControl, Usadi.Valid49.Aset.MAT
    [Serializable]
    public class HistrenovdetControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
    {
        #region Properties
        public long Id { get; set; }
        public string Idbrg { get; set; }
        public string Asetkey { get; set; }
        public decimal Nilai { get; set; }
        public decimal Umeko { get; set; }
        public decimal Nilairenov { get; set; }
        public decimal Umekorenov { get; set; }
        public string Kdunit { get; set; }
        public string Nmunit { get; set; }
        public string Kdunit2 { get; set; }
        public string Nmunit2 { get; set; }
        public string Nmkdunit2 { get; set; }
        public string Kdaset { get; set; }
        public string Nmaset { get; set; }
        public string Kdper { get; set; }
        public string Nmper { get; set; }
        public string Kdtahap { get; set; }
        public string Kdkegunit { get; set; }
        public string Kdtans { get; set; }
        public DateTime Tglvalid { get; set; }
        public string Kddana { get; set; }
        public DateTime Tglbarenov { get; set; }
        public string Nokontrak { get; set; }
        public string Kdkib { get; set; }
        public string Nmkib { get; set; }
        public decimal Prosen { get; set; }
        public string Idtarget { get { return Asetkey + '.' + Nilai.ToString("#,##0"); } }
        public string Unitkey { get; set; }
        public string Unitkey2 { get; set; }
        public string Nobarenov { get; set; }
        public string Mtgkey { get; set; }
        public string Blokid { get; set; }
        public string Kdobjekaset { get; set; }
        public string Nmobjekaset { get; set; }
        public string Asetkeyrenov { get; set; }
        #endregion Properties 

        #region Methods 
        public HistrenovdetControl()
        {
            XMLName = ConstantTablesAsetMAT.XMLHISTRENOVDET;
        }
        public new IProperties GetProperties()
        {
            ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
            cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
            cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Nobarenov", "Mtgkey", "Unitkey2", "Idbrg" };
            cViewListProperties.IDKey = "Id";
            cViewListProperties.IDProperty = "Id";
            cViewListProperties.ReadOnlyFields = new String[] { "Unitkey", "Kdunit", "Nmunit", "Nobarenov", "Kdtahap", "Kdkegunit", "Nokontrak" };
            cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
            cViewListProperties.PageSize = 20;

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
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmkdunit2=SKPD Pengguna"), typeof(string), 55, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdper=Kode Rekening"), typeof(string), 20, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmper=Rekening"), typeof(string), 40, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 25, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 40, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai=Nilai Aset"), typeof(decimal), 25, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Umeko=Masa Manfaat"), typeof(decimal), 20, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilairenov=Penambahan Nilai"), typeof(decimal), 25, HorizontalAlign.Left)
              .SetEditable(enable));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Umekorenov=Penambahan Masa Manfaat"), typeof(decimal), 30, HorizontalAlign.Left)
              .SetEditable(enable));
            return columns;
        }
        public new void SetFilterKey(BaseBO bo)
        {
            if (typeof(IDataControlMenu).IsInstanceOfType(bo))
            {
                Last_by = ((BaseBO)bo).Userid;
            }
            else if (typeof(HistrenovControl).IsInstanceOfType(bo))
            {
                Unitkey = ((HistrenovControl)bo).Unitkey;
                Kdunit = ((HistrenovControl)bo).Kdunit;
                Nmunit = ((HistrenovControl)bo).Nmunit;
                Nobarenov = ((HistrenovControl)bo).Nobarenov;
                Kdtans = ((HistrenovControl)bo).Kdtans;
                Kdtahap = ((HistrenovControl)bo).Kdtahap;
                Kdkegunit = ((HistrenovControl)bo).Kdkegunit;
                Nokontrak = ((HistrenovControl)bo).Nokontrak;
                Tglvalid = ((HistrenovControl)bo).Tglvalid;
                Blokid = ((HistrenovControl)bo).Blokid;
            }
        }
        public new void SetPrimaryKey()
        {
            Unitkey2 = Unitkey;
            Kdunit2 = Kdunit;
            Nmunit2 = Nmunit;
        }
        public new IList View()
        {
            IList list = this.View(BaseDataControl.ALL);
            return list;
        }
        public new IList View(string label)
        {
            IList list = ((BaseDataControl)this).View(label);
            List<HistrenovdetControl> ListData = new List<HistrenovdetControl>();
            foreach (HistrenovdetControl dc in list)
            {
                ListData.Add(dc);
            }
            return ListData;
        }
        public new void Insert()
        {
            string sql = @"
        exec [dbo].[WSP_VALHISTRENOVDET]
        @UNITKEY = N'{0}',
        @NOBARENOV = N'{1}',
        @UNITKEY2 = N'{2}',
        @NOKONTRAK = N'{3}',
        @MTGKEY = N'{4}',
        @KDTAHAP = N'{5}',
        @KDKEGUNIT = N'{6}',
        @IDBRG = N'{7}',
        @ASETKEY = N'{8}',
        @NILAIASET = N'{9}',
        @UMEKOASET = N'{10}',
        @NILAIRENOV = N'{11}'
        ";

            sql = string.Format(sql, Unitkey, Nobarenov, Unitkey2, Nokontrak, Mtgkey, Kdtahap, Kdkegunit, Idbrg, Asetkeyrenov
              , Nilai, Umeko, Nilairenov);
            BaseDataAdapter.ExecuteCmd(this, sql);
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
            hpars.Add(DaftunitSkpenggunaLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(enable).SetEditable(false).SetAllowRefresh(true).SetAllowEmpty(false));
            hpars.Add(DaskrRenovLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetAllowEmpty(false));
            hpars.Add(DaftasetObjekLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(true).SetAllowRefresh(true).SetAllowEmpty(false));

            //hpars.Add(DaftasetKibfilterLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(enable).SetAllowRefresh(true).SetAllowEmpty(false));
            hpars.Add(ViewasetRenovLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(enable).SetAllowRefresh(true).SetAllowEmpty(false));
            hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Nilairenov=Nilai"), true, 35).SetEnable(enable).SetEditable(false)
               .SetAllowEmpty(false));
            return hpars;
        }

        #endregion Methods 
    }
    #endregion Histrenovdet
}

