using CoreNET.Common.Base;
using CoreNET.Common.BO;
using Ext.Net;
using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Web;

using System.Web.UI.WebControls;

public partial class MainMenu : System.Web.UI.Page
{
    private WindowDebug WindowDebug1 = null;
    private WindowAdmin[] WindowAdmin1 = new WindowAdmin[] {
    new WindowAdmin(0,BOSysUtils.ID_APP_CONFIG),
    new WindowAdmin(1,BOSysUtils.ID_DICTIONARY)
  };
    private Hashtable WinFilters;
    protected void Page_Load(object sender, EventArgs e)
    {
        CoreNET.Common.Base.AssemblyUtils.WriteBeginLog();

        try
        {
            if (GlobalAsp.CekSessionMenu(Page))
            {
                WindowDebug1 = new WindowDebug(1, GlobalAsp.GetURLSessionKey());
                #region StatusBar
                if (MasterAppConstants.Instance.StatusTesting)
                {
                    Ext.Net.Button btnSign = new Ext.Net.Button() { ID = "btnSign" };
                    btnSign.OnClientClick = string.Format(@"
          Ext.Msg.alert('Current User Key','{0}');
          ", GlobalExt.GetSessionKey());
                    StatusBar2.Items.Add(btnSign);

                    Ext.Net.Button btnConfig = new Ext.Net.Button() { ID = "btnConfig" };
                    string url = string.Format("Page/PageTabular.aspx?app={0}&id={1}", Request["app"], "0A992D53-DB54-4303-A3B9-F575AAF7EC1A");
                    btnConfig.OnClientClick = string.Format(@"
          loadPageByID('Pages','Config','Config','{0}');
        ", url);
                    StatusBar2.Items.Add(btnConfig);
                }
                #endregion
                IDataControlAppuserUI cUserdb = GlobalExt.GetSessionUser();
                IDataControlMenu dcMenu = (IDataControlMenu)GlobalExt.GetSessionMenu();
                UtilityUI.SetDataControl(1, dcMenu);
                HashTableofParameterRow h = dcMenu.GetFilters();
                btnLookupApp.Hidden = (h.Count == 0) || !cUserdb.IsVisibleLookup();
                StatusBar1.Hidden = GlobalAsp.GetRequestSub().Equals("1");
                TopPanel.Height = (StatusBar1.Hidden) ? new Unit(32, UnitType.Pixel) : new Unit(64, UnitType.Pixel);
                StatusBar2.Hidden = StatusBar1.Hidden;
                if (h.Count > 0)
                {
                    string[] keys = new string[h.Count];
                    h.Keys.CopyTo(keys, 0);
                    btnLookupApp.Listeners.Click.Handler = string.Format("CoreNET.btnLookup_DirectClick('{0}')", keys[0]);
                    WinFilters = ExtWindows.CreateLookupFilterWindow(Page, 1, dcMenu, ExtGridPanelFilter.TREE, false);
                }
                #region !X.IsAjaxRequest
                if (!Page.IsPostBack && !X.IsAjaxRequest)
                {
                    //Harusnya khusus modul admin
                    if (MasterAppConstants.Instance.StatusTesting)
                    {
                        try
                        {
                            ConstantDict.SetInstanceNullForReloadDict();
                            BaseDataAdapter.ExecuteCmd(dcMenu, string.Format("exec RevalidateSSApp"));
                            BaseDataAdapter.ExecuteCmd(dcMenu, string.Format("exec RevalidateSSAppconfig '{0}'", string.Empty));
                            BaseDataAdapter.ExecuteCmd(dcMenu, string.Format("exec RevalidateSSAppMenu '{0}'", string.Empty));
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message.Contains("")) { }
                        }
                    }


                    string title = (string)GlobalAsp.GetSessionAppValue(MasterAppConstants.APPTITLE);
                    string idappProp = (string)GlobalAsp.GetSessionAppValue(MasterAppConstants.IDAPP_PROPERTY);
                    string nmappProp = (string)GlobalAsp.GetSessionAppValue(MasterAppConstants.NMAPP_PROPERTY);
                    textFilterIdapp.DataIndex = idappProp;
                    textFilterNmapp.DataIndex = nmappProp;

                    Title = title;
                    string idapp = string.Empty;
                    if (dcMenu.GetValue(textFilterIdapp.DataIndex) == null)
                    {
                        idapp = (string)dcMenu.GetValue(textFilterIdapp.DataIndex);
                    }
                    else//Misal tahun
                    {
                        idapp = dcMenu.GetValue(textFilterIdapp.DataIndex).ToString();
                    }
                    if (string.IsNullOrEmpty(idapp))
                    {
                        object oid = dcMenu.GetValue(textFilterIdapp.DataIndex);
                        idapp = (oid == null) ? string.Empty : oid.ToString();
                    }
                    //Set AppID inside
                    dcMenu.SetOtorisasiMenu(Page, idapp);
                    btnLookupApp.Disabled = !cUserdb.IsEnableLookup();
                    textFilterIdapp.Text = idapp;
                    if (!string.IsNullOrEmpty(idapp))
                    {
                        textFilterNmapp.Text = dcMenu.GetAppName(textFilterIdapp.Text);
                        textFilterNmapp.ToolTip = textFilterNmapp.Text;
                    }
                    Title = dcMenu.GetAppTitle(textFilterIdapp.Text);
                    Title = string.IsNullOrEmpty(Title) ? ConstantDict.Translate("Select") + " " + textFilterIdapp.DataIndex : Title;
                    if (MasterAppConstants.Instance.StatusTesting)
                    {
                        Title += " (Versi 1.0523)";
                    }
                    Panel1.Html = @"<div id='Div1' class='header' style='height:32px;border:0;width:100%'>
										<a style='float:right;margin-right:10px' href='http://www.usadi.co,id' target='_blank'></a>
										<div class='api-title' style='font-size:16px;font-weight:bold'>" + Title + "</div></div>";
                    string script = @"
            if(#{Pages}.activeTab!=null){
              CoreNET.ExpandTreePanel(#{Pages}.activeTab.id);
              var title=#{Pages}.activeTab.id
              var obj = document.getElementById(title+'_IFrame')
              if(!!obj){
                if(!!obj.contentWindow.refreshDataWithSelection){
                  obj.contentWindow.refreshDataWithSelection();
                }else{
                  obj.contentWindow.location.reload();
                }
              }
            }
          ";
                    Pages.Listeners.TabChange.Handler = script;


                    try
                    {
                        CreateToolbar();
                        CreateNode(TreePanel1);

                        if (!string.IsNullOrEmpty(idapp))
                        {
                            string roleid = string.Empty;
                            string url = string.Empty;
                            dcMenu.GetDefaultURL(out roleid, out url);
                            if (!string.IsNullOrEmpty(url))
                            {
                                UtilityUI.SetSessionMenuPage(url, roleid, true);
                                UtilityExt.LoadUrl(tabHome, url);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (MasterAppConstants.Instance.StatusTesting)
                        {
                            throw ex;
                        }
                        else
                        {
                            TreePanel1.Hidden = true;
                            WindowDebug.ShowMessage(Page, ConstantDictExt.TranslateException(ex, MethodBase.GetCurrentMethod().Name));
                            return;
                        }
                    }
                }
                #endregion
            }
            else
            {
                //X.Js.Call("home");
                string urlout = GlobalAsp.GetLogoutURL() + "?local=" + Request["local"] + "&kdapp=" + GlobalAsp.GetRequestKdapp()
                  + "&sub=" + GlobalAsp.GetRequestSub();
                Response.Redirect(urlout);
            }
        }
        catch (Exception ex)
        {
            if (ex.Message.Equals("SESSION_EXPIRED"))
            {
                string basehomeURL = GlobalAsp.GetHomePortalURL();
                if (string.IsNullOrEmpty(basehomeURL))
                {
                    basehomeURL = GlobalAsp.GetHomeURL();
                }
                Response.Redirect(basehomeURL);
            }
            TreePanel1.Visible = false;
            TreePanel1.Hidden = true;

            string msg = string.Empty;
            if (MasterAppConstants.Instance.StatusTesting)
            {
                msg = ConstantDictExt.TranslateException(ex, MethodBase.GetCurrentMethod().Name);
                X.Msg.Alert(GlobalAsp.GetConfigLabelInfo(), msg).Show();
            }
            else
            {
                string basehomeURL = GlobalAsp.GetHomePortalURL();
                if (string.IsNullOrEmpty(basehomeURL))
                {
                    basehomeURL = GlobalAsp.GetHomeURL();
                }
                Response.Redirect(basehomeURL);
            }
        }

        CoreNET.Common.Base.AssemblyUtils.WriteEndLog();
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void btnLookup_DirectClick(string par)
    {
        if (GlobalAsp.CekSessionGrid(Page))
        {
            ((Window)WinFilters[par]).Render(Page.Form);
            ((Window)WinFilters[par]).Show();
        }
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void FilterClick(string key)
    {
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public string SettingGroup()
    {
        string json = string.Empty;
        try
        {
            IDataControlMenu cMenu = (IDataControlMenu)GlobalExt.GetSessionMenu();
            IDataControlAppuserUI cUserdb = GlobalExt.GetSessionUser();

            cMenu.SetOtorisasiMenu(Page, textFilterIdapp.Text);
            Title = cMenu.GetAppTitle(textFilterIdapp.Text);
            if (MasterAppConstants.Instance.StatusTesting)
            {
                Title += " (Versi Beta)";
            }
            Panel1.Html = @"<div id='Div1' class='header' style='height:32px;border:0;width:100%'>
										<a style='float:right;margin-right:10px' href='http://www.usadi.co,id' target='_blank'></a>
										<div class='api-title' style='font-size:16px;font-weight:bold'>" + Title + "</div></div>";

            json = RefreshTree();

            #region Self Redirect => Refresh
            string roleid = string.Empty;
            string url = string.Empty;
            cMenu.GetDefaultURL(out roleid, out url);
            //X.Js.Call("closeAllPages");
            if (!string.IsNullOrEmpty(url))
            {
                UtilityUI.SetSessionMenuPage(url, roleid, true);
                UtilityExt.LoadUrl(tabHome, url);
                tabHome.LoadContent();//double load
            }
            #endregion
        }
        catch (Exception ex)
        {
            try
            {
                WindowDebug.ShowMessage(Page, ConstantDictExt.TranslateException(ex, MethodBase.GetCurrentMethod().Name));
            }
            catch (Exception)
            {
                string urlout = GlobalAsp.GetLogoutURL() + "?local=" + Request["local"] + "&kdapp=" + GlobalAsp.GetRequestKdapp()
                  + "&sub=" + GlobalAsp.GetRequestSub();
                Response.Redirect(urlout);
            }
        }
        return json;
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public string RefreshTree()
    {
        if (GlobalAsp.CekSession())
        {
            IList list = null;
            Ext.Net.TreeNode root = null;
            IDataControlAppuserUI cUserdb = GlobalExt.GetSessionUser();
            IDataControlMenu cMenu = (IDataControlMenu)GlobalExt.GetSessionMenu();
            if (!string.IsNullOrEmpty(textFilterNmapp.Text))
            {
                cMenu.SetPageKey();
                list = (IList)cMenu.View();
                //list = (IList)GlobalExt.GetSessionListMenu();
                //if (list == null)
                //{
                //  cMenu.SetPageKey();
                //  list = (IList)cMenu.View();
                //}
            }
            else
            {
                list = null;
            }
            UtilityUI.SetDataControl(1, cMenu);
            GlobalExt.SetSessionListMenu(list);

            if (list != null && list.Count > 0)
            {
                root = ((IDataControlMenu)cMenu).CreateMenu(list, ExtTreePanelUtil.TYPE_TREEMENU, ((ViewListProperties)cMenu.GetProperties()).HasTreeRoot);
            }
            else
            {
                root = new Ext.Net.TreeNode();
            }
            TreePanel1.Title = root.Text;
            Ext.Net.TreeNodeCollection nodes = new Ext.Net.TreeNodeCollection();
            if (root != null)
            {
                nodes.Add(root);
            }
            string json = nodes.ToJson();
            X.Get("loading-mask").SetStyle("display", "none");
            return json;
        }
        else
        {
            X.Get("loading-mask").SetStyle("display", "none");
            return string.Empty;
        }
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void Refresh()
    {
        X.Get("loading-mask").SetStyle("display", "none");

        GlobalExt.RefreshSession();
        try
        {
            #region SetAppID
            IDataControlMenu cMenu = (IDataControlMenu)GlobalExt.GetSessionMenu();
            cMenu.SetOtorisasiMenu(Page, textFilterIdapp.Text);
            #endregion
            #region Self Redirect
            string url = Request.UrlReferrer.OriginalString;
            Response.Redirect(url);
            #endregion
        }
        catch (Exception ex)
        {
            X.Msg.Alert(ConstantDictExt.Translate(GlobalExt.LBL_INFO), ex.Message).Show();
            //WindowDebug.ShowMessage(Page,ConstantDictExt.TranslateException(ex, MethodBase.GetCurrentMethod().Name));
        }
    }
    #region Dynamic Load
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void CreateToolbar()
    {
        #region Toolbar
        #region Standar Button
        Ext.Net.Button btnExpand = new Ext.Net.Button
        {
            ToolTip = "Expand All",
            IconCls = "icon-expand-all"
        };
        btnExpand.Listeners.Click.Handler = "PnlWest.expand();TreePanel1.expandAll();";

        Ext.Net.Button btnCollapse = new Ext.Net.Button
        {
            ToolTip = "Collapse All",
            IconCls = "icon-collapse-all"
        };
        btnCollapse.Listeners.Click.Handler = "PnlWest.expand();TreePanel1.collapseAll();";

        Ext.Net.Button btnShowMenu = new Ext.Net.Button
        {
            ToolTip = "Show Menu",
            Icon = Icon.ApplicationSideTree// ApplicationSideExpand;
        };
        btnShowMenu.Listeners.Click.Handler = "PnlWest.expand()";//"TreePanel1.expand();"; 

        Ext.Net.Button btnHideMenu = new Ext.Net.Button
        {
            ToolTip = "Hide Menu",
            Icon = Icon.Application
        };
        btnHideMenu.Listeners.Click.Handler = "PnlWest.collapse()";//TreePanel1.collapse();

        btnRefresh.ToolTip = ConstantDictExt.TranslateTabTip(btnRefresh);
        btnRefresh.Listeners.Click.Handler = @"onClick();CoreNET.Refresh()";

        Ext.Net.Label lblWait = new Ext.Net.Label() { ID = "lblWait" };
        lblWait.Html = @"<div id='loading-mask' style='display:none'><div id='loading'><div class='loading-indicator'>Loading..</div></div></div>";

        Ext.Net.Button btnFile = new Ext.Net.Button
        {
            ToolTip = ConstantDictExt.Translate(GlobalExt.BTN_FILE_MANAGEMENT),
            Icon = Icon.Folder
        };
        btnFile.Listeners.Click.Handler = string.Format("e.stopEvent(); loadPage(#{0}, '{1}','{1}','Page/FileBrowser.aspx?path=File&tbfolder=1&lcfolder=1&mode=prepare');", "{Pages}", btnFile.ToolTip);

        bool isframe = !string.IsNullOrEmpty(Request["frame"]) && Request["frame"].Equals("1");

        Ext.Net.Button btnLogout = new Ext.Net.Button
        {
            ToolTip = "Logout",
            Icon = Icon.House
        };
        string urlout = GlobalAsp.GetLogoutURL() + "?local=" + Request["local"] + "&kdapp=" + GlobalAsp.GetRequestKdapp()
          + "&sub=" + GlobalAsp.GetRequestSub();
        btnLogout.Listeners.Click.Handler = string.Format("window.location = '" + urlout + "'");

        logout.Attributes.Add("onclick", string.Format("window.location = '" + urlout + "'"));
        btnLogout.Hidden = isframe;
        //if (StatusBar1.Hidden)
        //{
        //  logout.Visible = !(StatusBar1.Hidden);
        //}

        string change_passwd = ConstantDictExt.Translate(GlobalExt.BTN_CHANGE_PASSWORD);
        Ext.Net.Button btnPassword = new Ext.Net.Button
        {
            ToolTip = change_passwd,
            Icon = Icon.UserKey
        };
        urlout = "System/SetPassword.aspx?local=" + Request["local"] + "&app=" + GlobalAsp.GetSessionApp()
          + "&sub=" + GlobalAsp.GetRequestSub();
        btnPassword.Listeners.Click.Handler = string.Format("e.stopEvent(); loadPage(#{0}, '{1}','{1}','{2}');", "{Pages}", change_passwd, urlout);
        btnPassword.Hidden = isframe;

        string user_profile = ConstantDictExt.Translate(GlobalExt.BTN_PROFILE1);
        Ext.Net.Button btnProfile = new Ext.Net.Button
        {
            ToolTip = user_profile,
            Icon = Icon.UserSuitBlack
        };
        btnProfile.Listeners.Click.Handler = "var el = document.getElementById('status'); el.style.display = (el.style.display == 'block') ? 'none' : 'block'";
        btnProfile.Hidden = isframe;

        //Ext.Net.Button btnForum = new Ext.Net.Button();//Sdh ada di aspx
        string idapppname = textFilterIdapp.DataIndex;
        string url = UtilityExt.ValidateURL(null, string.Format("Page/Comment.aspx?local=" + HttpContext.Current.Request["local"]
        + "&app=" + GlobalAsp.GetRequestApp()
        + "&id=" + GlobalAsp.GetRequestId()
        + "&pk=" + idapppname
        + "&i=1"
        + "&type=" + NotesControl.FORUM
        + "&parentsave=0"
        ));
        btnForum.Hidden = StatusBar1.Hidden;
        btnForum.ToolTip = ConstantDictExt.Translate(GlobalExt.BTN_FORUM1);
        btnForum.Icon = Icon.EmailStar;
        btnForum.Listeners.Click.Handler = string.Format("e.stopEvent(); loadPage(#{0}, '{1}','{1}','{2}');", "{Pages}",
          btnForum.ToolTip, url);
        #endregion
        #region toolBar.Items.Add
        //toolBar.Items.Insert(toolBar.Items.Count - 3, btnRefresh);
        toolBar.Items.Insert(toolBar.Items.Count - 3, new ToolbarSeparator());

        toolBar.Items.Insert(toolBar.Items.Count - 3, btnExpand);
        toolBar.Items.Insert(toolBar.Items.Count - 3, btnCollapse);
        toolBar.Items.Insert(toolBar.Items.Count - 3, btnShowMenu);
        toolBar.Items.Insert(toolBar.Items.Count - 3, btnHideMenu);
        toolBar.Items.Insert(toolBar.Items.Count - 3, new ToolbarSeparator());
        toolBar.Items.Insert(toolBar.Items.Count - 3, btnFile);
        toolBar.Items.Insert(toolBar.Items.Count - 3, new ToolbarSeparator());
        toolBar.Items.Insert(toolBar.Items.Count - 3, lblWait);
        toolBar.Items.Insert(toolBar.Items.Count - 3, new ToolbarFill());
        #endregion
        #region Notifikasi Mode dan Berdasarkan App
        if (!isframe)
        {
            toolBar.Items.Insert(toolBar.Items.Count - 3, new ToolbarSeparator());
            //toolBar.Items.Add(new ToolbarSeparator());
            if (MasterAppConstants.Instance.StatusTesting)
            {
                Ext.Net.Button btnMenu = new Ext.Net.Button() { ID = GlobalAsp.BTN_MENU2, Icon = Icon.Wrench };
                btnMenu.ToolTip = ConstantDictExt.TranslateTabTip(btnMenu);

                Ext.Net.Menu menuDebug = new Ext.Net.Menu() { };
                btnMenu.Menu.Add(menuDebug);

                Ext.Net.MenuItem mnInfo = new Ext.Net.MenuItem() { ID = GlobalAsp.MENU_INFO1, Icon = Icon.PageLightning };
                mnInfo.Text = ConstantDictExt.Translate(mnInfo.ID);
                mnInfo.ToolTip = ConstantDictExt.TranslateTabTip(mnInfo);
                mnInfo.Listeners.Click.Handler = "CoreNET.btnDebugClick()";
                menuDebug.Items.Add(mnInfo);
                menuDebug.Items.Add(new Ext.Net.MenuSeparator());

                Ext.Net.MenuItem mnconfig = new Ext.Net.MenuItem() { ID = GlobalAsp.MENU_CONFIG1, Icon = Icon.ApplicationForm };
                mnconfig.Text = ConstantDictExt.Translate(mnconfig.ID);
                mnconfig.ToolTip = ConstantDictExt.TranslateTabTip(mnconfig);
                mnconfig.Listeners.Click.Handler = "CoreNET.btnAdminClick(0)";
                menuDebug.Items.Add(mnconfig);

                Ext.Net.MenuItem mnlist = new Ext.Net.MenuItem() { ID = GlobalAsp.MENU_LISTMENU1, Icon = Icon.ApplicationSideTree };
                mnlist.Text = ConstantDictExt.Translate(mnlist.ID);
                mnlist.ToolTip = ConstantDictExt.TranslateTabTip(mnlist);
                url = UtilityExt.ValidateURL(null, string.Format("Page/PageTreeGrid.aspx?id=" + BOSysUtils.ID_LIST_MENU + "&app=" + GlobalAsp.GetRequestApp()));
                mnlist.Listeners.Click.Handler = string.Format("e.stopEvent(); CoreNET.SaveSessionPageURL('{4}');loadPage5(#{0}, '{1}','{2}','{3}','{4}');", "{Pages}", mnlist.Text, mnlist.Text, mnlist.ToolTip, url);
                menuDebug.Items.Add(mnlist);

                Ext.Net.MenuItem mnedit = new Ext.Net.MenuItem() { ID = GlobalAsp.MENU_EDITMENU1, Icon = Icon.ApplicationSideTree };
                mnedit.Text = ConstantDictExt.Translate(mnedit.ID);
                mnedit.ToolTip = ConstantDictExt.TranslateTabTip(mnedit);
                url = UtilityExt.ValidateURL(null, string.Format("Page/PageTreeGridDetil.aspx?id=" + BOSysUtils.ID_LIST_MENU + "&app=" + GlobalAsp.GetRequestApp()));
                mnedit.Listeners.Click.Handler = string.Format("e.stopEvent(); CoreNET.SaveSessionPageURL('{4}');loadPage5(#{0}, '{1}','{2}','{3}','{4}');", "{Pages}", mnedit.Text, mnedit.Text, mnedit.ToolTip, url);
                menuDebug.Items.Add(mnedit);

                Ext.Net.MenuItem mneditc = new Ext.Net.MenuItem() { ID = GlobalAsp.MENU_EDITCONFIG1, Icon = Icon.ApplicationSideTree };
                mneditc.Text = ConstantDictExt.Translate(mneditc.ID);
                mneditc.ToolTip = ConstantDictExt.TranslateTabTip(mneditc);
                url = UtilityExt.ValidateURL(null, string.Format("Page/PageMenu.aspx?id=" + BOSysUtils.ID_EDITCONFIG + "&app=" + GlobalAsp.GetRequestApp()));
                mneditc.Listeners.Click.Handler = string.Format("e.stopEvent(); CoreNET.SaveSessionPageURL('{4}');loadPage5(#{0}, '{1}','{2}','{3}','{4}');", "{Pages}", mneditc.Text, mneditc.Text, mneditc.ToolTip, url);
                menuDebug.Items.Add(mneditc);

                Ext.Net.MenuItem mnmenukeys = new Ext.Net.MenuItem() { ID = GlobalAsp.MENU_EDITKEYS1, Icon = Icon.ApplicationSideTree };
                mnmenukeys.Text = ConstantDictExt.Translate(mnmenukeys.ID);
                mnmenukeys.ToolTip = ConstantDictExt.TranslateTabTip(mnmenukeys);
                url = UtilityExt.ValidateURL(null, string.Format("Page/PageMenu.aspx?id=" + BOSysUtils.ID_KEYS + "&app=" + GlobalAsp.GetRequestApp()));
                mnmenukeys.Listeners.Click.Handler = string.Format("e.stopEvent(); CoreNET.SaveSessionPageURL('{4}');loadPage5(#{0}, '{1}','{2}','{3}','{4}');", "{Pages}", mnmenukeys.Text, mnmenukeys.Text, mnmenukeys.ToolTip, url);
                menuDebug.Items.Add(mnmenukeys);

                Ext.Net.MenuItem mnmenurows = new Ext.Net.MenuItem() { ID = GlobalAsp.MENU_EDITROWS1, Icon = Icon.ApplicationSideTree };
                mnmenurows.Text = ConstantDictExt.Translate(mnmenurows.ID);
                mnmenurows.ToolTip = ConstantDictExt.TranslateTabTip(mnmenurows);
                url = UtilityExt.ValidateURL(null, string.Format("Page/PageMenu.aspx?id=" + BOSysUtils.ID_FORM_ENTRY + "&app=" + GlobalAsp.GetRequestApp()));
                mnmenurows.Listeners.Click.Handler = string.Format("e.stopEvent(); CoreNET.SaveSessionPageURL('{4}');loadPage5(#{0}, '{1}','{2}','{3}','{4}');", "{Pages}", mnmenurows.Text, mnmenurows.Text, mnmenurows.ToolTip, url);
                menuDebug.Items.Add(mnmenurows);

                Ext.Net.MenuItem mnmenucols = new Ext.Net.MenuItem() { ID = GlobalAsp.MENU_EDITCOLS1, Icon = Icon.ApplicationSideTree };
                mnmenucols.Text = ConstantDictExt.Translate(mnmenucols.ID);
                mnmenucols.ToolTip = ConstantDictExt.TranslateTabTip(mnmenucols);
                url = UtilityExt.ValidateURL(null, string.Format("Page/PageMenu.aspx?id=" + BOSysUtils.ID_COL_GRID + "&app=" + GlobalAsp.GetRequestApp()));
                mnmenucols.Listeners.Click.Handler = string.Format("e.stopEvent(); CoreNET.SaveSessionPageURL('{4}');loadPage5(#{0}, '{1}','{2}','{3}','{4}');", "{Pages}", mnmenucols.Text, mnmenucols.Text, mnmenucols.ToolTip, url);
                menuDebug.Items.Add(mnmenucols);

                Ext.Net.MenuItem mnmenutreecols = new Ext.Net.MenuItem() { ID = GlobalAsp.MENU_EDITTREECOLS1, Icon = Icon.ApplicationSideTree };
                mnmenutreecols.Text = ConstantDictExt.Translate(mnmenutreecols.ID);
                mnmenutreecols.ToolTip = ConstantDictExt.TranslateTabTip(mnmenutreecols);
                url = UtilityExt.ValidateURL(null, string.Format("Page/PageMenu.aspx?id=" + BOSysUtils.ID_COL_TREE + "&app=" + GlobalAsp.GetRequestApp()));
                mnmenutreecols.Listeners.Click.Handler = string.Format("e.stopEvent(); CoreNET.SaveSessionPageURL('{4}');loadPage5(#{0}, '{1}','{2}','{3}','{4}');", "{Pages}", mnmenutreecols.Text, mnmenutreecols.Text, mnmenutreecols.ToolTip, url);
                menuDebug.Items.Add(mnmenutreecols);

                Ext.Net.MenuItem mnmenufilters = new Ext.Net.MenuItem() { ID = GlobalAsp.MENU_EDITFILTERS1, Icon = Icon.ApplicationSideTree };
                mnmenufilters.Text = ConstantDictExt.Translate(mnmenufilters.ID);
                mnmenufilters.ToolTip = ConstantDictExt.TranslateTabTip(mnmenufilters);
                url = UtilityExt.ValidateURL(null, string.Format("Page/PageMenu.aspx?id=" + BOSysUtils.ID_FILTERS + "&app=" + GlobalAsp.GetRequestApp()));
                mnmenufilters.Listeners.Click.Handler = string.Format("e.stopEvent(); CoreNET.SaveSessionPageURL('{4}');loadPage5(#{0}, '{1}','{2}','{3}','{4}');", "{Pages}", mnmenufilters.Text, mnmenufilters.Text, mnmenufilters.ToolTip, url);
                menuDebug.Items.Add(mnmenufilters);

                Ext.Net.MenuItem mnmenumanual = new Ext.Net.MenuItem() { ID = GlobalAsp.MENU_EDITMANUAL1, Icon = Icon.ApplicationSideTree };
                mnmenumanual.Text = ConstantDictExt.Translate(mnmenumanual.ID);
                mnmenumanual.ToolTip = ConstantDictExt.TranslateTabTip(mnmenumanual);
                url = UtilityExt.ValidateURL(null, string.Format("Page/PageMenu.aspx?id=" + BOSysUtils.ID_MANUAL + "&app=" + GlobalAsp.GetRequestApp()));
                mnmenumanual.Listeners.Click.Handler = string.Format("e.stopEvent(); CoreNET.SaveSessionPageURL('{4}');loadPage5(#{0}, '{1}','{2}','{3}','{4}');", "{Pages}", mnmenumanual.Text, mnmenumanual.Text, mnmenumanual.ToolTip, url);
                menuDebug.Items.Add(mnmenumanual);

                Ext.Net.MenuItem mnmenuref = new Ext.Net.MenuItem() { ID = GlobalAsp.MENU_EDITREF1, Icon = Icon.ApplicationSideTree };
                mnmenuref.Text = ConstantDictExt.Translate(mnmenuref.ID);
                mnmenuref.ToolTip = ConstantDictExt.TranslateTabTip(mnmenuref);
                url = UtilityExt.ValidateURL(null, string.Format("Page/PageMenu.aspx?id=" + BOSysUtils.ID_REFERENSI + "&app=" + GlobalAsp.GetRequestApp()));
                mnmenuref.Listeners.Click.Handler = string.Format("e.stopEvent(); CoreNET.SaveSessionPageURL('{4}');loadPage5(#{0}, '{1}','{2}','{3}','{4}');", "{Pages}", mnmenuref.Text, mnmenuref.Text, mnmenuref.ToolTip, url);
                menuDebug.Items.Add(mnmenuref);

                Ext.Net.MenuItem mnmenuforum = new Ext.Net.MenuItem() { ID = GlobalAsp.MENU_EDITFORUM1, Icon = Icon.ApplicationSideTree };
                mnmenuforum.Text = ConstantDictExt.Translate(mnmenuforum.ID);
                mnmenuforum.ToolTip = ConstantDictExt.TranslateTabTip(mnmenuforum);
                url = UtilityExt.ValidateURL(null, string.Format("Page/PageTreePanelDetil.aspx?id=" + BOSysUtils.ID_FORUM + "&app=" + GlobalAsp.GetRequestApp()));
                mnmenuforum.Listeners.Click.Handler = string.Format("e.stopEvent(); CoreNET.SaveSessionPageURL('{4}');loadPage5(#{0}, '{1}','{2}','{3}','{4}');", "{Pages}", mnmenuforum.Text, mnmenuforum.Text, mnmenuforum.ToolTip, url);
                menuDebug.Items.Add(mnmenuforum);

                Ext.Net.MenuItem mnmenuversi = new Ext.Net.MenuItem() { ID = GlobalAsp.MENU_EDITVERSI1, Icon = Icon.ApplicationSideTree };
                mnmenuversi.Text = ConstantDictExt.Translate(mnmenuversi.ID);
                mnmenuversi.ToolTip = ConstantDictExt.TranslateTabTip(mnmenuversi);
                url = UtilityExt.ValidateURL(null, string.Format("Page/PageMenu.aspx?id=" + BOSysUtils.ID_VERSI + "&app=" + GlobalAsp.GetRequestApp()));
                mnmenuversi.Listeners.Click.Handler = string.Format("e.stopEvent(); CoreNET.SaveSessionPageURL('{4}');loadPage5(#{0}, '{1}','{2}','{3}','{4}');", "{Pages}", mnmenuversi.Text, mnmenuversi.Text, mnmenuversi.ToolTip, url);
                menuDebug.Items.Add(mnmenuversi);


                Ext.Net.MenuItem mndict = new Ext.Net.MenuItem() { ID = GlobalAsp.MENU_DICT1, Icon = Icon.World };
                mndict.Text = ConstantDictExt.Translate(mndict.ID);
                mndict.Listeners.Click.Handler = "CoreNET.btnAdminClick(1)";
                menuDebug.Items.Add(mndict);

                toolBar.Items.Insert(toolBar.Items.Count - 3, btnMenu);
                toolBar.Items.Insert(toolBar.Items.Count - 3, new ToolbarSeparator());

                if (!StatusBar1.Hidden)
                {
                    toolBar.Items.Insert(toolBar.Items.Count - 3, new ToolbarTextItem() { Text = "Mode Testing" });
                    toolBar.Items.Insert(toolBar.Items.Count - 3, new ToolbarSeparator());
                }
            }
            if (!StatusBar1.Hidden)
            {
                toolBar.Items.Insert(toolBar.Items.Count - 3, new ToolbarTextItem() { Text = "Mode Online" });
                toolBar.Items.Insert(toolBar.Items.Count - 3, new ToolbarSeparator());
            }

            toolBar.Items.Add(btnPassword);
            toolBar.Items.Add(btnProfile);
        }
        #endregion
        IDataControlMenu cMenu = GlobalExt.GetSessionMenu();
        IList list = (IList)GlobalExt.GetSessionListMenuRef();
        if (list == null)
        {
            list = cMenu.GetListRef();
        }

        if (list != null && list.Count > 0)
        {
            GlobalExt.SetSessionListMenuRef(list);
            btnMenu.Menu.Add(cMenu.GetMenu(list, 2));
        }
        #endregion Toolbar
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void btnDebugClick()
    {
        WindowDebug1.Render(Page.Form);
        WindowDebug1.LoadContent();
        WindowDebug1.Show();
    }
    private void CreateNode(TreePanel tree)
    {
        //tree.Icon = Icon.BookOpen;
        //tree.Title = ConstantDictExt.Translate("LIST_MENU");
        tree.AutoScroll = true;

        StatusBar statusBar = new StatusBar
        {
            AutoClear = 1000
        };
        tree.BottomBar.Add(statusBar);

        tree.Listeners.Click.Handler = statusBar.ClientID + ".setStatus({text: 'Node Selected: <b>' + node.text + '</b>', clear: true});";
        tree.Listeners.ExpandNode.Handler = statusBar.ClientID + ".setStatus({text: 'Node Expanded: <b>' + node.text + '</b>', clear: true});";
        tree.Listeners.ExpandNode.Delay = 30;
        tree.Listeners.CollapseNode.Handler = statusBar.ClientID + ".setStatus({text: 'Node Collapsed: <b>' + node.text + '</b>', clear: true});";

        IList list = null;
        Ext.Net.TreeNode root = null;
        IDataControlAppuserUI cUserdb = GlobalExt.GetSessionUser();
        IDataControlMenu cMenu = (IDataControlMenu)GlobalExt.GetSessionMenu();
        try
        {
            if (!string.IsNullOrEmpty(textFilterIdapp.Text))
            {
                list = (IList)GlobalExt.GetSessionListMenu();
                if ((list == null) || (MasterAppConstants.Instance.StatusTesting))
                {
                    cMenu.SetPageKey();
                    list = (IList)cMenu.View();
                }
            }
            else
            {
                list = null;
            }
            UtilityUI.SetDataControl(1, cMenu);
            GlobalExt.SetSessionListMenu(list);

            if (list != null && list.Count > 0)
            {
                root = ((IDataControlMenu)cMenu).CreateMenu(list, ExtTreePanelUtil.TYPE_TREEMENU, ((ViewListProperties)cMenu.GetProperties()).HasTreeRoot);
            }
            else
            {
                root = new Ext.Net.TreeNode();
            }
            tree.Root.Add(root);
            tree.Listeners.Click.Handler = statusBar.ClientID + ".setStatus({text: 'Node Selected: <b>' + node.text + '</b>', clear: true});";
            tree.Listeners.Click.Handler += "if (node.attributes.href) { CoreNET.SaveSessionPage(node.attributes.href,node.attributes.id,node.getPath('id','/'));e.stopEvent(); loadNode(#{Pages}, node,node.getPath('id','/')); }";
            tree.Title = root.Text;
            tree.RootVisible = false;
        }
        catch (Exception ex)
        {
            UtilityBO.Log(ex);
            if (MasterAppConstants.Instance.StatusTesting)
            {
                throw new Exception(ConstantDict.Translate("LBL_ERROR_CREATE_MENU"), ex);
            }
            else
            {
                WindowDebug.ShowMessage(Page, ConstantDictExt.TranslateException(ex, MethodBase.GetCurrentMethod().Name));
                //string urlout = GlobalAsp.GetLogoutURL() + "?local=" + Request["local"] + "&kdapp=" + GlobalAsp.GetRequestKdapp()
                //  + "&sub=" + GlobalAsp.GetRequestSub();
                //Response.Redirect(urlout);
            }
        }

    }

    #endregion

    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void ExpandTreePanel(string path)
    {
        TreePanel1.SelectPath(path);
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void ReloadPage()
    {
        tabHome.LoadContent();
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void btnAdminClick(int idx)
    {
        WindowAdmin1[idx].Render(Page.Form);
        WindowAdmin1[idx].LoadContent();
        WindowAdmin1[idx].Show();
    }

    public void Empty(DirectoryInfo directory)
    {
        foreach (FileInfo file in directory.GetFiles())
        {
            file.Delete();
        }

        foreach (DirectoryInfo subDirectory in directory.GetDirectories())
        {
            subDirectory.Delete(true);
        }
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void Home()
    {
        if (MasterAppConstants.Instance.StatusTesting)
        {
            string msg = string.Format("Error in class {0}, method {1}", GetType().Name,
              MethodBase.GetCurrentMethod().Name);
            X.Msg.Alert(ConstantDictExt.Translate(GlobalExt.LBL_INFO), msg).Show();

        }
        else
        {
            string urlout = GlobalAsp.GetLogoutURL() + "?local=" + Request["local"] + "&kdapp=" + GlobalAsp.GetRequestKdapp()
              + "&sub=" + GlobalAsp.GetRequestSub();
            Response.Redirect(urlout);
        }
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void SaveSessionPage(string url, string roleid, string path)
    {
        UtilityUI.SetSessionMenuPage(url, roleid, true);
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void SaveSessionPageURL(string url)
    {
        UtilityUI.SetSessionMenuPage(url, string.Empty, true);
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void ExpandRightFrame()
    {
        TreePanel1.Collapse();
    }

    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void WindowShow()
    {
        PanelLink.AutoLoad.Url = UtilityExt.ValidateURL(this, string.Format("~/Prototype/Manifest.aspx?app=" + GlobalAsp.GetRequestApp() + "&id=" + GlobalExt.GetRequestId()));
        UtilityExt.SetIFrameAutoLoad(PanelLink.AutoLoad);
        PanelLink.LoadContent();
        WindowLink.Title = "Garuda";
        WindowLink.Icon = Icon.CarRed;
        WindowLink.Show();
    }
    [DirectMethod(Timeout = 3600000, Namespace = "CoreNET")]
    public void btnCloseDialogClick()
    {
        //X.Js.Call();
    }

}
