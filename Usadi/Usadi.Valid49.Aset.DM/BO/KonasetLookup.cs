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
  #region Usadi.Valid49.BO.KonasetLookupControl, CoreNET.Common.BO
  [Serializable]
  public class KonasetLookupControl :  KonasetControl, IDataControlLookup, IHasJSScript
  {
    #region Singleton
    private static  KonasetLookupControl _Instance = null;
    public static  KonasetLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new  KonasetLookupControl();
        }
        return _Instance;
      }
    }
    //public static void SetSessionListDataNull()
    //{
    //  string session_name = (new KonasetControl()).XMLName; ;
    //  HttpContext.Current.Session[session_name] = null;
    //}
    //public static List<KonasetControl> GetSessionListDataSingleton()
    //{
    //  string session_name = (new KonasetControl()).XMLName; ;
    //  List<KonasetControl> _ListData = (List<KonasetControl>)HttpContext.Current.Session[session_name];
    //  if (_ListData == null)
    //  {
    //    try
    //    {
    //      KonasetLookupControl dc = new KonasetLookupControl();
    //      dc.SetPageKey();
    //      _ListData = (List<KonasetControl>)dc.View(BaseDataControl.ALL);
    //    }
    //    catch(Exception){}
    //    HttpContext.Current.Session[session_name] = _ListData;
    //  }
    //  return _ListData;
    //}
    private static List<KonasetControl> _ListData = null;
    public static void SetListDataNull()
    {
      _ListData = null;
    }
    public static List<KonasetControl> GetListDataSingleton()
    {
      if (_ListData == null)
      {
        KonasetLookupControl dc = new KonasetLookupControl();
        dc.SetPageKey();
        _ListData = (List<KonasetControl>)dc.View(BaseDataControl.LOOKUP);
      }
      return _ListData;
    }
    #endregion
    public KonasetLookupControl()
    {
      XMLName = ConstantTablesAsetDM.XMLKONASET;
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
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdkon=Kode"), typeof(string), 30, HorizontalAlign.Left).SetEditable(false));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmkon=Kondisi"), typeof(string), 50, HorizontalAlign.Left).SetEditable(false));
      return columns;
    }
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
        && string.IsNullOrEmpty((string)callerCtr.GetValue("Kdkon"));

      KonasetLookupControl dclookup = new KonasetLookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys =  new String[] { "Kdkon", "Nmkon" };
      string[] targets =  new String[] { "Kdkon", "Nmkon" };
      ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 20, 75, 0 }, targets)
      {
        Label = "Kondisi",
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
      return "Kdkon=Nmkon";
    }
  }
  #endregion KonasetLookup
}

