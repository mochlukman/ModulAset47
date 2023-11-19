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
  #region Usadi.Valid49.BO.StruasetLookupControl, CoreNET.Common.BO
  [Serializable]
  public class StruasetLookupControl :  StruasetControl, IDataControlLookup, IHasJSScript
  {
    #region Singleton
    private static  StruasetLookupControl _Instance = null;
    public static  StruasetLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new  StruasetLookupControl();
        }
        return _Instance;
      }
    }
    //public static void SetSessionListDataNull()
    //{
    //  string session_name = (new StruasetControl()).XMLName; ;
    //  HttpContext.Current.Session[session_name] = null;
    //}
    //public static List<StruasetControl> GetSessionListDataSingleton()
    //{
    //  string session_name = (new StruasetControl()).XMLName; ;
    //  List<StruasetControl> _ListData = (List<StruasetControl>)HttpContext.Current.Session[session_name];
    //  if (_ListData == null)
    //  {
    //    try
    //    {
    //      StruasetLookupControl dc = new StruasetLookupControl();
    //      dc.SetPageKey();
    //      _ListData = (List<StruasetControl>)dc.View(BaseDataControl.ALL);
    //    }
    //    catch(Exception){}
    //    HttpContext.Current.Session[session_name] = _ListData;
    //  }
    //  return _ListData;
    //}
    private static List<StruasetControl> _ListData = null;
    public static void SetListDataNull()
    {
      _ListData = null;
    }
    public static List<StruasetControl> GetListDataSingleton()
    {
      if (_ListData == null)
      {
        StruasetLookupControl dc = new StruasetLookupControl();
        dc.SetPageKey();
        _ListData = (List<StruasetControl>)dc.View(BaseDataControl.LOOKUP);
      }
      return _ListData;
    }
    #endregion
    public StruasetLookupControl()
    {
      XMLName = ConstantTablesAsetDM.XMLSTRUASET;
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
      StruasetLookupControl dclookup = new StruasetLookupControl();
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
  #endregion StruasetLookup
}

