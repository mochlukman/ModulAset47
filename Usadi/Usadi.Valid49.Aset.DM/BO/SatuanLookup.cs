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
  #region Usadi.Valid49.BO.SatuanLookupControl, CoreNET.Common.BO
  [Serializable]
  public class SatuanLookupControl :  SatuanControl, IDataControlLookup, IHasJSScript
  {
    #region Singleton
    private static  SatuanLookupControl _Instance = null;
    public static  SatuanLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new  SatuanLookupControl();
        }
        return _Instance;
      }
    }
    //public static void SetSessionListDataNull()
    //{
    //  string session_name = (new SatuanControl()).XMLName; ;
    //  HttpContext.Current.Session[session_name] = null;
    //}
    //public static List<SatuanControl> GetSessionListDataSingleton()
    //{
    //  string session_name = (new SatuanControl()).XMLName; ;
    //  List<SatuanControl> _ListData = (List<SatuanControl>)HttpContext.Current.Session[session_name];
    //  if (_ListData == null)
    //  {
    //    try
    //    {
    //      SatuanLookupControl dc = new SatuanLookupControl();
    //      dc.SetPageKey();
    //      _ListData = (List<SatuanControl>)dc.View(BaseDataControl.ALL);
    //    }
    //    catch(Exception){}
    //    HttpContext.Current.Session[session_name] = _ListData;
    //  }
    //  return _ListData;
    //}
    private static List<SatuanControl> _ListData = null;
    public static void SetListDataNull()
    {
      _ListData = null;
    }
    public static List<SatuanControl> GetListDataSingleton()
    {
      if (_ListData == null)
      {
        SatuanLookupControl dc = new SatuanLookupControl();
        dc.SetPageKey();
        _ListData = (List<SatuanControl>)dc.View(BaseDataControl.LOOKUP);
      }
      return _ListData;
    }
    #endregion
    public SatuanLookupControl()
    {
      XMLName = ConstantTablesAsetDM.XMLSATUAN;
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
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdsatuan=Kode"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmsatuan=Satuan"), typeof(string), 50, HorizontalAlign.Left));
      return columns;
    }
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
        && string.IsNullOrEmpty((string)callerCtr.GetValue("Kdsatuan"));

      SatuanLookupControl dclookup = new SatuanLookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys =  new String[] { "Kdsatuan", "Nmsatuan" };
      string[] targets =  new String[] { "Kdsatuan", "Nmsatuan" };
      ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 20, 75, 0 }, targets)
      {
        Label = "Satuan",
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
      return "Kdsatuan=Nmsatuan";
    }
  }
  #endregion SatuanLookup
}

