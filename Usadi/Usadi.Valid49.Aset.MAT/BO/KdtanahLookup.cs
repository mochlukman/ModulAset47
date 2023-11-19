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
  #region Usadi.Valid49.BO.KdtanahLookupControl, CoreNET.Common.BO
  [Serializable]
  public class KdtanahLookupControl :  KibaControl, IDataControlLookup, IHasJSScript
  {
    #region Singleton
    private static  KdtanahLookupControl _Instance = null;
    public static  KdtanahLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new  KdtanahLookupControl();
        }
        return _Instance;
      }
    }
    //public static void SetSessionListDataNull()
    //{
    //  string session_name = (new DaftdokControl()).XMLName; ;
    //  HttpContext.Current.Session[session_name] = null;
    //}
    //public static List<DaftdokControl> GetSessionListDataSingleton()
    //{
    //  string session_name = (new DaftdokControl()).XMLName; ;
    //  List<DaftdokControl> _ListData = (List<DaftdokControl>)HttpContext.Current.Session[session_name];
    //  if (_ListData == null)
    //  {
    //    try
    //    {
    //      KdtanahLookupControl dc = new KdtanahLookupControl();
    //      dc.SetPageKey();
    //      _ListData = (List<DaftdokControl>)dc.View(BaseDataControl.ALL);
    //    }
    //    catch(Exception){}
    //    HttpContext.Current.Session[session_name] = _ListData;
    //  }
    //  return _ListData;
    //}
    private static List<KibaControl> _ListData = null;
    public static void SetListDataNull()
    {
      _ListData = null;
    }
    public static List<KibaControl> GetListDataSingleton()
    {
      if (_ListData == null)
      {
        KdtanahLookupControl dc = new KdtanahLookupControl();
        dc.SetPageKey();
        _ListData = (List<KibaControl>)dc.View(BaseDataControl.LOOKUP);
      }
      return _ListData;
    }
    #endregion
    public KdtanahLookupControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLKIBA;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      return cViewListProperties;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      if (typeof(IDataControlMenu).IsInstanceOfType(bo))
      {
        Last_by = ((BaseBO)bo).Userid;
      }
      else if (bo.GetProperty("Unitkey") != null)
      {
        Unitkey = bo.GetValue("Unitkey").ToString();
        Kdunit = bo.GetValue("Kdunit").ToString();
        Nmunit = bo.GetValue("Nmunit").ToString();
      }
    }
    public new IList View()
    {
      IList list = this.View("Kdtanah");
      return list;
    }
    public new HashTableofParameterRow GetFilters()
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(DaftunitLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(false).SetAllowEmpty(false));

      return hpars;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nokdtanah=Kode Tanah"), typeof(string), 15, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun=Tahun Perolehan"), typeof(string), 15, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreg=No Register"), typeof(string), 15, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai"), typeof(decimal), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Alamat"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 100, HorizontalAlign.Left));
      return columns;
    }
    
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
        && string.IsNullOrEmpty((string)callerCtr.GetValue("Unitkey"));

      KdtanahLookupControl dclookup = new KdtanahLookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys =  new String[] { "Nokdtanah", "Nmkdtanah", "Nokdtanah" };
      string[] targets =  new String[] { "Nokdtanah", "Nmkdtanah", "Nokdtanah" };
      ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys,new int[] { 26, 63, 0 }, targets)
      {
        Label = "Tanah",
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
      return "Nokdtanah=Nmkdtanah";
    }
  }
  #endregion KdtanahLookup
}

