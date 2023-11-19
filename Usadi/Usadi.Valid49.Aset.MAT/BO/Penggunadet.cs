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
  #region Usadi.Valid49.BO.PenggunadetControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class PenggunadetControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
  {
    #region Properties
    public long Id { get; set; }
    public decimal Jangkawaktu { get; set; }
    public string Kdsatwaktu { get; set; }
    public string Idbrg { get; set; }
    public string Statuspengguna { get; set; }
    public string Statuspenggunaan { get; set; }
    public string Kdaset { get; set; }
    public string Nmaset { get; set; }
    public string Kdnmaset { get; set; }
    public string Nmsatwaktu { get; set; }
    public DateTime Tglselesai { get; set; }
    public DateTime Tglvalid { get; set; }
    public string Kdkib { get; set; }
    public string Nmkib { get; set; }
    public string Unitkey { get; set; }
    public string Nodokpengguna { get; set; }
    public string Kdtans { get; set; }
    public string Asetkey { get; set; }
    public string Noreg { get; set; }
    public string Blokid { get; set; }
    #endregion Properties 

    #region Methods 
    public PenggunadetControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLPENGGUNADET;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Nodokpengguna", "Kdtans", "Asetkey", "Tahun", "Noreg" };
      cViewListProperties.IDKey = "Id";
      cViewListProperties.IDProperty = "Id";
      cViewListProperties.ReadOnlyFields = new String[] { };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_LOOKUP;
      cViewListProperties.LookupDC = " Usadi.Valid49.BO.ViewasetPenggunaControl, Usadi.Valid49.Aset.MAT";
      cViewListProperties.LookupLabelQuery = "";
      cViewListProperties.PageSize = 30;

      if (Tglvalid != new DateTime() || Blokid == "1")
      {
        cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      }
      else
      {
        cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_EDIT_DEL;
        cViewListProperties.AllowMultiDelete = true;
      }
      return cViewListProperties;
    }
    public override DataControlFieldCollection GetColumns()
    {
      bool enable = true;

      if (Tglvalid != new DateTime() || Blokid == "1")
      {
        enable = false;
      }

      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center).SetVisible(enable));

      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun=Tahun Perolehan"), typeof(int), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreg=No Register"), typeof(string), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmsatwaktu=Satuan Waktu"), typeof(string), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Jangkawaktu=Jangka Waktu"), typeof(decimal), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Statuspenggunaan=Status"), typeof(string), 20, HorizontalAlign.Center));

      return columns;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      if (typeof(IDataControlMenu).IsInstanceOfType(bo))
      {
        Last_by = ((BaseBO)bo).Userid;
      }
      else if (typeof(PenggunaControl).IsInstanceOfType(bo))
      {
        Unitkey = ((PenggunaControl)bo).Unitkey;
        Nodokpengguna = ((PenggunaControl)bo).Nodokpengguna;
        Kdtans = ((PenggunaControl)bo).Kdtans;
        Tglselesai = ((PenggunaControl)bo).Tglselesai;
        Tglvalid = ((PenggunaControl)bo).Tglvalid;
        Blokid = ((PenggunaControl)bo).Blokid;
      }
      else if (typeof(ViewasetPenggunaControl).IsInstanceOfType(bo))
      {
        Asetkey = ((ViewasetPenggunaControl)bo).Asetkey;
        Tahun = ((ViewasetPenggunaControl)bo).Tahun;
        Noreg = ((ViewasetPenggunaControl)bo).Noreg;
        Idbrg = ((ViewasetPenggunaControl)bo).Idbrg;
      }
    }
    public new void SetPrimaryKey()
    {
      Statuspengguna = "1";
    }
    public new IList View()
    {
      IList list = this.View(BaseDataControl.ALL);
      return list;
    }
    public new IList View(string label)
    {
      IList list = ((BaseDataControl)this).View(label);
      List<PenggunadetControl> ListData = new List<PenggunadetControl>();
      foreach (PenggunadetControl dc in list)
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
      bool enable = true; //enableTgl = !Valid; enableSelesai = false;

      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Kdnmaset=Kode Barang"), false, 95).SetEnable(false));
      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdsatwaktu=Satuan Waktu"),
      GetList(new SatwaktuLookupControl()), "Kdsatwaktu=Nmsatwaktu", 30).SetAllowRefresh(false).SetEnable(enable).SetAllowEmpty(false));
      hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Jangkawaktu=Jangka Waktu"), true, 30).SetEnable(enable).SetEditable(true));
      ArrayList list = new ArrayList(new ParamControl[] {
        new ParamControl() {  Kdpar="0",Nmpar="Sebagian "}
        ,new ParamControl() { Kdpar="1",Nmpar="Semua "}
      });
      hpars.Add(new ParameterRow(ConstantDict.GetColumnTitleEntry("Statuspengguna=Status"), ParameterRow.MODE_SELECT,
        list, "Kdpar=Nmpar", 30).SetAllowRefresh(false).SetEnable(enable).SetEditable(true));
      return hpars;
    }

    #endregion Methods 
  }
  #endregion Penggunadet
}

