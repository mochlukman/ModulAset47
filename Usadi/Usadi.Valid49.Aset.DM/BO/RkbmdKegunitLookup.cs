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
  #region Usadi.Valid49.BO.RkbmdKegunitLookupControl, CoreNET.Common.BO
  [Serializable]
  public class RkbmdKegunitLookupControl : RkbmdKegunitControl, IDataControlLookup, IHasJSScript
  {
    #region Singleton
    private static RkbmdKegunitLookupControl _Instance = null;
    public static RkbmdKegunitLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new RkbmdKegunitLookupControl();
        }
        return _Instance;
      }
    }

    private static List<RkbmdKegunitControl> _ListData = null;
    public static void SetListDataNull()
    {
      _ListData = null;
    }
    public static List<RkbmdKegunitControl> GetListDataSingleton()
    {
      if (_ListData == null)
      {
        RkbmdKegunitLookupControl dc = new RkbmdKegunitLookupControl();
        dc.SetPageKey();
        _ListData = (List<RkbmdKegunitControl>)dc.View(BaseDataControl.LOOKUP);
      }
      return _ListData;
    }
    public static RkbmdKegunitControl FindAndSetValuesInto(IDataControlUI dc)
    {
      RkbmdKegunitControl founddc = null;
      List<RkbmdKegunitControl> _ListData = GetListDataSingleton();
      if (_ListData != null)
      {
        founddc = (RkbmdKegunitControl)_ListData.Find(o => o.Kdkegunit.Equals(dc.GetValue("Kdkegunit")));
        if (founddc != null)
        {
          if (typeof(RkbmdKegunitControl).IsInstanceOfType(dc))
          {
            founddc.CopyPropertyBOTo(dc);
          }
          else
          {
            dc.SetValue("Kdkegunit", founddc.Kdkegunit);
          }
        }
      }
      return founddc;
    }
    public static string[] GetFieldValueProps()
    {
      return new string[] { "Kdkegunit" };
    }
    #endregion
    public RkbmdKegunitLookupControl()
    {
      XMLName = ConstantTablesAsetDM.XMLRKBMD_KEGUNIT;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      //cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      return cViewListProperties;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      Unitkey = (string)bo.GetValue("Unitkey");
      Kdkegunit = (string)bo.GetValue("Kdkegunit");
      Thang = (string)bo.GetValue("Thang");
    }

    public new IList View()
    {
      string sql = @"
        exec [dbo].[RKBMD_TREEVIEWKEG]
		    @UNITKEY = N'{0}',
		    @THANG = N'{1}'
      ";
      sql = string.Format(sql, Unitkey, Thang);
      string[] fields = new string[] { "Kdkegunit", "Nukeg", "Nmkegunit", "Type", "Kdlevel" };
      List<IDataControl> list = BaseDataAdapter.GetListDC(this, sql, fields);
      List<RkbmdKegunitControl> ListData = new List<RkbmdKegunitControl>();      

      foreach (RkbmdKegunitControl dc in list)
      {
        dc.Unitkey = dc.Unitkey;
        dc.Kdunit = dc.Kdunit;
        dc.Nmunit = dc.Nmunit;
        dc.Idprgrm = dc.Idprgrm;
        dc.Nuprgrm = dc.Nuprgrm;
        dc.Nmprgrm = dc.Nmprgrm;
        ListData.Add(dc);
      }
      return ListData;
    }

    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nukeg"), typeof(string), 30, HorizontalAlign.Left).SetEditable(false));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmkegunit"), typeof(string), 90, HorizontalAlign.Left).SetEditable(false));
      return columns;
    }
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
        && string.IsNullOrEmpty((string)callerCtr.GetValue("Kdkegunit"));

      RkbmdKegunitLookupControl dclookup = new RkbmdKegunitLookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys = new String[] { "Nukeg", "Nmkegunit", "Kdkegunit" };
      string[] targets = new String[] { "Nukeg", "Nmkegunit", "Kdkegunit" };
      ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 20, 75, 0 }, targets)
      {
        Label = "Kegiatan",
        VisibleControls = new bool[] { true, true, !entry },
        AllowRefresh = !entry,
        DCLookup = dclookup,
        IsTree = true,
        SelectionCriteria = ParameterRow.SELECTION_CRITERIA_TYPE,
        SelectionType = "D"
      };
      return par;
    }
    public string GetFieldValueMap()
    {
      return "Kdkegunit=Kdkegunit";
    }
  }
  #endregion RkbmdKegunitLookup
}

