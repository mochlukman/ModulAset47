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
  #region Usadi.Valid49.BO.TahunLookupControl, CoreNET.Common.BO
  [Serializable]
  public class TahunLookupControl :  TahunControl, IDataControlLookup, IHasJSScript
  {
    #region Singleton
    private static  TahunLookupControl _Instance = null;
    public static  TahunLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new  TahunLookupControl();
        }
        return _Instance;
      }
    }
    //public static void SetSessionListDataNull()
    //{
    //  string session_name = (new TahunControl()).XMLName; ;
    //  HttpContext.Current.Session[session_name] = null;
    //}
    //public static List<TahunControl> GetSessionListDataSingleton()
    //{
    //  string session_name = (new TahunControl()).XMLName; ;
    //  List<TahunControl> _ListData = (List<TahunControl>)HttpContext.Current.Session[session_name];
    //  if (_ListData == null)
    //  {
    //    try
    //    {
    //      TahunLookupControl dc = new TahunLookupControl();
    //      dc.SetPageKey();
    //      _ListData = (List<TahunControl>)dc.View(BaseDataControl.ALL);
    //    }
    //    catch(Exception){}
    //    HttpContext.Current.Session[session_name] = _ListData;
    //  }
    //  return _ListData;
    //}
    private static List<TahunControl> _ListData = null;
    public static void SetListDataNull()
    {
      _ListData = null;
    }
    public static List<TahunControl> GetListDataSingleton()
    {
      if (_ListData == null)
      {
        TahunLookupControl dc = new TahunLookupControl();
        dc.SetPageKey();
        _ListData = (List<TahunControl>)dc.View(BaseDataControl.LOOKUP);
      }
      return _ListData;
    }
    #endregion
    public TahunLookupControl()
    {
      XMLName = ConstantTablesAsetDM.XMLTAHUN;
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
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdtahun=Kode"), typeof(string), 30, HorizontalAlign.Left).SetEditable(false));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmtahun=Tahun"), typeof(string), 50, HorizontalAlign.Left).SetEditable(false));
      return columns;
    }
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
        && string.IsNullOrEmpty((string)callerCtr.GetValue("Kdtahun"));

      TahunLookupControl dclookup = new TahunLookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys =  new String[] { "Kdtahun", "Nmtahun" };
      string[] targets =  new String[] { "Kdtahun", "Nmtahun" };
      ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 20, 75, 0 }, targets)
      {
        Label = "Tahun",
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
      return "Kdtahun=Nmtahun";
    }
  }
  #endregion TahunLookup
}

