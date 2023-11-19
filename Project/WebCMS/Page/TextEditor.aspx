<%@ Page Language="C#" AutoEventWireup="True" ValidateRequest="false" Inherits="TextEditor"
  CodeFile="TextEditor.aspx.cs" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Pustaka</title>
  <script type="text/javascript">
    var home = function () {
      parent.home();
    }
    var SetText = function (text) {
      CoreNET.SetText(text);
    };
    var onClickUpload = function () {
      document.getElementById("loading-mask-upload").style.display = '';
    };
  </script>
</head>
<body>
  <form id="Form1" runat="server">
  <ext:ResourceManager ID="ResourceManager1" runat="server" />
  <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
    <Items>
      <ext:Panel ID="PanelText" runat="server" Width="610" Height="240" Html="" Padding="6"
        Layout="Fit" Title="" AutoScroll="true" PreventBodyReset="true" BodyStyle="font-family:tahoma;font-size:12px">
        <TopBar>
          <ext:Toolbar ID="Toolbar1" runat="server">
            <Items>
              <ext:Button ID="btnEdit1" runat="server" Text="Edit" Icon="Pencil">
              </ext:Button>
              <ext:Button ID="btnSave1" runat="server" Text="Save" Icon="Disk" Visible="true" Disabled="true"/>
              <ext:Button ID="btnCancel1" runat="server" Text="Batal" Icon="Reload" Disabled="false">
                <Listeners>
                  <Click Handler="this.setDisabled(true);#{btnEdit1}.setDisabled(false);#{BtnLink}.setDisabled(false);#{Toolbar2}.setVisible(false);#{PnlText}.setVisible(true);#{PanelEditor}.cancelEdit();CoreNET.NormalizeFrame();" />
                </Listeners>
              </ext:Button>
              <ext:Button ID="BtnLink" runat="server" Text="Copy Link" Icon="LinkAdd" Visible="true"
                Disabled="false">
                <Listeners>
                  <Click Handler="CoreNET.CopyLink();" />
                </Listeners>
              </ext:Button>
              <ext:Button ID="btnWord" runat="server" Text="Export ke Word (docx)" Icon="PageWord"
                Hidden="true">
              </ext:Button>
            </Items>
          </ext:Toolbar>
        </TopBar>
        <Items>
          <ext:Panel ID="PnlText" runat="server" Width="610" Height="240" Html="" Padding="6"
            Layout="Fit" Title="" AutoScroll="true" PreventBodyReset="true" BodyStyle="font-family:tahoma;font-size:12px">
          </ext:Panel>
          <ext:Editor ID="PanelEditor" runat="server" AutoSize="Fit" Shadow="None">
            <Field>
              <ext:HtmlEditor ID="HtmlEditor1" runat="server" />
            </Field>
          </ext:Editor>
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
                      <ext:Button runat="server" ID="Button4" Text="Upload" Icon="FolderUp" Width="70">
                        <Listeners>
                          <Click Handler="onClickUpload();CoreNET.BtnWindowUploadClick(HtmlEditor1.getValue());" />
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
      </ext:Panel>
      <ext:Window ID="WindowLink" runat="server" Icon="LinkAdd" Title="Add Link" Hidden="true"
        Modal="true" Width="700" Height="450">
        <Items>
          <ext:Panel runat="server" ID="PanelLink" Frame="true" Height="420">
          </ext:Panel>
        </Items>
      </ext:Window>
      <ext:Window ID="WindowPrint" runat="server" Icon="Printer" Title="Print" Hidden="true"
        Modal="true" Width="700" Height="450">
        <Items>
          <ext:Panel runat="server" ID="PanelPrint" Frame="true" Height="420">
          </ext:Panel>
        </Items>
      </ext:Window>
    </Items>
  </ext:Viewport>
  </form>
</body>
</html>
