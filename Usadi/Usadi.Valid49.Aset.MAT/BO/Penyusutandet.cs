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
  #region Usadi.Valid49.BO.PenyusutandetControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class PenyusutandetControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
  {
    #region Properties
    public long Id { get; set; }
    public string Noreg { get; set; }
    public int Masapakai { get; set; }
    public decimal Nilaiaset { get; set; }
    public decimal Nilaiasettbh { get; set; }
    public decimal Nilaiasethitung { get; set; }
    public decimal Umeko { get; set; }
    public decimal Umekohitung { get; set; }
    public decimal Umekotbh { get; set; }
    public decimal Umekosisa { get; set; }
    public decimal Nilaisusut { get; set; }
    public decimal Nilaiakumsusut { get; set; }
    public decimal Nilaibuku { get; set; }
    public string Kdtans { get; set; }
    public string Kdunit { get; set; }
    public string Nmunit { get; set; }
    public string Kdaset { get; set; }
    public string Nmaset { get; set; }
    public string Ket_bulan { get; set; }
    public string Nmtrans { get; set; }
    public new int Bulan { get; set; }
    public int Kdtahun { get;  set; }
    public string Kdbulan { get; set; }
    public int Tahunhitung { get; set; }
    public string Idbrg { get; set; }
    public string Unitkey { get; set; }
    public string Asetkey { get; set; }
    #endregion Properties 

    #region Methods 
    public PenyusutandetControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLPENYUSUTANDET;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Bulan", "Tahunhitung", "Idbrg", "Unitkey", "Asetkey"};
      cViewListProperties.IDKey = "Id";
      cViewListProperties.IDProperty = "Id";
      cViewListProperties.ReadOnlyFields = new String[] { "Bulan" };
      cViewListProperties.SortFields = new String[] { "Masapakai" };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      cViewListProperties.PageSize = 50;
      return cViewListProperties;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahunhitung=Tahun"), typeof(int), 10, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket_bulan=Bulan"), typeof(string), 15, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmtrans=Jenis Transaksi"), typeof(string), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilaiasettbh=Penambahan Nilai"), typeof(decimal), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Umekotbh=Penambahan Umur"), typeof(decimal), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilaiasethitung=Nilai Aset Hitung"), typeof(decimal), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Umekohitung=Umur Hitung"), typeof(decimal), 15, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Umekosisa=Sisa Umur"), typeof(decimal), 15, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilaisusut=Nilai Penyusutan"), typeof(decimal), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilaiakumsusut=Nilai Akumulasi"), typeof(decimal), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilaibuku=Nilai Buku"), typeof(decimal), 20, HorizontalAlign.Left));

      return columns;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      if (typeof(IDataControlMenu).IsInstanceOfType(bo))
      {
        Last_by = ((BaseBO)bo).Userid;
      }
      else if (typeof(PenyusutanControl).IsInstanceOfType(bo))
      {
        Idbrg = ((PenyusutanControl)bo).Idbrg;
        Bulan = Int32.Parse(((PenyusutanControl)bo).Kd_bulan.Trim());
        Tahun= ((PenyusutanControl)bo).Tahun;
        Kdtahun = Int32.Parse(((PenyusutanControl)bo).Kdtahun.Trim());
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
      List<PenyusutandetControl> ListData = new List<PenyusutandetControl>();
      foreach (PenyusutandetControl dc in list)
      {
        ListData.Add(dc);
      }

      return ListData;
    }
    #endregion Methods 
  }
  #endregion Penyusutandet
}

