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
  #region Usadi.Valid49.BO.DlgRptMappingControl, Usadi.Valid49.Aset.Rpt
  [Serializable]
  public class DlgRptMappingControl : DmpemdaControl, IDataControlUIEntry, IPrintable
  {

    public DlgRptMappingControl()
    {
      //PemdaControl cPemda = new PemdaControl();
      //cPemda.Configid = "sikdpemda";
      //cPemda.Load("PK");

      XMLName = ConstantTablesSys.XMLDMPEMDA;
    }

    ViewListProperties cViewListProperties = null;
    public new IProperties GetProperties()
    {
      if (cViewListProperties == null)
      {
        cViewListProperties = (ViewListProperties)base.GetProperties();
        cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
        cViewListProperties.ReadOnlyFields = new string[] { "Kdpemda" };
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

    public new IList View()
    {
      IList list = this.View(BaseDataControl.FILTER);
      return list;
    }

    public new string[] GetKeys()
    {
      return new string[] { "LinkPdf", "Statusicon", "Kdpemda", "Nmpemda" };
    }
    public new void SetFilterKey(BaseBO bo)
    {
      Kdpemda = (string)GlobalAsp.GetSessionUser().GetValue("Kdpemda");
      Nmpemda = (string)GlobalAsp.GetSessionUser().GetValue("Nmpemda");
    }
  
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection
      {
        //Fields.Create(ConstantDict.GetColumnTitle("Statusicon"), typeof(CommandColumn), 5, HorizontalAlign.Center)
      };
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Stricon"), typeof(String), 1, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Stricon"), typeof(CommandColumn), Cmds, 5, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdpemda"), typeof(string), 10, HorizontalAlign.Left).SetEditable(false));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmpemda"), typeof(string), 50, HorizontalAlign.Left).SetEditable(false));
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
        return ConstantDict.Translate(XMLName) + " " + Kdpemda + "|" + url;
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
      CS[ReportUtils.RPTNAME] = "MAPPING.rpt";
      CS[ReportUtils.PDFNAME] = "MAPPING_" + (string)GlobalAsp.GetSessionUser().GetValue("Userid") + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";


      Hashtable Params = new Hashtable();
      Params["@kdpemda"] = Kdpemda;

      string pdfurl = ReportUtils.ExportPDF("Usadi.Valid49.Aset.Rpt.Rpt.MAPPING.rpt", CS, Params);
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
      return new List<RptPars>();
    }

    public void Print(int n)
    {
    }
  }
  #endregion DlgRptMapping
}

