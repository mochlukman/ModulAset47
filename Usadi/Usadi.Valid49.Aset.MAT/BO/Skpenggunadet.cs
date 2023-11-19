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
  #region Usadi.Valid49.BO.SkpenggunadetControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class SkpenggunadetControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
  {
    #region Properties
    public long Id { get; set; }
    public DateTime Tglskpengguna { get; set; }
    public DateTime Tgldokumen { get; set; }
    public DateTime Tglvalid { get; set; }
    public int Statusdok { get; set; }
    public string Statusdokumen { get; set; }
    public new string Ket { get; set; }
    public string Nmpengguna { get; set; }
    public string Kdunit { get; set; }
    public string Nmunit { get; set; }
    public string Kdunit2 { get; set; }
    public string Nmunit2 { get; set; }
    public int Jmlbrg { get; set; }
    public int Jmlkib { get; set; }
    public int Tahunsa { get; set; }
    public DateTime Tglsa { get; set; }
    public string Unitkey { get; set; }
    public string Noskpengguna { get; set; }
    public string Nodokumen { get; set; }
    public string Unitkey2 { get; set; }
    public string Blokid { get; set; }
    public ImageCommand[] Cmds
    {
      get
      {
        ImageCommand cmd1 = new ImageCommand()
        {
          CommandName = "ViewSkpenggunadetbrg",
          Icon = Icon.PageCopy
        };
        cmd1.ToolTip.Text = "Klik Untuk Menampilkan Kode Barang";
        return new ImageCommand[] { cmd1 };
      }
    }
    public string ViewSkpenggunadetbrg
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
        return "Rincian Kode Barang SKPD Pengguna; " + Kdunit2 + " - " + Nmunit2 + ":" + url;
      }
    }
    #endregion Properties 

    #region Methods 
    public SkpenggunadetControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLSKPENGGUNADET;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Noskpengguna", "Nodokumen", "Unitkey2" };
      cViewListProperties.IDKey = "Id";
      cViewListProperties.IDProperty = "Id";
      cViewListProperties.ReadOnlyFields = new String[] { "Unitkey", "Kdunit", "Nmunit", "Noskpengguna" };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
      cViewListProperties.PageSize = 20;
      cViewListProperties.RefreshFilter = true;
      cViewListProperties.RefreshParent = true;

            //if (Blokid == "1")
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
      bool enable = true;

      if (Blokid == "1")
      {
        enable = false;
      }

      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center).SetVisible(enable));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Stricon"), typeof(CommandColumn), Cmds, 5, HorizontalAlign.Center));

      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nodokumen=Nomor Dokumen"), typeof(string), 35, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmpengguna=SKPD Pengguna"), typeof(string), 65, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tgldokumen=Tanggal Dokumen"), typeof(DateTime), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Statusdokumen=Status"), typeof(string), 15, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglvalid=Tanggal Pengesahan"), typeof(DateTime), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 100, HorizontalAlign.Left));
      return columns;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      if (typeof(IDataControlMenu).IsInstanceOfType(bo))
      {
        Last_by = ((BaseBO)bo).Userid;
      }
      else if (typeof(SkpenggunaControl).IsInstanceOfType(bo))
      {
        Unitkey = ((SkpenggunaControl)bo).Unitkey;
        Kdunit = ((SkpenggunaControl)bo).Kdunit;
        Nmunit = ((SkpenggunaControl)bo).Nmunit;
        Noskpengguna = ((SkpenggunaControl)bo).Noskpengguna;
        Tglskpengguna = ((SkpenggunaControl)bo).Tglskpengguna;
        Blokid = ((SkpenggunaControl)bo).Blokid;
      }
    }
    public new void SetPrimaryKey()
    {
      PemdaControl cPemda = new PemdaControl();
      cPemda.Configid = "cur_thang";
      cPemda.Load("PK");

      Tahunsa = (Int32.Parse(cPemda.Configval));
      Tglsa = new DateTime(Tahunsa, DateTime.Today.Month, DateTime.Today.Day);
      Tgldokumen = Tglsa;
      Unitkey2 = Unitkey;
      Kdunit2 = Kdunit;
      Nmunit2 = Nmunit;
    }
    public new IList View()
    {
      IList list = this.View(BaseDataControl.ALL);
      return list;
    }
    public new IList View(string label)
    {
      IList list = ((BaseDataControl)this).View(label);
      List<SkpenggunadetControl> ListData = new List<SkpenggunadetControl>();
      foreach (SkpenggunadetControl dc in list)
      {
        dc.Valid = (dc.Tglvalid != new DateTime());
        ListData.Add(dc);
      }

      return ListData;
    }
    public bool IsValid()
    {
      bool valid = true;
      return valid;
    }
    public new void Insert()
    {
      PemdaControl cPemda = new PemdaControl();
      cPemda.Configid = "cur_thang";
      cPemda.Load("PK");

      if (Tgldokumen.Year.ToString().Trim() != cPemda.Configval.Trim())
      {
        throw new Exception("Gagal menyimpan data : tanggal dokumen hanya untuk tahun anggaran berjalan");
      }

      try
      {
        base.Insert();
      }
      catch (Exception ex)
      {
        if (ex.Message.Contains("Violation of PRIMARY KEY"))
        {
          string msg = "Gagal menambah data : nomor dokumen dengan SKPD Pengguna yang di entrikan sudah ada";
          msg = string.Format(msg);
          throw new Exception(msg);
        }
      }
    }
    public new int Update()
    {
      PemdaControl cPemda = new PemdaControl();
      cPemda.Configid = "cur_thang";
      cPemda.Load("PK");

      int n = 0;
      if (IsValid())
      {
        if (Tgldokumen.Year.ToString().Trim() != cPemda.Configval.Trim())
        {
          throw new Exception("Transaksi penetapan pengguna hanya untuk tahun anggaran berjalan, cek kembali tanggal dokumen");
        }
        else
        {
          base.Update();
        }

        if (Valid)
        {
          SkpenggunadetControl cSkpenggunadetCekjmlkib = new SkpenggunadetControl();
          cSkpenggunadetCekjmlkib.Noskpengguna = Noskpengguna;
          cSkpenggunadetCekjmlkib.Nodokumen = Nodokumen;
          cSkpenggunadetCekjmlkib.Load("Jmlkib");

          if (cSkpenggunadetCekjmlkib.Jmlkib >= 1)
          {
            throw new Exception("Gagal mengubah data : Dokumen SK pengguna ini sudah di sahkan dan masuk ke KIB");
          }
          else
          {
            //if (Statusdok == 1) //status diterima
            //{
              string sql = @"
              exec [dbo].[WSP_VALSKPENGGUNADET]
              @UNITKEY = N'{0}',
              @NOSKPENGGUNA = N'{1}',
              @TGLSKPENGGUNA = N'{2}',
              @NODOKUMEN = N'{3}',
              @TGLDOKUMEN = N'{4}',
              @UNITKEY2 = N'{5}'
              ";
              sql = string.Format(sql, Unitkey, Noskpengguna, Tglskpengguna.ToString("yyyy-MM-dd"), Nodokumen, Tgldokumen.ToString("yyyy-MM-dd"), Unitkey2);
              BaseDataAdapter.ExecuteCmd(this, sql);

              base.Update("Status");
            //}
            //else if (Statusdok == 0) //status diajukan
            //{
            //  throw new Exception("Gagal menyimpan data: status pengajuan belum ditetapkan");
            //}
            //else //status ditolak
            //{
            //  Tglvalid = Tgldokumen;
            //  base.Update("Status");
            //  base.Update("Sah");
            //}
          }
        }
        else
        {
          base.Update("Status");
        }

      }
      return n;
    }
    public new int Delete()
    {
      int n = 0;

      //bool cekjmlkib = false;

      //SkpenggunadetControl cSkpenggunadetCekjmlkib = new SkpenggunadetControl();
      //cSkpenggunadetCekjmlkib.Noskpengguna = Noskpengguna;
      //cSkpenggunadetCekjmlkib.Nodokumen = Nodokumen;
      //cSkpenggunadetCekjmlkib.Load("Jmlkib");
      //Jmlkib = cSkpenggunadetCekjmlkib.Jmlkib;

      //cekjmlkib = (Jmlkib >= 1);

      if (Valid)
      {
        //if (cekjmlkib == true)
        //{
        //  throw new Exception("Aset sudah masuk ke KIB SKPD pengguna, pengesahan tidak dapat dicabut");
        //}
        string sql = @"
              exec [dbo].[WSP_DEL_SKPENGGUNADET]
              @UNITKEY = N'{0}',
              @NOSKPENGGUNA = N'{1}',
              @NODOKUMEN = N'{2}',
              @UNITKEY2 = N'{3}'
              ";
        sql = string.Format(sql, Unitkey, Noskpengguna, Nodokumen, Unitkey2);
        BaseDataAdapter.ExecuteCmd(this, sql);

        return ((BaseDataControlUI)this).Update("Draft");
      }
      else
      {
        Status = -1;
        base.Delete("Detail");
        base.Delete();

        //try
        //{
        //  Status = -1;
        //  base.Delete();
        //}
        //catch (Exception ex)
        //{
        //  if (ex.Message.Contains("REFERENCE"))
        //  {
        //    string msg = "Hapus rincian barang terlebih dahulu";
        //    msg = string.Format(msg);
        //    throw new Exception(msg);
        //  }
        //}
      }

      //if (Valid)
      //{
      //  {
      //    throw new Exception("Gagal menghapus data: Status dokumen ini sudah disahkan/diverifikasi");
      //  }
      //}
      //else
      //{
      //  try
      //  {
      //    Status = -1;
      //    base.Delete();
      //  }
      //  catch (Exception ex)
      //  {
      //    if (ex.Message.Contains("REFERENCE"))
      //    {
      //      string msg = "Hapus rincian barang terlebih dahulu";
      //      msg = string.Format(msg);
      //      throw new Exception(msg);
      //    }
      //  }
      //}
      return n;
    }
    public override HashTableofParameterRow GetEntries()
    {
      bool enable = !Valid, enableValid = false;

      SkpenggunadetControl cSkpenggunadetCekjmlbrg = new SkpenggunadetControl();
      cSkpenggunadetCekjmlbrg.Unitkey = Unitkey;
      cSkpenggunadetCekjmlbrg.Noskpengguna = Noskpengguna;
      cSkpenggunadetCekjmlbrg.Nodokumen = Nodokumen;
      cSkpenggunadetCekjmlbrg.Unitkey2 = Unitkey2;
      cSkpenggunadetCekjmlbrg.Load("Jmlbrg");
      Jmlbrg = cSkpenggunadetCekjmlbrg.Jmlbrg;

      if (Jmlbrg != 0) //cek jumlah barang untuk validasi
      {
        enableValid = true;
      }

      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(DaftunitSkpenggunaLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(enable).SetEditable(false).SetAllowRefresh(true).SetAllowEmpty(false));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nodokumen=No Dokumen"), false, 50).SetEnable(enable).SetEditable(false)
        .SetAllowEmpty(false));
      hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tgldokumen=Tgl Dokumen"), true).SetEnable(enable).SetEditable(enable)
        .SetAllowEmpty(false));
      hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Ket=Keterangan"), true, 3).SetEnable(enable).SetAllowEmpty(true));
      // ArrayList list = new ArrayList(new ParamControl[] {
      //  new ParamControl() {  Kdpar="0",Nmpar="Diajukan "}
      //  ,new ParamControl() { Kdpar="1",Nmpar="Diterima "}
      //  ,new ParamControl() { Kdpar="2",Nmpar="Ditolak "}
      //});
            //hpars.Add(new ParameterRow(ConstantDict.GetColumnTitleEntry("Statusdok=Status"), ParameterRow.MODE_SELECT,
            //  list, "Kdpar=Nmpar", 30).SetAllowRefresh(true).SetEnable(enableValid).SetEditable(true));
            //hpars.Add(new ParameterRowCek(this, true).SetEnable(enableValid).SetEditable(enable));
      return hpars;
    }

    #endregion Methods 
  }
  #endregion Skpenggunadet

  #region SkpenggunadetValidControl
  [Serializable]
  public class SkpenggunadetValidControl : SkpenggunadetControl, IDataControlUIEntry, IHasJSScript
  {
    #region Methods
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Noskpengguna", "Nodokumen", "Unitkey2" };
      cViewListProperties.IDKey = "Id";
      cViewListProperties.IDProperty = "Id";
      cViewListProperties.ReadOnlyFields = new String[] { "Unitkey", "Noskpengguna" };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
      cViewListProperties.PageSize = 20;
      cViewListProperties.AllowMultiDelete = true;

      if (Blokid == "1")
      {
        cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      }
      else
      {
        cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_DEL;
        cViewListProperties.AllowMultiDelete = true;
      }

      return cViewListProperties;
    }
    public override HashTableofParameterRow GetEntries()
    {
      bool enable = !Valid, enableValid = false;

      SkpenggunadetValidControl cSkpenggunadetCekjmlbrg = new SkpenggunadetValidControl();
      cSkpenggunadetCekjmlbrg.Unitkey = Unitkey;
      cSkpenggunadetCekjmlbrg.Noskpengguna = Noskpengguna;
      cSkpenggunadetCekjmlbrg.Nodokumen = Nodokumen;
      cSkpenggunadetCekjmlbrg.Unitkey2 = Unitkey2;
      cSkpenggunadetCekjmlbrg.Load("Jmlbrg");
      Jmlbrg = cSkpenggunadetCekjmlbrg.Jmlbrg;

      if (Jmlbrg != 0) //cek jumlah barang untuk validasi
      {
        enableValid = true;
      }

      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(DaftunitSkpenggunaLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(enable).SetEditable(false).SetAllowRefresh(true));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nodokumen=No Dokumen"), false, 50).SetEnable(enable).SetEditable(false)
        .SetAllowEmpty(false));
      hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tgldokumen=Tgl Dokumen"), true).SetEnable(enable).SetEditable(enable)
        .SetAllowEmpty(false));
      hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Ket=Keterangan"), true, 3).SetEnable(enable).SetAllowEmpty(true));
            ArrayList list = new ArrayList(new ParamControl[] {
        new ParamControl() { Kdpar="1",Nmpar="Diterima "}
        ,new ParamControl() { Kdpar="2",Nmpar="Ditolak "}
      });
            hpars.Add(new ParameterRow(ConstantDict.GetColumnTitleEntry("Statusdok=Status"), ParameterRow.MODE_SELECT,
              list, "Kdpar=Nmpar", 30).SetAllowRefresh(true).SetEnable(enable).SetEditable(true));
            hpars.Add(new ParameterRowCek(this, true).SetEnable(enableValid).SetEditable(enable));
      return hpars;
    }
    public new int Update()
    {
      int n = 0;
      if (IsValid())
      {
        if (Valid)
        {
          SkpenggunadetControl cSkpenggunadetCekjmlkib = new SkpenggunadetControl();
          cSkpenggunadetCekjmlkib.Noskpengguna = Noskpengguna;
          cSkpenggunadetCekjmlkib.Nodokumen = Nodokumen;
          cSkpenggunadetCekjmlkib.Load("Jmlkib");

          if (cSkpenggunadetCekjmlkib.Jmlkib >= 1)
          {
            throw new Exception("Gagal mengubah data : Dokumen SK pengguna ini sudah di sahkan dan masuk ke KIB");
          }
          else
          {
            if (Statusdok == 1) //status diterima
            {
              string sql = @"
              exec [dbo].[WSP_VALSKPENGGUNADET]
              @UNITKEY = N'{0}',
              @NOSKPENGGUNA = N'{1}',
              @TGLSKPENGGUNA = N'{2}',
              @NODOKUMEN = N'{3}',
              @TGLDOKUMEN = N'{4}',
              @UNITKEY2 = N'{5}'
              ";
              sql = string.Format(sql, Unitkey, Noskpengguna, Tglskpengguna.ToString("yyyy-MM-dd"), Nodokumen, Tgldokumen.ToString("yyyy-MM-dd"), Unitkey2);
              BaseDataAdapter.ExecuteCmd(this, sql);

              base.Update("Status");
            }
            else if (Statusdok == 0) //status diajukan
            {
              throw new Exception("Gagal menyimpan data: status pengajuan belum ditetapkan");
            }
            else //status ditolak
            {
              Tglvalid = Tgldokumen;
              base.Update("Status");
              base.Update("Sah");
            }
          }
        }
        else
        {
          base.Update("Status");
        }
      }
      return n;
    }
    public new int Delete()
    {
      bool cekjmlkib = false;

      SkpenggunadetControl cSkpenggunadetCekjmlkib = new SkpenggunadetControl();
      cSkpenggunadetCekjmlkib.Noskpengguna = Noskpengguna;
      cSkpenggunadetCekjmlkib.Nodokumen = Nodokumen;
      cSkpenggunadetCekjmlkib.Load("Jmlkib");
      Jmlkib = cSkpenggunadetCekjmlkib.Jmlkib;

      cekjmlkib = (Jmlkib >= 1);

      if (Valid)
      {
        if (cekjmlkib == true)
        {
          throw new Exception("Aset sudah masuk ke KIB SKPD pengguna, pengesahan tidak dapat dicabut");
        }

        return ((BaseDataControlUI)this).Update("Draft");
      }
      else
      {
        throw new Exception("Hapus data hanya bisa dilakukan di menu usulan");
      }
    }
    #endregion Methods 
  }
  #endregion SkpenggunadetValidControl
}

