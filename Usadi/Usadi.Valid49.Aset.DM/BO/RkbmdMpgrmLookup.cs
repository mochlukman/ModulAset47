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
  #region Usadi.Valid49.BO.RkbmdMpgrmLookupControl, Usadi.Valid49.Aset.DM
  [Serializable]
  public class RkbmdMpgrmLookupControl : RkbmdMpgrmControl, IDataControlLookup, IHasJSScript
  {
    #region Singleton
    private static RkbmdMpgrmLookupControl _Instance = null;
    public static RkbmdMpgrmLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new RkbmdMpgrmLookupControl();
        }
        return _Instance;
      }
    }
    
    private static List<RkbmdMpgrmControl> _ListData = null;
    public static void SetListDataNull()
    {
      _ListData = null;
    }
    public static List<RkbmdMpgrmControl> GetListDataSingleton()
    {
      if (_ListData == null)
      {
        RkbmdMpgrmLookupControl dc = new RkbmdMpgrmLookupControl();
        dc.SetPageKey();
        _ListData = (List<RkbmdMpgrmControl>)dc.View(BaseDataControl.ALL);
      }
      return _ListData;
    }
    public static RkbmdMpgrmControl FindAndSetValuesInto(IDataControlUI dc)
    {
      RkbmdMpgrmControl founddc = null;
      List<RkbmdMpgrmControl> _ListData = GetListDataSingleton();
      if (_ListData != null)
      {
        founddc = (RkbmdMpgrmControl)_ListData.Find(o => o.Idprgrm.Equals(dc.GetValue("Idprgrm")));
        if (founddc != null)
        {
          if (typeof(RkbmdMpgrmControl).IsInstanceOfType(dc))
          {
            founddc.CopyPropertyBOTo(dc);
          }
          else
          {
            dc.SetValue("Idprgrm", founddc.Idprgrm);
          }
        }
      }
      return founddc;
    }
    public static string[] GetFieldValueProps()
    {
      return new string[] { "Idprgrm" };
    }
    #endregion
    public RkbmdMpgrmLookupControl()
    {
      XMLName = ConstantTablesAsetDM.XMLRKBMD_MPGRM;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      return cViewListProperties;
    }
    public new void SetPageKey()
    {
    }
    public new void SetPrimaryKey()
    {
    }

    //public new IList View()
    //{
    //  string sql = @"
    //    exec [dbo].[WSP_VIEWKEG]
		  //  @UNITKEY = N'{0}',
		  //  @OPSI = 0,
		  //  @IDPRGRM = N'{1}',
		  //  @VIEWKEG = N'mprog'
    //  ";
    //  sql = string.Format(sql, Unitkey, Idprgrm);
    //  string[] fields = new string[] { "Kdkegunit", "Nukeg", "Nmkegunit", "Type", "Kdlevel" };
    //  List<IDataControl> list = BaseDataAdapter.GetListDC(this, sql, fields);
    //  List<RkbmdMpgrmControl> ListData = new List<RkbmdMpgrmControl>();

    //  foreach (RkbmdMpgrmControl dc in list)
    //  {
    //    dc.Idprgrm = dc.Kdkegunit;
    //    dc.Nuprgrm = dc.Nukeg;
    //    dc.Nmprgrm = dc.Nmkegunit;
    //    ListData.Add(dc);
    //  }
    //  return ListData;
    //}
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      RkbmdMpgrmLookupControl dclookup = new RkbmdMpgrmLookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys = new String[] { "Nuprgrm", "Nmprgrm", "Idprgrm" };
      string[] targets = new String[] { "Nuprgrm", "Nmprgrm", "Idprgrm" };
      ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 20, 75, 0 }, targets)
      {
        Label = "Master Program",
        VisibleControls = new bool[] { true, true, !entry },
        AllowRefresh = !entry,
        DCLookup = dclookup,
        IsTree = true,
        SelectionCriteria = ParameterRow.SELECTION_CRITERIA_TYPE,
        //SelectionLevel = "1,2"
        SelectionType = "D"
      };
      return par;
    }
    public string GetFieldValueMap()
    {
      return "Idprgrm=Nmprgrm";
    }
  }
  #endregion RkbmdMpgrmLookup
}

