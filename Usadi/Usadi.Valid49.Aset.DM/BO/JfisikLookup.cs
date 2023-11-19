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
  #region Usadi.Valid49.BO.JfisikLookupControl, CoreNET.Common.BO
  [Serializable]
  public class JfisikLookupControl :  JfisikControl, IDataControlLookup, IHasJSScript
  {
    #region Singleton
    private static  JfisikLookupControl _Instance = null;
    public static  JfisikLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new  JfisikLookupControl();
        }
        return _Instance;
      }
    }
    //public static void SetSessionListDataNull()
    //{
    //  string session_name = (new JfisikControl()).XMLName; ;
    //  HttpContext.Current.Session[session_name] = null;
    //}
    //public static List<JfisikControl> GetSessionListDataSingleton()
    //{
    //  string session_name = (new JfisikControl()).XMLName; ;
    //  List<JfisikControl> _ListData = (List<JfisikControl>)HttpContext.Current.Session[session_name];
    //  if (_ListData == null)
    //  {
    //    try
    //    {
    //      JfisikLookupControl dc = new JfisikLookupControl();
    //      dc.SetPageKey();
    //      _ListData = (List<JfisikControl>)dc.View(BaseDataControl.ALL);
    //    }
    //    catch(Exception){}
    //    HttpContext.Current.Session[session_name] = _ListData;
    //  }
    //  return _ListData;
    //}
    private static List<JfisikControl> _ListData = null;
    public static void SetListDataNull()
    {
      _ListData = null;
    }
    public static List<JfisikControl> GetListDataSingleton()
    {
      if (_ListData == null)
      {
        JfisikLookupControl dc = new JfisikLookupControl();
        dc.SetPageKey();
        _ListData = (List<JfisikControl>)dc.View(BaseDataControl.LOOKUP);
      }
      return _ListData;
    }
    #endregion
    public JfisikLookupControl()
    {
      XMLName = ConstantTablesAsetDM.XMLJFISIK;
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
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdfisik=Kode"), typeof(string), 30, HorizontalAlign.Left).SetEditable(false));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmfisik=Fisik"), typeof(string), 50, HorizontalAlign.Left).SetEditable(false));
      return columns;
    }
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
        && string.IsNullOrEmpty((string)callerCtr.GetValue("Kdfisik"));

      JfisikLookupControl dclookup = new JfisikLookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys =  new String[] { "Kdfisik", "Nmfisik" };
      string[] targets =  new String[] { "Kdfisik", "Nmfisik" };
      ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 20, 75, 0 }, targets)
      {
        Label = "Fisik",
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
      return "Kdfisik=Nmfisik";
    }
  }
  #endregion JfisikLookup
}

