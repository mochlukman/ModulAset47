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
  #region Usadi.Valid49.BO.KemitraanLookupControl, CoreNET.Common.BO
  [Serializable]
  public class KemitraanLookupControl :  KemitraanControl, IDataControlLookup, IHasJSScript
  {
    #region Singleton
    private static  KemitraanLookupControl _Instance = null;
    public static  KemitraanLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new  KemitraanLookupControl();
        }
        return _Instance;
      }
    }
    //public static void SetSessionListDataNull()
    //{
    //  string session_name = (new KemitraanControl()).XMLName; ;
    //  HttpContext.Current.Session[session_name] = null;
    //}
    //public static List<KemitraanControl> GetSessionListDataSingleton()
    //{
    //  string session_name = (new KemitraanControl()).XMLName; ;
    //  List<KemitraanControl> _ListData = (List<KemitraanControl>)HttpContext.Current.Session[session_name];
    //  if (_ListData == null)
    //  {
    //    try
    //    {
    //      KemitraanLookupControl dc = new KemitraanLookupControl();
    //      dc.SetPageKey();
    //      _ListData = (List<KemitraanControl>)dc.View(BaseDataControl.ALL);
    //    }
    //    catch(Exception){}
    //    HttpContext.Current.Session[session_name] = _ListData;
    //  }
    //  return _ListData;
    //}
    private static List<KemitraanControl> _ListData = null;
    public static void SetListDataNull()
    {
      _ListData = null;
    }
    public static List<KemitraanControl> GetListDataSingleton()
    {
      if (_ListData == null)
      {
        KemitraanLookupControl dc = new KemitraanLookupControl();
        dc.SetPageKey();
        _ListData = (List<KemitraanControl>)dc.View(BaseDataControl.LOOKUP);
      }
      return _ListData;
    }
    #endregion
    public KemitraanLookupControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLKEMITRAAN;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      return cViewListProperties;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      if (bo.GetProperty("Unitkey") != null)
      {
        Unitkey = bo.GetValue("Unitkey").ToString();
        Kdunit = bo.GetValue("Kdunit").ToString();
        Nmunit = bo.GetValue("Nmunit").ToString();
        Kdtans = bo.GetValue("Kdtans").ToString();
      }
    }
    public new HashTableofParameterRow GetFilters()
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(DaftunitLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetAllowEmpty(false));
      return hpars;
    }
    public new IList View()
    {
      IList list = this.View(BaseDataControl.LOOKUP);
      return list;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nodokumen=No Dokumen"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmtrans=Jenis Transaksi"), typeof(string), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglawal=Tanggal Awal"), typeof(DateTime), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglakhir=Tanggal Akhir"), typeof(DateTime), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai=Harga"), typeof(decimal), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 100, HorizontalAlign.Left));
      return columns;
    }
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
        && string.IsNullOrEmpty((string)callerCtr.GetValue("Unitkey"));

      KemitraanLookupControl dclookup = new KemitraanLookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys =  new String[] { "Iddokumenawal", "Nodokumenawal", "Iddokumen" };
      string[] targets =  new String[] { "Iddokumenawal=Iddokumen", "Nodokumenawal=Nodokumen", "Iddokumen" };
      ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys,new int[] { 26, 63, 0 }, targets)
      {
        Label = "Kontrak Awal",
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
      return "Iddokumenawal=Nodokumenawal";
    }
  }
  #endregion KemitraanLookup
}

