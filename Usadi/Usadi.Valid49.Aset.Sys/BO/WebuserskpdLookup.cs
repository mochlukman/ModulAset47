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
  #region Usadi.Valid49.BO.WebuserskpdLookupControl, CoreNET.Common.BO
  [Serializable]
  public class WebuserskpdLookupControl : WebuserControl, IDataControlLookup, IHasJSScript
  {
    #region Singleton
    private static WebuserskpdLookupControl _Instance = null;
    public static WebuserskpdLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new WebuserskpdLookupControl();
        }
        return _Instance;
      }
    }

    private static List<WebuserControl> _ListData = null;
    public static void SetListDataNull()
    {
      _ListData = null;
    }
    public static List<WebuserControl> GetListDataSingleton()
    {
      if (_ListData == null)
      {
        WebuserskpdLookupControl dc = new WebuserskpdLookupControl();
        dc.SetPageKey();
        _ListData = (List<WebuserControl>)dc.View(BaseDataControl.LOOKUP);
      }
      return _ListData;
    }
    #endregion
    public WebuserskpdLookupControl()
    {
      XMLName = ConstantTablesAsetSys.XMLWEBUSER;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      return cViewListProperties;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      Idapp = (string)bo.GetValue("Idapp");
    }
    public new IList 
      View()
    {
      IList list = this.View("Userskpd");
      return list;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Userid"), typeof(string), 30, HorizontalAlign.Left).SetEditable(true));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nama"), typeof(string), 50, HorizontalAlign.Left).SetEditable(true));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket"), typeof(string), 50, HorizontalAlign.Left).SetEditable(true));
      return columns;
    }
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      WebuserskpdLookupControl dclookup = new WebuserskpdLookupControl();
      string title = "User";
      string[] keys = new String[] { "Userid", "Nama", "Userid" };
      string[] targets = new String[] { "Nama=Nama", "Userid=Userid" };
      ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 20, 75, 0 }, targets)
      {
        Label = title,
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
      return "Userid=Nama";
    }
    public static bool IsRootCondition(DaftunitControl dc)
    {
      //DaftunitControl cDaftunitceklevel = new DaftunitControl();
      //cDaftunitceklevel.Load("Kdlevelmin");

      return (dc.Kdlevel == 3);
    }
  }
  #endregion WebuserskpdLookupControl
}

