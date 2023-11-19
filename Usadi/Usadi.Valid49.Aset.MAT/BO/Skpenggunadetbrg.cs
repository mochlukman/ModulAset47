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
  #region Usadi.Valid49.BO.SkpenggunadetbrgControl, Usadi.Valid49.Aset.DM
  [Serializable]
  public class SkpenggunadetbrgControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript, IExtDataControl
  {
    #region Properties
    public long Id { get; set; }
    public string Noba { get; set; }
    public string Asetkey { get; set; }
    public string Kdaset { get; set; }
    public string Nmaset { get; set; }
    public string Noreg { get; set; }
    public Decimal Nilai { get; set; }
    public string Kdpemilik { get; set; }
    public string Nmpemilik { get; set; }
    public string Asalusul { get; set; }
    public string Kdsatuan { get; set; }
    public string Nmsatuan { get; set; }
    public string Spesifikasi { get; set; }
    public string Ukuran { get; set; }
    public string Bahan { get; set; }
    public string Nosertifikat { get; set; }
    public string Alamat { get; set; }
    public new string Ket { get; set; }
    public string Kdkib { get; set; }
    public DateTime Tglvalid { get; set; }
    public string Unitkey { get; set; }
    public string Noskpengguna { get; set; }
    public string Nodokumen { get; set; }
    public string Unitkey2 { get; set; }
    public string Idbrg { get; set; }
    public string Blokid { get; set; }
    #endregion Properties 

    #region Methods 
    public SkpenggunadetbrgControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLSKPENGGUNADETBRG;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Noskpengguna", "Nodokumen", "Unitkey2", "Idbrg" };
      cViewListProperties.IDKey = "Id";
      cViewListProperties.IDProperty = "Id";
      cViewListProperties.ReadOnlyFields = new String[] { };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_LOOKUP;
      cViewListProperties.LookupDC = " Usadi.Valid49.BO.ViewasetSkpenggunaControl, Usadi.Valid49.Aset.MAT";
      cViewListProperties.LookupLabelQuery = "";
      cViewListProperties.PageSize = 20;

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
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 45, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noba=Nomor BAST"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun"), typeof(int), 15, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreg=No Register"), typeof(string), 15, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai=Nilai Perolehan"), typeof(decimal), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Asalusul=Asal Usul"), typeof(string), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmpemilik=Pemilik"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmsatuan=Satuan"), typeof(string), 15, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Spesifikasi"), typeof(string), 30, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nosertifikat=No Sertifikat"), typeof(string), 35, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ukuran"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Bahan"), typeof(string), 15, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Alamat"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 50, HorizontalAlign.Left));

      return columns;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      if (typeof(IDataControlMenu).IsInstanceOfType(bo))
      {
        Last_by = ((BaseBO)bo).Userid;
      }
      else if (typeof(SkpenggunadetControl).IsInstanceOfType(bo))
      {
        Unitkey = ((SkpenggunadetControl)bo).Unitkey;
        Noskpengguna = ((SkpenggunadetControl)bo).Noskpengguna;
        Nodokumen = ((SkpenggunadetControl)bo).Nodokumen;
        Unitkey2 = ((SkpenggunadetControl)bo).Unitkey2;
        Tglvalid = ((SkpenggunadetControl)bo).Tglvalid;
        Blokid = ((SkpenggunadetControl)bo).Blokid;
      }
      else if (typeof(ViewasetSkpenggunaControl).IsInstanceOfType(bo))
      {
        Idbrg = ((ViewasetSkpenggunaControl)bo).Idbrg;
        Noba = ((ViewasetSkpenggunaControl)bo).Dokperolehan;
        Asetkey = ((ViewasetSkpenggunaControl)bo).Asetkey;
        Tahun = ((ViewasetSkpenggunaControl)bo).Tahun;
        Nilai = ((ViewasetSkpenggunaControl)bo).Nilai;
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
      List<SkpenggunadetbrgControl> ListData = new List<SkpenggunadetbrgControl>();
      foreach (SkpenggunadetbrgControl dc in list)
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
            SkpenggunadetbrgControl ctrl = (SkpenggunadetbrgControl)list[i];
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
  #endregion Skpenggunadetbrg

  #region SkpenggunadetbrgValidControl
  [Serializable]
  public class SkpenggunadetbrgValidControl : SkpenggunadetbrgControl, IDataControlUIEntry, IHasJSScript
  {
    #region Methods 
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      cViewListProperties.AllowMultiDelete = false;
      return cViewListProperties;
    }
    #endregion Methods 
  }
  #endregion SkpenggunadetValidControl
}

