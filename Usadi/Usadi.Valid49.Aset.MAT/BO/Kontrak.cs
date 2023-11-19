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
  #region Usadi.Valid49.BO.KontrakControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class KontrakControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript, IExtDataControl
  {
    #region Properties
    public long Id { get; set; }
    public string Kdp3 { get; set; }
    public string Nmp3 { get; set; }
    public string Nminst { get; set; }
    public string Kdkegunit { get; set; }
    public string Nukeg { get; set; }
    public string Nmkegunit { get; set; }
    public DateTime Tglkon { get; set; }
    public DateTime Tglawalkontrak { get; set; }
    public DateTime Tglakhirkontrak { get; set; }
    public DateTime Tglslskonk { get; set; }
    public string Uraian { get; set; }
    public decimal Nilai { get; set; }
    public string Kdunit { get; set; }
    public string Nmunit { get; set; }
    public string Kdtahap { get; set; }
    public string Nokontrak { get; set; }
    public string Unitkey { get; set; }
    public int Jmldata { get; set; }
    public decimal Nilpagu { get; set; }
    public decimal Nilkontrak { get; set; }
    public decimal Nilperkontrak { get; set; }
    public int Tahunsa { get; set; }
    public DateTime Tglsa { get; set; }
    public string Entrykontrak { get; set; }
    public string Blokid
    {
      get
      {
        WebuserControl cWebuserGetid = new WebuserControl();
        cWebuserGetid.Userid = GlobalAsp.GetSessionUser().GetUserID();
        cWebuserGetid.Load("PK");

        return cWebuserGetid.Blokid;
      }
    }
    #endregion Properties 

    #region Methods 
    public KontrakControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLKONTRAK;
      #region COR-49 Refactor Get Tahun dari Setting DB - STEP 2
      Ss10userControl dcUser = (Ss10userControl)GlobalAsp.GetSessionUser();
      Tahun = dcUser.Tahun;
      Tahunsa = dcUser.Tahun;
      #endregion
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Kdkegunit", "Kdtahap" };
      cViewListProperties.IDKey = "Id";
      cViewListProperties.IDProperty = "Id";
      cViewListProperties.ReadOnlyFields = new String[] { "Unitkey", "Kdunit", "Nmunit", "Kdtahap", "Kdkegunit", "Tahunsa", "Tahun" };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
      cViewListProperties.PageSize = 30;
      cViewListProperties.RefreshFilter = true;

      WebsetControl cWebset = new WebsetControl();
      cWebset.Kdset = "phk3kontrak";
      cWebset.Load("PK");
      Entrykontrak = cWebset.Valset.ToUpper();

      if (Blokid == "1" || Entrykontrak != "Y")
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
      bool enable = true;

      if (Blokid == "1")
      {
        enable = false;
      }

      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center).SetVisible(enable));

      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nokontrak=No Kontrak"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglawalkontrak=Tanggal Kontrak"), typeof(DateTime), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglakhirkontrak=Tanggal Selesai"), typeof(DateTime), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nminst=Nama Pihak 3"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Uraian"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai"), typeof(decimal), 25, HorizontalAlign.Left));

      return columns;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      if (typeof(IDataControlMenu).IsInstanceOfType(bo))
      {
        Unitkey = (string)GlobalAsp.GetSessionUser().GetValue("Unitkey");
        Kdunit = (string)GlobalAsp.GetSessionUser().GetValue("Kdunit");
        Nmunit = (string)GlobalAsp.GetSessionUser().GetValue("Nmunit");
      }
      else if (typeof(KontrakControl).IsInstanceOfType(bo))
      {
        Kdkegunit = ((KontrakControl)bo).Kdkegunit;
        Kdtahap = ((KontrakControl)bo).Kdtahap;
      }
    }
    public new void SetPrimaryKey()
    {
      Tglsa = new DateTime(Tahunsa, DateTime.Today.Month, DateTime.Today.Day);
      Tglawalkontrak = Tglsa;
      Tglakhirkontrak = Tglsa;
    }
    public new HashTableofParameterRow GetFilters()
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(DaftunitLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetAllowEmpty(false));
      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdtahap=Tahap"),
      GetList(new TahapLookupControl()), "Kdtahap=Uraian", 41).SetAllowRefresh(true).SetEnable(enableFilter));
      hpars.Add(KegunitLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetAllowEmpty(false));

      return hpars;
    }

    public override HashTableofParameterRow GetEntries()
    {
      bool enable = true;
      KontrakControl cKonCekdata = new KontrakControl();
      cKonCekdata.Unitkey = Unitkey;
      cKonCekdata.Nokontrak = Nokontrak;
      cKonCekdata.Kdkegunit = Kdkegunit;
      cKonCekdata.Load("Cekjmldata");

      if (cKonCekdata.Jmldata != 0)
      {
        enable = false;
      }

      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nokontrak=No Kontrak"), false, 97).SetEnable(enable));
      hpars.Add(Daftphk3LookupControl.Instance.GetLookupParameterRow(this, false).SetAllowEmpty(false).SetEnable(enable));
      hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglawalkontrak=Tanggal Kontrak"), true).SetEnable(enable));
      hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglakhirkontrak=Tanggal Selesai"), true).SetEnable(enable));
      hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Uraian=Uraian"), true, 99).SetEnable(enable));
      hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Nilai=Nilai Kontrak"), true, 30).SetEnable(enable));

      return hpars;
    }
    public new void Insert()
    {
      PemdaControl cPemda = new PemdaControl();
      cPemda.Configid = "cur_thang";
      cPemda.Load("PK");

      KontrakControl cKonPagu = new KontrakControl();
      cKonPagu.Unitkey = Unitkey;
      cKonPagu.Kdtahap = Kdtahap;
      cKonPagu.Kdkegunit = Kdkegunit;
      cKonPagu.Load("Nilpagu");

      KontrakControl cKonNilkon = new KontrakControl();
      cKonNilkon.Unitkey = Unitkey;
      cKonNilkon.Kdkegunit = Kdkegunit;
      cKonNilkon.Load("Nilkontrak");

      if (Tglawalkontrak.Year.ToString().Trim() != cPemda.Configval.Trim())
      {
        throw new Exception("Gagal menyimpan data : Kontrak yang diinput hanya untuk tahun anggaran berjalan.");
      }
      if ((cKonNilkon.Nilkontrak + Nilai) > cKonPagu.Nilpagu)
      {
        string sisa = (cKonPagu.Nilpagu - Nilkontrak).ToString("#,##0");
        throw new Exception("Gagal menyimpan data : Nilai kontrak melebihi pagu kegiatan, sisa pagu " + sisa);
      }
      base.Insert();
    }
    public new int Update()
    {
      KontrakControl cKonPagu = new KontrakControl();
      cKonPagu.Unitkey = Unitkey;
      cKonPagu.Kdtahap = Kdtahap;
      cKonPagu.Kdkegunit = Kdkegunit;
      cKonPagu.Load("Nilpagu");

      KontrakControl cKonNilkon = new KontrakControl();
      cKonNilkon.Unitkey = Unitkey;
      cKonNilkon.Kdkegunit = Kdkegunit;
      cKonNilkon.Load("Nilkontrak");

      KontrakControl cKonPernomor = new KontrakControl();
      cKonPernomor.Unitkey = Unitkey;
      cKonPernomor.Kdkegunit = Kdkegunit;
      cKonPernomor.Nokontrak = Nokontrak;
      cKonPernomor.Load("Nokontrak");

      decimal sisakontrak = (cKonNilkon.Nilkontrak - cKonPernomor.Nilperkontrak);
      decimal sisa = (cKonPagu.Nilpagu - cKonNilkon.Nilkontrak);

      PemdaControl cPemda = new PemdaControl();
      cPemda.Configid = "cur_thang";
      cPemda.Load("PK");

      if (cKonPagu.Nilpagu < (Nilai + sisakontrak))
      {
        throw new Exception("Gagal menyimpan data : Nilai kontrak melebihi pagu kegiatan, sisa pagu " + sisa.ToString("#,##0"));
      }
      else if (Tglawalkontrak.Year.ToString().Trim() != cPemda.Configval.Trim())
      {
        throw new Exception("Gagal menyimpan data : Tanggal kontrak hanya untuk tahun anggaran berjalan");
      }
      else
      {
        return base.Update();
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
      List<KontrakControl> ListData = new List<KontrakControl>();
      foreach (KontrakControl dc in list)
      {
        ListData.Add(dc);
      }
      return ListData;
    }
    public new int Delete()
    {
      Status = -1;

      KontrakControl cKonCekdata = new KontrakControl();
      cKonCekdata.Unitkey = Unitkey;
      cKonCekdata.Nokontrak = Nokontrak;
      cKonCekdata.Kdkegunit = Kdkegunit;
      cKonCekdata.Load("Cekjmldata");

      if (cKonCekdata.Jmldata != 0)
      {
        throw new Exception("Gagal menghapus data : Kontrak sudah digunakan diberita acara");
      }
      else
      {
        int n = ((BaseDataControlUI)this).Delete(BaseDataControl.DEFAULT);
        return n;
      }
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

        decimal total = 0;
        decimal subtotal = 0;
        if (list != null && list.Count > 0)
        {
          int start = (idx * pagesize);
          int finish = ((idx + 1) * pagesize);
          for (int i = 0; i < list.Count; i++)
          {
            KontrakControl ctrl = (KontrakControl)list[i];
            if ((i >= start) && (i <= finish))
            {
              subtotal += ctrl.Nilai;
            }
            total += ctrl.Nilai;
          }
        }
        //DisplayField DfSubTotal = ControlUtils.FindControl<DisplayField>(seed, "DfSubTotal");
        DisplayField DfTotal = ControlUtils.FindControl<DisplayField>(seed, "DfTotal");
        //DfSubTotal.Text = "Pagu kegiatan = " + subtotal.ToString("#,##0");
        DfTotal.Text = "Nilai Total Kontrak = " + total.ToString("#,##0");
      }
    }
    #endregion Methods 
  }
  #endregion Kontrak
}

