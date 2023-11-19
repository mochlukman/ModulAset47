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
  #region Usadi.Valid49.BO.HistrenovLookupControl, CoreNET.Common.BO
  [Serializable]
  public class HistrenovLookupControl :  HistrenovControl, IDataControlLookup, IHasJSScript
  {
    #region Singleton
    private static  HistrenovLookupControl _Instance = null;
    public static  HistrenovLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new  HistrenovLookupControl();
        }
        return _Instance;
      }
    }
    //public static void SetSessionListDataNull()
    //{
    //  string session_name = (new BeritaControl()).XMLName; ;
    //  HttpContext.Current.Session[session_name] = null;
    //}
    //public static List<BeritaControl> GetSessionListDataSingleton()
    //{
    //  string session_name = (new BeritaControl()).XMLName; ;
    //  List<BeritaControl> _ListData = (List<BeritaControl>)HttpContext.Current.Session[session_name];
    //  if (_ListData == null)
    //  {
    //    try
    //    {
    //      HistrenovLookupControl dc = new HistrenovLookupControl();
    //      dc.SetPageKey();
    //      _ListData = (List<BeritaControl>)dc.View(BaseDataControl.ALL);
    //    }
    //    catch(Exception){}
    //    HttpContext.Current.Session[session_name] = _ListData;
    //  }
    //  return _ListData;
    //}
    private static List<BeritaControl> _ListData = null;
    public static void SetListDataNull()
    {
      _ListData = null;
    }
    public static List<BeritaControl> GetListDataSingleton()
    {
      if (_ListData == null)
      {
        HistrenovLookupControl dc = new HistrenovLookupControl();
        dc.SetPageKey();
        _ListData = (List<BeritaControl>)dc.View(BaseDataControl.LOOKUP);
      }
      return _ListData;
    }
    #endregion
    public HistrenovLookupControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLHISTRENOV;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      return cViewListProperties;
    }
    public new IList View()
    {
      IList list = this.View(BaseDataControl.LOOKUP);
      return list;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      Unitkey = (string)bo.GetValue("Unitkey");
      Kdunit = (string)bo.GetValue("Kdunit");
      Nmunit = (string)bo.GetValue("Nmunit");
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
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nobarenov=No BAST"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglbarenov=Tanggal BAST"), typeof(DateTime), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmtrans=Jenis Transaksi"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nokontrak=No Kontrak"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai=Nilai Kontrak"), typeof(decimal), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Uraibarenov=Uraian BAST"), typeof(string), 50, HorizontalAlign.Left));
      return columns;
    }
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
        && string.IsNullOrEmpty((string)callerCtr.GetValue("Nobarenov"));

      HistrenovLookupControl dclookup = new HistrenovLookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys =  new String[] { "Tglbalookup", "Noba" };
      string[] targets =  new String[] { "Tglbalookup", "Noba=Nobarenov" };
      ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys,new int[] { 22, 65, 0 }, targets)
      {
        Label = "Nomor BAST",
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
      return "Tglbalookup=Noba";
    }
  }
  #endregion HistrenovLookup
}

