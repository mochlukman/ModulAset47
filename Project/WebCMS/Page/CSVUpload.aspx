<%@ Page Language="C#" AutoEventWireup="True" ValidateRequest="false" CodeFile="CSVUpload.aspx.cs"
  Inherits="CSVUpload" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title></title>
  <script type="text/javascript">
    var home = function () {
      parent.home();
    }
    var refreshData = function () {
      Store1.reload();
    }

    var refreshDataWithSelection = function () {
      refreshData();
    }

  </script>
</head>
<body>
  <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" />
    <ext:Store ID="Store1" runat="server">
      <Reader>
        <ext:ArrayReader>
          <Fields>
            <ext:RecordField Name="value" />
            <ext:RecordField Name="text" />
          </Fields>
        </ext:ArrayReader>
      </Reader>
    </ext:Store>
    <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
      <Items>
        <ext:FormPanel ID="BasicForm" runat="server" Width="600" Frame="true" AutoHeight="false"
          MonitorValid="true" PaddingSummary="10px 10px 0 10px" LabelWidth="50">
          <Defaults>
            <ext:Parameter Name="anchor" Value="95%" Mode="Value" />
            <ext:Parameter Name="allowBlank" Value="false" Mode="Raw" />
            <ext:Parameter Name="msgTarget" Value="side" Mode="Value" />
          </Defaults>
          <Items>
            <ext:TextArea ID="txtPar" runat="server" FieldLabel="Query" Note="Kosongkan Query jika menggunakan query default. Exec Query untuk mengeksekusi Query" />
            <ext:ComboBox ID="ComboBox1" FieldLabel="Jenis Data" runat="server" StoreID="Store1"
              Editable="false" DisplayField="text" ValueField="value" TypeAhead="true" Mode="Local"
              ForceSelection="true" EmptyText="Pilih Jenis Data" Resizable="true" SelectOnFocus="true" />
            <ext:TextArea ID="txtInfo" runat="server" FieldLabel="Deskripsi" Height="200" Disabled="true" />
            <ext:FileUploadField ID="FileUploadField1" runat="server" EmptyText="Select location"
              FieldLabel="File" ButtonText="" Icon="ImageAdd" MinWidth="150" MaxWidth="400" IsUpload="true" />
            <ext:Checkbox ID="cbDelimiter" runat="server" Text="Using Comma as Delimiter (Default: Semi Colon)" FieldLabel="Using Comma as Delimiter" />
            <ext:Checkbox ID="cbDelete" runat="server" Text="Hapus Dulu" FieldLabel="Hapus Data" />
            <ext:Label ID="lbl_message" runat="server" Text="" />
          </Items>
          <Listeners>
            <ClientValidation Handler="#{SaveButton}.setDisabled(!valid);" />
          </Listeners>
          <Buttons>
            <ext:Button ID="ExecQuery" runat="server" allowEmpty="true" Text="Exec Query" />
            <ext:Button ID="ExportButton" runat="server" Text="Export" />
            <ext:Button ID="SaveButton" runat="server" Text="Impor">
              <DirectEvents>
                <Click OnEvent="UploadClick" Before="if (!#{BasicForm}.getForm().isValid()) { return false; } 
                                Ext.Msg.wait('Uploading your file...', 'Uploading');"
                  Failure="Ext.Msg.show({ 
                                title   : 'Error', 
                                msg     : 'Error during uploading', 
                                minWidth: 200, 
                                modal   : true, 
                                icon    : Ext.Msg.ERROR, 
                                buttons : Ext.Msg.OK 
                            });">
                </Click>
              </DirectEvents>
            </ext:Button>
            <ext:Button ID="Button1" runat="server" Text="Reset">
              <Listeners>
                <Click Handler="#{BasicForm}.getForm().reset();" />
              </Listeners>
            </ext:Button>
          </Buttons>
        </ext:FormPanel>
      </Items>
    </ext:Viewport>
    <ext:Window ID="WindowLink" runat="Server" Modal="true" Hidden="true" Padding="5"
      Title="Save Dialog" Width="500" Height="240" Layout="BorderLayout">
      <Items>
        <ext:FormPanel ID="BasicForm2" runat="server" Width="500" Frame="true" Region="Center"
          AutoHeight="false" MonitorValid="true" PaddingSummary="10px 10px 0 10px" LabelWidth="50">
          <Items>
            <ext:HyperLink ID="FileDownloadLink1" runat="server" Icon="Table" Target="_blank"
              NavigateUrl="http://www.ext.net" Text="http://www.ext.net" />
          </Items>
        </ext:FormPanel>
      </Items>
    </ext:Window>
  </form>
</body>
</html>
