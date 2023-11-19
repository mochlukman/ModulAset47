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
  #region Usadi.Valid49.BO.DaftasetKibfilterLookupControl, Usadi.Valid49.Aset.DM
  [Serializable]
  public class DaftasetKibfilterLookupControl :  DaftasetControl, IDataControlLookup, IHasJSScript
  {
    #region Properties
    public string Kdobjekaset { get; set; }
    public string Nmobjekaset { get; set; }
    #endregion Properties

    #region Singleton
    private static  DaftasetKibfilterLookupControl _Instance = null;
    public static  DaftasetKibfilterLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new  DaftasetKibfilterLookupControl();
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
    //      DaftasetKibfilterLookupControl dc = new DaftasetKibfilterLookupControl();
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
        DaftasetKibfilterLookupControl dc = new DaftasetKibfilterLookupControl();
        dc.SetPageKey();
        _ListData = (List<DaftasetControl>)dc.View(BaseDataControl.LOOKUP);
      }
      return _ListData;
    }
    public static DaftasetControl FindAndSetValuesInto(IDataControlUI dc)
    {
      DaftasetControl founddc = null;
      List<DaftasetControl> _ListData = GetListDataSingleton();
      if(_ListData != null)
      {
        founddc = (DaftasetControl)_ListData.Find(o=>o.Asetkey.Equals(dc.GetValue("Asetkey")));
        if(founddc != null)
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
      return new string[]{ "Asetkey" };
    }
    #endregion
    public DaftasetKibfilterLookupControl()
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
    public new HashTableofParameterRow GetFilters()
    {
      HashTableofParameterRow hpars = new HashTableofParameterRow();
            if (Kddana == "00")
            {
                hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"),
                GetList(new DaftasetObjeksusutLookupControl()), "Kdaset=Kdnmaset", 110).SetAllowRefresh(true).SetEnable(true).SetAllowEmpty(false));
            }
            else
            {
                hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"),
                GetList(new DaftasetObjekLookupControl()), "Kdaset=Kdnmaset", 110).SetAllowRefresh(true).SetEnable(true).SetAllowEmpty(false));
            }
      //hpars.Add(DaftasetObjekLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(true).SetAllowRefresh(true).SetAllowEmpty(false));
      return hpars;
    }
    //public new HashTableofParameterRow GetFilters()
    //{
    //  HashTableofParameterRow hpars = new HashTableofParameterRow();
    //  hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdkib=Jenis KIB"),
    //  GetList(new JnskibTransLookupControl()), "Kdkib=Nmkib", 48).SetAllowRefresh(true).SetEnable(true).SetAllowEmpty(false));

    //  return hpars;
    //}
    public new void SetFilterKey(BaseBO bo)
    {
      Kddana = (string)bo.GetValue("Kddana");
    }
    public new IList View()
    {
      IList list = this.View("Kibfilter");
      return list;
    }
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      //bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
      //  && string.IsNullOrEmpty((string)callerCtr.GetValue("Asetkey"));

      DaftasetKibfilterLookupControl dclookup = new DaftasetKibfilterLookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys =  new String[] { "Kdaset", "Nmaset", "Asetkey" };
      string[] targets = new String[] { "Kdaset", "Nmaset", "Asetkey" };
      ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys,new int[] { 20, 75, 0 }, targets)
      {
        Label = "Kode Barang",
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
  #endregion DaftasetKibfilterLookup
}

