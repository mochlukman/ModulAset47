<%@ Page Language="C#" AutoEventWireup="True" ValidateRequest="false" Inherits="Page_FileBrowser" CodeFile="FileBrowser.aspx.cs" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <title>Pustaka</title>
  <script type="text/javascript">
    var onClickUpload = function () {
      document.getElementById("loading-mask-upload").style.display = '';
    };
    var opentab = function (el) {
      var link = el.getAttribute("data-download");
      window.open(link, '_blank');
    };
    var copylink = function (el) {
      var link = el.getAttribute("data-download");
      try{
        window.clipboardData.setData('Text', link);
        Ext.Msg.prompt("Information", "Paste Link for Cross Reference", null, null, true, link);
      }
      catch (e) 
      {
        Ext.Msg.prompt("Information", "Copy to clipboard: Ctrl+A, Ctrl+C", null, null, true, link);
      }
    };
    var opentab3 = function (title, url) {
      var tabpages = parent.Pages;
      if (!tabpages) {
        tabpages = parent.parent.parent.Pages;
      }
      var tab = tabpages.getItem(title);

      if (!tab) {
        tab = tabpages.add({
          id: title,
          title: title,
          closable: true,
          autoLoad: {
            showMask: true,
            url: url,
            mode: "iframe",
            maskMsg: "Loading ..."
          },
          listeners: {
            update: {
              fn: function (tab, cfg) {
                cfg.iframe.setHeight(cfg.iframe.getSize().height - 20);
              },
              scope: this,
              single: true
            }
          }
        });
      }
      tabpages.setActiveTab(tab);
    }
    var downloadFile = function (el) {
      var namafile = el.getAttribute("data-download");
      CoreNET.OpenTab(namafile);
    };
    var deleteFile = function (el) {
      var namafile = el.getAttribute("data-delete");
      Ext.Msg.confirm("Confirmation", "Delete " + namafile + "?", function (id, val) {
        if (id === "yes") {
          CoreNET.DeleteFile(namafile);
        }
      }, this);
    };
    var refreshTree = function () {
      CoreNET.RefreshMenu({
        success: function (result) {
          var nodes = eval(result);
          if (nodes.length > 0) {
            var path = '';
            if (!!TreePanel1.getSelectedNodes()) {
              path = TreePanel1.getSelectedNodes().path;
            };
            TreePanel1.initChildren(nodes);
            if (path != '') {
              CoreNET.ExpandTree(path);
            }
          }
          else {
            TreePanel1.getRootNode().removeChildren();
          }          
        }
      });
    }
    function select_all(obj) {
      var text_val = eval(obj);
      text_val.focus();
      text_val.select();
      if (!document.all) return; // IE only
      r = text_val.createTextRange();
      r.execCommand('copy');
    }
//    function Validate() {
//      var msg = "";
//      var bIsValid = true;
//      var uploadedFile = document.getElementById("<%#FileUploadField1.ClientID %>");

//      if (uploadedFile.files[0].size > 102400000) // greater than 512MB
//      {
//        msg += "File Size is limited to 100 MB!";
//        bIsValid = false;
//      }

////      var ext = uploadedFile.value.substr(uploadedFile.value.indexOf(".") + 1, uploadedFile.value.length);
////      if (ext != "jpg" || ext != "png") {
////        msg += "You can upload only .jpg or .png images!";
////        bIsValid = false;
////      }
//      if (!bIsValid) {
//        document.getElementById("Message").innerHTML = msg;
//        return false;
//      }
//      //On Success
//      return true;
//    }
  </script>
</head>
<body>
  <form id="Form1" runat="server">
  <ext:resourcemanager id="ResourceManager1" runat="server" />
  <ext:viewport id="Viewport1" runat="server" layout="BorderLayout">
    <items>
      <ext:BorderLayout ID="BorderLayout1" runat="server">
        <West CollapseMode="Mini" Collapsible="true">
          <ext:Panel ID="pnlWest" runat="server" AutoScroll="true" TabTip="Folder" Layout="Fit" Collapsed="true" Width="300">
          <Items>
            <ext:TreePanel ID="TreePanel1" runat="server" Width="300" Height="450" Icon="BookOpen"
              Title="Folder" RootVisible="true" AutoScroll="true" Region="West">
              <TopBar>
                <ext:Toolbar ID="tbFolder" runat="server">
                  <Items>
                    <ext:Button ID="BtnTreeAdd" runat="server" Icon="FolderAdd" Text="New">
                      <DirectEvents>
                        <Click OnEvent="BtnFolderAddClick" />
                      </DirectEvents>
                    </ext:Button>
                    <ext:Button ID="BtnTreeRename" runat="server" Icon="FolderEdit" Text="Rename">
                      <Listeners>
                        <Click Handler="CoreNET.BtnFolderRenameClick(TreePanel1.getSelectionModel().selNode.id);" />
                      </Listeners>
                    </ext:Button>
                    <ext:Button ID="BtnTreeDelete" runat="server" Icon="FolderDelete" Text="Hapus">
                      <DirectEvents>
                        <Click OnEvent="BtnFolderDeleteClick" />
                      </DirectEvents>
                    </ext:Button>
                    <ext:Button ID="BtnTreeRefresh" runat="server" Icon="Reload" Text="Refresh">
                      <Listeners>
                        <Click Handler="refreshTree();" />
                      </Listeners>
                    </ext:Button>
                  </Items>
                </ext:Toolbar>
              </TopBar>
              <Root>
                <ext:TreeNode NodeID="root" Text="Albums" />
              </Root>
              <DirectEvents>
                <Click OnEvent="TreeNodeClick" />
              </DirectEvents>
<%--              <Editors>
                <ext:TreeEditor ID="TreeEditor1" runat="server" AutoShow="false">
                  <Listeners>
                    <Complete Handler="CoreNET.TreeNodeRename(value, startValue);" />
                  </Listeners>
                </ext:TreeEditor>
              </Editors>--%>
            </ext:TreePanel>
            </Items>
            </ext:Panel>
          </West>
          <Center>
            <ext:Panel ID="Panel1" runat="server" Title="~/File" AutoScroll="true" Region="Center">
              <TopBar>
                <ext:Toolbar ID="tbFile" runat="server">
                  <Items>
                    <ext:Button ID="BtnFileRefresh" runat="server" Icon="Reload" Text="Refresh">
                      <DirectEvents>
                        <Click OnEvent="TreeNodeClick" />
                      </DirectEvents>
                    </ext:Button>
                    <ext:Button ID="BtnFileUpload" runat="server" Icon="PageAdd" Text="Upload File" Hidden="true">
                      <DirectEvents>
                        <Click OnEvent="BtnFileUploadClick" />
                      </DirectEvents>
                    </ext:Button>
                    <ext:Button ID="BtnNewExcel" runat="server" Icon="PageWhiteExcel" Text="New Excel"
                      Hidden="true">
                      <DirectEvents>
                        <Click OnEvent="BtnNewExcelClick" />
                      </DirectEvents>
                    </ext:Button>
                    <ext:Button ID="BtnNewWord" runat="server" Icon="PageWhiteWord" Text="New Word" Hidden="true">
                      <DirectEvents>
                        <Click OnEvent="BtnNewWordClick" />
                      </DirectEvents>
                    </ext:Button>
                  </Items>
                </ext:Toolbar>
              </TopBar>
              <Items>
                <ext:ListView ID="ListView1" runat="server" MultiSelect="true" EmptyText="" Width="650">
                  <Store>
                    <ext:Store ID="Store2" runat="server">
                      <Reader>
                        <ext:JsonReader IDProperty="name">
                          <Fields>
                            <ext:RecordField Name="name" />
                            <ext:RecordField Name="icon" />
                            <ext:RecordField Name="size" Type="Int" />
                            <ext:RecordField Name="credate" Type="Date" />
                            <ext:RecordField Name="lastmod" Type="Date" />
                            <ext:RecordField Name="download" />
                            <ext:RecordField Name="rellink" />
                          </Fields>
                        </ext:JsonReader>
                      </Reader>
                    </ext:Store>
                  </Store>
                  <Columns>
                    <ext:ListViewColumn Align="Right" Header="" Width="0.05" DataIndex="icon" Template='<img style="width:16px;height:16px;" src="../icons/{icon}-png/ext.axd" />' />
                    <ext:ListViewColumn Header="File" Width="0.45" DataIndex="name" Template='{name}' />
                    <ext:ListViewColumn Header="Created Date" Width="0.13" DataIndex="credate" Template='{credate:date("m-d h:i a")}' />
                    <ext:ListViewColumn Header="Last Modified" Width="0.13" DataIndex="lastmod" Template='{lastmod:date("m-d h:i a")}' />
                    <ext:ListViewColumn Header="Size" Width="0.1" DataIndex="size" Align="Right" Template="{size:fileSize}" />
                  </Columns>
                </ext:ListView>
              </Items>
            </ext:Panel>
          </Center>
        <South>
          <ext:StatusBar ID="StatusBarTree1" runat="server" onclick="select_all(this)" />
        </South>
        </ext:BorderLayout>
    </items>
  </ext:viewport>
  <ext:window id="WindowUploadFile" runat="server" icon="PageAdd" title="Upload File"
    hidden="true" modal="true" width="580" height="200">
    <items>
      <ext:FormPanel runat="server" ID="FormPanel1" Frame="true" AutoHeight="true" Padding="10">
        <Items>
          <ext:FileUploadField runat="server" ID="FileUploadField1" FieldLabel="File" Icon="Folder"
            Width="400" />
          <ext:TextArea runat="server" ID="TextField1" FieldLabel="Deskripsi" Width="400" TabTip="Klik upload untuk memulai" />
          <ext:CompositeField runat="server" ID="CompositeField1" Width="400">
            <Items>
              <ext:Button runat="server" ID="Button4" Text="Upload" Icon="FolderUp" Scale="Medium" Width="70">
                <Listeners>
                  <Click Handler="onClickUpload();CoreNET.BtnWindowUploadClick();" />
                </Listeners>
              </ext:Button>
              <ext:Label ID="lblWait" runat="server" Html='<div id="loading-mask-upload" style="display:none"><div id="loading"><div class="loading-indicator">Loading...</div></div></div>' />
            </Items>
          </ext:CompositeField>
        </Items>
      </ext:FormPanel>
    </items>
  </ext:window>
  <ext:window id="WindowAddFolder" runat="server" icon="PageAdd" title="Buat Folder Baru"
    hidden="true" modal="true" width="550" autoheight="true">
    <items>
      <ext:FormPanel runat="server" ID="FormPanel2" Frame="true" AutoHeight="true" Padding="10">
        <Items>
          <ext:CompositeField runat="server" ID="CompositeField2">
            <Items>
              <ext:TextField runat="server" ID="TextField2" FieldLabel="Nama Folder" Width="300" />
              <ext:Button runat="server" ID="Button1" Text="New Folder" Icon="Folder">
                <Listeners>
                  <Click Handler="CoreNET.BtnWindowAddClick(TextField2.getValue());" />
                </Listeners>
              </ext:Button>
            </Items>
          </ext:CompositeField>
        </Items>
      </ext:FormPanel>
    </items>
  </ext:window>
  </form>
</body>
</html>
