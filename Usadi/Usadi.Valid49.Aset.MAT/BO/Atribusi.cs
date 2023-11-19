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
    #region Usadi.Valid49.BO.AtribusiControl, Usadi.Valid49.Aset.MAT
    [Serializable]
    public class AtribusiControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
    {
        #region Properties
        public long Id { get; set; }
        public DateTime Tglatribusi { get; set; }
        public DateTime Tglvalid { get; set; }
        public new string Ket { get; set; }
        public string Kdunit { get; set; }
        public string Nmunit { get; set; }
        public DateTime Tglba { get; set; }
        public string Tglbalookup { get { return Tglba.ToString("dd/MM/yyyy"); } }
        public string Uraiba { get; set; }
        public string Kdtahap { get; set; }
        public string Uraian { get; set; }
        public int Jmlbukti { get; set; }
        public int Jmlrek { get; set; }
        public int Jmlbrg { get; set; }
        public int Jmlgenerated { get; set; }
        public int Jmlkib { get; set; }
        public int Jmlbarenov { get; set; }
        public int Tahunsa { get; set; }
        public DateTime Tglsa { get; set; }
        public string Nobarenov { get; set; }
        public string Unitkey { get; set; }
        public string Noatribusi { get; set; }
        public string Noba { get; set; }
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
                    CommandName = "ViewAtribusidet",
                    Icon = Icon.PageCopy
                };
                cmd1.ToolTip.Text = "Klik Untuk Menampilkan Rincian Rekening Atribusi";
                return new ImageCommand[] { cmd1 };
            }
        }
        public string ViewAtribusidet
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
                return "Rincian Rekening; " + Noatribusi + "- No. BAST; " + Noba + ":" + url;
            }
        }
        #endregion Properties 

        #region Methods 
        public AtribusiControl()
        {
            XMLName = ConstantTablesAsetMAT.XMLATRIBUSI;
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
            cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Noatribusi", "Noba" };
            cViewListProperties.IDKey = "Id";
            cViewListProperties.IDProperty = "Id";
            #region COR-49 Refactor Get Tahun dari Setting DB - STEP 2
            cViewListProperties.ReadOnlyFields = new String[] { "Unitkey", "Kdunit", "Nmunit", "Tahun", "Tahunsa" };
            #endregion
            cViewListProperties.SortFields = new String[] { "Tglatribusi", "Noatribusi" };
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

            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noatribusi=Nomor Bukti"), typeof(string), 30, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglatribusi=Tanggal Bukti"), typeof(DateTime), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglvalid=Tanggal Pengesahan"), typeof(DateTime), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noba=Nomor BAST"), typeof(string), 25, HorizontalAlign.Left));
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
            #region Hapus
            //PemdaControl cPemda = new PemdaControl();
            //cPemda.Configid = "cur_thang";
            //cPemda.Load("PK");
            //Tahunsa = (Int32.Parse(cPemda.Configval));
            #endregion

            Tglsa = new DateTime(Tahunsa, DateTime.Today.Month, DateTime.Today.Day);
            Tglatribusi = Tglsa;
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
            List<AtribusiControl> ListData = new List<AtribusiControl>();
            foreach (AtribusiControl dc in list)
            {
                dc.Valid = (dc.Tglvalid != new DateTime());
                ListData.Add(dc);
            }
            return ListData;
        }
        public override HashTableofParameterRow GetEntries()
        {
            bool enableValid = false, enable = !Valid;

            AtribusiControl cAtribusiCekjmlrek = new AtribusiControl();
            cAtribusiCekjmlrek.Unitkey = Unitkey;
            cAtribusiCekjmlrek.Noatribusi = Noatribusi;
            cAtribusiCekjmlrek.Noba = Noba;
            cAtribusiCekjmlrek.Load("Jmlrek");
            Jmlrek = cAtribusiCekjmlrek.Jmlrek;

            AtribusiControl cAtribusiCekjmlbrg = new AtribusiControl();
            cAtribusiCekjmlbrg.Unitkey = Unitkey;
            cAtribusiCekjmlbrg.Noatribusi = Noatribusi;
            cAtribusiCekjmlbrg.Noba = Noba;
            cAtribusiCekjmlbrg.Load("Jmlbrg");
            Jmlbrg = cAtribusiCekjmlbrg.Jmlbrg;

            AtribusiControl cAtribusiCekjmlgenerated = new AtribusiControl();
            cAtribusiCekjmlgenerated.Unitkey = Unitkey;
            cAtribusiCekjmlgenerated.Noatribusi = Noatribusi;
            cAtribusiCekjmlgenerated.Noba = Noba;
            cAtribusiCekjmlgenerated.Load("Jmlgenerated");
            Jmlgenerated = cAtribusiCekjmlgenerated.Jmlgenerated;

            if (Jmlrek != 0 && (Jmlbrg - Jmlgenerated) == 0)
            {
                enableValid = true;
            }

            HashTableofParameterRow hpars = new HashTableofParameterRow();
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Noatribusi=Nomor Bukti"), false, 50).SetEnable(enable).SetAllowEmpty(false));
            hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglatribusi=Tanggal Bukti"), true).SetEnable(enable).SetAllowEmpty(false));
            hpars.Add(BeritaLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(enable).SetEditable(false));
            hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Ket=Keterangan"), true, 3).SetEnable(enable).SetAllowEmpty(true));
            hpars.Add(new ParameterRowCek(this, true).SetEnable(enableValid).SetEditable(enable));
            return hpars;
        }
        public bool IsValid()
        {
            bool valid = true;
            return valid;
        }
        public new void Insert()
        {
            PemdaControl cPemda = new PemdaControl();
            cPemda.Configid = "cur_thang";
            cPemda.Load("PK");

            if (Tglatribusi.Year.ToString().Trim() != cPemda.Configval.Trim())
            {
                throw new Exception("Gagal menyimpan data : tanggal atribusi hanya untuk tahun anggaran berjalan");
            }
            if (Tglatribusi < Tglba)
            {
                throw new Exception("Gagal menyimpan data : tanggal atribusi tidak boleh mendahului tanggal BAST");
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
                if (Tglatribusi.Year.ToString().Trim() != cPemda.Configval.Trim())
                {
                    throw new Exception("Gagal menyimpan data : tanggal atribusi hanya untuk tahun anggaran berjalan");
                }
                else if (Tglatribusi < Tglba)
                {
                    throw new Exception("Gagal menyimpan data : tanggal atribusi tidak boleh mendahului tanggal BAST");
                }
                else
                {
                    if (Valid)
                    {
                        AtribusiControl cAtribusiCekjmlkib = new AtribusiControl();
                        cAtribusiCekjmlkib.Unitkey = Unitkey;
                        cAtribusiCekjmlkib.Noatribusi = Noatribusi;
                        cAtribusiCekjmlkib.Load("Jmlkib");

                        if (cAtribusiCekjmlkib.Jmlkib >= 1)
                        {
                            throw new Exception("Gagal mengubah data : Dokumen atribusi ini sudah di sahkan dan masuk ke KIB");
                        }
                        else
                        {
                            Tglvalid = Tglba;
                            base.Update("Sah");
                        }
                    }
                    else
                    {
                        base.Update();
                    }
                }
            }
            return n;
        }
        public new int Delete()
        {
            bool cekjmlkib = false;

            AtribusiControl cAtribusiCekjmlkib = new AtribusiControl();
            cAtribusiCekjmlkib.Unitkey = Unitkey;
            cAtribusiCekjmlkib.Noatribusi = Noatribusi;
            cAtribusiCekjmlkib.Load("Jmlkib");

            Jmlkib = cAtribusiCekjmlkib.Jmlkib;

            cekjmlkib = (Jmlkib >= 1);

            int n = 0;
            if (Valid)
            {
                if (cekjmlkib == true)
                {
                    throw new Exception("Atribusi sudah masuk ke register KIB, pengesahan tidak dapat dicabut");
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
    #endregion Atribusi

    #region AtribusiRenov
    [Serializable]
    public class AtribusiRenovControl : AtribusiControl, IDataControlUIEntry, IHasJSScript
    {
        #region Methods
        public new IList View()
        {
            IList list = this.View("Renov");
            return list;
        }
        public new int Update()
        {
            PemdaControl cPemda = new PemdaControl();
            cPemda.Configid = "cur_thang";
            cPemda.Load("PK");

            AtribusiControl cAtribusiGetjmlbarenov = new AtribusiControl();
            cAtribusiGetjmlbarenov.Unitkey = Unitkey;
            cAtribusiGetjmlbarenov.Noba = Noba;
            cAtribusiGetjmlbarenov.Load("Jmlbarenov");
            Jmlbarenov = cAtribusiGetjmlbarenov.Jmlbarenov;

            int n = 0;

            if (Tglatribusi.Year.ToString().Trim() != cPemda.Configval.Trim())
            {
                throw new Exception("Gagal menyimpan data : tanggal atribusi hanya untuk tahun anggaran berjalan");
            }
            else if (Tglatribusi < Tglba)
            {
                throw new Exception("Gagal menyimpan data : tanggal atribusi tidak boleh mendahului tanggal BAST");
            }
            else
            {
                n = ((BaseDataControlUI)this).Update();

                if (Valid)
                {
                    if (Jmlbarenov == 0)
                    {
                        throw new Exception("Gagal menyimpan data : BAST pemeliharaan atau renovasi dalam atribusi ini belum disahkan");
                    }
                    else
                    {
                        string sql = @"
            exec [dbo].[WSPX_ATRIBUSIRENOV]
            @UNITKEY = N'{0}',
            @NOATRIBUSI = N'{1}',
            @NOBA = N'{2}',
            @TGLATRIBUSI = N'{3}'
            ";
                        sql = string.Format(sql, Unitkey, Noatribusi, Noba, Tglatribusi.ToString("yyyy-MM-dd"));
                        BaseDataAdapter.ExecuteCmd(this, sql);
                    }
                }
            }
            return n;
        }
        public override HashTableofParameterRow GetEntries()
        {
            bool enableValid = false, enable = !Valid;

            AtribusiControl cAtribusiCekjmlrek = new AtribusiControl();
            cAtribusiCekjmlrek.Unitkey = Unitkey;
            cAtribusiCekjmlrek.Noatribusi = Noatribusi;
            cAtribusiCekjmlrek.Noba = Noba;
            cAtribusiCekjmlrek.Load("Jmlrek");
            Jmlrek = cAtribusiCekjmlrek.Jmlrek;

            AtribusiControl cAtribusiCekjmlbrg = new AtribusiControl();
            cAtribusiCekjmlbrg.Unitkey = Unitkey;
            cAtribusiCekjmlbrg.Noatribusi = Noatribusi;
            cAtribusiCekjmlbrg.Noba = Noba;
            cAtribusiCekjmlbrg.Load("Jmlbrg");
            Jmlbrg = cAtribusiCekjmlbrg.Jmlbrg;

            AtribusiControl cAtribusiCekjmlgenerated = new AtribusiControl();
            cAtribusiCekjmlgenerated.Unitkey = Unitkey;
            cAtribusiCekjmlgenerated.Noatribusi = Noatribusi;
            cAtribusiCekjmlgenerated.Noba = Noba;
            cAtribusiCekjmlgenerated.Load("Jmlgenerated");
            Jmlgenerated = cAtribusiCekjmlgenerated.Jmlgenerated;

            if (Jmlrek != 0 && (Jmlbrg - Jmlgenerated) == 0)
            {
                enableValid = true;
            }

            HashTableofParameterRow hpars = new HashTableofParameterRow();
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Noatribusi=Nomor Bukti"), false, 50).SetEnable(enable).SetAllowEmpty(false));
            hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglatribusi=Tanggal Bukti"), true).SetEnable(enable).SetAllowEmpty(false));
            hpars.Add(HistrenovLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(enable).SetEditable(false));
            hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Ket=Keterangan"), true, 3).SetEnable(enable).SetAllowEmpty(true));
            hpars.Add(new ParameterRowCek(this, true).SetEnable(enableValid).SetEditable(enable));
            return hpars;
        }
        #endregion Methods 
    }
    #endregion AtribusiRenov

}

