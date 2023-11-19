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
  #region Usadi.Valid49.BO.DaftunitLookupControl, Usadi.Valid49.Aset.DM
  [Serializable]
  public class DaftunitLookupControl : DaftunitControl, IDataControlLookup, IHasJSScript
  {
    #region Singleton
    private static DaftunitLookupControl _Instance = null;
    public static DaftunitLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new DaftunitLookupControl();
        }
        return _Instance;
      }
    }
    private static List<DaftunitControl> _ListData = null;
    public static void SetListDataNull()
    {
      _ListData = null;
    }
    public static List<DaftunitControl> GetListDataSingleton()
    {
      if (_ListData == null)
      {
        DaftunitLookupControl dc = new DaftunitLookupControl();
        dc.SetPageKey();
        _ListData = (List<DaftunitControl>)dc.View(BaseDataControl.LOOKUP);
      }
      return _ListData;
    }
    public static DaftunitControl FindAndSetValuesInto(IDataControlUI dc)
    {
      DaftunitControl founddc = null;
      List<DaftunitControl> _ListData = GetListDataSingleton();
      if (_ListData != null)
      {
        founddc = (DaftunitControl)_ListData.Find(o => o.Unitkey.Equals(dc.GetValue("Unitkey")));
        if (founddc != null)
        {
          if (typeof(DaftunitControl).IsInstanceOfType(dc))
          {
            founddc.CopyPropertyBOTo(dc);
          }
          else
          {
            dc.SetValue("Unitkey", founddc.Unitkey);
          }
        }
      }
      return founddc;
    }
    public static string[] GetFieldValueProps()
    {
      return new string[] { "Unitkey" };
    }
    #endregion
    public DaftunitLookupControl()
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
      Userid = GlobalAsp.GetSessionUser().GetUserID();
    }
    public new IList View()
    {
      string sql = @"
        exec [dbo].[TREEVIEWUNIT]
		    @USERID = N'{0}'
      ";
      sql = string.Format(sql, Userid);
      string[] fields = new string[] { "Unitkey", "Kdlevel", "Kdunit", "Nmunit", "Akrounit", "Alamat", "Telepon", "Type" };
      List<IDataControl> list = BaseDataAdapter.GetListDC(this, sql, fields);
      List<DaftunitControl> ListData = new List<DaftunitControl>();

      foreach (DaftunitControl dc in list)
      {
        dc.Unitkey = dc.Unitkey;
        dc.Kdunit = dc.Kdunit;
        dc.Nmunit = dc.Nmunit;
        dc.Kdlevel = dc.Kdlevel;
        dc.Akrounit = dc.Akrounit;
        dc.Alamat = dc.Alamat;
        dc.Telepon = dc.Telepon;
        dc.Type = dc.Type;
        ListData.Add(dc);
      }
      return ListData;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdunit"), typeof(string), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmunit"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdlevel"), typeof(string), 10, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Type"), typeof(string), 10, HorizontalAlign.Center));

      return columns;
    }
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      bool enabbeTree = true;
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
        && string.IsNullOrEmpty((string)callerCtr.GetValue("Unitkey"));

      DaftunitControl cGetkdgroup = new DaftunitControl();
      Userid = GlobalAsp.GetSessionUser().GetUserID();
      cGetkdgroup.Userid = Userid;
      cGetkdgroup.Load("Getkdgroup");

      if (cGetkdgroup.Kdgroup == "01")
      {
        enabbeTree = true;
      }

      DaftunitLookupControl dclookup = new DaftunitLookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys = new String[] { "Kdunit", "Nmunit", "Unitkey" };
      string[] targets = new String[] { "Kdunit", "Nmunit", "Unitkey" };
      ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 20, 75, 0 }, targets)
      {
        Label = "SKPD",
        VisibleControls = new bool[] { true, true, !entry },
        AllowRefresh = !entry,
        DCLookup = dclookup,
        IsTree = enabbeTree,
        SelectionCriteria = ParameterRow.SELECTION_CRITERIA_LEVEL,
        //SelectionType = "D"
        SelectionLevel = "3,4,5,6"
      };
      par.SetEnable(enableFilter);
      return par;
    }
    public string GetFieldValueMap()
    {
      return "Kdunit=Nmunit";
    }
  }
  #endregion DaftunitLookup
}

