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
  #region Usadi.Valid49.BO.SpbLookupControl, CoreNET.Common.BO
  [Serializable]
  public class SpbLookupControl : InvspbControl, IDataControlLookup, IHasJSScript
  {
    #region Singleton
    private static SpbLookupControl _Instance = null;
    public static SpbLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new SpbLookupControl();
        }
        return _Instance;
      }
    }
    private static List<InvspbControl> _ListData = null;
    public static void SetListDataNull()
    {
      _ListData = null;
    }
    public static List<InvspbControl> GetListDataSingleton()
    {
      if (_ListData == null)
      {
        SpbLookupControl dc = new SpbLookupControl();
        dc.SetPageKey();
        _ListData = (List<InvspbControl>)dc.View(BaseDataControl.LOOKUP);
      }
      return _ListData;
    }
    #endregion
    public SpbLookupControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLINVSPB;
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
      //IList list = this.View(BaseDataControl.LOOKUP);
      return list;

    }
    public new void SetFilterKey(BaseBO bo)
    {
      Unitkey2 = (string)bo.GetValue("Unitkey");
      Kdunit2 = (string)bo.GetValue("Kdunit");
      Nmunit2 = (string)bo.GetValue("Nmunit");
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
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nospb=No SPB"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nonpb=No NPB"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglspb=Tanggal SPB"), typeof(DateTime), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Untuk=Untuk"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Ket"), typeof(string), 50, HorizontalAlign.Left));
      return columns;
    }
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
        && string.IsNullOrEmpty((string)callerCtr.GetValue("Nospb"));

      SpbLookupControl dclookup = new SpbLookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys = new String[] { "Nospb", "Tglspb" };
      string[] targets = new String[] { "Nospb", "Tglspb" };
      ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 50, 25, 0 }, targets)
      {
        Label = "No SPB",
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
      return "Nospb=Nospb";
    }
  }
  #endregion SpbLookup
}

