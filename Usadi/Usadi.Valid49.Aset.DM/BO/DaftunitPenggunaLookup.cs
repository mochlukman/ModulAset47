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
  #region Usadi.Valid49.BO.DaftunitPenggunaLookupControl, Usadi.Valid49.Aset.DM
  [Serializable]
  public class DaftunitPenggunaLookupControl : DaftunitControl, IDataControlLookup, IHasJSScript
  {
    private static DaftunitPenggunaLookupControl _Instance = null;
    public static DaftunitPenggunaLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new DaftunitPenggunaLookupControl();
        }
        return _Instance;
      }
    }
    public DaftunitPenggunaLookupControl()
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
      DaftunitPenggunaLookupControl dclookup = new DaftunitPenggunaLookupControl();
      string title = "SKPD Pengguna"; // ConstantDict.Translate(dclookup.XMLName);
      string[] keys = new String[] { "Kdunitpengguna", "Nmunitpengguna", "Unitkeypengguna" };
      string[] targets = new String[] { "Kdunitpengguna=Kdunit", "Nmunitpengguna=Nmunit", "Unitkeypengguna=Unitkey" };
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
  
  #endregion DaftunitPenggunaLookup
}

