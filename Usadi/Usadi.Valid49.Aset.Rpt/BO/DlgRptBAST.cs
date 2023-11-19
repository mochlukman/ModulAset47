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
  #region Usadi.Valid49.BO.DlgRptBASTControl, Usadi.Valid49.Aset.Rpt
  [Serializable]
  public class DlgRptBASTControl : BeritaControl, IDataControlUIEntry, IPrintable
  {
    public DlgRptBASTControl()
    {
      XMLName = ConstantTablesAsetMAT.XMLBERITA;
    }

    ViewListProperties cViewListProperties = null;
    public new IProperties GetProperties()
    {
      if (cViewListProperties == null)
      {
        cViewListProperties = (ViewListProperties)base.GetProperties();
        cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
        cViewListProperties.ReadOnlyFields = new string[] { "Unitkey" };
        cViewListProperties.PageSize = 20;
      }
      return cViewListProperties;
    }

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

    public new IList View()
    {
      IList list = this.View("Rpt");
      List<BeritaControl> ListData = new List<BeritaControl>();
      foreach (BeritaControl dc in list)
      {
        dc.Unitkey = Unitkey;
        ListData.Add(dc);
      }
      return ListData;
    }

    public new string[] GetKeys()
    {
      return new string[] { "Unitkey", "Statusicon", "LinkPdf", "Kdtans", "Nmtrans", "Noba", "Tglba", "Nokontrak", "Uraiba" };
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
      hpars.Add(DaftunitLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(true));
      return hpars;
    }

    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection
      {
        // Fields.Create(ConstantDict.GetColumnTitle("Statusicon"), typeof(Icon), 5, HorizontalAlign.Center)
      };
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Stricon"), typeof(string), 1, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Stricon"), typeof(CommandColumn), Cmds, 5, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noba=No BAST"), typeof(string), 30, HorizontalAlign.Left).SetEditable(false));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmtrans=Jenis BAST"), typeof(string), 25, HorizontalAlign.Left).SetEditable(false));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglba=Tanggal BAST"), typeof(DateTime), 20, HorizontalAlign.Center).SetEditable(false));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nokontrak=No Kontrak"), typeof(string), 30, HorizontalAlign.Left).SetEditable(false));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Uraiba=Uraian"), typeof(string), 55, HorizontalAlign.Left).SetEditable(false));

      return columns;
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
        return ConstantDict.Translate(XMLName) + " " + Noba + "|" + url;
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
      CS[ReportUtils.RPTNAME] = "BAST.rpt";
      CS[ReportUtils.PDFNAME] = "BAST_" + (string)GlobalAsp.GetSessionUser().GetValue("Userid") + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";

      Hashtable Params = new Hashtable();
      Params["@unitkey"] = Unitkey;
      Params["@noba"] = Noba;

      string pdfurl = ReportUtils.ExportPDF("Usadi.Valid49.Aset.Rpt.Rpt.BAST.rpt", CS, Params);
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
      RptPars rptPar = new RptPars() { Title = "BAST", RptClass = LinkPdf };
      return new List<RptPars>(new RptPars[] { rptPar });
    }

    public void Print(int n)
    {
    }
  }
  #endregion DlgRptBAST
}

