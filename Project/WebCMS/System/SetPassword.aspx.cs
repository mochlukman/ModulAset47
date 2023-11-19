using CoreNET.Common.Base;
using Ext.Net;
using System;
using System.Reflection;
using CoreNET.Common.BO;

public partial class pSistem_SetPassword : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (GlobalAsp.CekSessionPage(Page))
    {
      corenet_UserID.Value = GlobalExt.GetSessionUser().GetUserID();
      corenet_UserID.Disabled = true;
    }
    else
    {
      X.Js.Call("home");
    }
  }
  protected void Button1_Click(object sender, EventArgs e)
  {
    bool error = true;
    if (newPassword.Value.Trim().Equals(confirmPassword.Value.Trim())
      && !string.IsNullOrEmpty(newPassword.Value.Trim())
      && !string.IsNullOrEmpty(currentPassword.Value.Trim()))
    {
      error = false;
    }

    if (error)
    {
      string msg = "Cek kembali data yang anda isikan";
      X.Msg.Alert(GlobalAsp.GetConfigLabelInfo(), msg).Show();
      return;
    }
    try
    {
      if (!string.IsNullOrEmpty(utxt_Code.Value))
      {
        GlobalExt.UpdatePassword(utxt_Code.Value);
        string msg = "Ubah password berhasil";
        X.Msg.Alert(GlobalAsp.GetConfigLabelInfo(), msg).Show();
      }
    }
    catch (Exception ex)
    {
      UtilityBO.Log(ex);
      if (ex.Message.Contains("Unable to open connection"))
      {
        Response.Redirect(GlobalExt.GetHomeURL());
      }
      else
      {
        WindowDebug.ShowMessage(Page, ConstantDictExt.TranslateException(ex, MethodBase.GetCurrentMethod().Name));
      }
    }
  }
  protected void CheckField(object sender, RemoteValidationEventArgs e)
  {
    TextField field = (TextField)sender;
    IDataControlAppuserUI cUserdb = GlobalExt.GetSessionUser();
    string pwd = UtilityLib.Encode(field.Text);
    if (newPassword.Value.Trim().Equals(confirmPassword.Value.Trim())
      && !string.IsNullOrEmpty(newPassword.Value.Trim())
      && !string.IsNullOrEmpty(currentPassword.Value.Trim()))
    {
      e.Success = true;
    }
    else
    {
      e.Success = false;
      e.ErrorMessage = "Konfirmasi password berbeda dengan password baru";
    }
  }
}
