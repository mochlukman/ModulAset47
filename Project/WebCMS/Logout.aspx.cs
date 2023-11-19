using CoreNET.Common.Base;
using CoreNET.Common.BO;
using System;
using System.Collections;
using System.Configuration;
using System.Web;

public partial class Logout : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    string kdapp = GlobalAsp.GetRequestKdapp();
    string basehomeURL = GlobalAsp.GetHomePortalURL();
    if (string.IsNullOrEmpty(basehomeURL))
    {
      basehomeURL = GlobalAsp.GetHomeURL() + "?sub=" + GlobalAsp.GetRequestSub();
    }
    string homeURL = string.Empty;
    homeURL = basehomeURL;
    if (string.IsNullOrEmpty(kdapp))//(homeURL.Contains("PageMenu.aspx"))
    {
      kdapp = ConfigurationManager.AppSettings["PrefixPortal"];
      if (!basehomeURL.Contains("kdapp="))
      {
        homeURL = basehomeURL + "&kdapp=" + kdapp;
      }
    }

    string local = (Request.IsLocal && (Request["local"] == "1")) ? "1" : "0";// (string)Session[ConstantApp.SESSION_LOCAL];
    try
    {
      if (!string.IsNullOrEmpty(Request["prev"]))
      {
        try
        {
          homeURL = basehomeURL + "&kdapp=" + GlobalAsp.GetRequestKdapp();
        }
        catch (Exception ex)
        {
          UtilityBO.Log(ex);
        }
      }

      if (GlobalExt.GetSessionUser() == null)
      {
        //if (homeURL.ToLower().Contains("portal"))
        //{
        //  homeURL = GlobalAsp.GetHomeURL();
        //}
        Response.Redirect(homeURL);
        return;
      }
      IDataControlAppuserUI cWebuser = GlobalExt.GetSessionUser();
      ArrayList cUsers = GlobalExt.GetSessionUsers();
      if (cWebuser != null)
      {
        while (cUsers.Contains(cWebuser.GetUserID().Trim()))
        {
          cUsers.Remove(cWebuser.GetUserID().Trim());
        }
      }
      GlobalExt.SetSessionUsers(cUsers);
      Session.Abandon();
      Session.RemoveAll();
      HttpContext.Current.Application[GlobalAsp.DAO] = null;
    }
    catch (Exception ex)
    {
      UtilityBO.Log(ex);
    }
    //if (homeURL.ToLower().Contains("portal"))
    //{
    //  homeURL = GlobalAsp.GetHomeURL();
    //}
    Response.Redirect(homeURL);
  }
}
