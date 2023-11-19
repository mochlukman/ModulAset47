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
  #region Usadi.Valid49.BO.DlgRptKibaralControl, Usadi.Valid49.Aset.Rpt
  [Serializable]
  public class DlgRptKibaralControl : KiblainnyaControl, IDataControlUIEntry, IPrintable
  {

    public new ImageCommand[] Cmds
    {
      get
      {
        ImageCommand cmd1 = new ImageCommand()
        {
          CommandName = "LinkPdf",
          Icon = Icon.Printer,
          ToolTip =
          {
            Text = "Klik tombol ini untuk mencetak data"
          }
        };
        return new ImageCommand[] { cmd1 };
      }
    }

    public ImageCommand[] Cmds1
    {
      get
      {
        ImageCommand cmd2 = new ImageCommand()
        {
          CommandName = "LinkExcel",
          Icon = Icon.PageExcel,
          ToolTip =
          {
            Text = "Klik tombol ini untuk export ke excel"
          }
        };
        return new ImageCommand[] { cmd2 };
      }
    }


    public DlgRptKibaralControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLKIBLAINNYADET;
    }

    ViewListProperties cViewListProperties = null;
    public new IProperties GetProperties()
    {
      if (cViewListProperties == null)
      {
        cViewListProperties = (ViewListProperties)base.GetProperties();
        cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
        cViewListProperties.ReadOnlyFields = new string[] { "Asetkey", "Kdaset", "Nmaset", "Unitkey", "Kdunit", "Nmunit", "Tahun", "Tahunsa" };
        cViewListProperties.PageSize = 50;
      }
      return cViewListProperties;
    }

    public string LinkPdf
    {
      get
      {
        string app = GlobalAsp.GetRequestApp();
        string id = GlobalAsp.GetRequestId();
        string idprev = GlobalAsp.GetRequestId();
        string kode = GlobalAsp.GetRequestKode();
        string idx = GlobalAsp.GetRequestIndex();
        string strenable = "&enable=" + ((Status == 0) ? 1 : 0);
        string url = string.Format("Page/PageRpt.aspx?passdc=1&app={0}&i=1&id={1}&idprev={2}&kode={3}&idx={4}&val=0" + strenable, app, id, idprev, kode, idx);
        return ConstantDict.Translate(XMLName) + " " + Noreg + "|" + url;
      }
    }

    public string LinkExcel
    {
      get
      {
        string app = GlobalAsp.GetRequestApp();
        string id = GlobalAsp.GetRequestId();
        string idprev = GlobalAsp.GetRequestId();
        string kode = GlobalAsp.GetRequestKode();
        string idx = GlobalAsp.GetRequestIndex();
        string strenable = "&enable=" + ((Status == 0) ? 1 : 0);
        string url = string.Format("Page/PageRpt.aspx?passdc=1&app={0}&i=1&id={1}&idprev={2}&kode={3}&idx={4}&val=1" + strenable, app, id, idprev, kode, idx);
        return ConstantDict.Translate(XMLName) + " " + Noreg + "|" + url;
      }
    }

    public override DataControlFieldCollection GetColumns()
    {

      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Stricon"), typeof(String), 1, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Stricon"), typeof(CommandColumn), Cmds, 5, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Stricon"), typeof(CommandColumn), Cmds1, 5, HorizontalAlign.Center));

      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 40, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglperolehan=Tanggal Perolehan"), typeof(DateTime), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun"), typeof(int), 15, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreg=No Register"), typeof(string), 15, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai"), typeof(decimal), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Umeko=Masa Pakai"), typeof(decimal), 15, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Asalusul=Asal Usul"), typeof(string), 25, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Pengguna"), typeof(string), 40, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmkon=Kondisi"), typeof(string), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmhak=Hak"), typeof(string), 25, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Luaslt=Luas Lantai"), typeof(decimal), 15, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Luastnh=Luas Tanah"), typeof(decimal), 15, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nodokgdg=Nomor Dokumen"), typeof(string), 20, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tgdokgdg=Tanggal Dokumen"), typeof(DateTime), 20, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Alamat"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 100, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Lokasi=Lokasi"), typeof(string), 100, HorizontalAlign.Left));

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

    public new HashTableofParameterRow GetFilters()
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(DaftasetKibLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enableFilter).SetAllowEmpty(false));
      hpars.Add(DaftunitLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetAllowEmpty(false));

      return hpars;
    }

    public string GetURLReport(int n)
    {
      Hashtable CS = new Hashtable();
      CS[ReportUtils.SQLINSTANCE] = GlobalAsp.GetDataSource();
      CS[ReportUtils.DBNAME] = (string)((IDataControlAppuser)GlobalAsp.GetSessionUser()).GetValue("DBName");
      CS[ReportUtils.DBUSER] = SQLDataSource.GetUserDB();
      CS[ReportUtils.DBPASSWORD] = SQLDataSource.GetPwdDB();
      CS[ReportUtils.PATH] = "Report/";
      CS[ReportUtils.RPTLIB] = "Usadi.Valid49.Aset.Rpt.dll";
      CS[ReportUtils.RPTNAME] = "KIBARAL.rpt";
      CS[ReportUtils.PDFNAME] = "KIBARAL_" + (string)GlobalAsp.GetSessionUser().GetValue("Userid") + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";

      Hashtable Params = new Hashtable();
      Params["@unitkey"] = Unitkey;
      Params["@asetkey"] = Asetkey;
      Params["@tahun"] = Tahun;
      Params["@noreg"] = Noreg;

      string pdfurl;
      if (n == 0)
      {
        pdfurl = ReportUtils.ExportPDF("Usadi.Valid49.Aset.Rpt.Rpt.KIBARAL.rpt", CS, Params);
      }
      else
      {
        CS[ReportUtils.PDFNAME] = "KIBARAL_" + (string)GlobalAsp.GetSessionUser().GetValue("Userid") + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        pdfurl = ReportUtils.ExportExcel("Usadi.Valid49.Aset.Rpt.Rpt.KIBARAL.rpt", CS, Params);
      }
      return pdfurl;
    }


    public new void SetPageKey()
    {
      Kdkib = ConstantTablesAsetMAT.KDKIBLAINNYA;
    }

    public Hashtable GetDocProperties()
    {
      return new Hashtable();
    }

    public List<RptPars> GetReports()
    {
      return new List<RptPars>();
    }

    public void Print(int n)
    {
    }

  }
  #endregion Kibaral
}

