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
  #region Usadi.Valid49.BO.StskirLookupControl, CoreNET.Common.BO
  [Serializable]
  public class StskirLookupControl :  StskirControl, IDataControlLookup, IHasJSScript
  {
    #region Singleton
    private static DaftruangLookupControl _Instance = null;
    public static DaftruangLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new DaftruangLookupControl();
        }
        return _Instance;
      }
    }
    
    private static List<DaftruangControl> _ListData = null;
    public static void SetListDataNull()
    {
      _ListData = null;
    }
    public static List<DaftruangControl> GetListDataSingleton()
    {
      if (_ListData == null)
      {
        DaftruangLookupControl dc = new DaftruangLookupControl();
        dc.SetPageKey();
        _ListData = (List<DaftruangControl>)dc.View(BaseDataControl.LOOKUP);
      }
      return _ListData;
    }
    #endregion
    public StskirLookupControl()
    {
      XMLName = ConstantTablesAsetDM.XMLSTSKIR;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      return cViewListProperties;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      if (typeof(IDataControlMenu).IsInstanceOfType(bo))
      {
        Last_by = ((BaseBO)bo).Userid;
      }
    }
    public new IList View()
    {
      IList list = this.View("All");
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
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdbapkir=Kode"), typeof(int), 15, HorizontalAlign.Left).SetEditable(false));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmbapkir=Jenis Transaksi KIR"), typeof(string), 30, HorizontalAlign.Left).SetEditable(false));
      return columns;
    }
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
        && string.IsNullOrEmpty((string)callerCtr.GetValue("Kdbapkir"));

      DaftruangLookupControl dclookup = new DaftruangLookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys =  new String[] { "Kdbapkir","Nmbapkir" };
      string[] targets =  new String[] { "Kdbapkir", "Nmbapkir" };
      ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 20, 75, 0 }, targets)
      {
        Label = "Jenis Transaksi KIR",
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
      return "Kdbapkir=Nmbapkir";
    }
  }
  #endregion StskirLookup
}

