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
  #region Usadi.Valid49.BO.PegawaiControl, Usadi.Valid49.Aset.DM
  [Serializable]
  public class PegawaiControl : BaseDataControlAsetDM, IDataControlUIEntry, IHasJSScript
  {
    #region Properties
    public long Id { get; set; }
    public string Unitkey { get; set; }
    public string Kdunit { get; set; }
    public string Nmunit { get; set; }
    public string Kdgol { get; set; }
    public string Nmgol { get; set; }
    public string Pangkat { get; set; }
    public string Nama { get; set; }
    public string Alamat { get; set; }
    public string Jabatan { get; set; }
    public string Pddk { get; set; }
    public string Npwp { get; set; }
    public string Nip { get; set; }
    public string Kdjbt { get; set; }
    public string Nmjbt { get; set; }
    #endregion Properties 

    #region Methods 
    public PegawaiControl()
    {
      XMLName = ConstantTablesAsetDM.XMLPEGAWAI;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Nip" };
      cViewListProperties.IDKey = "Nip";
      cViewListProperties.IDProperty = "Nip";
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
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nip=NIP"), typeof(string), 35, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nama"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Pangkat"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmjbt=Jabatan"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Pddk=Pendidikan"), typeof(string), 30, HorizontalAlign.Left));

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
      Unitkey = Unitkey;
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
      List<PegawaiControl> ListData = new List<PegawaiControl>();
      foreach (PegawaiControl dc in list)
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
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nip"), false, 70).SetEnable(enable));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nama"), true, 70).SetEnable(enable));
      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdgol=Golongan"),
      GetList(new GolonganLookupControl()), "Kdgol=Pangkat", 50).SetAllowRefresh(false).SetEnable(enable));
      
      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdjbt=Jabatan"),
      GetList(new DaftjabatanLookupControl()), "Kdjbt=Nmjbt", 50).SetAllowRefresh(false).SetEnable(enable));

      //hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Jabatan"), true, 70).SetEnable(enable));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Pddk=Pendidikan"), true, 70).SetEnable(enable));
      hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Alamat"), true, 3).SetEnable(enable));


      return hpars;
    }

    #endregion Methods 
  }
  #endregion Pegawai
}

