using System;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Web;
using Microsoft.VisualBasic.FileIO;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using CoreNET.Common.BO;
using CoreNET.Common.Base;

public class CsvConverter
{

  private static void setColumnHeader(DataTable dataTable, string columnName)
  {
    DataColumn datacolumn = new DataColumn(columnName);
    datacolumn.AllowDBNull = true;
    if (columnName.Equals("DEBET", StringComparison.CurrentCultureIgnoreCase))
    {
      datacolumn.DataType = typeof(decimal);
    }
    else if (columnName.Equals("KREDIT", StringComparison.CurrentCultureIgnoreCase))
    {
      datacolumn.DataType = typeof(decimal);
    }
    else if (columnName.Equals("SALDO", StringComparison.CurrentCultureIgnoreCase))
    {
      datacolumn.DataType = typeof(decimal);
    }
    dataTable.Columns.Add(datacolumn);
  }

  private static string[] removeColumns(string[] originalFields, int[] columnsIndexesToRemove)
  {
    var list = new List<string>(originalFields);
    foreach (int idx in columnsIndexesToRemove)
    {
      list.RemoveAt(idx);
    }
    return list.ToArray();
  }

  public static DataTable GetDataTableFromCSVFile(string csvFilePath, string delimeter, int[] columnsToSkip, bool firstRowIsColumnDefinition)
  {
    DataTable csvData = new DataTable();
    try
    {
      using (TextFieldParser csvReader = new TextFieldParser(csvFilePath))
      {
        csvReader.SetDelimiters(new string[] { delimeter });
        csvReader.HasFieldsEnclosedInQuotes = true;

        string[] colFields = null;

        if (firstRowIsColumnDefinition)//baris 1 adalah judul/header
        {
          string str = csvReader.ReadLine();
          colFields = str.Split(new string[] { delimeter }, StringSplitOptions.RemoveEmptyEntries);
          //colFields = csvReader.ReadFields();

          if (columnsToSkip.Length > 0)
          {
            colFields = removeColumns(colFields, columnsToSkip);
          }

          foreach (string column in colFields)
          {
            setColumnHeader(csvData, column);
          }
        }

        while (!csvReader.EndOfData)
        {
          string str = csvReader.ReadLine();
          string[] fieldData = str.Split(new string[] { delimeter }, StringSplitOptions.None);
          //string[] fieldData = csvReader.ReadFields();

          if (columnsToSkip.Length > 0)
            fieldData = removeColumns(fieldData, columnsToSkip);

          //Making empty value as null
          for (int i = 0; i < fieldData.Length; i++)
          {
            //if (!firstRowIsColumnDefinition && !firstPass)
            //{
            //  string columName = "col" + (i + 1).ToString();
            //  setColumnHeader(csvData, columName);
            //}

            if ((fieldData[i] == "") || (fieldData[i] == "NULL"))
            {
              fieldData[i] = null;
            }
          }
          DataRow dr = csvData.NewRow();
          //for (int i = 0; i < fieldData.Length; i++)
          //{
          //  dr[i] = fieldData[i];
          //}
          for (int i = 0; i < colFields.Length; i++)
          {
            if (fieldData[i] == null)
            {
              dr[i] = DBNull.Value;
            }
            else
            {
              dr[i] = fieldData[i];
            }
          }
          csvData.Rows.Add(dr);
          //csvData.Rows.Add(fieldData); 
        }
      }
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message + " at " + ex.StackTrace);
    }
    return csvData;
  }

  public static void ExportCsv(IList cdata, string csvfname, string delim, string[] coldefs)
  {
    #region Export ke CSV
    //progress++;
    //msg = "ekspor data ke CSV dimulai \n";
    //worker.ReportProgress(progress;

    string[] cols = new string[coldefs.Length];
    for (int j = 0; j < coldefs.Length; j++)
    {
      string[] strs = coldefs[j].Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
      cols[j] = strs[0].Trim();
    }

    int count = cdata.Count;
    int counter = 0;
    try
    {
      if (MasterAppConstants.Instance.StatusTesting && (cdata.Count > 0))
      {
        ArrayList naProps = new ArrayList();
        for (int i = 0; i < cols.Length; i++)
        {
          PropertyInfo Prop = ((IDataControl)cdata[0]).GetProperty(cols[i].Trim());
        }
        if (naProps.Count > 0)
        {
          string props = (string)naProps[0];
          for (int i = 1; i < naProps.Count; i++)
          {
            props += "," + naProps[i];
          }
          throw new Exception("This properties " + props + " not exist!");
        }
      }

      if (!Directory.Exists(Path.GetDirectoryName(csvfname)))
      {
        Directory.CreateDirectory(Path.GetDirectoryName(csvfname));
      }
      if (File.Exists(csvfname))
      {
        File.Delete(csvfname);
      }
      using (StreamWriter sw = new StreamWriter(csvfname,false))
      {
        foreach (IDataControl dp in cdata)
        {
          counter++;
          string line = "";
          string colline = "";
          for (int i = 0; i < cols.Length; i++)
          {
            if (counter == 1)
            {
              string[] strs = coldefs[i].Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
              if (strs.Length > 1)
              {
                colline += strs[1].Trim();
              }
              else
              {
                colline += cols[i].Trim();
              }
            }
            PropertyInfo Prop = dp.GetProperty(cols[i].Trim());
            if (Prop == null)
            {
              throw new Exception(ConstantDictExt.Translate(MethodBase.GetCurrentMethod().Name) + " line 178 cause there is no property named " + cols[i]);
            }
            object obj = Prop.GetValue(dp, null);
            string str = (obj == null) ? "" : obj.ToString().Trim();
            str = str.Replace("\r\n", HttpUtility.HtmlEncode("|"));
            str = str.Replace("\r", HttpUtility.HtmlEncode("|"));
            str = str.Replace("\n", HttpUtility.HtmlEncode("|"));
            str = str.Replace("<br/>", HttpUtility.HtmlEncode("|"));
            str = Regex.Replace(str, @"^\s*$\n", string.Empty, RegexOptions.Multiline);
            //str = HttpUtility.HtmlEncode(str);
            //if (Prop.PropertyType == typeof(System.Nullable<decimal>))
            if (Prop.PropertyType == typeof(decimal))
            {
              str = ((decimal)obj).ToString();
              str = str.Replace(",0000", "");
              str = str.Replace(",", ".");
            }
            else if (Prop.PropertyType == typeof(bool))
            {
              str = ((bool)obj) ? "1" : "0";
            }
            else if (Prop.PropertyType == typeof(DateTime))
            {
              DateTime d = ((DateTime)obj);
              if (d == new DateTime())
              {
                str = string.Empty;
              }
              else
              {
                str = d.ToString("dd/MM/yyyy");
              }
            }
            else if (Prop.PropertyType == typeof(string))
            {
              if (str.StartsWith("0"))
              {
                str = "'" + str;
              }
            }
            str = Printer.CheckBase64Encoding(str);
            line += str.Trim();
            if (i < cols.Length - 1)
            {
              line += delim;
              colline += delim;
            }
          }
          if (counter == 1)
          {
            sw.WriteLine(colline);
          }
          sw.WriteLine(line);
          //progress += (counter / count) * 50;
          //worker.ReportProgress(progress);
        }
      }
      //msg = "ekspor data ke CSV berhasil \n";
      //progress++;
      //worker.ReportProgress(progress;

    }
    catch (Exception ex)
    {
      //msg = "ekspor data ke CSV gagal karena " + ex.Message + " \n";
      //progress = 100;
      //worker.ReportProgress(progress;
      throw new Exception(ex.Message + " at " + ex.StackTrace);
    }

    #endregion
  }


  public static StringBuilder ExportHtml(ICollection cdata, string csvfname, string[] coldefs)
  {
    return ExportHtml(cdata, csvfname, coldefs, new string[] { }, null);
  }
  public static StringBuilder ExportHtml(ICollection cdata, string csvfname, string[] coldefs, string[] htmlcol, Hashtable strhtmlcols)
  {
    #region Export ke Html
    string[] cols = new string[coldefs.Length];
    for (int j = 0; j < coldefs.Length; j++)
    {
      string[] strs = coldefs[j].Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
      cols[j] = strs[0];
    }


    StringBuilder strText = new StringBuilder();

    int count = cdata.Count;
    string colname = string.Empty;
    int counter = 0;
    try
    {
      using (StringWriter sw = new StringWriter(strText))
      //using (StreamWriter sw = new StreamWriter(csvfname))
      {
        sw.WriteLine("<table border=1>");
        foreach (IDataControl dp in cdata)
        {
          counter++;
          string colline = "<tr>";//judul
          #region looping kolom
          string line = "<tr>";
          for (int i = 0; i < cols.Length; i++)
          {
            #region Header Row Tabel
            if (counter == 1)
            {
              string[] strs = coldefs[i].Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
              if (strs.Length > 1)
              {
                colline += "<td>" + strs[1].Trim() + "</td>";
              }
              else
              {
                colline += "<td>" + cols[i].Trim() + "</td>";
              }
            }
            #endregion
            PropertyInfo Prop = dp.GetProperty(cols[i]);
            if (Prop == null)
            {
              throw new Exception(ConstantDictExt.Translate(MethodBase.GetCurrentMethod().Name));
            }
            object obj = Prop.GetValue(dp, null);
            string str = (obj == null) ? "-" : obj.ToString().Trim();
            if (Prop.PropertyType == typeof(System.Nullable<decimal>))
            {
              str = ((decimal)obj).ToString();
              str = str.Replace(",0000", "");
              str = str.Replace(",", ".");
            }
            else if (Prop.PropertyType == typeof(bool))
            {
              str = ((bool)obj) ? "1" : "0";
            }
            str = string.IsNullOrEmpty(str) ? "-" : str;
            if (new ArrayList(htmlcol).IndexOf(cols[i]) != -1)
            {
              string id = cols[i] + counter;
              line += "<td><h1>" + id + "</h1></td>";
              Hashtable h = (Hashtable)strhtmlcols[cols[i]];
              h[id] = Printer.CheckBase64Encoding(str.Trim());
            }
            else
            {
              line += "<td>" + str.Trim() + "</td>";
            }
          }
          #region RowHeader
          if (counter == 1)
          {
            colline += "</tr>";
            sw.WriteLine(colline);
          }
          #endregion

          line += "</tr>";
          sw.WriteLine(line);
          #endregion
        }
        sw.WriteLine("</table>");
      }
    }
    catch (Exception ex)
    {
      UtilityBO.Log(ex);
    }

    return strText;

    #endregion
  }
}
