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
  #region Usadi.Valid49.BO.DaftunitUrusLookupControl, Usadi.Valid49.Aset.DM
  [Serializable]
  public class DaftunitUrusLookupControl : DaftunitLookupControl, IDataControlLookup, IHasJSScript
  {
    private static DaftunitUrusLookupControl _Instance = null;
    public new static DaftunitUrusLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new DaftunitUrusLookupControl();
        }
        return _Instance;
      }
    }
    public DaftunitUrusLookupControl()
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
      IList list = this.View("Urus");
      return list;
    }
    public new ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      DaftunitUrusLookupControl dclookup = new DaftunitUrusLookupControl();
      string title = "Urusan"; // ConstantDict.Translate(dclookup.XMLName);
      string[] keys = new String[] { "Kdurus", "Nmurus", "Uruskey" };
      string[] targets = new String[] { "Kdurus=Kdunit", "Nmurus=Nmunit", "Uruskey=Unitkey" };
      ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 20, 75, 0 }, targets)
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
  }
  #endregion DaftunitUrusLookup
}

