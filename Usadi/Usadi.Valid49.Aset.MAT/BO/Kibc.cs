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
  #region Usadi.Valid49.BO.KibcControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class KibcControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
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
    public decimal Nilai { get; set; }
    public decimal Umeko { get; set; }
    public string Kdpemilik { get; set; }
    public string Nmpemilik { get; set; }
    public string Asalusul { get; set; }
    public string Pengguna { get; set; }
    public string Kdkon { get; set; }
    public string Nmkon { get; set; }
    public string Kdhak { get; set; }
    public string Nmhak { get; set; }
    public string Kdsatuan { get; set; }
    public string Nmsatuan { get; set; }
    public string Bertingkat { get; set; }
    public string Beton { get; set; }
    public decimal Luaslt { get; set; }
    public string Nodokgdg { get; set; }
    public DateTime Tgdokgdg { get; set; }
    public decimal Luastnh { get; set; }
    public string Nokdtanah { get; set; }
    public string Alamat { get; set; }
    public new string Ket { get; set; }
    public string Kdlokpo { get; set; }
    public string Kdbrgpo { get; set; }
    public string Kdklas { get; set; }
    public string Uraiklas { get; set; }
    public string Kdsensus { get; set; }
    public string Kdstatusaset { get; set; }
    public string Kdkib { get; set; }
    public string Kdtahun { get; set; }
    public string Nmtahun { get; set; }
    public string Nmkdtanah { get; set; }
    public string Noba { get; set; }
    public string Kdtans { get; set; }
    public int Jumlah { get; set; }
    public string Nilinsert { get; set; }
    public int Tahunsa { get; set; }
    public DateTime Tglsa { get; set; }
    public int Uruttrans { get; set; }
    public int Uruthist { get; set; }
    public int Minuruthist { get; set; }
    public int Jmlkibdet { get; set; }
    public int Jmlskpengguna { get; set; }
    public string Entrysa { get; set; }
    public string Lokasi { get; set; }
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
    public KibcControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLKIBC;
      Ss10userControl dcUser = (Ss10userControl)GlobalAsp.GetSessionUser();
      Tahun = dcUser.Tahun;
      Tahunsa = dcUser.Tahun;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Idbrg", "Uruttrans", "Uruthist" };
      cViewListProperties.IDKey = "Id";
      cViewListProperties.IDProperty = "Id";
      cViewListProperties.ReadOnlyFields = new String[] { "Asetkey", "Kdaset", "Nmaset", "Unitkey", "Kdunit", "Nmunit", "Tahun", "Tahunsa" };
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
          cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_EDIT_DEL;
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
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai"), typeof(decimal), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Umeko=Masa Pakai"), typeof(decimal), 15, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Asalusul=Asal Usul"), typeof(string), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Pengguna"), typeof(string), 40, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmkon=Kondisi"), typeof(string), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmhak=Hak"), typeof(string), 25, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Luaslt=Luas Lantai"), typeof(decimal), 15, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Luastnh=Luas Tanah"), typeof(decimal), 15, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nodokgdg=Nomor Dokumen"), typeof(string), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tgdokgdg=Tanggal Dokumen"), typeof(DateTime), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Alamat"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 100, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Lokasi=Lokasi"), typeof(string), 100, HorizontalAlign.Left));

      return columns;
    }
    public new void SetPageKey()
    {
      Kdkib = ConstantTablesAsetMAT.KDKIBC;
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
      Tglperolehan = Tglsa;
      Tgdokgdg = Tglsa;

      Bertingkat = "0";
      Beton = "0";
      Kdtans = "000";
      Jumlah = 1;
      Nilinsert = "2";
    }
    public new HashTableofParameterRow GetFilters()
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
      HashTableofParameterRow hpars = new HashTableofParameterRow();
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
      List<KibcControl> ListData = new List<KibcControl>();
      foreach (KibcControl dc in list)
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


      string sql = @"
        exec [dbo].[KIBC_INSERT]
        @UNITKEY = N'{0}',
        @ASETKEY = N'{1}',
        @TAHUN = N'{2}',
        @KDPEMILIK = N'{3}',
        @ASALUSUL = N'{4}',
        @PENGGUNA = N'{5}',
        @KET = N'{6}',
        @KDHAK = N'{7}',
        @LUASTNH = N'{8}',
        @ALAMAT = N'{9}',
        @KDSATUAN = N'{10}',
        @KDKON = N'{11}',
        @BERTINGKAT = N'{12}',
        @BETON = N'{13}',
        @LUASLT = N'{14}',
        @NODOKGDG = N'{15}',
        @TGDOKGDG = N'{16}',
        @NOKDTANAH = N'{17}',
        @NOBA = N'{18}',
        @TGLBA = N'{19}',
        @KDTANS = N'{20}',
        @JUMLAH = N'{21}',
        @NILAI = N'{22}',
        @NILINSERT = N'{23}',       
        @LOKASI = N'{24}'
        ";
      sql = string.Format(sql, Unitkey, Asetkey, Tahun, Kdpemilik, Asalusul, Pengguna, Ket, Kdhak, Luastnh, Alamat, Kdsatuan, Kdkon
        , Bertingkat, Beton, Luaslt, Nodokgdg, Tgdokgdg.ToString("yyyy-MM-dd"), Nokdtanah, Noba, Tglperolehan.ToString("yyyy-MM-dd")
        , Kdtans, Jumlah, Nilai, Nilinsert, Lokasi);
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
      KibcControl cKibcGetjmlkibdet = new KibcControl();
      cKibcGetjmlkibdet.Idbrg = Idbrg;
      cKibcGetjmlkibdet.Uruthist = Uruthist;
      cKibcGetjmlkibdet.Load("Jmlkibdet");

      KibcControl cKibcGetminuruthist = new KibcControl();
      cKibcGetminuruthist.Idbrg = Idbrg;
      cKibcGetminuruthist.Load("Minuruthist");

      KibcControl cKibcGetjmlskpengguna = new KibcControl();
      cKibcGetjmlskpengguna.Idbrg = Idbrg;
      cKibcGetjmlskpengguna.Load("Jmlskpengguna");

      int n = 0;
      if (cKibcGetjmlkibdet.Jmlkibdet != 0)
      {
        throw new Exception("Hapus rincian transaksi terlebih dahulu");
      }
      else
      {
        Status = -1;
        base.Delete("Spesifikasi");

        if (Uruthist == cKibcGetminuruthist.Minuruthist)
        {
          base.Update("Statuspengguna");

          if (cKibcGetjmlskpengguna.Jmlskpengguna == 0)
          {
            base.Delete("Kib"); //hapus entrian saldo awal
          }
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

      Jumlah = 1;
      Nilinsert = "2";

      KibcControl KibcGetkdtanah = new KibcControl();
      KibcGetkdtanah.Nokdtanah = Nokdtanah;
      KibcGetkdtanah.Load("Kdtanah");
      Nmkdtanah = KibcGetkdtanah.Nmkdtanah;

      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdpemilik=Pemilik"),
      GetList(new JmilikLookupControl()), "Kdpemilik=Nmpemilik", 50).SetAllowRefresh(false).SetEnable(enable).SetAllowEmpty(false).SetGroup(GROUP_1));

      hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglperolehan=Tanggal Perolehan"), false).SetEnable(enable).SetGroup(GROUP_1));

      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdkon=Kondisi"),
      GetList(new KonasetLookupControl()), "Kdkon=Nmkon", 50).SetAllowRefresh(false).SetEnable(enable).SetAllowEmpty(false)
      .SetEditable(false).SetGroup(GROUP_1));

      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdhak=Hak"),
      GetList(new JhakLookupControl()), "Kdhak=Nmhak", 50).SetAllowRefresh(false).SetEnable(enable).SetAllowEmpty(false).SetGroup(GROUP_1));

      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdsatuan=Satuan"),
      GetList(new SatuanLookupControl()), "Kdsatuan=Nmsatuan", 50).SetAllowRefresh(false).SetEnable(enable).SetAllowEmpty(false).SetGroup(GROUP_1));

      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Asalusul=Asal Usul"), true, 90).SetEnable(enable).SetGroup(GROUP_1).SetLength(200));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Pengguna"), true, 90).SetEnable(enable).SetGroup(GROUP_1).SetLength(200));
      hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Alamat"), true, 3).SetEnable(enable).SetAllowEmpty(true).SetGroup(GROUP_1).SetLength(200));

      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nodokgdg=Nomor Dokumen"), true, 50).SetEnable(enable).SetGroup(GROUP_2).SetLength(30));
      hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tgdokgdg=Tanggal Dokumen"), true).SetEnable(enable).SetGroup(GROUP_2));

      hpars.Add(KdtanahLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enable)
        .SetAllowEmpty(true).SetGroup(GROUP_2));

      hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Luastnh=Luas Tanah"), true, 50).SetEnable(enable).SetGroup(GROUP_2));
      hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Luaslt=Luas Lantai"), true, 50).SetEnable(enable).SetGroup(GROUP_2));

      ArrayList listBertingkat = new ArrayList(new ParamControl[] {
        new ParamControl() {  Kdpar="1",Nmpar="Bertingkat "}
        ,new ParamControl() { Kdpar="0",Nmpar="Tidak "}
      });
      hpars.Add(new ParameterRow(ConstantDict.GetColumnTitleEntry("Bertingkat=Bertingkat"), ParameterRow.MODE_TYPE,
        listBertingkat, "Kdpar=Nmpar", 70).SetEnable(enable).SetEditable(enable).SetGroup(GROUP_2));

      ArrayList listBeton = new ArrayList(new ParamControl[] {
        new ParamControl() {  Kdpar="1",Nmpar="Beton "}
        ,new ParamControl() { Kdpar="0",Nmpar="Tidak "}
      });
      hpars.Add(new ParameterRow(ConstantDict.GetColumnTitleEntry("Beton=Beton"), ParameterRow.MODE_TYPE,
        listBeton, "Kdpar=Nmpar", 70).SetEnable(enable).SetEditable(enable).SetGroup(GROUP_2));

      hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Lokasi"), true, 3).SetEnable(enable).SetAllowEmpty(true).SetGroup(GROUP_2).SetLength(200));

      hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Ket=Keterangan"), true, 3).SetEnable(enable).SetAllowEmpty(true)
        .SetGroup(GROUP_2).SetLength(200));

      hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Jumlah=Jumlah Barang"), true, 15).SetEnable(enable).SetEditable(false)
        .SetAllowEmpty(false).SetGroup(GROUP_3));

      ArrayList list = new ArrayList(new ParamControl[] {
        new ParamControl() {  Kdpar="1",Nmpar="Nilai Total "}
        ,new ParamControl() { Kdpar="2",Nmpar="Nilai Satuan "}
      });
      hpars.Add(new ParameterRow(ConstantDict.GetColumnTitleEntry("Nilinsert=Tipe Nilai"), ParameterRow.MODE_TYPE,
        list, "Kdpar=Nmpar", 70).SetAllowRefresh(false).SetEnable(enable).SetEditable(false).SetGroup(GROUP_3));

      hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Nilai"), true, 50).SetEnable(enable).SetEditable(false)
        .SetAllowEmpty(false).SetGroup(GROUP_3));

      hpars.Add(new ParameterRowUploadFile(this, true));
      return hpars;
    }

    #endregion Methods 
  }
  #endregion Kibc
}

