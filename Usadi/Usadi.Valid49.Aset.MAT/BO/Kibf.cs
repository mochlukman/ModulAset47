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
  #region Usadi.Valid49.BO.KibfControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class KibfControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
  {
    #region Properties
    public long Id { get; set; }
    public string Idbrg { get; set; }
    public string Unitkey { get; set; }
    public string Kdunit { get; set; }
    public string Nmunit { get; set; }
    public string Asetkey { get; set; }
    public string Kdaset { get; set; }
    public string Nmaset { get; set; }
    public DateTime Tglperolehan { get; set; }
    public string Noreg { get; set; }
    public int Jumlah { get; set; }
    public decimal Nilai { get; set; }
    public string Kdpemilik { get; set; }
    public string Nmpemilik { get; set; }
    public string Kdhak { get; set; }
    public string Nmhak { get; set; }
    public string Kdfisik { get; set; }
    public string Nmfisik { get; set; }
    public string Asalusul { get; set; }
    public new string Ket { get; set; }
    public string Bertingkat { get; set; }
    public string Beton { get; set; }
    public decimal Luaslt { get; set; }
    public string Alamat { get; set; }
    public string Nodokkdp { get; set; }
    public DateTime Tgdokkdp { get; set; }
    public DateTime Tgmulai { get; set; }
    public string Nokdtanah { get; set; }
    public string Kdlokpo { get; set; }
    public string Kdbrgpo { get; set; }
    public string Kdsatuan { get; set; }
    public string Nmsatuan { get; set; }
    public string Kdkib { get; set; }
    public string Kdtahun { get; set; }
    public string Nmtahun { get; set; }
    public string Nmkdtanah { get; set; }
    public string Noba { get; set; }
    public string Kdtans { get; set; }
    public int Idtermin { get; set; }
    public decimal Prosenfisik { get; set; }
    public decimal Prosenbiaya { get; set; }
    public string Nilinsert { get; set; }
    public int Jmldata { get; set; }
    public int Tahunsa { get; set; }
    public DateTime Tglsa { get; set; }
    public int Uruttrans { get; set; }
    public int Uruthist { get; set; }
    public int Minuruthist { get; set; }
    public int Jmlkibdet { get; set; }
    public string Entrysa { get; set; }
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
          CommandName = "ViewTransaksi",
          Icon = Icon.PageCopy
        };
        cmd1.ToolTip.Text = "Klik Untuk Menampilkan Rincian Transaksi";
        return new ImageCommand[] { cmd1 };
      }
    }

    public string ViewTransaksi
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
        return "" + Nmaset + "; Tahun " + Tahun + "; Register " + Noreg + "; " + Alamat + ":" + url;
      }
    }
    #endregion Properties 

    #region Methods 
    public KibfControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLKIBF;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Idbrg", "Uruttrans", "Uruthist" };
      cViewListProperties.IDKey = "Id";
      cViewListProperties.IDProperty = "Id";
      cViewListProperties.ReadOnlyFields = new String[] { "Asetkey", "Kdaset", "Nmaset", "Unitkey", "Kdunit", "Nmunit" };
      cViewListProperties.SortFields = new String[] { };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
      cViewListProperties.PageSize = 30;
      cViewListProperties.RefreshFilter = true;

      WebsetControl cWebset = new WebsetControl();
      cWebset.Kdset = "saldoawal";
      cWebset.Load("PK");
      Entrysa = cWebset.Valset.ToUpper();

      if (Blokid == "1")
      {
        cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      }
      else
      {
        if (Entrysa == "Y")
        {
          cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_EDIT_DEL;
        }
        else
        {
          cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
        }

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

      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 40, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglperolehan=Tanggal Perolehan"), typeof(DateTime), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun"), typeof(int), 15, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreg=No Register"), typeof(string), 15, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Jumlah=Jumlah Barang"), typeof(int), 18, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai"), typeof(decimal), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmhak=Hak"), typeof(string), 25, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmfisik=Fisik"), typeof(string), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Luaslt=Luas Lantai"), typeof(decimal), 15, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nodokkdp=No. Dok KDP"), typeof(string), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tgdokkdp=Tgl Dok KDP"), typeof(DateTime), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Alamat"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 100, HorizontalAlign.Left));

      return columns;
    }
    public new void SetPageKey()
    {
      Kdkib = ConstantTablesAsetMAT.KDKIBF;
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

      Tahunsa = (Int32.Parse(cPemda.Configval) - 1);
      Tglsa = new DateTime(Tahunsa, DateTime.Today.Month, DateTime.Today.Day);
      Tglperolehan = Tglsa;
      Tgdokkdp = Tglsa;
      Tgmulai = Tglsa;

      Bertingkat = "0";
      Beton = "0";
      Kdtans = "000";
      Jumlah = 1;
    }
    public new HashTableofParameterRow GetFilters()
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      //hpars.Add(DaftasetKibfilterLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enableFilter).SetAllowEmpty(false));
      hpars.Add(DaftasetKibLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enableFilter).SetAllowEmpty(false));
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
      List<KibfControl> ListData = new List<KibfControl>();
      foreach (KibfControl dc in list)
      {
        ListData.Add(dc);
      }

      return ListData;
    }
    private bool IsValid()
    {
      bool valid = true;
      return valid;
    }
    public new void Insert()
    {
      Tahun = Tglperolehan.Year;

      PemdaControl cPemda = new PemdaControl();
      cPemda.Configid = "cur_thang";
      cPemda.Load("PK");

      if (Tglperolehan.Year >= Int32.Parse(cPemda.Configval.Trim()))
      {
        throw new Exception("Gagal menyimpan data : entri saldo awal hanya untuk perolehan aset dibawah tahun berjalan, cek kembali tanggal perolehan");
      }
      if (Prosenfisik > 100)
      {
        throw new Exception("Gagal menyimpan data : Prosentase fisik tidak boleh melebihi 100%");
      }
      if (Prosenbiaya > 100)
      {
        throw new Exception("Gagal menyimpan data : Prosentase biaya tidak boleh melebihi 100%");
      }

      string sql = @"
        exec [dbo].[KIBF_INSERT]
        @UNITKEY = N'{0}',
        @ASETKEY = N'{1}',
        @TAHUN = N'{2}',
        @KDPEMILIK = N'{3}',
        @ASALUSUL = N'{4}',
        @KET = N'{5}',
        @KDHAK = N'{6}',
        @KDFISIK = N'{7}',
        @BERTINGKAT = N'{8}',
        @BETON = N'{9}',
        @LUASLT = N'{10}',
        @ALAMAT = N'{11}',
        @NODOKKDP = N'{12}',
        @TGDOKKDP = N'{13}',
        @TGMULAI = N'{14}',
        @NOKDTANAH = N'{15}',
        @KDSATUAN = N'{16}',
        @NOBA = N'{17}',
        @TGLBA = N'{18}',
        @KDTANS = N'{19}',
        @NILAI = N'{20}',
        @JUMLAH = N'{21}',
        @KDKIB = N'{22}'
        ";
      sql = string.Format(sql, Unitkey, Asetkey, Tahun, Kdpemilik, Asalusul, Ket, Kdhak, Kdfisik, Bertingkat, Beton, Luaslt
        , Alamat, Nodokkdp, Tgdokkdp.ToString("yyyy-MM-dd"), Tgmulai.ToString("yyyy-MM-dd"), Nokdtanah, Kdsatuan
        , Noba, Tglperolehan.ToString("yyyy-MM-dd"), Kdtans, Nilai, Jumlah, Kdkib);
      BaseDataAdapter.ExecuteCmd(this, sql);
    }
    public new int Update()
    {
      int n = 0;
      if (IsValid())
      {
        n = base.Update();
      }
      else
      {
        throw new Exception(ConstantDict.Translate("LBL_INVALID_UPDATE"));
      }
      return n;
    }
    public new int Delete()
    {
      KibfControl cKibfGetjmlkibdet = new KibfControl();
      cKibfGetjmlkibdet.Idbrg = Idbrg;
      cKibfGetjmlkibdet.Uruthist = Uruthist;
      cKibfGetjmlkibdet.Load("Jmlkibdet");

      KibfControl cKibfGetminuruthist = new KibfControl();
      cKibfGetminuruthist.Idbrg = Idbrg;
      cKibfGetminuruthist.Load("Minuruthist");

      //KibfControl cKibfGetjmlskpengguna = new KibfControl();
      //cKibfGetjmlskpengguna.Idbrg = Idbrg;
      //cKibfGetjmlskpengguna.Load("Jmlskpengguna");

      int n = 0;
      if (cKibfGetjmlkibdet.Jmlkibdet != 0)
      {
        throw new Exception("Hapus rincian transaksi terlebih dahulu");
      }
      else
      {
        Status = -1;
        base.Delete("Spesifikasi");

        if (Uruthist == cKibfGetminuruthist.Minuruthist)
        {
          base.Delete("Kib"); //hapus entrian saldo awal
        }
      }
      return n;
    }

    public const string GROUP_1 = "A. Spesifikasi 1";
    public const string GROUP_2 = "B. Spesifikasi 2";
    public const string GROUP_3 = "C. Nilai";

    public override HashTableofParameterRow GetEntries()
    {
      bool enable = true;
      HashTableofParameterRow hpars = new HashTableofParameterRow();

      KibfControl KibfGetkdtanah = new KibfControl();
      KibfGetkdtanah.Nokdtanah = Nokdtanah;
      KibfGetkdtanah.Load("Kdtanah");
      Nmkdtanah = KibfGetkdtanah.Nmkdtanah;

      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdpemilik=Pemilik"),
      GetList(new JmilikLookupControl()), "Kdpemilik=Nmpemilik", 50).SetAllowRefresh(false).SetEnable(enable).SetAllowEmpty(false).SetGroup(GROUP_1));

      hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglperolehan=Tanggal Perolehan"), false).SetEnable(enable).SetGroup(GROUP_1));

      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdhak=Hak"),
      GetList(new JhakLookupControl()), "Kdhak=Nmhak", 50).SetAllowRefresh(false).SetEnable(enable).SetAllowEmpty(false).SetGroup(GROUP_1));

      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdfisik=Fisik"),
      GetList(new JfisikLookupControl()), "Kdfisik=Nmfisik", 50).SetAllowRefresh(false).SetEnable(enable).SetAllowEmpty(false).SetGroup(GROUP_1));

      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdsatuan=Satuan"),
      GetList(new SatuanLookupControl()), "Kdsatuan=Nmsatuan", 50).SetAllowRefresh(false).SetEnable(enable).SetAllowEmpty(false).SetGroup(GROUP_1));

      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Asalusul=Asal Usul"), true, 90).SetEnable(enable).SetGroup(GROUP_1));

      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nodokkdp=No Dokumen KDP"), true, 50).SetEnable(enable).SetGroup(GROUP_1).SetLength(30));
      hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tgdokkdp=Tgl Dokumen KDP"), true).SetEnable(enable).SetGroup(GROUP_1));
      hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tgmulai=Tanggal Mulai"), true).SetEnable(enable).SetGroup(GROUP_1));

      hpars.Add(KdtanahLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enable)
        .SetAllowEmpty(true).SetGroup(GROUP_2));

      ArrayList listBertingkat = new ArrayList(new ParamControl[] {
        new ParamControl() {  Kdpar="1",Nmpar="Bertingkat "}
        ,new ParamControl() { Kdpar="0",Nmpar="Tidak "}
      });
      hpars.Add(new ParameterRow(ConstantDict.GetColumnTitleEntry("Bertingkat=Bertingkat"), ParameterRow.MODE_TYPE,
        listBertingkat, "Kdpar=Nmpar", 70).SetAllowRefresh(false).SetEnable(enable).SetEditable(enable).SetGroup(GROUP_2));

      ArrayList listBeton = new ArrayList(new ParamControl[] {
        new ParamControl() {  Kdpar="1",Nmpar="Beton "}
        ,new ParamControl() { Kdpar="0",Nmpar="Tidak "}
      });
      hpars.Add(new ParameterRow(ConstantDict.GetColumnTitleEntry("Beton=Beton"), ParameterRow.MODE_TYPE,
        listBeton, "Kdpar=Nmpar", 70).SetAllowRefresh(false).SetEnable(enable).SetEditable(enable).SetGroup(GROUP_2));

      hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Luaslt=Luas Lantai"), true, 50).SetEnable(enable).SetGroup(GROUP_2));
      hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Alamat"), true, 3).SetEnable(enable).SetAllowEmpty(true).SetGroup(GROUP_2).SetLength(200));
      hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Ket=Keterangan"), true, 3).SetEnable(enable).SetAllowEmpty(true)
        .SetGroup(GROUP_2).SetLength(200));

      //hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Idtermin=Termin"), false, 15).SetEnable(enable).SetEditable(enable)
      //  .SetAllowEmpty(false).SetGroup(GROUP_3));

      //hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Prosenfisik=Prosentase Fisik %"), false, 15).SetEnable(enable).SetEditable(enable)
      //  .SetAllowEmpty(false).SetGroup(GROUP_3));

      //hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Prosenbiaya=Prosentase Biaya %"), false, 15).SetEnable(enable).SetEditable(enable)
      //  .SetAllowEmpty(false).SetGroup(GROUP_3));

      hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Jumlah=Jumlah Barang"), true, 15).SetEnable(enable).SetEditable(false)
        .SetAllowEmpty(false).SetGroup(GROUP_3));

      hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Nilai"), true, 35).SetEnable(enable).SetEditable(false)
        .SetAllowEmpty(false).SetGroup(GROUP_3));
      if (Entrysa == "Y")
      {
        hpars.Add(new ParameterRowUploadFile(this, true));
      }
      //hpars.Add(new ParameterRowUploadFile(this, true));
      return hpars;
    }

    #endregion Methods 
  }
  #endregion Kibf
}

