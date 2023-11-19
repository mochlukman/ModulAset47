<%@ Page Language="C#" AutoEventWireup="True" ValidateRequest="false" Inherits="PageMasterDetil"
  CodeFile="PageMasterDetil.aspx.cs" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Daftar Fitur</title>
  <script type="text/javascript">
    var home = function () {
      parent.home();
    }
    var loadPageByID = function (treeID, id, title, url) {
      parent.loadPageByID(treeID, id, title, url);
    }
    var refreshDataWithSelection = function () {
      Panel1.getBody().refreshDataWithSelection();
    }
    var record = null;
      var NormalizeFrame = function () {
      CoreNET.NormalizeFrame();
    }
  </script>
</head>
<body>
  <form id="Form1" runat="server">
  <ext:ResourceManager ID="ResourceManager1" runat="server" DirectMethodNamespace="MyMethods" />
  <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
    <Items>
      <ext:Panel ID="Panel1" runat="server" Region="North" Height="250" Collapsible="true"
        Split="true" AnimCollapse="false" />
      <ext:TabPanel ID="TabPanel1" runat="server" Region="Center" AnimCollapse="true" Hidden="false" />
    </Items>
  </ext:Viewport>
  </form>
</body>
</html>
