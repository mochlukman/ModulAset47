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
  #region Usadi.Valid49.BO.KoreksidetspekControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class KoreksidetspekControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
  {
    #region Properties
    public long Id { get; set; }
    public string Asetkey { get; set; }
    public string Kdaset { get; set; }
    public string Nmaset { get; set; }
    public string Spesifikasi { get; set; }
    public DateTime Tglvalid { get; set; }
    public string Kdkib { get; set; }
    public string Nmkib { get; set; }
    public string Unitkey { get; set; }
    public string Nobakoreksi { get; set; }
    public string Idbrg { get; set; }
    public string Blokid { get; set; }
    public string Kdtans { get; set; }
    public string Kdpemilik { get; set; }
    public string Nmpemilik { get; set; }
    //public string Tahun { get; set; }
    public string Noreg { get; set; }
    public string Kdhak { get; set; }
    public string Nmhak { get; set; }
    public string Kdsatuan { get; set; }
    public string Nmsatuan { get; set; }
    public string Asalusul { get; set; }
    public string Pengguna { get; set; }
    public string Alamat { get; set; }
    public string Nofikat { get; set; }
    public DateTime Tgfikat { get; set; }
    public decimal Luastnh { get; set; }
    public string Utara { get; set; }
    public string Timur { get; set; }
    public string Selatan { get; set; }
    public string Barat { get; set; }
    public string Lokasi { get; set; }
    public string Ket { get; set; }
    public string Kdwarna { get; set; }
    public string Nmwarna { get; set; }
    public string Ukuran { get; set; }
    public string Bahan { get; set; }
    public string Nopabrik { get; set; }
    public string Norangka { get; set; }
    public string Nopolisi { get; set; }
    public string Nobpkb { get; set; }
    public string Nomesin { get; set; }
    public string Merktype { get; set; }
    public string Nodokgdg { get; set; }
    public DateTime Tgdokgdg { get; set; }
    public string Nokdtanah { get; set; }
    public decimal Luaslt { get; set; }
    public string Bertingkat { get; set; }
    public string Beton { get; set; }
    public string Nodokjij { get; set; }
    public DateTime Tgdokjij { get; set; }
    public string Konstruksi { get; set; }
    public decimal Panjang { get; set; }
    public decimal Lebar { get; set; }
    public decimal Luas { get; set; }
    public string Jdlpenerbit { get; set; }
    public string Bkpencipta { get; set; }
    public string Asaldaerah { get; set; }
    public string Pencipta { get; set; }
    public string Jenis { get; set; }
    public string Kdfisik { get; set; }
    public string Nmfisik { get; set; }
    public string Nodokkdp { get; set; }
    public DateTime Tgdokkdp { get; set; }
    public DateTime Tgmulai { get; set; }

    #endregion Properties 

    #region Methods 
    public KoreksidetspekControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLKOREKSIDETSPEK;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Nobakoreksi", "Idbrg" };
      cViewListProperties.IDKey = "Id";
      cViewListProperties.IDProperty = "Id";
      cViewListProperties.ReadOnlyFields = new String[] { };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_LOOKUP;
      cViewListProperties.LookupDC = " Usadi.Valid49.BO.ViewasetSpesifikasiControl, Usadi.Valid49.Aset.MAT";
      cViewListProperties.LookupLabelQuery = "";
      cViewListProperties.PageSize = 20;
      cViewListProperties.RefreshFilter = true;

      if (Tglvalid != new DateTime() || Blokid == "1")
      {
        cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      }
      else
      {
        cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_DEL;
        cViewListProperties.AllowMultiDelete = true;
      }
      return cViewListProperties;
    }
    public override DataControlFieldCollection GetColumns()
    {
      bool enable = true;

      if (Tglvalid != new DateTime() || Blokid == "1")
      {
        enable = false;
      }

      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center).SetVisible(enable));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreg=No Register"), typeof(string), 15, HorizontalAlign.Center));
      //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdkib=Kode KIB"), typeof(string), 15, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmpemilik=Nama Pemilik"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Pengguna=Pengguna"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 50, HorizontalAlign.Left));

      return columns;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      if (typeof(IDataControlMenu).IsInstanceOfType(bo))
      {
        Last_by = ((BaseBO)bo).Userid;
      }
      else if (typeof(KoreksiControl).IsInstanceOfType(bo))
      {
        Unitkey = ((KoreksiControl)bo).Unitkey;
        Nobakoreksi = ((KoreksiControl)bo).Nobakoreksi;
        Tglvalid = ((KoreksiControl)bo).Tglvalid;
        Blokid = ((KoreksiControl)bo).Blokid;
        Kdtans = ((KoreksiControl)bo).Kdtans;
      }
      else if (typeof(ViewasetSpesifikasiControl).IsInstanceOfType(bo))
      {
        Idbrg = ((ViewasetSpesifikasiControl)bo).Idbrg;
        Asetkey = ((ViewasetSpesifikasiControl)bo).Asetkey;
        Noreg = ((ViewasetSpesifikasiControl)bo).Noreg;
        Kdkib = ((ViewasetSpesifikasiControl)bo).Kdkib;
        Pengguna = ((ViewasetSpesifikasiControl)bo).Pengguna;
        Spesifikasi = ((ViewasetSpesifikasiControl)bo).Spesifikasi;
        Ket = ((ViewasetSpesifikasiControl)bo).Ket;
        Kdpemilik = ((ViewasetSpesifikasiControl)bo).Kdpemilik;
        Nmpemilik = ((ViewasetSpesifikasiControl)bo).Nmpemilik;
        Kdhak = ((ViewasetSpesifikasiControl)bo).Kdhak;
        Nmhak = ((ViewasetSpesifikasiControl)bo).Nmhak;
        Kdsatuan = ((ViewasetSpesifikasiControl)bo).Kdsatuan;
        Nmsatuan = ((ViewasetSpesifikasiControl)bo).Nmsatuan;
        Nofikat = ((ViewasetSpesifikasiControl)bo).Nofikat;
        Tgfikat = ((ViewasetSpesifikasiControl)bo).Tgfikat;
        Utara = ((ViewasetSpesifikasiControl)bo).Utara;
        Timur = ((ViewasetSpesifikasiControl)bo).Timur;
        Selatan = ((ViewasetSpesifikasiControl)bo).Selatan;
        Barat = ((ViewasetSpesifikasiControl)bo).Barat;
        Lokasi = ((ViewasetSpesifikasiControl)bo).Lokasi;
        Kdwarna = ((ViewasetSpesifikasiControl)bo).Kdwarna;
        Nmwarna = ((ViewasetSpesifikasiControl)bo).Nmwarna;
        Ukuran = ((ViewasetSpesifikasiControl)bo).Ukuran;
        Bahan = ((ViewasetSpesifikasiControl)bo).Bahan;
        Nopabrik = ((ViewasetSpesifikasiControl)bo).Nopabrik;
        Norangka = ((ViewasetSpesifikasiControl)bo).Norangka;
        Nopolisi = ((ViewasetSpesifikasiControl)bo).Nopolisi;
        Nobpkb = ((ViewasetSpesifikasiControl)bo).Nobpkb;
        Nomesin = ((ViewasetSpesifikasiControl)bo).Nomesin;
        Merktype = ((ViewasetSpesifikasiControl)bo).Merktype;
        Bertingkat = ((ViewasetSpesifikasiControl)bo).Bertingkat;
        Beton = ((ViewasetSpesifikasiControl)bo).Beton;
        Luaslt = ((ViewasetSpesifikasiControl)bo).Luaslt;
        Nodokgdg = ((ViewasetSpesifikasiControl)bo).Nodokgdg;
        Tgdokgdg = ((ViewasetSpesifikasiControl)bo).Tgdokgdg;
        Nokdtanah = ((ViewasetSpesifikasiControl)bo).Nokdtanah;
        Nodokjij = ((ViewasetSpesifikasiControl)bo).Nodokjij;
        Tgdokjij = ((ViewasetSpesifikasiControl)bo).Tgdokjij;
        Konstruksi = ((ViewasetSpesifikasiControl)bo).Konstruksi;
        Panjang = ((ViewasetSpesifikasiControl)bo).Panjang;
        Luas = ((ViewasetSpesifikasiControl)bo).Luas;
        Jdlpenerbit = ((ViewasetSpesifikasiControl)bo).Jdlpenerbit;
        Bkpencipta = ((ViewasetSpesifikasiControl)bo).Bkpencipta;
        Asaldaerah = ((ViewasetSpesifikasiControl)bo).Asaldaerah;
        Pencipta = ((ViewasetSpesifikasiControl)bo).Pencipta;
        Kdfisik = ((ViewasetSpesifikasiControl)bo).Kdfisik;
        Nmfisik = ((ViewasetSpesifikasiControl)bo).Nmfisik;
        Nodokkdp = ((ViewasetSpesifikasiControl)bo).Nodokkdp;
        Tgdokkdp = ((ViewasetSpesifikasiControl)bo).Tgdokkdp;
        Tgmulai = ((ViewasetSpesifikasiControl)bo).Tgmulai;
        Asalusul = ((ViewasetSpesifikasiControl)bo).Asalusul;
        Tahun = ((ViewasetSpesifikasiControl)bo).Tahun;

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
      List<KoreksidetspekControl> ListData = new List<KoreksidetspekControl>();
      foreach (KoreksidetspekControl dc in list)
      {
        ListData.Add(dc);
      }

      return ListData;
    }
    public new int Delete()
    {
      Status = -1;
      int n = ((BaseDataControlUI)this).Delete(BaseDataControl.DEFAULT);
      return n;
    }

    public const string GROUP_1 = "A. Spesifikasi 1";
    public const string GROUP_2 = "B. Spesifikasi 2";
    public const string GROUP_3 = "C. Spesifikasi 3";
    public const string GROUP_4 = "D. Spesifikasi 4";
    public const string GROUP_5 = "E. Spesifikasi 5";
    public const string GROUP_6 = "F. Spesifikasi 6";
    public override HashTableofParameterRow GetEntries()
    {
      bool enable = true;
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      
      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdpemilik=Pemilik"),
      GetList(new JmilikLookupControl()), "Kdpemilik=Nmpemilik", 50).SetAllowRefresh(false).SetEnable(enable).SetAllowEmpty(false).SetGroup(GROUP_1));
      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdsatuan=Satuan"),
      GetList(new SatuanLookupControl()), "Kdsatuan=Nmsatuan", 50).SetAllowRefresh(false).SetEnable(enable).SetAllowEmpty(false).SetGroup(GROUP_1));
      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdfisik=Fisik"),
      GetList(new JfisikLookupControl()), "Kdfisik=Nmfisik", 50).SetAllowRefresh(false).SetEnable(enable).SetAllowEmpty(false).SetGroup(GROUP_1));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nodokkdp=No Dokumen KDP"), true, 50).SetEnable(enable).SetGroup(GROUP_1).SetLength(30));
      hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tgdokkdp=Tgl Dokumen KDP"), true).SetEnable(enable).SetGroup(GROUP_1));
      hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tgmulai=Tanggal Mulai"), true).SetEnable(enable).SetGroup(GROUP_1));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Asalusul=Asal Usul"), true, 90).SetEnable(enable).SetGroup(GROUP_1));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Pengguna"), true, 90).SetEnable(enable).SetGroup(GROUP_1));
      hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Spesifikasi=Spesifikasi"), true, 3).SetEnable(enable).SetAllowEmpty(true).SetGroup(GROUP_1));
      hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Lokasi"), true, 3).SetEnable(enable).SetAllowEmpty(true).SetGroup(GROUP_1).SetLength(200));
      hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Ket=Keterangan"), true, 3).SetEnable(enable).SetAllowEmpty(true).SetGroup(GROUP_1));


      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdhak=Hak"),
      GetList(new JhakLookupControl()), "Kdhak=Nmhak", 50).SetAllowRefresh(false).SetEnable(enable).SetAllowEmpty(false).SetGroup(GROUP_2));
      hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Alamat"), true, 3).SetEnable(enable).SetAllowEmpty(true).SetGroup(GROUP_2).SetLength(200));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nofikat=Nomor Sertifikat"), true, 50).SetEnable(enable).SetGroup(GROUP_2).SetLength(50));
      hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tgfikat=Tanggal Sertifikat"), true).SetEnable(enable).SetGroup(GROUP_2));
      hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Luastnh=Luas Tanah"), true, 50).SetEnable(enable).SetGroup(GROUP_2).SetLength(50));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Utara"), true, 90).SetEnable(enable).SetGroup(GROUP_2).SetLength(200));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Timur"), true, 90).SetEnable(enable).SetGroup(GROUP_2).SetLength(200));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Selatan"), true, 90).SetEnable(enable).SetGroup(GROUP_2).SetLength(200));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Barat"), true, 90).SetEnable(enable).SetGroup(GROUP_2).SetLength(200));

      
      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdwarna=Warna"),
            GetList(new WarnaLookupControl()), "Kdwarna=Nmwarna", 50).SetAllowRefresh(false).SetEnable(enable).SetAllowEmpty(false).SetGroup(GROUP_3));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Merktype=Merk/Type"), true, 50).SetEnable(enable).SetGroup(GROUP_3));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Ukuran"), true, 50).SetEnable(enable).SetGroup(GROUP_3));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Bahan"), true, 50).SetEnable(enable).SetGroup(GROUP_3));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nopabrik=No. Pabrik"), true, 50).SetEnable(enable).SetGroup(GROUP_3));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nomesin=No. Mesin"), true, 50).SetEnable(enable).SetGroup(GROUP_3));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Norangka=No. Rangka"), true, 50).SetEnable(enable).SetGroup(GROUP_3));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nopolisi=No. Polisi"), true, 50).SetEnable(enable).SetGroup(GROUP_3));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nobpkb=No. BPKB"), true, 50).SetEnable(enable).SetGroup(GROUP_3));

      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nodokgdg=No Dok (KIB C)"), true, 50).SetEnable(enable).SetGroup(GROUP_2).SetLength(30));
      hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tgdokgdg=Tanggal Dok (KIB C)"), true).SetEnable(enable).SetGroup(GROUP_2));

      //hpars.Add(KdtanahLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enable)
      //  .SetAllowEmpty(true).SetGroup(GROUP_2));

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

      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nodokjij=No Dok (KIB D)"), true, 50).SetEnable(enable).SetGroup(GROUP_2).SetLength(30));
      hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tgdokjij=Tanggal Dok (KIB D)"), true).SetEnable(enable).SetGroup(GROUP_2));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Konstruksi"), true, 50).SetEnable(enable).SetGroup(GROUP_2).SetLength(50));
      hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Panjang"), true, 50).SetEnable(enable).SetGroup(GROUP_2));
      hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Lebar"), true, 50).SetEnable(enable).SetGroup(GROUP_2));
      hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Luas"), true, 50).SetEnable(enable).SetGroup(GROUP_2));

      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Jdlpenerbit=Buku Judul/Penerbit"), true, 90).SetEnable(enable)
        .SetGroup(GROUP_2));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Bkpencipta=Buku Pencipta"), true, 90).SetEnable(enable).SetGroup(GROUP_3));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Asaldaerah=Asal Daerah"), true, 90).SetEnable(enable).SetGroup(GROUP_3));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Pencipta"), true, 90).SetEnable(enable).SetGroup(GROUP_3));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Jenis"), true, 90).SetEnable(enable).SetGroup(GROUP_3));


      return hpars;

    }

    #endregion Methods 
  }
  #endregion Koreksidetspek
}

