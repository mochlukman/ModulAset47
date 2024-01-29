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
  #region Usadi.Valid49.BO.KibmutasiControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class KibmutasiControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
  {
    #region Properties
    public long Id { get; set; }
    public DateTime Tglmutasikel { get; set; }
    public string Kdtans { get; set; }
    public new string Ket { get; set; }
    public DateTime Tglmutasiter { get; set; }
    public string Nmtrans { get; set; }
    public string Kdunit { get; set; }
    public string Nmunit { get; set; }
    public string Kdunit2 { get; set; }
    public string Nmunit2 { get; set; }
    public string Kdnmunit { get; set; }
    public string Kdnmunit2 { get; set; }
    public int Jmlkib { get; set; }
    public int Tahunsa { get; set; }
    public DateTime Tglsa { get; set; }
    public string Unitkey { get; set; }
    public string Unitkey2 { get; set; }
    public string Nomutasikel { get; set; }
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
          CommandName = "ViewKibmutasidet",
          Icon = Icon.PageCopy
        };
        cmd1.ToolTip.Text = "Klik Untuk Menampilkan Rincian Mutasi";
        return new ImageCommand[] { cmd1 };
      }
    }

    public string ViewKibmutasidet
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
        return "No. BAST; " + Nomutasikel + ":" + url;
      }
    }
    #endregion Properties 

    #region Methods 
    public KibmutasiControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLKIBMUTASI;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Unitkey2", "Nomutasikel" };
      cViewListProperties.IDKey = "Id";
      cViewListProperties.IDProperty = "Id";
      cViewListProperties.ReadOnlyFields = new String[] { "Unitkey", "Kdunit", "Nmunit" };
      cViewListProperties.SortFields = new String[] { "Tglmutasikel" };
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
        public new void SetPageKey()
        {
            Kdtans = "205";
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

      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nomutasikel=Nomor BA Mutasi"), typeof(string), 40, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglmutasikel=Tanggal BA Mutasi"), typeof(DateTime), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdunit2=Kode Unit"), typeof(string), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmunit2=Unit Tujuan"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglmutasiter=Tanggal Terima"), typeof(DateTime), 20, HorizontalAlign.Center));
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
      PemdaControl cPemda = new PemdaControl();
      cPemda.Configid = "cur_thang";
      cPemda.Load("PK");

      Tahunsa = (Int32.Parse(cPemda.Configval));
      Tglsa = new DateTime(Tahunsa, DateTime.Today.Month, DateTime.Today.Day);
      Tglmutasikel = Tglsa;
    }
    public new HashTableofParameterRow GetFilters()
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(DaftunitLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetAllowEmpty(false).SetEnable(true));

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
      List<KibmutasiControl> ListData = new List<KibmutasiControl>();
      foreach (KibmutasiControl dc in list)
      {
        dc.Valid = (dc.Tglmutasiter != new DateTime());
        ListData.Add(dc);
      }

      return ListData;
    }
    public new void Insert()
    {
      if (Unitkey2.Trim() == Unitkey.Trim())
      {
        throw new Exception("Gagal menyimpan data : Unit Tujuan tidak boleh sama dengan Unit Asal.");
      }
      PemdaControl cPemda = new PemdaControl();
      cPemda.Configid = "cur_thang";
      cPemda.Load("PK");

      if (Tglmutasikel.Year.ToString().Trim() != cPemda.Configval.Trim())
      {
        throw new Exception("Gagal menyimpan data : Proses mutasi hanya untuk tahun anggaran berjalan.");
      }
      base.Insert();
    }
    public new int Update()
    {
      int n = 0;
      PemdaControl cPemda = new PemdaControl();
      cPemda.Configid = "cur_thang";
      cPemda.Load("PK");

      if (Tglmutasikel.Year.ToString().Trim() != cPemda.Configval.Trim())
      {
        throw new Exception("Transaksi mutasi hanya untuk tahun anggaran berjalan, cek kembali tanggal mutasi");
      }

      base.Update();
      return n;
    }
    public new int Delete()
    {
      bool cekjmlkib = false;

      KibmutasiControl cKibmutasicekkib = new KibmutasiControl();
      cKibmutasicekkib.Unitkey = Unitkey;
      cKibmutasicekkib.Nomutasikel = Nomutasikel;
      cKibmutasicekkib.Load("Jmlkib");
      Jmlkib = cKibmutasicekkib.Jmlkib;

      cekjmlkib = (Jmlkib >= 1);

      int n = 0;
      if (Valid)
      {
        if (cekjmlkib == true)
        {
          throw new Exception("Aset sudah diterima Unit tujuan, penerimaan tidak dapat dicabut.");
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
      bool enable = !Valid;
      HashTableofParameterRow hpars = new HashTableofParameterRow();

      hpars.Add(DaftunitMutasiLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enable).SetEditable(false));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nomutasikel=Nomor BA Mutasi"), false, 50).SetEnable(enable));
      hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglmutasikel=Tgl BA Mutasi"), true).SetEnable(enable));
      hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Ket=Keterangan"), true, 3).SetEnable(enable).SetAllowEmpty(true));
      return hpars;
    }

    #endregion Methods 
  }
    #endregion Kibmutasi
    #region KibmutasiTerima
    [Serializable]
    public class KibmutasiInternalControl : KibmutasiControl, IDataControlUIEntry
    {
        public KibmutasiInternalControl()
        {
            XMLName = ConstantTablesAsetMAT.XMLKIBMUTASI;
        }
        public new IList View()
        {
            IList list = this.View("Keluarint");
            return list;
        }
        
        public new void SetPageKey()
        {
            Kdtans = "227";
        }

        public override HashTableofParameterRow GetEntries()
        {
            bool enable = !Valid;
            HashTableofParameterRow hpars = new HashTableofParameterRow();

            hpars.Add(DaftunitMutasiInternalLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enable).SetEditable(false));
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nomutasikel=Nomor BA Mutasi"), false, 50).SetEnable(enable));
            hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglmutasikel=Tgl BA Mutasi"), true).SetEnable(enable));
            hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Ket=Keterangan"), true, 3).SetEnable(enable).SetAllowEmpty(true));
            return hpars;
        }

        private bool IsValid()
        {
            bool valid = true;
            return valid;
        }

    }
    #endregion KibmutasiInternal
  #region KibmutasiTerima
    [Serializable]
  public class KibmutasiTerimaControl : KibmutasiControl, IDataControlUIEntry
  {
    public KibmutasiTerimaControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLKIBMUTASI;
    }
    public new IList View()
    {
      IList list = this.View("Terima");
      return list;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.ReadOnlyFields = new String[] { };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
      cViewListProperties.PageSize = 20;
      cViewListProperties.AllowMultiDelete = false;

      if (Blokid == "1")
      {
        cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      }
      else
      {
        cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_EDIT_DEL;
        cViewListProperties.AllowMultiDelete = true;
      }

      return cViewListProperties;
    }
     public new void SetPageKey()
     {
            Kdtans = "105";
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

      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nomutasikel=Nomor BA Mutasi"), typeof(string), 40, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglmutasikel=Tanggal BA Mutasi"), typeof(DateTime), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdunit=Kode Unit"), typeof(string), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmunit=Unit Asal"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglmutasiter=Tanggal Terima"), typeof(DateTime), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 50, HorizontalAlign.Left));

      return columns;
    }
    public override HashTableofParameterRow GetEntries()
    {
      bool enableValid = true;
      bool enable = !Valid;
      HashTableofParameterRow hpars = new HashTableofParameterRow();

      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Kdnmunit=Unit Asal"), false, 95).SetEnable(enable));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nomutasikel=Nomor BA Mutasi"), false, 50).SetEnable(enable));
      hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglmutasikel=Tgl BA Mutasi"), false).SetEnable(enable));
      hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Ket=Keterangan"), false, 3).SetEnable(enable).SetAllowEmpty(true));
      hpars.Add(new ParameterRowCek(this, true).SetEnable(enableValid).SetEditable(enable));
      return hpars;
    }
    private bool IsValid()
    {
      bool valid = true;
      return valid;
    }
    public new int Update()
    {
      Tglmutasiter = Tglmutasikel;

      int n = 0;
      if (IsValid())
      {
        if (Valid)
        {
          KibmutasiControl cKibmutasicekkib = new KibmutasiControl();
          cKibmutasicekkib.Unitkey = Unitkey;
          cKibmutasicekkib.Nomutasikel = Nomutasikel;
          cKibmutasicekkib.Load("Jmlkib");

          if (cKibmutasicekkib.Jmlkib >= 1)
          {
            throw new Exception("Gagal mengubah data : Dokumen mutasi ini sudah di sahkan dan masuk ke KIB SKPD penerima");
          }
          else
          {
            string sql = @"
            exec [dbo].[WSPX_MUTASI]
            @Unitkey = N'{0}',
            @Unitkey2 = N'{1}',
            @Nomutasikel = N'{2}',
            @Tglmutasikel = N'{3}'
            ";
            sql = string.Format(sql, Unitkey, Unitkey2, Nomutasikel, Tglmutasikel.ToString("yyyy-MM-dd"));
            BaseDataAdapter.ExecuteCmd(this, sql);

            //base.Update("Terima");
          }
        }
      }
      return n;
    }
    public new int Delete()
    {
      int n = 0;
      if (Valid)
      {
        
        string sql = @"
                      exec [dbo].[WSP_DEL_MUTASI]
                      @UNITKEY = N'{0}',
                      @UNITKEY2 = N'{1}',
                      @NOMUTASIKEL = N'{2}'
                      ";
        sql = string.Format(sql, Unitkey,Unitkey2,Nomutasikel);
        BaseDataAdapter.ExecuteCmd(this, sql);
        return ((BaseDataControlUI)this).Update("Draft");
      }
      //else
      //{
      //  //Status = -1;
      //  //base.Delete();
      //  //string msg = "Nomor Mutasi Belum di Valid!!!";
      //  //msg = string.Format(msg);
      //  //throw new Exception(msg);
      //}
      return n;
    }
  }
    #endregion KibmutasiTerima
  #region KibmutasiTerimaInternal
    [Serializable]
    public class KibmutasiTerimaInternalControl : KibmutasiControl, IDataControlUIEntry
    {
        public KibmutasiTerimaInternalControl()
        {
            XMLName = ConstantTablesAsetMAT.XMLKIBMUTASI;
        }
        public new IList View()
        {
            IList list = this.View("Terimaint");
            return list;
        }
        public new IProperties GetProperties()
        {
            ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
            cViewListProperties.ReadOnlyFields = new String[] { };
            cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
            cViewListProperties.PageSize = 20;
            cViewListProperties.AllowMultiDelete = false;

            if (Blokid == "1")
            {
                cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
            }
            else
            {
                cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_EDIT;
            }

            return cViewListProperties;
        }
        public new void SetPageKey()
        {
            Kdtans = "227";
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

            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nomutasikel=Nomor BA Mutasi"), typeof(string), 40, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglmutasikel=Tanggal BA Mutasi"), typeof(DateTime), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdunit=Kode Unit"), typeof(string), 20, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmunit=Unit Asal"), typeof(string), 50, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglmutasiter=Tanggal Terima"), typeof(DateTime), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 50, HorizontalAlign.Left));

            return columns;
        }
        public override HashTableofParameterRow GetEntries()
        {
            bool enableValid = true;
            bool enable = !Valid;
            HashTableofParameterRow hpars = new HashTableofParameterRow();

            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Kdnmunit=Unit Asal"), false, 95).SetEnable(enable));
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nomutasikel=Nomor BA Mutasi"), false, 50).SetEnable(enable));
            hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglmutasikel=Tgl BA Mutasi"), false).SetEnable(enable));
            hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Ket=Keterangan"), false, 3).SetEnable(enable).SetAllowEmpty(true));
            hpars.Add(new ParameterRowCek(this, true).SetEnable(enableValid).SetEditable(enable));
            return hpars;
        }
        private bool IsValid()
        {
            bool valid = true;
            return valid;
        }
        public new int Update()
        {
            Tglmutasiter = Tglmutasikel;

            int n = 0;
            if (IsValid())
            {
                if (Valid)
                {
                    KibmutasiControl cKibmutasicekkib = new KibmutasiControl();
                    cKibmutasicekkib.Unitkey = Unitkey;
                    cKibmutasicekkib.Nomutasikel = Nomutasikel;
                    cKibmutasicekkib.Load("Jmlkib");

                    if (cKibmutasicekkib.Jmlkib >= 1)
                    {
                        throw new Exception("Gagal mengubah data : Dokumen mutasi ini sudah di sahkan dan masuk ke KIB SKPD penerima");
                    }
                    else
                    {
                        string sql = @"
            exec [dbo].[WSPX_MUTASI_INTERNAL]
            @Unitkey = N'{0}',
            @Unitkey2 = N'{1}',
            @Nomutasikel = N'{2}',
            @Tglmutasikel = N'{3}'
            ";
                        sql = string.Format(sql, Unitkey, Unitkey2, Nomutasikel, Tglmutasikel.ToString("yyyy-MM-dd"));
                        BaseDataAdapter.ExecuteCmd(this, sql);

                        //base.Update("Terima");
                    }
                }
            }
            return n;
        }
    }
    #endregion KibmutasiTerimaInternal
}

