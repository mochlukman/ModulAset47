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
  #region Usadi.Valid49.BO.DaskrRenovLookupControl, CoreNET.Common.BO
  [Serializable]
  public class DaskrRenovLookupControl :  DaskrControl, IDataControlLookup, IHasJSScript
  {
    #region Singleton
    private static  DaskrRenovLookupControl _Instance = null;
    public static  DaskrRenovLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new  DaskrRenovLookupControl();
        }
        return _Instance;
      }
    }
    //public static void SetSessionListDataNull()
    //{
    //  string session_name = (new DaskrControl()).XMLName; ;
    //  HttpContext.Current.Session[session_name] = null;
    //}
    //public static List<DaskrControl> GetSessionListDataSingleton()
    //{
    //  string session_name = (new DaskrControl()).XMLName; ;
    //  List<DaskrControl> _ListData = (List<DaskrControl>)HttpContext.Current.Session[session_name];
    //  if (_ListData == null)
    //  {
    //    try
    //    {
    //      DaskrRenovLookupControl dc = new DaskrRenovLookupControl();
    //      dc.SetPageKey();
    //      _ListData = (List<DaskrControl>)dc.View(BaseDataControl.ALL);
    //    }
    //    catch(Exception){}
    //    HttpContext.Current.Session[session_name] = _ListData;
    //  }
    //  return _ListData;
    //}
    private static List<DaskrControl> _ListData = null;
    public static void SetListDataNull()
    {
      _ListData = null;
    }
    public static List<DaskrControl> GetListDataSingleton()
    {
      if (_ListData == null)
      {
        DaskrRenovLookupControl dc = new DaskrRenovLookupControl();
        dc.SetPageKey();
        _ListData = (List<DaskrControl>)dc.View(BaseDataControl.LOOKUP);
      }
      return _ListData;
    }
    #endregion
    public DaskrRenovLookupControl()
    {
      XMLName = ConstantTablesAsetDM.XMLDASKR;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      return cViewListProperties;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      Unitkey = (string)bo.GetValue("Unitkey");
      Kdtahap = (string)bo.GetValue("Kdtahap");
      Kdkegunit = (string)bo.GetValue("Kdkegunit");
      Nobarenov = (string)bo.GetValue("Nobarenov");
    }
    public new IList View()
    {
      IList list = this.View("Renov");
      return list;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdper=Kode Rekening"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmper=Rekening"), typeof(string), 70, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai"), typeof(decimal), 30, HorizontalAlign.Left));
      return columns;
    }
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
        && string.IsNullOrEmpty((string)callerCtr.GetValue("Mtgkey"));

      DaskrRenovLookupControl dclookup = new DaskrRenovLookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys =  new String[] { "Kdper", "Nmper", "Mtgkey" };
      string[] targets =  new String[] { "Kdper", "Nmper", "Mtgkey" };
      ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys,new int[] { 20, 75, 0 }, targets)
      {
        Label = "Rekening",
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
      return "Kdper=Nmper";
    }
  }
  #endregion DaskrRenovLookup
}

