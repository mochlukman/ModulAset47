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
  #region Usadi.Valid49.BO.AtribusidetControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class AtribusidetControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript, IExtDataControl
  {
    #region Properties
    public long Id { get; set; }
    public decimal Nilai { get; set; }
    public string Kdunit { get; set; }
    public string Nmunit { get; set; }
    public string Kdper { get; set; }
    public string Nmper { get; set; }
    public DateTime Tglatribusi { get; set; }
    public DateTime Tglvalid { get; set; }
    public string Blokid { get; set; }
    public string Unitkey { get; set; }
    public string Noatribusi { get; set; }
    public string Noba { get; set; }
    public string Mtgkey { get; set; }
    public ImageCommand[] Cmds
    {
      get
      {
        ImageCommand cmd1 = new ImageCommand()
        {
          CommandName = "ViewAtribusidetbrg",
          Icon = Icon.PageCopy
        };
        cmd1.ToolTip.Text = "Klik Untuk Menampilkan Kode Barang";
        return new ImageCommand[] { cmd1 };
      }
    }
    public string ViewAtribusidetbrg
    {
      get
      {
        string app = GlobalAsp.GetRequestApp();
        string id = GlobalAsp.GetRequestId();
        string idprev = GlobalAsp.GetRequestId();
        string kode = GlobalAsp.GetRequestKode();
        string idx = GlobalAsp.GetRequestIndex();
        string strenable = "&enable=" + ((Status == 0) ? 1 : 0);
        string url = string.Format("PageTabular.aspx?passdc=1&app={0}&i=12&iprev=11&id={1}&idprev={2}&kode={3}&idx={4}" + strenable, app, id, idprev, kode, idx);
        return "Rincian Kode Barang - Rekening " + Nmper + ":" + url;
      }
    }
    #endregion Properties 

    #region Methods 
    public AtribusidetControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLATRIBUSIDET;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Noatribusi", "Noba", "Mtgkey" };
      cViewListProperties.IDKey = "Id";
      cViewListProperties.IDProperty = "Id";
      cViewListProperties.ReadOnlyFields = new String[] {  };
      cViewListProperties.SortFields = new String[] { "Kdper" };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_LOOKUP;
      cViewListProperties.LookupDC = " Usadi.Valid49.BO.MatangrLookupControl, Usadi.Valid49.Aset.DM";
      cViewListProperties.LookupLabelQuery = "Atribusi";
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
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Stricon"), typeof(CommandColumn), Cmds, 5, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdper=Kode Rekening"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmper=Rekening"), typeof(string), 60, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai"), typeof(decimal), 30, HorizontalAlign.Left));

      return columns;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      if (typeof(AtribusiControl).IsInstanceOfType(bo))
      {
        Unitkey = ((AtribusiControl)bo).Unitkey;
        Noatribusi = ((AtribusiControl)bo).Noatribusi;
        Noba = ((AtribusiControl)bo).Noba;
        Tglatribusi = ((AtribusiControl)bo).Tglatribusi;
        Tglvalid = ((AtribusiControl)bo).Tglvalid;
        Blokid = ((AtribusiControl)bo).Blokid;
      }
      else if (typeof(MatangrControl).IsInstanceOfType(bo))
      {
        Mtgkey = ((MatangrControl)bo).Mtgkey;
      }
    }
    public new void SetPrimaryKey()
    {
      Nilai = 0;
    }
    public new IList View()
    {
      IList list = this.View(BaseDataControl.ALL);
      return list;
    }
    public new IList View(string label)
    {
      IList list = ((BaseDataControl)this).View(label);
      List<AtribusidetControl> ListData = new List<AtribusidetControl>();
      foreach (AtribusidetControl dc in list)
      {
        ListData.Add(dc);
      }
      return ListData;
    }
    public override HashTableofParameterRow GetEntries()
    {
      //bool enable = true;
      HashTableofParameterRow hpars = new HashTableofParameterRow();

      return hpars;
    }
    public new int Delete()
    {
      int n = 0;
      Status = -1;
      base.Delete("Detil");
      base.Delete();
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
            AtribusidetControl ctrl = (AtribusidetControl)list[i];
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
  #endregion Atribusidet

  #region AtribusidetRenov
  [Serializable]
  public class AtribusidetRenovControl : AtribusidetControl, IDataControlUIEntry, IHasJSScript
  {
    #region Methods 
    public new void Insert()
    {
      base.Insert("Renov");
    }
    #endregion Methods 
  }
  #endregion AtribusidetRenov
}

