<%@ Page Language="C#" AutoEventWireup="True" ValidateRequest="false" CodeFile="Comment.aspx.cs"
  Inherits="System_Comment" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Forum</title>
  <script type="text/javascript">
    var home = function () {
      parent.home();
    }
    var onClickUpload = function () {
      document.getElementById("loading-mask-upload").style.display = '';
    };
  </script>
  <link href="../Res/Css/examples.css" rel="stylesheet" type="text/css" />
</head>
<body>
  <form id="Form1" runat="server">
  <ext:ResourceManager ID="ResourceManager1" runat="server" />
  <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
    <Items>
      <ext:FormPanel ID="FormPanel1" runat="server" Title="Forum" Icon="Book"
        Layout="FitLayout" Border="false" Frame="true" Timeout="600" AutoScroll="true">
        <TopBar>
          <ext:Toolbar ID="Toolbar1" runat="server">
            <Items>
              <ext:Button ID="btnAdd" runat="server" Text="Edit" Icon="Pencil" />
              <ext:Button ID="btnSave" runat="server" Text="Simpan" OnDirectClick="OnSave" Icon="Disk"
                Disabled="true" />
              <ext:Button ID="btnCancel" runat="server" Text="Batal" OnDirectClick="OnCancel" Icon="Reload"
                Disabled="true" />
            </Items>
          </ext:Toolbar>
        </TopBar>
        <Items>
          <ext:HtmlEditor ID="HtmlEditor1" runat="server" Hidden="true" />
          <ext:Panel ID="pnlComment" runat="server" />
        </Items>
        <BottomBar>
          <ext:Toolbar ID="Toolbar2" runat="server" Layout="FitLayout" Hidden="true">
            <Items>
              <ext:FormPanel runat="server" ID="FormPanel2" Frame="true" AutoHeight="true" Padding="10">
                <Items>
                  <ext:CompositeField runat="server" ID="CompositeField1" Width="400">
                    <Items>
                      <ext:FileUploadField runat="server" ID="FileUploadField1" FieldLabel="File" Icon="Folder"
                        Width="300" />
                      <ext:Button runat="server" ID="Button4" Text="Upload" Icon="FolderUp" 
                        Width="70">
                        <Listeners>
                          <Click Handler="onClickUpload();CoreNET.BtnWindowUploadClick();" />
                        </Listeners>
                      </ext:Button>
                      <ext:Label ID="lblWait" runat="server" Html='<div id="loading-mask-upload" style="display:none"><div id="loading"><div class="loading-indicator">Loading...</div></div></div>' />
                    </Items>
                  </ext:CompositeField>
                </Items>
              </ext:FormPanel>
            </Items>
          </ext:Toolbar>
        </BottomBar>
      </ext:FormPanel>
    </Items>
  </ext:Viewport>
  </form>
</body>
</html>
