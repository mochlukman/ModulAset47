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
    #region Usadi.Valid49.BO.KoreksiControl, Usadi.Valid49.Aset.MAT
    [Serializable]
    public class KoreksiControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
    {
        #region Properties
        public long Id { get; set; }
        public DateTime Tglbakoreksi { get; set; }
        public string Uraibakoreksi { get; set; }
        public string Kdtans { get; set; }
        public DateTime Tglvalid { get; set; }
        public string Kdunit { get; set; }
        public string Nmunit { get; set; }
        public string Nmtrans { get; set; }
        public int Jmldata { get; set; }
        public int Jmlkib { get; set; }
        public int Tahunsa { get; set; }
        public DateTime Tglsa { get; set; }
        public string Unitkey { get; set; }
        public string Nobakoreksi { get; set; }

        public string Noreg { get; set; }
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
                    CommandName = "ViewKoreksidet",
                    Icon = Icon.PageCopy
                };
                cmd1.ToolTip.Text = "Klik Untuk Menampilkan Rincian Mutasi";
                return new ImageCommand[] { cmd1 };
            }
        }

        public string ViewKoreksidet
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
                return "No. Dokumen; " + Nobakoreksi + ":" + url;
            }
        }
        #endregion Properties 

        #region Methods 
        public KoreksiControl()
        {
            XMLName = ConstantTablesAsetMAT.XMLKOREKSI;
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
            cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Nobakoreksi" };
            cViewListProperties.IDKey = "Id";
            cViewListProperties.IDProperty = "Id";
            cViewListProperties.ReadOnlyFields = new String[] { "Unitkey", "Kdtans", "Tahunsa", "Tahun" };
            cViewListProperties.SortFields = new String[] { "Tglbakoreksi" };
            cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
            cViewListProperties.PageSize = 20;
            cViewListProperties.RefreshFilter = true;

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

            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nobakoreksi=Nomor Dokumen"), typeof(string), 40, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglbakoreksi=Tanggal Dokumen"), typeof(DateTime), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglvalid=Tanggal Pengesahan"), typeof(DateTime), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Uraibakoreksi=Uraian"), typeof(string), 50, HorizontalAlign.Left));

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
            Tglbakoreksi = Tglsa;
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
            List<KoreksiControl> ListData = new List<KoreksiControl>();
            foreach (KoreksiControl dc in list)
            {
                dc.Valid = (dc.Tglvalid != new DateTime());
                ListData.Add(dc);
            }

            return ListData;
        }
        private bool IsValidInsert()
        {
            var data = this.View("Noba");
            if (data.Count == 0)
            {
                return true;
            }
            else
            {
                throw new Exception($"Data dengan No.Berita Acara = {Nobakoreksi} sdh ada");
            }
        }
        public new void Insert()
        {
            PemdaControl cPemda = new PemdaControl();
            cPemda.Configid = "cur_thang";
            cPemda.Load("PK");

            if (Tglbakoreksi.Year.ToString().Trim() != cPemda.Configval.Trim())
            {
                throw new Exception("Gagal menyimpan data : Transaksi koreksi hanya untuk tahun anggaran berjalan.");
            }
            if (IsValidInsert())
            {
                base.Insert();
            }
        }
        private bool IsValidUpdate()
        {
            bool valid = true;
            return valid;
        }
        public new int Update()
        {
            PemdaControl cPemda = new PemdaControl();
            cPemda.Configid = "cur_thang";
            cPemda.Load("PK");

            int n = 0;
            if (IsValidUpdate())
            {
                if (Tglbakoreksi.Year.ToString().Trim() != cPemda.Configval.Trim())
                {
                    throw new Exception("Gagal menyimpan data : Transaksi Koreksi hanya untuk tahun anggaran berjalan.");
                }
                else
                {
                    n = ((BaseDataControlUI)this).Update();

                    if (Valid)
                    {
                        KoreksiControl cKoreksiGetjmlkib = new KoreksiControl();
                        cKoreksiGetjmlkib.Unitkey = Unitkey;
                        cKoreksiGetjmlkib.Nobakoreksi = Nobakoreksi;
                        cKoreksiGetjmlkib.Kdtans = Kdtans;
                        cKoreksiGetjmlkib.Load("Jmlkib");

                        if (cKoreksiGetjmlkib.Jmlkib >= 1)
                        {
                            throw new Exception("Gagal mengubah data : Dokumen koreksi ini sudah di sahkan dan masuk ke KIB");
                        }
                        else
                        {
                            string sql = @"
              exec [dbo].[WSPX_KOREKSI]
              @UNITKEY = N'{0}',
              @NOBAKOREKSI = N'{1}',
              @TGLBAKOREKSI = N'{2}',
              @KDTANS = N'{3}'
              ";
                            sql = string.Format(sql, Unitkey, Nobakoreksi, Tglbakoreksi.ToString("yyyy-MM-dd"), Kdtans);
                            BaseDataAdapter.ExecuteCmd(this, sql);
                        }
                    }
                }
            }
            return n;
        }
        public new int Delete()
        {
            bool cekjmlkib = false;

            KoreksiControl cKoreksiGetjmlkib = new KoreksiControl();
            cKoreksiGetjmlkib.Unitkey = Unitkey;
            cKoreksiGetjmlkib.Nobakoreksi = Nobakoreksi;
            cKoreksiGetjmlkib.Kdtans = Kdtans;
            cKoreksiGetjmlkib.Load("Jmlkib");
            Jmlkib = cKoreksiGetjmlkib.Jmlkib;

            cekjmlkib = (Jmlkib >= 1);

            int n = 0;
            if (Valid)
            {
                if (cekjmlkib == true)
                {
                    throw new Exception("Koreksi sudah masuk ke KIB, pengesahan tidak dapat dicabut");
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
        public override HashTableofParameterRow GetEntries()
        {
            bool enable = !Valid, enableValid = false;

            KoreksiControl cKoreksiGetjmldata = new KoreksiControl();
            cKoreksiGetjmldata.Unitkey = Unitkey;
            cKoreksiGetjmldata.Nobakoreksi = Nobakoreksi;
            cKoreksiGetjmldata.Load("Jmldata");

            if (cKoreksiGetjmldata.Jmldata != 0)
            {
                enableValid = true;
            }

            HashTableofParameterRow hpars = new HashTableofParameterRow();
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nobakoreksi=Nomor Dokumen"), false, 50).SetEnable(enable));
            hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglbakoreksi=Tgl Dokumen"), true).SetEnable(enable));
            hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Uraibakoreksi=Uraian"), true, 3).SetEnable(enable).SetAllowEmpty(true));
            hpars.Add(new ParameterRowCek(this, true).SetEnable(enableValid).SetEditable(enable));
            return hpars;
        }
        #endregion Methods 
    }
    #endregion Koreksi

    #region KoreksiTambah
    [Serializable]
    public class KoreksiTambahControl : KoreksiControl, IDataControlUIEntry
    {
        public new void SetPageKey()
        {
            Kdtans = "113";
        }
    }
    #endregion KoreksiTambah

    #region KoreksiKurang
    [Serializable]
    public class KoreksiKurangControl : KoreksiControl, IDataControlUIEntry
    {
        public new void SetPageKey()
        {
            Kdtans = "213";
        }
    }
    #endregion KoreksiKurang

    #region KoreksiGanda
    [Serializable]
    public class KoreksiGandaControl : KoreksiControl, IDataControlUIEntry
    {
        public new void SetPageKey()
        {
            Kdtans = "228";
        }
    }
    #endregion KoreksiGanda

    #region KoreksiSpesifikasi
    [Serializable]
    public class KoreksiSpesifikasiControl : KoreksiControl, IDataControlUIEntry
    {
        public new void SetPageKey()
        {
            Kdtans = "131";
        }
    }
    #endregion KoreksiSpesifikasi

    #region KoreksiLainnya
    [Serializable]
    public class KoreksiLainnyaControl : KoreksiControl, IDataControlUIEntry
    {
        public new void SetPageKey()
        {
            Kdtans = "132";
        }
    }
    #endregion KoreksiLainnya
}

