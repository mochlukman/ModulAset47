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
  #region Usadi.Valid49.BO.UsulanhapusdetControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class UsulanhapusdetControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript, IExtDataControl
  {
    #region Properties
    public long Id { get; set; }
    public string Kdunit { get; set; }
    public string Nmunit { get; set; }
    public string Asetkey { get; set; }
    public string Kdaset { get; set; }
    public string Nmaset { get; set; }
    public string Noreg { get; set; }
    public string Kdkon { get; set; }
    public string Nmkon { get; set; }
    public decimal Nilai { get; set; }
    public DateTime Tglvalid { get; set; }
    public string Unitkey { get; set; }
    public string Nousulan { get; set; }
    public string Idbrg { get; set; }
    public string Blokid { get; set; }
    #endregion Properties 

    #region Methods 
    public UsulanhapusdetControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLUSULANHAPUSDET;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Nousulan", "Idbrg" };
      cViewListProperties.IDKey = "Id";
      cViewListProperties.IDProperty = "Id";
      cViewListProperties.ReadOnlyFields = new String[] { };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_LOOKUP;
      cViewListProperties.LookupDC = " Usadi.Valid49.BO.ViewasetUsulanhapusControl, Usadi.Valid49.Aset.MAT";
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
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang Awal"), typeof(string), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang Awal"), typeof(string), 40, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun=Tahun Perolehan"), typeof(string), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreg=No Register Awal"), typeof(string), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai"), typeof(decimal), 20, HorizontalAlign.Right));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmkon=Kondisi Awal"), typeof(string), 20, HorizontalAlign.Center));

      return columns;
    }
    public new void SetPageKey()
    {
    }
    public new void SetFilterKey(BaseBO bo)
    {
      if (typeof(IDataControlMenu).IsInstanceOfType(bo))
      {
        Last_by = ((BaseBO)bo).Userid;
      }
      else if (typeof(UsulanhapusControl).IsInstanceOfType(bo))
      {
        Unitkey = ((UsulanhapusControl)bo).Unitkey;
        Nousulan = ((UsulanhapusControl)bo).Nousulan;
        Tglvalid = ((UsulanhapusControl)bo).Tglvalid;
        Blokid = ((UsulanhapusControl)bo).Blokid;
      }
      else if (typeof(ViewasetUsulanhapusControl).IsInstanceOfType(bo))
      {
        Asetkey = ((ViewasetUsulanhapusControl)bo).Asetkey;
        Tahun = ((ViewasetUsulanhapusControl)bo).Tahun;
        Noreg = ((ViewasetUsulanhapusControl)bo).Noreg;
        Idbrg = ((ViewasetUsulanhapusControl)bo).Idbrg;
        Kdkon = ((ViewasetUsulanhapusControl)bo).Kdkon;
        Nilai = ((ViewasetUsulanhapusControl)bo).Nilai;
      }
    }
    public new void SetPrimaryKey()
    {
    }
    public new IList View()
    {
      IList list = this.View(BaseDataControl.ALL);
      return list;
    }
    public new IList View(string label)
    {
      IList list = ((BaseDataControl)this).View(label);
      List<UsulanhapusdetControl> ListData = new List<UsulanhapusdetControl>();
      foreach (UsulanhapusdetControl dc in list)
      {
        ListData.Add(dc);
      }

      return ListData;
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
            UsulanhapusdetControl ctrl = (UsulanhapusdetControl)list[i];
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
  #endregion Usulanhapusdet
}

