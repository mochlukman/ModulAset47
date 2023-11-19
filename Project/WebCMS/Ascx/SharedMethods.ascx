<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SharedMethods.ascx.cs"
  Inherits="Ascx_SharedMethods" %>
<ext:Window ID="WindowDetil1" runat="Server" Modal="true" Hidden="true" Padding="5"
  Maximizable="true" Resizable="true" AutoDoLayout="true" Layout="FitLayout">
  <Items>
  </Items>
</ext:Window>
<ext:Window ID="WindowView1" runat="Server" Modal="true" Hidden="true" Padding="5"
  Maximizable="false" Resizable="false" AutoDoLayout="true" Layout="FitLayout" AutoHeight="false">
  <Items>
  </Items>
</ext:Window>
<ext:Window ID="WindowLink" runat="Server" Modal="true" Hidden="true" Padding="5"
  Title="Save Dialog" Width="500" Height="240" Layout="BorderLayout">
  <Items>
    <ext:FormPanel ID="BasicForm" runat="server" Width="500" Frame="true" Region="Center"
      AutoHeight="false" MonitorValid="true" PaddingSummary="10px 10px 0 10px" LabelWidth="50">
      <Items>
        <ext:Label ID="lblWaitLink" runat="server">
        </ext:Label>
        <ext:HyperLink ID="FileDownloadLink1" runat="server" Icon="Table" Target="_blank"
          NavigateUrl="http://www.ext.net" Text="http://www.ext.net" Hidden="true" />
      </Items>
    </ext:FormPanel>
  </Items>
</ext:Window>
