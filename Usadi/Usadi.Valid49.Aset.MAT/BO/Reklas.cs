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
    #region Usadi.Valid49.BO.ReklasControl, Usadi.Valid49.Aset.MAT
    [Serializable]
    public class ReklasControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
    {
        #region Properties
        public long Id { get; set; }
        public DateTime Tglreklas { get; set; }
        public DateTime Tglvalid { get; set; }
        public new string Ket { get; set; }
        public string Kdunit { get; set; }
        public string Nmunit { get; set; }
        public string Nmtrans { get; set; }
        public int Jmldata { get; set; }
        public int Jmlasetkeynull { get; set; }
        public int Jmlkib { get; set; }
        public int Tahunsa { get; set; }
        public DateTime Tglsa { get; set; }
        public string Kdtans { get; set; }
        public string Noreklas { get; set; }
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
                    CommandName = "ViewReklasdet",
                    Icon = Icon.PageCopy
                };
                cmd1.ToolTip.Text = "Klik Untuk Menampilkan Rincian Reklas";
                return new ImageCommand[] { cmd1 };
            }
        }
        public string ViewReklasdet
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
                return "No. BA Reklas; " + Noreklas + ":" + url;
            }
        }
        #endregion Properties 

        #region Methods 
        public ReklasControl()
        {
            XMLName = ConstantTablesAsetMAT.XMLREKLAS;
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
            cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Noreklas", "Kdtans" };
            cViewListProperties.IDKey = "Id";
            cViewListProperties.IDProperty = "Id";
            #region COR-49 Refactor Get Tahun dari Setting DB - STEP 2
            cViewListProperties.ReadOnlyFields = new String[] { "Unitkey", "Tahunsa", "Tahun" };
            #endregion
            cViewListProperties.SortFields = new String[] { "Tglreklas", "Noreklas" };
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

            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreklas=Nomor Dokumen"), typeof(string), 40, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglreklas=Tanggal Dokumen"), typeof(DateTime), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglvalid=Tanggal Pengesahan"), typeof(DateTime), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 50, HorizontalAlign.Left));

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
            Tglreklas = Tglsa;
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
            List<ReklasControl> ListData = new List<ReklasControl>();
            foreach (ReklasControl dc in list)
            {
                dc.Valid = (dc.Tglvalid != new DateTime());
                ListData.Add(dc);
            }

            return ListData;
        }
        public override HashTableofParameterRow GetEntries()
        {
            bool enableValid = false, enable = !Valid;

            ReklasControl cReklasGetjmldata = new ReklasControl();
            cReklasGetjmldata.Unitkey = Unitkey;
            cReklasGetjmldata.Noreklas = Noreklas;
            cReklasGetjmldata.Kdtans = Kdtans;
            cReklasGetjmldata.Load("Jmldata");

            ReklasControl cReklasGetjmlasetkeynull = new ReklasControl();
            cReklasGetjmlasetkeynull.Unitkey = Unitkey;
            cReklasGetjmlasetkeynull.Noreklas = Noreklas;
            cReklasGetjmlasetkeynull.Kdtans = Kdtans;
            cReklasGetjmlasetkeynull.Load("Jmlasetkeynull");

            if (cReklasGetjmldata.Jmldata != 0 && cReklasGetjmlasetkeynull.Jmlasetkeynull == 0)
                //if (cReklasGetjmldata.Jmldata != 0)
            {
                enableValid = true;
            }

            HashTableofParameterRow hpars = new HashTableofParameterRow();
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Noreklas=Nomor Dokumen"), false, 50).SetEnable(enable));
            hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglreklas=Tgl Dokumen"), true).SetEnable(enable));
            hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Ket=Keterangan"), true, 3).SetEnable(enable).SetAllowEmpty(true));
            hpars.Add(new ParameterRowCek(this, true).SetEnable(enableValid).SetEditable(enable));
            //hpars.Add(new ParameterRowCek(this, true).SetEnable(enableValid));
            return hpars;
        }
        public new void Insert()
        {
            if (IsValidInsert())
            {
                if (Tglreklas.Year != Tahunsa)
                {
                    throw new Exception("Gagal menyimpan data : Transaksi reklas hanya untuk tahun anggaran berjalan, cek kembali tanggal reklas");
                }
                base.Insert();
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
                throw new Exception($"Data dengan No Dokumen = {Noreklas} sdh ada");
            }
        }
        private bool IsValidUpdate()
        {
            bool valid = true;
            return valid;
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
                if (Tglreklas.Year.ToString().Trim() != cPemda.Configval.Trim())
                {
                    throw new Exception("Gagal mengubah data : tanggal reklas hanya untuk tahun anggaran berjalan");
                }
                else
                {
                    n = ((BaseDataControlUI)this).Update();

                    if (Valid)
                    {
                        ReklasControl cReklascekkib = new ReklasControl();
                        cReklascekkib.Unitkey = Unitkey;
                        cReklascekkib.Noreklas = Noreklas;
                        cReklascekkib.Kdtans = Kdtans;
                        cReklascekkib.Load("Jmlkib");

                        if (cReklascekkib.Jmlkib >= 1)
                        {
                            throw new Exception("Gagal mengubah data : Dokumen reklas ini sudah di sahkan dan masuk ke KIB");
                        }
                        else
                        {
                            if (Kdtans == "215") // Reklas KDP (Keluar)
                            {
                                string sql = @"
                exec [dbo].[WSPX_REKLASKDP]
                @UNITKEY = N'{0}',
                @NOREKLAS = N'{1}',
                @TGLREKLAS = N'{2}',
                @KDTANS = N'{3}'
                ";
                                sql = string.Format(sql, Unitkey, Noreklas, Tglreklas.ToString("yyyy-MM-dd"), Kdtans);
                                BaseDataAdapter.ExecuteCmd(this, sql);
                            }
                            else if (Kdtans == "225") // Reklas Rusak Berat (Keluar)
                            {
                                string sql = @"
                exec [dbo].[WSPX_REKLASARB]
                @UNITKEY = N'{0}',
                @NOREKLAS = N'{1}',
                @TGLREKLAS = N'{2}',
                @KDTANS = N'{3}'
                ";
                                sql = string.Format(sql, Unitkey, Noreklas, Tglreklas.ToString("yyyy-MM-dd"), Kdtans);
                                BaseDataAdapter.ExecuteCmd(this, sql);
                            }
                            else if (Kdtans == "226") // Reklas Non Operasional (Keluar)
                            {
                                string sql = @"
                exec [dbo].[WSPX_REKLASNONOP]
                @UNITKEY = N'{0}',
                @NOREKLAS = N'{1}',
                @TGLREKLAS = N'{2}',
                @KDTANS = N'{3}'
                ";
                                sql = string.Format(sql, Unitkey, Noreklas, Tglreklas.ToString("yyyy-MM-dd"), Kdtans);
                                BaseDataAdapter.ExecuteCmd(this, sql);
                            }
                            else if (Kdtans == "233") // Reklas Aset Lain-lain nya (Keluar)
                            {
                                string sql = @"
                exec [dbo].[WSPX_REKLASLAIN2]
                @UNITKEY = N'{0}',
                @NOREKLAS = N'{1}',
                @TGLREKLAS = N'{2}',
                @KDTANS = N'{3}'
                ";
                                sql = string.Format(sql, Unitkey, Noreklas, Tglreklas.ToString("yyyy-MM-dd"), Kdtans);
                                BaseDataAdapter.ExecuteCmd(this, sql);
                            }
                            else if (Kdtans == "216") // Reklas Aset Tetap Lain to aset tetap (Masuk)
                            {
                              string sql = @"
                exec [dbo].[WSPX_REKLASALAT]
                @UNITKEY = N'{0}',
                @NOREKLAS = N'{1}',
                @TGLREKLAS = N'{2}',
                @KDTANS = N'{3}'
                ";
                              sql = string.Format(sql, Unitkey, Noreklas, Tglreklas.ToString("yyyy-MM-dd"), Kdtans);
                              BaseDataAdapter.ExecuteCmd(this, sql);
                            }
              else
                            {
                                string sql = @"
                exec [dbo].[WSPX_REKLAS]
                @UNITKEY = N'{0}',
                @NOREKLAS = N'{1}',
                @TGLREKLAS = N'{2}',
                @KDTANS = N'{3}'
                ";
                                sql = string.Format(sql, Unitkey, Noreklas, Tglreklas.ToString("yyyy-MM-dd"), Kdtans);
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

          //tidak usah pake cekjml kib
            //bool cekjmlkib = false;

            //ReklasControl cReklascekkib = new ReklasControl();
            //cReklascekkib.Unitkey = Unitkey;
            //cReklascekkib.Noreklas = Noreklas;
            //cReklascekkib.Kdtans = Kdtans;
            //cReklascekkib.Load("Jmlkib");
            //Jmlkib = cReklascekkib.Jmlkib;

            //cekjmlkib = (Jmlkib >= 1);

            int n = 0;
            if (Valid)
            {
                //if (cekjmlkib == true)
                //{
                //    throw new Exception("Aset sudah di reklas, pengesahan tidak dapat dicabut.");
                //}
                string sql = @"
                      exec [dbo].[WSP_DEL_REKLAS]
                      @UNITKEY = N'{0}',
                      @NOREKLAS = N'{1}'
                      ";
                sql = string.Format(sql, Unitkey, Noreklas);
                BaseDataAdapter.ExecuteCmd(this, sql);
                return ((BaseDataControlUI)this).Update("Draft");
            }
            else
            {
                 Status = -1;
                 base.Delete();
                //try
                //{
                //    Status = -1;
                //    base.Delete();
                //}
                //catch (Exception ex)
                //{
                //    if (ex.Message.Contains("REFERENCE"))
                //    {
                //        string msg = "Hapus rincian barang terlebih dahulu";
                //        msg = string.Format(msg);
                //        throw new Exception(msg);
                //    }
                //}
            }
            return n;
        }
        #endregion Methods 
    }
    #endregion Reklas

    #region ReklasAsettetap
    [Serializable]
    public class ReklasAsettetapControl : ReklasControl, IDataControlUIEntry
    {
        public new void SetPageKey()
        {
            Kdtans = "208";
        }
    }
    #endregion ReklasAsettetap

    #region ReklasAsetLainToAsettetap
    [Serializable]
    public class ReklasAsetLainToAsettetapControl : ReklasControl, IDataControlUIEntry
    {
        public new void SetPageKey()
        {
          //Kdtans = "130";
          Kdtans = "216";
        }
    }
    #endregion ReklasAsetLainToAsettetap

    #region ReklasPersediaan
    [Serializable]
    public class ReklasPersediaanControl : ReklasControl, IDataControlUIEntry
    {
        public new void SetPageKey()
        {
            Kdtans = "209";
        }
    }
    #endregion ReklasPersediaan

    #region ReklasPersediaanToTetap
    [Serializable]
    public class ReklasPersediaanToTetapControl : ReklasControl, IDataControlUIEntry
    {
        public new void SetPageKey()
        {
            Kdtans = "129";
        }
    }
    #endregion ReklasPersediaanToTetap

    #region ReklasRusakberat
    [Serializable]
    public class ReklasRusakberatControl : ReklasControl, IDataControlUIEntry
    {
        public new void SetPageKey()
        {
            Kdtans = "225";
        }
    }
    #endregion ReklasRusakberat

    #region ReklasNonoperasional
    [Serializable]
    public class ReklasNonoperasionalControl : ReklasControl, IDataControlUIEntry
    {
        public new void SetPageKey()
        {
            Kdtans = "226";
        }
    }
    #endregion ReklasNonoperasional

    #region ReklasAsetlainlain
    [Serializable]
    public class ReklasAsetlainlainControl : ReklasControl, IDataControlUIEntry
    {
        public new void SetPageKey()
        {
            Kdtans = "233";
        }
    }
    #endregion ReklasAsetlainlain

    #region ReklasKDP
    [Serializable]
    public class ReklasKdpControl : ReklasControl, IDataControlUIEntry
    {
        public new void SetPageKey()
        {
            Kdtans = "215";
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

            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreklas=Nomor BAST"), typeof(string), 40, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglreklas=Tanggal BAST"), typeof(DateTime), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglvalid=Tanggal Pengesahan"), typeof(DateTime), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 50, HorizontalAlign.Left));

            return columns;
        }
        public override HashTableofParameterRow GetEntries()
        {
            bool enableValid = false, enable = !Valid;

            ReklasControl cReklasGetjmldata = new ReklasControl();
            cReklasGetjmldata.Unitkey = Unitkey;
            cReklasGetjmldata.Noreklas = Noreklas;
            cReklasGetjmldata.Kdtans = Kdtans;
            cReklasGetjmldata.Load("Jmldata");

            ReklasControl cReklasGetjmlasetkeynull = new ReklasControl();
            cReklasGetjmlasetkeynull.Unitkey = Unitkey;
            cReklasGetjmlasetkeynull.Noreklas = Noreklas;
            cReklasGetjmlasetkeynull.Kdtans = Kdtans;
            cReklasGetjmlasetkeynull.Load("Jmlasetkeynull");

            if (cReklasGetjmldata.Jmldata != 0 && cReklasGetjmlasetkeynull.Jmlasetkeynull == 0)
            {
                enableValid = true;
            }

            HashTableofParameterRow hpars = new HashTableofParameterRow();
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Noreklas=Nomor BAST"), false, 50).SetEnable(enable));
            hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglreklas=Tanggal BAST"), true).SetEnable(enable));
            hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Ket=Keterangan"), true, 3).SetEnable(enable).SetAllowEmpty(true));
            hpars.Add(new ParameterRowCek(this, true).SetEnable(enableValid).SetEditable(enable));
            return hpars;
        }
    }
    #endregion ReklasKDP
}

