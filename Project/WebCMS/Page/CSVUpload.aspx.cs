using CoreNET.Common.Base;
using CoreNET.Common.BO;
using Ext.Net;
//using OfficeOpenXml;//Bikin Error Precompiled
using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;

public partial class CSVUpload : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (GlobalAsp.CekSessionPage(Page))
    {
      Title = string.IsNullOrEmpty(Request["mode"]) ? Title : Request["title"];
      txtPar.Visible = string.IsNullOrEmpty(Request["mode"]);
      ComboBox1.Visible = string.IsNullOrEmpty(Request["mode"]);
      txtInfo.Visible = string.IsNullOrEmpty(Request["mode"]);
      ExecQuery.Visible = string.IsNullOrEmpty(Request["mode"]);
      ExportButton.Visible = string.IsNullOrEmpty(Request["mode"]);
      Button1.Visible = string.IsNullOrEmpty(Request["mode"]);
      SaveButton.Text = (ConstantDictExt.IDLOCALE == ConstantDictExt.EN) ? "Import" : "Impor";

      if (!Page.IsPostBack)
      {
        IDataControl ctrl = UtilityUI.GetDataControl(GlobalAsp.GetRequestI());
        string csvs = SysgetkeysLookupControl.GetLoadConfig(ctrl);
        if (string.IsNullOrEmpty(csvs))
        {
          //Isi nilai default aja ya
          csvs = "{0}|{1}={2}";
          csvs = string.Format(csvs, ((BaseBO)ctrl).XMLName, ctrl.GetType().FullName, "Load Config");
          //csvs = "RMNILAIDET|CoreNET.Common.BO.RmSkorFilterControl, Usadi.Valid49.RM=Load Form Config";
        }
        string[] strs = null;
        //Ss00appmenuconfigControl dc = new Ss00appmenuconfigControl() { Idmenu = GlobalAsp.GetRequestId(), Par = "LOADCSV" };
        //dc = Ss00appmenuconfigLookupControl.FindAndSetValuesInto(dc);
        //string csvs = dc.Value;
        string[] entries = csvs.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        ArrayList list = new ArrayList();
        for (int i = 0; i < entries.Length; i++)
        {
          string str = entries[i].Trim();
          strs = str.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
          if (strs.Length > 1)
          {
            list.Add(new string[] { strs[0], strs[1] });
          }
        }
        Store1.DataSource = list;
        Store1.DataBind();

        ComboBox1.Listeners.Select.Handler = @"CoreNET.Methods1.CBSelected()";
        ExecQuery.Listeners.Click.Handler = @"CoreNET.Methods1.btnExecQuery()";
        ExportButton.Listeners.Click.Handler = @"CoreNET.Methods1.btnSaveCsvClick()";

        ComboBox1.SelectedIndex = int.Parse(string.IsNullOrEmpty(Request["mode"]) ? "0" : Request["mode"]);
        CBSelected();
        //ComboBox1.Text = (Request["mode"]);
        //ComboBox1.SelectedIndex = 3;
        //ComboBox1.FireEvent("select", null);
        //ComboBox1.Text = !string.IsNullOrEmpty(Request["mode"]) ? Request["mode"] : ComboBox1.Text;

        string config = (string)ComboBox1.SelectedItem.Value;
        strs = config.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
        string cname = strs[1];
        IDataControlUI dc = UtilityUI.GetDataControl(GlobalAsp.GetRequestI());
        if (dc == null)
        {
          try
          {
            dc = (IDataControlUI)UtilityUI.Create(cname);
          }
          catch (Exception ex)
          {
            UtilityBO.Log(ex);
            X.Msg.Alert("Error", ex.Message + "=" + cname).Show();
            return;
          }
        }

        string[] mappings = ((ILoadCsv)dc).GetLoadCsvColumns();
        if (MasterAppConstants.Instance.StatusTesting)
        {
          string props = string.Empty;
          for (int i = 0; i < mappings.Length; i++)
          {
            string[] coltitles = mappings[i].Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
            if (dc.GetProperty(coltitles[0]) == null)
            {
              props += coltitles[0] + ",";
            }
          }
          if (!string.IsNullOrEmpty(props))
          {
            throw new Exception("This properties " + props + " not exist!");
          }
        }
      }
    }
    else
    {
      X.Js.Call("home");
    }
  }

  public void UploadClick(object sender, DirectEventArgs e)
  {
    string tpl = "Uploaded file: {0}<br/>Size: {1} bytes";

    if (FileUploadField1.HasFile)
    {
      HttpPostedFile file = FileUploadField1.PostedFile;
      string[] strs = file.FileName.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
      string dir = GlobalAsp.GetDataDir() + "Tmp\\";
      if (!Directory.Exists(dir))
      {
        Directory.CreateDirectory(dir);
      }
      //string csvFilePath = dir + strs[strs.Length - 1];
      string csvFilePath = dir + string.Format("{0}{1}", GlobalAsp.GetSessionUser(), GlobalAsp.GetRequestId()) + Path.GetFileName(file.FileName);
      file.SaveAs(csvFilePath);


      string config = (string)ComboBox1.SelectedItem.Value;
      //string config = ((string[])((IList)Store1.DataSource)[2])[0];
      strs = config.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
      string cname = strs[1];
      IDataControlUI dc = UtilityUI.GetDataControl(GlobalAsp.GetRequestI());
      if (dc == null)
      {
        try
        {
          dc = (IDataControlUI)UtilityUI.Create(cname);
        }
        catch (Exception ex)
        {
          UtilityBO.Log(ex);
          X.Msg.Alert("Error", ex.Message + "=" + cname).Show();
          return;
        }
      }


      #region UploadCSV
      if (csvFilePath != "")
      {
        #region LoadCSV
        #region Load Mapping
        //string pk = ((ViewListProperties)dc.GetProperties()).IDKey;
        //if (string.IsNullOrEmpty(pk))
        //{
        //  pk = ((ViewListProperties)dc.GetProperties()).PrimaryKeys[0];
        //}
        string tname = strs[0];
        string[] mappings = ((ILoadCsv)dc).GetLoadCsvColumns();

        Hashtable Maps = new Hashtable();
        for (int i = 0; i < mappings.Length; i++)
        {
          string[] maps = mappings[i].Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);

          string colname = maps[0];
          Maps[maps[0]] = maps[0];
          if (maps.Length > 1)
          {
            colname = maps[1];
            Maps[maps[0]] = maps[1];
          }
        }
        #endregion
        int counter = 0;
        try
        {
          #region Load DataTable From CSV File
          int[] columnsToSkip = { };
          string delimiter = ";";
          if (dc != null)
          {
            delimiter = ((ViewListProperties)dc.GetProperties()).Delimiter;
          }
          DataTable csvData = CsvConverter.GetDataTableFromCSVFile(csvFilePath, delimiter, columnsToSkip, true);

          #endregion
          #region Generate Query
          ((IDataControlUI)dc).SetPageKey();
          ((ILoadCsv)dc).ExecutedPreSQL(tname, cbDelete.Checked);
          for (counter = 0; counter < csvData.Rows.Count; counter++)
          {
            DataRow dr = csvData.Rows[counter];
            //string key = dr[Maps[pk].ToString()].ToString().Trim();
            //#region PreQuery
            //((ILoadCsv)dc).ExecutedPreSQL(tname, pk, key, cbDelete.Checked);
            //#endregion
            #region Query

            ((ILoadCsv)dc).ExecutedSQL(tname, csvData, counter, cbDelete.Checked);
            #endregion
            //#region Post Query
            //((ILoadCsv)dc).ExecutedPostSQL(tname, pk, key, cbDelete.Checked);
            //#endregion
          }
          ((ILoadCsv)dc).ExecutedPostSQL(tname, cbDelete.Checked, GlobalAsp.GetRequestId());  //nggak boleh gini, update primary key bikin gagal sinkron??
          #endregion
          X.MessageBox.Alert(ConstantDictExt.Translate(GlobalExt.LBL_INFO), ConstantDictExt.Translate("INFO_UPLOAD_SUCCESS ")).Show();
        }
        catch (Exception ex)
        {
          WindowDebug.ShowMessage(Page, ConstantDictExt.TranslateException(ex, ConstantDictExt.Translate("ERROR_UPLOAD")));
          return;
        }
        #endregion
      }
      #endregion

      X.MessageBox.Alert(ConstantDictExt.Translate(GlobalExt.LBL_INFO), string.Format(tpl, FileUploadField1.PostedFile.FileName, FileUploadField1.PostedFile.ContentLength)).Show();
    }
    else
    {
      X.MessageBox.Alert(ConstantDictExt.Translate(GlobalExt.LBL_INFO), ConstantDictExt.Translate("ERROR_UPLOAD")).Show();
    }
  }

  //public void UploadXls()
  //{
  //  //using OfficeOpenXml;
  //  using (ExcelPackage xlPackage = new ExcelPackage(new FileInfo(@"C:\YourDirectory\sample.xlsx")))
  //  {
  //    //var myWorksheet = xlPackage.Workbook.Worksheets.First(); //select sheet here
  //    IEnumerator enumerator = xlPackage.Workbook.Worksheets.GetEnumerator();
  //    ExcelWorksheet myWorksheet = null;

  //    while (enumerator.MoveNext())
  //    {
  //      myWorksheet = (ExcelWorksheet)enumerator.Current;
  //      // Perform logic on the item
  //    }

  //    int totalRows = myWorksheet.Dimension.End.Row;
  //    int totalColumns = myWorksheet.Dimension.End.Column;

  //    StringBuilder sb = new StringBuilder(); //this is your data
  //    for (int rowNum = 1; rowNum <= totalRows; rowNum++) //select starting row here
  //    {
  //      object row = myWorksheet.Cells[rowNum, 1, rowNum, totalColumns].Value;
  //      //var row = myWorksheet.Cells[rowNum, 1, rowNum, totalColumns].Select(c => c.Value == null ? string.Empty : c.Value.ToString());
  //      //sb.AppendLine(string.Join(",", row));
  //    }
  //  }
  //}
  #region DirectMethods
  #endregion
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void CBSelected()
  {
    string config = string.Empty;
    if (string.IsNullOrEmpty(ComboBox1.Text))
    {
      config = ((string[])((IList)Store1.DataSource)[ComboBox1.SelectedIndex])[0];
      ComboBox1.Text = config;
    }
    else
    {
      config = ComboBox1.Text;
    }
    //string config = (string)ComboBox1.SelectedItem.Value;
    string[] strs = config.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);

    string cname = strs[1];
    IDataControlUI dc = UtilityUI.GetDataControl(GlobalAsp.GetRequestI());
    if (dc == null)
    {
      try
      {
        dc = (IDataControlUI)UtilityUI.Create(cname);
      }
      catch (Exception ex)
      {
        UtilityBO.Log(ex);
        X.Msg.Alert("Error", ex.Message + "=" + cname).Show();
        return;
      }
    }
    string pk = ((ViewListProperties)dc.GetProperties()).PrimaryKeys[0];
    string scol = string.Empty;
    string lvlscol = string.Empty;
    //string typscol = "TYPE";
    string tname = strs[0];
    string[] mappings = new string[] { };

    if (typeof(ILoadCsv).IsInstanceOfType(dc))
    {
      mappings = ((ILoadCsv)dc).GetLoadCsvColumns();
    }
    else
    {
      if (typeof(ICsv).IsInstanceOfType(dc))
      {
        mappings = ((ICsv)dc).GetCsvColumns(MyDocumentFormat.DOC);
      }
      else
      {
        if (typeof(IDataControlTreeGrid3).IsInstanceOfType(dc))
        {
          mappings = ((IDataControlTreeGrid3)dc).GetKeys();
        }
        else
        {
          mappings = dc.GetKeys();
        }
      }
    }
    string delimiter = ";";
    if (dc != null)
    {
      delimiter = ((ViewListProperties)dc.GetProperties()).Delimiter;
    }
    string strmaps = "";
    string colnames = "";
    int counter = 0;
    foreach (string str in mappings)
    {
      counter++;
      strmaps += str + "|";
      string[] temps = str.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
      colnames += (temps.Length > 1) ? temps[1] : temps[0];
      if (counter < mappings.Length)
      {
        colnames += ",";
      }
    }
    txtInfo.Text =
      config + "\n" +
      strmaps + "\n" +
      "Delimiter=" + delimiter;

    if (ConstantDictExt.IDLOCALE == ConstantDictExt.EN)
    {
      lbl_message.Text = "Create a CSV file with delimiter ';' contain columns " + colnames;
    }
    else
    {
      lbl_message.Text = "Buat file CSV dengan pemisah karakter ';' dengan kolom " + colnames;
    }



  }
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void btnExecQuery()
  {
    if (!string.IsNullOrEmpty(txtPar.Text.Trim()))
    {
      try
      {
        //BaseDataAdapter.ExecuteCmd(SQLDataSource.MODE_DB_OPERATIONAL, txtPar.Text.Trim());
        X.Msg.Alert(ConstantDictExt.Translate(GlobalExt.LBL_INFO), ConstantDictExt.Translate("INFO_PROCESS_SUCCESS")).Show();
        txtPar.Text = "";
      }
      catch (Exception ex)
      {
        UtilityBO.Log(ex);
      }
    }
    else
    {
      X.Msg.Alert(ConstantDictExt.Translate(GlobalExt.LBL_INFO), ConstantDictExt.Translate("ERROR_PROCESS")).Show();
    }
  }
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void btnSaveCsvClick()
  {

    string config = (string)ComboBox1.SelectedItem.Value;
    string[] strs = config.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);

    string cname = strs[1];
    IDataControlUI dc = UtilityUI.GetDataControl(GlobalAsp.GetRequestI());
    if (dc == null)
    {
      try
      {
        dc = (IDataControlUI)UtilityUI.Create(cname);
      }
      catch (Exception ex)
      {
        UtilityBO.Log(ex);
        X.Msg.Alert("Error", ex.Message + "=" + cname).Show();
        return;
      }
    }

    IList list = null;
    if (string.IsNullOrEmpty(txtPar.Text.Trim()))
    {
      list = dc.View();
    }
    else
    {
      try
      {
        list = BaseDataAdapter.GetListDC(dc, txtPar.Text.Trim());
        if (list.Count == 0)
        {
          list = dc.View();
        }
      }
      catch (Exception ex)
      {
        UtilityBO.Log(ex);
      }
    }


    string[] key = null;
    if (typeof(ILoadCsv).IsInstanceOfType(dc))
    {
      key = ((ILoadCsv)dc).GetLoadCsvColumns();
    }
    else
    {
      if (typeof(ICsv).IsInstanceOfType(dc))
      {
        key = ((ICsv)dc).GetCsvColumns(MyDocumentFormat.CSV);
      }
      else
      {
        key = dc.GetKeys();
      }
    }
    #region Export Data
    if (list != null)
    {
      string fullpath = GlobalAsp.GetDataDir();
      string dir = "Tmp\\";
      string csvname = "file.csv";
      if (typeof(IEksporable).IsInstanceOfType(dc))
      {
        csvname = ((IEksporable)dc).GetFileName(MyDocumentFormat.CSV);
      }
      PropertyInfo prop = dc.GetProperty("FolderRef");
      if (prop != null)
      {
        dir = (string)prop.GetValue(dc, null);
      }
      if (typeof(IPrintable).IsInstanceOfType(dc))
      {
        fullpath = ((IEksporable)dc).GetFilePath(MyDocumentFormat.CSV);
      }
      else
      {
        fullpath += dir;
        if (!System.IO.Directory.Exists(fullpath))
        {
          System.IO.Directory.CreateDirectory(fullpath);
        }
        fullpath += csvname;
      }
      CsvConverter.ExportCsv(list, fullpath, ((ViewListProperties)dc.GetProperties()).Delimiter, key);

      FileDownloadLink1.Text = "Download File CSV";

      string fname = dir + csvname;
      string url = GlobalAsp.GetDataURL() + fname;
      FileDownloadLink1.NavigateUrl = url;
      WindowLink.Show();
    }
    else
    {
      X.Msg.Alert(ConstantDictExt.Translate(GlobalExt.LBL_INFO), ConstantDictExt.Translate("ERROR_FITUR_NOT_IMPLEMENTED")).Show();
    }
    #endregion

  }

}