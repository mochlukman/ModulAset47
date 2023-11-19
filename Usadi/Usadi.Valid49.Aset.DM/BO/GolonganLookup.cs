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
  #region Usadi.Valid49.BO.GolonganLookupControl, CoreNET.Common.BO
  [Serializable]
  public class GolonganLookupControl :  GolonganControl, IDataControlLookup, IHasJSScript
  {
    #region Singleton
    private static  GolonganLookupControl _Instance = null;
    public static  GolonganLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new  GolonganLookupControl();
        }
        return _Instance;
      }
    }
    //public static void SetSessionListDataNull()
    //{
    //  string session_name = (new GolonganControl()).XMLName; ;
    //  HttpContext.Current.Session[session_name] = null;
    //}
    //public static List<GolonganControl> GetSessionListDataSingleton()
    //{
    //  string session_name = (new GolonganControl()).XMLName; ;
    //  List<GolonganControl> _ListData = (List<GolonganControl>)HttpContext.Current.Session[session_name];
    //  if (_ListData == null)
    //  {
    //    try
    //    {
    //      GolonganLookupControl dc = new GolonganLookupControl();
    //      dc.SetPageKey();
    //      _ListData = (List<GolonganControl>)dc.View(BaseDataControl.ALL);
    //    }
    //    catch(Exception){}
    //    HttpContext.Current.Session[session_name] = _ListData;
    //  }
    //  return _ListData;
    //}
    private static List<GolonganControl> _ListData = null;
    public static void SetListDataNull()
    {
      _ListData = null;
    }
    public static List<GolonganControl> GetListDataSingleton()
    {
      if (_ListData == null)
      {
        GolonganLookupControl dc = new GolonganLookupControl();
        dc.SetPageKey();
        _ListData = (List<GolonganControl>)dc.View(BaseDataControl.LOOKUP);
      }
      return _ListData;
    }
    #endregion
    public GolonganLookupControl()
    {
      XMLName = ConstantTablesAsetDM.XMLGOLONGAN;
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
      //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdgol"), typeof(int), 10, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmgol"), typeof(string), 50, HorizontalAlign.Left).SetEditable(false));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Pangkat"), typeof(string), 50, HorizontalAlign.Left).SetEditable(false));
      return columns;
    }
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
        && string.IsNullOrEmpty((string)callerCtr.GetValue("Kdgol"));

      GolonganLookupControl dclookup = new GolonganLookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys =  new String[] { "Kdgol", "Nmgol", "Pangkat" };
      string[] targets =  new String[] { "Kdgol", "Nmgol", "Pangkat" };
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
      return "Nmgol=Pangkat";
    }
  }
  #endregion GolonganLookup
}

