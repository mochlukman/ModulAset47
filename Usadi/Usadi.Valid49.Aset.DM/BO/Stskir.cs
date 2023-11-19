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
  #region Usadi.Valid49.BO.StskirControl, Usadi.Valid49.Aset.DM
  [Serializable]
  public class StskirControl : BaseDataControlAsetDM, IDataControlUIEntry, IHasJSScript
  {
    #region Properties
    public long Id { get; set; }
    public string Nmbapkir { get; set; }
    public string Jnstranskir { get; set; }
    public string Kdbapkir { get; set; }
    #endregion Properties 

    #region Methods 
    public StskirControl()
    {
      XMLName = ConstantTablesAsetDM.XMLSTSKIR;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Kdbapkir" };
      cViewListProperties.IDKey = "Kdbapkir";
      cViewListProperties.IDProperty = "Kdbapkir";
      cViewListProperties.ReadOnlyFields = new String[] { };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_DEL;
      cViewListProperties.AllowMultiDelete = true;
      return cViewListProperties;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdbapkir=Kode"), typeof(string), 15, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmbapkir=Uraian"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Jnstranskir=Jenis"), typeof(string), 30, HorizontalAlign.Left));

      return columns;
    }
    public new void SetPageKey()
    {
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
      Kdbapkir = Guid.NewGuid().ToString();
      UtilityUI.GetNoUrut(this, "Kdbapkir", 2, "Kdbapkir", string.Empty, string.Empty);
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
      List<StskirControl> ListData = new List<StskirControl>();
      foreach (StskirControl dc in list)
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
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Kdbapkir=Kode"), false, 10).SetEnable(false));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nmbapkir=Uraian"), true, 70).SetEnable(enable));
      ArrayList list = new ArrayList(new ParamControl[] {
        new ParamControl() {  Kdpar="1",Nmpar="Masuk"}
        ,new ParamControl() { Kdpar="0",Nmpar="Keluar"}
      });
      hpars.Add(new ParameterRow(ConstantDict.GetColumnTitleEntry("Jnstranskir=Jenis"), ParameterRow.MODE_SELECT,
        list, "Kdpar=Nmpar", 30).SetAllowRefresh(false).SetEnable(enable).SetEditable(false));

      return hpars;
    }

    #endregion Methods 
  }
  #endregion Stskir
}

