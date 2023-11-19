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
  #region Usadi.Valid49.BO.KemitraandetControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class KemitraandetControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript, IExtDataControl
  {
    #region Properties
    public long Id { get; set; }
    public string Idbrg { get; set; }
    public string Kdsatwaktu { get; set; }
    public string Nmsatwaktu { get; set; }
    public decimal Jangkawaktu { get; set; }
    public decimal Nilai { get; set; }
    public string Nopenilaian { get; set; }
    public decimal Nilpenilaian { get; set; }
    public string Statusmitra { get; set; }
    public string Statuskemitraan { get; set; }
    public string Kdunit { get; set; }
    public string Nmunit { get; set; }
    public string Kdaset { get; set; }
    public string Nmaset { get; set; }
    public string Nminst { get; set; }
    public string Nmtrans { get; set; }
    public string Kdjenis { get; set; }
    public DateTime Tglvalid { get; set; }
    public string Noreg { get; set; }
    public string Asetkey { get; set; }
    public string Kdkib { get; set; }
    public string Kdtans { get; set; }
    public string Kdp3 { get; set; }
    public string Nodokumen { get; set; }
    public string Unitkey { get; set; }
    public string Blokid { get; set; }
    #endregion Properties 

    #region Methods 
    public KemitraandetControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLKEMITRAANDET;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Nodokumen", "Kdp3", "Kdtans", "Asetkey", "Tahun", "Noreg" };
      cViewListProperties.IDKey = "Id";
      cViewListProperties.IDProperty = "Id";
      cViewListProperties.ReadOnlyFields = new String[] { "Unitkey", "Nodokumen", "Kdp3", "Kdtans" };
      cViewListProperties.SortFields = new String[] { "Kdaset", "Tahun", "Noreg" };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
      cViewListProperties.PageSize = 20;

      if (Tglvalid != new DateTime() || Blokid == "1")
      {
        cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      }
      else
      {
        if (Kdjenis == "02")
        {
          cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_EDIT;
        }
        else
        {
          cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_DEL;
          cViewListProperties.AllowMultiDelete = true;
        }
      }

      return cViewListProperties;
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
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun=Tahun Perolehan"), typeof(int), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreg=No Register"), typeof(string), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmsatwaktu=Satuan Waktu"), typeof(string), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Jangkawaktu=Jangka Waktu"), typeof(decimal), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Statuskemitraan=Status"), typeof(string), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai=Harga"), typeof(decimal), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nopenilaian=No Penilaian"), typeof(string), 25, HorizontalAlign.Left));
      return columns;
    }
    public override HashTableofParameterRow GetEntries()
    {
      bool enable = true; //enableTgl = !Valid; enableSelesai = false;

      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(ViewasetKemitraanLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enable).SetEditable(false));
      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdsatwaktu=Satuan Waktu"),
      GetList(new SatwaktuLookupControl()), "Kdsatwaktu=Nmsatwaktu", 30).SetAllowRefresh(false).SetEnable(enable).SetAllowEmpty(false));
      hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Jangkawaktu=Jangka Waktu"), true, 30).SetEnable(enable).SetEditable(true));
      ArrayList list = new ArrayList(new ParamControl[] {
        new ParamControl() {  Kdpar="0",Nmpar="Sebagian "}
        ,new ParamControl() { Kdpar="1",Nmpar="Semua "}
      });
      hpars.Add(new ParameterRow(ConstantDict.GetColumnTitleEntry("Statusmitra=Status"), ParameterRow.MODE_SELECT,
        list, "Kdpar=Nmpar", 30).SetAllowRefresh(false).SetEnable(enable).SetEditable(true));
      hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Nilai=Harga"), true, 30).SetEnable(enable).SetEditable(true));

      return hpars;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      if (typeof(IDataControlMenu).IsInstanceOfType(bo))
      {
        Last_by = ((BaseBO)bo).Userid;
      }
      else if (typeof(KemitraanControl).IsInstanceOfType(bo))
      {
        Unitkey = ((KemitraanControl)bo).Unitkey;
        Nodokumen = ((KemitraanControl)bo).Nodokumen;
        Kdp3 = ((KemitraanControl)bo).Kdp3;
        Kdtans = ((KemitraanControl)bo).Kdtans;
        Kdjenis = ((KemitraanControl)bo).Kdjenis;
        Tglvalid = ((KemitraanControl)bo).Tglvalid;
        Blokid = ((KemitraanControl)bo).Blokid;
      }
    }
    public new void SetPrimaryKey()
    {
      Statusmitra = "1";
    }
    public new IList View()
    {
      IList list = this.View(BaseDataControl.ALL);
      return list;
    }
    public new IList View(string label)
    {
      IList list = ((BaseDataControl)this).View(label);
      List<KemitraandetControl> ListData = new List<KemitraandetControl>();
      foreach (KemitraandetControl dc in list)
      {
        ListData.Add(dc);
      }

      return ListData;
    }
    public new void Insert()
    {
      string sql = @"
        exec [dbo].[WSP_VALKEMITRAANDET]
        @UNITKEY = N'{0}',
        @NODOKUMEN = N'{1}',
        @KDP3 = N'{2}',
        @KDTANS = N'{3}',
        @IDBRG = N'{4}',
        @ASETKEY = N'{5}',
        @KDSATWAKTU = N'{6}',
        @JANGKAWAKTU = N'{7}',
        @NOPENILAIAN = N'{8}',
        @NILAI = N'{9}',
        @STATUS = N'{10}'
        ";

      sql = string.Format(sql, Unitkey, Nodokumen, Kdp3, Kdtans, Idbrg, Asetkey, Kdsatwaktu, Jangkawaktu, Nopenilaian, Nilai, Statusmitra);
      BaseDataAdapter.ExecuteCmd(this, sql);
    }
    public new int Update()
    {
      KemitraandetControl cKemitraandetGetnilpenilaian = new KemitraandetControl();
      cKemitraandetGetnilpenilaian.Unitkey = Unitkey;
      cKemitraandetGetnilpenilaian.Nopenilaian = Nopenilaian;
      cKemitraandetGetnilpenilaian.Kdtans = Kdtans;
      cKemitraandetGetnilpenilaian.Idbrg = Idbrg;
      cKemitraandetGetnilpenilaian.Asetkey = Asetkey;
      cKemitraandetGetnilpenilaian.Load("Nilpenilaian");
      Nilpenilaian = cKemitraandetGetnilpenilaian.Nilpenilaian;

      int n = 0;
      if (Nilai < Nilpenilaian)
      {
        throw new Exception("Harga tidak boleh kurang dari penilaian: Rp. " + Nilpenilaian.ToString("#,##0"));
      }
      else
      {
        base.Update();
        base.Update("Header");
      }
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
            KemitraandetControl ctrl = (KemitraandetControl)list[i];
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
  #endregion Kemitraandet
}

