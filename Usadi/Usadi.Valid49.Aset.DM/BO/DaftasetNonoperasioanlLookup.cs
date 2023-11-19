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
  #region Usadi.Valid49.BO.DaftasetNonoperasionalLookupControl, Usadi.Valid49.Aset.DM
  [Serializable]
  public class DaftasetNonoperasionalLookupControl : DaftasetControl, IDataControlLookup, IHasJSScript
  {
    #region Singleton
    private static DaftasetNonoperasionalLookupControl _Instance = null;
    public static DaftasetNonoperasionalLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new DaftasetNonoperasionalLookupControl();
        }
        return _Instance;
      }
    }
    //public static void SetSessionListDataNull()
    //{
    //  string session_name = (new DaftasetControl()).XMLName; ;
    //  HttpContext.Current.Session[session_name] = null;
    //}
    //public static List<DaftasetControl> GetSessionListDataSingleton()
    //{
    //  string session_name = (new DaftasetControl()).XMLName; ;
    //  List<DaftasetControl> _ListData = (List<DaftasetControl>)HttpContext.Current.Session[session_name];
    //  if (_ListData == null)
    //  {
    //    try
    //    {
    //      DaftasetNonoperasionalLookupControl dc = new DaftasetNonoperasionalLookupControl();
    //      dc.SetPageKey();
    //      _ListData = (List<DaftasetControl>)dc.View(BaseDataControl.ALL);
    //    }
    //    catch(Exception){}
    //    HttpContext.Current.Session[session_name] = _ListData;
    //  }
    //  return _ListData;
    //}
    private static List<DaftasetControl> _ListData = null;
    public static void SetListDataNull()
    {
      _ListData = null;
    }
    public static List<DaftasetControl> GetListDataSingleton()
    {
      if (_ListData == null)
      {
        DaftasetNonoperasionalLookupControl dc = new DaftasetNonoperasionalLookupControl();
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
    public DaftasetNonoperasionalLookupControl()
    {
      XMLName = ConstantTablesAsetDM.XMLDAFTASET;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      return cViewListProperties;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 60, HorizontalAlign.Left));

      return columns;
    }
    public new void SetPageKey()
    {
    }
    //public new void SetFilterKey(BaseBO bo)
    //{
    //  Kdkib = (string)bo.GetValue("Kdkib");
    //}
    public new IList View()
    {
      IList list = this.View("Nonoperasional");
      return list;
    }
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      //bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
      //  && string.IsNullOrEmpty((string)callerCtr.GetValue("Asetkey"));

      DaftasetNonoperasionalLookupControl dclookup = new DaftasetNonoperasionalLookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys = new String[] { "Kdaset2", "Nmaset2", "Asetkey2" };
      string[] targets = new String[] { "Kdaset2=Kdaset", "Nmaset2=Nmaset", "Asetkey2=Asetkey" };
      ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 20, 75, 0 }, targets)
      {
        Label = "Kode Barang Baru",
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
  #endregion DaftasetNonoperasionalLookup
}

