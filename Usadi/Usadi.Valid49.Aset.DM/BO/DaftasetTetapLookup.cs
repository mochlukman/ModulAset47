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
  #region Usadi.Valid49.BO.DaftasetTetapLookupControl, Usadi.Valid49.Aset.DM
  [Serializable]
  public class DaftasetTetapLookupControl : DaftasetLookupControl, IDataControlLookup, IHasJSScript
  {
    private static DaftasetTetapLookupControl _Instance = null;
    public new static DaftasetTetapLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new DaftasetTetapLookupControl();
        }
        return _Instance;
      }
    }
    public DaftasetTetapLookupControl()
    {
      XMLName = ConstantTablesAsetDM.XMLDAFTASET;
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
      IList list = this.View("Asettetap");
      return list;
    }
    public new ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      DaftasetTetapLookupControl dclookup = new DaftasetTetapLookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys = new String[] { "Kdaset", "Nmaset", "Asetkey" };
      string[] targets = new String[] { "Kdaset", "Nmaset", "Asetkey" };
      ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 20, 75, 0 }, targets)
      {
        Label = "Kode Barang",
        VisibleControls = new bool[] { true, true, !entry },
        AllowRefresh = !entry,
        DCLookup = dclookup,
        IsTree = true,
        SelectionCriteria = ParameterRow.SELECTION_CRITERIA_TYPE,
        SelectionLevel = "D"
        //SelectionType = "3"
      };
      return par;
    }
  }
  #endregion DaftasetTetapLookup
}

