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
  #region Usadi.Valid49.BO.ReklasdetbrgControl, Usadi.Valid49.Aset.DM
  [Serializable]
  public class ReklasdetbrgControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
  {
    #region Properties
    public long Id { get; set; }
    public string Unitkey { get; set; }
    public string Kdunit { get; set; }
    public string Nmunit { get; set; }
    public string Asetkey { get; set; }
    public string Kdaset { get; set; }
    public string Nmaset { get; set; }
    public string Noreg { get; set; }
    public string Kdkib { get; set; }
    public string Kdpemilik { get; set; }
    public string Nmpemilik { get; set; }
    public string Asalusul { get; set; }
    public string Pengguna { get; set; }
    public new string Ket { get; set; }
    public string Kdhak { get; set; }
    public string Nmhak { get; set; }
    public decimal Luastnh { get; set; }
    public string Alamat { get; set; }
    public string Nofikat { get; set; }
    public DateTime Tgfikat { get; set; }
    public string Kdsatuan { get; set; }
    public string Nmsatuan { get; set; }
    public string Merktype { get; set; }
    public string Ukuran { get; set; }
    public string Bahan { get; set; }
    public string Kdwarna { get; set; }
    public string Nmwarna { get; set; }
    public string Nopabrik { get; set; }
    public string Norangka { get; set; }
    public string Nopolisi { get; set; }
    public string Nobpkb { get; set; }
    public string Nomesin { get; set; }
    public string Bertingkat { get; set; }
    public string Beton { get; set; }
    public decimal Luaslt { get; set; }
    public string Nodokgdg { get; set; }
    public DateTime Tgdokgdg { get; set; }
    public string Konstruksi { get; set; }
    public decimal Panjang { get; set; }
    public decimal Lebar { get; set; }
    public decimal Luas { get; set; }
    public string Nodokjij { get; set; }
    public DateTime Tgdokjij { get; set; }
    public string Jdlpenerbit { get; set; }
    public string Bkpencipta { get; set; }
    public string Spesifikasi { get; set; }
    public string Asaldaerah { get; set; }
    public string Pencipta { get; set; }
    public string Jenis { get; set; }
    public string Kdfisik { get; set; }
    public string Nmfisik { get; set; }
    public string Nodokkdp { get; set; }
    public DateTime Tgdokkdp { get; set; }
    public DateTime Tgmulai { get; set; }
    public string Nokdtanah { get; set; }
    public string Judul { get; set; }
    public DateTime Tglvalid { get; set; }
    public string Nmkdtanah { get; set; }
    public string Idbrg { get; set; }
    public int Uruthist { get; set; }
    public string Blokid { get; set; }
    #endregion Properties 

    #region Methods 
    public ReklasdetbrgControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLREKLASDETBRG;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Idbrg", "Uruthist" };
      cViewListProperties.IDKey = "Id";
      cViewListProperties.IDProperty = "Id";
      cViewListProperties.ReadOnlyFields = new String[] { };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_EDIT;
      cViewListProperties.PageSize = 20;
      cViewListProperties.RefreshFilter = true;

      if (Tglvalid != new DateTime() || Blokid == "1")
      {
        cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      }
      else
      {
        cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_EDIT;
      }
      return cViewListProperties;
    }
    public new IList View()
    {
      IList list = this.View("Reklaskdp");
      return list;
    }
    public new IList View(string label)
    {
      IList list = ((BaseDataControl)this).View(label);
      List<ReklasdetbrgControl> ListData = new List<ReklasdetbrgControl>();
      foreach (ReklasdetbrgControl dc in list)
      {
        ListData.Add(dc);
      }

      return ListData;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      if (typeof(IDataControlMenu).IsInstanceOfType(bo))
      {
        Last_by = ((BaseBO)bo).Userid;
      }
      else if (typeof(ReklasdetControl).IsInstanceOfType(bo))
      {
        Idbrg = ((ReklasdetControl)bo).Idbrg;
        Unitkey = ((ReklasdetControl)bo).Unitkey;
        Asetkey = ((ReklasdetControl)bo).Asetkey2;
        Tglvalid = ((ReklasdetControl)bo).Tglvalid;
        Kdkib = ((ReklasdetControl)bo).Kdkibaset2;
        Blokid = ((ReklasdetControl)bo).Blokid;
      }
    }
    public override DataControlFieldCollection GetColumns()
    {
      bool enable = true;

      if (Tglvalid != new DateTime() || Blokid == "1")
      {
        enable = false;
      }

      DataControlFieldCollection columns = new DataControlFieldCollection();
      if (Kdkib == "01")
      {
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center).SetVisible(enable));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 20, HorizontalAlign.Left));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 40, HorizontalAlign.Left));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun"), typeof(int), 15, HorizontalAlign.Center));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmpemilik=Pemilik"), typeof(string), 30, HorizontalAlign.Left));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Asalusul=Asal Usul"), typeof(string), 25, HorizontalAlign.Left));
        //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Pengguna"), typeof(string), 25, HorizontalAlign.Left));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmhak=Hak"), typeof(string), 15, HorizontalAlign.Center));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmsatuan=Satuan"), typeof(string), 15, HorizontalAlign.Center));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nofikat=Nomor Sertifikat"), typeof(string), 20, HorizontalAlign.Left));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tgfikat=Tanggal Sertifikat"), typeof(DateTime), 20, HorizontalAlign.Center));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Luastnh=Luas Tanah"), typeof(decimal), 15, HorizontalAlign.Left));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Alamat"), typeof(string), 50, HorizontalAlign.Left));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 100, HorizontalAlign.Left));
      }
      else if (Kdkib == "02")
      {
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center).SetVisible(enable));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 20, HorizontalAlign.Left));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 40, HorizontalAlign.Left));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun"), typeof(int), 15, HorizontalAlign.Center));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmpemilik=Pemilik"), typeof(string), 30, HorizontalAlign.Left));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Asalusul=Asal Usul"), typeof(string), 25, HorizontalAlign.Left));
        //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Pengguna"), typeof(string), 25, HorizontalAlign.Left));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmsatuan=Satuan"), typeof(string), 15, HorizontalAlign.Center));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmwarna=Warna"), typeof(string), 15, HorizontalAlign.Center));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Merktype=Merk/Type"), typeof(string), 25, HorizontalAlign.Left));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ukuran"), typeof(string), 15, HorizontalAlign.Center));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Bahan"), typeof(string), 15, HorizontalAlign.Center));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nopabrik=No Pabrik"), typeof(string), 20, HorizontalAlign.Left));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Norangka=No Rangka"), typeof(string), 25, HorizontalAlign.Left));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nopolisi=No Polisi"), typeof(string), 15, HorizontalAlign.Center));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nobpkb=No BPKB"), typeof(string), 15, HorizontalAlign.Center));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nomesin=No Mesin"), typeof(string), 20, HorizontalAlign.Left));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 100, HorizontalAlign.Left));
      }
      else if (Kdkib == "03")
      {
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center).SetVisible(enable));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 20, HorizontalAlign.Left));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 40, HorizontalAlign.Left));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun"), typeof(int), 15, HorizontalAlign.Center));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmpemilik=Pemilik"), typeof(string), 30, HorizontalAlign.Left));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Asalusul=Asal Usul"), typeof(string), 25, HorizontalAlign.Center));
        //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Pengguna"), typeof(string), 25, HorizontalAlign.Left));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmhak=Hak"), typeof(string), 25, HorizontalAlign.Center));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmsatuan=Satuan"), typeof(string), 15, HorizontalAlign.Center));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Luaslt=Luas Lantai"), typeof(decimal), 15, HorizontalAlign.Left));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Luastnh=Luas Tanah"), typeof(decimal), 15, HorizontalAlign.Left));
        //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Bertingkat=Bertingkat!"), typeof(string), 15, HorizontalAlign.Center));
        //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Beton=Beton!"), typeof(string), 15, HorizontalAlign.Center));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nodokgdg=Nomor Dokumen"), typeof(string), 20, HorizontalAlign.Left));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tgdokgdg=Tanggal Dokumen"), typeof(DateTime), 20, HorizontalAlign.Center));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Alamat"), typeof(string), 50, HorizontalAlign.Left));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 100, HorizontalAlign.Left));
      }
      else if (Kdkib == "04")
      {
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center).SetVisible(enable));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 20, HorizontalAlign.Left));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 40, HorizontalAlign.Left));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun"), typeof(int), 15, HorizontalAlign.Center));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmpemilik=Pemilik"), typeof(string), 30, HorizontalAlign.Left));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Asalusul=Asal Usul"), typeof(string), 25, HorizontalAlign.Center));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmhak=Hak"), typeof(string), 25, HorizontalAlign.Center));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmsatuan=Satuan"), typeof(string), 15, HorizontalAlign.Center));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nodokjij=Nomor Dokumen"), typeof(string), 20, HorizontalAlign.Left));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tgdokjij=Tanggal Dokumen"), typeof(DateTime), 20, HorizontalAlign.Center));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Konstruksi"), typeof(string), 30, HorizontalAlign.Left));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Panjang"), typeof(decimal), 15, HorizontalAlign.Left));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Lebar"), typeof(decimal), 15, HorizontalAlign.Left));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Luas"), typeof(decimal), 15, HorizontalAlign.Left));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Alamat"), typeof(string), 50, HorizontalAlign.Left));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 100, HorizontalAlign.Left));
      }
      else if (Kdkib == "05")
      {
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center).SetVisible(enable));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 20, HorizontalAlign.Left));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 40, HorizontalAlign.Left));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun"), typeof(int), 15, HorizontalAlign.Center));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmpemilik=Pemilik"), typeof(string), 30, HorizontalAlign.Left));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Asalusul=Asal Usul"), typeof(string), 25, HorizontalAlign.Center));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmsatuan=Satuan"), typeof(string), 15, HorizontalAlign.Center));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Jdlpenerbit=Buku Judul/Penerbit"), typeof(string), 30, HorizontalAlign.Left));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Bkpencipta=Buku Pencipta"), typeof(string), 30, HorizontalAlign.Left));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Spesifikasi"), typeof(string), 15, HorizontalAlign.Left));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Asaldaerah=Asal Daerah"), typeof(string), 15, HorizontalAlign.Left));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Pencipta"), typeof(string), 15, HorizontalAlign.Left));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Bahan"), typeof(string), 15, HorizontalAlign.Left));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Jenis"), typeof(string), 15, HorizontalAlign.Left));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ukuran"), typeof(string), 15, HorizontalAlign.Left));
        columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 100, HorizontalAlign.Left));
      }
      return columns;
    }
    public const string GROUP_1 = "A. Spesifikasi 1";
    public const string GROUP_2 = "B. Spesifikasi 2";
    public const string GROUP_3 = "C. Spesifikasi 3";
    public override HashTableofParameterRow GetEntries()
    {
      HashTableofParameterRow hpars = new HashTableofParameterRow();

      bool enable = true, kdpemilik_e = false, alamat_e = false, asaldaerah_e = false, asalusul_e = false, bahan_e = false, bertingkat_e = false, beton_e = false
        , bkpencipta_e = false, jdlpenerbit_e = false, jenis_e = false, kdfisik_e = false, kdhak_e = false, kdsatuan_e = false
        , kdwarna_e = false, ket_e = false, konstruksi_e = false, lebar_e = false, luas_e = false, luaslt_e = false, luastnh_e = false
        , merktype_e = false, nobpkb_e = false, nodokgdg_e = false, nodokjij_e = false, nofikat_e = false, nomesin_e = false
        , nopabrik_e = false, nopolisi_e = false, norangka_e = false, panjang_e = false, pencipta_e = false, spesifikasi_e = false
        , tgdokgdg_e = false, tgdokjij_e = false, tgfikat_e = false, ukuran_e = false
        , pengguna_e = false
        , kdtanah_e = false;

      if (Kdkib == "01")
      {
        kdpemilik_e = true;
        asalusul_e = true;
        //pengguna_e = true;
        ket_e = true;
        kdhak_e = true;
        luastnh_e = true;
        alamat_e = true;
        nofikat_e = true;
        tgfikat_e = true;
        kdsatuan_e = true;

        //Pengguna = null;
        //Merktype = null;
        //Ukuran = null;
        //Bahan = null;
        //Nomesin = null;
        //Nopabrik = null;
        //Nopolisi = null;
        //Norangka = null;
        //Nobpkb = null;
        //Kdwarna = null;
        //Nodokgdg = null;
        //Nodokjij = null;
        //Nodokkdp = null;
        //Konstruksi = null;
        //Bertingkat = null;
        //Beton = null;
        //Jdlpenerbit = null;
        //Bkpencipta = null;
        //Pencipta = null;
        //Spesifikasi = null;
        //Asaldaerah = null;
        //Jenis = null;
        //Judul = null;
        //Kdfisik = null;
        //Nokdtanah = null;
      }
      if (Kdkib == "02")
      {
        kdpemilik_e = true;
        asalusul_e = true;
        //pengguna_e = true;
        ket_e = true;
        merktype_e = true;
        ukuran_e = true;
        bahan_e = true;
        kdwarna_e = true;
        nopabrik_e = true;
        norangka_e = true;
        nopolisi_e = true;
        nobpkb_e = true;
        nomesin_e = true;
        kdsatuan_e = true;

        //Pengguna = null;
        //Kdhak = null;
        //Nofikat = null;
        //Nodokgdg = null;
        //Nodokjij = null;
        //Nodokkdp = null;
        //Konstruksi = null;
        //Bertingkat = null;
        //Beton = null;
        //Jdlpenerbit = null;
        //Bkpencipta = null;
        //Pencipta = null;
        //Spesifikasi = null;
        //Asaldaerah = null;
        //Jenis = null;
        //Judul = null;
        //Kdfisik = null;
        //Nokdtanah = null;
        //Alamat = null;
      }
      if (Kdkib == "03")
      {
        kdpemilik_e = true;
        asalusul_e = true;
        //pengguna_e = true;
        ket_e = true;
        kdhak_e = true;
        bertingkat_e = true;
        beton_e = true;
        luaslt_e = true;
        alamat_e = true;
        nodokgdg_e = true;
        tgdokgdg_e = true;
        luastnh_e = true;
        kdtanah_e = true;
        kdsatuan_e = true;

        //Pengguna = null;
        //Nofikat = null;
        //Kdfisik = null;
        //Kdwarna = null;
        //Merktype = null;
        //Ukuran = null;
        //Bahan = null;
        //Nomesin = null;
        //Nopabrik = null;
        //Nopolisi = null;
        //Norangka = null;
        //Nobpkb = null;
        //Nodokjij = null;
        //Konstruksi = null;
        //Nodokkdp = null;
        //Bkpencipta = null;
        //Jdlpenerbit = null;
        //Jenis = null;
        //Spesifikasi = null;
        //Pencipta = null;
        //Asaldaerah = null;
        //Judul = null;
      }
      if (Kdkib == "04")
      {
        kdpemilik_e = true;
        asalusul_e = true;
        ket_e = true;
        kdhak_e = true;
        konstruksi_e = true;
        panjang_e = true;
        lebar_e = true;
        luas_e = true;
        alamat_e = true;
        nodokjij_e = true;
        tgdokjij_e = true;
        kdtanah_e = true;
        kdsatuan_e = true;

        //Pengguna = null;
        //Nofikat = null;
        //Kdfisik = null;
        //Kdwarna = null;
        //Merktype = null;
        //Ukuran = null;
        //Bahan = null;
        //Nomesin = null;
        //Nopabrik = null;
        //Nopolisi = null;
        //Norangka = null;
        //Nobpkb = null;
        //Nodokgdg = null;
        //Bertingkat = null;
        //Beton = null;
        //Nodokkdp = null;
        //Bkpencipta = null;
        //Jdlpenerbit = null;
        //Jenis = null;
        //Spesifikasi = null;
        //Pencipta = null;
        //Asaldaerah = null;
        //Judul = null;
      }
      if (Kdkib == "05")
      {
        kdpemilik_e = true;
        asalusul_e = true;
        ket_e = true;
        jdlpenerbit_e = true;
        bkpencipta_e = true;
        spesifikasi_e = true;
        asaldaerah_e = true;
        pencipta_e = true;
        bahan_e = true;
        jenis_e = true;
        ukuran_e = true;
        kdsatuan_e = true;

        //Pengguna = null;
        //Nofikat = null;
        //Kdfisik = null;
        //Kdhak = null;
        //Kdwarna = null;
        //Merktype = null;
        //Nomesin = null;
        //Nopabrik = null;
        //Nopolisi = null;
        //Norangka = null;
        //Nobpkb = null;
        //Nodokgdg = null;
        //Nodokjij = null;
        //Konstruksi = null;
        //Bertingkat = null;
        //Beton = null;
        //Nodokkdp = null;
        //Judul = null;
        //Nokdtanah = null;
        //Alamat = null;
      }

      ReklasdetbrgControl cReklasdetbrgGetkdtanah = new ReklasdetbrgControl();
      cReklasdetbrgGetkdtanah.Nokdtanah = Nokdtanah;
      cReklasdetbrgGetkdtanah.Load("Kdtanah");
      Nmkdtanah = cReklasdetbrgGetkdtanah.Nmkdtanah;

      //kib all group 1
      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdpemilik=Pemilik"),
      GetList(new JmilikLookupControl()), "Kdpemilik=Nmpemilik", 50).SetEnable(enable).SetAllowEmpty(false)
        .SetVisible(kdpemilik_e).SetGroup(GROUP_1));
      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdhak=Hak"),
      GetList(new JhakLookupControl()), "Kdhak=Nmhak", 50).SetEnable(enable).SetAllowEmpty(false)
        .SetVisible(kdhak_e).SetGroup(GROUP_1));
      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdsatuan=Satuan"),
      GetList(new SatuanLookupControl()), "Kdsatuan=Nmsatuan", 50).SetEnable(enable).SetAllowEmpty(false)
        .SetVisible(kdsatuan_e).SetGroup(GROUP_1));
      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdwarna=Warna"),
      GetList(new WarnaLookupControl()), "Kdwarna=Nmwarna", 50).SetEnable(enable).SetAllowEmpty(false)
        .SetVisible(kdwarna_e).SetGroup(GROUP_1));
      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdfisik=Fisik"),
      GetList(new JfisikLookupControl()), "Kdfisik=Nmfisik", 50).SetEnable(enable).SetAllowEmpty(false)
        .SetVisible(kdfisik_e).SetGroup(GROUP_1));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Asalusul=Asal Usul"), true, 90).SetEnable(enable)
        .SetVisible(asalusul_e).SetGroup(GROUP_1));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Pengguna"), true, 90).SetEnable(enable)
        .SetVisible(pengguna_e).SetGroup(GROUP_1));

      //kiba
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nofikat=Nomor Sertifikat"), true, 50).SetEnable(enable)
        .SetVisible(nofikat_e).SetGroup(GROUP_2));
      hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tgfikat=Tanggal Sertifikat"), true).SetEnable(enable)
        .SetVisible(tgfikat_e).SetGroup(GROUP_2));

      //kibb
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Merktype=Merk/Type"), true, 50).SetEnable(enable)
        .SetVisible(merktype_e).SetGroup(GROUP_2));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nopabrik=No. Pabrik"), true, 50).SetEnable(enable)
        .SetVisible(nopabrik_e).SetGroup(GROUP_2));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nomesin=No. Mesin"), true, 50).SetEnable(enable)
        .SetVisible(nomesin_e).SetGroup(GROUP_2));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Norangka=No. Rangka"), true, 50).SetEnable(enable)
        .SetVisible(norangka_e).SetGroup(GROUP_2));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nopolisi=No. Polisi"), true, 50).SetEnable(enable)
        .SetVisible(nopolisi_e).SetGroup(GROUP_2));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nobpkb=No. BPKB"), true, 50).SetEnable(enable)
        .SetVisible(nobpkb_e).SetGroup(GROUP_2));

      //kibc
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nodokgdg=Nomor Dokumen"), true, 50).SetEnable(enable)
        .SetVisible(nodokgdg_e).SetGroup(GROUP_2));
      hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tgdokgdg=Tanggal Dokumen"), true).SetEnable(enable)
        .SetVisible(tgdokgdg_e).SetGroup(GROUP_2));

      //kibd
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nodokjij=Nomor Dokumen"), true, 50).SetEnable(enable)
        .SetVisible(nodokjij_e).SetGroup(GROUP_2));
      hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tgdokjij=Tanggal Dokumen"), true).SetEnable(enable)
        .SetVisible(tgdokjij_e).SetGroup(GROUP_2));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Konstruksi"), true, 50).SetEnable(enable)
        .SetVisible(konstruksi_e).SetGroup(GROUP_2));
      hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Panjang"), true, 50).SetEnable(enable)
        .SetVisible(panjang_e).SetGroup(GROUP_2));
      hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Lebar"), true, 50).SetEnable(enable)
        .SetVisible(lebar_e).SetGroup(GROUP_2));
      hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Luas"), true, 50).SetEnable(enable)
        .SetVisible(luas_e).SetGroup(GROUP_2));

      //kibe
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Jdlpenerbit=Buku Judul/Penerbit"), true, 90).SetEnable(enable)
        .SetVisible(jdlpenerbit_e).SetGroup(GROUP_2));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Bkpencipta=Buku Pencipta"), true, 90).SetEnable(enable)
        .SetVisible(bkpencipta_e).SetGroup(GROUP_2));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Asaldaerah=Asal Daerah"), true, 90).SetEnable(enable)
        .SetVisible(asaldaerah_e).SetGroup(GROUP_2));

      // kib all group 2
      hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Luastnh=Luas Tanah"), true, 50).SetEnable(enable)
        .SetVisible(luastnh_e).SetGroup(GROUP_2));
      hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Luaslt=Luas Lantai"), true, 50).SetEnable(enable)
        .SetVisible(luaslt_e).SetGroup(GROUP_2));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Pencipta"), true, 90).SetEnable(enable)
        .SetVisible(pencipta_e).SetGroup(GROUP_2));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Spesifikasi"), true, 90).SetEnable(enable)
        .SetVisible(spesifikasi_e).SetGroup(GROUP_2));

      hpars.Add(KdtanahLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enable).SetAllowEmpty(true)
        .SetVisible(kdtanah_e).SetGroup(GROUP_2));
      ArrayList listBertingkat = new ArrayList(new ParamControl[] {
        new ParamControl() {  Kdpar="1",Nmpar="Bertingkat "}
        ,new ParamControl() { Kdpar="0",Nmpar="Tidak "}
      });
      hpars.Add(new ParameterRow(ConstantDict.GetColumnTitleEntry("Bertingkat=Bertingkat"), ParameterRow.MODE_TYPE,
        listBertingkat, "Kdpar=Nmpar", 70).SetEnable(enable).SetEditable(enable).SetVisible(bertingkat_e).SetGroup(GROUP_2));

      ArrayList listBeton = new ArrayList(new ParamControl[] {
        new ParamControl() {  Kdpar="1",Nmpar="Beton "}
        ,new ParamControl() { Kdpar="0",Nmpar="Tidak "}
      });
      hpars.Add(new ParameterRow(ConstantDict.GetColumnTitleEntry("Beton=Beton"), ParameterRow.MODE_TYPE,
        listBeton, "Kdpar=Nmpar", 70).SetEnable(enable).SetEditable(enable).SetVisible(beton_e).SetGroup(GROUP_2));

      //kib all group 3
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Bahan"), true, 50).SetEnable(enable)
        .SetVisible(bahan_e).SetGroup(GROUP_3));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Jenis"), true, 50).SetEnable(enable)
        .SetVisible(jenis_e).SetGroup(GROUP_3));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Ukuran"), true, 50).SetEnable(enable)
        .SetVisible(ukuran_e).SetGroup(GROUP_3));
      hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Alamat"), true, 3).SetEnable(enable).SetAllowEmpty(true)
        .SetVisible(alamat_e).SetGroup(GROUP_3));
      hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Ket=Keterangan"), true, 3).SetEnable(enable).SetAllowEmpty(true)
        .SetVisible(ket_e).SetGroup(GROUP_3));
      return hpars;

    }

    public new int Update()
    {
      int n = 0;
      try
      {
        if(Kdkib == "01")
        {
          base.Update("Kiba");
        }
        else if (Kdkib == "02")
        {
          base.Update("Kibb");
        }
        else if (Kdkib == "03")
        {
          base.Update("Kibc");
        }
        else if (Kdkib == "04")
        {
          base.Update("Kibd");
        }
        else if (Kdkib == "05")
        {
          base.Update("Kibe");
        }
      }
      catch (Exception ex)
      {

        if (ex.Message.Contains("FK_ASET_KIBSPESIFIKASI_JMILIK"))
        {
          string msg = "Gagal menyimpan data : kolom PEMILIK tidak boleh kosong";
          msg = string.Format(msg);
          throw new Exception(msg);
        }
        else if (ex.Message.Contains("FK_ASET_KIBSPESIFIKASI_JHAK"))
        {
          string msg = "Gagal menyimpan data : kolom HAK tidak boleh kosong";
          msg = string.Format(msg);
          throw new Exception(msg);
        }
        else if (ex.Message.Contains("FK_ASET_KIBSPESIFIKASI_SATUAN"))
        {
          string msg = "Gagal menyimpan data : kolom SATUAN tidak boleh kosong";
          msg = string.Format(msg);
          throw new Exception(msg);
        }
        else if (ex.Message.Contains("FK_ASET_KIBSPESIFIKASI_WARNA"))
        {
          string msg = "Gagal menyimpan data : kolom WARNA tidak boleh kosong";
          msg = string.Format(msg);
          throw new Exception(msg);
        }
      }
      return n;
    }

    #endregion Methods 
  }
  #endregion Reklasdetbrg
}

