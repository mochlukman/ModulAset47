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
  #region Usadi.Valid49.BO.WarnaLookupControl, CoreNET.Common.BO
  [Serializable]
  public class WarnaLookupControl :  WarnaControl, IDataControlLookup, IHasJSScript
  {
    #region Singleton
    private static  WarnaLookupControl _Instance = null;
    public static  WarnaLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new  WarnaLookupControl();
        }
        return _Instance;
      }
    }
    //public static void SetSessionListDataNull()
    //{
    //  string session_name = (new WarnaControl()).XMLName; ;
    //  HttpContext.Current.Session[session_name] = null;
    //}
    //public static List<WarnaControl> GetSessionListDataSingleton()
    //{
    //  string session_name = (new WarnaControl()).XMLName; ;
    //  List<WarnaControl> _ListData = (List<WarnaControl>)HttpContext.Current.Session[session_name];
    //  if (_ListData == null)
    //  {
    //    try
    //    {
    //      WarnaLookupControl dc = new WarnaLookupControl();
    //      dc.SetPageKey();
    //      _ListData = (List<WarnaControl>)dc.View(BaseDataControl.ALL);
    //    }
    //    catch(Exception){}
    //    HttpContext.Current.Session[session_name] = _ListData;
    //  }
    //  return _ListData;
    //}
    private static List<WarnaControl> _ListData = null;
    public static void SetListDataNull()
    {
      _ListData = null;
    }
    public static List<WarnaControl> GetListDataSingleton()
    {
      if (_ListData == null)
      {
        WarnaLookupControl dc = new WarnaLookupControl();
        dc.SetPageKey();
        _ListData = (List<WarnaControl>)dc.View(BaseDataControl.LOOKUP);
      }
      return _ListData;
    }
    #endregion
    public WarnaLookupControl()
    {
      XMLName = ConstantTablesAsetDM.XMLWARNA;
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
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdwarna=Kode"), typeof(string), 30, HorizontalAlign.Left).SetEditable(false));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmwarna=Warna"), typeof(string), 50, HorizontalAlign.Left).SetEditable(false));
      return columns;
    }
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
        && string.IsNullOrEmpty((string)callerCtr.GetValue("Kdwarna"));

      WarnaLookupControl dclookup = new WarnaLookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys =  new String[] { "Kdwarna", "Nmwarna" };
      string[] targets =  new String[] { "Kdwarna", "Nmwarna" };
      ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 20, 75, 0 }, targets)
      {
        Label = "Warna",
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
      return "Kdwarna=Nmwarna";
    }
  }
  #endregion WarnaLookup
}

