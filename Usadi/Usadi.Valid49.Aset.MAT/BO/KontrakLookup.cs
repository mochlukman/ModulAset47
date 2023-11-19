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
  #region Usadi.Valid49.BO.KontrakLookupControl, CoreNET.Common.BO
  [Serializable]
  public class KontrakLookupControl :  KontrakControl, IDataControlLookup, IHasJSScript
  {
    #region Singleton
    private static  KontrakLookupControl _Instance = null;
    public static  KontrakLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new  KontrakLookupControl();
        }
        return _Instance;
      }
    }
    //public static void SetSessionListDataNull()
    //{
    //  string session_name = (new KontrakControl()).XMLName; ;
    //  HttpContext.Current.Session[session_name] = null;
    //}
    //public static List<KontrakControl> GetSessionListDataSingleton()
    //{
    //  string session_name = (new KontrakControl()).XMLName; ;
    //  List<KontrakControl> _ListData = (List<KontrakControl>)HttpContext.Current.Session[session_name];
    //  if (_ListData == null)
    //  {
    //    try
    //    {
    //      KontrakLookupControl dc = new KontrakLookupControl();
    //      dc.SetPageKey();
    //      _ListData = (List<KontrakControl>)dc.View(BaseDataControl.ALL);
    //    }
    //    catch(Exception){}
    //    HttpContext.Current.Session[session_name] = _ListData;
    //  }
    //  return _ListData;
    //}
    private static List<KontrakControl> _ListData = null;
    public static void SetListDataNull()
    {
      _ListData = null;
    }
    public static List<KontrakControl> GetListDataSingleton()
    {
      if (_ListData == null)
      {
        KontrakLookupControl dc = new KontrakLookupControl();
        dc.SetPageKey();
        _ListData = (List<KontrakControl>)dc.View(BaseDataControl.LOOKUP);
      }
      return _ListData;
    }
    #endregion
    public KontrakLookupControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLKONTRAK;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      return cViewListProperties;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      Unitkey = (string)bo.GetValue("Unitkey");
      Kdunit = (string)bo.GetValue("Kdunit");
      Nmunit = (string)bo.GetValue("Nmunit");
      Kdtahap = (string)bo.GetValue("Kdtahap");
      Kdkegunit = (string)bo.GetValue("Kdkegunit");
      Nukeg = (string)bo.GetValue("Nukeg");
      Nmkegunit = (string)bo.GetValue("Nmkegunit");
    }
    public new IList View()
    {
      IList list = this.View(BaseDataControl.LOOKUP);
      return list;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nokontrak=Nomor Kontrak"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglkon=Tanggal Kontrak"), typeof(DateTime), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nminst=Instansi"), typeof(string), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai=Nilai Kontrak"), typeof(decimal), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Uraian"), typeof(string), 50, HorizontalAlign.Left));
      return columns;
    }
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
        && string.IsNullOrEmpty((string)callerCtr.GetValue("Nokontrak"));

      KontrakLookupControl dclookup = new KontrakLookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys =  new String[] { "Nokontrak", "Nminst", "Uraian" };
      string[] targets =  new String[] { "Nokontrak", "Nminst", "Uraiba=Uraian", "Uraibarenov=Uraian" };
      ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 20, 75, 0 }, targets)
      {
        Label = "Kontrak",
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
      return "Nokontrak=Nminst";
    }
  }
  #endregion KontrakLookup
}

