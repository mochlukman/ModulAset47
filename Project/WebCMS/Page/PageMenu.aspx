<%@ Page Language="C#" AutoEventWireup="True" ValidateRequest="false" Inherits="PageMenu"
  CodeFile="PageMenu.aspx.cs" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="../Ascx/SharedMethods.ascx" TagName="Methods" TagPrefix="uc" %>
<%@ Register Src="../Ascx/TreeShared.ascx" TagName="TreeMethods" TagPrefix="uc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Electronic Working Paper</title>
  <link href="Res/css/common.css" rel="stylesheet" type="text/css" />
  <link href="Res/css/userbox.css" rel="stylesheet" type="text/css" />
  <link href="Res/img/icon.ico" rel="shortcut icon" />
  <style type="text/css">
    .icon-expand-all2
    {
      background-image: url(../Res/Img/expand-all.gif) !important;
    }
    
    .icon-collapse-all2
    {
      background-image: url(../Res/Img/collapse-all.gif) !important;
    }
  </style>
  <script type="text/javascript">
</script>
</head>
<body>
  <form id="Form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" ShowWarningOnAjaxFailure="false" />
    <uc:Methods ID="Methods1" runat="server" name="Methods1" />
    <uc:TreeMethods ID="Methods2" runat="server" name="Methods2" />
    <ext:Menu ID="TabMenu" runat="server">
      <Items>
        <ext:MenuItem ID="MenuReload" runat="server" Text="Reload Page">
          <Listeners>
            <Click Handler="CoreNET.ReloadPage();" />
          </Listeners>
        </ext:MenuItem>
      </Items>
    </ext:Menu>
    <ext:Viewport ID="Viewport1" runat="server" Layout="Border">
      <Items>
        <ext:TreePanel ID="TreeGrid1" runat="server" Width="300" Height="240" NoLeafIcon="false"
          RootVisible="false" EnableDD="true" Region="West" Split="true">
          <Loader>
            <ext:PageTreeLoader OnNodeLoad="LoadPages" Timeout="360000" />
          </Loader>
          <BottomBar>
            <ext:Toolbar ID="BottomBar1" runat="server" />
          </BottomBar>
        </ext:TreePanel>
        <ext:TabPanel ID="Pages" runat="server" EnableTabScroll="true" TabPosition="Top" Region="Center">
          <Items>
          </Items>
          <Plugins>
            <ext:TabCloseMenu ID="TabCloseMenu1" runat="server" />
          </Plugins>
          <BottomBar>
            <ext:StatusBar ID="StatusBar2" runat="server">
              <Items>
                <ext:ToolbarFill />
                <ext:Label ID="Watch" runat="server" Cls="watch" />
              </Items>
            </ext:StatusBar>
          </BottomBar>
        </ext:TabPanel>
        <%--      <ext:BorderLayout ID="BorderLayout1" runat="server">
        <West CollapseMode="Mini" Collapsible="true">
        </West>
        <Center>
        </Center>
      </ext:BorderLayout>--%>
      </Items>
    </ext:Viewport>
  </form>
</body>
</html>
