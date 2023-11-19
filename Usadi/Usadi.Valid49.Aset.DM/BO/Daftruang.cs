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
  #region Usadi.Valid49.BO.DaftruangControl, Usadi.Valid49.Aset.DM
  [Serializable]
  public class DaftruangControl : BaseDataControlAsetDM, IDataControlUIEntry, IHasJSScript
  {
    #region Properties
    public long Id { get; set; }
    public string Unitkey { get; set; }
    public string Kdunit { get; set; }
    public string Nmunit { get; set; }
    public string Kdruang { get; set; }
    public string Nmruang { get; set; }
    public string Keterangan { get; set; }
    public string Jnsruang { get; set; }
    public string Nmlevel { get; set; }
    public string Ruangkey { get; set; }
    public string Nip { get; set; }
    public string Nama { get; set; }
    #endregion Properties 

    #region Methods 
    public DaftruangControl()
    {
      XMLName = ConstantTablesAsetDM.XMLDAFTRUANG;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Ruangkey" };
      cViewListProperties.IDKey = "Ruangkey";
      cViewListProperties.IDProperty = "Ruangkey";
      cViewListProperties.ReadOnlyFields = new String[] { "Unitkey" };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_DEL;
      cViewListProperties.PageSize = 30;
      cViewListProperties.AllowMultiDelete = true;
      return cViewListProperties;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdruang=Kode"), typeof(string), 15, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmruang=Uraian"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nama=Penanggung Jawab"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Keterangan"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmlevel=Level"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Type=Tipe"), typeof(string), 10, HorizontalAlign.Center));

      return columns;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      if (typeof(IDataControlMenu).IsInstanceOfType(bo))
      {
        Unitkey = (string)GlobalAsp.GetSessionUser().GetValue("Unitkey");
        Kdunit = (string)GlobalAsp.GetSessionUser().GetValue("Kdunit");
        Nmunit = (string)GlobalAsp.GetSessionUser().GetValue("Nmunit");
      }
    }
    public new void SetPrimaryKey()
    {
      Kdlevel = 3;
      Type = "D";
    }
    public new HashTableofParameterRow GetFilters()
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(DaftunitLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetAllowEmpty(false));

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
      List<DaftruangControl> ListData = new List<DaftruangControl>();
      foreach (DaftruangControl dc in list)
      {
        ListData.Add(dc);
      }

      return ListData;
    }
    public new void Insert()
    {
      DaftruangControl cDaftruangGetruangkey = new DaftruangControl();
      cDaftruangGetruangkey.Load("Ruangkey");
      Ruangkey = cDaftruangGetruangkey.Ruangkey;

      base.Insert();
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
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Kdruang=Kode"), true, 30).SetEnable(enable));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nmruang=Nama"), true, 94).SetEnable(enable));
      hpars.Add(PegawaiLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(true).SetAllowEmpty(false));
      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdlevel=Level"),
      GetList(new StruruangLookupControl()), "Level=Nmlevel", 30).SetAllowRefresh(false).SetEnable(enable));
      hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Keterangan"), true, 2).SetEnable(enable));
      hpars.Add(new ParameterRowType(this, true));
      
      return hpars;
    }
    #endregion Methods 
  }
  #endregion Daftruang
}

