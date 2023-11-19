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
  #region Usadi.Valid49.BO.DaftunitSkpenggunaLookupControl, Usadi.Valid49.Aset.DM
  [Serializable]
  public class DaftunitSkpenggunaLookupControl : DaftunitControl, IDataControlLookup, IHasJSScript
  {
    private static DaftunitSkpenggunaLookupControl _Instance = null;
    public static DaftunitSkpenggunaLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new DaftunitSkpenggunaLookupControl();
        }
        return _Instance;
      }
    }
    public DaftunitSkpenggunaLookupControl()
    {
      XMLName = ConstantTablesAsetDM.XMLDAFTUNIT;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      return cViewListProperties;
    }
    public new void SetPageKey()
    {
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
      IList list = this.View("Unit");
      return list;
    }
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      DaftunitSkpenggunaLookupControl dclookup = new DaftunitSkpenggunaLookupControl();
      string title = "SKPD Pengguna"; // ConstantDict.Translate(dclookup.XMLName);
      string[] keys = new String[] { "Kdunit2", "Nmunit2", "Unitkey2" };
      string[] targets = new String[] { "Kdunit2=Kdunit", "Nmunit2=Nmunit", "Unitkey2=Unitkey" };
      ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 26, 63, 0 }, targets)
      {
        Label = title,
        VisibleControls = new bool[] { true, true, !entry },
        AllowRefresh = !entry,
        DCLookup = dclookup,
        IsTree = false,
        SelectionCriteria = ParameterRow.SELECTION_CRITERIA_LEVEL,
        //SelectionType = "D"
        SelectionLevel = "3,4"
      };
      return par;
    }
    public string GetFieldValueMap()
    {
      return "Kdunit=Nmunit";
    }
  }
  #endregion DaftunitSkpenggunaLookup
}

