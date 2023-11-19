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
  #region Usadi.Valid49.BO.AtribusidetbrgControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class AtribusidetbrgControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript, IExtDataControl
  {
    #region Properties
    public long Id { get; set; }
    public string Asetkey { get; set; }
    public decimal Nilai { get; set; }
    public string Noreg { get; set; }
    public bool Generated { get; set; }
    public int Jumlah { get; set; }
    public decimal Nilaiba { get; set; }
    public string Kdaset { get; set; }
    public string Nmaset { get; set; }
    public string Kdnmaset { get; set; }
    public DateTime Tglatribusi { get; set; }
    public DateTime Tglvalid { get; set; }
    public string Blokid { get; set; }
    public string Unitkey { get; set; }
    public string Noatribusi { get; set; }
    public string Noba { get; set; }
    public string Mtgkey { get; set; }
    public string Idbrg { get; set; }
    public int Urutbrg { get; set; }
    public ImageCommand[] Cmds
    {
      get
      {
        ImageCommand cmd1 = new ImageCommand()
        {
          CommandName = "ViewBeritadetbrg",
          Icon = Icon.PageCopy
        };
        cmd1.ToolTip.Text = "Klik Untuk Menampilkan Spesifikasi Barang";
        return new ImageCommand[] { cmd1 };
      }
    }
    public string ViewBeritadetbrg
    {
      get
      {
        string app = GlobalAsp.GetRequestApp();
        string id = GlobalAsp.GetRequestId();
        string idprev = GlobalAsp.GetRequestId();
        string kode = GlobalAsp.GetRequestKode();
        string idx = GlobalAsp.GetRequestIndex();
        string strenable = "&enable=" + ((Status == 0) ? 1 : 0);
        string url = string.Format("PageTabular.aspx?passdc=1&app={0}&i=13&iprev=12&id={1}&idprev={2}&kode={3}&idx={4}" + strenable, app, id, idprev, kode, idx);
        return "Register Atribusi Barang; " + Kdaset + " - " + Nmaset + ":" + url;
      }
    }
    #endregion Properties 

    #region Methods 
    public AtribusidetbrgControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLATRIBUSIDETBRG;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Noatribusi", "Noba", "Mtgkey", "Asetkey", "Urutbrg" };
      cViewListProperties.IDKey = "Id";
      cViewListProperties.IDProperty = "Id";
      cViewListProperties.ReadOnlyFields = new String[] { };
      cViewListProperties.SortFields = new String[] { "Kdaset", "Jumlah", "Nilaiba" };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      cViewListProperties.PageSize = 20;
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
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Stricon"), typeof(CommandColumn), Cmds, 5, HorizontalAlign.Center));

      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Jumlah=Jumlah Barang"), typeof(string), 15, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilaiba=Nilai BAST"), typeof(decimal), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai=Nilai Atribusi"), typeof(decimal), 25, HorizontalAlign.Left).SetEditable(enable));

      return columns;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      if (typeof(AtribusidetControl).IsInstanceOfType(bo))
      {
        Unitkey = ((AtribusidetControl)bo).Unitkey;
        Noatribusi = ((AtribusidetControl)bo).Noatribusi;
        Noba = ((AtribusidetControl)bo).Noba;
        Mtgkey = ((AtribusidetControl)bo).Mtgkey;
        Tglatribusi = ((AtribusidetControl)bo).Tglatribusi;
        Tglvalid = ((AtribusidetControl)bo).Tglvalid;
        Blokid = ((AtribusidetControl)bo).Blokid;
      }
    }
    public new IList View()
    {
      IList list = this.View("Urutbrg");
      return list;
    }
    public new IList View(string label)
    {
      IList list = ((BaseDataControl)this).View(label);
      List<AtribusidetbrgControl> ListData = new List<AtribusidetbrgControl>();
      foreach (AtribusidetbrgControl dc in list)
      {
        ListData.Add(dc);
      }

      return ListData;
    }
    public override HashTableofParameterRow GetEntries()
    {
      bool enable = true;
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Kdnmaset=Kode Barang"), false, 95).SetEnable(enable));
      hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Nilai=Nilai Atribusi"), true, 35).SetEnable(enable).SetAllowEmpty(false));
      return hpars;
    }
    public new int Update()
    {
      int n = 0;
      base.Update();
      base.Update("Nilairek");
      return n;
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
            AtribusidetbrgControl ctrl = (AtribusidetbrgControl)list[i];
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
        DfTotal.Text = "Total Atribusi = " + total.ToString("#,##0");
      }
    }
    #endregion Methods 
  }
  #endregion Atribusidetbrg

  #region AtribusidetbrgRinci
  [Serializable]
  public class AtribusidetbrgRinciControl : AtribusidetbrgControl, IDataControlUIEntry, IHasJSScript
  {
    #region Methods 
    public new IList View()
    {
      IList list = this.View("Idbrg");
      return list;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      if (typeof(AtribusidetbrgControl).IsInstanceOfType(bo))
      {
        Unitkey = ((AtribusidetbrgControl)bo).Unitkey;
        Noatribusi = ((AtribusidetbrgControl)bo).Noatribusi;
        Noba = ((AtribusidetbrgControl)bo).Noba;
        Mtgkey = ((AtribusidetbrgControl)bo).Mtgkey;
        Urutbrg = ((AtribusidetbrgControl)bo).Urutbrg;
      }
    }

    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun"), typeof(int), 15, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreg=No Register"), typeof(string), 15, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai=Nilai Atribusi"), typeof(decimal), 25, HorizontalAlign.Left));

      return columns;
    }

    #endregion Methods 
  }
  #endregion AtribusidetbrgRinciControl

  #region AtribusidetbrgRenov
  [Serializable]
  public class AtribusidetbrgRenovControl : AtribusidetbrgControl, IDataControlUIEntry, IHasJSScript
  {
    #region Methods 
    public new IList View()
    {
      IList list = this.View("Renov");
      return list;
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

      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilaiba=Nilai BAST Pemeliharaan"), typeof(decimal), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai=Nilai Atribusi"), typeof(decimal), 25, HorizontalAlign.Left).SetEditable(enable));

      return columns;
    }
    #endregion Methods 
  }
  #endregion AtribusidetRenov
}

