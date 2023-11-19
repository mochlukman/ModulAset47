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
  #region Usadi.Valid49.BO.UsulanhapusControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class UsulanhapusControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
  {
    #region Properties
    public long Id { get; set; }
    public string Kdunit { get; set; }
    public string Nmunit { get; set; }
    public string Kdtans { get; set; }
    public DateTime Tglusulan { get; set; }
    public DateTime Tglvalid { get; set; }
    public new string Ket { get; set; }
    public int Jmldata { get; set; }
    public int Jmlkib { get; set; }
    public int Tahunsa { get; set; }
    public DateTime Tglsa { get; set; }
    public string Unitkey { get; set; }
    public string Nousulan { get; set; }
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
          CommandName = "ViewUsulanhapusdet",
          Icon = Icon.PageCopy
        };
        cmd1.ToolTip.Text = "Klik Untuk Menampilkan Rincian Reklas";
        return new ImageCommand[] { cmd1 };
      }
    }
    public string ViewUsulanhapusdet
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
        return "No. BA Reklas; " + Nousulan + ":" + url;
      }
    }
    #endregion Properties 

    #region Methods 
    public UsulanhapusControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLUSULANHAPUS;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Nousulan" };
      cViewListProperties.IDKey = "Id";
      cViewListProperties.IDProperty = "Id";
      cViewListProperties.ReadOnlyFields = new String[] { "Unitkey", "Kdunit", "Nmunit" };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
      cViewListProperties.PageSize = 20;

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

      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nousulan=Nomor Dokumen"), typeof(string), 40, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglusulan=Tanggal Dokumen"), typeof(DateTime), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglvalid=Tanggal Pengesahan"), typeof(DateTime), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 50, HorizontalAlign.Left));

      return columns;
    }
    public new void SetPageKey()
    {
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
      Tglusulan = Tglsa;
    }
    public new HashTableofParameterRow GetFilters()
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(DaftunitLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetAllowEmpty(false));

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
      List<UsulanhapusControl> ListData = new List<UsulanhapusControl>();
      foreach (UsulanhapusControl dc in list)
      {
        dc.Valid = (dc.Tglvalid != new DateTime());
        ListData.Add(dc);
      }

      return ListData;
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

      if (Tglusulan.Year.ToString().Trim() != cPemda.Configval.Trim())
      {
        throw new Exception("Gagal menyimpan data : Transaksi reklas hanya untuk tahun anggaran berjalan, cek kembali tanggal reklas");
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
        if (Tglusulan.Year.ToString().Trim() != cPemda.Configval.Trim())
        {
          throw new Exception("Gagal menyimpan data : Transaksi Koreksi hanya untuk tahun anggaran berjalan.");
        }
        else
        {
          n = ((BaseDataControlUI)this).Update();

          if (Valid)
          {
            UsulanhapusControl cUsulanGetjmlkib = new UsulanhapusControl();
            cUsulanGetjmlkib.Unitkey = Unitkey;
            cUsulanGetjmlkib.Nousulan = Nousulan;
            cUsulanGetjmlkib.Kdtans = Kdtans;
            cUsulanGetjmlkib.Load("Jmlkib");

            if (cUsulanGetjmlkib.Jmlkib >= 1)
            {
              throw new Exception("Gagal mengubah data : Dokumen usulan ini sudah di sahkan dan masuk ke KIB");
            }
            else
            {
              string sql = @"
              exec [dbo].[WSPX_USULANHAPUS]
              @UNITKEY = N'{0}',
              @NOUSULAN = N'{1}',
              @TGLUSULAN = N'{2}',
              @KDTANS = N'{3}'
              ";
              sql = string.Format(sql, Unitkey, Nousulan, Tglusulan.ToString("yyyy-MM-dd"), Kdtans);
              BaseDataAdapter.ExecuteCmd(this, sql);
            }
          }
        }
      }
      return n;
    }
    public new int Delete()
    {
      int n = 0;
      if (Valid)
      {
        //if (cekjmlkib == true)
        //{
        //  throw new Exception("Koreksi sudah masuk ke KIB, pengesahan tidak dapat dicabut");
        //}
        string sql = @"
              exec [dbo].[WSP_DEL_USULANHAPUS]
              @UNITKEY = N'{0}',
              @NOUSULAN = N'{1}',
              @KDTANS = N'{2}'
              ";

        sql = string.Format(sql, Unitkey, Nousulan, Kdtans);
        BaseDataAdapter.ExecuteCmd(this, sql);

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
    public override HashTableofParameterRow GetEntries()
    {
      bool enable = !Valid, enableValid = false;

      UsulanhapusControl cUsulanGetjmldata = new UsulanhapusControl();
      cUsulanGetjmldata.Unitkey = Unitkey;
      cUsulanGetjmldata.Nousulan = Nousulan;
      cUsulanGetjmldata.Load("Jmldata");

      if (cUsulanGetjmldata.Jmldata != 0)
      {
        enableValid = true;
      }

      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nousulan=Nomor Dokumen"), false, 50).SetEnable(enable));
      hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglusulan=Tgl Dokumen"), true).SetEnable(enable));
      hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Ket=Keterangan"), true, 3).SetEnable(enable).SetAllowEmpty(true));
      hpars.Add(new ParameterRowCek(this, true).SetEnable(enableValid).SetEditable(enable));
      return hpars;
    }

    #endregion Methods 
  }
  #endregion Usulanhapus

  #region UsulanhapusRb
  [Serializable]
  public class UsulanhapusRbControl : UsulanhapusControl, IDataControlUIEntry
  {
    public new void SetPageKey()
    {
      Kdtans = "307";
    }
  }
  #endregion UsulanhapusRb

  #region UsulanhapusNonop
  [Serializable]
  public class UsulanhapusNonopControl : UsulanhapusControl, IDataControlUIEntry
  {
    public new void SetPageKey()
    {
      Kdtans = "310";
    }
  }
  #endregion UsulanhapusNonop

}

