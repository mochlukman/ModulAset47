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
  #region Usadi.Valid49.BO.DaftdokLookupControl, CoreNET.Common.BO
  [Serializable]
  public class DaftdokLookupControl :  DaftdokControl, IDataControlLookup, IHasJSScript
  {
    #region Singleton
    private static  DaftdokLookupControl _Instance = null;
    public static  DaftdokLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new  DaftdokLookupControl();
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
    //      DaftdokLookupControl dc = new DaftdokLookupControl();
    //      dc.SetPageKey();
    //      _ListData = (List<DaftdokControl>)dc.View(BaseDataControl.ALL);
    //    }
    //    catch(Exception){}
    //    HttpContext.Current.Session[session_name] = _ListData;
    //  }
    //  return _ListData;
    //}
    private static List<DaftdokControl> _ListData = null;
    public static void SetListDataNull()
    {
      _ListData = null;
    }
    public static List<DaftdokControl> GetListDataSingleton()
    {
      if (_ListData == null)
      {
        DaftdokLookupControl dc = new DaftdokLookupControl();
        dc.SetPageKey();
        _ListData = (List<DaftdokControl>)dc.View(BaseDataControl.LOOKUP);
      }
      return _ListData;
    }
    #endregion
    public DaftdokLookupControl()
    {
      XMLName = ConstantTablesAsetDM.XMLDAFTDOK;
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
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kddok"), typeof(int), 10, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmdok"), typeof(string), 50, HorizontalAlign.Left).SetEditable(true));
      return columns;
    }
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      DaftdokLookupControl dclookup = new DaftdokLookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys =  new String[] { "Kddok", "Nmdok" };
      string[] targets =  new String[] { "Kddok", "Nmdok" };
      ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys,new int[] { 20, 75, 0 }, targets)
      {
        Label = title,
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
      return "Kddok=Nmdok";
    }
  }
  #endregion DaftdokLookup
}

