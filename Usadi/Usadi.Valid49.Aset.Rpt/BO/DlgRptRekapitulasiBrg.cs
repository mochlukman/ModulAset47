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
  #region Usadi.Valid49.BO.DlgRptRekapitulasiBrgControl, Usadi.Valid49.Aset.Rpt
  [Serializable]
  public class DlgRptRekapitulasiBrgControl : DaftunitControl, IDataControlUIEntry, IPrintable
  {
    #region Property Subunit
    private string _Subunit;
    public string Subunit
    {
      get { return _Subunit; }
      set { _Subunit = value; }
    }
    #endregion
    #region Property Kdpar
    private string _Kdpar;
    public string Kdpar
    {
      get { return _Kdpar; }
      set { _Kdpar = value; }
    }
    #endregion
    #region Property Nmkib
    private string _Nmkib;
    public string Nmkib
    {
      get { return _Nmkib; }
      set { _Nmkib = value; }
    }
    #endregion
    #region Property Kdkib
    private string _Kdkib;
    public string Kdkib
    {
      get { return _Kdkib; }
      set { _Kdkib = value; }
    }
    #endregion
    #region Property Tglawal
    private DateTime _Tglawal;
    public DateTime Tglawal
    {
      get { return _Tglawal; }
      set { _Tglawal = value; }
    }
    #endregion
    #region Property Tglakhir
    private DateTime _Tglakhir;
    public DateTime Tglakhir
    {
      get { return _Tglakhir; }
      set { _Tglakhir = value; }
    }
    #endregion
    #region Property Kelaset
    private string _Kelaset;
    public string Kelaset
    {
      get { return _Kelaset; }
      set { _Kelaset = value; }
    }
    #endregion
    #region Property Kdkel
    private string _Kdkel;
    public string Kdkel
    {
      get { return _Kdkel; }
      set { _Kdkel = value; }
    }
    #endregion
    public string Golkib { get; set; }
    public string Kdklas { get; set; }
    public string Uraiklas { get; set; }
    public DlgRptRekapitulasiBrgControl()
    {
      XMLName = ConstantTablesAsetDM.XMLDAFTUNIT;
      SetDefaultDate(RANGE_MONTH);
      Kdklas = "01";
      Subunit = "1";
      Tglawal = Convert.ToDateTime("01/01/1900");
      Kelaset = "11";
    }

    ViewListProperties cViewListProperties = null;
    public new IProperties GetProperties()
    {
      if (cViewListProperties == null)
      {
        cViewListProperties = (ViewListProperties)base.GetProperties();
        cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
        cViewListProperties.ReadOnlyFields = new string[] { };
        cViewListProperties.ModeToolbar = ViewListProperties.MODE_TOOLBAR_PRINT;
      }
      return cViewListProperties;
    }

    public ImageCommand[] Cmds
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
            Text = "Klik tombol ini untuk mencetak data"
          }
        };
        return new ImageCommand[] { cmd2 };
      }
    }


    public override HashTableofParameterRow GetEntries()
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(DaftunitLookupControl.Instance.GetLookupParameterRow(this, false));
      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdklas=Kelas Aset"),
        GetList(new JklasRptLookupControl()), "Kdklas=Uraiklas", 54).SetEnable(enableFilter));
      hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglawal=Tanggal Awal"), true).SetEnable(true));
      hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglakhir=Tanggal Akhir"), true).SetEnable(true));
      //hpars.Add(new ParameterRowDate(this, ParameterRow.MODE_DATE).SetEnable(enableFilter));
      ArrayList list = new ArrayList(new ParamControl[] {
         new ParamControl() { Kdpar="1",Nmpar="Ya"}
        ,new ParamControl() { Kdpar="0",Nmpar="Tidak"}
      });
      hpars.Add(new ParameterRow(ConstantDict.GetColumnTitleEntry("Subunit=Subunit"), ParameterRow.MODE_SELECT,
        list, "Kdpar=Nmpar", 26).SetEnable(enableFilter));

      return hpars;
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
      return hpars;
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
        return ConstantDict.Translate(XMLName) + " " + Kdunit + "|" + url;
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
        string url = string.Format("Page/PageRpt.aspx?passdc=1&app={0}&i=1&id={1}&idprev={2}&kode={3}&idx={4}&val=0" + strenable, app, id, idprev, kode, idx);
        return ConstantDict.Translate(XMLName) + " " + Kdunit + "|" + url;
      }
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
      CS[ReportUtils.RPTNAME] = "RekapBrgSKPD.rpt";
      CS[ReportUtils.PDFNAME] = "RekapBrgSKPD_" + (string)GlobalAsp.GetSessionUser().GetValue("Userid") + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";


      Hashtable Params = new Hashtable();
      Params["@unitkey"] = Unitkey;
      Params["@Kdklas"] = Kdklas;
      Params["@tglawal"] = Tglawal; //Tgl1;   //Tglawal;
      Params["@tglakhir"] = Tglakhir; //Tgl2; //Tglakhir;
      Params["@subunit"] = Subunit;


      //string pdfurl = ReportUtils.ExportPDF("Usadi.Valid49.Aset.Rpt.Rpt.PENGADAAN.rpt", CS, Params);

      string pdfurl;
      if (n == 0)
      {
        pdfurl = ReportUtils.ExportPDF("Usadi.Valid49.Aset.Rpt.Rpt.RekapBrgSKPD.rpt", CS, Params);
      }
      else
      {
        CS[ReportUtils.PDFNAME] = "RekapBrgSKPD_" + (string)GlobalAsp.GetSessionUser().GetValue("Userid") + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        pdfurl = ReportUtils.ExportExcel("Usadi.Valid49.Aset.Rpt.Rpt.RekapBrgSKPD.rpt", CS, Params);
      }

      return pdfurl;

    }

    public new void SetPageKey()
    {
    }

    public Hashtable GetDocProperties()
    {
      return new Hashtable();
    }

    public List<RptPars> GetReports()
    {
      //RptPars rptPar = new RptPars() { Title = "Pengadaan BMD dari APBD", RptClass = LinkPdf };
      //return new List<RptPars>(new RptPars[] { rptPar });
      RptPars rptPar = new RptPars() { Title = "Pdf", RptClass = LinkPdf };
      RptPars rptPar1 = new RptPars() { Title = "Excel", RptClass = LinkExcel };
      return new List<RptPars>(new RptPars[] { rptPar, rptPar1 });
    }

    public void Print(int n)
    {
    }
  }
  #endregion DlgRptRekapitulasiBrg
}

