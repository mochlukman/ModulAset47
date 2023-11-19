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
  #region Usadi.Valid49.BO.Daftphk3Control, Usadi.Valid49.Aset.DM
  [Serializable]
  public class Daftphk3Control : BaseDataControlAsetDM, IDataControlUIEntry, IHasJSScript
  {
    #region Properties
    public long Id { get; set; }
    public string Nmp3 { get; set; }
    public string Nminst { get; set; }
    public string Idbank { get; set; }
    public string Nmbank { get; set; }
    public string Cabangbank { get; set; }
    public string Alamatbank { get; set; }
    public string Norekbank { get; set; }
    public string Kdjenis { get; set; }
    public string Jenisbadanusaha { get; set; }
    public string Alamat { get; set; }
    public string Telepon { get; set; }
    public string Npwp { get; set; }
    public string Unitkey { get; set; }
    public string Kdunit { get; set; }
    public string Nmunit { get; set; }
    public int Stvalid { get; set; }
    public string Kdp3 { get; set; }
    public int Jmlkontrak { get; set; }
    public int Jmlkemitraan { get; set; }
    public string Entryphk3 { get; set; }
    #endregion Properties 

    #region Methods 
    public Daftphk3Control()
    {
      XMLName = ConstantTablesAsetDM.XMLDAFTPHK3;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Kdp3" };
      cViewListProperties.IDKey = "Kdp3";
      cViewListProperties.IDProperty = "Kdp3";
      cViewListProperties.ReadOnlyFields = new String[] { "Unitkey", "Kdunit", "Nmunit" };
      cViewListProperties.SortFields = new String[] { "Kdp3" };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
      cViewListProperties.PageSize = 30;

      WebsetControl cWebset = new WebsetControl();
      cWebset.Kdset = "phk3kontrak";
      cWebset.Load("PK");
      Entryphk3 = cWebset.Valset.ToUpper();

      if (Entryphk3 == "Y")
      {
        cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_EDIT_DEL;
        cViewListProperties.AllowMultiDelete = true;
      }
      else
      {
        cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      }

      return cViewListProperties;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdp3=Kode"), typeof(string), 15, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nminst=Nama Instansi"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmp3=Pimpinan"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Alamat"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmbank=Bank"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Cabangbank=Cabang"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Norekbank=Nomor Rekening"), typeof(string), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Npwp=NPWP"), typeof(string), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Telepon"), typeof(string), 30, HorizontalAlign.Left));

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
      Kdp3 = Guid.NewGuid().ToString();
      UtilityUI.GetNoUrut(this, "Kdp3", 10, "Kdp3", string.Empty, string.Empty);
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
      List<Daftphk3Control> ListData = new List<Daftphk3Control>();
      foreach (Daftphk3Control dc in list)
      {
        ListData.Add(dc);
      }

      return ListData;
    }
    public new int Delete()
    {
      Daftphk3Control cDaftphk3kontrak = new Daftphk3Control();
      cDaftphk3kontrak.Kdp3 = Kdp3;
      cDaftphk3kontrak.Load("Cekkontrak");

      Daftphk3Control cDaftphk3kemitraan = new Daftphk3Control();
      cDaftphk3kemitraan.Kdp3 = Kdp3;
      cDaftphk3kemitraan.Load("Cekkemitraan");

      if (cDaftphk3kontrak.Jmlkontrak != 0 || cDaftphk3kemitraan.Jmlkemitraan != 0)
      {
        throw new Exception("Gagal menghapus data : Pihak ketiga sudah digunakan");
      }

      Status = -1;
      int n = ((BaseDataControlUI)this).Delete(BaseDataControl.DEFAULT);
      return n;
    }
    public override HashTableofParameterRow GetEntries()
    {
      bool enable = true;
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Kdp3=Kode"), false, 20).SetEnable(false));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nmp3=Pimpinan"), false, 95).SetEnable(enable).SetEditable(enable));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nminst=Instansi"), false, 95).SetEnable(enable).SetEditable(enable));
      hpars.Add(JbankLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetAllowEmpty(false).SetEditable(enable));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Norekbank=No Rekening"), false, 50).SetEnable(enable).SetEditable(enable));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Npwp=NPWP"), false, 50).SetEnable(enable).SetEditable(enable));
      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdjenis=Jenis Usaha"),
        GetList(new JusahaLookupControl()), "Kdjenis=Jenisbadanusaha", 50).SetEnable(enable).SetAllowEmpty(false).SetEditable(enable));      
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Telepon"), false, 50).SetEnable(enable).SetEditable(enable));
      hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Alamat"), true, 75).SetEnable(enable).SetEditable(enable));

      return hpars;
    }
    #endregion Methods
  }
  #endregion Daftphk3
}

