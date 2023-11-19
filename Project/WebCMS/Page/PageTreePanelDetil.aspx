<%@ Page Language="C#" AutoEventWireup="True" ValidateRequest="false" Inherits="PageTreePanelDetil"
  CodeFile="PageTreePanelDetil.aspx.cs" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="../Ascx/SharedMethods.ascx" TagName="Methods" TagPrefix="uc" %>
<%@ Register Src="../Ascx/TreeShared.ascx" TagName="TreeMethods" TagPrefix="uc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Daftar Fitur</title>
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
  <ext:ResourceManager ID="ResourceManager1" runat="server" />
  <uc:Methods ID="Methods1" runat="server" name="Methods1" />
  <uc:TreeMethods ID="Methods2" runat="server" name="Methods2" />
  <ext:Hidden ID="Hidden1" runat="server" Style="text-decoration: none" />
  <ext:Hidden ID="Hidden2" runat="server" />
  <ext:Hidden ID="Hidden3" runat="server" />
  <ext:Hidden ID="Hidden4" runat="server" />
  <ext:Hidden ID="Hidden5" runat="server" />
  <ext:Hidden ID="Hidden6" runat="server" />
  <ext:Hidden ID="Hidden7" runat="server" />
  <ext:Hidden ID="Hidden8" runat="server" />
  <ext:Hidden ID="Hidden9" runat="server" />
  <ext:Hidden ID="Hidden10" runat="server" />
  <ext:Hidden ID="Hidden11" runat="server" />
  <ext:Menu ID="GridMenu" runat="server" Visible="true"/>
  <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
    <Items>
      <ext:Panel ID="PanelCenter" runat="server" Layout="BorderLayout">
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
          <ext:Panel ID="PanelFormEntry" runat="Server" modal="true" Padding="5" Resizeable="true"
            Layout="BorderLayout" Region="Center" Split="true" CollapseMode="Mini" Collapsible="false"
            Width="1000" Height="600">
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
