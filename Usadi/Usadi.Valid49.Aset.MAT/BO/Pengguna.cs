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
    #region Usadi.Valid49.BO.PenggunaControl, Usadi.Valid49.Aset.MAT
    [Serializable]
    public class PenggunaControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
    {
        #region Properties
        public long Id { get; set; }
        public string Kdpengguna { get; set; }
        public string Unitkeypengguna { get; set; }
        public DateTime Tgldokpengguna { get; set; }
        public DateTime Tglmulai { get; set; }
        public DateTime Tglselesai { get; set; }
        public DateTime Tglvalid { get; set; }
        public new string Ket { get; set; }
        public string Kdunit { get; set; }
        public string Nmunit { get; set; }
        public string Kdunitpengguna { get; set; }
        public string Nmunitpengguna { get; set; }
        public string Kdnmunitpengguna { get; set; }
        public string Nmtrans { get; set; }
        public string Nmpengguna { get; set; }
        public string Statuspenggunaan { get; set; }
        public string Nminst { get; set; }
        public int Jmlbrg { get; set; }
        public int Tahunsa { get; set; }
        public DateTime Tglsa { get; set; }
        public string Unitkey { get; set; }
        public string Nodokpengguna { get; set; }
        public string Kdtans { get; set; }
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
                    CommandName = "ViewPenggunadet",
                    Icon = Icon.PageCopy
                };
                cmd1.ToolTip.Text = "Klik Untuk Menampilkan Rincian Kemitraan";
                return new ImageCommand[] { cmd1 };
            }
        }
        public string ViewPenggunadet
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
                return "No. Dokumen; " + Nodokpengguna + ":" + url;
            }
        }
        #endregion Properties 

        #region Methods 
        public PenggunaControl()
        {
            XMLName = ConstantTablesAsetMAT.XMLPENGGUNA;
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
            cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Nodokpengguna", "Kdtans" };
            cViewListProperties.IDKey = "Id";
            cViewListProperties.IDProperty = "Id";
            #region COR-49 Refactor Get Tahun dari Setting DB - STEP 2
            cViewListProperties.ReadOnlyFields = new String[] { "Unitkey", "Tahunsa", "Tahun" };
            #endregion
            cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
            cViewListProperties.PageSize = 30;

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
            bool enable = true, enableSkpd = false, enablePhklain = true;

            if (Blokid == "1")
            {
                enable = false;
            }

            if (Kdtans == "312")
            {
                enableSkpd = true;
                enablePhklain = false;
            }

            DataControlFieldCollection columns = new DataControlFieldCollection();
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center).SetVisible(enable));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Stricon"), typeof(CommandColumn), Cmds, 5, HorizontalAlign.Center));

            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nodokpengguna=Nomor Dokumen"), typeof(string), 30, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdnmunitpengguna=SKPD Pengguna"), typeof(string), 50, HorizontalAlign.Left).SetVisible(enableSkpd));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmpengguna=Nama Pengguna"), typeof(string), 50, HorizontalAlign.Left).SetVisible(enablePhklain));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nminst=Instansi"), typeof(string), 50, HorizontalAlign.Left).SetVisible(enablePhklain));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Statuspenggunaan=Status"), typeof(string), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tgldokpengguna=Tanggal Dokumen"), typeof(DateTime), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglmulai=Tanggal Mulai"), typeof(DateTime), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglselesai=Tanggal Selesai"), typeof(DateTime), 20, HorizontalAlign.Center));
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
            Tgldokpengguna = Tglsa;
            Tglmulai = Tglsa;
            Tglselesai = Tglsa;
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
            List<PenggunaControl> ListData = new List<PenggunaControl>();
            foreach (PenggunaControl dc in list)
            {
                dc.Valid = (dc.Tglvalid != new DateTime());
                ListData.Add(dc);
            }

            return ListData;
        }

        public new void Insert()
        {
            if (IsValidInsert())
            {
                base.Insert();
            }
            else
            {
                throw new Exception(ConstantDict.Translate("LBL_INVALID_INSERT"));
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
                throw new Exception($"Data dengan No Dokumen = {Nodokpengguna} sdh ada");
            }
        }
        private bool IsValidUpdate()
        {
            bool valid = true;
            return valid;
        }

        public new int Update()
        {
            int n = 0;
            if (IsValidUpdate())
            {
                n = ((BaseDataControlUI)this).Update();

                if (Valid)
                {
                    Tglvalid = Tglselesai;
                    base.Update("Sah");
                }
            }
            return n;
        }
        public new int Delete()
        {
            int n = 0;
            if (Valid)
            {
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
            bool enable = !Valid, enableValid = false, enableUnit = false, enablePengguna = true;

            if (Kdtans == "312")
            {
                enableUnit = true;
                enablePengguna = false;
            }
            else
            {
                enableUnit = false;
                enablePengguna = true;
            }

            PenggunaControl cPenggunaGetjmlbrg = new PenggunaControl();
            cPenggunaGetjmlbrg.Unitkey = Unitkey;
            cPenggunaGetjmlbrg.Nodokpengguna = Nodokpengguna;
            cPenggunaGetjmlbrg.Kdtans = Kdtans;
            cPenggunaGetjmlbrg.Load("Jmlbrg");

            if (cPenggunaGetjmlbrg.Jmlbrg != 0)
            {
                enableValid = true;
            }


            HashTableofParameterRow hpars = new HashTableofParameterRow();
            hpars.Add(DaftunitPenggunaLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enable)
              .SetEditable(false).SetVisible(enableUnit));
            hpars.Add(DaftpenggunaLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enable)
              .SetEditable(false).SetVisible(enablePengguna));
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nodokpengguna=Nomor Dokumen"), false, 50).SetEnable(enable));
            hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tgldokpengguna=Tgl Dokumen"), true).SetEnable(enable));
            hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglmulai=Tanggal Mulai"), true).SetEnable(enable));
            hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglselesai=Tanggal Selesai"), true).SetEnable(enable));
            hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Ket=Keterangan"), true, 3).SetEnable(enable).SetAllowEmpty(true));
            hpars.Add(new ParameterRowCek(this, true).SetEnable(enableValid).SetEditable(enable));

            return hpars;
        }
        #endregion Methods 
    }
    #endregion Pengguna

    #region PenggunaSementara
    [Serializable]
    public class PenggunaSementaraControl : PenggunaControl, IDataControlUIEntry
    {
        public new void SetPageKey()
        {
            Kdtans = "312";
            Kdpengguna = null;
        }
        public new void Insert()
        {
            if (Unitkeypengguna == Unitkey)
            {
                throw new Exception("SKPD pengguna tidak boleh sama dengan SKPD pemilik");
            }

            base.Insert();
        }
    }
    #endregion PenggunaSementara

    #region PenggunaPihaklain
    [Serializable]
    public class PenggunaPihaklainControl : PenggunaControl, IDataControlUIEntry
    {
        public new void SetPageKey()
        {
            Kdtans = "313";
            Unitkeypengguna = null;
        }
    }
    #endregion PenggunaPihaklain

    #region PenerimaanBmdInternal
    [Serializable]
    public class PenerimaanBmdInternalControl : PenggunaControl, IDataControlUIEntry
    {
        public new void SetPageKey()
        {
            Kdtans = "126";
            Unitkeypengguna = null;
        }
    }
    #endregion PenerimaanBmdInternal

    #region PengeluaranBmdInternal
    [Serializable]
    public class PengeluaranBmdInternalControl : PenggunaControl, IDataControlUIEntry
    {
        public new void SetPageKey()
        {
            Kdtans = "127";
            Unitkeypengguna = null;
        }
    }
    #endregion PengeluaranBmdInternal

}

