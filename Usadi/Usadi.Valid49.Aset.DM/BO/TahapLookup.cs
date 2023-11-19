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
  #region Usadi.Valid49.BO.TahapLookupControl, Usadi.Valid49.Aset.DM
  [Serializable]
  public class TahapLookupControl :  TahapControl, IDataControlLookup, IHasJSScript
  {
    #region Singleton
    private static  TahapLookupControl _Instance = null;
    public static  TahapLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new  TahapLookupControl();
        }
        return _Instance;
      }
    }
    //public static void SetSessionListDataNull()
    //{
    //  string session_name = (new TahapControl()).XMLName; ;
    //  HttpContext.Current.Session[session_name] = null;
    //}
    //public static List<TahapControl> GetSessionListDataSingleton()
    //{
    //  string session_name = (new TahapControl()).XMLName; ;
    //  List<TahapControl> _ListData = (List<TahapControl>)HttpContext.Current.Session[session_name];
    //  if (_ListData == null)
    //  {
    //    try
    //    {
    //      TahapLookupControl dc = new TahapLookupControl();
    //      dc.SetPageKey();
    //      _ListData = (List<TahapControl>)dc.View(BaseDataControl.ALL);
    //    }
    //    catch(Exception){}
    //    HttpContext.Current.Session[session_name] = _ListData;
    //  }
    //  return _ListData;
    //}
    private static List<TahapControl> _ListData = null;
    public static void SetListDataNull()
    {
      _ListData = null;
    }
    public static List<TahapControl> GetListDataSingleton()
    {
      if (_ListData == null)
      {
        TahapLookupControl dc = new TahapLookupControl();
        dc.SetPageKey();
        _ListData = (List<TahapControl>)dc.View(BaseDataControl.LOOKUP);
      }
      return _ListData;
    }
    #endregion
    public TahapLookupControl()
    {
      XMLName = ConstantTablesAsetDM.XMLTAHAP;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      return cViewListProperties;
    }
    public new IList View()
    {
      IList list = this.View("Perda");
      return list;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdtahap"), typeof(string), 30, HorizontalAlign.Left).SetEditable(true));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Uraian"), typeof(string), 50, HorizontalAlign.Left).SetEditable(true));
      return columns;
    }
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      TahapLookupControl dclookup = new TahapLookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys =  new String[] { "Kdtahap","Uraian","Kdtahap"};
      string[] targets =  new String[] { "Kdtahap=Kdtahap", "Uraian=Uraian" };
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
      return "Kdtahap=Uraian";
    }
  }
  #endregion TahapLookup
}

