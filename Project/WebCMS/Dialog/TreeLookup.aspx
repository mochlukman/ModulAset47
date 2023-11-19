<%@ Page Language="C#" AutoEventWireup="True" ValidateRequest="false" Inherits="TreeLookup"
  CodeFile="TreeLookup.aspx.cs" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="../Ascx/SharedMethods.ascx" TagName="Methods" TagPrefix="uc" %>
<%@ Register Src="../Ascx/TreeShared.ascx" TagName="TreeMethods" TagPrefix="uc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Filter Tabular</title>
  <script type="text/javascript">
    var locked = false;
    var setCheck = function myself(node, checked) {
      if (!locked) {
        for (var i = 0; i < node.childNodes.length; i++) {
          var n = node.childNodes[i];
          locked = true;
          n.attributes.checked = checked;
          n.getUI().checkbox.checked = checked;
          locked = false;
          if (n.childNodes.length > 0) {
            myself(n, checked);
          }
        }
      }
    }

    var onCheckedChange = function (node, checked) {
      setCheck(node, checked);
    }

  </script>
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
  <ext:ResourceManager ID="ResourceManager1" runat="server" />
  <uc:Methods ID="Methods1" runat="server" name="Methods1" />
  <uc:TreeMethods ID="Methods2" runat="server" name="Methods2" />
  <script type="text/javascript">
  </script>
  <ext:Viewport ID="VPLookup" runat="server" Layout="Fit">
    <Items>
      <ext:Panel ID="PCLookup" runat="server" Layout="BorderLayout">
        <TopBar>
          <ext:Toolbar ID="TBLookup1" runat="server" />
        </TopBar>
        <Items>
          <ext:TreePanel ID="TPLookup1" runat="server" Region="Center" Padding="5" Margins="0 0 5 5"
            AutoDoLayout="true" AutoScroll="true" NoLeafIcon="true" OnSubmit="SubmitNodes">
            <SelectionModel>
              <ext:MultiSelectionModel />
            </SelectionModel>
            <Loader>
              <ext:PageTreeLoader OnNodeLoad="LoadPages" />
            </Loader>
            <Listeners>
              <CheckChange Handler="node.expand(true);onCheckedChange(node, checked);" />
            </Listeners>
          </ext:TreePanel>
        </Items>
      </ext:Panel>
    </Items>
  </ext:Viewport>
  </form>
</body>
</html>
