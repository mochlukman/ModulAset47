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
  #region Usadi.Valid49.BO.PindahtangandetControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class PindahtangandetControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript, IExtDataControl
  {
    #region Properties
    public long Id { get; set; }
    public string Unitkey { get; set; }
    public string Nopindahtangan { get; set; }
    public string Kdtans { get; set; }
    public string Nmtrans { get; set; }
    public DateTime Tglpindahtangan { get; set; }
    public DateTime Tglvalid { get; set; }
    public string Asetkey { get; set; }
    public string Kdaset { get; set; }
    public string Nmaset { get; set; }
    public string Noreg { get; set; }
    public string Idbrg { get; set; }
    public decimal Nilai { get; set; }
    public string Kdkon { get; set; }
    public string Kdkib { get; set; }
    public string Nmkib { get; set; }
    public string Nopenilaian { get; set; }
    public string Kdunit { get; set; }
    public string Nmunit { get; set; }
    public string Blokid { get; set; }
    #endregion Properties 

    #region Methods 
    public PindahtangandetControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLPINDAHTANGANDET;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Nopindahtangan", "Kdtans", "Asetkey", "Tahun", "Noreg" };
      cViewListProperties.IDKey = "Id";
      cViewListProperties.IDProperty = "Id";
      cViewListProperties.ReadOnlyFields = new String[] { };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_LOOKUP;
      cViewListProperties.LookupDC = " Usadi.Valid49.BO.ViewasetPindahtanganControl, Usadi.Valid49.Aset.MAT";
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
      bool enable = false;

      if(Kdtans == "201" || Kdtans == "204")
      {
        enable = true;
      }

      DataControlFieldCollection columns = new DataControlFieldCollection();

      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 40, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun=Tahun Perolehan"), typeof(string), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreg=No Register"), typeof(string), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai"), typeof(decimal), 20, HorizontalAlign.Right));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nopenilaian=Dokumen Penilaian"), typeof(string), 30, HorizontalAlign.Left).SetVisible(enable));
      return columns;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      if (typeof(IDataControlMenu).IsInstanceOfType(bo))
      {
        Last_by = ((BaseBO)bo).Userid;
      }
      else if (typeof(PindahtanganControl).IsInstanceOfType(bo))
      {
        Unitkey = ((PindahtanganControl)bo).Unitkey;
        Nopindahtangan = ((PindahtanganControl)bo).Nopindahtangan;
        Kdtans = ((PindahtanganControl)bo).Kdtans;
        Tglvalid = ((PindahtanganControl)bo).Tglvalid;
        Blokid = ((PindahtanganControl)bo).Blokid;
      }
      else if (typeof(ViewasetPindahtanganControl).IsInstanceOfType(bo))
      {
        Asetkey = ((ViewasetPindahtanganControl)bo).Asetkey;
        Tahun = ((ViewasetPindahtanganControl)bo).Tahun;
        Noreg = ((ViewasetPindahtanganControl)bo).Noreg;
        Idbrg = ((ViewasetPindahtanganControl)bo).Idbrg;
        Nilai = ((ViewasetPindahtanganControl)bo).Nilai;
        Kdkon = ((ViewasetPindahtanganControl)bo).Kdkon;
        Nopenilaian = ((ViewasetPindahtanganControl)bo).Nopenilaian;
      }
    }
    public new void Insert()
    {
      if (Kdkon == "")
      {
        Kdkon = null;
      }
      else
      {
        Kdkon = Kdkon;
      }
      base.Insert();
    }
    public new IList View()
    {
      IList list = this.View(BaseDataControl.ALL);
      return list;
    }
    public new IList View(string label)
    {
      IList list = ((BaseDataControl)this).View(label);
      List<PindahtangandetControl> ListData = new List<PindahtangandetControl>();
      foreach (PindahtangandetControl dc in list)
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
            PindahtangandetControl ctrl = (PindahtangandetControl)list[i];
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
  #endregion Pindahtangandet
}

