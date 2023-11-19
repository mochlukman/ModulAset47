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
  #region Usadi.Valid49.BO.RkbmdPengadaanControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class RkbmdPengadaanControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
  {
    #region Properties
    public long Id { get; set; }
    public string Idprgrm { get; set; }
    public int Jumlahaset { get; set; }
    public int Jumlahrb { get; set; }
    public int Jmltersedia { get; set; }
    public int Kebutuhan { get; set; }
    public int Jumlahusulan { get; set; }
    public int Jumlahsah { get; set; }
    public string Kdsatuan { get; set; }
    public new string Ket { get; set; }
    public DateTime Tglvalid { get; set; }
    public string Statususulan { get; set; }
    public string Kdnmaset { get; set; }
    public int Jmldata { get; set; }
    public string Kdunit { get; set; }
    public string Nmunit { get; set; }
    public string Nmkegunit { get; set; }
    public string Nukeg { get; set; }
    public string Nmprgrm { get; set; }
    public string Kdaset { get; set; }
    public string Nmaset { get; set; }
    public string Nmsatuan { get; set; }
    public string Kdkib { get; set; }
    public string Nmkib { get; set; }
    public string Unitkey { get; set; }
    public string Kdkegunit { get; set; }
    public string Asetkey { get; set; }
    public string Thang { get; set; }
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
    public RkbmdPengadaanControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLRKBMD_PENGADAAN;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Kdkegunit", "Asetkey", "Thang" };
      cViewListProperties.IDKey = "Id";
      cViewListProperties.IDProperty = "Id";
      cViewListProperties.ReadOnlyFields = new String[] { "Kdtahun", "Unitkey", "Kdkegunit" };
      cViewListProperties.SortFields = new String[] { "Kdaset" };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_LOOKUP;
      cViewListProperties.LookupDC = " Usadi.Valid49.BO.ViewasetRkbmdPengadaanControl, Usadi.Valid49.Aset.MAT";
      cViewListProperties.LookupLabelQuery = "";
      cViewListProperties.PageSize = 30;
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
    public new void SetFilterKey(BaseBO bo)
    {
      if (typeof(IDataControlMenu).IsInstanceOfType(bo))
      {
        Unitkey = (string)GlobalAsp.GetSessionUser().GetValue("Unitkey");
        Kdunit = (string)GlobalAsp.GetSessionUser().GetValue("Kdunit");
        Nmunit = (string)GlobalAsp.GetSessionUser().GetValue("Nmunit");
      }
      else if (typeof(ViewasetRkbmdPengadaanControl).IsInstanceOfType(bo))
      {
        Asetkey = ((ViewasetRkbmdPengadaanControl)bo).Asetkey;
        Jumlahaset = ((ViewasetRkbmdPengadaanControl)bo).Jumlahaset;
        Jumlahrb = ((ViewasetRkbmdPengadaanControl)bo).Jumlahrb;
      }
    }
    public new void SetPageKey()
    {
      PemdaControl cPemda = new PemdaControl();
      cPemda.Configid = "cur_thang";
      cPemda.Load("PK");

      Thang = (Int32.Parse(cPemda.Configval) + 1).ToString();
    }
    public new void SetPrimaryKey()
    {
      RkbmdKegunitControl cRkbmdKegunit = new RkbmdKegunitControl();
      cRkbmdKegunit.Unitkey = Unitkey;
      cRkbmdKegunit.Kdkegunit = Kdkegunit;
      cRkbmdKegunit.Thang = Thang;
      cRkbmdKegunit.Load("PK");

      Idprgrm = cRkbmdKegunit.Idprgrm.Trim();
    }
    public new IList View()
    {
      IList list = this.View(BaseDataControl.ALL);
      return list;
    }
    public new IList View(string label)
    {
      IList list = ((BaseDataControl)this).View(label);
      List<RkbmdPengadaanControl> ListData = new List<RkbmdPengadaanControl>();
      foreach (RkbmdPengadaanControl dc in list)
      {
        dc.Valid = (dc.Tglvalid != new DateTime());
        ListData.Add(dc);
      }
      return ListData;
    }
    public new HashTableofParameterRow GetFilters()
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(DaftunitLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetAllowEmpty(false));
      hpars.Add(RkbmdKegunitLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetAllowEmpty(false));
      return hpars;
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
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 18, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 40, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmsatuan=Satuan"), typeof(string), 10, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Jmltersedia=Barang yg Dapat Dioptimalisasikan"), typeof(int), 30, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kebutuhan"), typeof(int), 15, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Jumlahusulan=Rencana Pengadaan"), typeof(int), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Jumlahsah=Barang yg Disetujui"), typeof(int), 25, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Statususulan=Status"), typeof(string), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 50, HorizontalAlign.Left));
      return columns;
    }
    public override HashTableofParameterRow GetEntries()
    {
      bool enable = !Valid, enableValid = false;

      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Kdnmaset=Kode Barang"), true, 95).SetEnable(false).SetEditable(false));
      hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Jumlahaset=Jumlah Barang"), true, 30).SetEnable(false).SetEditable(false));
      hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Jumlahrb=Rusak Berat"), true, 30).SetEnable(false).SetEditable(false));
      hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Jmltersedia=Barang yg Dapat Dioptimalisasikan"), true, 30).SetEnable(false).SetEditable(false));
      hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Kebutuhan"), true, 30).SetEnable(enable).SetAllowEmpty(false));
      hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Jumlahusulan=Jml Pengadaan"), true, 30).SetEnable(enable).SetAllowEmpty(false));
      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdsatuan=Satuan"),
      GetList(new SatuanLookupControl()), "Kdsatuan=Nmsatuan", 30).SetAllowRefresh(false).SetEnable(enable).SetAllowEmpty(false));
      hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Ket=Keterangan"), true, 3).SetEnable(enable).SetAllowEmpty(true));
      hpars.Add(new ParameterRowCek(this, true).SetEnable(enableValid).SetEditable(enable));
      return hpars;
    }
    public bool IsValid()
    {
      bool valid = true;
      return valid;
    }
    public new int Update()
    {
      int n = ((BaseDataControlUI)this).Update(BaseDataControl.DEFAULT);
      return n;
    }
    public new int Delete()
    {
      if (Valid)
      {
        throw new Exception("Gagal menghapus data : Status barang ini sudah di sah kan");
      }
      else
      {
        Status = -1;
        int n = ((BaseDataControlUI)this).Delete(BaseDataControl.DEFAULT);
        return n;
      }
    }
    #endregion Methods 
  }
  #endregion RkbmdPengadaan

  #region RkbmdPengadaanValid
  [Serializable]
  public class RkbmdPengadaanValidControl : RkbmdPengadaanControl, IDataControlUIEntry, IHasJSScript
  {
    #region Methods 
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;

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
    public override HashTableofParameterRow GetEntries()
    {
      bool enable = !Valid, enableValid = true;

      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Kdnmaset=Kode Barang"), true, 95).SetEnable(false).SetEditable(false));
      hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Jumlahaset=Jumlah Barang"), true, 30).SetEnable(false).SetEditable(false));
      hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Jumlahrb=Rusak Berat"), true, 30).SetEnable(false).SetEditable(false));
      hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Jmltersedia=Barang yg Dapat Dioptimalisasikan"), true, 30).SetEnable(false).SetEditable(false));
      hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Kebutuhan"), true, 30).SetEnable(false).SetAllowEmpty(false));
      hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Jumlahusulan=Jml Pengadaan"), true, 30).SetEnable(false).SetAllowEmpty(false));
      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdsatuan=Satuan"),
      GetList(new SatuanLookupControl()), "Kdsatuan=Nmsatuan", 30).SetAllowRefresh(false).SetEnable(false).SetAllowEmpty(false));
      hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Ket=Keterangan"), true, 3).SetEnable(false).SetAllowEmpty(true));
      hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Jumlahsah=Barang yg Disetujui"), true, 30).SetEnable(enable).SetAllowEmpty(false));
      hpars.Add(new ParameterRowCek(this, true).SetEnable(enableValid).SetEditable(enable));
      return hpars;
    }
    public new int Update()
    {
      int n = 0;
      if (IsValid())
      {
        if (Jumlahsah == 0)
        {
          throw new Exception("Jumlah barang yang disetujui tidak boleh 0");
        }
        if (Jumlahsah > Jumlahusulan)
        {
          throw new Exception("Jumlah barang yang disetujui tidak boleh melebihi jumlah usulan pengadaan");
        }

        if (Valid)
        {
          Tglvalid = DateTime.Now;
          base.Update("Sah");
        }
        else
        {
          base.Update("Verifikasi");
        }
      }
      return n;
    }
    public new int Delete()
    {
      return ((BaseDataControlUI)this).Update("Draft");
    }
    #endregion Methods 
  }
  #endregion RkbmdPengadaanValid
}

