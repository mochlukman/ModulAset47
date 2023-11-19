using CoreNET.Common.Base;
using CoreNET.Common.BO;
using Ext.Net;
using System;
using System.Collections;
using System.Configuration;
using System.Reflection;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CoreNET.Common.Base.AssemblyUtils.WriteBeginLog();

        GlobalExt.InitHome(Page, null);
        if (!Page.IsPostBack)
        {
            //@2019-07-08 Ditambahkan untuk memaksa logout ketika login
            //2019-07-17 Kenapa Login kok ,sh dieksekusi ya..pelajari event logon click
            //Session.Abandon();
            //Session.RemoveAll();//Show Debug Jadi Ngga Muncul

            MasterAppConstants.Instance.ShowContextMenu = true;
            MasterAppConstants.Instance.ShowTranslatedLabel = true;
            string sufix = string.Empty;

            string baseurl = GlobalAsp.GetBaseURL();
            /*
             * Kasus 1: server ngga bisa akses url selain localhost
             * Kasus 2: portal
             * 
             */
            string idapp = GlobalAsp.GetIdApp();
            if (MasterAppConstants.Instance.StatusTesting)
            {
                MasterAppConstants.Instance.ShowContextMenu = (ConfigurationManager.AppSettings["ShowContextMenu"] == "1");
                MasterAppConstants.Instance.ShowTranslatedLabel = (ConfigurationManager.AppSettings["ShowTranslatedLabel"] == "1");


                //mainDiv.Style.Add("background-image", baseurl + "App_Portal/Img/bg2.png");
                //mainDiv.Style.Add("width", "1366px");
                //mainDiv.Style.Add("height", "600px");
                //mainDiv.Style.Add("background-image", baseurl + "App_Portal/Img/bg.jpg");
                //mainDiv.Style.Add("width", "1366px");
                //mainDiv.Style.Add("height", "600px");
            }
            if (!string.IsNullOrEmpty(idapp))
            {
                MasterAppConstants.Instance.StatusAdmin = false;
                GlobalAsp.SetConfiguration(idapp, false);

                string dcdict = (string)SsappconfigLookupControl.Instance.Params["DCDictionary"];
                if (!string.IsNullOrEmpty(dcdict) && !MasterAppConstants.Instance.DictionaryDC.Equals(dcdict))
                {
                    MasterAppConstants.Instance.SetValue(MasterAppConstants.DICTIONARYDC, dcdict);
                    ConstantDict.SetInstanceNullForReloadDict();
                }

                if (ConfigurationManager.AppSettings["ShowKodeApp"] != "1")
                {
                    Title = (string)GlobalAsp.GetSessionAppValue(MasterAppConstants.APPTITLE);
                    if (MasterAppConstants.Instance.StatusTesting)
                    {
                        Title += " (Versi Beta)";
                    }
                    //Window1.Title = "Login " + Title;
                }
                else
                {
                    string idappfromreq = "0";
                    string idappcosfail = "0";
                    CoreNET.Common.Base.SQLDataSource.Instance.SetDataSourceConfig(CoreNET.Common.Base.GlobalAsp.GetDataSourceConfig());
                    string sesname = GlobalAsp.SESSION_APP_PARAMS;
                    string sub = GlobalAsp.GetRequestSub();
                    string cname = MasterAppConstants.Instance.AppDC;
                    string db = SQLDataSource.Instance.CS_Config;
                    string publickey = GlobalLib.GetPublishedKey();
                    string info = string.Format("(Kdapp='{0}';Idapp={1};Sesname={2};ByReq={3};" +
                      "ByFail={4};Sub={5};Cname={6};DBName={7};Key={8})",
                      GlobalAsp.GetRequestKdapp(), idapp, sesname, idappfromreq, idappcosfail, sub, cname, db, publickey);
                    WindowDebug.ShowMessage(Page, info);
                }
            }
            else
            {
                //Error ini, karena kdapp tidak terdapatdi database
                string msg = ConstantDict.Translate(GlobalAsp.MSG_ERRORAPP);
                X.Msg.Alert(GlobalAsp.GetConfigLabelInfo(), msg).Show();
                return;
            }
        }
        //Tadinya mau pake GetEntries. Tapi untuk versi public, pake logn standar aja.

        CoreNET.Common.Base.AssemblyUtils.WriteEndLog();
    }
    [DirectMethod(Timeout = 3600000)]
    public void ProcessLogin() { }
    protected void btnLogin_Click(object sender, DirectEventArgs e)
    {
        CoreNET.Common.Base.AssemblyUtils.WriteBeginLog();

        bool ok = false;
        string key = utxt_Code.Value;
        try
        {
            if (string.IsNullOrEmpty(txtUser.Text))
            {
                X.Msg.Alert(GlobalAsp.GetConfigLabelInfo(), ConstantDictExt.Translate("LBL_EMPTY_USERID")).Show();
                return;
            }
            #region Authentication
            /*User Key ini di log di GlobalAsp.CekHashData*/
            GlobalExt.SetSessionUser(GlobalAsp.GetSessionApp(), key);
            #endregion

            ok = true;
        }
        catch (Exception ex)
        {
            try
            {
                string msg = string.Empty;
                if (MasterAppConstants.Instance.StatusTesting)
                {
                    msg = string.Format(@"Testing: Error executing '{0}'. Cause: {1}; In: {2} ", MethodBase.GetCurrentMethod().Name, ex.Message, ex.StackTrace);
                    WindowDebug.ShowMessage(Page, msg);
                }
                else
                {
                    msg = ConstantDictExt.Translate(ex.Message);
                    X.Msg.Alert(GlobalAsp.GetConfigLabelInfo(), msg).Show();
                }
            }
            catch (Exception)
            {
                if (ex.Message.Contains("Unable to open connection"))
                {
                    Response.Redirect(GlobalExt.GetHomeURL());
                }
            }
        }
        if (ok)
        {
            string app = GlobalAsp.GetRequestApp();
            if (string.IsNullOrEmpty(app))
            {
                app = GlobalAsp.GetSessionApp();
            }

            string url = string.Empty;
            string sub = ConfigurationManager.AppSettings["IsEtalase"];
            switch (GlobalAsp.GetRequestMode())
            {
                case "2":
                    url = "Index.aspx" +
                      string.Format("?app={0}&key={1}&sub={2}&kdapp={3}&frame={4}", MasterAppConstants.MASTERAPPID,
                      key, sub, GlobalAsp.GetRequestKdapp(), GlobalAsp.GetRequestFrame());//Modul Admin
                    break;
                default:
                    url = GlobalAsp.GetMenuURL() +
                      string.Format("?app={0}&key={1}&sub={2}&kdapp={3}&frame={4}", app, key,
                      sub, GlobalAsp.GetRequestKdapp(), GlobalAsp.GetRequestFrame());//Modul Admin
                    break;
            }
            CoreNET.Common.Base.AssemblyUtils.WriteEndLog();

            //Window1.Hide();
            Response.Redirect(url);

        }
    }

    protected void btnCancel_Click(object sender, DirectEventArgs e)
    {
        string sub = ConfigurationManager.AppSettings["IsEtalase"];
        string urlout = GlobalAsp.GetLogoutURL() + "?local=" + Request["local"] + "&kdapp=" + GlobalAsp.GetRequestKdapp()
          + "&sub=" + sub;
        Response.Redirect(urlout);
    }
}