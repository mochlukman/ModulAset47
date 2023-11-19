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
  #region Usadi.Valid49.BO.DlgRptDaftrincbwkapitalisasiControl, Usadi.Valid49.Aset.Rpt
  [Serializable]
  public class DlgRptDaftrincbwkapitalisasiControl : DaftunitControl, IDataControlUIEntry, IPrintable
  {
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
    #region Property Subunit
    private string _Subunit;
    public string Subunit
    {
      get { return _Subunit; }
      set { _Subunit = value; }
    }
    #endregion
    public DlgRptDaftrincbwkapitalisasiControl()
    {
      PemdaControl cPemda = new PemdaControl();
      cPemda.Configid = "cur_thang";
      cPemda.Load("PK");

      XMLName = ConstantTablesAsetDM.XMLDAFTUNIT;
      Subunit = "1";
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

    public override HashTableofParameterRow GetEntries()
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(DaftunitLookupControl.Instance.GetLookupParameterRow(this, false));

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

    public string GetURLReport(int n)
    {
      Hashtable CS = new Hashtable();
      CS[ReportUtils.SQLINSTANCE] = GlobalAsp.GetDataSource();
      CS[ReportUtils.DBNAME] = (string)((IDataControlAppuser)GlobalAsp.GetSessionUser()).GetValue("DBName");
      CS[ReportUtils.DBUSER] = SQLDataSource.GetUserDB();
      CS[ReportUtils.DBPASSWORD] = SQLDataSource.GetPwdDB();
      CS[ReportUtils.PATH] = "Report/";
      CS[ReportUtils.RPTLIB] = "Usadi.Valid49.Aset.Rpt.dll";
      CS[ReportUtils.RPTNAME] = "DaftRincBwKapitalisasi.rpt";
      CS[ReportUtils.PDFNAME] = "DaftRincBwKapitalisasi_" + (string)GlobalAsp.GetSessionUser().GetValue("Userid") + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";


      Hashtable Params = new Hashtable();
      Params["@unitkey"] = Unitkey;
      Params["@tglawal"] = Tglawal;
      Params["@tglakhir"] = Tglakhir;
      Params["@subunit"] = Subunit;

      string pdfurl = ReportUtils.ExportPDF("Usadi.Valid49.Aset.Rpt.Rpt.DaftRincBwKapitalisasi.rpt", CS, Params);
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
      RptPars rptPar = new RptPars() { Title = "Barang Dibawah Nilai Kapitalisasi", RptClass = LinkPdf };
      return new List<RptPars>(new RptPars[] { rptPar });
    }

    public void Print(int n)
    {
    }
  }
  #endregion DlgRptDaftrincbwkapitalisasi
}

