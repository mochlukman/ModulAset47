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
  #region Usadi.Valid49.BO.DaftpenggunaControl, Usadi.Valid49.Aset.DM
  [Serializable]
  public class DaftpenggunaControl : BaseDataControlAsetDM, IDataControlUIEntry, IHasJSScript
  {
    #region Properties
    public long Id { get; set; }
    public string Nmpengguna { get; set; }
    public string Nminst { get; set; }
    public string Alamat { get; set; }
    public string Telepon { get; set; }
    public string Npwp { get; set; }
    public int Stvalid { get; set; }
    public string Kdpengguna { get; set; }
    #endregion Properties 

    #region Methods 
    public DaftpenggunaControl()
    {
      XMLName = ConstantTablesAsetDM.XMLDAFTPENGGUNA;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Kdpengguna" };
      cViewListProperties.IDKey = "Id";
      cViewListProperties.IDProperty = "Id";
      cViewListProperties.ReadOnlyFields = new String[] { };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_EDIT_DEL;
      cViewListProperties.PageSize = 20;
      cViewListProperties.AllowMultiDelete = true;
      return cViewListProperties;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center));

      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdpengguna=Kode"), typeof(string), 15, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmpengguna=Nama Pengguna"), typeof(string), 35, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nminst=Instansi"), typeof(string), 40, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Telepon"), typeof(string), 15, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Npwp=NPWP"), typeof(string), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Alamat"), typeof(string), 50, HorizontalAlign.Left));

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
      Kdpengguna = Guid.NewGuid().ToString();
      UtilityUI.GetNoUrut(this, "Kdpengguna", 10, "Kdpengguna", string.Empty, string.Empty);
    }
    public new IList View()
    {
      IList list = this.View(BaseDataControl.ALL);
      return list;
    }
    public new IList View(string label)
    {
      IList list = ((BaseDataControl)this).View(label);
      List<DaftpenggunaControl> ListData = new List<DaftpenggunaControl>();
      foreach (DaftpenggunaControl dc in list)
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
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Kdpengguna=Kode"), false, 20).SetEnable(false));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nmpengguna=Nama Pengguna"), false, 95).SetEnable(enable).SetEditable(enable));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nminst=Instansi"), false, 95).SetEnable(enable).SetEditable(enable));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Telepon"), false, 50).SetEnable(enable).SetEditable(enable));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Npwp=NPWP"), false, 50).SetEnable(enable).SetEditable(enable));
      hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Alamat"), true, 75).SetEnable(enable).SetEditable(enable));
      return hpars;
    }

    #endregion Methods 
  }
  #endregion Daftpengguna
}

