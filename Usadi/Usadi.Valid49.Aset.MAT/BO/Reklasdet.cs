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
  #region Usadi.Valid49.BO.ReklasdetControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class ReklasdetControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript, IExtDataControl
  {
    #region Properties
    public long Id { get; set; }
    public string Kdunit { get; set; }
    public string Nmunit { get; set; }
    public string Kdaset { get; set; }
    public string Nmaset { get; set; }
    public string Kdaset2 { get; set; }
    public string Nmaset2 { get; set; }
    public string Asetkey2 { get; set; }
    public string Noreg2 { get; set; }
    public string Idbrg { get; set; }
    public string Kdkon { get; set; }
    public string Nmkon { get; set; }
    public decimal Nilai { get; set; }
    public string Kdkibaset1 { get; set; }
    public string Kdkibaset2 { get; set; }
    public string Statusreklas { get; set; }
    public DateTime Tglvalid { get; set; }
    public string Kdkib { get; set; }
    public string Nmkib { get; set; }
    public string Idreklas { get { return Asetkey + '.' + Nilai.ToString("#,##0"); } }
    public string Unitkey { get; set; }
    public string Noreklas { get; set; }
    public string Kdtans { get; set; }
    public string Noreg { get; set; }
    public string Asetkey { get; set; }
    public string Blokid { get; set; }
    #endregion Properties 

    #region Methods 
    public ReklasdetControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLREKLASDET;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Noreklas", "Kdtans", "Noreg", "Asetkey" };
      cViewListProperties.IDKey = "Id";
      cViewListProperties.IDProperty = "Id";
      cViewListProperties.ReadOnlyFields = new String[] { "Kdaset2", "Nmaset2", "Asetkey2" };
      cViewListProperties.SortFields = new String[] { "Kdaset", "Tahun", "Noreg" };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_LOOKUP;
      cViewListProperties.LookupLabelQuery = "";
      cViewListProperties.PageSize = 30;
      cViewListProperties.RefreshFilter = true;

      if (Kdtans == "215")
      {
        cViewListProperties.LookupDC = " Usadi.Valid49.BO.ViewasetKdpControl, Usadi.Valid49.Aset.MAT";
      }
      else
      {
        cViewListProperties.LookupDC = " Usadi.Valid49.BO.ViewasetReklasControl, Usadi.Valid49.Aset.MAT";
      }

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
      bool enable = false, enableEdit = true;

      if (Kdtans == "307") //reklas ke aset lain2 enable kdkon
      {
        enable = true;
      }

      if (Tglvalid != new DateTime() || Blokid == "1")
      {
        enableEdit = false;
      }

      DataControlFieldCollection columns = new DataControlFieldCollection();

      columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center).SetVisible(enableEdit));

      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang Awal"), typeof(string), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang Awal"), typeof(string), 40, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun=Tahun Perolehan"), typeof(string), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreg=No Register Awal"), typeof(string), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai"), typeof(decimal), 20, HorizontalAlign.Right));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmkon=Kondisi Awal"), typeof(string), 20, HorizontalAlign.Center).SetVisible(enable));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset2=Kode Barang Baru"), typeof(string), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset2=Nama Barang Baru"), typeof(string), 40, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreg2=No Register Baru"), typeof(string), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Statusreklas=Status"), typeof(string), 20, HorizontalAlign.Center));

      return columns;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      if (typeof(IDataControlMenu).IsInstanceOfType(bo))
      {
        Last_by = ((BaseBO)bo).Userid;
      }
      else if (typeof(ReklasControl).IsInstanceOfType(bo))
      {
        Unitkey = ((ReklasControl)bo).Unitkey;
        Noreklas = ((ReklasControl)bo).Noreklas;
        Kdtans = ((ReklasControl)bo).Kdtans;
        Tglvalid = ((ReklasControl)bo).Tglvalid;
        Blokid = ((ReklasControl)bo).Blokid;
      }
      else if (typeof(ViewasetReklasControl).IsInstanceOfType(bo))
      {
        Asetkey = ((ViewasetReklasControl)bo).Asetkey;
        Tahun = ((ViewasetReklasControl)bo).Tahun;
        Noreg = ((ViewasetReklasControl)bo).Noreg;
        Idbrg = ((ViewasetReklasControl)bo).Idbrg;
        Kdkon = ((ViewasetReklasControl)bo).Kdkon;
        Nilai = ((ViewasetReklasControl)bo).Nilai;
      }
      else if (typeof(ViewasetKdpControl).IsInstanceOfType(bo))
      {
        Asetkey = ((ViewasetKdpControl)bo).Asetkey;
        Tahun = ((ViewasetKdpControl)bo).Tahun;
        Noreg = ((ViewasetKdpControl)bo).Noreg;
        Idbrg = ((ViewasetKdpControl)bo).Idbrg;
        Nilai = ((ViewasetKdpControl)bo).Nilai;
        Kdkib = ((ViewasetKdpControl)bo).Kdkib;
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
      List<ReklasdetControl> ListData = new List<ReklasdetControl>();
      foreach (ReklasdetControl dc in list)
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

      if (Kdtans == "215" && Kdkib != "06")
      {
        Asetkey2 = Asetkey;
      }
      else
      {
        Asetkey2 = null;
      }

      base.Insert();
    }
    public new int Update()
    {
      int n = 0;
      if (Kdtans != "215" && Asetkey2 == Asetkey)
      {
        throw new Exception("Kode barang baru tidak boleh sama dengan kode barang awal");
      }
      else
      {
        base.Update();
      }
      return n;
    }
    public new int Delete()
    {
      Status = -1;
      int n = ((BaseDataControlUI)this).Delete(BaseDataControl.DEFAULT);
      return n;
    }
    public override HashTableofParameterRow GetEntries()
        {
            bool enable = true;

            HashTableofParameterRow hpars = new HashTableofParameterRow();
            //if (Kdtans == "208")
            //{
            //  hpars.Add(DaftasetReklasLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enable).SetEditable(enable));
            //}
            if (Kdtans == "209") //Persediaan
            {
                hpars.Add(DaftasetPersediaanLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enable).SetEditable(enable));
            }
            else if (Kdtans == "225") //Rusak berat
            {
                hpars.Add(DaftasetRusakberatLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enable).SetEditable(enable));
            }
            else if (Kdtans == "226") //Non operasional
            {
                hpars.Add(DaftasetNonoperasionalLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enable).SetEditable(enable));
            }
            else if (Kdtans == "233") //Aset Lain-lain lainnya
            {
                hpars.Add(DaftasetAsetlainnyaLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enable).SetEditable(enable));
            }
            else if (Kdtans == "240") //Investasi
            {
              hpars.Add(DaftasetInvestasiLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enable).SetEditable(enable));
            }
            else // aset tetap dan kdp
            {
                hpars.Add(DaftasetReklasLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enable).SetEditable(enable));
            }
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
            ReklasdetControl ctrl = (ReklasdetControl)list[i];
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
  #endregion Reklasdet

  #region ReklasdetKdp
  [Serializable]
  public class ReklasdetKdpControl : ReklasdetControl, IDataControlUIEntry, IHasJSScript
  {
    public ImageCommand[] Cmds
    {
      get
      {
        ImageCommand cmd1 = new ImageCommand()
        {
          CommandName = "ViewReklasdetbrgkdp",
          Icon = Icon.PageCopy
        };
        cmd1.ToolTip.Text = "Klik Untuk Menampilkan Spesifikasi barang";
        return new ImageCommand[] { cmd1 };
      }
    }
    public string ViewReklasdetbrgkdp
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
        return "Spesifikasi Barang; " + Kdaset + " - " + Nmaset + ":" + url;
      }
    }
    #region Methods 
    public new IList View()
    {
      IList list = this.View("Kdp");
      return list;
    }
    public override DataControlFieldCollection GetColumns()
    {
      bool enableEdit = true;

      if (Tglvalid != new DateTime() || Blokid == "1")
      {
        enableEdit = false;
      }

      DataControlFieldCollection columns = new DataControlFieldCollection();

      columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center).SetVisible(enableEdit));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Stricon"), typeof(CommandColumn), Cmds, 5, HorizontalAlign.Center));

      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang Awal"), typeof(string), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang Awal"), typeof(string), 40, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun=Tahun Perolehan"), typeof(string), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreg=No Register Awal"), typeof(string), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai"), typeof(decimal), 20, HorizontalAlign.Right));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset2=Kode Barang Baru"), typeof(string), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset2=Nama Barang Baru"), typeof(string), 40, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreg2=No Register Baru"), typeof(string), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Statusreklas=Status"), typeof(string), 20, HorizontalAlign.Center));

      return columns;
    }
    #endregion Methods 
  }
  #endregion ReklasdetKdp

  #region ReklasdetRusakberat
  [Serializable]
    public class ReklasdetRusakberatControl : ReklasdetControl, IDataControlUIEntry, IHasJSScript
    {
        public ImageCommand[] Cmds
        {
            get
            {
                ImageCommand cmd1 = new ImageCommand()
                {
                    CommandName = "ViewReklasdetbrgrusakberat",
                    Icon = Icon.PageCopy
                };
                cmd1.ToolTip.Text = "Klik Untuk Menampilkan Spesifikasi barang";
                return new ImageCommand[] { cmd1 };
            }
        }
        public string ViewReklasdetbrgrusakberat
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
                return "Spesifikasi Barang; " + Kdaset + " - " + Nmaset + ":" + url;
            }
        }
        #region Methods 
        public new IList View()
        {
            IList list = this.View("Asetlainnya");
            return list;
        }
        public override DataControlFieldCollection GetColumns()
        {
            bool enableEdit = true;

            if (Tglvalid != new DateTime() || Blokid == "1")
            {
                enableEdit = false;
            }

            DataControlFieldCollection columns = new DataControlFieldCollection();

            columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center).SetVisible(enableEdit));
            //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Stricon"), typeof(CommandColumn), Cmds, 5, HorizontalAlign.Center));

            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang Awal"), typeof(string), 20, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang Awal"), typeof(string), 40, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun=Tahun Perolehan"), typeof(string), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreg=No Register Awal"), typeof(string), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai"), typeof(decimal), 20, HorizontalAlign.Right));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset2=Kode Barang Baru"), typeof(string), 20, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset2=Nama Barang Baru"), typeof(string), 40, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreg2=No Register Baru"), typeof(string), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Statusreklas=Status"), typeof(string), 20, HorizontalAlign.Center));

            return columns;
        }
        #endregion Methods 
    }
    #endregion ReklasdetRusakberat

    #region ReklasdetLainnya
    [Serializable]
    public class ReklasdetLainnyaControl : ReklasdetControl, IDataControlUIEntry, IHasJSScript
    {
        public ImageCommand[] Cmds
        {
            get
            {
                ImageCommand cmd1 = new ImageCommand()
                {
                    CommandName = "ViewReklasdetbrglainnya",
                    Icon = Icon.PageCopy
                };
                cmd1.ToolTip.Text = "Klik Untuk Menampilkan Spesifikasi barang";
                return new ImageCommand[] { cmd1 };
            }
        }
        public string ViewReklasdetbrglainnya
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
                return "Spesifikasi Barang; " + Kdaset + " - " + Nmaset + ":" + url;
            }
        }
        #region Methods 
        public new IList View()
        {
            IList list = this.View("Asetlainnya");
            return list;
        }
        public override DataControlFieldCollection GetColumns()
        {
            bool enableEdit = true;

            if (Tglvalid != new DateTime() || Blokid == "1")
            {
                enableEdit = false;
            }

            DataControlFieldCollection columns = new DataControlFieldCollection();

            columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center).SetVisible(enableEdit));
            //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Stricon"), typeof(CommandColumn), Cmds, 5, HorizontalAlign.Center));

            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang Awal"), typeof(string), 20, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang Awal"), typeof(string), 40, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun=Tahun Perolehan"), typeof(string), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreg=No Register Awal"), typeof(string), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai"), typeof(decimal), 20, HorizontalAlign.Right));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset2=Kode Barang Baru"), typeof(string), 20, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset2=Nama Barang Baru"), typeof(string), 40, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreg2=No Register Baru"), typeof(string), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Statusreklas=Status"), typeof(string), 20, HorizontalAlign.Center));

            return columns;
        }
        #endregion Methods 
    }
    #endregion ReklasdetRusakberat

    #region ReklasdetNonoperasional
    [Serializable]
    public class ReklasdetNonoperasionalControl : ReklasdetControl, IDataControlUIEntry, IHasJSScript
    {
        public ImageCommand[] Cmds
        {
            get
            {
                ImageCommand cmd1 = new ImageCommand()
                {
                    CommandName = "ViewReklasdetnonoperasional",
                    Icon = Icon.PageCopy
                };
                cmd1.ToolTip.Text = "Klik Untuk Menampilkan Spesifikasi barang";
                return new ImageCommand[] { cmd1 };
            }
        }
        public string ViewReklasdetnonoperasional
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
                return "Spesifikasi Barang; " + Kdaset + " - " + Nmaset + ":" + url;
            }
        }
        #region Methods 
        public new IList View()
        {
            IList list = this.View("Asetlainnya");
            return list;
        }
        public override DataControlFieldCollection GetColumns()
        {
            bool enableEdit = true;

            if (Tglvalid != new DateTime() || Blokid == "1")
            {
                enableEdit = false;
            }

            DataControlFieldCollection columns = new DataControlFieldCollection();

            columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center).SetVisible(enableEdit));
            //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Stricon"), typeof(CommandColumn), Cmds, 5, HorizontalAlign.Center));

            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang Awal"), typeof(string), 20, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang Awal"), typeof(string), 40, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun=Tahun Perolehan"), typeof(string), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreg=No Register Awal"), typeof(string), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai"), typeof(decimal), 20, HorizontalAlign.Right));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset2=Kode Barang Baru"), typeof(string), 20, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset2=Nama Barang Baru"), typeof(string), 40, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreg2=No Register Baru"), typeof(string), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Statusreklas=Status"), typeof(string), 20, HorizontalAlign.Center));

            return columns;
        }
        #endregion Methods 
    }
    #endregion ReklasdetNonoperasional
}

