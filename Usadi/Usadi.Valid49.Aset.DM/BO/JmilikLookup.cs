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
  #region Usadi.Valid49.BO.JmilikLookupControl, CoreNET.Common.BO
  [Serializable]
  public class JmilikLookupControl :  JmilikControl, IDataControlLookup, IHasJSScript
  {
    #region Singleton
    private static  JmilikLookupControl _Instance = null;
    public static  JmilikLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new  JmilikLookupControl();
        }
        return _Instance;
      }
    }
    //public static void SetSessionListDataNull()
    //{
    //  string session_name = (new JmilikControl()).XMLName; ;
    //  HttpContext.Current.Session[session_name] = null;
    //}
    //public static List<JmilikControl> GetSessionListDataSingleton()
    //{
    //  string session_name = (new JmilikControl()).XMLName; ;
    //  List<JmilikControl> _ListData = (List<JmilikControl>)HttpContext.Current.Session[session_name];
    //  if (_ListData == null)
    //  {
    //    try
    //    {
    //      JmilikLookupControl dc = new JmilikLookupControl();
    //      dc.SetPageKey();
    //      _ListData = (List<JmilikControl>)dc.View(BaseDataControl.ALL);
    //    }
    //    catch(Exception){}
    //    HttpContext.Current.Session[session_name] = _ListData;
    //  }
    //  return _ListData;
    //}
    private static List<JmilikControl> _ListData = null;
    public static void SetListDataNull()
    {
      _ListData = null;
    }
    public static List<JmilikControl> GetListDataSingleton()
    {
      if (_ListData == null)
      {
        JmilikLookupControl dc = new JmilikLookupControl();
        dc.SetPageKey();
        _ListData = (List<JmilikControl>)dc.View(BaseDataControl.LOOKUP);
      }
      return _ListData;
    }
    #endregion
    public JmilikLookupControl()
    {
      XMLName = ConstantTablesAsetDM.XMLJMILIK;
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
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdpemilik=Kode"), typeof(string), 30, HorizontalAlign.Left).SetEditable(false));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmpemilik=Pemilik"), typeof(string), 50, HorizontalAlign.Left).SetEditable(false));
      return columns;
    }
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
        && string.IsNullOrEmpty((string)callerCtr.GetValue("Kdpemilik"));

      JmilikLookupControl dclookup = new JmilikLookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys =  new String[] { "Kdpemilik", "Nmpemilik" };
      string[] targets =  new String[] { "Kdpemilik", "Nmpemilik" };
      ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 20, 75, 0 }, targets)
      {
        Label = "Pemilik",
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
      return "Kdpemilik=Nmpemilik";
    }
  }
  #endregion JmilikLookup
}

