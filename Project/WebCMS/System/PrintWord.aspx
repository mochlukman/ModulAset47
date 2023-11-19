<%@ Page Language="C#" AutoEventWireup="True" ValidateRequest="false" CodeFile="PrintWord.aspx.cs" Inherits="System_PrintWord" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title>Convert HTML to Word</title>
  <script type="text/javascript">
    var home = function () {
      parent.home();
    }
  </script>
	<style type="text/css">
		body
		{
			font-family: Arial, 'DejaVu Sans' , 'Liberation Sans' , Freesans, sans-serif;
		}
	</style>
</head>
<body>
	<form id="Form2" runat="server">
	<ext:resourcemanager id="ResourceManager1" runat="server" />
	<ext:viewport id="Viewport1" runat="server" layout="Fit">
		<items>
    <ext:Label ID="label1" runat="server" Text="" />
<%--			<ext:LinkButton id="docSavedLink" runat="server" Text="Dokumen telah disimpan" onDirectClick="openTheDoc" Hidden="true" />--%>
    </items>
	</ext:viewport>
	</form>
</body>
</html>
