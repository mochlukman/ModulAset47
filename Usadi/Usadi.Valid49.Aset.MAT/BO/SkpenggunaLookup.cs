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
  #region Usadi.Valid49.BO.SkpenggunaLookupControl, CoreNET.Common.BO
  [Serializable]
  public class SkpenggunaLookupControl : SkpenggunaControl, IDataControlLookup, IHasJSScript
  {
    #region Singleton
    private static SkpenggunaLookupControl _Instance = null;
    public static SkpenggunaLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new SkpenggunaLookupControl();
        }
        return _Instance;
      }
    }
    //public static void SetSessionListDataNull()
    //{
    //  string session_name = (new SkpenggunaControl()).XMLName; ;
    //  HttpContext.Current.Session[session_name] = null;
    //}
    //public static List<SkpenggunaControl> GetSessionListDataSingleton()
    //{
    //  string session_name = (new SkpenggunaControl()).XMLName; ;
    //  List<SkpenggunaControl> _ListData = (List<SkpenggunaControl>)HttpContext.Current.Session[session_name];
    //  if (_ListData == null)
    //  {
    //    try
    //    {
    //      SkpenggunaLookupControl dc = new SkpenggunaLookupControl();
    //      dc.SetPageKey();
    //      _ListData = (List<SkpenggunaControl>)dc.View(BaseDataControl.ALL);
    //    }
    //    catch(Exception){}
    //    HttpContext.Current.Session[session_name] = _ListData;
    //  }
    //  return _ListData;
    //}
    private static List<SkpenggunaControl> _ListData = null;
    public static void SetListDataNull()
    {
      _ListData = null;
    }
    public static List<SkpenggunaControl> GetListDataSingleton()
    {
      if (_ListData == null)
      {
        SkpenggunaLookupControl dc = new SkpenggunaLookupControl();
        dc.SetPageKey();
        _ListData = (List<SkpenggunaControl>)dc.View(BaseDataControl.LOOKUP);
      }
      return _ListData;
    }
    #endregion
    public SkpenggunaLookupControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLSKPENGGUNA;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      Unitkey = (string)bo.GetValue("Unitkey");
      Kdunit = (string)bo.GetValue("Kdunit");
      Nmunit = (string)bo.GetValue("Nmunit");
      Kdtahun = (string)bo.GetValue("Kdtahun");
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.ReadOnlyFields = new String[] { "Unitkey", "Kdunit", "Nmunit", "Kdtahun" };//Key in GetFilters should put here
      return cViewListProperties;
    }
    public new IList View()
    {
      IList list = this.View(BaseDataControl.LOOKUP);
      return list;
    }
    public new HashTableofParameterRow GetFilters()
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      return hpars;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdtahun=Tahun"), typeof(string), 10, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nodokumen=No Dokumen"), typeof(string), 30, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tgldokumen=Tanggal Dokumen"), typeof(DateTime), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 50, HorizontalAlign.Left));
      return columns;
    }
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
        && string.IsNullOrEmpty((string)callerCtr.GetValue("Nodokumen"));

      SkpenggunaLookupControl dclookup = new SkpenggunaLookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys = new String[] { "Kdtahun", "Nodokumen", "Ket" };
      string[] targets = new String[] { "Kdtahun", "Nodokumen", "Ket" };
      ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 20, 75, 0 }, targets)
      {
        Label = "No SK Pengguna",
        VisibleControls = new bool[] { true, true, !entry },
        AllowRefresh = !entry,
        DCLookup = dclookup,
        IsTree = false,
        SelectionCriteria = ParameterRow.SELECTION_CRITERIA_TYPE,
        //SelectionType = "D"
      };
      return par;
    }
    public string GetFieldValueMap()
    {
      return "Nodokumen=Nodokumen";
    }
  }
  #endregion SkpenggunaLookup
}

