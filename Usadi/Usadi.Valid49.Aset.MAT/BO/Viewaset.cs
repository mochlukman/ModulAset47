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
    #region Usadi.Valid49.BO.ViewasetControl, Usadi.Valid49.Aset.MAT
    [Serializable]
    public class ViewasetControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
    {
        #region Properties
        public string Idbrg { get; set; }
        public string Unitkey { get; set; }
        public string Asetkey { get; set; }
        public string Kdaset { get; set; }
        public string Nmaset { get; set; }
        public string Noreg { get; set; }
        public string Dokperolehan { get; set; }
        public DateTime Tglperolehan { get; set; }
        public decimal Nilai { get; set; }
        public decimal Umeko { get; set; }
        public string Kdpemilik { get; set; }
        public string Nmpemilik { get; set; }
        public string Kdkon { get; set; }
        public string Nmkon { get; set; }
        public string Asalusul { get; set; }
        public string Pengguna { get; set; }
        public string Kdsatuan { get; set; }
        public string Nmsatuan { get; set; }
        public string Spesifikasi { get; set; }
        public string Nosertifikat { get; set; }
        public string Ukuran { get; set; }
        public string Bahan { get; set; }
        public string Alamat { get; set; }
        public new string Ket { get; set; }
        public string Judul { get; set; }
        public string Kdklas { get; set; }
        public string Uraiklas { get; set; }
        public string Kdstatusaset { get; set; }
        public string Kdkib { get; set; }
        public string Nmkib { get; set; }
        public string Kdtans { get; set; }
        public string Nmtrans { get; set; }
        public string Kdbapkir { get; set; }
        public string Nopenilaian { get; set; }
        public string Nopindahtangan { get; set; }
        public string Noskhapus { get; set; }
        public string Ruangkey { get; set; }
        public string Kdobjekaset { get; set; }
        public string Nmobjekaset { get; set; }
        public string Idkey { get { return Idbrg + Asetkey + Unitkey; } }

        #endregion Properties 

        #region Methods 
        public new IProperties GetProperties()
        {
            ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
            cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
            cViewListProperties.IDKey = "Idkey";
            cViewListProperties.IDProperty = "Idkey";
            cViewListProperties.SortFields = new String[] { };
            cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
            cViewListProperties.PageSize = 30;
            return cViewListProperties;
        }
        public override DataControlFieldCollection GetColumns()
        {
            DataControlFieldCollection columns = new DataControlFieldCollection();
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 20, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 35, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun"), typeof(int), 15, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreg=No Register"), typeof(string), 15, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai=Nilai Perolehan"), typeof(decimal), 25, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Umeko=Masa Pakai"), typeof(decimal), 25, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmpemilik=Pemilik"), typeof(string), 30, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmkon=Kondisi"), typeof(string), 15, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmsatuan=Satuan"), typeof(string), 15, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Judul"), typeof(string), 25, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Asalusul=Asal Usul"), typeof(string), 25, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Pengguna"), typeof(string), 25, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Spesifikasi"), typeof(string), 30, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nosertifikat"), typeof(string), 35, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ukuran"), typeof(string), 30, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Bahan"), typeof(string), 15, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Alamat"), typeof(string), 50, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 100, HorizontalAlign.Left));

            return columns;
        }
        #endregion Methods 
    }
    #endregion Viewaset

    #region ViewasetSkpengguna
    [Serializable]
    public class ViewasetSkpenggunaControl : ViewasetControl, IDataControlUIEntry, IHasJSScript
    {
        #region Methods 
        public new void SetFilterKey(BaseBO bo)
        {
            if (bo.GetProperty("Unitkey") != null)
            {
                Unitkey = bo.GetValue("Unitkey").ToString();
            }
        }
        public new IList View()
        {
            string sql = @"
        exec [dbo].[WSPV_SKPENGGUNADETBRG]
		    @UNITKEY = N'{0}'
      ";

            sql = string.Format(sql, Unitkey);
            string[] fields = new string[] { "Idbrg", "Unitkey", "Asetkey", "Kdaset", "Nmaset", "Tahun", "Noreg"
        , "Dokperolehan", "Tglperolehan", "Nilai", "Umeko", "Kdpemilik", "Nmpemilik", "Kdkon", "Nmkon", "Asalusul"
        , "Pengguna", "Kdsatuan", "Nmsatuan", "Spesifikasi", "Ukuran", "Bahan", "Nosertifikat", "Alamat", "Ket"
        , "Kdkib", "Nmkib" };
            List<IDataControl> list = BaseDataAdapter.GetListDC(this, sql, fields);
            List<ViewasetSkpenggunaControl> ListData = new List<ViewasetSkpenggunaControl>();

            foreach (ViewasetSkpenggunaControl dc in list)
            {
                ListData.Add(dc);
            }
            return ListData;
        }
        public override DataControlFieldCollection GetColumns()
        {
            DataControlFieldCollection columns = new DataControlFieldCollection();
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 18, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 45, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Dokperolehan=Nomor BAST"), typeof(string), 35, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglperolehan=Tanggal BAST"), typeof(DateTime), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun"), typeof(int), 15, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreg=No Register"), typeof(string), 15, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai=Nilai Perolehan"), typeof(decimal), 25, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Umeko=Masa Pakai"), typeof(decimal), 25, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmpemilik=Pemilik"), typeof(string), 30, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmkon=Kondisi"), typeof(string), 15, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmsatuan=Satuan"), typeof(string), 15, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Asalusul=Asal Usul"), typeof(string), 25, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Pengguna"), typeof(string), 25, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Spesifikasi"), typeof(string), 30, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nosertifikat"), typeof(string), 35, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ukuran"), typeof(string), 30, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Bahan"), typeof(string), 15, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Alamat"), typeof(string), 50, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 100, HorizontalAlign.Left));

            return columns;
        }
        #endregion Methods 
    }
    #endregion ViewasetSkpengguna

    #region ViewasetMutasi
    [Serializable]
    public class ViewasetMutasiControl : ViewasetControl, IDataControlUIEntry, IHasJSScript
    {
        #region Methods 
        public new void SetFilterKey(BaseBO bo)
        {
            if (bo.GetProperty("Unitkey") != null)
            {
                Unitkey = bo.GetValue("Unitkey").ToString();
            }
        }
        public new HashTableofParameterRow GetFilters()
        {
            HashTableofParameterRow hpars = new HashTableofParameterRow();
            //hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdkib=Jenis KIB"),
            //GetList(new JnskibTransLookupControl()), "Kdkib=Nmkib", 48).SetAllowRefresh(true).SetEnable(true).SetAllowEmpty(false));
            //hpars.Add(DaftasetKibLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(true).SetAllowEmpty(false));
            hpars.Add(DaftasetObjekLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(true).SetAllowRefresh(true).SetAllowEmpty(false));
            return hpars;
        }
        public new IList View()
        {
            string sql = @"
        exec [dbo].[WSPV_KIBMUTASIDET]
		    @UNITKEY = N'{0}',
		    @KDASET = N'{1}'
      ";

            sql = string.Format(sql, Unitkey, Kdobjekaset);
            string[] fields = new string[] { "Idbrg", "Unitkey", "Asetkey", "Kdaset", "Nmaset", "Tahun", "Noreg"
        , "Nilai", "Umeko", "Kdpemilik", "Nmpemilik", "Kdkon", "Nmkon", "Asalusul"
        , "Pengguna", "Kdsatuan", "Nmsatuan","Spesifikasi", "Ukuran", "Bahan", "Nosertifikat", "Alamat", "Ket"
        , "Kdklas", "Uraiklas", "Kdstatusaset", "Kdkib", "Nmkib" };
            List<IDataControl> list = BaseDataAdapter.GetListDC(this, sql, fields);
            List<ViewasetMutasiControl> ListData = new List<ViewasetMutasiControl>();

            foreach (ViewasetMutasiControl dc in list)
            {
                ListData.Add(dc);
            }
            return ListData;
        }
        #endregion Methods 
    }
    #endregion ViewasetMutasi

    #region ViewasetBapkir
    [Serializable]
    public class ViewasetBapkirControl : ViewasetControl, IDataControlUIEntry, IHasJSScript
    {
        #region Methods 
        public new void SetFilterKey(BaseBO bo)
        {
            if (bo.GetProperty("Unitkey") != null)
            {
                Unitkey = bo.GetValue("Unitkey").ToString();
                Kdbapkir = bo.GetValue("Kdbapkir").ToString();
                Ruangkey = bo.GetValue("Ruangkey").ToString();
            }
        }
        public new HashTableofParameterRow GetFilters()
        {
            bool enable = true;

            if (Kdbapkir != "01")
            {
                enable = false;
            }

            HashTableofParameterRow hpars = new HashTableofParameterRow();
            //hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdkib=Jenis KIB"),
            //GetList(new JnskibKirLookupControl()), "Kdkib=Nmkib", 48).SetAllowRefresh(true).SetEnable(true).SetAllowEmpty(false).SetVisible(enable));
            //hpars.Add(DaftasetKibLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(true).SetAllowEmpty(false).SetVisible(enable));
            hpars.Add(DaftasetKirLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(true).SetAllowRefresh(true).SetAllowEmpty(false).SetVisible(enable));
            return hpars;
        }
        public new IList View()
        {
            string sql = @"
        exec [dbo].[WSPV_BAPKIRDET]
		    @UNITKEY = N'{0}',
		    @KDBAPKIR = N'{1}',
		    @KDASET = N'{2}',
        @RUANGKEY = N'{3}'
      ";

            sql = string.Format(sql, Unitkey, Kdbapkir, Kdobjekaset, Ruangkey);
            string[] fields = new string[] { "Idbrg", "Unitkey", "Asetkey", "Kdaset", "Nmaset", "Tahun", "Noreg"
        , "Nilai", "Umeko", "Kdpemilik", "Nmpemilik", "Kdkon", "Nmkon", "Asalusul"
        , "Pengguna", "Kdsatuan", "Nmsatuan","Spesifikasi", "Ukuran", "Bahan", "Nosertifikat", "Alamat", "Ket"
        , "Kdklas", "Uraiklas", "Kdstatusaset", "Kdkib", "Nmkib" };
            List<IDataControl> list = BaseDataAdapter.GetListDC(this, sql, fields);
            List<ViewasetBapkirControl> ListData = new List<ViewasetBapkirControl>();

            foreach (ViewasetBapkirControl dc in list)
            {
                ListData.Add(dc);
            }
            return ListData;
        }
        #endregion Methods 
    }
#endregion ViewasetBapkir

    #region ViewasetPenilaian
    [Serializable]
    public class ViewasetPenilaianControl : ViewasetControl, IDataControlUIEntry, IHasJSScript
    {
        #region Methods 
        public new void SetFilterKey(BaseBO bo)
        {
            if (bo.GetProperty("Unitkey") != null)
            {
                Unitkey = bo.GetValue("Unitkey").ToString();
                Kdtans = bo.GetValue("Kdtans").ToString();
            }
        }
        public new HashTableofParameterRow GetFilters()
        {
            bool enable = true;

            if (Kdtans == "201" || Kdtans == "204")
            {
                enable = false;
            }

            HashTableofParameterRow hpars = new HashTableofParameterRow();
            hpars.Add(DaftasetObjekLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(true).SetAllowRefresh(true).SetAllowEmpty(false).SetVisible(enable));
            return hpars;
        }
        public new IList View()
        {
            string sql = @"
        exec [dbo].[WSPV_PENILAIANDET]
		    @UNITKEY = N'{0}',
		    @KDTANS = N'{1}',
		    @KDASET = N'{2}'
      ";

            sql = string.Format(sql, Unitkey, Kdtans, Kdobjekaset);
            string[] fields = new string[] { "Idbrg", "Unitkey", "Asetkey", "Kdaset", "Nmaset", "Tahun", "Noreg"
        , "Nilai", "Umeko", "Kdpemilik", "Nmpemilik", "Kdkon", "Nmkon", "Asalusul"
        , "Pengguna", "Kdsatuan", "Nmsatuan","Spesifikasi", "Ukuran", "Bahan", "Nosertifikat", "Alamat", "Ket"
        , "Kdklas", "Uraiklas", "Kdstatusaset", "Kdkib", "Nmkib" };
            List<IDataControl> list = BaseDataAdapter.GetListDC(this, sql, fields);
            List<ViewasetPenilaianControl> ListData = new List<ViewasetPenilaianControl>();

            foreach (ViewasetPenilaianControl dc in list)
            {
                ListData.Add(dc);
            }
            return ListData;
        }
        #endregion Methods 
    }
    #endregion ViewasetPenilaian

    #region ViewasetPindahtangan
    [Serializable]
    public class ViewasetPindahtanganControl : ViewasetControl, IDataControlUIEntry, IHasJSScript
    {
        #region Methods 
        public new void SetFilterKey(BaseBO bo)
        {
            if (bo.GetProperty("Unitkey") != null)
            {
                Unitkey = bo.GetValue("Unitkey").ToString();
                Kdtans = bo.GetValue("Kdtans").ToString();
            }
        }
        public new HashTableofParameterRow GetFilters()
        {
            bool enable = true;
            if (Kdtans == "201" || Kdtans == "204")
            {
                enable = false;
            }
            HashTableofParameterRow hpars = new HashTableofParameterRow();
            //hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdkib=Jenis KIB"),
            //GetList(new JnskibTransLookupControl()), "Kdkib=Nmkib", 48).SetAllowRefresh(true).SetEnable(true).SetAllowEmpty(false).SetVisible(enable));
            //hpars.Add(DaftasetKibLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(true).SetAllowEmpty(false).SetVisible(enable));
            hpars.Add(DaftasetObjekLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(true).SetAllowRefresh(true).SetAllowEmpty(false).SetVisible(enable));
            return hpars;
        }
        public new IList View()
        {
            string sql = @"
        exec [dbo].[WSPV_PINDAHTANGANDET]
		    @UNITKEY = N'{0}',
		    @KDTANS = N'{1}',
		    @KDASET = N'{2}'
      ";

            sql = string.Format(sql, Unitkey, Kdtans, Kdobjekaset);
            string[] fields = new string[] { "Idbrg", "Unitkey", "Asetkey", "Kdaset", "Nmaset", "Tahun", "Noreg"
        , "Nilai", "Umeko", "Kdpemilik", "Nmpemilik", "Kdkon", "Nmkon", "Asalusul"
        , "Pengguna", "Kdsatuan", "Nmsatuan","Spesifikasi", "Ukuran", "Bahan", "Nosertifikat", "Alamat", "Ket"
        , "Kdklas", "Uraiklas", "Kdstatusaset", "Kdkib", "Nmkib", "Nopenilaian" };
            List<IDataControl> list = BaseDataAdapter.GetListDC(this, sql, fields);
            List<ViewasetPindahtanganControl> ListData = new List<ViewasetPindahtanganControl>();

            foreach (ViewasetPindahtanganControl dc in list)
            {
                ListData.Add(dc);
            }
            return ListData;
        }
        #endregion Methods 
    }
    #endregion ViewasetPindahtangan

    #region ViewasetHapussk
    [Serializable]
    public class ViewasetHapusskControl : ViewasetControl, IDataControlUIEntry, IHasJSScript
    {
        #region Methods 
        public new void SetFilterKey(BaseBO bo)
        {
            if (bo.GetProperty("Unitkey") != null)
            {
                Unitkey = bo.GetValue("Unitkey").ToString();
            }
            if (bo.GetProperty("Noskhapus") != null)
            {
                Noskhapus = bo.GetValue("Noskhapus").ToString();
            }
        }
        public new IList View()
        {
            string sql = @"
        exec [dbo].[WSPV_HAPUSSKDET]
		    @UNITKEY = N'{0}',
             @NOSKHAPUS = N'{1}'
      ";

            sql = string.Format(sql, Unitkey, Noskhapus);
            string[] fields = new string[] { "Idbrg", "Unitkey", "Asetkey", "Kdaset", "Nmaset", "Tahun", "Noreg"
        , "Nilai", "Umeko", "Kdpemilik", "Nmpemilik", "Kdkon", "Nmkon", "Asalusul"
        , "Pengguna", "Kdsatuan", "Nmsatuan","Spesifikasi", "Ukuran", "Bahan", "Nosertifikat", "Alamat", "Ket"
        , "Kdklas", "Uraiklas", "Kdstatusaset", "Kdkib", "Nmkib", "Kdtans", "Nmtrans", "Nopindahtangan" };
            List<IDataControl> list = BaseDataAdapter.GetListDC(this, sql, fields);
            List<ViewasetHapusskControl> ListData = new List<ViewasetHapusskControl>();

            foreach (ViewasetHapusskControl dc in list)
            {
                ListData.Add(dc);
            }
            return ListData;
        }
        public override DataControlFieldCollection GetColumns()
        {
            DataControlFieldCollection columns = new DataControlFieldCollection();
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nopindahtangan=Dokumen Pindahtangan"), typeof(string), 30, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmtrans=Jenis Transaksi"), typeof(string), 25, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 20, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 35, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun"), typeof(int), 15, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreg=No Register"), typeof(string), 15, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai=Nilai Perolehan"), typeof(decimal), 25, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Umeko=Masa Pakai"), typeof(decimal), 25, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmpemilik=Pemilik"), typeof(string), 30, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmkon=Kondisi"), typeof(string), 15, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmsatuan=Satuan"), typeof(string), 15, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Asalusul=Asal Usul"), typeof(string), 25, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Pengguna"), typeof(string), 25, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Spesifikasi"), typeof(string), 30, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nosertifikat"), typeof(string), 35, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ukuran"), typeof(string), 30, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Bahan"), typeof(string), 15, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Alamat"), typeof(string), 50, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 100, HorizontalAlign.Left));

            return columns;
        }
        #endregion Methods 
    }
    #endregion ViewasetHapussk

    #region ViewasetReklas
    [Serializable]
    public class ViewasetReklasControl : ViewasetControl, IDataControlUIEntry, IHasJSScript
    {
        #region Methods 
        public new void SetFilterKey(BaseBO bo)
        {
            if (bo.GetProperty("Unitkey") != null)
            {
                Unitkey = bo.GetValue("Unitkey").ToString();
            }
            if (bo.GetProperty("Kdtans") != null)
            {
                Kdtans = bo.GetValue("Kdtans").ToString();
            }
        }
        public new HashTableofParameterRow GetFilters
            ()
        {
            HashTableofParameterRow hpars = new HashTableofParameterRow();
            //hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdkib=Jenis KIB"),
            //GetList(new JnskibTransLookupControl()), "Kdkib=Nmkib", 48).SetAllowRefresh(true).SetEnable(true).SetAllowEmpty(false));
            //hpars.Add(DaftasetKibLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(true).SetAllowEmpty(false));
            if (Kdtans == "130")
            {
                hpars.Add(DaftasetObjekAsetlainnyaLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(true).SetAllowRefresh(true).SetAllowEmpty(false));
            }
            else if (Kdtans == "129")
            {
                hpars.Add(DaftasetObjekPersediaanLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(true).SetAllowRefresh(true).SetAllowEmpty(false));
            }
            else
            {
                hpars.Add(DaftasetObjekLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(true).SetAllowRefresh(true).SetAllowEmpty(false));
            }

            return hpars;
        }
        public new IList View()
        {
            string sql = @"
        exec [dbo].[WSPV_REKLASDET]
		    @UNITKEY = N'{0}',
		    @KDASET = N'{1}'
      ";

            sql = string.Format(sql, Unitkey, Kdobjekaset);
            string[] fields = new string[] { "Idbrg", "Unitkey", "Asetkey", "Kdaset", "Nmaset", "Tahun", "Noreg"
        , "Nilai", "Umeko", "Kdpemilik", "Nmpemilik", "Kdkon", "Nmkon", "Asalusul"
        , "Pengguna", "Kdsatuan", "Nmsatuan","Spesifikasi", "Ukuran", "Bahan", "Nosertifikat", "Alamat", "Ket", "Judul"
        , "Kdklas", "Uraiklas", "Kdstatusaset", "Kdkib", "Nmkib" };
            List<IDataControl> list = BaseDataAdapter.GetListDC(this, sql, fields);
            List<ViewasetReklasControl> ListData = new List<ViewasetReklasControl>();

            foreach (ViewasetReklasControl dc in list)
            {
                ListData.Add(dc);
            }
            return ListData;
        }
        #endregion Methods 
    }
    #endregion ViewasetReklas

    #region ViewasetKoreksi
    [Serializable]
    public class ViewasetKoreksiControl : ViewasetControl, IDataControlUIEntry, IHasJSScript
    {
        #region Methods 
        public new void SetFilterKey(BaseBO bo)
        {
            if (bo.GetProperty("Unitkey") != null)
            {
                Unitkey = bo.GetValue("Unitkey").ToString();
            }
        }
        public new HashTableofParameterRow GetFilters()
        {
            HashTableofParameterRow hpars = new HashTableofParameterRow();
            //hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdkib=Jenis KIB"),
            //GetList(new JnskibTransLookupControl()), "Kdkib=Nmkib", 48).SetAllowRefresh(true).SetEnable(true).SetAllowEmpty(false));
            //hpars.Add(DaftasetKibLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(true).SetAllowEmpty(false));
            hpars.Add(DaftasetObjekLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(true).SetAllowRefresh(true).SetAllowEmpty(false));
            return hpars;
        }
        public new IList View()
        {
            string sql = @"
        exec [dbo].[WSPV_KOREKSIDET]
		    @UNITKEY = N'{0}',
		    @KDASET = N'{1}'
      ";

            sql = string.Format(sql, Unitkey, Kdobjekaset);
            string[] fields = new string[] { "Idbrg", "Unitkey", "Asetkey", "Kdaset", "Nmaset", "Tahun", "Noreg"
        , "Nilai", "Umeko", "Kdpemilik", "Nmpemilik", "Kdkon", "Nmkon", "Asalusul"
        , "Pengguna", "Kdsatuan", "Nmsatuan","Spesifikasi", "Ukuran", "Bahan", "Nosertifikat", "Alamat", "Ket"
        , "Kdklas", "Uraiklas", "Kdstatusaset", "Kdkib", "Nmkib" };
            List<IDataControl> list = BaseDataAdapter.GetListDC(this, sql, fields);
            List<ViewasetKoreksiControl> ListData = new List<ViewasetKoreksiControl>();

            foreach (ViewasetKoreksiControl dc in list)
            {
                ListData.Add(dc);
            }
            return ListData;
        }
        #endregion Methods 
    }
    #endregion ViewasetKoreksi

    #region ViewasetPengguna
    [Serializable]
    public class ViewasetPenggunaControl : ViewasetControl, IDataControlUIEntry, IHasJSScript
    {
        #region Methods 
        public new void SetFilterKey(BaseBO bo)
        {
            if (bo.GetProperty("Unitkey") != null)
            {
                Unitkey = bo.GetValue("Unitkey").ToString();
            }
        }
        public new HashTableofParameterRow GetFilters()
        {
            HashTableofParameterRow hpars = new HashTableofParameterRow();
            //hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdkib=Jenis KIB"),
            //GetList(new JnskibTransLookupControl()), "Kdkib=Nmkib", 48).SetAllowRefresh(true).SetEnable(true).SetAllowEmpty(false));
            //hpars.Add(DaftasetKibLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(true).SetAllowEmpty(false));
            hpars.Add(DaftasetObjekLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(true).SetAllowRefresh(true).SetAllowEmpty(false));
            return hpars;
        }
        public new IList View()
        {
            string sql = @"
        exec [dbo].[WSPV_PENGGUNADET]
		    @UNITKEY = N'{0}',
		    @KDASET = N'{1}'
      ";

            sql = string.Format(sql, Unitkey, Kdobjekaset);
            string[] fields = new string[] { "Idbrg", "Unitkey", "Asetkey", "Kdaset", "Nmaset", "Tahun", "Noreg"
        , "Nilai", "Umeko", "Kdpemilik", "Nmpemilik", "Kdkon", "Nmkon", "Asalusul"
        , "Pengguna", "Kdsatuan", "Nmsatuan","Spesifikasi", "Ukuran", "Bahan", "Nosertifikat", "Alamat", "Ket"
        , "Kdklas", "Uraiklas", "Kdstatusaset", "Kdkib", "Nmkib" };
            List<IDataControl> list = BaseDataAdapter.GetListDC(this, sql, fields);
            List<ViewasetPenggunaControl> ListData = new List<ViewasetPenggunaControl>();

            foreach (ViewasetPenggunaControl dc in list)
            {
                ListData.Add(dc);
            }
            return ListData;
        }
        #endregion Methods 
    }
    #endregion ViewasetPengguna

    #region ViewasetHapussksebagian
    [Serializable]
    public class ViewasetHapussksebagianControl : ViewasetControl, IDataControlUIEntry, IHasJSScript
    {
        #region Methods 
        public new void SetFilterKey(BaseBO bo)
        {
            if (bo.GetProperty("Unitkey") != null)
            {
                Unitkey = bo.GetValue("Unitkey").ToString();
            }
        }
        public new IList View()
        {
            string sql = @"
        exec [dbo].[WSPV_HAPUSSKSEBAGIAN]
		    @UNITKEY = N'{0}'
      ";

            sql = string.Format(sql, Unitkey);
            string[] fields = new string[] { "Idbrg", "Unitkey", "Asetkey", "Kdaset", "Nmaset", "Tahun", "Noreg"
        , "Nilai", "Umeko", "Kdpemilik", "Nmpemilik", "Kdkon", "Nmkon", "Asalusul"
        , "Pengguna", "Kdsatuan", "Nmsatuan","Spesifikasi", "Ukuran", "Bahan", "Nosertifikat", "Alamat", "Ket"
        , "Kdklas", "Uraiklas", "Kdstatusaset", "Kdkib", "Nmkib", "Kdtans", "Nmtrans", "Nopenilaian" };
            List<IDataControl> list = BaseDataAdapter.GetListDC(this, sql, fields);
            List<ViewasetHapussksebagianControl> ListData = new List<ViewasetHapussksebagianControl>();

            foreach (ViewasetHapussksebagianControl dc in list)
            {
                ListData.Add(dc);
            }
            return ListData;
        }
        public override DataControlFieldCollection GetColumns()
        {
            DataControlFieldCollection columns = new DataControlFieldCollection();
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 20, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 35, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun"), typeof(int), 15, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreg=No Register"), typeof(string), 15, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai=Nilai Penghapusan"), typeof(decimal), 25, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Umeko=Masa Pakai"), typeof(decimal), 25, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmpemilik=Pemilik"), typeof(string), 30, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmkon=Kondisi"), typeof(string), 15, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmsatuan=Satuan"), typeof(string), 15, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Asalusul=Asal Usul"), typeof(string), 25, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Pengguna"), typeof(string), 25, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Spesifikasi"), typeof(string), 30, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nosertifikat"), typeof(string), 35, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ukuran"), typeof(string), 30, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Bahan"), typeof(string), 15, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Alamat"), typeof(string), 50, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 100, HorizontalAlign.Left));

            return columns;
        }
        #endregion Methods 
    }
    #endregion ViewasetHapussksebagian

    #region ViewasetUsulanhapus
    [Serializable]
    public class ViewasetUsulanhapusControl : ViewasetControl, IDataControlUIEntry, IHasJSScript
    {
        #region Methods 
        public new void SetFilterKey(BaseBO bo)
        {
            if (bo.GetProperty("Unitkey") != null)
            {
                Unitkey = bo.GetValue("Unitkey").ToString();
            }
        }
        public new HashTableofParameterRow GetFilters()
        {
            HashTableofParameterRow hpars = new HashTableofParameterRow();
            hpars.Add(DaftasetObjekLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(true).SetAllowRefresh(true).SetAllowEmpty(false));
            return hpars;
        }
        public new IList View()
        {
            string sql = @"
        exec [dbo].[WSPV_USULANHAPUSDET]
		    @UNITKEY = N'{0}',
		    @KDASET = N'{1}'
      ";

            sql = string.Format(sql, Unitkey, Kdobjekaset);
            string[] fields = new string[] { "Idbrg", "Unitkey", "Asetkey", "Kdaset", "Nmaset", "Tahun", "Noreg"
        , "Nilai", "Umeko", "Kdpemilik", "Nmpemilik", "Kdkon", "Nmkon", "Asalusul"
        , "Pengguna", "Kdsatuan", "Nmsatuan","Spesifikasi", "Ukuran", "Bahan", "Nosertifikat", "Alamat", "Ket"
        , "Kdklas", "Uraiklas", "Kdstatusaset", "Kdkib", "Nmkib" };
            List<IDataControl> list = BaseDataAdapter.GetListDC(this, sql, fields);
            List<ViewasetUsulanhapusControl> ListData = new List<ViewasetUsulanhapusControl>();

            foreach (ViewasetUsulanhapusControl dc in list)
            {
                ListData.Add(dc);
            }
            return ListData;
        }
        #endregion Methods 
    }
    #endregion ViewasetUsulanhapus

    #region ViewasetHapusbatal
    [Serializable]
    public class ViewasetHapusbatalControl : ViewasetControl, IDataControlUIEntry, IHasJSScript
    {
        #region Methods 
        public new void SetFilterKey(BaseBO bo)
        {
            if (bo.GetProperty("Unitkey") != null)
            {
                Unitkey = bo.GetValue("Unitkey").ToString();
            }
        }
        public new IList View()
        {
            string sql = @"
        exec [dbo].[WSPV_HAPUSBATAL]
		    @UNITKEY = N'{0}'
      ";

            sql = string.Format(sql, Unitkey);
            string[] fields = new string[] { "Idbrg", "Unitkey", "Asetkey", "Kdaset", "Nmaset", "Tahun", "Noreg"
        , "Nilai", "Umeko", "Kdpemilik", "Nmpemilik", "Kdkon", "Nmkon", "Asalusul"
        , "Pengguna", "Kdsatuan", "Nmsatuan","Spesifikasi", "Ukuran", "Bahan", "Nosertifikat", "Alamat", "Ket"
        , "Kdklas", "Uraiklas", "Kdstatusaset", "Kdkib", "Nmkib", "Kdtans", "Nmtrans", "Noskhapus" };
            List<IDataControl> list = BaseDataAdapter.GetListDC(this, sql, fields);
            List<ViewasetHapusbatalControl> ListData = new List<ViewasetHapusbatalControl>();

            foreach (ViewasetHapusbatalControl dc in list)
            {
                ListData.Add(dc);
            }
            return ListData;
        }
        public override DataControlFieldCollection GetColumns()
        {
            DataControlFieldCollection columns = new DataControlFieldCollection();
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noskhapus=No. Penghapusan"), typeof(string), 30, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmtrans=Jenis Transaksi"), typeof(string), 25, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 20, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 35, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun"), typeof(int), 15, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreg=No Register"), typeof(string), 15, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai=Nilai Perolehan"), typeof(decimal), 25, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Umeko=Masa Pakai"), typeof(decimal), 25, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmpemilik=Pemilik"), typeof(string), 30, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmkon=Kondisi"), typeof(string), 15, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmsatuan=Satuan"), typeof(string), 15, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Asalusul=Asal Usul"), typeof(string), 25, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Pengguna"), typeof(string), 25, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Spesifikasi"), typeof(string), 30, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nosertifikat"), typeof(string), 35, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ukuran"), typeof(string), 30, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Bahan"), typeof(string), 15, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Alamat"), typeof(string), 50, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 100, HorizontalAlign.Left));

            return columns;
        }
        #endregion Methods 
    }
    #endregion ViewasetHapusbatal
}

