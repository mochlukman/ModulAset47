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
  #region Usadi.Valid49.BO.ViewasetLookupControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class ViewasetLookupControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
  {
    #region Properties
    public string Idbrg { get; set; }
    public string Unitkey { get; set; }
    public string Unitkey2 { get; set; }
    public string Asetkey { get; set; }
    public string Kdaset { get; set; }
    public string Nmaset { get; set; }
    public string Noreg { get; set; }
    public string Dokperolehan { get; set; }
    public DateTime Tglperolehan { get; set; }
    public decimal Nilai { get; set; }
    public decimal Umeko { get; set; }
    public string Kdpemilik { get; set; }
    public string Nmpemilik { get; set; }
    public string Kdkon { get; set; }
    public string Nmkon { get; set; }
    public string Asalusul { get; set; }
    public string Pengguna { get; set; }
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
    public string Kdtans { get; set; }
    public string Nopenilaian { get; set; }
    public decimal Nilpenilaian { get; set; }
    public string Idkey { get { return Idbrg + Asetkey + Unitkey; } }
    public string Idtarget { get { return Asetkey + '.' + Nilai.ToString("#,##0"); } }
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
      cViewListProperties.RefreshParent = true;
      return cViewListProperties;
    }
    
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 35, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun"), typeof(int), 15, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreg=No Register"), typeof(string), 15, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai"), typeof(decimal), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmpemilik=Pemilik"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmsatuan=Satuan"), typeof(string), 15, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmkon=Kondisi"), typeof(string), 15, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Asalusul=Asal Usul"), typeof(string), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Pengguna"), typeof(string), 25, HorizontalAlign.Left));
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
  #endregion ViewasetLookup

  #region ViewasetRenovLookup
  [Serializable]
  public class ViewasetRenovLookupControl : ViewasetLookupControl, IDataControlLookup, IHasJSScript
  {
    #region Methods 
    #region Singleton
    private static ViewasetRenovLookupControl _Instance = null;
    public static ViewasetRenovLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new ViewasetRenovLookupControl();
        }
        return _Instance;
      }
    }
    #endregion Singleton
    public new void SetFilterKey(BaseBO bo)
    {
      if (bo.GetProperty("Unitkey2") != null)
      {
        Unitkey = bo.GetValue("Unitkey2").ToString();
        Asetkey = bo.GetValue("Asetkey").ToString();
      }
    }
    public new IList View()
    {
      string sql = @"
        exec [dbo].[WSPV_HISTRENOVDET]
		    @UNITKEY = N'{0}',
		    @ASETKEY = N'{1}'
      ";

      sql = string.Format(sql, Unitkey, Asetkey);
      string[] fields = new string[] { "Idbrg", "Unitkey","Unitkey2", "Asetkey", "Kdaset", "Nmaset", "Tahun", "Noreg", "Dokperolehan"
        , "Tglperolehan", "Nilai", "Umeko", "Kdpemilik", "Nmpemilik", "Kdkon", "Nmkon", "Asalusul", "Pengguna", "Kdsatuan"
        , "Nmsatuan", "Spesifikasi", "Ukuran", "Bahan", "Nosertifikat", "Alamat", "Ket", "Kdklas", "Uraiklas", "Kdstatusaset"};
      List<IDataControl> list = BaseDataAdapter.GetListDC(this, sql, fields);
      List<ViewasetRenovLookupControl> ListData = new List<ViewasetRenovLookupControl>();

      foreach (ViewasetRenovLookupControl dc in list)
      {
        ListData.Add(dc);
      }
      return ListData;
    }
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      ViewasetRenovLookupControl dclookup = new ViewasetRenovLookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys = new String[] { "Idbrg", "Idtarget", "Nilai", "Umeko" };
      string[] targets = new String[] { "Idbrg", "Idtarget", "Nilai", "Umeko", "Asetkeyrenov=Asetkey" };
      ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 20, 75, 0, 0 }, targets)
      {
        Label = "Register Barang",
        VisibleControls = new bool[] { true, true, !entry },
        AllowRefresh = !entry,
        DCLookup = dclookup,
        IsTree = false,
        SelectionCriteria = ParameterRow.SELECTION_CRITERIA_TYPE,
        SelectionType = "D"
      };
      return par;
    }
    public string GetFieldValueMap()
    {
      return "Idbrg=Idtarget";
    }
    #endregion Methods 
  }
  #endregion ViewasetRenovLookup

  #region ViewasetKemitraanLookup
  [Serializable]
  public class ViewasetKemitraanLookupControl : ViewasetLookupControl, IDataControlLookup, IHasJSScript
  {
    #region Methods 
    #region Singleton
    private static ViewasetKemitraanLookupControl _Instance = null;
    public static ViewasetKemitraanLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new ViewasetKemitraanLookupControl();
        }
        return _Instance;
      }
    }
    #endregion Singleton
    public new void SetFilterKey(BaseBO bo)
    {
      if (bo.GetProperty("Unitkey") != null)
      {
        Unitkey = bo.GetValue("Unitkey").ToString();
        Kdtans = bo.GetValue("Kdtans").ToString();
      }
    }
    public new IList View()
    {
      string sql = @"
        exec [dbo].[WSPV_KEMITRAANDET]
		    @UNITKEY = N'{0}',
		    @KDTANS = N'{1}'
      ";

      sql = string.Format(sql, Unitkey, Kdtans);
      string[] fields = new string[] { "Idbrg", "Unitkey", "Asetkey", "Kdaset", "Nmaset", "Tahun", "Noreg"
        , "Nilai", "Umeko", "Kdpemilik", "Nmpemilik", "Kdkon", "Nmkon", "Asalusul"
        , "Pengguna", "Kdsatuan", "Nmsatuan","Spesifikasi", "Ukuran", "Bahan", "Nosertifikat", "Alamat", "Ket"
        , "Kdklas", "Uraiklas", "Kdstatusaset", "Kdkib", "Nmkib", "Nopenilaian", "Nilpenilaian" };
      List<IDataControl> list = BaseDataAdapter.GetListDC(this, sql, fields);
      List<ViewasetKemitraanLookupControl> ListData = new List<ViewasetKemitraanLookupControl>();

      foreach (ViewasetKemitraanLookupControl dc in list)
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
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai=Harga Perolehan"), typeof(decimal), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilpenilaian=Harga Penilaian"), typeof(decimal), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmpemilik=Pemilik"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmsatuan=Satuan"), typeof(string), 15, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmkon=Kondisi"), typeof(string), 15, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Asalusul=Asal Usul"), typeof(string), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Pengguna"), typeof(string), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Spesifikasi"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nosertifikat"), typeof(string), 35, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ukuran"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Bahan"), typeof(string), 15, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Alamat"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 100, HorizontalAlign.Left));

      return columns;
    }
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      ViewasetKemitraanLookupControl dclookup = new ViewasetKemitraanLookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys = new String[] { "Kdaset", "Nmaset", "Asetkey" };
      string[] targets = new String[] { "Kdaset", "Nmaset", "Asetkey", "Idbrg", "Nopenilaian", "Nilai=Nilpenilaian" };
      ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 30, 58, 0 }, targets)
      {
        Label = "Kode Barang",
        VisibleControls = new bool[] { true, true, !entry },
        AllowRefresh = !entry,
        DCLookup = dclookup,
        IsTree = false,
        SelectionCriteria = ParameterRow.SELECTION_CRITERIA_TYPE,
        SelectionType = "D"
      };
      return par;
    }
    public string GetFieldValueMap()
    {
      return "Kdaset=Nmaset";
    }
    #endregion Methods 
  }
  #endregion ViewasetKemitraanLookup
}

