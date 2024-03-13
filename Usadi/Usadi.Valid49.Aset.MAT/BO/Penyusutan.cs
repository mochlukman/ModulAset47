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
    #region Usadi.Valid49.BO.PenyusutanControl, Usadi.Valid49.Aset.MAT
    [Serializable]
    public class PenyusutanControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
    {
        #region Properties
        public long Id { get; set; }
        public string Noreg { get; set; }
        public DateTime Tglhitung { get; set; }
        public Decimal Umeko { get; set; }
        public int Periodeke { get; set; }
        public int Sisamasapakai { get; set; }
        public DateTime Tglsusut { get; set; }
        public decimal Nilaiaset { get; set; }
        public decimal Nilaisusut { get; set; }
        public decimal Nilaiakumsusut { get; set; }
        public decimal Nilaibuku { get; set; }
        public DateTime Tglvalid { get; set; }
        public string Ket_bulan { get; set; }
        public string Nmtahun { get; set; }
        public string Kdunit { get; set; }
        public string Nmunit { get; set; }
        public string Kdaset { get; set; }
        public string Nmaset { get; set; }
        public string Kdkib { get; set; }
        public string Nmkib { get; set; }
        public int Jmldata { get; set; }
        public string Kd_bulan { get; set; }
        public string Kdtahun { get; set; }
        public string Unitkey { get; set; }
        public string Asetkey { get; set; }
        public string Idbrg { get; set; }
        public string Kuncisusut { get; set; }
    public string Blokid
        {
            get
            {
                WebuserControl cWebuserGetid = new WebuserControl();
                cWebuserGetid.Userid = GlobalAsp.GetSessionUser().GetUserID();
                cWebuserGetid.Load("PK");

                return cWebuserGetid.Blokid;
            }
        }
        public ImageCommand[] Cmds
        {
            get
            {
                ImageCommand cmd1 = new ImageCommand()
                {
                    CommandName = "ViewTransaksi",
                    Icon = Icon.PageCopy
                };
                cmd1.ToolTip.Text = "Klik Untuk Menampilkan Rincian Perhitungan Penyusutan";
                return new ImageCommand[] { cmd1 };
            }
        }

        public string ViewTransaksi
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
                return "" + Nmaset + "; Tahun " + Tahun + "; Register " + Noreg + "; Nilai Aset; " + Nilaiaset.ToString("#,##0") + ":" + url;
            }
        }
        #endregion Properties 

        #region Methods 
        public PenyusutanControl()
        {
            XMLName = ConstantTablesAsetMAT.XMLPENYUSUTAN;
        }
        public new IProperties GetProperties()
        {
            ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
            cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
            cViewListProperties.PrimaryKeys = new String[] { "Kd_bulan", "Kdtahun", "Unitkey", "Asetkey", "Idbrg" };
            cViewListProperties.IDKey = "Id";
            cViewListProperties.IDProperty = "Id";
            cViewListProperties.ReadOnlyFields = new String[] { "Unitkey", "Kdunit", "Nmunit", "Asetkey", "Kdaset", "Nmaset", "Kdtahun", "Kd_bulan" };
            cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
            cViewListProperties.PageSize = 30;
            cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
            //cViewListProperties.AllowMultiDelete = true;

            //if (Blokid == "1")
            //{
            //  cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
            //}
            //else
            //{
            //  cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD;
            //  cViewListProperties.AllowMultiDelete = true;
            //}

            return cViewListProperties;
        }
        public override DataControlFieldCollection GetColumns()
        {
            DataControlFieldCollection columns = new DataControlFieldCollection();
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Stricon"), typeof(CommandColumn), Cmds, 5, HorizontalAlign.Center));

            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 20, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 50, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun=Tahun Perolehan"), typeof(string), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreg=No Register"), typeof(string), 15, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Umeko=Masa Pakai"), typeof(decimal), 15, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Sisamasapakai=Sisa Masa Pakai"), typeof(int), 18, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilaiaset=Nilai Aset"), typeof(decimal), 25, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilaisusut=Nilai Penyusutan"), typeof(decimal), 25, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilaiakumsusut=Nilai Akumulasi"), typeof(decimal), 25, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilaibuku=Nilai Buku"), typeof(decimal), 25, HorizontalAlign.Left));

            return columns;
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
        public new HashTableofParameterRow GetFilters()
        {
            bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
            HashTableofParameterRow hpars = new HashTableofParameterRow();
            hpars.Add(DaftasetSusutLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enableFilter).SetAllowEmpty(false));
            hpars.Add(DaftunitLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetAllowEmpty(false));
            hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdtahun=Tahun"),
            GetList(new TahunSusutLookupControl()), "Kdtahun=Nmtahun", 41).SetAllowRefresh(true).SetEnable(enableFilter).SetAllowEmpty(false));
            hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kd_bulan=Bulan"),
            GetList(new BulanLookupControl()), "Kd_bulan=Ket_bulan", 41).SetAllowRefresh(true).SetEnable(enableFilter).SetAllowEmpty(false));

            return hpars;
        }
        public override HashTableofParameterRow GetEntries()
        {
            //bool enable = true;
            HashTableofParameterRow hpars = new HashTableofParameterRow();

            //hpars.Add(DaftasetSusutLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(false));
            //hpars.Add(DaftunitLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(false));
            //hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Kdtahun=Tahun"), true, 20).SetEnable(false));
            //hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Kd_bulan=Bulan"), true, 20).SetEnable(false));

            return hpars;
        }
        public new IList View()
        {
            IList list = this.View("All");
            return list;
        }
        public new IList View(string label)
        {
            IList list = ((BaseDataControl)this).View(label);
            List<PenyusutanControl> ListData = new List<PenyusutanControl>();
            foreach (PenyusutanControl dc in list)
            {
                dc.Valid = (dc.Tglvalid != new DateTime());
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
    #endregion Penyusutan

    #region Usadi.Valid49.BO.PenyusutanValidControl, Usadi.Valid49.Aset.MAT
    [Serializable]
    public class PenyusutanValidControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
    {
        #region Properties
        public long Id { get; set; }
        public string Noreg { get; set; }
        public DateTime Tglhitung { get; set; }
        public Decimal Umeko { get; set; }
        public int Periodeke { get; set; }
        public int Sisamasapakai { get; set; }
        public DateTime Tglsusut { get; set; }
        public decimal Nilaiaset { get; set; }
        public decimal Nilaisusut { get; set; }
        public decimal Nilaiakumsusut { get; set; }
        public decimal Nilaibuku { get; set; }
        public DateTime Tglvalid { get; set; }
        public string Ket_bulan { get; set; }
        public string Nmtahun { get; set; }
        public string Kdunit { get; set; }
        public string Nmunit { get; set; }
        public string Kdaset { get; set; }
        public string Nmaset { get; set; }
        public string Kdkib { get; set; }
        public string Nmkib { get; set; }
        public int Jmldata { get; set; }
        public string Kd_bulan { get; set; }
        public string Kdtahun { get; set; }
        public string Unitkey { get; set; }
        public string Asetkey { get; set; }
        public string Idbrg { get; set; }
        public string Blokid
        {
            get
            {
                WebuserControl cWebuserGetid = new WebuserControl();
                cWebuserGetid.Userid = GlobalAsp.GetSessionUser().GetUserID();
                cWebuserGetid.Load("PK");

                return cWebuserGetid.Blokid;
            }
        }
        public ImageCommand[] Cmds
        {
            get
            {
                ImageCommand cmd1 = new ImageCommand()
                {
                    CommandName = "ViewTransaksi",
                    Icon = Icon.PageCopy
                };
                cmd1.ToolTip.Text = "Klik Untuk Menampilkan Rincian Perhitungan Penyusutan";
                return new ImageCommand[] { cmd1 };
            }
        }

        public string ViewTransaksi
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
                return "" + Nmaset + "; Tahun " + Tahun + "; Register " + Noreg + "; Nilai Aset; " + Nilaiaset.ToString("#,##0") + ":" + url;
            }
        }
        #endregion Properties 

        #region Methods 
        public PenyusutanValidControl()
        {
            XMLName = ConstantTablesAsetMAT.XMLPENYUSUTAN;
        }
        public new IProperties GetProperties()
        {
            ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
            cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
            cViewListProperties.PrimaryKeys = new String[] { "Kd_bulan", "Kdtahun", "Unitkey", "Asetkey", "Idbrg" };
            cViewListProperties.IDKey = "Id";
            cViewListProperties.IDProperty = "Id";
            cViewListProperties.ReadOnlyFields = new String[] { "Unitkey", "Kdunit", "Nmunit", "Asetkey", "Kdaset", "Nmaset", "Kdtahun", "Kd_bulan" };
            cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
            cViewListProperties.PageSize = 30;
            cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
            //cViewListProperties.AllowMultiDelete = true;

            //if (Blokid == "1")
            //{
            //  cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
            //}
            //else
            //{
            //  cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD;
            //  cViewListProperties.AllowMultiDelete = true;
            //}

            return cViewListProperties;
        }
        public override DataControlFieldCollection GetColumns()
        {
            DataControlFieldCollection columns = new DataControlFieldCollection();
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Stricon"), typeof(CommandColumn), Cmds, 5, HorizontalAlign.Center));

            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 20, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 50, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun=Tahun Perolehan"), typeof(string), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreg=No Register"), typeof(string), 15, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Umeko=Masa Pakai"), typeof(decimal), 15, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Sisamasapakai=Sisa Masa Pakai"), typeof(int), 18, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilaiaset=Nilai Aset"), typeof(decimal), 25, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilaisusut=Nilai Penyusutan"), typeof(decimal), 25, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilaiakumsusut=Nilai Akumulasi"), typeof(decimal), 25, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilaibuku=Nilai Buku"), typeof(decimal), 25, HorizontalAlign.Left));

            return columns;
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
        public new HashTableofParameterRow GetFilters()
        {
            bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
            HashTableofParameterRow hpars = new HashTableofParameterRow();
            hpars.Add(DaftasetSusutLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enableFilter).SetAllowEmpty(false));
            hpars.Add(DaftunitLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetAllowEmpty(false));
            hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdtahun=Tahun"),
            GetList(new TahunSusutLookupControl()), "Kdtahun=Nmtahun", 41).SetAllowRefresh(true).SetEnable(enableFilter).SetAllowEmpty(false));
            hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kd_bulan=Bulan"),
            GetList(new BulanLookupControl()), "Kd_bulan=Ket_bulan", 41).SetAllowRefresh(true).SetEnable(enableFilter).SetAllowEmpty(false));

            return hpars;
        }
        public override HashTableofParameterRow GetEntries()
        {
            //bool enable = true;
            HashTableofParameterRow hpars = new HashTableofParameterRow();

            //hpars.Add(DaftasetSusutLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(false));
            //hpars.Add(DaftunitLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(false));
            //hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Kdtahun=Tahun"), true, 20).SetEnable(false));
            //hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Kd_bulan=Bulan"), true, 20).SetEnable(false));

            return hpars;
        }
        public new IList View()
        {
            IList list = this.View("All");
            return list;
        }
        public new IList View(string label)
        {
            IList list = ((BaseDataControl)this).View(label);
            List<PenyusutanControl> ListData = new List<PenyusutanControl>();
            foreach (PenyusutanControl dc in list)
            {
                dc.Valid = (dc.Tglvalid != new DateTime());
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
    #endregion PenyusutanValid


    #region PenyusutanHitung
    [Serializable]
    public class PenyusutanHitungControl : PenyusutanControl, IDataControlUIEntry
    {
        public new IProperties GetProperties()
        {
            ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
            cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
            cViewListProperties.PrimaryKeys = new String[] { "Kd_bulan", "Kdtahun", "Unitkey" };
            cViewListProperties.IDKey = "Kd_bulan";
            cViewListProperties.IDProperty = "Kd_bulan";
            cViewListProperties.ReadOnlyFields = new String[] { "Unitkey", "Kdtahun" };
            cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
            //cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_EDIT_DEL;
            cViewListProperties.PageSize = 15;
            cViewListProperties.AllowMultiDelete = true;
            cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_EDIT_DEL;

            //WebsetControl cWebset = new WebsetControl();
            //cWebset.Kdset = "kuncisusut";
            //cWebset.Load("PK");
            //Kuncisusut = cWebset.Valset.ToUpper();

            //if (Kuncisusut == "Y")
            //{
            //  cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;

            //}
            //else
            //{
            //  cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_EDIT_DEL;
            //}
      

      return cViewListProperties;
        }
        public new IList View()
        {
            IList list = this.View("Hitung");
            return list;
        }
        public new void SetPrimaryKey()
        {
            Kd_bulan = Kd_bulan;
        }
        public new HashTableofParameterRow GetFilters()
        {
            bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
            HashTableofParameterRow hpars = new HashTableofParameterRow();

            hpars.Add(DaftunitLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enableFilter));
            hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdtahun=Tahun"),
            GetList(new TahunSusutLookupControl()), "Kdtahun=Nmtahun", 41).SetAllowRefresh(true).SetEnable(enableFilter));

            return hpars;
        }
        public override DataControlFieldCollection GetColumns()
        {
            DataControlFieldCollection columns = new DataControlFieldCollection();
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center));

            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kd_bulan=Kode Bulan"), typeof(int), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket_bulan=Bulan"), typeof(string), 30, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglhitung=Tanggal Hitung"), typeof(DateTime), 30, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglvalid=Tanggal Pengesahan"), typeof(DateTime), 30, HorizontalAlign.Center));

            return columns;
        }
        public override HashTableofParameterRow GetEntries()
        {
            bool enableValid = false;
            bool enable = !Valid;

            PenyusutanHitungControl cPenyusutanCekvalid = new PenyusutanHitungControl();
            cPenyusutanCekvalid.Unitkey = Unitkey;
            cPenyusutanCekvalid.Kdtahun = Kdtahun;
            cPenyusutanCekvalid.Kd_bulan = Kd_bulan;
            cPenyusutanCekvalid.Load("Cekvalid");

            if (cPenyusutanCekvalid.Jmldata != 0)
            {
                enableValid = true;
            }

            HashTableofParameterRow hpars = new HashTableofParameterRow();
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Kdtahun=Tahun"), false, 50).SetEnable(false));
            hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kd_bulan=Bulan"),
              GetList(new BulanLookupControl()), "Kd_bulan=Ket_bulan", 50).SetEnable(enable).SetEditable(false).SetAllowEmpty(false));
            //hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Kdtahun=Tahun"), false, 50).SetEnable(false));
            //hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Ket_bulan=Bulan"), false, 50).SetEnable(false));
            hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglhitung=Tanggal Hitung"), true).SetEnable(false));
            hpars.Add(new ParameterRowCek(this, true).SetEnable(enableValid).SetEditable(enable));
            return hpars;
        }
        private bool IsValid()
        {
            bool valid = true;
            return valid;
        }
        public new void Insert()
        {
            if (Kd_bulan == "" || Kd_bulan == null)
            {
                throw new Exception("Gagal menyimpan data : hitung penyusutan harus memilih bulan");
            }

            string sql = @"
        exec [dbo].[WSP_PENYUSUTAN]
        @Unitkey = N'{0}',
        @Kdtahun = N'{1}',
        @Kd_bulan = N'{2}'
        ";

            sql = string.Format(sql, Unitkey, Kdtahun, Kd_bulan);
            BaseDataAdapter.ExecuteCmd(this, sql);
        }
        public new int Update()
        {
            Tglvalid = Tglhitung;

            int n = 0;
            if (IsValid())
            {
                if (Valid)
                {
                    base.Update("Sah");
                }
            }
            return n;
        }
        public new int Delete()
        {
            PemdaControl cPemda = new PemdaControl();
            cPemda.Configid = "cur_thang";
            cPemda.Load("PK");
            WebsetControl cWebset = new WebsetControl();
            cWebset.Kdset = "kuncisusut";
            cWebset.Load("PK");
            Kuncisusut = cWebset.Valset.ToUpper();

            if (Kuncisusut == "Y" && Kdtahun.Trim() != cPemda.Configval.Trim())
            {
              throw new Exception("Gagal menghapus : Hitung penyusutan sudah di kunci !!!");
            }

            int n = 0;
            if (Valid)
            {
                if (Kuncisusut == "Y" && Kdtahun.Trim() != cPemda.Configval.Trim())
                {
                  throw new Exception("Gagal menghapus : Hitung penyusutan sudah di kunci !!!");
                }
                else
                {
                  return ((BaseDataControlUI)this).Update("Draft");
                }
                
            }
            else
            {
                string sql = @"
        exec [dbo].[WSP_DEL_PENYUSUTAN]
        @UNITKEY = N'{0}',
        @BULAN = N'{1}',
        @KDTAHUN = N'{2}'
        ";

                sql = string.Format(sql, Unitkey, Kd_bulan, Kdtahun);
                BaseDataAdapter.ExecuteCmd(this, sql);
            }
            return n;
        }
    }
    #endregion PenyusutanValid
}

