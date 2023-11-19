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
  #region Usadi.Valid49.BO.JusahaLookupControl, CoreNET.Common.BO
  [Serializable]
  public class JusahaLookupControl : JusahaControl, IDataControlLookup, IHasJSScript
  {
    #region Singleton
    private static JusahaLookupControl _Instance = null;
    public static JusahaLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new JusahaLookupControl();
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
    private static List<JusahaControl> _ListData = null;
    public static void SetListDataNull()
    {
      _ListData = null;
    }
    public static List<JusahaControl> GetListDataSingleton()
    {
      if (_ListData == null)
      {
        JusahaLookupControl dc = new JusahaLookupControl();
        dc.SetPageKey();
        _ListData = (List<JusahaControl>)dc.View(BaseDataControl.LOOKUP);
      }
      return _ListData;
    }
    #endregion
    public JusahaLookupControl()
    {
      XMLName = ConstantTablesAsetDM.XMLJUSAHA;
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
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdjenis=Kode"), typeof(string), 15, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Jenisbadanusaha=Jenis Badan Usaha"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Keterangan"), typeof(string), 50, HorizontalAlign.Left));
      return columns;
    }
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
        && string.IsNullOrEmpty((string)callerCtr.GetValue("Kdjenis"));

      JusahaLookupControl dclookup = new JusahaLookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys = new String[] { "Kdjenis", "Jenisbadanusaha" };
      string[] targets = new String[] { "Kdjenis", "Jenisbadanusaha" };
      ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 15, 50 }, targets)
      {
        Label = "Jenis Usaha",
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
      return "Kdjenis=Jenisbadanusaha";
    }
  }
  #endregion JusahaLookup
}

