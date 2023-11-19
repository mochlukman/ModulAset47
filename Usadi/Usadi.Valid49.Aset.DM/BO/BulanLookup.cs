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
  #region Usadi.Valid49.BO.BulanLookupControl, CoreNET.Common.BO
  [Serializable]
  public class BulanLookupControl :  BulanControl, IDataControlLookup, IHasJSScript
  {
    #region Singleton
    private static  BulanLookupControl _Instance = null;
    public static  BulanLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new  BulanLookupControl();
        }
        return _Instance;
      }
    }
    //public static void SetSessionListDataNull()
    //{
    //  string session_name = (new BulanControl()).XMLName; ;
    //  HttpContext.Current.Session[session_name] = null;
    //}
    //public static List<BulanControl> GetSessionListDataSingleton()
    //{
    //  string session_name = (new BulanControl()).XMLName; ;
    //  List<BulanControl> _ListData = (List<BulanControl>)HttpContext.Current.Session[session_name];
    //  if (_ListData == null)
    //  {
    //    try
    //    {
    //      BulanLookupControl dc = new BulanLookupControl();
    //      dc.SetPageKey();
    //      _ListData = (List<BulanControl>)dc.View(BaseDataControl.ALL);
    //    }
    //    catch(Exception){}
    //    HttpContext.Current.Session[session_name] = _ListData;
    //  }
    //  return _ListData;
    //}
    private static List<BulanControl> _ListData = null;
    public static void SetListDataNull()
    {
      _ListData = null;
    }
    public static List<BulanControl> GetListDataSingleton()
    {
      if (_ListData == null)
      {
        BulanLookupControl dc = new BulanLookupControl();
        dc.SetPageKey();
        _ListData = (List<BulanControl>)dc.View(BaseDataControl.LOOKUP);
      }
      return _ListData;
    }
    #endregion
    public BulanLookupControl()
    {
      XMLName = ConstantTablesAsetDM.XMLBULAN;
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
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kd_bulan=Kode"), typeof(string), 15, HorizontalAlign.Left).SetEditable(false));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket_bulan=Bulan"), typeof(string), 30, HorizontalAlign.Left).SetEditable(false));
      return columns;
    }
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
        && string.IsNullOrEmpty((string)callerCtr.GetValue("Kd_bulan"));

      BulanLookupControl dclookup = new BulanLookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys =  new String[] { "Kd_bulan", "Ket_bulan" };
      string[] targets =  new String[] { "Kd_bulan", "Ket_bulan" };
      ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys,new int[] { 20, 75, 0 }, targets)
      {
        Label = "Bulan",
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
      return "Kd_bulan=Ket_bulan";
    }
  }
  #endregion BulanLookup
}

