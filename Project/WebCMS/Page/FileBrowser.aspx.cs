using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Linq;
using System.Configuration;
using System.IO;
using System.Security.Permissions;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using CoreNET.Common.Base;
using CoreNET.Common.BO;

public partial class Page_FileBrowser : System.Web.UI.Page
{
  private int NewIndex
  {
    get
    {
      return (int)(Session["newIndex"] ?? 1);
    }
    set
    {
      Session["newIndex"] = value;
    }
  }

  private string _CurrentDir;
  private string CurrentDir
  {
    get
    {
      if (Request["mode"] == "copylink")
      {
        return _CurrentDir;
      }
      else
      {
        return (string)Session[GlobalExt.CURRENT_DIR];
      }
    }
    set
    {
      if (Request["mode"] == "copylink")
      {
        _CurrentDir = value;
      }
      else
      {
        Session[GlobalExt.CURRENT_DIR] = value;
      }
    }
  }

  Dictionary<string, string> MapGambar = new Dictionary<string, string>()
    {
      { ".css", "page_white_code" },
      { ".csv", "page_white_excel" },
      { ".doc", "page_white_word" },
      { ".docx", "page_white_word" },
      { ".exe", "application" },
      { ".flv", "film" },
      { ".gif", "picture" },
      { ".htm", "html" },
      { ".html", "html" },
      { ".jpeg", "picture" },
      { ".jpg", "picture" },
      { ".js", "page_white_code" },
      { ".mp3", "music" },
      { ".mp4", "film" },
      { ".mpeg", "film" },
      { ".mpg", "film" },
      { ".msi", "application" },
      { ".pdf", "page_white_acrobat" },
      { ".png", "picture" },
      { ".ppt", "page_white_powerpoint" },
      { ".pptx", "page_white_powerpoint" },
      { ".rar", "package_green" },
      { ".txt", "page_white_text" },
      { ".wma", "music" },
      { ".wmv", "film" },
      { ".xls", "page_white_excel" },
      { ".xlsx", "page_white_excel" },
      { ".xml", "page_white_code" },
      { ".zip", "package_green" }
    };
  public IDataControlUIEntry GetDataControlForDir()
  {
    IDataControlUIEntry dc = null;
    if (Request["current"] != null)
    {
      int id = GlobalExt.GetRequestI();
      dc = (IDataControlUIEntry)UtilityUI.GetDataControl(id);
    }
    return dc;
  }
  protected void Page_Load(object sender, EventArgs e)
  {
    if (GlobalAsp.CekSessionPage(Page))
    {

      bool uploadEnabled = true;
      if ((Request["uploadEnabled"] != null && Request["uploadEnabled"] == "0")
        || (Session["viewmode"] != null && int.Parse(Session["viewmode"].ToString()) > 2)
        || Request["mode"] == "copylink"
        || Request["mode"] == "copytemp"
        )
      {
        uploadEnabled = false;
      }

      if (!X.IsAjaxRequest)
      {
        string datadir = GlobalAsp.GetDataDir();
        if (Request["path"] != null)//Prioritas path
        {
          CurrentDir = string.IsNullOrEmpty(Request["path"]) ? "File" : Request["path"];
        }
        else if (!string.IsNullOrEmpty(Request["pname"]))
        {
          IDataControlUIEntry dc = GetDataControlForDir();
          if (dc != null)
          {
            string pname = Request["pname"];
            CurrentDir = (string)dc.GetProperty(pname).GetValue(dc, null);
            uploadEnabled = dc.IsEditable();
          }
        }
        else if (!string.IsNullOrEmpty(Request["pathpname"]))
        {
          IDataControlUIEntry dc = GetDataControlForDir();
          if (dc != null)
          {
            string pname = Request["pathpname"];
            CurrentDir = (string)dc.GetProperty(pname).GetValue(dc, null);
          }
        }


        //Template = "<a href='#' data-download='{download}' onclick='opentab(this)'><img style='width:16px;height:16px;' alt='download' src='../icons/basket_put-png/ext.axd' /></a>"
        ListViewColumn colDownload = new ListViewColumn()
        {
          Align = Ext.Net.TextAlign.Center,
          Header = "",
          Width = 0.04,
          DataIndex = "download",
          Template = "<a href='{download}' onclick='opentab(\"Download\",\"{download}\")' target='_blank'><img style='width:16px;height:16px;' alt='download' src='../icons/basket_put-png/ext.axd' /></a>"
        };
        ListViewColumn colCopy = new ListViewColumn()
        {
          Align = Ext.Net.TextAlign.Center,
          Header = "",
          Width = 0.04,
          DataIndex = "download",
          Template = "<a href='#' data-download='{download}' onclick='CoreNET.Download(\"{download}\");'><img style='width:16px;height:16px;' alt='download' src='../icons/basket_put-png/ext.axd' /></a>"
        };
        ListViewColumn colLink = new ListViewColumn()
        {
          Align = Ext.Net.TextAlign.Center,
          Header = "",
          Width = 0.04,
          DataIndex = "download",//"rellink",
          Template = "<a href='#' data-download='{download}' onclick='copylink(this);CoreNET.CopyClipboard(\"{download}\");'><img style='width:16px;height:16px;' alt='link' src='../icons/link_add-png/ext.axd' /></a>"
        };
        ListViewColumn colDelete = new ListViewColumn()
        {
          Align = Ext.Net.TextAlign.Center,
          Header = "",
          Width = 0.04,
          DataIndex = "download",
          Template = "<a href='#' data-delete='{name}' onclick='deleteFile(this)'><img style='width:16px;height:16px;' alt='delete' src='../icons/cross-png/ext.axd' /></a>"
        };

        ListViewColumn colNewFrom = new ListViewColumn()
        {
          Align = Ext.Net.TextAlign.Center,
          Header = "",
          Width = 0.04,
          DataIndex = "download",
          Template = "<a href='#' onclick='CoreNET.CopyTemplate();CoreNET.CloseForm();'><img  alt='new' style='width:16px;height:16px;' src='../icons/folder_explore-png/ext.axd' /></a>"
        };

        ListViewColumn copytemp = new ListViewColumn()
        {
          Align = Ext.Net.TextAlign.Center,
          Header = "",
          Width = 0.04,
          DataIndex = "download",
          Template = "<a href='#' data-download='{download}' onclick='CoreNET.CopyTemplateFile(\"{download}\");'><img style='width:16px;height:16px;' alt='copy template' src='../icons/folder_explore-png/ext.axd' /></a>"
        };

        string viewMode = Request.QueryString["mode"];
        switch (viewMode)
        {
          case "readonly":
            break;
          case "copyfile":
            ListView1.Columns.Insert(0, colCopy);//Insert
            break;
          case "copylink":
            ListView1.Columns.Insert(0, colLink);//Insert
            break;
          case "upload":
          case "prepare":
            if (uploadEnabled)
            {
              BtnFileUpload.Show();
            }
            if (!MasterAppConstants.Instance.StatusServer)
            {
              BtnNewExcel.Show();
              BtnNewWord.Show();
            }
            ListView1.Columns.Insert(0, colDelete);
            ListView1.Columns.Insert(0, colDownload);
            ListView1.Columns.Insert(0, colLink);
            break;
          case "review":
            if (uploadEnabled)
            {
              BtnFileUpload.Show();
            }
            ListView1.Columns.Insert(0, colDownload);
            ListView1.Columns.Insert(0, colLink);
            break;
          case "download":
            ListView1.Columns.Insert(0, colDownload);
            ListView1.Columns.Insert(0, colLink);
            break;
          case "newform":
            if (!MasterAppConstants.Instance.StatusServer)
            {
              BtnNewExcel.Show();
              BtnNewWord.Show();
            }
            ListView1.Columns.Insert(0, colNewFrom);
            break;
          case "copytemp":
            ListView1.Columns.Insert(0, copytemp);
            ListView1.Columns.Insert(0, colDownload);
            break;
          default:
            if (uploadEnabled)
            {
              BtnFileUpload.Show();
            }
            ListView1.Columns.Insert(0, colDownload);
            break;
        }

        TreePanel1.Visible = (Request["lcfolder"] == null) || (Request["lcfolder"] != "0");
        tbFolder.Visible = (Request["tbfolder"] == null) || (Request["tbfolder"] != "0");
        tbFile.Visible = (Request["tbfile"] == null) || (Request["tbfile"] != "0");

        if (uploadEnabled)//"IsMasterServer"
        {
          BtnFileUpload.Show();
        }

        if (GlobalExt.GetSessionGroup() != null)
        {
          BtnTreeAdd.Visible = false;
          BtnTreeRename.Visible = false;
          BtnTreeDelete.Visible = false;
        }
        this.RefreshListView(CurrentDir);

        string title = (!string.IsNullOrEmpty(Request["path"])) ? Request["path"] : CurrentDir;
        Ext.Net.TreeNode root = new Ext.Net.TreeNode() { Text = title, Icon = Icon.Folder, Expanded = true, NodeID = "root" };
        this.TreePanel1.Root.Clear();
        this.TreePanel1.Root.Add(root);
        this.ScanFolder(root, GlobalAsp.GetDataDir() + CurrentDir);

        TreePanel1.SelectNode("root");

      }
    }
    else
    {
      X.Js.Call("home");
    }
  }

  protected void BtnFileRefreshClick(object sender, DirectEventArgs e)
  {
    this.RefreshListView(CurrentDir);
  }

  protected void BtnFileUploadClick(object sender, DirectEventArgs e)
  {
    X.Js.Call(@"(function(){
      if(parent.CoreNET.PnlDesLebar) {
        parent.CoreNET.PnlDesLebar();
        parent.CoreNET.ExpandFrame();
      }
      if (!!parent.GridPanel1){
        if (!!parent.GridPanel1.getSelectionModel().getSelected()|| !!parent.GridPanel1.record) {
          #{WindowUploadFile}.show();
        }else{
          #{WindowUploadFile}.show();
        }
      }else{
        #{WindowUploadFile}.show();
      }
    })");

    //" Ext.Msg.alert('" + ConstantDictExt.Translate(GlobalExt.LBL_INFO) + "','" + ConstantLanguages.TxtLblSelectVersionRef + @"');
    //this.WindowUploadFile.Show();
  }

  protected void BtnFolderAddClick(object sender, EventArgs e)
  {
    this.WindowAddFolder.Show();
  }

  protected void BtnFolderDeleteClick(object sender, EventArgs e)
  {
    try
    {
      Directory.Delete(GlobalAsp.GetDataDir() + CurrentDir, true);
      this.BtnTreeRefresh.FireEvent(HtmlEvent.Click.ToString());
    }
    catch (Exception ex)
    {
      WindowDebug.ShowMessage(Page, ConstantDictExt.TranslateException(ex, MethodBase.GetCurrentMethod().Name));
    }
  }

  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void BtnFolderRenameClick(string nodeid)
  {
    TreePanel1.StartEdit(nodeid);
  }

  #region Property ClipBoardText
  private string _ClipBoardText;

  public string ClipBoardText
  {
    get { return _ClipBoardText; }
    set { _ClipBoardText = value; }
  }

  #endregion

  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void Download(string url)
  {
    try
    {
      IDataControl dc = (IDataControl)GetDataControlForDir();
      string FolderRef = (string)dc.GetProperty("FolderRef").GetValue(dc, null);
      string targetdir = GlobalAsp.GetDataDir() + FolderRef;
      if (!Directory.Exists(targetdir))
      {
        Directory.CreateDirectory(targetdir);
      }
      Uri uri = new Uri(url);
      targetdir += Uri.UnescapeDataString(Path.GetFileName(uri.AbsolutePath));
      WebClient myWebClient = new WebClient();
      myWebClient.DownloadFile(url, targetdir);
      X.Msg.Alert(ConstantDictExt.Translate(GlobalExt.LBL_INFO), ConstantDictExt.Translate("INFO_DOWNLOAD_SUCCESS ")).Show();
    }
    catch (Exception ex)
    {
      WindowDebug.ShowMessage(Page, ConstantDictExt.TranslateException(ex, MethodBase.GetCurrentMethod().Name));
    }
  }

  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void CopyClipboard(string text)
  {
    //string url = GlobalAsp.GetDataURL() + text;
    ClipBoardText = text;
    StatusBarTree1.Text = text;
    NormalizeFrame();
    CloseLink();
  }

  protected void BtnNewExcelClick(object sender, DirectEventArgs e)
  {
    if (!string.IsNullOrEmpty(Request["win"]))
    {
      this.CloseForm();
    }
    IDataControl dc = (IDataControl)GetDataControlForDir();
    string targetdir = GlobalAsp.GetDataDir() + dc.GetProperty(Request["targetpathpname"]).GetValue(dc, null);
    if (!Directory.Exists(targetdir))
    {
      Directory.CreateDirectory(targetdir);
    }
    //string srcfile = Server.MapPath("~/Pustaka/Template/New Microsoft Excel Worksheet.xlsx");
    string srcfile = GlobalAsp.GetDataDir() + "Pustaka/Template/New Microsoft Excel Worksheet.xlsx";
    string targetfile = targetdir + "New Microsoft Excel Worksheet.xlsx";
    if (File.Exists(srcfile) && !File.Exists(targetfile))
    {
      File.Copy(srcfile, targetfile);
    }
    this.BtnFileRefreshClick(sender, e);
  }

  protected void BtnNewWordClick(object sender, DirectEventArgs e)
  {
    if (!string.IsNullOrEmpty(Request["win"]))
    {
      this.CloseForm();
    }
    IDataControl dc = (IDataControl)GetDataControlForDir();
    string targetdir = GlobalAsp.GetDataDir() + dc.GetProperty(Request["targetpathpname"]).GetValue(dc, null);
    if (!Directory.Exists(targetdir))
    {
      Directory.CreateDirectory(targetdir);
    }
    //string srcfile = Server.MapPath("~/Pustaka/Template/New Microsoft Word Document.docx");
    string srcfile = GlobalAsp.GetDataDir() + "Pustaka/Template/New Microsoft Word Document.docx";
    string targetfile = GlobalAsp.GetDataDir() + dc.GetProperty(Request["targetpathpname"]).GetValue(dc, null) + "New Microsoft Word Document.docx";
    if (File.Exists(srcfile) && !File.Exists(targetfile))
    {
      File.Copy(srcfile, targetfile);
    }
    this.BtnFileRefreshClick(sender, e);
  }

  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void BtnWindowAddClick(string namaFolder)
  {
    this.WindowAddFolder.Hide();
    string path = GlobalAsp.GetDataDir() + CurrentDir + "/" + namaFolder;
    if (Directory.Exists(path))
    {
      X.Msg.Alert(ConstantDictExt.Translate(GlobalExt.LBL_INFO), ConstantDict.Translate("INFO_ADD_FOLDER_SUCCESS")).Show();
    }
    else
    {
      Directory.CreateDirectory(path);
      this.BtnTreeRefresh.FireEvent(HtmlEvent.Click.ToString());
    }
  }
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void ExpandFrame()
  {
    X.Js.Call(@"(function() {
      if (!!parent) 
      {
        if (!!parent.parent) 
        {
          if(!!parent.parent.MyMethods){
            if (location.search.indexOf('child') !== -1) {
              parent.parent.MyMethods.ExpandTopFrame();
            } else {
              parent.parent.MyMethods.ExpandBotFrame();
            }
          }
        }
      }
    })");
  }
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void NormalizeFrame()
  {
    X.Js.Call(@"(function() {
      if (!!parent) 
      {
        if (!!parent.parent) 
        {
          if(!!parent.parent.MyMethods){
            parent.parent.MyMethods.NormalizeFrame();
          }
        }
      }
    })");
  }

  protected void BtnWindowUploadClick(object sender, DirectEventArgs e)
  {
    BtnWindowUploadClick();
  }
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void BtnWindowUploadClick()
  {
    Button4.Disabled = true;
    if (this.FileUploadField1.HasFile)
    {
      bool sukses = true;
      HttpPostedFile file = FileUploadField1.PostedFile;
      string folder = GlobalAsp.GetDataDir() + CurrentDir + "/";
      if (!Directory.Exists(folder))
      {
        Directory.CreateDirectory(folder);
      }
      string path = folder + Path.GetFileName(file.FileName);
      try
      {
        if (File.Exists(path))
        {
          throw new Exception(string.Format(ConstantDictExt.Translate("ERROR_FILE_EXIST"), file.FileName));
        }
        if (file.ContentLength > 30000000) //Batasi ukuran file 30MB
        {
          X.Msg.Alert(ConstantDictExt.Translate(GlobalExt.LBL_INFO), ConstantDictExt.Translate("ERROR_30MB_FILE_SIZE")).Show();
          return;
        }


        file.SaveAs(path);
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
        this.WindowUploadFile.Hide();
        if (sukses)
        {
          if (file.FileName.Substring(file.FileName.Length - 4).ToLower().Equals(".doc"))
          {
            //var doc = new OleDocumentPropertiesClass();
            //doc.Open(path, false, dsoFileOpenOptions.dsoOptionDefault);
            //doc.SummaryProperties.Comments = TextField1.Text;
            //doc.Save();
          }
          X.Msg.Alert(ConstantDictExt.Translate(GlobalExt.LBL_INFO), ConstantDictExt.Translate("INFO_UPLOAD_SUCCESS ")).Show();
          this.RefreshListView(CurrentDir);
        }
      }
      X.Js.Call(@"(function(){
        if(parent.CoreNET.PnlDesNyempit) {
          parent.CoreNET.PnlDesNyempit();
          parent.CoreNET.NormalizeFrame();
        }
      })");

      //string url = ;
      //string username = ;
      //string password = ;

      //string filePath = FileUploadField1.PostedFile.FileName;
      //if (filePath != String.Empty)
      //  UploadFileToFtp(url, filePath, username, password);

    }
    X.Get("loading-mask-upload").SetStyle("display", "none");
    Button4.Disabled = false;
  }

  public static void UploadFileToFtp(string url, string filePath, string username, string password)
  {
    using (WebClient client = new WebClient())
    {
      client.Credentials = new NetworkCredential(username, password);
      client.UploadFile("ftp://ftpserver.com/target.zip", "STOR", filePath);
    }
  }

  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void BukaFolder()
  {
    IDataControl dc = (IDataControl)GetDataControlForDir();
    string FolderRef = (string)dc.GetProperty("FolderRef").GetValue(dc, null);
    string targetdir = GlobalAsp.GetDataDir() + FolderRef;
    if (!Directory.Exists(targetdir))
    {
      Directory.CreateDirectory(targetdir);
    }
    //Process.Start("explorer.exe", targetdir.Replace("/","\\"));
  }

  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void CopyTemplate()
  {
    IDataControl dc = GetDataControlForDir();
    string targetdir = GlobalAsp.GetDataDir() + dc.GetProperty(Request["targetpathpname"]).GetValue(dc, null);
    if (!Directory.Exists(targetdir))
    {
      Directory.CreateDirectory(targetdir);
    }
    string srcdir = GlobalAsp.GetDataDir() + Request["path"];
    string[] files = Directory.GetFiles(srcdir);
    foreach (string s in files)
    {
      string extension = Path.GetExtension(s);
      if (extension.ToLower().Contains("xls") || extension.ToLower().Contains("doc"))
      {
        string fileName = Path.GetFileName(s);
        string destFile = Path.Combine(targetdir, fileName);
        File.Copy(s, destFile, true);
      }
    }
  }
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void CopyTemplateFile(string fname)
  {
    IDataControl dc = GetDataControlForDir();
    string targetdir = GlobalAsp.GetDataDir() + dc.GetProperty(Request["targetpathpname"]).GetValue(dc, null);
    if (!Directory.Exists(targetdir))
    {
      Directory.CreateDirectory(targetdir);
    }
    string[] temps = fname.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
    string srcdir = GlobalAsp.GetDataDir() + Request["path"];
    string fileName = temps[temps.Length - 1];
    string scrFile = Path.Combine(srcdir, fileName);
    string destFile = Path.Combine(targetdir, fileName);
    File.Copy(scrFile, destFile, true);
  }
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void CloseLink()
  {
    X.Js.Call(@"(function(){
      if(!!parent.PanelFormEntry){
        parent.WindowLink.hide();
      }
    })");
  }
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void CloseForm()
  {
    X.Js.Call(@"(function(){
      if(!!parent.PanelFormEntry){
        parent.CoreNET.btnSaveForm();
        parent.Store1.reload();
        parent.PanelFormEntry.hide();
     }
    })");
  }

  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void DeleteFile(string namafile)
  {
    string path = GlobalAsp.GetDataDir() + CurrentDir + "/" + namafile;
    try
    {
      File.Delete(path);
      this.RefreshListView(CurrentDir);

    }
    catch (Exception ex)
    {
      WindowDebug.ShowMessage(Page, ConstantDictExt.TranslateException(ex, MethodBase.GetCurrentMethod().Name));
    }
  }

  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void RefreshListView(string url)
  {
    if (!string.IsNullOrEmpty(url))
    {
      CurrentDir = url.Replace("//", "/");
      if (!CurrentDir.EndsWith("/"))
      {
        CurrentDir += "/";
      }

      string docurl = GlobalAsp.GetDataURL();

      string fullurl = docurl + CurrentDir;
      string[] strs = docurl.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
      string baserelativeurl = strs[strs.Length - 1] + "/";
      string relativeurl = baserelativeurl + CurrentDir;
      string path = GlobalAsp.GetDataDir() + url.Replace("//", "/");
      path = path.Replace("%20", " ");
      if (Directory.Exists(path))
      {
        string[] files = Directory.GetFiles(path);
        List<object> data = new List<object>(files.Length);
        //file:///C:/inetpub/Docs/File/09/12/0874/2014/01/A/14/01/001/ITPlan.xlsx

        foreach (string fileName in files)
        {
          FileInfo file = new FileInfo(fileName);
          string fname = file.Name.Replace("'", "%27");
          data.Add(new
          {
            name = file.Name,
            icon = MapGambar.ContainsKey(file.Extension) ? MapGambar[file.Extension] : "page_white",
            size = file.Length,
            credate = file.CreationTime,
            lastmod = file.LastAccessTime,
            download = (fullurl + fname),
            rellink = (relativeurl + fname)
            //download = "http://" + Request.Url.Host + ":" + Request.Url.Port + Request.ApplicationPath + "/" + url + "/" + file.Name
          });
        }

        var store = this.ListView1.GetStore();
        store.DataSource = data;
        store.DataBind();

        url = url.Replace("//", "/");
        string title = ("Path: /" + url).Replace("//", "/");
        Panel1.Title = ((Request["lcfolder"] == null) || (Request["lcfolder"] != "0")) ? title : "";
      }
      else
      {
        url = url.Replace("//", "/");
        string title = ("Path: /" + url).Replace("//", "/");
        Panel1.Title = ((Request["lcfolder"] == null) || (Request["lcfolder"] != "0")) ? title : "";
      }
      try
      {
        int nfiles = (from file in Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly)
                      select file).Count();

        StatusBarTree1.Text = string.Format("{0} files", nfiles);
      }
      catch (IOException)
      {

      }
    }
  }

  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public string RefreshMenu()
  {
    string title = (!string.IsNullOrEmpty(Request["path"])) ? Request["path"] : CurrentDir;
    Ext.Net.TreeNode root = new Ext.Net.TreeNode() { NodeID = title, Text = title, Icon = Icon.Folder, Expanded = true };
    Ext.Net.TreeNodeCollection nodes = new Ext.Net.TreeNodeCollection();
    CurrentDir = GlobalAsp.GetDataDir() + Request["path"];
    nodes.Add(this.ScanFolder(root, GlobalAsp.GetDataDir() + Request["path"]));
    return nodes.ToJson();
  }

  protected Ext.Net.TreeNode ScanFolder(Ext.Net.TreeNode node, string rel)
  {
    string path = rel;
    if (Directory.Exists(path))
    {
      string[] files = Directory.GetFiles(path);
      string[] folders = Directory.GetDirectories(path);

      foreach (string namaFolder in folders)
      {
        DirectoryInfo dir = new DirectoryInfo(namaFolder);
        Ext.Net.TreeNode nodebaru = new Ext.Net.TreeNode() { NodeID = dir.FullName, Text = dir.Name, Icon = Icon.Folder };
        this.ScanFolder(nodebaru, rel + "/" + dir.Name);
        node.Nodes.Add(nodebaru);
      }
    }

    return node;
  }

  protected void TreeNodeClick(object sender, DirectEventArgs e)
  {
    X.Js.Call(@"(function() {
        var getPath = function (node) {
          var parent = node.parentNode;
          var path = node.text;
          if (parent) {
            path = getPath(parent) + '/' + path;
          }
          return path;
        };
        var sel = TreePanel1.getSelectionModel().selNode;

        if(!!sel)
        {
          CoreNET.RefreshListView(encodeURI(getPath(sel)+'/'));
        }
        else
        {
          Ext.Msg.alert('" + ConstantDictExt.Translate(GlobalExt.LBL_INFO) + "','" + ConstantDictExt.Translate("ERROR_DIR_NOT_SELECTED") + @"');
        }
      })");
  }
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void ExpandTree(string path)
  {
    TreePanel1.SelectPath(path);
  }

  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void TreeNodeRename(string newName, string oldName)
  {
    string path = GlobalAsp.GetDataDir() + CurrentDir;
    try
    {
      Directory.Move(path, path.Replace(oldName, newName));
    }
    catch (Exception ex)
    {
      WindowDebug.ShowMessage(Page, ConstantDictExt.TranslateException(ex, MethodBase.GetCurrentMethod().Name));
    }
  }
  [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
  public void OpenTab(string filename)
  {
    string title = Path.GetFileName(filename.Replace("/", "\\"));
    string url = filename.Replace("\\", "/").Replace("../", "").Replace("//", "/");
    X.Js.Call("opentab", title, url);
  }
}