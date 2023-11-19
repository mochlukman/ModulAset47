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
  #region Usadi.Valid49.BO.ViewasetRkbmdPengadaanControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class ViewasetRkbmdPengadaanControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
  {
    #region Properties
    public long Id { get; set; }
    public string Asetkey { get; set; }
    public string Kdaset { get; set; }
    public string Nmaset { get; set; }
    public string Kdobjekaset { get; set; }
    public string Nmobjekaset { get; set; }
    public string Kdkib { get; set; }
    public string Nmkib { get; set; }
    public int Jumlahaset { get; set; }
    public int Jumlahrb { get; set; }
    public int Jmltersedia { get; set; }
    public string Unitkey { get; set; }
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
      cViewListProperties.SortFields = new String[] {  };
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
    public new IList View()
    {
      string sql = @"
        exec [dbo].[WSPV_RKBMD_PENGADAAN]
		    @UNITKEY = N'{0}',
		    @KDKEGUNIT = N'{1}',
		    @THANG = N'{2}',
		    @KDASET = N'{3}'
      ";

      sql = string.Format(sql, Unitkey, Kdkegunit, Thang, Kdobjekaset);
      string[] fields = new string[] { "Id", "Asetkey", "Kdlevel", "Kdaset", "Nmaset", "Type", "Jumlahaset", "Jumlahrb", "Jmltersedia" };
      List<IDataControl> list = BaseDataAdapter.GetListDC(this, sql, fields);
      List<ViewasetRkbmdPengadaanControl> ListData = new List<ViewasetRkbmdPengadaanControl>();

      foreach (ViewasetRkbmdPengadaanControl dc in list)
      {
        ListData.Add(dc);
      }
      return ListData;
    }
    public new HashTableofParameterRow GetFilters()
    {
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      //hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdkib=Jenis Barang"),
      //GetList(new JnskibTransLookupControl()), "Kdkib=Nmkib", 55).SetAllowRefresh(true).SetEnable(true).SetAllowEmpty(false));
      hpars.Add(DaftasetObjekLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(true).SetAllowRefresh(true).SetAllowEmpty(false));
      return hpars;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Jumlahaset=Jumlah Barang"), typeof(int), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Jumlahrb=Rusak Berat"), typeof(int), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Jmltersedia=Barang yg Dapat Dioptimalisasikan"), typeof(int), 30, HorizontalAlign.Center));
      return columns;
    }
    #endregion Methods 
  }
  #endregion ViewasetRkbmdPengadaan
}

