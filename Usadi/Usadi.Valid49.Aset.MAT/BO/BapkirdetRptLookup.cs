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
  #region Usadi.Valid49.BO.BapkirdetRptLookupControl, CoreNET.Common.BO
  [Serializable]
  public class BapkirdetRptLookupControl : BapkirdetControl, IDataControlLookup, IHasJSScript
  {
    #region Singleton
    private static BapkirdetRptLookupControl _Instance = null;
    public static BapkirdetRptLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new BapkirdetRptLookupControl();
        }
        return _Instance;
      }
    }

    private static List<BapkirdetControl> _ListData = null;
    public static void SetListDataNull()
    {
      _ListData = null;
    }
    public static List<BapkirdetControl> GetListDataSingleton()
    {
      if (_ListData == null)
      {
        BapkirdetRptLookupControl dc = new BapkirdetRptLookupControl();
        dc.SetPageKey();
        _ListData = (List<BapkirdetControl>)dc.View(BaseDataControl.LOOKUP);
      }
      return _ListData;
    }
    #endregion
    public BapkirdetRptLookupControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLBAPKIRDET;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      return cViewListProperties;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      Unitkey = (string)bo.GetValue("Unitkey");
    }
    public new IList View()
    {
      IList list = this.View("RptLookup");
      return list;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreg=No Register"), typeof(string), 15, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun=Tahun Perolehan"), typeof(string), 20, HorizontalAlign.Center));
      return columns;
    }
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
        && string.IsNullOrEmpty((string)callerCtr.GetValue("Kdaset"));

      BapkirdetRptLookupControl dclookup = new BapkirdetRptLookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys = new String[] { "Kdaset", "Nmaset", "Noreg" };
      string[] targets = new String[] { "Kdaset", "Nmaset","Noreg" };
      ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 20, 75, 0 }, targets)
      {
        Label = "No Register",
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
      return "Kdaset=Nmaset";
    }
  }
  #endregion BapkirdetRptLookupControl
}

