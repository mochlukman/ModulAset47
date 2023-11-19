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
  #region Usadi.Valid49.BO.JnskibKirLookupControl, CoreNET.Common.BO
  [Serializable]
  public class JnskibKirLookupControl :  JnskibControl, IDataControlLookup, IHasJSScript
  {
    #region Singleton
    private static  JnskibKirLookupControl _Instance = null;
    public static  JnskibKirLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new  JnskibKirLookupControl();
        }
        return _Instance;
      }
    }
    //public static void SetSessionListDataNull()
    //{
    //  string session_name = (new JnskibControl()).XMLName; ;
    //  HttpContext.Current.Session[session_name] = null;
    //}
    //public static List<JnskibControl> GetSessionListDataSingleton()
    //{
    //  string session_name = (new JnskibControl()).XMLName; ;
    //  List<JnskibControl> _ListData = (List<JnskibControl>)HttpContext.Current.Session[session_name];
    //  if (_ListData == null)
    //  {
    //    try
    //    {
    //      JnskibKirLookupControl dc = new JnskibKirLookupControl();
    //      dc.SetPageKey();
    //      _ListData = (List<JnskibControl>)dc.View(BaseDataControl.ALL);
    //    }
    //    catch(Exception){}
    //    HttpContext.Current.Session[session_name] = _ListData;
    //  }
    //  return _ListData;
    //}
    private static List<JnskibControl> _ListData = null;
    public static void SetListDataNull()
    {
      _ListData = null;
    }
    public static List<JnskibControl> GetListDataSingleton()
    {
      if (_ListData == null)
      {
        JnskibKirLookupControl dc = new JnskibKirLookupControl();
        dc.SetPageKey();
        _ListData = (List<JnskibControl>)dc.View(BaseDataControl.LOOKUP);
      }
      return _ListData;
    }
    #endregion
    public JnskibKirLookupControl()
    {
      XMLName = ConstantTablesAsetDM.XMLJNSKIB;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      return cViewListProperties;
    }
    public new IList View()
    {
      IList list = this.View("Kir");
      return list;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdkib=Kode"), typeof(string), 15, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmkib=Uraian"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Golkib=Golongan"), typeof(string), 15, HorizontalAlign.Center));
      return columns;
    }
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      //bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
      //  && string.IsNullOrEmpty((string)callerCtr.GetValue("Kdkib"));

      JnskibKirLookupControl dclookup = new JnskibKirLookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys =  new String[] { "Kdkib", "Nmkib" };
      string[] targets =  new String[] { "Kdkib", "Nmkib" };
      ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 20, 75, 0 }, targets)
      {
        Label = "Jenis Barang",
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
      return "Kdkib=Nmkib";
    }
  }
  #endregion JnskibKirLookup
}

