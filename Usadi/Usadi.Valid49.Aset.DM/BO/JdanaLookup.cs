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
  #region Usadi.Valid49.BO.JdanaLookupControl, Usadi.Valid49.Aset.DM
  [Serializable]
  public class JdanaLookupControl :  JdanaControl, IDataControlLookup, IHasJSScript
  {
    #region Singleton
    private static  JdanaLookupControl _Instance = null;
    public static  JdanaLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new  JdanaLookupControl();
        }
        return _Instance;
      }
    }
    //public static void SetSessionListDataNull()
    //{
    //  string session_name = (new JdanaControl()).XMLName; ;
    //  HttpContext.Current.Session[session_name] = null;
    //}
    //public static List<JdanaControl> GetSessionListDataSingleton()
    //{
    //  string session_name = (new JdanaControl()).XMLName; ;
    //  List<JdanaControl> _ListData = (List<JdanaControl>)HttpContext.Current.Session[session_name];
    //  if (_ListData == null)
    //  {
    //    try
    //    {
    //      JdanaLookupControl dc = new JdanaLookupControl();
    //      dc.SetPageKey();
    //      _ListData = (List<JdanaControl>)dc.View(BaseDataControl.ALL);
    //    }
    //    catch(Exception){}
    //    HttpContext.Current.Session[session_name] = _ListData;
    //  }
    //  return _ListData;
    //}
    private static List<JdanaControl> _ListData = null;
    public static void SetListDataNull()
    {
      _ListData = null;
    }
    public static List<JdanaControl> GetListDataSingleton()
    {
      if (_ListData == null)
      {
        JdanaLookupControl dc = new JdanaLookupControl();
        dc.SetPageKey();
        _ListData = (List<JdanaControl>)dc.View(BaseDataControl.LOOKUP);
      }
      return _ListData;
    }
    #endregion
    public JdanaLookupControl()
    {
      XMLName = ConstantTablesAsetDM.XMLJDANA;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      return cViewListProperties;
    }
    public new IList View()
    {
      IList list = this.View("All");
      return list;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kddana=Kode"), typeof(string), 30, HorizontalAlign.Left).SetEditable(true));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmdana=Jenis Dana"), typeof(string), 50, HorizontalAlign.Left).SetEditable(true));
      return columns;
    }
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      JdanaLookupControl dclookup = new JdanaLookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys =  new String[] { "Kddana", "Nmdana", "Kddana" };
      string[] targets =  new String[] { "Kddana=Kddana", "Nmdana=Nmdana" };
      ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 20, 75, 0 }, targets)
      {
        Label = "Sumber Dana",
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
      return "Kddana=Nmdana";
    }
  }
  #endregion JdanaLookup
}

