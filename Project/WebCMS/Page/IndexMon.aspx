<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true"
  CodeFile="IndexMon.aspx.cs" Inherits="IndexMon" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<html>
<head>
<title>Daftar Aplikasi</title>
</head>
<body>
  <form id="Form1" runat="server">
  <ext:ResourceManager ID="ResourceManager1" runat="server" ShowWarningOnAjaxFailure="false" />
  <ext:Menu ID="TabMenu" runat="server">
    <Items>
      <ext:MenuItem ID="MenuReload" runat="server" Text="Reload Page">
        <Listeners>
          <Click Handler="CoreNET.ReloadPage();" />
        </Listeners>
      </ext:MenuItem>
    </Items>
  </ext:Menu>
  <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
    <Items>
      <ext:TreeGrid ID="TreePanel1" runat="server" Width="310" Title="Daftar Menu" Icon="ChartOrganisation"
        Split="true" >
      </ext:TreeGrid>
    </Items>
  </ext:Viewport>
  </form>
</body>
</html>
