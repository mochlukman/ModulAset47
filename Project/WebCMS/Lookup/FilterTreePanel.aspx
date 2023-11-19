<%@ Page Language="C#" AutoEventWireup="True" ValidateRequest="false" Inherits="FilterTreePanel" CodeFile="FilterTreePanel.aspx.cs" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Filter Tabular</title>
  <script type="text/javascript">
  </script>
  <%--<link href="../Res/css/common.css" rel="stylesheet" type="text/css" />--%>
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
</head>
<body>
  <form id="Form1" runat="server">
  <ext:resourcemanager id="ResourceManager1" runat="server" />
  <ext:Hidden ID="Hidden1" runat="server" />
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
  <ext:viewport id="Viewport1" runat="server" layout="Fit">
    <items>
      <ext:TreeGrid ID="TreeGrid1" runat="server" Width="500" Height="240"  NoLeafIcon="false"
        EnableDD="true" Region="Center">
        <TopBar>
          <ext:Toolbar ID="TopBar1" runat="server">
          </ext:Toolbar>
        </TopBar>
        <Loader>
          <ext:PageTreeLoader OnNodeLoad="LoadPages" />
        </Loader>
      </ext:TreeGrid>
    </items>
  </ext:viewport>
  </form>
</body>
</html>
