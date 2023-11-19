<%@ Page Language="C#" AutoEventWireup="True" ValidateRequest="false" Inherits="FilterTabular" CodeFile="FilterTabular.aspx.cs" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="../Ascx/SharedMethods.ascx" TagName="Methods" TagPrefix="uc" %>
<%@ Register Src="../Ascx/TabularShared.ascx" TagName="TabMethods" TagPrefix="uc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Filter Tabular</title>
    <script type="text/javascript">
        var home = function () {
            parent.WindowFilter.hide();
            parent.home();
        }
        var formatdec = function (value) {
            return value; //.toLocaleString("#,##0.00");
        };

    </script>
</head>
<body>
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <uc:methods id="Methods1" runat="server" name="Methods1" />
        <uc:tabmethods id="Methods2" runat="server" name="Methods2" />
        <ext:Store ID="Store1" runat="server" OnRefreshData="store_RefreshData" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
            <Items>
                <ext:Panel ID="PanelCenter" runat="server" Layout="BorderLayout">
                </ext:Panel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
