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
  #region Usadi.Valid49.BO.BapkirControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class BapkirControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
  {
    #region Properties
    public long Id { get; set; }
    public string Kdunit { get; set; }
    public string Nmunit { get; set; }
    public string Kdruang { get; set; }
    public string Nmruang { get; set; }
    public string Nobapkir { get; set; }
    public string Kdbapkir { get; set; }
    public string Nmbapkir { get; set; }
    public DateTime Tglbapkir { get; set; }
    public new string Ket { get; set; }
    public int Jmldata { get; set; }
    public int Tahunsa { get; set; }
    public DateTime Tglsa { get; set; }
    public string Unitkey { get; set; }
    public string Ruangkey { get; set; }
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
          CommandName = "ViewTransaksi",
          Icon = Icon.PageCopy
        };
        cmd1.ToolTip.Text = "Klik Untuk Menampilkan Rincian Transaksi";
        return new ImageCommand[] { cmd1 };
      }
    }

    public string ViewTransaksi
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
        return "No. KIR " + Nobapkir + "; " + "Ruangan " + Nmruang + "; " + "Jenis KIR " + Nmbapkir + ":" + url;
      }
    }
    #endregion Properties 

    #region Methods 
    public BapkirControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLBAPKIR;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Ruangkey", "Nobapkir", "Kdbapkir" };
      cViewListProperties.IDKey = "Id";
      cViewListProperties.IDProperty = "Id";
      cViewListProperties.ReadOnlyFields = new String[] { "Unitkey", "Ruangkey" };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
      cViewListProperties.PageSize = 30;

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

      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nobapkir=Nomor KIR"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglbapkir=Tgl Penempatan"), typeof(DateTime), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmbapkir= Jenis Transaksi KIR"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 60, HorizontalAlign.Left));

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
      Tglbapkir = Tglsa;
    }
    public new HashTableofParameterRow GetFilters()
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(DaftunitLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetAllowEmpty(false));
      hpars.Add(StskirLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enableFilter).SetAllowEmpty(false));

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
      List<BapkirControl> ListData = new List<BapkirControl>();
      foreach (BapkirControl dc in list)
      {
        ListData.Add(dc);
      }
      return ListData;
    }

    public override HashTableofParameterRow GetEntries()
    {
      bool enable = true;
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nobapkir=Nomor KIR"), false, 50).SetEnable(enable));
      hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglbapkir=Tanggal KIR"), true).SetEnable(enable));
      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdbapkir=Jenis KIR"),
      GetList(new StskirLookupControl()), "Kdbapkir=Nmbapkir", 48).SetEnable(enable).SetAllowEmpty(false));
      hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Ket=Keterangan"), true, 90).SetEnable(enable).SetAllowEmpty(true));

      return hpars;
    }
    public new void Insert()
    {
      PemdaControl cPemda = new PemdaControl();
      cPemda.Configid = "cur_thang";
      cPemda.Load("PK");

      if (Tglbapkir.Year.ToString().Trim() != cPemda.Configval.Trim())
      {
        throw new Exception("Gagal menyimpan data : Tgl penempatan aset hanya untuk tahun anggaran berjalan.");
      }
      base.Insert();
    }
    public new int Update()
    {
      PemdaControl cPemda = new PemdaControl();
      cPemda.Configid = "cur_thang";
      cPemda.Load("PK");

      int n = 0;

      if (Tglbapkir.Year.ToString().Trim() != cPemda.Configval.Trim())
      {
        throw new Exception("Gagal mengubah data : tanggal penempatan aset hanya untuk tahun anggaran berjalan");
      }
      else
      {
        base.Update();
      }
      return n;
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
    #endregion Methods 
  }
  #endregion Bapkir
}

