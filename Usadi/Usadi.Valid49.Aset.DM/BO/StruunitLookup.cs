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
  #region Usadi.Valid49.BO.StruunitLookupControl, CoreNET.Common.BO
  [Serializable]
  public class StruunitLookupControl :  StruunitControl, IDataControlLookup, IHasJSScript
  {
    #region Singleton
    private static  StruunitLookupControl _Instance = null;
    public static  StruunitLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new  StruunitLookupControl();
        }
        return _Instance;
      }
    }
    //public static void SetSessionListDataNull()
    //{
    //  string session_name = (new StruunitControl()).XMLName; ;
    //  HttpContext.Current.Session[session_name] = null;
    //}
    //public static List<StruunitControl> GetSessionListDataSingleton()
    //{
    //  string session_name = (new StruunitControl()).XMLName; ;
    //  List<StruunitControl> _ListData = (List<StruunitControl>)HttpContext.Current.Session[session_name];
    //  if (_ListData == null)
    //  {
    //    try
    //    {
    //      StruunitLookupControl dc = new StruunitLookupControl();
    //      dc.SetPageKey();
    //      _ListData = (List<StruunitControl>)dc.View(BaseDataControl.ALL);
    //    }
    //    catch(Exception){}
    //    HttpContext.Current.Session[session_name] = _ListData;
    //  }
    //  return _ListData;
    //}
    private static List<StruunitControl> _ListData = null;
    public static void SetListDataNull()
    {
      _ListData = null;
    }
    public static List<StruunitControl> GetListDataSingleton()
    {
      if (_ListData == null)
      {
        StruunitLookupControl dc = new StruunitLookupControl();
        dc.SetPageKey();
        _ListData = (List<StruunitControl>)dc.View(BaseDataControl.LOOKUP);
      }
      return _ListData;
    }
    #endregion
    public StruunitLookupControl()
    {
      XMLName = ConstantTablesAsetDM.XMLSTRUUNIT;
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
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Level"), typeof(int), 10, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmlevel"), typeof(string), 50, HorizontalAlign.Left).SetEditable(true));
      return columns;
    }
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      StruunitLookupControl dclookup = new StruunitLookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys =  new String[] { "Level", "Level" };
      string[] targets =  new String[] { "Level=Level" };
      ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 20, 75, 0 }, targets)
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
      return "Level=Level";
    }
  }
  #endregion StruunitLookup
}

