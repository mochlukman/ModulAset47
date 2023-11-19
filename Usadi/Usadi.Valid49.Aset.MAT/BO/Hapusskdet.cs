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
  #region Usadi.Valid49.BO.HapusskdetControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class HapusskdetControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript, IExtDataControl
  {
    #region Properties
    public long Id { get; set; }
    public string Nmtrans { get; set; }
    public string Kdaset { get; set; }
    public string Nmaset { get; set; }
    public string Idbrg { get; set; }
    public decimal Nilai { get; set; }
    public string Nopindahtangan { get; set; }
    public string Kdunit { get; set; }
    public string Nmunit { get; set; }
    public DateTime Tglvalid { get; set; }
    public DateTime Tglpindahtangan { get; set; }
    public string Noreg { get; set; }
    public string Asetkey { get; set; }
    public string Kdtans { get; set; }
    public string Noskhapus { get; set; }
    public string Unitkey { get; set; }
    public string Blokid { get; set; }
    #endregion Properties 

    #region Methods 
    public HapusskdetControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLHAPUSSKDET;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Nohapussk", "Kdtans", "Asetkey", "Tahun", "Noreg" };
      cViewListProperties.IDKey = "Id";
      cViewListProperties.IDProperty = "Id";
      cViewListProperties.ReadOnlyFields = new String[] { };
      cViewListProperties.SortFields = new String[] { "Kdaset", "Nmaset", "Tahun", "Noreg" };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_LOOKUP;
      cViewListProperties.LookupDC = " Usadi.Valid49.BO.ViewasetHapusskControl, Usadi.Valid49.Aset.MAT";
      cViewListProperties.LookupLabelQuery = "";
      cViewListProperties.PageSize = 20;
      cViewListProperties.RefreshFilter = true;

      if (Tglvalid != new DateTime() || Blokid == "1")
      {
        cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      }
      else
      {
        cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_DEL;
        cViewListProperties.AllowMultiDelete = true;
      }
      return cViewListProperties;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();

      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 40, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmtrans=Jenis Penghapusan"), typeof(string), 40, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun"), typeof(string), 15, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreg=No Register"), typeof(string), 15, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai"), typeof(decimal), 20, HorizontalAlign.Right));     
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nopindahtangan=Dokumen Pindahtangan"), typeof(string), 30, HorizontalAlign.Left));
      return columns;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      if (typeof(IDataControlMenu).IsInstanceOfType(bo))
      {
        Last_by = ((BaseBO)bo).Userid;
      }
      else if (typeof(HapusskControl).IsInstanceOfType(bo))
      {
        Unitkey = ((HapusskControl)bo).Unitkey;
        Noskhapus = ((HapusskControl)bo).Noskhapus;
        Tglvalid = ((HapusskControl)bo).Tglvalid;
        Blokid = ((HapusskControl)bo).Blokid;
      }
      else if (typeof(ViewasetHapusskControl).IsInstanceOfType(bo))
      {
        Asetkey = ((ViewasetHapusskControl)bo).Asetkey;
        Kdtans = ((ViewasetHapusskControl)bo).Kdtans;
        Tahun = ((ViewasetHapusskControl)bo).Tahun;
        Noreg = ((ViewasetHapusskControl)bo).Noreg;
        Idbrg = ((ViewasetHapusskControl)bo).Idbrg;
        Nilai = ((ViewasetHapusskControl)bo).Nilai;
        Nopindahtangan = ((ViewasetHapusskControl)bo).Nopindahtangan;
      }
      else if (typeof(ViewasetHapussksebagianControl).IsInstanceOfType(bo))
      {
        Asetkey = ((ViewasetHapussksebagianControl)bo).Asetkey;
        Kdtans = ((ViewasetHapussksebagianControl)bo).Kdtans;
        Tahun = ((ViewasetHapussksebagianControl)bo).Tahun;
        Noreg = ((ViewasetHapussksebagianControl)bo).Noreg;
        Idbrg = ((ViewasetHapussksebagianControl)bo).Idbrg;
        Nilai = ((ViewasetHapussksebagianControl)bo).Nilai;
        Nopindahtangan = ((ViewasetHapussksebagianControl)bo).Nopenilaian;
      }
    }
    public new IList View()
    {
      IList list = this.View(BaseDataControl.ALL);
      return list;
    }
    public new IList View(string label)
    {
      IList list = ((BaseDataControl)this).View(label);
      List<HapusskdetControl> ListData = new List<HapusskdetControl>();
      foreach (HapusskdetControl dc in list)
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
      //bool enable = true;
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      return hpars;
    }
    public Icon GetIcon()
    {
      return Icon.Table;
    }
    public string GetIconText()
    {
      return string.Empty;
    }
    public void SetTotalFields(Toolbar tbbtm)
    {
      if (true)
      {
        tbbtm.Add(new ToolbarFill());
        //tbbtm.Add(new DisplayField() { ID = "DfSubTotal", Text = "0" });
        tbbtm.Add(new ToolbarSeparator());
        tbbtm.Add(new DisplayField() { ID = "DfTotal", Text = "0" });
      }
    }
    public void SetTotal(Control seed)
    {
      if (true)
      {
        PagingToolbar toolbar = ControlUtils.FindControl<PagingToolbar>(seed, "TopBar1");
        int idx = 0;
        int pagesize = 0;
        if (toolbar != null)
        {
          idx = toolbar.PageIndex;
          pagesize = toolbar.PageSize;
        }

        int id = GlobalAsp.GetRequestI();
        IList list = GlobalAsp.GetSessionListRows();

        decimal subtotal = 0;
        decimal total = 0;
        if (list != null && list.Count > 0)
        {
          int start = (idx * pagesize);
          int finish = ((idx + 1) * pagesize);
          for (int i = 0; i < list.Count; i++)
          {
            HapusskdetControl ctrl = (HapusskdetControl)list[i];
            if ((i >= start) && (i <= finish))
            {
              subtotal += ctrl.Nilai;
            }
            total += ctrl.Nilai;
          }
        }
        //DisplayField DfSubTotal = ControlUtils.FindControl<DisplayField>(seed, "DfSubTotal");
        DisplayField DfTotal = ControlUtils.FindControl<DisplayField>(seed, "DfTotal");
        //DfSubTotal.Text = "Subtotal = " + subtotal.ToString("#,##0");
        DfTotal.Text = "Total = " + total.ToString("#,##0");
      }
    }
    #endregion Methods 
  }
  #endregion Hapusskdet

  #region HapusskdetSebagian
  [Serializable]
  public class HapusskdetSebagianControl : HapusskdetControl, IDataControlUIEntry, IHasJSScript
  {
    #region Methods 
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Nohapussk", "Kdtans", "Asetkey", "Tahun", "Noreg" };
      cViewListProperties.IDKey = "Id";
      cViewListProperties.IDProperty = "Id";
      cViewListProperties.ReadOnlyFields = new String[] { };
      cViewListProperties.SortFields = new String[] { "Kdaset", "Nmaset", "Tahun", "Noreg" };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_LOOKUP;
      cViewListProperties.LookupDC = " Usadi.Valid49.BO.ViewasetHapussksebagianControl, Usadi.Valid49.Aset.MAT";
      cViewListProperties.LookupLabelQuery = "";
      cViewListProperties.PageSize = 20;
      cViewListProperties.RefreshFilter = true;

      if (Tglvalid != new DateTime() || Blokid == "1")
      {
        cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      }
      else
      {
        cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_DEL;
        cViewListProperties.AllowMultiDelete = true;
      }
      return cViewListProperties;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();

      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 40, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun"), typeof(string), 15, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreg=No Register"), typeof(string), 15, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai"), typeof(decimal), 20, HorizontalAlign.Right));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nopindahtangan=Dokumen Penilaian"), typeof(string), 30, HorizontalAlign.Left));
      return columns;
    }
    #endregion Methods 
  }
  #endregion HapusskdetSebagian
}

