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
    #region Usadi.Valid49.BO.HistrenovControl, Usadi.Valid49.Aset.MAT
    [Serializable]
    public class HistrenovControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
    {
        #region Properties
        public long Id { get; set; }
        public DateTime Tglbarenov { get; set; }
        public string Tglbalookup { get { return Tglbarenov.ToString("dd/MM/yyyy"); } }
        public string Kdkegunit { get; set; }
        public string Kdtahap { get; set; }
        public string Idprgrm { get; set; }
        public string Nokontrak { get; set; }
        public string Uraibarenov { get; set; }
        public string Kdbukti { get; set; }
        public string Kdtans { get; set; }
        public DateTime Tglvalid { get; set; }
        public string Kdunit { get; set; }
        public string Nmunit { get; set; }
        public string Nukeg { get; set; }
        public string Nmkegunit { get; set; }
        public string Nmprgrm { get; set; }
        public string Uraian { get; set; }
        public string Kdp3 { get; set; }
        public string Nminst { get; set; }
        public decimal Nilai { get; set; }
        public string Nmbukti { get; set; }
        public string Nmtrans { get; set; }
        public int Jmlbast { get; set; }
        public int Jmlbrg { get; set; }
        public int Jmlkib { get; set; }
        public int Tahunsa { get; set; }
        public DateTime Tglsa { get; set; }
        public string Unitkey { get; set; }
        public string Nobarenov { get; set; }
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
                    CommandName = "ViewHistrenovdet",
                    Icon = Icon.PageCopy
                };
                cmd1.ToolTip.Text = "Klik Untuk Menampilkan Rincian Rekening";
                return new ImageCommand[] { cmd1 };
            }
        }
        public string ViewHistrenovdet
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
                return "Rincian Rekening; " + Nobarenov + "- Nilai Kontrak Rp. " + Nilai.ToString("#,##0") + ":" + url;
            }
        }
        #endregion Properties 

        #region Methods 
        public HistrenovControl()
        {
            XMLName = ConstantTablesAsetMAT.XMLHISTRENOV;
        }
        public new IProperties GetProperties()
        {
            ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
            cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
            cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Nobarenov" };
            cViewListProperties.IDKey = "Id";
            cViewListProperties.IDProperty = "Id";
            cViewListProperties.ReadOnlyFields = new String[] { "Unitkey", "Kdunit", "Nmunit", "Kdtahap", "Kdkegunit", "Nukeg", "Nmkegunit" };
            cViewListProperties.SortFields = new String[] { "Tglbarenov", "Nobarenov" };
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

            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nobarenov=No BAST"), typeof(string), 30, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglbarenov=Tanggal BAST"), typeof(DateTime), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglvalid=Tanggal Pengesahan"), typeof(DateTime), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nokontrak=No Kontrak"), typeof(string), 30, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai=Nilai Kontrak"), typeof(decimal), 25, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmbukti=Jenis Bukti"), typeof(string), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmprgrm=Program"), typeof(string), 50, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmkegunit=Kegiatan"), typeof(string), 50, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Uraibarenov=Uraian BAST"), typeof(string), 50, HorizontalAlign.Left));
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
        public new IList View()
        {
            IList list = this.View(BaseDataControl.ALL);
            return list;
        }
        public new IList View(string label)
        {
            IList list = ((BaseDataControl)this).View(label);
            List<HistrenovControl> ListData = new List<HistrenovControl>();
            foreach (HistrenovControl dc in list)
            {
                dc.Valid = (dc.Tglvalid != new DateTime());
                ListData.Add(dc);
            }

            return ListData;
        }
        public new void SetPrimaryKey()
        {
            PemdaControl cPemda = new PemdaControl();
            cPemda.Configid = "cur_thang";
            cPemda.Load("PK");

            KegunitControl cKegunit = new KegunitControl();
            cKegunit.Unitkey = Unitkey;
            cKegunit.Kdkegunit = Kdkegunit;
            cKegunit.Kdtahap = Kdtahap;
            cKegunit.Thang = cPemda.Configval.Trim();
            cKegunit.Load("PK");

            Idprgrm = cKegunit.Idprgrm.Trim();
            Tahunsa = (Int32.Parse(cPemda.Configval));
            Tglsa = new DateTime(Tahunsa, DateTime.Today.Month, DateTime.Today.Day);
            Tglbarenov = Tglsa;
        }
        public new HashTableofParameterRow GetFilters()
        {
            bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
            HashTableofParameterRow hpars = new HashTableofParameterRow();
            hpars.Add(DaftunitLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetAllowEmpty(false));
            hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdtahap=Tahap"),
            GetList(new TahapLookupControl()), "Kdtahap=Uraian", 41).SetAllowRefresh(true).SetEnable(enableFilter).SetAllowEmpty(false));
            hpars.Add(KegunitLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(false).SetAllowEmpty(false));
            return hpars;
        }
        public new void Insert()
        {
            PemdaControl cPemda = new PemdaControl();
            cPemda.Configid = "cur_thang";
            cPemda.Load("PK");

            if (Tglbarenov.Year.ToString().Trim() != cPemda.Configval.Trim())
            {
                throw new Exception("Gagal menyimpan data : tanggal BAST hanya untuk tahun anggaran berjalan");
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
                else if (ex.Message.Contains("FK_ASET_BERITA_JBUKTI"))
                {
                    string msg = "Gagal menambah data : entri BAST harus memilih jenis bukti";
                    msg = string.Format(msg, Nmunit);
                    throw new Exception(msg);
                }
            }
        }
        private bool IsValid()
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
            if (IsValid())
            {
                if (Tglbarenov.Year.ToString().Trim() != cPemda.Configval.Trim())
                {
                    throw new Exception("Gagal mengubah data : Tanggal BAST hanya untuk tahun anggaran berjalan");
                }
                else
                {
                    n = ((BaseDataControlUI)this).Update();

                    if (Valid)
                    {
                        HistrenovControl cHistrenovCekjmlkib = new HistrenovControl();
                        cHistrenovCekjmlkib.Unitkey = Unitkey;
                        cHistrenovCekjmlkib.Nobarenov = Nobarenov;
                        cHistrenovCekjmlkib.Kdtans = Kdtans;
                        cHistrenovCekjmlkib.Load("Jmlkib");

                        if (cHistrenovCekjmlkib.Jmlkib >= 1)
                        {
                            throw new Exception("Gagal mengubah data : BAST ini sudah di sahkan dan masuk ke KIB");
                        }
                        else
                        {
                            string sql = @"
            exec [dbo].[WSP_VALHISTRENOV]
            @UNITKEY = N'{0}',
            @NOBARENOV = N'{1}',
            @TGLBARENOV = N'{2}',
            @KDTANS = N'{3}'
            ";
                            sql = string.Format(sql, Unitkey, Nobarenov, Tglbarenov.ToString("yyyy-MM-dd"), Kdtans);
                            BaseDataAdapter.ExecuteCmd(this, sql);
                        }
                    }
                }
            }
            return n;
        }
        public new int Delete()
        {
            //bool cekjmlkib = false;

            //HistrenovControl cHistrenovCekjmlkib = new HistrenovControl();
            //cHistrenovCekjmlkib.Unitkey = Unitkey;
            //cHistrenovCekjmlkib.Nobarenov = Nobarenov;
            //cHistrenovCekjmlkib.Kdtans = Kdtans;
            //cHistrenovCekjmlkib.Load("Jmlkib");
            //Jmlkib = cHistrenovCekjmlkib.Jmlkib;

            //cekjmlkib = (Jmlkib >= 1);

            int n = 0;
            if (Valid)
            {
                //if (cekjmlkib == true)
                //{
                //  throw new Exception("BAST sudah digunakan di KIB, pengesahan tidak dapat dicabut");
                //}
                string sql = @"
              exec [dbo].[WSP_DEL_HISTRENOV]
              @UNITKEY = N'{0}',
              @NOBARENOV = N'{1}',
              @KDTANS = N'{2}'
              ";

                sql = string.Format(sql, Unitkey, Nobarenov, Kdtans);
                BaseDataAdapter.ExecuteCmd(this, sql);

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

            HistrenovControl cHistrenovCekjmlbast = new HistrenovControl();
            cHistrenovCekjmlbast.Unitkey = Unitkey;
            cHistrenovCekjmlbast.Nobarenov = Nobarenov;
            cHistrenovCekjmlbast.Load("Jmlbast");
            Jmlbast = cHistrenovCekjmlbast.Jmlbast;

            HistrenovControl cHistrenovCekjmlbrg = new HistrenovControl();
            cHistrenovCekjmlbrg.Unitkey = Unitkey;
            cHistrenovCekjmlbrg.Nobarenov = Nobarenov;
            cHistrenovCekjmlbrg.Load("Jmlbrg");
            Jmlbrg = cHistrenovCekjmlbrg.Jmlbrg;

            if (Jmlbrg != 0 && Jmlbast != 0)
            {
                enableValid = true;
            }

            HashTableofParameterRow hpars = new HashTableofParameterRow();
            hpars.Add(KontrakLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(enable).SetEditable(false).SetAllowRefresh(true)
              .SetAllowEmpty(false));
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nobarenov=No BAST"), false, 50).SetEnable(enable).SetAllowEmpty(false));
            hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglbarenov=Tanggal BAST"), true).SetEnable(enable).SetAllowEmpty(false));
            hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdbukti=Jenis Bukti"),
              GetList(new JbuktiBastLookupControl()), "Kdbukti=Nmbukti", 50).SetEnable(enable).SetEditable(false)
              .SetAllowEmpty(false));
            hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Uraibarenov=Uraian BAST"), true, 3).SetEnable(enable).SetAllowEmpty(true));
            hpars.Add(new ParameterRowCek(this, true).SetEnable(enableValid).SetEditable(enable));
            return hpars;
        }

        #endregion Methods 
    }
    #endregion Histrenov

    #region Histrenov104
    [Serializable]
    public class Histrenov104Control : HistrenovControl, IDataControlUIEntry, IHasJSScript
    {
        #region Methods 
        public new void SetPageKey()
        {
            Kdtans = "104";
        }
        #endregion Methods 
    }
    #endregion Histrenov104

    #region Histrenov109
    [Serializable]
    public class Histrenov109Control : HistrenovControl, IDataControlUIEntry, IHasJSScript
    {
        #region Methods 
        public new void SetPageKey()
        {
            Kdtans = "109";
        }
        #endregion Methods 
    }
    #endregion Histrenov109
}

