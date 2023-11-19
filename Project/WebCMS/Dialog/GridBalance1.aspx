<%@ Page Language="C#" AutoEventWireup="True" ValidateRequest="false" 
  Inherits="GridBalance1" CodeFile="GridBalance1.aspx.cs" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Daftar Fitur</title>
  <script type="text/javascript">
    var closewin = function () {
      parent.WindowLookup1.hide(this);
      parent.refreshData();
    }
  </script>
</head>
<body>
  <form id="Form1" runat="server">
  <ext:ResourceManager ID="ResourceManager1" runat="server" ShowWarningOnAjaxFailure="false"/>
  <ext:Store ID="Store1" runat="server" OnRefreshData="store_RefreshData" />
  <ext:Menu ID="GridMenu" runat="server"/>
  <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
    <Items>
      <ext:Panel ID="PanelCenter" runat="server" Region="Center" Layout="BorderLayout">
      </ext:Panel>
    </Items>
  </ext:Viewport>
  <ext:Window ID="WindowFormJurnal" runat="Server" Modal="true" Hidden="true" Padding="5"
    Layout="FitLayout" CloseAction="Hide">
    <TopBar>
      <ext:Toolbar ID="TBDialog1" runat="server" />
    </TopBar>
    <Items>
    </Items>
  </ext:Window>
  </form>
</body>
</html>
