<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_reprinttax50.aspx.cs"
    Inherits="Saving.Applications.app_finance.w_sheet_reprinttax50" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
    <%=postReceiptRetrieve %>
    <%=postSetItem %>
    <%=postPrint %>

    <script type="text/javascript">

        function Validate() {
            objDwHead.AcceptText();
            objDwList.AcceptText();
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function DwlistClick(sender, rowNumber, objectName) {
            if (objectName == "select_flag") {
                Gcoop.CheckDw(sender, rowNumber, objectName, "select_flag", 1, 0);
                objDwList.AcceptText();
            }
        }

        function DwHeadButtonClick(sender, rowNumber, buttonName) {
            if (buttonName == "b_search") {
                objDwHead.AcceptText();
                postReceiptRetrieve();
            }
        }

        function DwlistButtonclick(sender, rowNumber, buttonName) {
            if (buttonName == "b_print") {
                objDwHead.AcceptText();
                postPrint();
            }
        }

        function DwHeadItemChange(sender, rowNumber, columnName, newValue) {
            objDwHead.SetItem(rowNumber, columnName, newValue);
            Gcoop.GetEl("HfNewValue").value = newValue;
            Gcoop.GetEl("HfColoum").value = columnName;
            objDwHead.AcceptText();
            postSetItem();
        }
        
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table>
        <tr>
            <td>
                ค้นหา
                <dw:WebDataWindowControl ID="DwHead" runat="server" DataWindowObject="d_reprinttax50_head"
                    LibraryList="~/DataWindow/App_finance/reprinttax50.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                    ClientEventItemChanged="DwHeadItemChange" ClientEventButtonClicked="DwHeadButtonClick">
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:CheckBox ID="CheckBox1" runat="server" Text="จากสหกรณ์" />
                <asp:CheckBox ID="CheckBox2" runat="server" Text="หนังสือสำหรับผู้ถูกหักภาษี ณ ที่จ่าย" />
                <asp:CheckBox ID="CheckBox3" runat="server" Text="หนังสือสำหรับผู้หักภาษี ณ ที่จ่าย" />
            </td>
        </tr>
    </table>
    รายการใบสำคัญจ่าย
    <dw:WebDataWindowControl ID="DwList" runat="server" DataWindowObject="d_reprinttax50_list"
        LibraryList="~/DataWindow/App_finance/reprinttax50.pbl" AutoRestoreContext="False"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
        ClientEventClicked="DwlistClick" ClientEventButtonClicked="DwlistButtonclick">
    </dw:WebDataWindowControl>
    <asp:HiddenField ID="HfNewValue" runat="server" />
    <asp:HiddenField ID="HfColoum" runat="server" />
</asp:Content>
