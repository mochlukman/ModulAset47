<%@ Page Language="C#" AutoEventWireup="True" ValidateRequest="false" CodeFile="Error.aspx.cs"
  Inherits="Error" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title></title>
</head>
<body>
  <form id="Form1" runat="server">
  <ext:ResourceManager ID="ResourceManager1" runat="server" ShowWarningOnAjaxFailure="false" />
  <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
    <Items>
    <ext:Panel ID="Panel1" runat="server"></ext:Panel>
    </Items>
  </ext:Viewport>
  </form>
</body>
</html>
