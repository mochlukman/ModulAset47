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
    #region Usadi.Valid49.BO.BeritaControl, Usadi.Valid49.Aset.MAT
    [Serializable]
    public class BeritaControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
    {
        #region Properties
        public long Id { get; set; }
        public DateTime Tglba { get; set; }
        public string Tglbalookup { get { return Tglba.ToString("dd/MM/yyyy"); } }
        public string Nobap { get; set; }
        public DateTime Tglbap { get; set; }
        public string Kdkegunit { get; set; }
        public string Kdtahap { get; set; }
        public string Idprgrm { get; set; }
        public string Nokontrak { get; set; }
        public string Uraiba { get; set; }
        public DateTime Tglvalid { get; set; }
        public string Kdbukti { get; set; }
        public string Kdtans { get; set; }
        public string Kddana { get; set; }
        public string Kdunit { get; set; }
        public string Nmunit { get; set; }
        public string Nukeg { get; set; }
        public string Nmkegunit { get; set; }
        public string Nmprgrm { get; set; }
        public string Uraian { get; set; }
        public string Kdp3 { get; set; }
        public string Nminst { get; set; }
        public decimal Nilai { get; set; }
        public decimal Nilaibakf { get; set; }
        public string Nmtrans { get; set; }
        public string Nmdana { get; set; }
        public string Nmbukti { get; set; }
        public int Jmlrek { get; set; }
        public int Jmlbrg { get; set; }
        public int Jmlgenerated { get; set; }
        public int Jmlbrgpengguna { get; set; }
        public int Jmlkib { get; set; }
        public int Tahunsa { get; set; }
        public DateTime Tglsa { get; set; }
        public string Unitkey { get; set; }
        public string Noba { get; set; }
        public string Kdkib { get; set; }
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
                    CommandName = "ViewBeritadetr",
                    Icon = Icon.PageCopy
                };
                cmd1.ToolTip.Text = "Klik Untuk Menampilkan Rincian Rekening";
                return new ImageCommand[] { cmd1 };
            }
        }

        public string ViewBeritadetr
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
                return "Rincian Rekening; " + Noba + "- Nilai Kontrak Rp. " + Nilai.ToString("#,##0") + ":" + url;
            }
        }
        #endregion Properties 

        #region Methods 
        public BeritaControl()
        {
            XMLName = ConstantTablesAsetMAT.XMLBERITA;
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
            cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Noba" };
            cViewListProperties.IDKey = "Id";
            cViewListProperties.IDProperty = "Id";
            cViewListProperties.ReadOnlyFields = new String[] { "Unitkey", "Kdunit", "Nmunit", "Kdtahap", "Kdkegunit", "Nukeg", "Nmkegunit", "Tahunsa", "Tahun" };
            cViewListProperties.SortFields = new String[] { "Tglba", "Noba" };
            cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
            cViewListProperties.PageSize = 30;
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
        public new HashTableofParameterRow GetFilters()
        {
            bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
            HashTableofParameterRow hpars = new HashTableofParameterRow();
            hpars.Add(DaftunitLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetAllowEmpty(false));
            return hpars;
        }
        public override DataControlFieldCollection GetColumns()
        {
            bool enable = true; string nama;

            if (Blokid == "1")
            {
                enable = false;
            }

            if (Kdtans == null || Kdtans == "")
            {
                nama = "BAKF";
            }
            else
            {
                nama = "BAST";
            }

            DataControlFieldCollection columns = new DataControlFieldCollection();
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center).SetVisible(enable));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Stricon"), typeof(CommandColumn), Cmds, 5, HorizontalAlign.Center));

            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noba=No " + nama), typeof(string), 30, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglba=Tanggal " + nama), typeof(DateTime), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglvalid=Tanggal Pengesahan"), typeof(DateTime), 20, HorizontalAlign.Center));

            if (Kdtans == "101")
            {
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nokontrak=No Kontrak"), typeof(string), 30, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai=Nilai Kontrak"), typeof(decimal), 25, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmdana=Sumber Dana"), typeof(string), 20, HorizontalAlign.Center));
            }

            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmbukti=Jenis Bukti"), typeof(string), 25, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Uraiba=Uraian " + nama), typeof(string), 50, HorizontalAlign.Left));
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
            Tglba = Tglsa;
            Tglbap = Tglsa;
        }
        public new IList View()
        {
            IList list = this.View(BaseDataControl.ALL);
            return list;
        }
        public new IList View(string label)
        {
            IList list = ((BaseDataControl)this).View(label);
            List<BeritaControl> ListData = new List<BeritaControl>();
            foreach (BeritaControl dc in list)
            {
                dc.Valid = (dc.Tglvalid != new DateTime());
                ListData.Add(dc);
            }
            return ListData;
        }
        public override HashTableofParameterRow GetEntries()
        {
            bool enable = !Valid, enableValid = false, enableBast = false, enablaJanjikontrak = false; //, enableEdit = true;

            if (Kdtans == "101")
            {
                enableBast = true;
            }

            if ((Kdtans == "116") || (Kdtans == "117"))
            {
                enablaJanjikontrak = true;
            }

            BeritaControl cBeritaCekjmlbrg = new BeritaControl();
            cBeritaCekjmlbrg.Unitkey = Unitkey;
            cBeritaCekjmlbrg.Noba = Noba;
            cBeritaCekjmlbrg.Load("Jmlbrg");
            Jmlbrg = cBeritaCekjmlbrg.Jmlbrg;

            BeritaControl cBeritaCekjmlgenerated = new BeritaControl();
            cBeritaCekjmlgenerated.Unitkey = Unitkey;
            cBeritaCekjmlgenerated.Noba = Noba;
            cBeritaCekjmlgenerated.Load("Jmlgenerated");
            Jmlgenerated = cBeritaCekjmlgenerated.Jmlgenerated;

            if (Jmlbrg != 0 && (Jmlbrg - Jmlgenerated) == 0) //cek jumlah barang untuk validasi
            {
                enableValid = true;
            }

            HashTableofParameterRow hpars = new HashTableofParameterRow();
            hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdtans=Jenis Transaksi"),
                 GetList(new JtransJanjikontrakLookupControl()), "Kdtans=Nmtrans", 50).SetEnable(enable).SetEditable(false).SetAllowEmpty(false).SetVisible(enablaJanjikontrak));
            hpars.Add(KontrakLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(enable).SetEditable(false).SetAllowRefresh(true)
              .SetAllowEmpty(false).SetVisible(enableBast));
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Noba=No BAST"), false, 50).SetEnable(enable).SetAllowEmpty(false));
            hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglba=Tanggal BAST"), true).SetEnable(enable).SetAllowEmpty(false));
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nobap=No BAP"), true, 50).SetEnable(enable));
            hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglbap=Tanggal BAP"), true).SetEnable(enable));
            hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kddana=Sumber Dana"),
              GetList(new JdanaLookupControl()), "Kddana=Nmdana", 50).SetEnable(enable).SetEditable(false)
              .SetAllowEmpty(false).SetVisible(enableBast));

            if (Kdtans == "101")
            {
                hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdbukti=Jenis Bukti"),
                GetList(new JbuktiBastLookupControl()), "Kdbukti=Nmbukti", 50).SetEnable(true).SetEditable(false).SetAllowEmpty(false));
            }
            else if (Kdtans == "103")
            {
                hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdbukti=Jenis Bukti"),
                GetList(new JbuktiTukarLookupControl()), "Kdbukti=Nmbukti", 50).SetEnable(true).SetEditable(false).SetAllowEmpty(false));
            }
            else if (Kdtans == "124")
            {
                hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdbukti=Jenis Bukti"),
                GetList(new JbuktiPembatalanLookupControl()), "Kdbukti=Nmbukti", 50).SetEnable(true).SetEditable(false).SetAllowEmpty(false));
            }
            else if ((Kdtans == "116") || (Kdtans == "117"))
            {
                hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdbukti=Jenis Bukti"),
              GetList(new JbuktiJanjikontrakLookupControl()), "Kdbukti=Nmbukti", 50).SetEnable(true).SetEditable(false).SetAllowEmpty(false));
            }
            else
            {
                hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdbukti=Jenis Bukti"),
                GetList(new JbuktiLainnyaLookupControl()), "Kdbukti=Nmbukti", 50).SetEnable(true).SetEditable(false));
            }

            hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Uraiba=Uraian BAST"), true, 3).SetEnable(enable).SetAllowEmpty(true));
            hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglvalid=Tanggal Valid"), true).SetEnable(enableValid).SetAllowEmpty(false).SetEditable(enable));
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
            PemdaControl cPemda = new PemdaControl();
            cPemda.Configid = "cur_thang";
            cPemda.Load("PK");

            if (Tglba.Year.ToString().Trim() != cPemda.Configval.Trim())
            {
                throw new Exception("Gagal menyimpan data : tanggal BAST hanya untuk tahun anggaran berjalan");
            }
            if (Tglbap.Year.ToString().Trim() != cPemda.Configval.Trim())
            {
                throw new Exception("Gagal menyimpan data : tanggal BAP hanya untuk tahun anggaran berjalan");
            }
            if (Tglbap > Tglba)
            {
                throw new Exception("Gagal menyimpan data : tanggal BAP tidak boleh melebihi tanggal BAST");
            }

            try
            {
                base.Insert();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Violation of PRIMARY KEY"))
                {
                    string msg = "Gagal menambah data : nomor BAST sudah digunakan";
                    msg = string.Format(msg);
                    throw new Exception(msg);
                }
                else if (ex.Message.Contains("FK_ASET_BERITA_KONTRAK"))
                {
                    string msg = "Gagal menambah data : entri BAST harus memilih kontrak";
                    msg = string.Format(msg, Nmunit);
                    throw new Exception(msg);
                }
                else if (ex.Message.Contains("FK_ASET_BERITA_JDANA"))
                {
                    string msg = "Gagal menambah data : entri BAST harus memilih sumber dana";
                    msg = string.Format(msg, Nmunit);
                    throw new Exception(msg);
                }
                else if (ex.Message.Contains("FK_ASET_BERITA_JBUKTI"))
                {
                    string msg = "Gagal menambah data : entri BAST harus memilih jenis bukti";
                    msg = string.Format(msg, Nmunit);
                    throw new Exception(msg);
                }
            }
        }
        public new int Update()
        {
            PemdaControl cPemda = new PemdaControl();
            cPemda.Configid = "cur_thang";
            cPemda.Load("PK");

            int n = 0;

            if (IsValid())
            {
                if (Tglba.Year.ToString().Trim() != cPemda.Configval.Trim() || Tglbap.Year.ToString().Trim() != cPemda.Configval.Trim())
                {
                    throw new Exception("Gagal mengubah data : Tanggal BAP / BAST hanya untuk tahun anggaran berjalan");
                }
                else
                {
                    n = ((BaseDataControlUI)this).Update();

                    if (Valid) //ceklis valid
                    {
                        if (Tglvalid < Tglba)
                        {
                            throw new Exception("Gagal mengubah data : Tanggal pengesahan tidak boleh mendahului tanggal BAST");
                        }
                        if (Tglvalid.Year.ToString().Trim() != cPemda.Configval.Trim())
                        {
                            throw new Exception("Gagal mengubah data : Tahun pengesahan BAST harus sama dengan tahun pengajuan BAST");
                        }

                        BeritaControl cBeritaGetjmlkib = new BeritaControl();
                        cBeritaGetjmlkib.Unitkey = Unitkey;
                        cBeritaGetjmlkib.Noba = Noba;
                        cBeritaGetjmlkib.Load("Jmlkib");

                        if (cBeritaGetjmlkib.Jmlkib >= 1)
                        {
                            throw new Exception("Gagal mengubah data : BAST ini sudah di sahkan dan masuk ke KIB");
                        }
                        else
                        {

                            if (Kdtans == "112") //transaksi penambahan termin KDP hanya update tglvalid
                            {
                                Tglvalid = Tglvalid;
                                base.Update("Sah");
                            }
                            else
                            {
                                string sql = @"
              exec [dbo].[WSP_VALBERITA]
              @UNITKEY = N'{0}',
              @NOBA = N'{1}',
              @TGLBA = N'{2}',
              @TGLVALID = N'{3}',
              @KDTANS = N'{4}'   
              ";
                                sql = string.Format(sql, Unitkey, Noba, Tglba.ToString("yyyy-MM-dd"), Tglvalid.ToString("yyyy-MM-dd"), Kdtans);
                                BaseDataAdapter.ExecuteCmd(this, sql);
                            }
                        }
                    }
                }
            }
            return n;
        }
        public new int Delete()
        {
            BeritaControl cBeritaGetjmlbrgpengguna = new BeritaControl();
            cBeritaGetjmlbrgpengguna.Unitkey = Unitkey;
            cBeritaGetjmlbrgpengguna.Noba = Noba;
            cBeritaGetjmlbrgpengguna.Load("Jmlbrgpengguna");

            int n = 0;
            if (Valid)
            {
                if (cBeritaGetjmlbrgpengguna.Jmlbrgpengguna >= 1)
                {
                    throw new Exception("Rincian barang BAST sudah digunakan di penetapan SK pengguna, pengesahan tidak dapat dicabut");
                }
                else
                {
                    base.Update("Kib");
                    base.Update("Draft");
                }
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
                        string msg = "Hapus rincian rekening terlebih dahulu";
                        msg = string.Format(msg);
                        throw new Exception(msg);
                    }
                }
            }
            return n;
        }
        #endregion Methods 
    }
    #endregion Berita


}

