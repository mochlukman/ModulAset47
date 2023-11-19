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
  #region Usadi.Valid49.BO.DaftasetBosbludLookupControl, Usadi.Valid49.Aset.DM
  [Serializable]
  public class DaftasetBosbludLookupControl : DaftasetControl, IDataControlLookup, IHasJSScript
  {

    #region Singleton
    private static DaftasetBosbludLookupControl _Instance = null;
    public static DaftasetBosbludLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new DaftasetBosbludLookupControl();
        }
        return _Instance;
      }
    }
    private static List<DaftasetControl> _ListData = null;
    public static void SetListDataNull()
    {
      _ListData = null;
    }
    public static List<DaftasetControl> GetListDataSingleton()
    {
      if (_ListData == null)
      {
        DaftasetBosbludLookupControl dc = new DaftasetBosbludLookupControl();
        dc.SetPageKey();
        _ListData = (List<DaftasetControl>)dc.View(BaseDataControl.LOOKUP);
      }
      return _ListData;
    }
    public static DaftasetControl FindAndSetValuesInto(IDataControlUI dc)
    {
      DaftasetControl founddc = null;
      List<DaftasetControl> _ListData = GetListDataSingleton();
      if (_ListData != null)
      {
        founddc = (DaftasetControl)_ListData.Find(o => o.Asetkey.Equals(dc.GetValue("Asetkey")));
        if (founddc != null)
        {
          if (typeof(DaftasetControl).IsInstanceOfType(dc))
          {
            founddc.CopyPropertyBOTo(dc);
          }
          else
          {
            dc.SetValue("Asetkey", founddc.Asetkey);
          }
        }
      }
      return founddc;
    }
    public static string[] GetFieldValueProps()
    {
      return new string[] { "Asetkey" };
    }
    #endregion
    public DaftasetBosbludLookupControl()
    {
      XMLName = ConstantTablesAsetDM.XMLDAFTASET;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      return cViewListProperties;
    }
    public new HashTableofParameterRow GetFilters()
    {
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"),
      GetList(new DaftasetObjekLookupControl()), "Kdaset=Kdnmaset", 110).SetAllowRefresh(true).SetEnable(true).SetAllowEmpty(false));
      //hpars.Add(DaftasetObjekLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(true).SetAllowRefresh(true).SetAllowEmpty(false));
      return hpars;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 60, HorizontalAlign.Left));

      return columns;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      Mtgkey = (string)bo.GetValue("Mtgkey");
      Kddana = (string)bo.GetValue("Kddana");
    }
    public new IList View()
    {
      IList list = this.View("Bosblud");
      return list;
    }
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      //bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
      //  && string.IsNullOrEmpty((string)callerCtr.GetValue("Asetkey"));

      DaftasetBosbludLookupControl dclookup = new DaftasetBosbludLookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys = new String[] { "Kdaset", "Nmaset", "Asetkey" };
      string[] targets = new String[] { "Kdaset", "Nmaset", "Asetkey" };
      ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 20, 75, 0 }, targets)
      {
        Label = "Kode Barang",
        VisibleControls = new bool[] { true, true, !entry },
        AllowRefresh = !entry,
        DCLookup = dclookup,
        IsTree = false,
        SelectionCriteria = ParameterRow.SELECTION_CRITERIA_TYPE,
        SelectionLevel = "D"
        //SelectionType = "3"
      };
      //par.SetEnable(enableFilter);
      return par;
    }
    public string GetFieldValueMap()
    {
      return "Kdaset=Nmaset";
    }
  }
  #endregion DaftasetBosbludLookup
}

