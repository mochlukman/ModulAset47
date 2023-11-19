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
  #region Usadi.Valid49.BO.ViewasetRkbmdPemeliharaanControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class ViewasetRkbmdPemeliharaanControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
  {
    #region Properties
    public long Id { get; set; }
    public string Idbrg { get; set; }
    public string Unitkey { get; set; }
    public string Asetkey { get; set; }
    public string Kdaset { get; set; }
    public string Nmaset { get; set; }
    public string Kdobjekaset { get; set; }
    public string Nmobjekaset { get; set; }
    public string Noreg { get; set; }
    public decimal Nilai { get; set; }
    public decimal Umeko { get; set; }
    public string Kdpemilik { get; set; }
    public string Nmpemilik { get; set; }
    public string Kdkon { get; set; }
    public string Nmkon { get; set; }
    public string Asalusul { get; set; }
    public string Kdsatuan { get; set; }
    public string Nmsatuan { get; set; }
    public string Spesifikasi { get; set; }
    public string Ukuran { get; set; }
    public string Bahan { get; set; }
    public string Nosertifikat { get; set; }
    public string Alamat { get; set; }
    public new string Ket { get; set; }
    public string Kdklas { get; set; }
    public string Uraiklas { get; set; }
    public string Kdstatusaset { get; set; }
    public string Kdkib { get; set; }
    public string Nmkib { get; set; }
    public string Kdkegunit { get; set; }
    public string Thang { get; set; }
    #endregion Properties 

    #region Methods
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = "Daftar Kode Barang";
      cViewListProperties.IDKey = "Id";
      cViewListProperties.IDProperty = "Id";
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
        Kdkegunit = bo.GetValue("Kdkegunit").ToString();
        Thang = bo.GetValue("Thang").ToString();
      }
    }
    public new HashTableofParameterRow GetFilters()
    {
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      //hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdkib=Jenis Barang"),
      //GetList(new JnskibTransLookupControl()), "Kdkib=Nmkib", 48).SetAllowRefresh(true).SetEnable(true).SetAllowEmpty(false));
      //hpars.Add(DaftasetKibLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(true).SetAllowEmpty(false));
      //hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"),
      //GetList(new DaftasetObjekLookupControl()), "Kdaset=Kdnmaset", 110).SetAllowRefresh(true).SetEnable(true).SetAllowEmpty(false));
      hpars.Add(DaftasetObjekLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(true).SetAllowRefresh(true).SetAllowEmpty(false));
      return hpars;
    }
    public new IList View()
    {
      string sql = @"
        exec [dbo].[WSPV_RKBMD_PEMELIHARAAN]
		    @UNITKEY = N'{0}',
		    @KDKEGUNIT = N'{1}',
		    @THANG = N'{2}',
        @KDASET = N'{3}'
      ";

      sql = string.Format(sql, Unitkey, Kdkegunit, Thang, Kdobjekaset);
      string[] fields = new string[] { "Id", "Idbrg", "Unitkey", "Asetkey", "Kdaset", "Nmaset", "Tahun", "Noreg", "Nilai", "Umeko", "Kdpemilik", "Nmpemilik"
      , "Kdkon", "Nmkon", "Asalusul", "Kdsatuan", "Nmsatuan", "Spesifikasi", "Ukuran", "Bahan", "Nosertifikat", "Alamat", "Ket", "Kdklas", "Uraiklas"
      , "Kdstatusaset", "Kdkib", "Nmkib"};
      List<IDataControl> list = BaseDataAdapter.GetListDC(this, sql, fields);
      List<ViewasetRkbmdPemeliharaanControl> ListData = new List<ViewasetRkbmdPemeliharaanControl>();

      foreach (ViewasetRkbmdPemeliharaanControl dc in list)
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
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai=Nilai Perolehan"), typeof(decimal), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Umeko=Masa Pakai"), typeof(decimal), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmpemilik=Pemilik"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmkon=Kondisi"), typeof(string), 15, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmsatuan=Satuan"), typeof(string), 15, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Asalusul=Asal Usul"), typeof(string), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Spesifikasi"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nosertifikat"), typeof(string), 35, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ukuran"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Bahan"), typeof(string), 15, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Alamat"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 100, HorizontalAlign.Left));
      return columns;
    }
    #endregion Methods 
  }
  #endregion ViewasetRkbmdPemeliharaan
}

