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
  #region Usadi.Valid49.BO.MatangrLookupControl, CoreNET.Common.BO
  [Serializable]
  public class MatangrLookupControl :  MatangrControl, IDataControlLookup, IHasJSScript
  {
    #region Singleton
    private static  MatangrLookupControl _Instance = null;
    public static  MatangrLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new  MatangrLookupControl();
        }
        return _Instance;
      }
    }
    //public static void SetSessionListDataNull()
    //{
    //  string session_name = (new MatangrControl()).XMLName; ;
    //  HttpContext.Current.Session[session_name] = null;
    //}
    //public static List<MatangrControl> GetSessionListDataSingleton()
    //{
    //  string session_name = (new MatangrControl()).XMLName; ;
    //  List<MatangrControl> _ListData = (List<MatangrControl>)HttpContext.Current.Session[session_name];
    //  if (_ListData == null)
    //  {
    //    try
    //    {
    //      MatangrLookupControl dc = new MatangrLookupControl();
    //      dc.SetPageKey();
    //      _ListData = (List<MatangrControl>)dc.View(BaseDataControl.ALL);
    //    }
    //    catch(Exception){}
    //    HttpContext.Current.Session[session_name] = _ListData;
    //  }
    //  return _ListData;
    //}
    private static List<MatangrControl> _ListData = null;
    public static void SetListDataNull()
    {
      _ListData = null;
    }
    public static List<MatangrControl> GetListDataSingleton()
    {
      if (_ListData == null)
      {
        MatangrLookupControl dc = new MatangrLookupControl();
        dc.SetPageKey();
        _ListData = (List<MatangrControl>)dc.View(BaseDataControl.LOOKUP);
      }
      return _ListData;
    }
    #endregion
    public MatangrLookupControl()
    {
      XMLName = ConstantTablesAsetDM.XMLMATANGR;
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
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdper=Kode Rekening"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmper=Rekening"), typeof(string), 70, HorizontalAlign.Left));
      return columns;
    }
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
        && string.IsNullOrEmpty((string)callerCtr.GetValue("Kdper"));

      MatangrLookupControl dclookup = new MatangrLookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys =  new String[] { "Kdper", "Nmper" };
      string[] targets =  new String[] { "Kdper", "Nmper" };
      ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 20, 75, 0 }, targets)
      {
        Label = "Rekening Belanja",
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
  #endregion MatangrLookup
}

