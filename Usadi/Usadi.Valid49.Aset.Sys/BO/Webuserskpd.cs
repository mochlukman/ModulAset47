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
  #region Usadi.Valid49.BO.WebuserskpdControl, Usadi.Valid49.Aset.Sys
  [Serializable]
  public class WebuserskpdControl : BaseDataControlAsetSys, IDataControlUIEntry, IHasJSScript
  {
    #region Properties 
    public new string Idapp { get; set; }
    public string Unitkey { get; set; }
    public string Kdunit { get; set; }
    public string Nmunit { get; set; }
    public string Nama { get; set; }

    #endregion Properties 

    #region Methods 
    public WebuserskpdControl()
    {
      XMLName = ConstantTablesAsetSys.XMLWEBUSERSKPD;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Userid","Unitkey" };
      cViewListProperties.IDKey = "Unitkey";//IDKey for ID Notes
      cViewListProperties.IDProperty = "Unitkey";//UniqueKey in gridview
      cViewListProperties.ReadOnlyFields = new String[] {"Userid"};//Key in GetFilters should put here
      cViewListProperties.SortFields = new String[] {"Kdunit"};//
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_DEL;
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_TREE;
      cViewListProperties.LookupDC = "Usadi.Valid49.BO.DaftunitUserskpdControl, Usadi.Valid49.Aset.DM";
      cViewListProperties.LookupLabelQuery = "";
      cViewListProperties.AllowMultiDelete = true;
      return cViewListProperties;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdunit=Kode SKPD"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmunit=Nama SKPD"), typeof(string), 70, HorizontalAlign.Left));
      return columns;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      if (typeof(IDataControlMenu).IsInstanceOfType(bo))
      {
        Last_by = ((BaseBO)bo).Userid;
      }
      else if (typeof(DaftunitUserskpdControl).IsInstanceOfType(bo))
      {
        Unitkey = ((DaftunitUserskpdControl)bo).Unitkey;
      }
    }
    public new HashTableofParameterRow GetFilters()
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(WebuserskpdLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(false).SetEnable(enableFilter));
      return hpars;
    }
    public new IList View()
    {
      Idapp = GlobalAsp.GetSessionApp();
      IList list = this.View(BaseDataControl.ALL);
      return list;
    }
    public new IList View(string label)
    {
      IList list = ((BaseDataControl)this).View(label);
      List<WebuserskpdControl> ListData = new List<WebuserskpdControl>();
      foreach (WebuserskpdControl dc in list)
      {
        ListData.Add(dc);
      }
      //Update(ListData);
      return ListData;
    }
    public new int Delete()
    {
      Status = -1;
      int n = ((BaseDataControlUI)this).Delete(BaseDataControl.DEFAULT);
      return n;
    }
    public override HashTableofParameterRow GetEntries()
    {
      bool enable = true;
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Userid"), true, 90).SetEnable(enable));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Unitkey"), true, 90).SetEnable(enable));
      return hpars;
    }
    #endregion Methods 
  }
  #endregion Webuserskpd
}

