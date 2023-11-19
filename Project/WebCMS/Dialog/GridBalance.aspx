<%@ Page Language="C#" AutoEventWireup="True" ValidateRequest="false" Inherits="GridBalance"
  CodeFile="GridBalance.aspx.cs" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="../Ascx/SharedMethods.ascx" TagName="Methods" TagPrefix="uc" %>
<%@ Register Src="../Ascx/TabularShared.ascx" TagName="TabMethods" TagPrefix="uc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Daftar Fitur</title>
  <script type="text/javascript">
    var closewin = function () {
      parent.WindowJurnal.hide(this);
      parent.refreshData();
    }
  </script>
  <style type="text/css">
    .number-selected
    {
      background: transparent url(star_n.png) repeat-x left center;
    }
    .number-unselected
    {
      background: transparent url(star_fade_n.png) repeat-x left center;
    }
  </style>
  <script type="text/javascript">
  </script>
</head>
<body>
  <form id="Form1" runat="server">
  <ext:ResourceManager ID="ResourceManager1" runat="server" ShowWarningOnAjaxFailure="false" />
  <uc:Methods ID="Methods1" runat="server" name="Methods1" />
  <uc:TabMethods ID="Methods2" runat="server" name="Methods2" />
  <ext:Store ID="Store1" runat="server" OnRefreshData="store_RefreshData" RemoteSort="false" />
  <ext:Menu ID="GridMenu" runat="server"/>
  <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
    <Items>
      <ext:Panel ID="PanelCenter" runat="server" Layout="BorderLayout">
        <Items>
          <ext:Panel ID="PanelFormEntry" runat="Server" modal="true" Padding="5" Resizeable="true"
            Layout="BorderLayout" Region="East" Split="true" CollapseMode="Mini" Collapsible="true"
            Collapsed="true" Width="500" Height="600">
            <TopBar>
              <ext:Toolbar ID="TBDialog1" runat="server" />
            </TopBar>
            <Items>
            </Items>
          </ext:Panel>
        </Items>
      </ext:Panel>
    </Items>
  </ext:Viewport>
  </form>
</body>
</html>
