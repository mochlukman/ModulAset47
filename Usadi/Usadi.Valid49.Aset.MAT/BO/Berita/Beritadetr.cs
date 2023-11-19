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
  #region Usadi.Valid49.BO.BeritadetrControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class BeritadetrControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript, IExtDataControl
  {
    #region Properties
    public long Id { get; set; }
    public decimal Nilairek { get; set; }
    public decimal Nilai { get; set; }
    public string Kdper { get; set; }
    public string Nmper { get; set; }
    public string Kdunit { get; set; }
    public string Nmunit { get; set; }
    public string Kdtahap { get; set; }
    public string Kdkegunit { get; set; }
    public string Kddana { get; set; }
    public string Kdtans { get; set; }
    public string Nokontrak { get; set; }
    public DateTime Tglba { get; set; }
    public DateTime Tglvalid { get; set; }
    public decimal Nilaisisa { get; set; }
    public string Unitkey { get; set; }
    public string Noba { get; set; }
    public string Mtgkey { get; set; }
    public string Blokid { get; set; }
    public ImageCommand[] Cmds
    {
      get
      {
        ImageCommand cmd1 = new ImageCommand()
        {
          CommandName = "ViewBeritadetbrg",
          Icon = Icon.PageCopy
        };
        cmd1.ToolTip.Text = "Klik Untuk Menampilkan Kode Barang";
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
        string url = string.Format("PageTabular.aspx?passdc=1&app={0}&i=12&iprev=11&id={1}&idprev={2}&kode={3}&idx={4}" + strenable, app, id, idprev, kode, idx);
        return "Rincian Kode Barang - Rekening " + Nmper + ":" + url;
      }
    }
    #endregion Properties 

    #region Methods 
    public BeritadetrControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLBERITADETR;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Noba", "Mtgkey" };
      cViewListProperties.IDKey = "Id";
      cViewListProperties.IDProperty = "Id";
      cViewListProperties.ReadOnlyFields = new String[] { };
      cViewListProperties.SortFields = new String[] { "Kdper" };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_LOOKUP;
      cViewListProperties.LookupDC = " Usadi.Valid49.BO.DaskrBastLookupControl, Usadi.Valid49.Aset.DM";
      cViewListProperties.LookupLabelQuery = "Pengadaan";
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
      bool enable = false;

      if (Kdtans == "112")
      {
        if (Tglvalid != new DateTime() || Blokid == "1")
        {
          enable = false;
        }
        else
        {
          enable = true;
        }
      }

      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center).SetVisible(enable));

            if (Kdtans != "112")
            {
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Stricon"), typeof(CommandColumn), Cmds, 5, HorizontalAlign.Center));
            }
            //if (Kdtans == "112")
            //{
            //    //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Stricon"), typeof(CommandColumn), Cmds, 5, HorizontalAlign.Center).SetVisible(false));
            //}
            //else
            //{
            //    columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Stricon"), typeof(CommandColumn), Cmds, 5, HorizontalAlign.Center));
            //}

            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdper=Kode Rekening"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmper=Rekening"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai=Nilai BAST"), typeof(decimal), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilairek=Nilai Rekening DPA"), typeof(decimal), 30, HorizontalAlign.Left));
      return columns;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      if (typeof(IDataControlMenu).IsInstanceOfType(bo))
      {
        Last_by = ((BaseBO)bo).Userid;
      }
      else if (typeof(BeritaControl).IsInstanceOfType(bo))
      {
        Unitkey = ((BeritaControl)bo).Unitkey;
        Noba = ((BeritaControl)bo).Noba;
        Nokontrak = ((BeritaControl)bo).Nokontrak;
        Kdtahap = ((BeritaControl)bo).Kdtahap;
        Kdkegunit = ((BeritaControl)bo).Kdkegunit;
        Kdtans = ((BeritaControl)bo).Kdtans;
        Kddana = ((BeritaControl)bo).Kddana;
        Tglba = ((BeritaControl)bo).Tglba;
        Tglvalid = ((BeritaControl)bo).Tglvalid;
        Blokid = ((BeritaControl)bo).Blokid;
      }
      else if (typeof(DaskrControl).IsInstanceOfType(bo))
      {
        Mtgkey = ((DaskrControl)bo).Mtgkey;
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
      List<BeritadetrControl> ListData = new List<BeritadetrControl>();
      foreach (BeritadetrControl dc in list)
      {
        ListData.Add(dc);
      }

      return ListData;
    }
    public new int Delete()
    {
      int n = 0;
      try
      {
        Status = -1;
        base.Delete();
      }
      catch (Exception ex)
      {
        if (ex.Message.Contains("REFERENCE"))
        {
          string msg = "Hapus rincian barang terlebih dahulu";
          msg = string.Format(msg);
          throw new Exception(msg);
        }
      }
      return n;
    }
    public override HashTableofParameterRow GetEntries()
    {
      bool enable = true;
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(DaskrBastLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(enable).SetEditable(false).SetAllowRefresh(true));
      hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Nilairek=Nilai Rekening DPA"), true, 28).SetEnable(false)
        .SetEditable(false).SetAllowEmpty(false));
      hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Nilai"), true, 28).SetEnable(enable)
        .SetEditable(enable).SetAllowEmpty(false));
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
            BeritadetrControl ctrl = (BeritadetrControl)list[i];
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
        DfTotal.Text = "Total Nilai BAST = " + total.ToString("#,##0");
      }
    }
    #endregion Methods 
  }
  #endregion Beritadetr

  #region BeritadetrBakf
  [Serializable]
  public class BeritadetrBakfControl : BeritadetrControl, IDataControlUIEntry, IHasJSScript
  {
    #region Methods 
    public new int Update()
    {
      int n = 0;

      string sql = @"
        exec [dbo].[WSP_VALBERITADETR]
        @UNITKEY = N'{0}',
        @NOBA = N'{1}',
        @NOKONTRAK = N'{2}',
        @MTGKEY = N'{3}',
        @KDTAHAP = N'{4}',
        @KDKEGUNIT = N'{5}',
        @NILAI = N'{6}'
        ";

      sql = string.Format(sql, Unitkey, Noba, Nokontrak, Mtgkey, Kdtahap, Kdkegunit, Nilai);
      BaseDataAdapter.ExecuteCmd(this, sql);

      //n = ((BaseDataControlUI)this).Update("Termin");
      return n;
    }
    #endregion Methods 
  }
  #endregion BeritadetrBakf
}

