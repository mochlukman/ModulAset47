<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetPassword.aspx.cs" Inherits="pSistem_SetPassword"
  Title="Set Password" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Ubah Sandi</title>
  <link href="../res/css/bootstrap.css" rel="stylesheet" />
  <script type="text/javascript">
    var encode = function () {
      var val1 = document.getElementById("corenet_UserID").value;
      var val2 = document.getElementById("currentPassword").value;
      var val3 = document.getElementById("newPassword").value;
      document.getElementById("utxt_Code").value = b64EncodeUnicode(val1 + '|' + val2 + '|' + val3);
    };
    function b64EncodeUnicode(str) {
      // first we use encodeURIComponent to get percent-encoded UTF-8,
      // then we convert the percent encodings into raw bytes which
      // can be fed into btoa.
      return btoa(encodeURIComponent(str).replace(/%([0-9A-F]{2})/g,
        function toSolidBytes(match, p1) {
          return String.fromCharCode('0x' + p1);
        }));
    }
    function b64DecodeUnicode(str) {
      // Going backwards: from bytestream, to percent-encoding, to original string.
      return decodeURIComponent(atob(str).split('').map(function (c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
      }).join(''));
    }

    function validatePassword() {
      var currentPassword, newPassword, confirmPassword, output = true;

      currentPassword = document.frmChange.currentPassword;
      newPassword = document.frmChange.newPassword;
      confirmPassword = document.frmChange.confirmPassword;

      if (!currentPassword.value) {
        currentPassword.focus();
        document.getElementById("currentPassword").innerHTML = "required";
        output = false;
      }
      else if (!newPassword.value) {
        newPassword.focus();
        document.getElementById("newPassword").innerHTML = "required";
        output = false;
      }
      else if (!confirmPassword.value) {
        confirmPassword.focus();
        document.getElementById("confirmPassword").innerHTML = "required";
        output = false;
      }
      if (newPassword.value != confirmPassword.value) {
        newPassword.value = "";
        confirmPassword.value = "";
        newPassword.focus();
        document.getElementById("confirmPassword").innerHTML = "not same";
        output = false;
      }
      return output;
    }
  </script>
</head>
<body>
  <form id="Form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" />
<%--    <div class="container">
      <div class="row">
        <div class="col-md-12 order-md-1">
          <div class="mb-3">
            <label for="username">Username</label>
            <div class="input-group">
              <input type="text" id="corenet_UserID" name="corenet_UserID" runat="server" class="form-control"
               placeholder="Username" onchange="encode();" required />
            </div>
          </div>
        </div>
      </div>
    </div>--%>

    <div style="width: 500px;padding:50px 50px 50px 50px">
      <div class="message">
        Masukkan password lama dan password baru
      </div>
      <table border="0" cellpadding="10" cellspacing="0" width="500" align="center" class="tblSaveForm">
        <tr>
          <td width="40%">
            <label>
              User ID</label>
          </td>
          <td width="60%">
            <input type="text" id="corenet_UserID" name="corenet_UserID" runat="server" class="txtField"
              onchange="encode();" /><span id="Span1" class="required"></span>
          </td>
        </tr>
        <tr>
          <td width="40%">
            <label>
              Current Password</label>
          </td>
          <td width="60%">
            <input type="password" id="currentPassword" name="currentPassword" runat="server"
              class="txtField" onchange="encode();" /><span id="currentPassword" class="required"></span>
          </td>
        </tr>
        <tr>
          <td>
            <label>
              New Password</label>
          </td>
          <td>
            <input type="password" id="newPassword" name="newPassword" runat="server" class="txtField"
              onchange="encode();" /><span id="newPassword" class="required"></span>
          </td>
        </tr>
        <tr>
          <td>
            <label>
              Confirm Password</label>
          </td>
          <td>
            <input type="password" id="confirmPassword" name="confirmPassword" runat="server" class="txtField" /><span id="confirmPassword"
              class="required"></span>
          </td>
        </tr>
        <tr>
          <td colspan="2">
            <asp:Button ID="Button1" runat="server" Text="Simpan"
              Style="border-width: 0px; vertical-align: middle;" 
              OnClick="Button1_Click" />
            <input name="utxt_Code" runat="server" type="hidden" id="utxt_Code" />
          </td>
        </tr>
      </table>
    </div>
  </form>
</body>
</html>
