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
  #region Usadi.Valid49.BO.JbankLookupControl, CoreNET.Common.BO
  [Serializable]
  public class JbankLookupControl :  JbankControl, IDataControlLookup, IHasJSScript
  {
    #region Singleton
    private static JbankLookupControl _Instance = null;
    public static JbankLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new JbankLookupControl();
        }
        return _Instance;
      }
    }
    //public static void SetSessionListDataNull()
    //{
    //  string session_name = (new Daftphk3Control()).XMLName; ;
    //  HttpContext.Current.Session[session_name] = null;
    //}
    //public static List<Daftphk3Control> GetSessionListDataSingleton()
    //{
    //  string session_name = (new Daftphk3Control()).XMLName; ;
    //  List<Daftphk3Control> _ListData = (List<Daftphk3Control>)HttpContext.Current.Session[session_name];
    //  if (_ListData == null)
    //  {
    //    try
    //    {
    //      Daftphk3LookupControl dc = new Daftphk3LookupControl();
    //      dc.SetPageKey();
    //      _ListData = (List<Daftphk3Control>)dc.View(BaseDataControl.ALL);
    //    }
    //    catch(Exception){}
    //    HttpContext.Current.Session[session_name] = _ListData;
    //  }
    //  return _ListData;
    //}
    private static List<JbankControl> _ListData = null;
    public static void SetListDataNull()
    {
      _ListData = null;
    }
    public static List<JbankControl> GetListDataSingleton()
    {
      if (_ListData == null)
      {
        JbankLookupControl dc = new JbankLookupControl();
        dc.SetPageKey();
        _ListData = (List<JbankControl>)dc.View(BaseDataControl.LOOKUP);
      }
      return _ListData;
    }
    #endregion
    public JbankLookupControl()
    {
      XMLName = ConstantTablesAsetDM.XMLJBANK;
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
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Idbank=Idbank"), typeof(string), 15, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmbank=Nama Bank"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Uraian"), typeof(string), 50, HorizontalAlign.Left));
      return columns;
    }
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
        && string.IsNullOrEmpty((string)callerCtr.GetValue("Idbank"));

      JbankLookupControl dclookup = new JbankLookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys =  new String[] { "Idbank", "Nmbank" };
      string[] targets =  new String[] { "Idbank", "Nmbank" };
      ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys,new int[] { 15, 72 }, targets)
      {
        Label = "Bank",
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
      return "Idbank=Nmbank";
    }
  }
  #endregion JbankLookup
}

