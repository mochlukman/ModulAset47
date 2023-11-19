<%@ Page Language="C#" AutoEventWireup="True" ValidateRequest="false" Inherits="Default" CodeFile="Default.aspx.cs" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Home</title>
</head>
<body>
  <form id="Form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" ShowWarningOnAjaxFailure="false" />
    <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout" AutoHeight="false">
      <Items>
        <ext:BorderLayout ID="BorderLayout1" runat="server">
          <Items>
            <ext:Panel ID="PanelCenter" runat="server" Region="Center">
              <AutoLoad Mode="IFrame" ShowMask="true" />
            </ext:Panel>
          </Items>
        </ext:BorderLayout>
      </Items>
    </ext:Viewport>
  </form>
</body>
</html>
