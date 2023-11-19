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
  #region Usadi.Valid49.BO.DaftruangLookupControl, CoreNET.Common.BO
  [Serializable]
  public class DaftruangLookupControl :  DaftruangControl, IDataControlLookup, IHasJSScript
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
    public DaftruangLookupControl()
    {
      XMLName = ConstantTablesAsetDM.XMLDAFTRUANG;
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
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdruang=Kode"), typeof(int), 15, HorizontalAlign.Left).SetEditable(false));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmruang=Ruangan"), typeof(string), 30, HorizontalAlign.Left).SetEditable(false));
      return columns;
    }
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
        && string.IsNullOrEmpty((string)callerCtr.GetValue("Ruangkey"));

      DaftruangLookupControl dclookup = new DaftruangLookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys =  new String[] { "Kdruang", "Nmruang", "Ruangkey" };
      string[] targets =  new String[] { "Kdruang", "Nmruang", "Ruangkey" };
      ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 20, 75, 0 }, targets)
      {
        Label = "Daftar Ruang",
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
      return "Kdruang=Nmruang";
    }
  }
  #endregion DaftruangLookup
}

