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
  #region Usadi.Valid49.BO.JklasLookupControl, CoreNET.Common.BO
  [Serializable]
  public class JklasLookupControl :  JklasControl, IDataControlLookup, IHasJSScript
  {
    #region Singleton
    private static  JklasLookupControl _Instance = null;
    public static  JklasLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new  JklasLookupControl();
        }
        return _Instance;
      }
    }
    //public static void SetSessionListDataNull()
    //{
    //  string session_name = (new JklasControl()).XMLName; ;
    //  HttpContext.Current.Session[session_name] = null;
    //}
    //public static List<JklasControl> GetSessionListDataSingleton()
    //{
    //  string session_name = (new JklasControl()).XMLName; ;
    //  List<JklasControl> _ListData = (List<JklasControl>)HttpContext.Current.Session[session_name];
    //  if (_ListData == null)
    //  {
    //    try
    //    {
    //      JklasLookupControl dc = new JklasLookupControl();
    //      dc.SetPageKey();
    //      _ListData = (List<JklasControl>)dc.View(BaseDataControl.ALL);
    //    }
    //    catch(Exception){}
    //    HttpContext.Current.Session[session_name] = _ListData;
    //  }
    //  return _ListData;
    //}
    private static List<JklasControl> _ListData = null;
    public static void SetListDataNull()
    {
      _ListData = null;
    }
    public static List<JklasControl> GetListDataSingleton()
    {
      if (_ListData == null)
      {
        JklasLookupControl dc = new JklasLookupControl();
        dc.SetPageKey();
        _ListData = (List<JklasControl>)dc.View(BaseDataControl.LOOKUP);
      }
      return _ListData;
    }
    #endregion
    public JklasLookupControl()
    {
      XMLName = ConstantTablesAsetDM.XMLJKLAS;
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
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdklas=Kode"), typeof(int), 15, HorizontalAlign.Left).SetEditable(false));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Uraiklas=Kelas"), typeof(string), 30, HorizontalAlign.Left).SetEditable(false));
      return columns;
    }
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
        && string.IsNullOrEmpty((string)callerCtr.GetValue("Kdklas"));

      JklasLookupControl dclookup = new JklasLookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys =  new String[] { "Kdklas", "Uraiklas" };
      string[] targets =  new String[] { "Kdklas", "Uraiklas" };
      ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 20, 75, 0 }, targets)
      {
        Label = "Kelas Aset",
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
      return "Kdklas=Uraiklas";
    }
  }
  #endregion JklasLookup
}

