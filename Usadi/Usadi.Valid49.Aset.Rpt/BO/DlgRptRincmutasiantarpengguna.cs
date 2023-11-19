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
  #region Usadi.Valid49.BO.DlgRptRincmutasiantarpenggunaControl, Usadi.Valid49.Aset.Rpt
  [Serializable]
  public class DlgRptRincmutasiantarpenggunaControl : DaftunitControl, IDataControlUIEntry, IPrintable
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
    #region Property Kdklas
    private string _Kdklas;
    public string Kdklas
    {
      get { return _Kdklas; }
      set { _Kdklas = value; }
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

    public DlgRptRincmutasiantarpenggunaControl()
    {
      PemdaControl cPemda = new PemdaControl();
      cPemda.Configid = "cur_thang";
      cPemda.Load("PK");

      XMLName = ConstantTablesAsetDM.XMLDAFTUNIT;
      Subunit = "1";
      Kdklas = "01";
      Tglawal = new DateTime(1900, 1, 1);
      Tglakhir = new DateTime((Int32.Parse(cPemda.Configval)), 12, 31);
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
      CS[ReportUtils.RPTNAME] = "RINCMUTASIANTARPENGGUNA.rpt";
      CS[ReportUtils.PDFNAME] = "RINCMUTASIANTARPENGGUNA_" + (string)GlobalAsp.GetSessionUser().GetValue("Userid") + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";

      Hashtable Params = new Hashtable();
      Params["@unitkey"] = Unitkey;
      Params["@tglawal"] = Tglawal;
      Params["@tglakhir"] = Tglakhir;
      Params["@kdklas"] = Kdklas;
      Params["@subunit"] = Subunit;

      string pdfurl;
      if(n == 0)
      {
        pdfurl = ReportUtils.ExportPDF("Usadi.Valid49.Aset.Rpt.Rpt.RINCMUTASIANTARPENGGUNA.rpt", CS, Params);
      }
      else
      {
        CS[ReportUtils.PDFNAME] = "RINCMUTASIANTARPENGGUNA_" + (string)GlobalAsp.GetSessionUser().GetValue("Userid") + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        pdfurl = ReportUtils.ExportExcel("Usadi.Valid49.Aset.Rpt.Rpt.RINCMUTASIANTARPENGGUNA.rpt", CS, Params);
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
      RptPars rptPar = new RptPars() { Title = "Pdf", RptClass = LinkPdf };
      RptPars rptPar1 = new RptPars() { Title = "Excel", RptClass = LinkExcel };
      return new List<RptPars>(new RptPars[] { rptPar, rptPar1 });
    }

    public void Print(int n)
    {
    }
  }
  #endregion DlgRptRincmutasiantarpengguna
}

