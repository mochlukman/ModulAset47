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
  #region Usadi.Valid49.BO.KegunitBastLookupControl, CoreNET.Common.BO
  [Serializable]
  public class KegunitBastLookupControl : KegunitControl, IDataControlLookup, IHasJSScript
  {
    #region Singleton
    private static KegunitBastLookupControl _Instance = null;
    public static KegunitBastLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new KegunitBastLookupControl();
        }
        return _Instance;
      }
    }

    private static List<KegunitControl> _ListData = null;
    public static void SetListDataNull()
    {
      _ListData = null;
    }
    public static List<KegunitControl> GetListDataSingleton()
    {
      if (_ListData == null)
      {
        KegunitBastLookupControl dc = new KegunitBastLookupControl();
        dc.SetPageKey();
        _ListData = (List<KegunitControl>)dc.View(BaseDataControl.LOOKUP);
      }
      return _ListData;
    }
    public static KegunitControl FindAndSetValuesInto(IDataControlUI dc)
    {
      KegunitControl founddc = null;
      List<KegunitControl> _ListData = GetListDataSingleton();
      if (_ListData != null)
      {
        founddc = (KegunitControl)_ListData.Find(o => o.Kdkegunit.Equals(dc.GetValue("Kdkegunit")));
        if (founddc != null)
        {
          if (typeof(KegunitControl).IsInstanceOfType(dc))
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
    public KegunitBastLookupControl()
    {
      XMLName = ConstantTablesAsetDM.XMLKEGUNIT;
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
      Kdtahap = (string)bo.GetValue("Kdtahap");
    }

    public new IList View()
    {
      string sql = @"
        exec [dbo].[TREEVIEWKEG]
		    @UNITKEY = N'{0}',
		    @KDTAHAP = N'{1}'
      ";
      sql = string.Format(sql, Unitkey, Kdtahap);
      string[] fields = new string[] { "Kdkegunit", "Nukeg", "Nmkegunit", "Type", "Kdlevel" };
      List<IDataControl> list = BaseDataAdapter.GetListDC(this, sql, fields);
      List<KegunitControl> ListData = new List<KegunitControl>();      

      foreach (KegunitControl dc in list)
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

      KegunitBastLookupControl dclookup = new KegunitBastLookupControl();
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
  #endregion KegunitBastLookup
}

