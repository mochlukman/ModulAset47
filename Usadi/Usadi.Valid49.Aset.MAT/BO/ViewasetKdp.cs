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
  #region Usadi.Valid49.BO.ViewasetKdpControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class ViewasetKdpControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
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
    public string Idkey { get { return Idbrg + Asetkey + Unitkey; } }

    #endregion Properties 

    #region Methods 
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.IDKey = "Idkey";
      cViewListProperties.IDProperty = "Idkey";
      cViewListProperties.SortFields = new String[] { "Kdaset", "Tahun", "Noreg" };
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      cViewListProperties.PageSize = 20;
      return cViewListProperties;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      if (bo.GetProperty("Unitkey") != null)
      {
        Unitkey = bo.GetValue("Unitkey").ToString();
      }
    }
    public new IList View()
    {
      string sql = @"
        exec [dbo].[WSPV_REKLASKDP]
		    @UNITKEY = N'{0}'
      ";

      sql = string.Format(sql, Unitkey);
      string[] fields = new string[] { "Idbrg", "Unitkey", "Asetkey", "Kdaset", "Nmaset", "Dokperolehan", "Tglperolehan"
        , "Tahun", "Noreg", "Jumlah", "Nilai", "Kdpemilik", "Nmpemilik", "Kdhak", "Nmhak", "Kdsatuan", "Nmsatuan", "Kdfisik"
        , "Nmfisik", "Bertingkat", "Beton", "Luaslt", "Nodokkdp", "Tgdokkdp", "Tgmulai", "Nokdtanah", "Alamat", "Ket", "Kdkib"};
      List<IDataControl> list = BaseDataAdapter.GetListDC(this, sql, fields);
      List<ViewasetKdpControl> ListData = new List<ViewasetKdpControl>();

      foreach (ViewasetKdpControl dc in list)
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
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Jumlah=Jumlah Barang"), typeof(decimal), 15, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai=Nilai Perolehan"), typeof(decimal), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmpemilik=Pemilik"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmsatuan=Satuan"), typeof(string), 15, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmhak=Hak"), typeof(string), 25, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmfisik=Fisik"), typeof(string), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Luaslt=Luas Lantai"), typeof(decimal), 15, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nodokkdp=No. Dok KDP"), typeof(string), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tgdokkdp=Tgl Dok KDP"), typeof(DateTime), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tgmulai=Tgl Mulai"), typeof(DateTime), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Alamat"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 100, HorizontalAlign.Left));

      return columns;
    }
    #endregion Methods 
  }
  #endregion ViewasetKdp
}

