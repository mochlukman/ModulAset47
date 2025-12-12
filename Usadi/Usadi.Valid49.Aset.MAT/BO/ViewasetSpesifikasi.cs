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
  #region Usadi.Valid49.BO.ViewasetSpesifikasiControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class ViewasetSpesifikasiControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
  {
    #region Properties
    public string Idbrg { get; set; }
    public string Unitkey { get; set; }
    public string Asetkey { get; set; }
    public string Kdaset { get; set; }
    public string Nmaset { get; set; }
    public string Dokperolehan { get; set; }
    public DateTime Tglperolehan { get; set; }
    public string Noreg { get; set; }
    public int Jumlah { get; set; }
    public decimal Nilai { get; set; }
    public string Kdpemilik { get; set; }
    public string Nmpemilik { get; set; }
    public string Kdhak { get; set; }
    public string Nmhak { get; set; }
    public string Kdsatuan { get; set; }
    public string Nmsatuan { get; set; }
    public string Kdfisik { get; set; }
    public string Nmfisik { get; set; }
    public string Bertingkat { get; set; }
    public string Beton { get; set; }
    public decimal Luaslt { get; set; }
    public string Nodokkdp { get; set; }
    public DateTime Tgdokkdp { get; set; }
    public DateTime Tgmulai { get; set; }
    public string Nokdtanah { get; set; }
    public string Alamat { get; set; }
    public new string Ket { get; set; }
    public string Kdkib { get; set; }
    public string Kdobjekaset { get; set; }
    public string Nmobjekaset { get; set; }

    public string Kdkon { get; set; }
    public string Nmkon { get; set; }
    public string Asalusul { get; set; }
    public string Pengguna { get; set; }
    public string Merktype { get; set; }
    public string Ukuran { get; set; }
    public decimal Luastnh { get; set; }
    public string Nofikat { get; set; }
    public DateTime Tgfikat { get; set; }
    public string Kdwarna { get; set; }
    public string Nopabrik { get; set; }
    public string Norangka { get; set; }
    public string Nopolisi { get; set; }
    public string Nobpkb { get; set; }
    public string Nomesin { get; set; }
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
    public string Judul { get; set; }
    public string Jenis { get; set; }
    public string Utara { get; set; }
    public string Timur { get; set; }
    public string Selatan { get; set; }
    public string Barat { get; set; }
    public string Kdklas { get; set; }
    public string Uraiklas { get; set; }
    public string Kdstatusaset { get; set; }
    public string Nmkib { get; set; }
    public string Lokasi { get; set; }
    public string Bahan { get; set; }
    public string Nmwarna { get; set; }



    public string Idkey { get { return Idbrg + Asetkey + Unitkey; } }

    #endregion Properties 

    #region Methods 
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.IDKey = "Idkey";
      cViewListProperties.IDProperty = "Idkey";
      cViewListProperties.SortFields = new String[] { };
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      cViewListProperties.PageSize = 30;
      return cViewListProperties;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      if (bo.GetProperty("Unitkey") != null)
      {
        Unitkey = bo.GetValue("Unitkey").ToString();
      }
    }
    public new HashTableofParameterRow GetFilters()
    {
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      //hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdkib=Jenis KIB"),
      //GetList(new JnskibTransLookupControl()), "Kdkib=Nmkib", 48).SetAllowRefresh(true).SetEnable(true).SetAllowEmpty(false));
      //hpars.Add(DaftasetKibLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(true).SetAllowEmpty(false));
      hpars.Add(DaftasetObjekLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(true).SetAllowRefresh(true).SetAllowEmpty(false));
      return hpars;
    }
    public new IList View()
    {
      string sql = @"
        exec [dbo].[WSPV_KOREKSIDETSPEK]
      @UNITKEY = N'{0}',
      @KDASET = N'{1}'
      ";

      sql = string.Format(sql, Unitkey, Kdobjekaset);
      string[] fields = new string[] { "Idbrg", "Unitkey", "Asetkey", "Kdaset", "Nmaset", "Tahun", "Noreg"
          , "Nilai", "Umeko", "Kdpemilik", "Nmpemilik", "Kdkon", "Nmkon", "Asalusul"
          , "Pengguna", "Kdhak","Nmhak","Kdsatuan", "Nmsatuan", "Merktype","Ukuran", "Bahan"
          , "Luastnh","Alamat", "Nofikat","Tgfikat"
          , "Kdwarna", "Nopabrik","Norangka","Nopolisi","Nobpkb","Nomesin"
          , "Bertingkat","Beton" ,"Luaslt", "Nodokgdg","Tgdokgdg","Konstruksi","Panjang","Lebar","Luas","Nodokjij","Tgdokjij"
          , "Jdlpenerbit" ,"Bkpencipta","Spesifikasi","Asaldaerah","Pencipta","Judul"
          , "Jenis","Kdfisik","Nmfisik"
          , "Nodokkdp","Tgdokkdp","Tgmulai","Nokdtanah"
          , "Utara","Timur","Selatan","Barat"
          , "Ket", "Kdklas", "Uraiklas", "Kdstatusaset", "Kdkib", "Nmkib", "Lokasi", "Nmwarna" };
      List<IDataControl> list = BaseDataAdapter.GetListDC(this, sql, fields);
      List<ViewasetSpesifikasiControl> ListData = new List<ViewasetSpesifikasiControl>();

      foreach (ViewasetSpesifikasiControl dc in list)
      {
        ListData.Add(dc);
      }
      return ListData;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 35, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun"), typeof(int), 15, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreg=No Register"), typeof(string), 15, HorizontalAlign.Center));
      //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Jumlah=Jumlah Barang"), typeof(decimal), 15, HorizontalAlign.Center));
      //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai=Nilai Perolehan"), typeof(decimal), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmpemilik=Pemilik"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmsatuan=Satuan"), typeof(string), 15, HorizontalAlign.Center));
      //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmhak=Hak"), typeof(string), 25, HorizontalAlign.Center));
      //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmfisik=Fisik"), typeof(string), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Luaslt=Luas Lantai"), typeof(decimal), 15, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Merktype=Merk"), typeof(decimal), 15, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nodokkdp=No. Dok KDP"), typeof(string), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tgdokkdp=Tgl Dok KDP"), typeof(DateTime), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tgmulai=Tgl Mulai"), typeof(DateTime), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Alamat"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 100, HorizontalAlign.Left));

      return columns;
    }
    #endregion Methods 
  }
  #endregion ViewasetSpesifikasi
}

