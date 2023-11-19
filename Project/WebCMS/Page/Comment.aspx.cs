using CoreNET.Common.Base;
using CoreNET.Common.BO;
using Ext.Net;
using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.UI;

public partial class System_Comment : System.Web.UI.Page
{
  private WindowDebug WindowDebug1 = null;

  public IDataControlUIEntry GetDataControlForText()
  {
    IDataControlUIEntry dc = null;
    int id = GlobalExt.GetRequestI();
    WindowDebug WindowDebug1 = new WindowDebug(id, GlobalAsp.GetURLSessionKey());

    if (Request["error"] == "1")
    {
      dc = new Ss01appmenuAdminControl();
      dc.SetPageKey();
      dc.SetValue("Idmenu", GlobalAsp.GetRequestId());
    }
    else
    {
      dc = (IDataControlUIEntry)UtilityUI.GetDataControl(id);
      if (dc == null)
      {
        UtilityUI.ProcessInfo();
        dc = (IDataControlUIEntry)UtilityUI.GetDataControl(id);
      }
      IDataControlUI dcprev = (IDataControlUIEntry)UtilityUI.GetDataControlPrev(id);
      if (dcprev == null)
      {
        dcprev = GlobalExt.GetSessionMenu();
      }
      if (dcprev != null)
      {
        dcprev.SetPageKey();
        dc.SetPageKey();
        dc.SetFilterKey((BaseBO)dcprev);
      }
    }
    ((BaseBO)dc).ModePage = ViewListProperties.MODE_COMMENT;
    return dc;
  }
  protected void Page_Load(object sender, EventArgs e)
  {
    if (GlobalAsp.CekSessionPage(Page))
    {
      IDataControlUIEntry dc = GetDataControlForText();
      Toolbar1.Visible = dc.IsEditable() || (Request["tb"] == "1");

      if (!Page.IsPostBack)
      {
        LoadMessages(dc);
      }
      btnCancel.Text = ConstantDictExt.Translate("BTN_CANCEL1");
      btnSave.Text = ConstantDictExt.Translate(GlobalExt.BTN_SAVE1);

      if ((!string.IsNullOrEmpty(Request["cname"])) || (Request["tb"] == null))
      {
        btnAdd.Listeners.Click.Handler =
          "CoreNET.OnAdd();";
      }
      else
      {
        string temp = @"
        if (!!parent.GridPanel1) {
          if(!!parent.GridPanel1.getSelectionModel().getSelected()|| !!parent.GridPanel1.record) {
            CoreNET.OnAdd();
          }else{
            CoreNET.OnAdd();
         }
        }else
        {
          CoreNET.OnAdd();
        }";
        btnAdd.Listeners.Click.Handler = temp;
      }
      if (Request["parentsave"] == "1")
      {
        //btnSave.Listeners.Click.Handler += @"
        //if(!!parent.BTN_BACK1)
        //{
        //  parent.BTN_BACK1.fireEvent('click');
        //}
        //";
        btnCancel.Listeners.Click.Handler = @"
            if(!!parent.BTN_BACK1)
            {
              parent.BTN_BACK1.fireEvent('click');
            }
          ";
      }


      #region Debugging
      if (MasterAppConstants.Instance.StatusTesting)
      {
        Ext.Net.Button btnDebug = new Ext.Net.Button() { ID = "btnDebug" + Toolbar1.ID, Text = "Debugging", Icon = Icon.PageLightning };
        btnDebug.DirectClick += new ComponentDirectEvent.DirectEventHandler(btnDebug_DirectClick);
        Toolbar1.Add(btnDebug);
      }
      #endregion
    }
    else
    {
      X.Js.Call("home");
    }
  }

  protected void LoadMessages(IDataControlUI dc)
  {
    IList listmsgs = NotesUIControl.LoadMsgs(dc);
    string comments = Utility.ConstructCoaching(listmsgs);
    pnlComment.Html = comments;

  }

  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  private void btnDebug_DirectClick(object sender, DirectEventArgs e)
  {
    WindowDebug1.Render(Form);
    WindowDebug1.Show();
  }

  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void OnAdd()
  {
    HtmlEditor1.Hidden = false;
    Toolbar2.Hidden = false;

    btnAdd.Disabled = true;
    btnSave.Disabled = btnCancel.Disabled = false;
  }

  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  protected void OnSave(object sender, EventArgs e)
  {
    HtmlEditor1.Hidden = true;
    Toolbar2.Hidden = true;

    int id = GlobalExt.GetRequestI();
    IDataControlUI dc = GetDataControlForText();

    DateTime tgl = DateTime.Now;
    int lastAnchor = Utility.getLastNotesNumber(pnlComment.Html) + 1;
    string Html = Utility.ConstructCommentEntry(dc, lastAnchor, HtmlEditor1.Text, tgl) + pnlComment.Html;

    try
    {
      NotesUIControl.SaveMsg(dc, HtmlEditor1.Text);
      try
      {
        dc.Update(BaseDataControl.COMMENT);
      }
      catch (Exception)
      {

      }
    }
    catch (Exception ex)
    {
      WindowDebug.ShowMessage(Page, ConstantDictExt.TranslateException(ex, MethodBase.GetCurrentMethod().Name));
    }
    HtmlEditor1.Text = "";
    pnlComment.Html = Html;

    LoadMessages(dc);

    btnAdd.Disabled = false;
    btnSave.Disabled = btnCancel.Disabled = true;
    X.Js.Call(@"(function () {
        if (parent.CoreNET.GridReload) {
            parent.CoreNET.GridReload();
        }
      })");

  }

  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  protected void OnCancel(object sender, EventArgs e)
  {
    HtmlEditor1.Hidden = true;
    Toolbar2.Hidden = true;
    HtmlEditor1.Text = "";
    btnAdd.Disabled = false;
    btnSave.Disabled = btnCancel.Disabled = true;

  }

  protected void BtnWindowUploadClick(object sender, DirectEventArgs e)
  {
    BtnWindowUploadClick();
  }
  public IDataControlUI GetDataControlForDir()
  {
    IDataControlUI dc = null;
    //if (Request["current"] != null)
    //{
    //  int id = GlobalExt.GetRequestI();
    //  dc = (IDataControlUI)UtilityUI.GetDataControl(id);
    //}
    int id = GlobalExt.GetRequestI();
    dc = (IDataControlUI)UtilityUI.GetDataControl(id);
    return dc;
  }
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void BtnWindowUploadClick()
  {
    Button4.Disabled = true;
    if (FileUploadField1.HasFile)
    {
      bool sukses = true;
      string CurrentDir = string.Empty;
      IDataControlUI dc = GetDataControlForDir();
      if (dc != null)
      {
        string pname = Request["pname"];
        if (string.IsNullOrEmpty(pname))
        {
          pname = "Path";
        }
        CurrentDir = (string)dc.GetProperty(pname).GetValue(dc, null);
        string Userid = GlobalExt.GetSessionUser().GetUserID();
        CurrentDir += Userid + "\\";
        DateTime d = ((DateTime)dc.GetValue("Last_date"));
        CurrentDir = "Forum\\" + d.ToString("yyyy") + "\\" + d.ToString("MM") + "\\" + d.ToString("dd") + "\\";
      }

      string path = GlobalAsp.GetDataDir() + CurrentDir;
      if (!Directory.Exists(path))
      {
        Directory.CreateDirectory(path);
      }

      HttpPostedFile file = FileUploadField1.PostedFile;
      string fullpath = path + "/" + Path.GetFileName(file.FileName);
      try
      {
        if (File.Exists(fullpath))
        {
          File.Delete(fullpath);
        }
        if (file.ContentLength > 30000000) //Batasi ukuran file 30MB
        {
          X.Msg.Alert(ConstantDictExt.Translate(GlobalExt.LBL_INFO), ConstantDictExt.Translate("ERROR_30MB_FILE_SIZE")).Show();
          return;
        }

        file.SaveAs(fullpath);

        string url = CurrentDir.Replace("\\", "/");
        string docurl = GlobalAsp.GetDataURL();

        string fullurl = docurl + url + Path.GetFileName(file.FileName);
        string[] strs = docurl.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
        string baserelativeurl = strs[strs.Length - 1] + "/";
        string relativeurl = "../../" + baserelativeurl + url + Path.GetFileName(file.FileName);

        HtmlEditor1.Text += string.Format("\n<a href='{0}' target='_blank'>{1}</a>", relativeurl, Path.GetFileName(file.FileName));
      }
      catch (Exception ex)
      {
        sukses = false;
        WindowDebug.ShowMessage(Page, ConstantDictExt.TranslateException(ex, MethodBase.GetCurrentMethod().Name));
      }
      finally
      {
        X.Get("loading-mask-upload").SetStyle("display", "none");
        Button4.Disabled = false;
        if (sukses)
        {
          X.Msg.Alert(ConstantDictExt.Translate(GlobalExt.LBL_INFO), ConstantDictExt.Translate("INFO_UPLOAD_SUCCESS ")).Show();
        }
      }
      X.Js.Call(@"(function(){
        if(parent.CoreNET.PnlDesNyempit) {
          parent.CoreNET.PnlDesNyempit();
          parent.CoreNET.NormalizeFrame();
        }
      })");

    }
    X.Get("loading-mask-upload").SetStyle("display", "none");
    Button4.Disabled = false;
  }

}