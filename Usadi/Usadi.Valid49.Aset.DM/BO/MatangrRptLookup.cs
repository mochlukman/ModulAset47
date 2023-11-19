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
  #region Usadi.Valid49.BO.MatangrRptLookupControl, CoreNET.Common.BO
  [Serializable]
  public class MatangrRptLookupControl : MatangrControl, IDataControlLookup, IHasJSScript
  {
    #region Singleton
    private static MatangrRptLookupControl _Instance = null;
    public static MatangrRptLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new MatangrRptLookupControl();
        }
        return _Instance;
      }
    }

    private static List<MatangrControl> _ListData = null;
    public static void SetListDataNull()
    {
      _ListData = null;
    }
    public static List<MatangrControl> GetListDataSingleton()
    {
      if (_ListData == null)
      {
        MatangrRptLookupControl dc = new MatangrRptLookupControl();
        dc.SetPageKey();
        _ListData = (List<MatangrControl>)dc.View(BaseDataControl.LOOKUP);
      }
      return _ListData;
    }
    #endregion
    public MatangrRptLookupControl()
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
      IList list = this.View("RptLookup");
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

      MatangrRptLookupControl dclookup = new MatangrRptLookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys = new String[] {"Kdper", "Nmper","Mtgkey" };
      string[] targets = new String[] { "Kdper", "Nmper", "Mtgkey" };
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
  #endregion MatangrRptLookupControl
}

