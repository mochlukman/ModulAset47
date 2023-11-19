using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections;
using System.IO;
using System.Web;

namespace CoreNET.Common.Base
{
  public class ReportUtils
  {
    public const string SQLINSTANCE = "SQLINSTANCE";
    public const string DBNAME = "DBNAME";
    public const string DBUSER = "DBUSER";
    public const string DBPASSWORD = "DBPASSWORD";
    public const string PATH = "PATH";
    public const string RPTLIB = "RPTLIB";
    public const string RPTNAME = "RPTNAME";
    public const string PDFNAME = "PDFNAME";

    public enum DocTypes
    {
      PDFMODE = 0,
      XLSMODE = 1
    }
    public static string ExportPDF(string cname, Hashtable CS, Hashtable Params)
    {
      return ExportPDF(cname, CS, Params, DocTypes.PDFMODE);
    }
    public static string ExportExcel(string cname, Hashtable CS, Hashtable Params)
    {
      return ExportPDF(cname, CS, Params, DocTypes.XLSMODE);
    }
    public static string ExportPDF(string cname, Hashtable CS, Hashtable Params, DocTypes docType)
    {
      bool isModeFile = false;
      try
      {
        isModeFile = File.Exists(cname);
      }
      catch { }

      string SQLInstance = (string)CS[SQLINSTANCE];
      string db = (string)CS[DBNAME];
      string user = (string)CS[DBUSER];
      string pwd = (string)CS[DBPASSWORD];

      //System.Type T = System.Type.GetType(cname);
      //ReportClass report = Activator.CreateInstance(T) as ReportClass;
      //report.SetParameterValue("@nofaktur",Nofaktur);
      //report.DataSourceConnections[0].SetConnection(SQLInstance, db, user, pwd);

      string folder = (string)CS[PATH]; //"Primos/Report/";
      string rpturl = GlobalAsp.GetDataURL() + folder;
      string rptpath = GlobalAsp.GetDataDir() + folder;
      string rptname = (string)CS[RPTNAME];// "Faktur.rpt";
      string rptfile = rptpath + rptname;
      string pdfname = (string)CS[PDFNAME]; //"Faktur.pdf";
      string pdffile = rptpath + pdfname;
      string pdfurl = rpturl + pdfname + "?";
      string dllpath = HttpContext.Current.Server.MapPath("~/bin/") + (string)CS[RPTLIB]; //"Diasoft.Report.dll";

      if (isModeFile)
      {
        rptfile = cname;
      }
      else
      {
        if (!Directory.Exists(rptpath))
        {
          Directory.CreateDirectory(rptpath);
        }
        System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFile(dllpath);

        File.Delete(pdffile);
        File.Delete(rptfile);//untuk security harus selalu dioverride
        AssemblyUtils.WriteToAnyFile(assembly, cname, rptfile);
      }

      ReportDocument rd = new ReportDocument();
      rd.Load(rptfile, OpenReportMethod.OpenReportByDefault);
      foreach (string key in Params.Keys)
      {
        rd.SetParameterValue(key, Params[key]);
      }
      rd.DataSourceConnections[0].SetConnection(SQLInstance, db, user, pwd);

      //CrystalDecisions.Web.CrystalReportViewer CrystalReportViewer1 = new CrystalDecisions.Web.CrystalReportViewer();
      //CrystalReportViewer1.ReportSource = rd;

      //CrystalReportViewer1.PrintMode = CrystalDecisions.Web.PrintMode.ActiveX;
      //CrystalReportViewer1.DisplayStatusbar = false;
      //CrystalReportViewer1.DisplayToolbar = false;
      //CrystalReportViewer1.RefreshReport();

      ExportOptions CrExportOptions;
      DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
      ExportFormatOptions CrFormatTypeOptions = null;
      switch (docType)
      {
        case DocTypes.XLSMODE:
          CrFormatTypeOptions = new ExcelFormatOptions();
          CrExportOptions = rd.ExportOptions;
          {
            CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            CrExportOptions.ExportFormatType = ExportFormatType.ExcelWorkbook;
            CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
            CrExportOptions.FormatOptions = CrFormatTypeOptions;
          }
          break;
        default:
          CrFormatTypeOptions = new PdfRtfWordFormatOptions();
          CrExportOptions = rd.ExportOptions;
          {
            CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
            CrExportOptions.FormatOptions = CrFormatTypeOptions;
          }
          break;
      }
      CrDiskFileDestinationOptions.DiskFileName = pdffile;
      try
      {
        rd.Export();
      }
      catch (Exception ex)
      {
        UtilityBO.Log(ex);
      }

      //foreach (string par in Request.Params)
      //{
      //  if (!par.Equals("cname") && par.Contains("@"))
      //  {
      //    report.SetParameterValue(par, Request[par]);
      //  }
      //}

      //string ConnectionString = string.Format("data source={0};initial catalog={3};user id={1};password={2};Asynchronous Processing=true",
      //  SQLInstance, user, pwd, db);
      //CrystalReportViewer1.ReportSource = report;

      return pdfurl;
    }
  }
}
