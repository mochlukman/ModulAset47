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
  #region Usadi.Valid49.BO.KibmutasidetControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class KibmutasidetControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript, IExtDataControl
  {
    #region Properties
    public long Id { get; set; }
    public string Noreg2 { get; set; }
    public string Idbrg { get; set; }
    public decimal Nilai { get; set; }
    public string Statusmutasi { get; set; }
    public string Kdaset { get; set; }
    public string Nmaset { get; set; }
    public DateTime Tglmutasiter { get; set; }
    public string Kddana { get; set; }
    public string Kdtans { get; set; }
    public string Kdkib { get; set; }
    public string Nmkib { get; set; }
    public string Unitkey { get; set; }
    public string Unitkey2 { get; set; }
    public string Nomutasikel { get; set; }
    public string Asetkey { get; set; }
    public string Noreg { get; set; }
    public string Blokid { get; set; }
    #endregion Properties 

    #region Methods 
    public KibmutasidetControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLKIBMUTASIDET;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Unitkey2", "Nomutasikel", "Asetkey", "Tahun", "Noreg" };
      cViewListProperties.IDKey = "Id";
      cViewListProperties.IDProperty = "Id";
      cViewListProperties.ReadOnlyFields = new String[] {  };
      cViewListProperties.SortFields = new String[] { "Kdaset", "Tahun", "Noreg" };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_LOOKUP;
      cViewListProperties.LookupDC = " Usadi.Valid49.BO.ViewasetMutasiControl, Usadi.Valid49.Aset.MAT";
      cViewListProperties.LookupLabelQuery = "";
      cViewListProperties.PageSize = 30;
      cViewListProperties.RefreshParent = true;
      cViewListProperties.RefreshFilter = true;

      if (Tglmutasiter != new DateTime() || Blokid == "1")
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
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun=Tahun Perolehan"), typeof(string), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreg=No Register Awal"), typeof(string), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreg2=No Register Baru"), typeof(string), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai"), typeof(decimal), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Statusmutasi=Status"), typeof(string), 20, HorizontalAlign.Center));

      return columns;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      if (typeof(IDataControlMenu).IsInstanceOfType(bo))
      {
        Last_by = ((BaseBO)bo).Userid;
      }
      else if (typeof(KibmutasiControl).IsInstanceOfType(bo))
      {
        Unitkey = ((KibmutasiControl)bo).Unitkey;
        Unitkey2 = ((KibmutasiControl)bo).Unitkey2;
        Nomutasikel = ((KibmutasiControl)bo).Nomutasikel;
        Tglmutasiter = ((KibmutasiControl)bo).Tglmutasiter;
        Kdtans = ((KibmutasiControl)bo).Kdtans;
        Blokid = ((KibmutasiControl)bo).Blokid;
      }
      else if (typeof(ViewasetMutasiControl).IsInstanceOfType(bo))
      {
        Asetkey = ((ViewasetMutasiControl)bo).Asetkey;
        Tahun = ((ViewasetMutasiControl)bo).Tahun;
        Noreg = ((ViewasetMutasiControl)bo).Noreg;
        Idbrg = ((ViewasetMutasiControl)bo).Idbrg;
        Nilai = ((ViewasetMutasiControl)bo).Nilai;
      }
    }
    public new void SetPrimaryKey()
    {
      Noreg2 = "";
      Statusmutasi = "0";
    }
    public new IList View()
    {
      IList list = this.View(BaseDataControl.ALL);
      return list;
    }
    public new IList View(string label)
    {
      IList list = ((BaseDataControl)this).View(label);
      List<KibmutasidetControl> ListData = new List<KibmutasidetControl>();
      foreach (KibmutasidetControl dc in list)
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
            KibmutasidetControl ctrl = (KibmutasidetControl)list[i];
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
  #endregion Kibmutasidet

  #region KibmutasidetTerima
  public class KibmutasidetTerimaControl : KibmutasidetControl, IDataControlUIEntry, IHasJSScript
  {
    public KibmutasidetTerimaControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLKIBMUTASIDET;
    }
    public new IList View()
    {
      IList list = this.View(BaseDataControl.ALL);
      return list;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.ReadOnlyFields = new String[] { };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      cViewListProperties.PageSize = 30;
      cViewListProperties.AllowMultiDelete = false;
      return cViewListProperties;
    }
  }
  #endregion KibmutasidetTerima
}

