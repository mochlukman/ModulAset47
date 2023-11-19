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
  #region Usadi.Valid49.BO.Daftphk3LookupControl, CoreNET.Common.BO
  [Serializable]
  public class Daftphk3LookupControl :  Daftphk3Control, IDataControlLookup, IHasJSScript
  {
    #region Singleton
    private static  Daftphk3LookupControl _Instance = null;
    public static  Daftphk3LookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new  Daftphk3LookupControl();
        }
        return _Instance;
      }
    }
    //public static void SetSessionListDataNull()
    //{
    //  string session_name = (new Daftphk3Control()).XMLName; ;
    //  HttpContext.Current.Session[session_name] = null;
    //}
    //public static List<Daftphk3Control> GetSessionListDataSingleton()
    //{
    //  string session_name = (new Daftphk3Control()).XMLName; ;
    //  List<Daftphk3Control> _ListData = (List<Daftphk3Control>)HttpContext.Current.Session[session_name];
    //  if (_ListData == null)
    //  {
    //    try
    //    {
    //      Daftphk3LookupControl dc = new Daftphk3LookupControl();
    //      dc.SetPageKey();
    //      _ListData = (List<Daftphk3Control>)dc.View(BaseDataControl.ALL);
    //    }
    //    catch(Exception){}
    //    HttpContext.Current.Session[session_name] = _ListData;
    //  }
    //  return _ListData;
    //}
    private static List<Daftphk3Control> _ListData = null;
    public static void SetListDataNull()
    {
      _ListData = null;
    }
    public static List<Daftphk3Control> GetListDataSingleton()
    {
      if (_ListData == null)
      {
        Daftphk3LookupControl dc = new Daftphk3LookupControl();
        dc.SetPageKey();
        _ListData = (List<Daftphk3Control>)dc.View(BaseDataControl.LOOKUP);
      }
      return _ListData;
    }
    #endregion
    public Daftphk3LookupControl()
    {
      XMLName = ConstantTablesAsetDM.XMLDAFTPHK3;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      return cViewListProperties;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      Unitkey = (string)bo.GetValue("Unitkey");
      Kdunit = (string)bo.GetValue("Kdunit");
      Nmunit = (string)bo.GetValue("Nmunit");
    }
    //public new IList View()
    //{
    //  IList list = this.View("Lookup");
    //  return list;
    //}
    public new IList View()
    {
      string sql = @"
        exec [dbo].[WSPV_DAFTPHK3]
		    @UNITKEY = N'{0}'
      ";
      sql = string.Format(sql, Unitkey);
      string[] fields = new string[] { "Id", "Kdp3", "Nmp3", "Nminst", "Idbank", "Nmbank", "Cabangbank", "Alamatbank", "Norekbank", "Kdjenis"
        , "Alamat", "Telepon", "Npwp", "Unitkey", "Kdunit", "Nmunit", "Stdvalid"};
      List<IDataControl> list = BaseDataAdapter.GetListDC(this, sql, fields);
      List<Daftphk3Control> ListData = new List<Daftphk3Control>();

      foreach (Daftphk3Control dc in list)
      {
        dc.Id = dc.Id;
        dc.Kdp3 = dc.Kdp3;
        dc.Nminst = dc.Nminst;
        dc.Idbank = dc.Idbank;
        dc.Nmbank = dc.Nmbank;
        dc.Cabangbank = dc.Cabangbank;
        dc.Alamatbank = dc.Alamatbank;
        dc.Norekbank = dc.Norekbank;
        dc.Kdjenis = dc.Kdjenis;
        dc.Alamat = dc.Alamat;
        dc.Telepon = dc.Telepon;
        dc.Npwp = dc.Npwp;
        dc.Unitkey = dc.Unitkey;
        dc.Kdunit = dc.Kdunit;
        dc.Nmunit = dc.Nmunit;
        dc.Stvalid = dc.Stvalid;
        ListData.Add(dc);
      }
      return ListData;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdp3=Kode"), typeof(string), 15, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nminst=Nama Instansi"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmp3=Pimpinan"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Alamat"), typeof(string), 50, HorizontalAlign.Left));
      return columns;
    }
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      //bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
      //  && string.IsNullOrEmpty((string)callerCtr.GetValue("Kdp3"));

      Daftphk3LookupControl dclookup = new Daftphk3LookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys =  new String[] { "Nmp3", "Nminst", "Kdp3" };
      string[] targets =  new String[] { "Nmp3", "Nminst", "Kdp3" };
      ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys,new int[] { 26, 63, 0 }, targets)
      {
        Label = "Mitra Pemanfaatan",
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
      return "Kdp3=Nminst";
    }
  }
  #endregion Daftphk3Lookup
}

