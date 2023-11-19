<%@ Page Language="C#" AutoEventWireup="True" ValidateRequest="false" Inherits="MainMenu"
  CodeFile="MainMenu.aspx.cs" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Electronic Working Paper</title>
  <link href="Res/css/common.css" rel="stylesheet" type="text/css" />
  <link href="Res/css/userbox.css" rel="stylesheet" type="text/css" />
  <link href="Res/img/icon.ico" rel="shortcut icon" />
  <script type="text/javascript">
  </script>
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
    <div id="status" runat="server">
      <div id="avatar" runat="server">
      </div>
      <div id="about" runat="server">
        <div id="userid" runat="server"/>
        <div id="nama" runat="server"/>
        <div id="nip" runat="server"/>
        <div id="email" runat="server"/>
        <div id="nohp" runat="server"/>
        <div id="role" runat="server"/>
        <div id="uraian" runat="server"/>
        <div id="ipaddr" runat="server"/>
      </div>
      <div id="divbuttons" runat="server">
        <div id="logout" runat="server">
          <img src="Res/Img/overlogout.png" alt="" style="top: 0px; left: 0px; position: absolute" />
        </div>
      </div>
    </div>
    <ext:Viewport ID="Viewport1" runat="server" Layout="Border">
      <Items>
        <ext:BorderLayout ID="BorderLayout1" runat="server">
          <North>
            <ext:Panel ID="TopPanel" runat="server" Height="64px" Layout="BorderLayout">
              <TopBar>
                <ext:Toolbar ID="StatusBar1" runat="server" LabelAlign="Left" Height="32px" Layout="BorderLayout">
                  <Items>
                    <ext:Panel ID="Panel1" runat="server" Visible="true" AutoHeight="true" Border="false"
                      Header="false" Region="North" Html="">
                    </ext:Panel>
                    <ext:Panel ID="pnlUser" runat="server" Visible="true" AutoHeight="true" Border="false"
                      Header="false" Region="Center" Html="" AnchorHorizontal="100%">
                    </ext:Panel>
                  </Items>
                </ext:Toolbar>
              </TopBar>
              <Items>
                <ext:Toolbar runat="server" ID="toolBar" Region="Center">
                  <Items>
                    <ext:TextField ID="textFilterIdapp" DataIndex="Idapp" runat="server" EmptyText="" Disabled="true" AnchorHorizontal="95%"
                      TypeAhead="true" Width="310" Text="27DD1F6E-511D-4637-BCA6-DE61082B7246" Hidden="true" />
                    <ext:TextField ID="textFilterKdapp" DataIndex="Kdapp" runat="server" EmptyText="" Disabled="true" AnchorHorizontal="95%"
                      TypeAhead="true" Width="310" Text="" ReadOnly="true" Hidden="true" />
                    <ext:TextField ID="textFilterNmapp" DataIndex="Nmapp" runat="server" EmptyText="" Disabled="true" AnchorHorizontal="95%"
                      TypeAhead="true" Width="310" Text="" ReadOnly="true" />
                    <ext:Button ID="btnLookupApp" runat="server" ToolTip="Lookup" Icon="Magnifier" Hidden="false" />
                    <ext:Button ID="btnRefresh" runat="server" ToolTip="Refresh" Icon="ArrowRefresh" Hidden="false" />
                    <ext:ToolbarSeparator />
                    <ext:Button ID="btnMenu" runat="server" ToolTip="Application" Icon="ApplicationGo" />
                    <%--                    <ext:Button ID="btnApp" runat="server" ToolTip="Application" Icon="ApplicationGo">
                      <Menu>
                        <ext:Menu ID="PanelsMenu" runat="server">
                          <Items>
                            <ext:ComponentMenuItem ID="menuCoaching" runat="server" Shift="false">
                              <Component>
                                <ext:Panel ID="PnlNotify" runat="server" Width="800" Height="300" />
                              </Component>
                            </ext:ComponentMenuItem>
                          </Items>
                        </ext:Menu>
                      </Menu>
                    </ext:Button>--%>
                    <ext:Button ID="btnForum" runat="server" ToolTip="Coaching" Icon="EmailStar" />
                    <ext:ToolbarTextItem ID="lblCoach" runat="server" />
                    <ext:Label ID="lblWait2" runat="server" Html='<div id="loading-mask2" style="display:none"><div id="loading"><div class="loading-indicator">Loading..</div></div></div>' />
                  </Items>
                </ext:Toolbar>
              </Items>
            </ext:Panel>
          </North>
          <West CollapseMode="Mini" Collapsible="true">
            <ext:Panel ID="PnlWest" runat="server" Region="West" Layout="Accordion"
              Split="true" CollapseMode="Mini" Collapsible="true"
              Width="310" Title="Daftar Menu" Icon="ChartOrganisation">
              <Items>
                <ext:TreePanel ID="TreePanel1" runat="server"/>
                <ext:Panel ID="Pnl2" runat="server"></ext:Panel>
              </Items>
            </ext:Panel>
          </West>
          <Center>
            <ext:TabPanel ID="Pages" runat="server" EnableTabScroll="true" TabPosition="Top">
              <Items>
                <ext:Panel ID="tabHome" runat="server" Title="Monitoring" ContextMenuID="TabMenu"
                  IconCls="icon-application">
                  <AutoLoad Mode="IFrame" ShowMask="true" />
                </ext:Panel>
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
          </Center>
          <East>
          </East>
        </ext:BorderLayout>
      </Items>
    </ext:Viewport>
    <ext:TaskManager ID="TaskManager1" runat="server">
      <Tasks>
        <ext:Task TaskID="servertime" Interval="1000">
          <Listeners>
            <Update Handler="#{Watch}.setText(new Date().dateFormat('H:i:s'));" />
          </Listeners>
        </ext:Task>
      </Tasks>
    </ext:TaskManager>
    <ext:Window ID="WindowLink" runat="server" Icon="CarRed" Title="Garuda" Hidden="true"
      Modal="true" Width="700" Height="450">
      <Items>
        <ext:Panel runat="server" ID="PanelLink" Frame="true" Height="420">
        </ext:Panel>
      </Items>
    </ext:Window>
  </form>
</body>
</html>
