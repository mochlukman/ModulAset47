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
  #region Usadi.Valid49.BO.SatwaktuLookupControl, CoreNET.Common.BO
  [Serializable]
  public class SatwaktuLookupControl :  SatwaktuControl, IDataControlLookup, IHasJSScript
  {
    #region Singleton
    private static  SatwaktuLookupControl _Instance = null;
    public static  SatwaktuLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new  SatwaktuLookupControl();
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
    //      SatwaktuLookupControl dc = new SatwaktuLookupControl();
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
        SatwaktuLookupControl dc = new SatwaktuLookupControl();
        dc.SetPageKey();
        _ListData = (List<SatuanControl>)dc.View(BaseDataControl.LOOKUP);
      }
      return _ListData;
    }
    #endregion
    public SatwaktuLookupControl()
    {
      XMLName = ConstantTablesAsetDM.XMLSATWAKTU;
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
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdsatwaktu=Kode"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmsatwaktu=Satuan Waktu"), typeof(string), 50, HorizontalAlign.Left));
      return columns;
    }
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
        && string.IsNullOrEmpty((string)callerCtr.GetValue("Kdsatwaktu"));

      SatwaktuLookupControl dclookup = new SatwaktuLookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys =  new String[] { "Kdsatwaktu", "Nmsatwaktu" };
      string[] targets =  new String[] { "Kdsatwaktu", "Nmsatwaktu" };
      ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 20, 75, 0 }, targets)
      {
        Label = "Satuan Waktu",
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
      return "Kdsatwaktu=Nmsatwaktu";
    }
  }
  #endregion SatwaktuLookup
}

