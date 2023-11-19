using System;
using System.Linq;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Ext.Net;
using Ext.Net.Utilities;
using CoreNET.Common.Base;
using CoreNET.Common.BO;


namespace Usadi.Valid49.BO
{
  #region Usadi.Valid49.BO.DlgRptRekappenggunaansementaraControl, Usadi.Valid49.Aset.Rpt
  [Serializable]
  public class DlgRptRekappenggunaansementaraControl : DaftunitControl, IDataControlUIEntry, IPrintable
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
    public string Kdklas { get;set;}
    public string Uraiklas { get; set; }
    public string Kdtans { get; set; }
    public string Nmtrans { get; set; }
    public Dictionary<string, string> MapMode { get; set; } = new Dictionary<string, string>();

    public ArrayList Options { get; set; } = new ArrayList();
    public string StrMode { get; set; } = "default";
    public DlgRptRekappenggunaansementaraControl()
    {
      XMLName = ConstantTablesAsetDM.XMLDAFTUNIT;
      SetDefaultDate(RANGE_MONTH);
      Subunit = "1";
      Kdklas = "01";
      //parsing suffix
      var suffix = HttpContext.Current.Request["suffix"];
      var temps = suffix.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
      for (int i = 0; i < temps.Length; i++)
      {
        Options.Add(new ParamControl() { Kdpar = temps[i], Nmpar = temps[i] });
      }
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
      //hpars.Add(JklasLookupControl.Instance.GetLookupParameterRow(this, false));
      //hpars.Add(JtransLookupControl.Instance.GetLookupParameterRow(this, false));
      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdklas=Kelas Aset"),
        GetList(new JklasRptLookupControl()), "Kdklas=Uraiklas", 54).SetEnable(enableFilter));
      ArrayList listkelaset = new ArrayList(new ParamControl[] {
         new ParamControl() { Kdpar="11",Nmpar="Aset Lancar"}
        ,new ParamControl() { Kdpar="13",Nmpar="Aset Tetap"}
        ,new ParamControl() { Kdpar="15",Nmpar="Aset Lainnya"}
      });
      hpars.Add(new ParameterRow(ConstantDict.GetColumnTitleEntry("Kelaset=Kelompok Aset"), ParameterRow.MODE_SELECT,
       listkelaset, "Kdpar=Nmpar", 54).SetEnable(enableFilter));
      //hpars.Add(new ParameterRowDate(this, ParameterRow.MODE_DATE_RANGE).SetEnable(enableFilter));
      hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglawal=Tanggal Awal"), true).SetEnable(true));
      hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglakhir=Tanggal Akhir"), true).SetEnable(true));

      ArrayList list = new ArrayList(new ParamControl[] {
         new ParamControl() { Kdpar="1",Nmpar="Ya"}
        ,new ParamControl() { Kdpar="0",Nmpar="Tidak"}
      });
      hpars.Add(new ParameterRow(ConstantDict.GetColumnTitleEntry("Subunit=Subunit"), ParameterRow.MODE_SELECT,
        list, "Kdpar=Nmpar", 26).SetEnable(enableFilter));

      //nambah dropdown buat suffix
      hpars.Add(new ParameterRow(ConstantDict.GetColumnTitleEntry("StrMode=Penggunaan"), ParameterRow.MODE_SELECT,
        Options, "Kdpar=Nmpar", 54).SetEnable(enableFilter));
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
      CS[ReportUtils.RPTNAME] = $"REKAPPENGGUNAAN_{StrMode}.rpt";
      CS[ReportUtils.PDFNAME] = $"REKAPPENGGUNAAN_{StrMode}_{(string)GlobalAsp.GetSessionUser().GetValue("Userid")}_{ DateTime.Now.ToString("yyyyMMddHHmmss")}.pdf";


      Hashtable Params = new Hashtable();
      Params["@unitkey"] = Unitkey;
      Params["@Kdklas"] = Kdklas;
      //Params["@Jtran"] = Kdtans;
      Params["@Kelompok"] = Kelaset;
      Params["@tglawal"] = Tglawal; //Tgl1;   //Tglawal;
      Params["@tglakhir"] = Tglakhir; //Tgl2; //Tglakhir;
      Params["@subunit"] = Subunit;

      //pilih rpt berdasarkan suffix
      //lokasi rpt
      //string lokasirpt = @"E:\Projc\ASET_NEW\ModulAset\Usadi\Usadi.Valid49.Aset.Rpt\Rpt\PENERIMAAN_HIBAH.rpt";
      //if (!StrMode.Equals("default"))
      //{
      //  lokasirpt = $@"E:\Projc\ASET_NEW\ModulAset\Usadi\Usadi.Valid49.Aset.Rpt\Rpt\PENERIMAAN_{StrMode}.rpt";
      //}
      //string pdfurl = ReportUtils.ExportPDF(lokasirpt, CS, Params);

      //maping suffix lowcase
      MapMode["default"] = string.Empty;
      MapMode["sementara"] = "SEMENTARA";
      MapMode["pihak lain"] = "DIOPERASIKANPIHAKLAIN";

      string cname = "Usadi.Valid49.Aset.Rpt.Rpt.REKAPPENGGUNAAN_SEMENTARA.rpt";
      if (!StrMode.Equals("default"))
      {
        //string strmode = StrMode.Replace(" ", "");
        string strmode = MapMode[StrMode.ToLower()];
        cname = $"Usadi.Valid49.Aset.Rpt.Rpt.REKAPPENGGUNAAN_{strmode}.rpt";
      }

      string pdfurl = ReportUtils.ExportPDF(cname, CS, Params);

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
      RptPars rptPar = new RptPars() { Title = "Rekapitulasi Laporan Penggunaan BMD Pemda", RptClass = LinkPdf };
      return new List<RptPars>(new RptPars[] { rptPar });
    }

    public void Print(int n)
    {
    }
  }
  #endregion DlgRptRekappenggunaansementara
}

