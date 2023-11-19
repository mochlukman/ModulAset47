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
  #region Usadi.Valid49.BO.JkontrakLookupControl, CoreNET.Common.BO
  [Serializable]
  public class JkontrakLookupControl :  JkontrakControl, IDataControlLookup, IHasJSScript
  {
    #region Singleton
    private static  JkontrakLookupControl _Instance = null;
    public static  JkontrakLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new  JkontrakLookupControl();
        }
        return _Instance;
      }
    }
    //public static void SetSessionListDataNull()
    //{
    //  string session_name = (new BulanControl()).XMLName; ;
    //  HttpContext.Current.Session[session_name] = null;
    //}
    //public static List<BulanControl> GetSessionListDataSingleton()
    //{
    //  string session_name = (new BulanControl()).XMLName; ;
    //  List<BulanControl> _ListData = (List<BulanControl>)HttpContext.Current.Session[session_name];
    //  if (_ListData == null)
    //  {
    //    try
    //    {
    //      JkontrakLookupControl dc = new JkontrakLookupControl();
    //      dc.SetPageKey();
    //      _ListData = (List<BulanControl>)dc.View(BaseDataControl.ALL);
    //    }
    //    catch(Exception){}
    //    HttpContext.Current.Session[session_name] = _ListData;
    //  }
    //  return _ListData;
    //}
    private static List<BulanControl> _ListData = null;
    public static void SetListDataNull()
    {
      _ListData = null;
    }
    public static List<BulanControl> GetListDataSingleton()
    {
      if (_ListData == null)
      {
        JkontrakLookupControl dc = new JkontrakLookupControl();
        dc.SetPageKey();
        _ListData = (List<BulanControl>)dc.View(BaseDataControl.LOOKUP);
      }
      return _ListData;
    }
    #endregion
    public JkontrakLookupControl()
    {
      XMLName = ConstantTablesAsetDM.XMLJKONTRAK;
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
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdjenis=Kode"), typeof(string), 15, HorizontalAlign.Center).SetEditable(false));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmjenis=Jenis Kontrak"), typeof(string), 30, HorizontalAlign.Left).SetEditable(false));
      return columns;
    }
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
        && string.IsNullOrEmpty((string)callerCtr.GetValue("Kdjenis"));

      JkontrakLookupControl dclookup = new JkontrakLookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys =  new String[] { "Kdjenis", "Nmjenis" };
      string[] targets =  new String[] { "Kdjenis", "Nmjenis" };
      ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys,new int[] { 20, 75, 0 }, targets)
      {
        Label = "Jenis Kontrak",
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
      return "Kdjenis=Ket_bulan";
    }
  }
  #endregion JkontrakLookup
}

