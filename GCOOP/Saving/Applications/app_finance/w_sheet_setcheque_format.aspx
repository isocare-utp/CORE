<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_setcheque_format.aspx.cs"
    Inherits="Saving.Applications.app_finance.w_sheet_setcheque_format" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=reFresh %>
    <%=postSave %>
    <%=postRetrieveMain%>
    <%=postExample %>
    <link href="../../css/jquery.ui.all.css" rel="stylesheet" type="text/css" />

    <script src="../../js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="../../js/ui/jquery.ui.position.js" type="text/javascript"></script>

    <script src="../../js/ui/jquery.ui.core.js" type="text/javascript"></script>

    <script src="../../js/ui/jquery.ui.widget.js" type="text/javascript"></script>

    <script src="../../js/ui/jquery.ui.mouse.js" type="text/javascript"></script>

    <script src="../../js/ui/jquery.ui.draggable.js" type="text/javascript"></script>

    <link href="../../css/demos.css" rel="stylesheet" type="text/css" />
    <style>
        .chq-head
        {
            height: 10px;
            width: 100px;
            padding: 0.5em;
            float: left;
            font-family: Tahoma;
            font-size: small;
        }
        #draggable
        {
            background-color: Red;
            width: 150px;
            height: 50px;
            padding: 0.5em;
            font-size: larger;
        }
        .draggable
        {
            width: 90px;
            height: 90px;
            padding: 0.5em;
            float: left;
            margin: 0 10px 10px 0;
        }
        .containment-wrapper
        {
            width: 300px;
            height: 300px;
            border: 2px solid #ccc;
            padding: 10px;
            font-size: 12px;
        }
    </style>

    <script type="text/javascript">

        function Validate() {
            objDwMain.AcceptText();
            objDwMain.Update();
            postSave();
        }

        function MenubarNew() {
            Gcoop.OpenIFrameExtend(380, 160, "w_dlg_chqformat_add.aspx", "");
        }

        function DwListClick(sender, rowNumber, objectName) {
            Gcoop.GetEl("HdRow").value = rowNumber;
            postRetrieveMain();
        }

        function DwMainItemChanged(sender, rowNumber, columnName, newValue) {
            objDwMain.SetItem(rowNumber, columnName, newValue);
            objDwMain.AcceptText();
        }

        function ExampleButtonClick() {
            postExample();
        }

       
    </script>

    <script>
        $(function() {
            $("#ed-drag").draggable({ containment: "#ed-wrapper" });
            $("#drag-test").draggable({ containment: "#red-wrapper" });
            $("#dragtest2").draggable({ containment: "#red-wrapper" });
            $("#dragtest3").draggable({ containment: "#red-wrapper" });
        });
        
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table width="100%" border="0">
        <tr>
            <td valign="top">
                <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="300px">
                    <dw:WebDataWindowControl ID="DwList" runat="server" AutoRestoreContext="False" ClientFormatting="True"
                        ClientScriptable="True" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        ClientEventItemChanged="DwMainItemChanged" DataWindowObject="d_fin_chqformat_list"
                        LibraryList="~/DataWindow/app_finance/setcheque_format.pbl" ClientEventClicked="DwListClick">
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
            <td>
                ค่าคงที่
                <dw:WebDataWindowControl ID="DwMain" runat="server" AutoRestoreContext="False" ClientFormatting="True"
                    ClientScriptable="True" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                    ClientEventItemChanged="DwMainItemChanged" DataWindowObject="d_change_format_detail"
                    LibraryList="~/DataWindow/app_finance/setcheque_format.pbl">
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
    <br />
    
    
    <!-- <asp:Button ID="Button1" runat="server" Text="ตัวอย่าง" Height="30px" OnClientClick="ExampleButtonClick()" />
    <table width="750px" border="1">
        <tr>
            <td>
                ตัวอย่าง
                <dw:WebDataWindowControl ID="DwPrev" runat="server" DataWindowObject="d_cheque_printformat"
                    LibraryList="~/DataWindow/App_finance/setcheque_format.pbl" ClientScriptable="True"
                    AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                    Height="600px" Width="750px">
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
    <div id="red-wrapper" style="width: 300px; height: 300px; border: solid">
        <div id="drag-test" style="background-color: white; width: 90px">
            Drag me around
        </div>
        <div id="dragtest2" style="background-color: white; width: 90px; font-size: x-small">
            Drag me around
        </div>
        <div id="dragtest3" style="background-color: white; width: 90px; font-size: x-small">
            Drag me around
        </div>
    </div> -->

    <asp:HiddenField ID="HdRow" runat="server" />
</asp:Content> 
