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
    #region Usadi.Valid49.BO.HapusskControl, Usadi.Valid49.Aset.MAT
    [Serializable]
    public class HapusskControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
    {
        #region Properties
        public long Id { get; set; }
        public DateTime Tglskhapus { get; set; }
        public new string Ket { get; set; }
        public DateTime Tglvalid { get; set; }
        public string Kdunit { get; set; }
        public string Nmunit { get; set; }
        public int Jmldata { get; set; }
        public int Jmlkib { get; set; }
        public int Tahunsa { get; set; }
        public DateTime Tglsa { get; set; }
        public string Unitkey { get; set; }
        public string Noskhapus { get; set; }
        public string Kdtans { get; set; }
        public string Nmtrans { get; set; }
        public int Statusdok { get; set; }
        public string Statusdokumen { get; set; }
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
                    CommandName = "ViewHapusskdet",
                    Icon = Icon.PageCopy
                };
                cmd1.ToolTip.Text = "Klik Untuk Menampilkan Rincian Barang";
                return new ImageCommand[] { cmd1 };
            }
        }

        public string ViewHapusskdet
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
                return "No. Penghapusan " + Noskhapus + ":" + url;
            }
        }
        #endregion Properties 

        #region Methods 
        public HapusskControl()
        {
            XMLName = ConstantTablesAsetMAT.XMLHAPUSSK;
        }
        public new IProperties GetProperties()
        {
            ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
            cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
            cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Noskhapus" };
            cViewListProperties.IDKey = "Id";
            cViewListProperties.IDProperty = "Id";
            cViewListProperties.ReadOnlyFields = new String[] { "Unitkey" };
            cViewListProperties.SortFields = new String[] { "Tglskhapus", "Noskhapus" };
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

            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noskhapus=Nomor SK Penghapusan"), typeof(string), 30, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglskhapus=Tanggal SK Penghapusan"), typeof(DateTime), 25, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglvalid=Tanggal Pengesahan"), typeof(DateTime), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmtrans= Jenis Penghapusan"), typeof(string), 40, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Statusdokumen=Status"), typeof(string), 15, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 70, HorizontalAlign.Left));

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
            PemdaControl cPemda = new PemdaControl();
            cPemda.Configid = "cur_thang";
            cPemda.Load("PK");

            Tahunsa = (Int32.Parse(cPemda.Configval));
            Tglsa = new DateTime(Tahunsa, DateTime.Today.Month, DateTime.Today.Day);
            Tglskhapus = Tglsa;
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
            List<HapusskControl> ListData = new List<HapusskControl>();
            foreach (HapusskControl dc in list)
            {
                dc.Valid = (dc.Tglvalid != new DateTime());
                ListData.Add(dc);
            }
            return ListData;
        }

        public override HashTableofParameterRow GetEntries()
        {
            bool enableValid = false;
            bool enable = !Valid;

            HapusskControl cHapusskCekdata = new HapusskControl();
            cHapusskCekdata.Unitkey = Unitkey;
            cHapusskCekdata.Noskhapus = Noskhapus;
            cHapusskCekdata.Load("Cekjmldata");

            if (cHapusskCekdata.Jmldata != 0)
            {
                enableValid = true;
            }

            HashTableofParameterRow hpars = new HashTableofParameterRow();
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Noskhapus=Nomor SK"), false, 50).SetEnable(enable));
            hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglskhapus=Tanggal SK"), true).SetEnable(enable));
            hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdtans=Jenis Penghapusan"),
            GetList(new JtransHapusskLookupControl()), "Kdtans=Nmtrans", 54).SetEnable(enable));
            hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Ket=Keterangan"), true, 90).SetEnable(enable).SetAllowEmpty(true));
            ArrayList list = new ArrayList(new ParamControl[] {
        new ParamControl() { Kdpar="1",Nmpar="Diterima "}
        ,new ParamControl() { Kdpar="2",Nmpar="Ditolak "}
      });
            hpars.Add(new ParameterRow(ConstantDict.GetColumnTitleEntry("Statusdok=Status"), ParameterRow.MODE_SELECT,
              list, "Kdpar=Nmpar", 30).SetAllowRefresh(true).SetEnable(enableValid).SetEditable(enable));
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

            if (Tglskhapus.Year.ToString().Trim() != cPemda.Configval.Trim())
            {
                throw new Exception("Gagal menyimpan data : Proses penghapusan hanya untuk tahun anggaran berjalan.");
            }
            if (Kdtans == "")
            {
              throw new Exception("Gagal menyimpan data : Jenis Penghapusan Tidak Boleh Kosong");
            }
            base.Insert();
        }
        public new int Update()
        {
            PemdaControl cPemda = new PemdaControl();
            cPemda.Configid = "cur_thang";
            cPemda.Load("PK");

            int n = 0;
            if (IsValid())
            {
                if (Tglskhapus.Year.ToString().Trim() != cPemda.Configval.Trim())
                {
                    throw new Exception("Transaksi penghapusan hanya untuk tahun anggaran berjalan, cek kembali tanggal SK");
                }
                else
                {
                    n = ((BaseDataControlUI)this).Update();

                    if (Valid)
                    {
                        HapusskControl cHapusskkib = new HapusskControl();
                        cHapusskkib.Unitkey = Unitkey;
                        cHapusskkib.Noskhapus = Noskhapus;
                        cHapusskkib.Load("Jmlkib");

                        if (cHapusskkib.Jmlkib >= 1)
                        {
                            throw new Exception("Gagal mengubah data : Dokumen penghapusan ini sudah di sahkan dan masuk ke KIB");
                        }
                        else
                        {
                            string sql = @"
              exec [dbo].[WSPX_HAPUSSK]
              @UNITKEY = N'{0}',
              @NOSKHAPUS = N'{1}',
              @TGLSKHAPUS = N'{2}',
                 @KDTANS = N'{3}'
            ";
                            sql = string.Format(sql, Unitkey, Noskhapus, Tglskhapus.ToString("yyyy-MM-dd"),Kdtans);
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

            HapusskControl cHapusskkib = new HapusskControl();
            cHapusskkib.Unitkey = Unitkey;
            cHapusskkib.Noskhapus = Noskhapus;
            cHapusskkib.Load("Jmlkib");
            Jmlkib = cHapusskkib.Jmlkib;

            cekjmlkib = (Jmlkib >= 1);

            int n = 0;
            if (Valid)
            {
                if (cekjmlkib == true)
                {
                    throw new Exception("Transaksi penghapusan sudah masuk ke KIB, pengesahan tidak dapat dicabut.");
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
        #endregion Methods 
    }
    #endregion Hapussk

    #region HapusskSebagian
    [Serializable]
    public class HapusskSebagianControl : HapusskControl, IDataControlUIEntry, IHasJSScript
    {
        #region Methods 
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

            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noskhapus=Nomor SK Penghapusan"), typeof(string), 30, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglskhapus=Tanggal SK Penghapusan"), typeof(DateTime), 25, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglvalid=Tanggal Pengesahan"), typeof(DateTime), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmtrans= Jenis Penghapusan"), typeof(string), 40, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Statusdokumen=Status"), typeof(string), 15, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 70, HorizontalAlign.Left));

            return columns;
        }
        #endregion Methods 
    }
    #endregion HapusskSebagian
}

