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
  #region Usadi.Valid49.BO.PenilaianControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class PenilaianControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
  {
    #region Properties
    public long Id { get; set; }
    public DateTime Tglpenilaian { get; set; }
    public new string Ket { get; set; }
    public DateTime Tglvalid { get; set; }
    public string Kdunit { get; set; }
    public string Nmunit { get; set; }
    public string Nmtrans { get; set; }
    public int Jmldata { get; set; }
    public int Tahunsa { get; set; }
    public DateTime Tglsa { get; set; }
    public string Kdtans { get; set; }
    public string Nopenilaian { get; set; }
    public string Unitkey { get; set; }
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
    public ImageCommand[] Cmds
    {
      get
      {
        ImageCommand cmd1 = new ImageCommand()
        {
          CommandName = "ViewPenilaiandet",
          Icon = Icon.PageCopy
        };
        cmd1.ToolTip.Text = "Klik Untuk Menampilkan Rincian Penilaian";
        return new ImageCommand[] { cmd1 };
      }
    }

    public string ViewPenilaiandet
    {
      get
      {
        string app = GlobalAsp.GetRequestApp();
        string id = GlobalAsp.GetRequestId();
        string idprev = GlobalAsp.GetRequestId();
        string kode = GlobalAsp.GetRequestKode();
        string idx = GlobalAsp.GetRequestIndex();
        string strenable = "&enable=" + ((Status == 0) ? 1 : 0);
        string url = string.Format("PageTabular.aspx?passdc=1&app={0}&i={1}&id={2}&idprev={3}&kode={4}&idx={5}" + strenable, app, 11, id, idprev, kode, idx);
        return "No. Penilaian; " + Nopenilaian + ":" + url;
      }
    }
    #endregion Properties 

    #region Methods 
    public PenilaianControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLPENILAIAN;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Nopenilaian", "Kdtans" };
      cViewListProperties.IDKey = "Id";
      cViewListProperties.IDProperty = "Id";
      cViewListProperties.ReadOnlyFields = new String[] { "Unitkey", "Kdtans" };
      cViewListProperties.SortFields = new String[] { "Tglpenilaian" };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
      cViewListProperties.PageSize = 20;
      cViewListProperties.RefreshFilter = true;

      if (Blokid == "1")
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
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Stricon"), typeof(CommandColumn), Cmds, 5, HorizontalAlign.Center));

      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nopenilaian=Nomor Dokumen"), typeof(string), 40, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmtrans=Jenis Transaksi"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglpenilaian=Tanggal Dokumen"), typeof(DateTime), 30, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglvalid=Tanggal Pengesahan"), typeof(DateTime), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 50, HorizontalAlign.Left));

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
    }
    public new void SetPrimaryKey()
    {
      PemdaControl cPemda = new PemdaControl();
      cPemda.Configid = "cur_thang";
      cPemda.Load("PK");

      Tahunsa = (Int32.Parse(cPemda.Configval));
      Tglsa = new DateTime(Tahunsa, DateTime.Today.Month, DateTime.Today.Day);
      Tglpenilaian = Tglsa;
    }
    public new HashTableofParameterRow GetFilters()
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(DaftunitLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetAllowEmpty(false));
      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdtans=Jenis Transaksi"),
      GetList(new JtransPenilaianLookupControl()), "Kdtans=Nmtrans", 48).SetAllowRefresh(true).SetEnable(enableFilter).SetAllowEmpty(false));

      return hpars;
    }
    public new IList View()
    {
      IList list = this.View(BaseDataControl.ALL);
      return list;
    }
    public new IList View(string label)
    {
      IList list = ((BaseDataControl)this).View(label);
      List<PenilaianControl> ListData = new List<PenilaianControl>();
      foreach (PenilaianControl dc in list)
      {
        dc.Valid = (dc.Tglvalid != new DateTime());
        ListData.Add(dc);
      }

      return ListData;
    }
    public override HashTableofParameterRow GetEntries()
    {
      bool enableValid = false;
      bool enable = !Valid;

      PenilaianControl cPenilaianCekdata = new PenilaianControl();
      cPenilaianCekdata.Unitkey = Unitkey;
      cPenilaianCekdata.Nopenilaian = Nopenilaian;
      cPenilaianCekdata.Kdtans = Kdtans;
      cPenilaianCekdata.Load("Cekjmldata");

      if (cPenilaianCekdata.Jmldata != 0)
      {
        enableValid = true;
      }

      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nopenilaian=Nomor Dokumen"), false, 50).SetEnable(enable));
      hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglpenilaian=Tgl Dokumen"), true).SetEnable(enable));
      hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Ket=Keterangan"), true, 3).SetEnable(enable).SetAllowEmpty(true));
      hpars.Add(new ParameterRowCek(this, true).SetEnable(enableValid).SetEditable(enable));
      return hpars;
    }
    private bool IsValid()
    {
      bool valid = true;
      return valid;
    }
    public new void Insert()
    {
      PemdaControl cPemda = new PemdaControl();
      cPemda.Configid = "cur_thang";
      cPemda.Load("PK");

      if (Tglpenilaian.Year.ToString().Trim() != cPemda.Configval.Trim())
      {
        throw new Exception("Transaksi penilaian hanya untuk tahun anggaran berjalan, cek kembali tanggal dokumen");
      }
      base.Insert();
    }
    public new int Update()
    {
      PemdaControl cPemda = new PemdaControl();
      cPemda.Configid = "cur_thang";
      cPemda.Load("PK");

      int n = 0;
      if (IsValid())
      {
        if (Tglpenilaian.Year.ToString().Trim() != cPemda.Configval.Trim())
        {
          throw new Exception("Transaksi penilaian hanya untuk tahun anggaran berjalan, cek kembali tanggal dokumen");
        }
        else
        {
          n = ((BaseDataControlUI)this).Update();

          if (Valid)
          {
            Tglvalid = Tglpenilaian;
            base.Update("Sah");
          }
        }
      }
      return n;
    }
    public new int Delete()
    {
      PenilaianControl cPenilaianCekkemitraan = new PenilaianControl();
      cPenilaianCekkemitraan.Unitkey = Unitkey;
      cPenilaianCekkemitraan.Nopenilaian = Nopenilaian;
      cPenilaianCekkemitraan.Kdtans = Kdtans;
      cPenilaianCekkemitraan.Load("Kemitraan");

      PenilaianControl cPenilaianCekpindahtangan = new PenilaianControl();
      cPenilaianCekpindahtangan.Unitkey = Unitkey;
      cPenilaianCekpindahtangan.Nopenilaian = Nopenilaian;
      cPenilaianCekpindahtangan.Kdtans = Kdtans;
      cPenilaianCekpindahtangan.Load("Pindahtangan");

      int n = 0;
      if (Valid)
      {
        if (cPenilaianCekkemitraan.Jmldata != 0)
        {
          throw new Exception("Aset sudah di digunakan di transaksi pemanfaatan, pengesahan tidak dapat dicabut.");
        }
        if (cPenilaianCekpindahtangan.Jmldata != 0)
        {
          throw new Exception("Aset sudah di digunakan di transaksi pemindahtanganan, pengesahan tidak dapat dicabut.");
        }

        return ((BaseDataControlUI)this).Update("Draft");
      }
      else
      {
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
      }
      return n;
    }
    #endregion Methods 
  }
  #endregion Penilaian
}

