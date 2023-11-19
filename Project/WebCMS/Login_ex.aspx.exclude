<%@ Page Language="C#" AutoEventWireup="True" ValidateRequest="false" CodeFile="Login.aspx.cs"
  Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title></title>
  <link href="res/css/imageslide.css" rel="stylesheet" type="text/css" media="all" />
  <link href="Res/img/icon.ico" rel="shortcut icon" />
  <style type="text/css">
    #Form1 {
      height: 254px;
    }
  </style>
</head>
<body style="background-color: antiquewhite">
  <ul class="cb-slideshow">
    <li style="list-style: none;"><span>Image 01</span></li>
    <li style="list-style: none;"><span>Image 02</span></li>
    <li style="list-style: none;"><span>Image 03</span></li>
  </ul>
  <div id="mainDiv" runat="server">
    <form id="Form1" runat="server">
      <ext:ResourceManager ID="ResourceManager1" runat="server" ShowWarningOnAjaxFailure="false" />
      <input name="utxt_Code" runat="server" type="hidden" id="utxt_Code" />
      <ext:Window
        ID="Window1"
        runat="server"
        Closable="false"
        Resizable="false"
        Height="150"
        Icon="Lock"
        Title="Login"
        Draggable="false"
        Width="350"
        Modal="true"
        Padding="5"
        Layout="Form">
        <Items>
          <ext:TextField
            ID="txtUser"
            runat="server"
            FieldLabel="Username"
            AllowBlank="false"
            BlankText="Your username is required."
            Text=""
            AutoScroll="false"
            AnchorHorizontal="100%" />
          <ext:TextField
            ID="txtPwd"
            runat="server"
            InputType="Password"
            FieldLabel="Password"
            AllowBlank="false"
            BlankText="Your password is required."
            Text=""
            AnchorHorizontal="100%" />
        </Items>
        <Buttons>
          <ext:Button ID="btnLogin" runat="server" Text="Login" Icon="Accept">
            <DirectEvents>
              <Click OnEvent="btnLogin_Click" Before="
                            if (!#{txtUser}.validate() || !#{txtPwd}.validate()) {
                                Ext.Msg.alert('Error','The Username and Password fields are both required');
                                // return false to prevent the btnLogin_Click Ajax Click event from firing.
                                return false; 
                            };return validateLogin();" After="Window1.hide()">
                <EventMask ShowMask="true" Msg="Tunggu halaman menu..." MinDelay="500" />
              </Click>
            </DirectEvents>
          </ext:Button>
          <ext:Button ID="btnCancel" runat="server" Text="Cancel" Icon="Decline">
            <DirectEvents>
              <Click OnEvent="btnCancel_Click">
                <EventMask ShowMask="true" Msg="Verifying..." MinDelay="500" />
              </Click>
            </DirectEvents>
          </ext:Button>
        </Buttons>
      </ext:Window>
    </form>
  </div>
</body>
</html>
