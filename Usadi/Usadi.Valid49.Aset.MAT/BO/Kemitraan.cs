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
    #region Usadi.Valid49.BO.KemitraanControl, Usadi.Valid49.Aset.MAT
    [Serializable]
    public class KemitraanControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
    {
        #region Properties
        public long Id { get; set; }
        public string Kdjenis { get; set; }
        public string Nmjenis { get; set; }
        public DateTime Tglawal { get; set; }
        public DateTime Tglakhir { get; set; }
        public DateTime Tglvalid { get; set; }
        public decimal Nilai { get; set; }
        public string Iddokumen { get; set; }
        public string Idpengajuan { get; set; }
        public new string Ket { get; set; }
        public string Kdunit { get; set; }
        public string Nmunit { get; set; }
        public string Nmp3 { get; set; }
        public string Nminst { get; set; }
        public string Nmtrans { get; set; }
        public string Iddokumenawal { get; set; }
        public string Nodokumenawal { get; set; }
        public int Jmldata { get; set; }
        public int Jmlkib { get; set; }
        public int Tahunsa { get; set; }
        public DateTime Tglsa { get; set; }
        public string Kdtans { get; set; }
        public string Kdp3 { get; set; }
        public string Kdpengguna { get; set; }
        public string Nmpengguna { get; set; }
        public string Nodokumen { get; set; }
        public string Unitkey { get; set; }
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
                    CommandName = "ViewKemitraandet",
                    Icon = Icon.PageCopy
                };
                cmd1.ToolTip.Text = "Klik Untuk Menampilkan Rincian Kemitraan";
                return new ImageCommand[] { cmd1 };
            }
        }

        public string ViewKemitraandet
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
                return "No. Dokumen; " + Nodokumen + ":" + url;
            }
        }
        #endregion Properties 

        #region Methods 
        public KemitraanControl()
        {
            XMLName = ConstantTablesAsetMAT.XMLKEMITRAAN;
            #region COR-49 Refactor Get Tahun dari Setting DB - STEP 2
            Ss10userControl dcUser = (Ss10userControl)GlobalAsp.GetSessionUser();
            Tahun = dcUser.Tahun;
            Tahunsa = dcUser.Tahun;
            #endregion
    }
    public new IProperties GetProperties()
        {
            ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
            cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
            cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Nodokumen", "Kdp3", "Kdtans" };
            cViewListProperties.IDKey = "Id";
            cViewListProperties.IDProperty = "Id";
            cViewListProperties.ReadOnlyFields = new String[] { "Unitkey", "Kdunit", "Nmunit", "Kdjenis", "Tahunsa", "Tahun" };
            cViewListProperties.SortFields = new String[] { "Tglawal", "Nodokumen" };
            cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
            cViewListProperties.PageSize = 20;

            if (Blokid == "1")
            {
                cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
            }
            else
            {
                cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_EDIT_DEL;
                cViewListProperties.AllowMultiDelete = true;
            }

            return cViewListProperties;
        }
        public override DataControlFieldCollection GetColumns()
        {
            bool enable = true;

            if (Blokid == "1")
            {
                enable = false;
            }

            DataControlFieldCollection columns = new DataControlFieldCollection();
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center).SetVisible(enable));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Stricon"), typeof(CommandColumn), Cmds, 5, HorizontalAlign.Center));

            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nodokumen=No Dokumen Kontrak"), typeof(string), 30, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmp3=Mitra Pemanfaatan"), typeof(string), 50, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglawal=Tanggal Awal"), typeof(DateTime), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglakhir=Tanggal Akhir"), typeof(DateTime), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglvalid=Tanggal Pengesahan"), typeof(DateTime), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai=Harga"), typeof(decimal), 25, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 100, HorizontalAlign.Left));

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
        public new void SetPrimaryKey()
        {

            Tglsa = new DateTime(Tahunsa, DateTime.Today.Month, DateTime.Today.Day);
            Tglawal = Tglsa;

            KemitraanControl cKemitraanGetiddokumen = new KemitraanControl();
            cKemitraanGetiddokumen.Load("Dokumen");

            Iddokumen = cKemitraanGetiddokumen.Iddokumen;
        }

        public new IList View()
        {
            IList list = this.View(BaseDataControl.ALL);
            return list;
        }
        public new IList View(string label)
        {
            IList list = ((BaseDataControl)this).View(label);
            List<KemitraanControl> ListData = new List<KemitraanControl>();
            foreach (KemitraanControl dc in list)
            {
                dc.Valid = (dc.Tglvalid != new DateTime());
                ListData.Add(dc);
            }

            return ListData;
        }
        private bool IsValidEdit()
        {
            bool valid = true;
            return valid;
        }
        public new void Insert()
        {
            if (IsValidInsert())
            {
                Kdp3 = Kdpengguna;
                KemitraanControl cKemitraanGetidpengajuan = new KemitraanControl();
                cKemitraanGetidpengajuan.Unitkey = Unitkey;
                cKemitraanGetidpengajuan.Iddokumen = Iddokumen;
                cKemitraanGetidpengajuan.Load("Pengajuan");

                Idpengajuan = cKemitraanGetidpengajuan.Idpengajuan;
                Nilai = 0;

                base.Insert();

                if (Kdjenis == "02")
                {
                    base.Insert("Perpanjangan");
                }
            }
        }
        private bool IsValidInsert()
        {
            var data = this.View("Nodok");
            if (data.Count == 0)
            {
                return true;
            }
            else
            {
                throw new Exception($"Data dengan No Dokumen = {Nodokumen} sdh ada");
            }
        }
        public new int Update()
        {
            int n = 0;
            //Kdp3 = Kdpengguna;
            if (IsValidEdit())
            {
                n = ((BaseDataControlUI)this).Update();

                if (Valid) //ceklis valid
                {
                    KemitraanControl cKemitraanGetjmlkib = new KemitraanControl();
                    cKemitraanGetjmlkib.Unitkey = Unitkey;
                    cKemitraanGetjmlkib.Nodokumen = Nodokumen;
                    cKemitraanGetjmlkib.Kdtans = Kdtans;
                    cKemitraanGetjmlkib.Load("Jmlkib");

                    if (cKemitraanGetjmlkib.Jmlkib >= 1)
                    {
                        throw new Exception("Gagal mengubah data : Dokumen ini sudah di sahkan dan masuk ke KIB");
                    }
                    else
                    {
                        string sql = @"
                        exec [dbo].[WSP_VALKEMITRAAN]
                        @UNITKEY = N'{0}',
                        @NODOKUMEN = N'{1}',
                        @TGLAWAL = N'{2}',
                        @TGLAKHIR = N'{3}',
                        @KDP3 = N'{4}',
                        @KDTANS = N'{5}'
                        ";

                        sql = string.Format(sql, Unitkey, Nodokumen, Tglawal.ToString("yyyy-MM-dd"), Tglakhir.ToString("yyyy-MM-dd"), Kdp3, Kdtans);
                        BaseDataAdapter.ExecuteCmd(this, sql);
                    }
                }
            }
            return n;
        }
        public new int Delete()
        {
            KemitraanControl cKemitraanGetjmlkib = new KemitraanControl();
            cKemitraanGetjmlkib.Unitkey = Unitkey;
            cKemitraanGetjmlkib.Nodokumen = Nodokumen;
            cKemitraanGetjmlkib.Kdtans = Kdtans;
            cKemitraanGetjmlkib.Load("Jmlkib");

            int n = 0;
            if (Valid)
            {
                if (cKemitraanGetjmlkib.Jmlkib != 0)
                {
                    throw new Exception("Dokumen kontrak sudah digunakan di KIB, pengesahan tidak dapat dicabut");
                }

                return ((BaseDataControlUI)this).Update("Draft");
            }
            else
            {
                try
                {
                    Status = -1;
                    base.Delete();
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("REFERENCE"))
                    {
                        string msg = "Hapus rincian barang terlebih dahulu";
                        msg = string.Format(msg);
                        throw new Exception(msg);
                    }
                }
            }
            return n;
        }
        public new HashTableofParameterRow GetFilters()
        {
            bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
            HashTableofParameterRow hpars = new HashTableofParameterRow();

            hpars.Add(DaftunitLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetAllowEmpty(false));
            hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdjenis=Jenis Kontrak"),
            GetList(new JkontrakLookupControl()), "Kdjenis=Nmjenis", 41).SetAllowRefresh(true).SetEnable(enableFilter).SetAllowEmpty(false));
            return hpars;
        }
        public override HashTableofParameterRow GetEntries()
        {
            bool enable = !Valid, enableValid = false, enableKontrak = false;

            if (Kdjenis == "02")
            {
                enableKontrak = true;
            }
            else if (Kdjenis == "01")
            {
                Iddokumenawal = null;
                Nodokumenawal = null;
            }

            KemitraanControl cKemitraanGetjmldata = new KemitraanControl();
            cKemitraanGetjmldata.Unitkey = Unitkey;
            cKemitraanGetjmldata.Nodokumen = Nodokumen;
            cKemitraanGetjmldata.Kdp3 = Kdp3;
            cKemitraanGetjmldata.Kdtans = Kdtans;
            cKemitraanGetjmldata.Load("Jmldata");

            if (cKemitraanGetjmldata.Jmldata != 0)
            {
                enableValid = true;
            }

            HashTableofParameterRow hpars = new HashTableofParameterRow();
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nodokumen=Nomor Dokumen"), false, 50).SetEnable(enable));
            hpars.Add(KemitraanLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enableKontrak).SetEditable(false));
            hpars.Add(DaftunitPenggunaLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enable)
              .SetEditable(false).SetVisible(false));
            hpars.Add(DaftpenggunaLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enable)
              .SetEditable(false).SetVisible(true));
            //hpars.Add(Daftphk3LookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enable).SetEditable(false));
            hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglawal=Tanggal Awal"), true).SetEnable(enable));
            hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglakhir=Tanggal Akhir"), true).SetEnable(enable));
            hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Ket=Keterangan"), true, 3).SetEnable(enable).SetAllowEmpty(true));
            hpars.Add(new ParameterRowCek(this, true).SetEnable(enableValid).SetEditable(enable));

            return hpars;
        }
        #endregion Methods 
    }
    #endregion Kemitraan

    #region Sewa
    [Serializable]
    public class SewaControl : KemitraanControl, IDataControlUIEntry
    {
        public new void SetPageKey()
        {
            //219 Disewakan (Keluar)
            Kdtans = "219"; // "301";
        }
    }
    #endregion Sewa

    #region Pinjampakai
    [Serializable]
    public class PinjamPakaiControl : KemitraanControl, IDataControlUIEntry
    {
        public new void SetPageKey()
        {
            Kdtans = "220"; //  Pinjam Pakai(Keluar) "302";
        }
    }
    #endregion Pinjampakai

    #region KSP
    [Serializable]
    public class KspControl : KemitraanControl, IDataControlUIEntry
    {
        public new void SetPageKey()
        {
            Kdtans = "221"; //  KSP(Keluar) "303";
        }
    }
    #endregion KSP

    #region KSPI
    [Serializable]
    public class KspiControl : KemitraanControl, IDataControlUIEntry
    {
        public new void SetPageKey()
        {
            Kdtans = "222"; //  KSPI(Keluar) "304";
        }
    }
    #endregion KSPI

    #region BGS
    [Serializable]
    public class BgsControl : KemitraanControl, IDataControlUIEntry
    {
        public new void SetPageKey()
        {
            Kdtans = "223"; // BGS(Keluar)"305";
        }
    }
    #endregion BGS

    #region BSG
    [Serializable]
    public class BsgControl : KemitraanControl, IDataControlUIEntry
    {
        public new void SetPageKey()
        {
            Kdtans = "224";//  BSG(Keluar)"306";
        }
    }
    #endregion BSG
}

