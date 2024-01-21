<%@ Page Language="C#" AutoEventWireup="True" ValidateRequest="false" CodeFile="Login.aspx.cs"
  Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title></title>
  <link href="res/css/imageslide.css" rel="stylesheet" type="text/css" media="all" />
  <link href="res/css/login.css" rel="stylesheet" type="text/css" media="all" />
  <link href="Res/img/icon.ico" rel="shortcut icon" />
  <link href='https://fonts.googleapis.com/css?family=Source Sans Pro' rel='stylesheet'/>
  <link href='https://fonts.googleapis.com/css?family=Inter' rel='stylesheet'/>
  <link href='https://fonts.googleapis.com/css?family=Poppins' rel='stylesheet'/>

</head>
<body>
<%--  <ul class="cb-slideshow">
    <li style="list-style: none;"><span>Image 01</span></li>
    <li style="list-style: none;"><span>Image 02</span></li>
    <li style="list-style: none;"><span>Image 03</span></li>
  </ul>--%>

    <nav class="navbar">
      <ul>
         <li class="big"><a href="https://quantum.intermatika.id/"><img class="medium" src="Res/img/Quantum.png" alt="Quantum"/> </a>Selamat Datang !</li>
      </ul>
      <ul>
        <li >
            <a href="https://api.whatsapp.com/send?phone=6285175366682&text=Hallo%2C%20Boleh%20Saya%20Minta%20Informasi%20Detil%20Tentang%20SIPKD%20BMD%3F" target="_blank"  class="image-link">
            <img class="medium" src="Res/img/whatsapp.png" alt="phone_icon"/> 
            <img class="small" src="Res/img/butuhbantuan.png" alt="butuh bantuan"/>
            </a>
        </li>
      </ul>
    </nav>

    <div class="row">
      <div id="infoDiv" class="column">
             <div class="caption">
              <img id="rafiki" src="Res/img/bg_sipkdbmd.png"" alt="SiGawai"  />
                 <br />
              <img id="sigawai" src="Res/img/sipkd_bmd.png" alt="SiGawai"/>
              <h2>Sistem Informasi Pengelolaan Barang Milik Daerah</h2>
              <p>Version 1.0523</p>
            </div>
        </div>

      <div id="container" class="column">
        <div id="mainDiv"  runat="server">

         <div class="title-container">
          <div class="title">Log in</div>
          <div class="title-image-container">
            <img src="Res/img/logopemda.png" alt="quantumlogo"/>
          </div>
        </div>


        <form id="Form1" runat="server">
          <ext:ResourceManager ID="ResourceManager1" runat="server" ShowWarningOnAjaxFailure="false" />
          <input name="utxt_Code" runat="server" type="hidden" id="utxt_Code" />
            <Items Cls ="container">
                <label class ="lblUser"> User name </label>
              <ext:TextField
                ID="txtUser"
                runat="server"
                AllowBlank="false"
                BlankText="Your user name is required."
                EmptyText="User name"
                AutoScroll="false"
                AnchorHorizontal="100%" 
                Cls="ExtTextField" />

                <label class ="lblPwd"> Password </label>
              <ext:TextField
                ID="txtPwd"
                runat="server"
                InputType="Password"
                AllowBlank="false"
                BlankText="Your password is required."
                EmptyText =""
                AnchorHorizontal="100%" 
                Cls="ExtTextField" />
            </Items>
            <div class="extranous">
                 <a href="#">Lupa Password</a>
                 <a href="#">Hubungi Admin</a>
            </div>


              <ext:Button ID="btnLogin" runat="server" Text="Log in" Cls="ExtButton" >
                <DirectEvents>
                  <Click OnEvent="btnLogin_Click" Before="
                                if (!#{txtUser}.validate() || !#{txtPwd}.validate()) {
                                    Ext.Msg.alert('Error','The User name and Password fields are both required');
                                    // return false to prevent the btnLogin_Click Ajax Click event from firing.
                                    return false; 
                                };return validateLogin();" >
                    <EventMask ShowMask="true" Msg="Tunggu halaman menu..." MinDelay="500" />
                  </Click>
                </DirectEvents>
              </ext:Button>
    
        </form>

            <div class="info">
                <p>Untuk permintaan User name dan Password </p>
                <p>Hubungi Admin SIPKD BMD </p>
            </div>
            <!--<h5>Ikuti Kami </h5>
            <div class="image-container">
              <a href="https://www.facebook.com" target="_blank"><img src="Res/img/facebook_logo.png" alt="facebooklogo"/></a>
              <a href="https://www.instagram.com" target="_blank"><img src="Res/img/instagram_logo.png" alt="instagramlogo"/></a>
              <a href="https://www.youtube.com" target="_blank"><img src="Res/img/youtube_logo.png" alt="youtubelogo"/></a>
            </div>-->
        </div>
      </div>
    </div>

   <div class="footer">
    <p> Quantum Sistem Intermatika - 2023</p>
  </div>

</body>
</html>
