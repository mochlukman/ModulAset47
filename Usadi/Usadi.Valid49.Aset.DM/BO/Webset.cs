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
  #region Usadi.Valid49.BO.WebsetControl, Usadi.Valid49.Aset.DM
  [Serializable]
  public class WebsetControl : BaseDataControlAsetDM, IDataControlUIEntry, IHasJSScript
  {
    #region Properties
    public string Valset { get; set; }
    public string Uraivalset { get; set; }
    public string Valdesc { get; set; }
    public int Modeentry { get; set; }
    public string Vallist { get; set; }
    public string Kdset { get; set; }
    #endregion Properties 

    #region Methods 
    public WebsetControl()
    {
      XMLName = ConstantTablesAsetDM.XMLWEBSET;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Kdset" };
      cViewListProperties.IDKey = "Kdset";
      cViewListProperties.IDProperty = "Kdset";
      cViewListProperties.ReadOnlyFields = new String[] { };
      cViewListProperties.SortFields = new String[] { "Kdset" };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_EDIT;
      cViewListProperties.PageSize = 20;
      return cViewListProperties;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Valdesc=Deskripsi"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Uraivalset=Nilai"), typeof(string), 20, HorizontalAlign.Center));

      return columns;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      if (typeof(IDataControlMenu).IsInstanceOfType(bo))
      {
        Last_by = ((BaseBO)bo).Userid;
      }
    }
    public new void SetPrimaryKey()
    {
    }
    public new HashTableofParameterRow GetFilters()
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
      HashTableofParameterRow hpars = new HashTableofParameterRow();

      return hpars;
    }
    public new IList View()
    {
      IList list = this.View(BaseDataControl.ALL);
      return list;
    }
    public new IList View(string label)
    {
      IList list = ((BaseDataControl)this).View(label);
      List<WebsetControl> ListData = new List<WebsetControl>();
      foreach (WebsetControl dc in list)
      {
        ListData.Add(dc);
      }

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
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Kdset=Kode"), true, 50).SetEnable(false));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Valdesc=Deskripsi"), true, 95).SetEnable(false));
      ArrayList list = new ArrayList(new ParamControl[] {
        new ParamControl() {  Kdpar="Y",Nmpar="Ya "}
        ,new ParamControl() { Kdpar="T",Nmpar="Tidak "}
      });
      hpars.Add(new ParameterRow(ConstantDict.GetColumnTitleEntry("Valset=Nilai"), ParameterRow.MODE_TYPE,
        list, "Kdpar=Nmpar", 50).SetAllowRefresh(false).SetEnable(enable).SetEditable(enable));
      hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Vallist=Keterangan"), true, 3).SetEnable(false).SetAllowEmpty(true));
      return hpars;
    }

    #endregion Methods 
  }
  #endregion Webset
}

