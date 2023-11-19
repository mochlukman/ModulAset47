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
  #region Usadi.Valid49.BO.WebuserLookupControl, CoreNET.Common.BO
  [Serializable]
  public class WebuserLookupControl :  WebuserControl, IDataControlLookup
  {
    #region Singleton
    private static  WebuserLookupControl _Instance = null;
    public static  WebuserLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new  WebuserLookupControl();
        }
        return _Instance;
      }
    }
    //public static void SetSessionListDataNull()
    //{
    //  string session_name = (new WebuserControl()).XMLName; ;
    //  HttpContext.Current.Session[session_name] = null;
    //}
    //public static List<WebuserControl> GetSessionListDataSingleton()
    //{
    //  string session_name = (new WebuserControl()).XMLName; ;
    //  List<WebuserControl> _ListData = (List<WebuserControl>)HttpContext.Current.Session[session_name];
    //  if (_ListData == null)
    //  {
    //    try
    //    {
    //      WebuserLookupControl dc = new WebuserLookupControl();
    //      dc.SetPageKey();
    //      _ListData = (List<WebuserControl>)dc.View(BaseDataControl.ALL);
    //    }
    //    catch(Exception){}
    //    HttpContext.Current.Session[session_name] = _ListData;
    //  }
    //  return _ListData;
    //}
    private static List<WebuserControl> _ListData = null;
    public static void SetListDataNull()
    {
      _ListData = null;
    }
    public static List<WebuserControl> GetListDataSingleton()
    {
      if (_ListData == null)
      {
        WebuserLookupControl dc = new WebuserLookupControl();
        dc.SetPageKey();
        _ListData = (List<WebuserControl>)dc.View(BaseDataControl.LOOKUP);
      }
      return _ListData;
    }
    #endregion
    public WebuserLookupControl()
    {
      XMLName = ConstantTablesAsetSys.XMLWEBUSER;
    }
    ViewListProperties cViewListProperties = null;
    public new IProperties GetProperties()
    {
      if(cViewListProperties == null)
      {
        cViewListProperties = (ViewListProperties)base.GetProperties();
      }
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      return cViewListProperties;
    }
    public new IList View()
    {
      IList list = this.View(BaseDataControl.LOOKUP);
      return list;
    }
    public new HashTableofParameterRow GetFilters()
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
      HashTableofParameterRow hpars = new HashTableofParameterRow();

      return hpars;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Userid=UserID"), typeof(string), 20, HorizontalAlign.Left).SetEditable(false));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nama"), typeof(string), 30, HorizontalAlign.Left).SetEditable(true));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdnmunit=SKPD"), typeof(string), 50, HorizontalAlign.Left).SetEditable(false));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmgroup=Kelompok"), typeof(string), 30, HorizontalAlign.Left).SetEditable(false));
      //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdpemda"), typeof(string), 30, HorizontalAlign.Left).SetEditable(true));
      return columns;
    }
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      WebuserLookupControl dclookup = new WebuserLookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys =  new String[] { "Userid" ,"Nama", "Userid" };
      string[] targets =  new String[] { "Nama=Nama", "Userid=Userid" };
      ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys,new int[] { 20, 75, 0 }, targets)
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
      return "Userid=Userid";
    }
  }
  #endregion WebuserLookup
}

