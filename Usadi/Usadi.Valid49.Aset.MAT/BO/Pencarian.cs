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
  #region Usadi.Valid49.BO.PencarianControl, Usadi.Valid49.Aset.MAT
  [Serializable]
  public class PencarianControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
  {
    #region Properties
    public long Id { get; set; }
    public string Idbrg { get; set; }
    public string Unitkey { get; set; }
    public string Kdunit { get; set; }
    public string Nmunit { get; set; }
    public string Asetkey { get; set; }
    public string Kdaset { get; set; }
    public string Nmaset { get; set; }
    public DateTime Tglperolehan { get; set; }
    public string Noreg { get; set; }
    public decimal Nilai { get; set; }
    public decimal Umeko { get; set; }
    public string Kdpemilik { get; set; }
    public string Nmpemilik { get; set; }
    public string Kdkon { get; set; }
    public string Nmkon { get; set; }
    public string Asalusul { get; set; }
    public string Pengguna { get; set; }
    public string Kdsatuan { get; set; }
    public string Nmsatuan { get; set; }
    public string Spesifikasi { get; set; }
    public string Ukuran { get; set; }
    public string Bahan { get; set; }
    public string Nosertifikat { get; set; }
    public string Alamat { get; set; }
    public new string Ket { get; set; }
    public string Kdklas { get; set; }
    public string Uraiklas { get; set; }
    public string Kdstatusaset { get; set; }
    public string Kdkib { get; set; }
    public string Nmkib { get; set; }
    public string Keywords { get; set; }
    public ImageCommand[] Cmds
    {
      get
      {
        ImageCommand cmd1 = new ImageCommand()
        {
          CommandName = "ViewPencariandet",
          Icon = Icon.PageCopy
        };
        cmd1.ToolTip.Text = "Klik Untuk Menampilkan Rincian Transaksi";
        return new ImageCommand[] { cmd1 };
      }
    }

    public string ViewPencariandet
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
        return "" + Nmaset + "; Tahun " + Tahun + "; Register " + Noreg + ":" + url;
      }
    }

    #endregion Properties 

    #region Methods
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { };
      cViewListProperties.IDKey = "Id";
      cViewListProperties.IDProperty = "Id";
      cViewListProperties.ReadOnlyFields = new String[] { "Kdkib", "Keywords" };
      cViewListProperties.SortFields = new String[] { "Kdaset", "Tahun", "Noreg", "Nilai" };
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      cViewListProperties.PageSize = 20;
      cViewListProperties.RefreshParent = true;
      return cViewListProperties;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Stricon"), typeof(CommandColumn), Cmds, 5, HorizontalAlign.Center));

      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 35, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglperolehan=Tanggal Perolehan"), typeof(DateTime), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun"), typeof(int), 15, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreg=No Register"), typeof(string), 15, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai=Nilai Perolehan"), typeof(decimal), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Umeko=Masa Pakai"), typeof(decimal), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmpemilik=Pemilik"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmkon=Kondisi"), typeof(string), 15, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmsatuan=Satuan"), typeof(string), 15, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Asalusul=Asal Usul"), typeof(string), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Pengguna"), typeof(string), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Spesifikasi"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nosertifikat"), typeof(string), 35, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ukuran"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Bahan"), typeof(string), 15, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Alamat"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 100, HorizontalAlign.Left));

      return columns;
    }
    public new HashTableofParameterRow GetFilters()
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdkib=Jenis KIB"),
      GetList(new JnskibLookupControl()), "Kdkib=Nmkib", 60).SetAllowRefresh(true).SetEnable(enableFilter).SetAllowEmpty(false));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Keywords=Kata Kunci"), true, 60).SetEnable(true).SetAllowEmpty(false));
      return hpars;
    }

    public new IList View()
    {
      string sql = @"
        exec [dbo].[REGISTER_PENCARIAN]
		    @KDKIB = N'{0}',
		    @KEYWORDS = N'{1}'
      ";

      sql = string.Format(sql, Kdkib, Keywords);
      string[] fields = new string[] { "Id", "Idbrg", "Unitkey", "Kdunit", "Nmunit", "Asetkey", "Kdaset", "Nmaset", "Tglperolehan"
        , "Tahun", "Noreg", "Nilai", "Umeko", "Kdpemilik", "Nmpemilik", "Kdkon", "Nmkon", "Asalusul", "Pengguna", "Kdsatuan", "Nmsatuan"
        , "Spesifikasi", "Ukuran", "Bahan", "Nosertifikat", "Alamat", "Ket", "Kdklas", "Uraiklas", "Kdstatusaset", "Kdkib", "Nmkib"  };
      List<IDataControl> list = BaseDataAdapter.GetListDC(this, sql, fields);
      List<PencarianControl> ListData = new List<PencarianControl>();

      foreach (PencarianControl dc in list)
      {
        ListData.Add(dc);
      }
      return ListData;
    }
    #endregion Methods 
  }
  #endregion Pencarian
}

