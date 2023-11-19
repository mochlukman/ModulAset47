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
  #region Usadi.Valid49.BO.JnskibFilterLookupControl, CoreNET.Common.BO
  [Serializable]
  public class JnskibFilterLookupControl :  JnskibControl, IDataControlLookup, IHasJSScript
  {

    public string Kelaset { get; set; }
    #region Singleton
    private static  JnskibFilterLookupControl _Instance = null;
    public static  JnskibFilterLookupControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new  JnskibFilterLookupControl();
        }
        return _Instance;
      }
    }
    private static List<JnskibControl> _ListData = null;
    public static void SetListDataNull()
    {
      _ListData = null;
    }
    public static List<JnskibControl> GetListDataSingleton()
    {
      if (_ListData == null)
      {
        JnskibFilterLookupControl dc = new JnskibFilterLookupControl();
        dc.SetPageKey();
        _ListData = (List<JnskibControl>)dc.View(BaseDataControl.LOOKUP);
      }
      return _ListData;
    }
    #endregion
    public JnskibFilterLookupControl()
    {
      XMLName = ConstantTablesAsetDM.XMLJNSKIB;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      return cViewListProperties;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      Kelaset = (string)bo.GetValue("Kelaset");
    }
    public new HashTableofParameterRow GetFilters()
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      ArrayList listkelaset = new ArrayList(new ParamControl[] {
         new ParamControl() { Kdpar="11",Nmpar="Aset Lancar"}
        ,new ParamControl() { Kdpar="13",Nmpar="Aset Tetap"}
        ,new ParamControl() { Kdpar="15",Nmpar="Aset Lainnya"}
      });
      hpars.Add(new ParameterRow(ConstantDict.GetColumnTitleEntry("Kelaset=Kelompok Aset"), ParameterRow.MODE_SELECT,
       listkelaset, "Kdpar=Nmpar", 54).SetEnable(enableFilter).SetAllowEmpty(false).SetAllowRefresh(true));
      return hpars;
    }

    public new IList View()
    {
      //IList list = this.View("Filter");
      //return list;
      string Querylabel;

      if (Kelaset == "11")
      {
        Querylabel = "Aslancar";
      }
      else if (Kelaset == "13")
      {
        Querylabel = "Astetap";
      }
      else if (Kelaset == "15")
      {
        Querylabel = "Aslain";
      }
      else
      {
        Querylabel = "Aslancar";
      }

      IList list = this.View(Querylabel);
      return list;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdkib=Kode"), typeof(string), 15, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmkib=Uraian"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Golkib=Golongan"), typeof(string), 15, HorizontalAlign.Center));
      return columns;
    }
    public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
    {
      //bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
      //  && string.IsNullOrEmpty((string)callerCtr.GetValue("Kdkib"));

      JnskibFilterLookupControl dclookup = new JnskibFilterLookupControl();
      string title = ConstantDict.Translate(dclookup.XMLName);
      string[] keys =  new String[] { "Kdkib", "Nmkib"};
      string[] targets =  new String[] { "Kdkib", "Nmkib"};
      ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 20, 75, 0 }, targets)
      {
        Label = "Jenis Barang",
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
      return "Kdkib=Nmkib";
    }
  }
  #endregion JnskibFilterLookup
}

