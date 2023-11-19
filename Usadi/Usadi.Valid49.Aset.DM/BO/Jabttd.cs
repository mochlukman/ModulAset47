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
  #region Usadi.Valid49.BO.JabttdControl, Usadi.Valid49.Aset.DM
  [Serializable]
  public class JabttdControl : BaseDataControlAsetDM, IDataControlUIEntry, IHasJSScript
  {
    #region Properties
    public long Id { get; set; }
    public string Unitkey { get; set; }
    public string Kdunit { get; set; }
    public string Nmunit { get; set; }
    public string Kddok { get; set; }
    public string Nmdok { get; set; }
    public string Nip { get; set; }
    public string Nama { get; set; }
    public string Jabatan { get; set; }
    public string Noskpttd { get; set; }
    public DateTime Tglskpttd { get; set; }
    public string Noskstopttd { get; set; }
    public DateTime Tglskstopttd { get; set; }
    public string Kdposttd { get; set; }
    public string Nmposttd { get; set; }
    public string Idxttd { get; set; }

    #endregion Properties 

    #region Methods 
    public JabttdControl()
    {
      XMLName = ConstantTablesAsetDM.XMLJABTTD;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Idxttd" };
      cViewListProperties.IDKey = "Idxttd";
      cViewListProperties.IDProperty = "Idxttd";
      cViewListProperties.ReadOnlyFields = new String[] { "Kdunit", "Nmunit", "Unitkey", "Nip", "Nama" };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_LOOKUP;
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_DEL;
      cViewListProperties.LookupDC = " Usadi.Valid49.BO.DaftdokLookupControl, Usadi.Valid49.Aset.DM";
      cViewListProperties.LookupLabelQuery = "Jabttd";
      cViewListProperties.AllowMultiDelete = true;
      return cViewListProperties;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kddok=Kode Dok"), typeof(string), 10, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmdok=Dokumen"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Jabatan"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmposttd=Posisi Layout"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noskpttd=No SK Pengangkatan"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglskpttd=Tgl SK Pengangkatan"), typeof(DateTime), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noskstopttd=No SK Pemberhentian"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglskstopttd=Tgl SK Pemberhentian"), typeof(DateTime), 30, HorizontalAlign.Left));

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
      else if (typeof(DaftdokControl).IsInstanceOfType(bo))
      {
        Kddok = ((DaftdokControl)bo).Kddok;
      }
    }
    public new void SetPrimaryKey()
    {
      Idxttd = Guid.NewGuid().ToString();
      UtilityUI.GetNoUrut(this, "Idxttd", 1, "Idxttd", string.Empty, string.Empty);
      Unitkey = Unitkey;
      Nip = Nip;
      Jabatan = null;
      Noskpttd = null;
      Tglskpttd = DateTime.Now;
      Noskstopttd = null;
      Tglskstopttd = DateTime.Now;
    }
    public new HashTableofParameterRow GetFilters()
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(DaftunitLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetAllowEmpty(false));
      hpars.Add(PegawaiLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enableFilter).SetAllowEmpty(false));
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
      List<JabttdControl> ListData = new List<JabttdControl>();
      foreach (JabttdControl dc in list)
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
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Jabatan"), true, 70).SetEnable(enable));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Noskpttd=No SK Pengangkatan"), true, 70).SetEnable(enable));
      hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglskpttd=Tgl SK Pengangkatan"), true).SetEnable(enable));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Noskstopttd=No SK Pemberhentian"), true, 70).SetEnable(enable));
      hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglskstopttd=Tgl SK Pemberhentian"), true).SetEnable(enable));
      ArrayList list = new ArrayList(new ParamControl[] {
        new ParamControl() {  Kdpar="01",Nmpar="Kiri Bawah "}
        ,new ParamControl() { Kdpar="02",Nmpar="Tengah Bawah "}
        ,new ParamControl() { Kdpar="03",Nmpar="Kanan Bawah "}
      });
      hpars.Add(new ParameterRow(ConstantDict.GetColumnTitleEntry("Kdposttd=Posisi Layout"), ParameterRow.MODE_SELECT,
        list, "Kdpar=Nmpar", 35).SetAllowRefresh(false).SetEnable(enable).SetEditable(enable));
      return hpars;
    }
    #endregion Methods 
  }
  #endregion Jabttd
}

