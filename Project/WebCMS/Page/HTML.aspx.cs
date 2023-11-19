using CoreNET.Common.Base;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI;

public partial class Page_HTML : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (!Page.IsPostBack)
    {
      UtilityUI.SetSessionExtPage(UtilityUI.PAGE_TABULAR);
      if (string.IsNullOrEmpty(Request["back"]) && string.IsNullOrEmpty(Request["passdc"]))
      {
        try
        {
          if (!Page.IsPostBack)
          {
            UtilityUI.ProcessInfo();
          }
        }
        catch (Exception ex)
        {
          if (MasterAppConstants.Instance.StatusTesting)
          {
            throw new Exception(ex.Message + " at " + ex.StackTrace);
          }
          else
          {
            WindowDebug.ShowMessage(Page, ConstantDictExt.TranslateException(ex, MethodBase.GetCurrentMethod().Name));
            return;
          }
        }
      }

      int i = GlobalAsp.GetRequestI();
      IDataControl dc = UtilityUI.GetDataControl(i);
      string sql = @"
        exec GenerateHTMLHeader 'select * from {0}'
      ";
      string db = GlobalAsp.GetRequestVal();
      db = db.Replace(".", "..");
      sql = string.Format(sql, db);
      string[] fields = new string[] { "Html" };
      List<IDataControl> list = BaseDataAdapter.GetListDC(dc, sql, fields);
      List<BaseDataControl> ListData = list.ConvertAll(new Converter<IDataControl, BaseDataControl>(delegate (IDataControl par) { return (BaseDataControl)par; }));

      string html1 = (string)list[0].GetValue("Html");

      sql = @"
        exec GenerateHTMLTabel 'select * from {0}'
      ";
      db = GlobalAsp.GetRequestVal();
      db = db.Replace(".", "..");
      sql = string.Format(sql, db);
      fields = new string[] { "Html" };
      list = BaseDataAdapter.GetListDC(dc, sql, fields);
      ListData = list.ConvertAll(new Converter<IDataControl, BaseDataControl>(delegate (IDataControl par) { return (BaseDataControl)par; }));

      string html2 = (string)list[0].GetValue("Html");

      string strhtml = @"
        <table border=1>
        <head>{0}</thead>
          {1}
        </table>
      ";
      Content.InnerHtml = string.Format(strhtml, html1, html2);
    }
  }
}