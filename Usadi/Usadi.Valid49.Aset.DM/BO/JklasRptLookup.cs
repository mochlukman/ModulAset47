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
  #region Usadi.Valid49.BO.JklasRptLookupControl, CoreNET.Common.BO
  [Serializable]
  public class JklasRptLookupControl : JklasControl, IDataControlLookup, IHasJSScript
  {
    #region Singleton
    private static JklasRptLookupControl _Instance = null;
    public static JklasRptLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new JklasRptLookupControl();
        }
        return _Instance;
      }
    }

    private static List<JklasControl> _ListData = null;
    public static void SetListDataNull()
    {
      _ListData = null;
    }
    public static List<JklasControl> GetListDataSingleton()
    {
      if (_ListData == null)
      {
        JklasRptLookupControl dc = new JklasRptLookupControl();
        dc.SetPageKey();
        _ListData = (List<JklasControl>)dc.View(BaseDataControl.LOOKUP);
      }
      return _ListData;
    }
    #endregion
    public JklasRptLookupControl()
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
      IList list = this.View("RptLookup");
      return list;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdklas"), typeof(int), 10, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Uraiklas"), typeof(string), 50, HorizontalAlign.Left).SetEditable(true));
      return columns;
    }
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
        && string.IsNullOrEmpty((string)callerCtr.GetValue("Kdklas"));

      JklasRptLookupControl dclookup = new JklasRptLookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys = new String[] { "Kdklas", "Uraiklas" };
      string[] targets = new String[] { "Kdklas", "Uraiklas" };
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
  #endregion JklasRptLookupControl
}

