<%@ Page Language="C#" AutoEventWireup="True" ValidateRequest="false" Inherits="GridLookup"
  CodeFile="GridLookup.aspx.cs" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="../Ascx/SharedMethods.ascx" TagName="Methods" TagPrefix="uc" %>
<%@ Register Src="../Ascx/TabularShared.ascx" TagName="TabMethods" TagPrefix="uc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Filter Tabular</title>
  <script type="text/javascript">
    var home = function () {
      parent.home();
    }
    var linkify = function (txt, cell, row) {
      return '<a href="#" onclick="CoreNET.OpenTab(\'' + txt + '\', \'' + row.data.Versi + '\');">' + txt + '</a>';
    };

    var iconify = function (status) {
      return '<img style="width:16px;height:16px;" src="../icons/' + status + '"/>';
    };

    var change = function (value) {
      return String.format(template, (value > 0) ? "green" : "red", value);
    };

    var formatdec = function (value) {
      return value; //.toLocaleString("#,##0.00");
    };  
    
    </script>
  <script type="text/javascript">
    var loadingmask = function () {
      document.getElementById("loading-mask").style.display = '';
    };
  </script>
</head>
<body>
  <form id="Form1" runat="server">
  <ext:ResourceManager ID="ResourceManager1" runat="server" />
  <uc:Methods ID="Methods1" runat="server" name="Methods1" />
  <uc:TabMethods ID="Methods2" runat="server" name="Methods2" />
  <ext:Store ID="Store1" runat="server" OnRefreshData="store_RefreshData" />
  <ext:Viewport ID="VPLookup" runat="server" Layout="Fit">
    <Items>
      <ext:Panel ID="PCLookup" runat="server" Layout="BorderLayout">
        <Items>
          <ext:Panel ID="PanelCenter" runat="server" Region="Center" Layout="BorderLayout">
          </ext:Panel>
        </Items>
      </ext:Panel>
    </Items>
  </ext:Viewport>
  </form>
</body>
</html>
