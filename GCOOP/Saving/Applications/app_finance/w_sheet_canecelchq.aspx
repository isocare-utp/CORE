<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_canecelchq.aspx.cs"
    Inherits="Saving.Applications.app_finance.w_sheet_canecelchq" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postBankBranch%>
    <%=postChqSearch %>
    <%=postFormat %>

    <script type="text/javascript">

        function Validate() {
            objDwHead.AcceptText();
            objDwList.AcceptText();
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function DwListItemChange(sender, rowNumber, columnName, newValue) {
            objDwList.SetItem(rowNumber, columnName, newValue);
            objDwList.AcceptText();
        }

        function DwListClick(sender, rowNumber, objectName) {
            Gcoop.CheckDw(sender, rowNumber, objectName, "ai_flag", 1, 0);
            objDwList.AcceptText();
        }

        function DwHeadItemChange(sender, rowNumber, columnName, newValue) {
            objDwHead.SetItem(rowNumber, columnName, newValue);
            objDwHead.AcceptText();
            if (columnName == "chq_no") {
                Gcoop.GetEl("HfContact").value = newValue;
                objDwHead.AcceptText();
                postFormat();
            }
            else if (columnName ==  "bank") {
                objDwHead.SetItem(rowNumber, columnName, newValue);
                objDwHead.AcceptText();
                postBankBranch();
            }
        }

        function DwHeadButtonClick(sender, rowNumber, buttonName) {
            objDwHead.AcceptText();
            postChqSearch();
        }
        
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <dw:WebDataWindowControl ID="DwHead" runat="server" DataWindowObject="d_search_cancel"
        LibraryList="~/DataWindow/App_finance/canecelchq.pbl" AutoRestoreContext="False"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
        ClientEventItemChanged="DwHeadItemChange" ClientEventButtonClicked="DwHeadButtonClick">
    </dw:WebDataWindowControl>
    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
        <table>
            <tr>
                <td>
                    <dw:WebDataWindowControl ID="DwList" runat="server" DataWindowObject="d_cancel_chequelist"
                        LibraryList="~/DataWindow/App_finance/canecelchq.pbl" Width="720px" VerticalScrollBar="Auto"
                        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        ClientScriptable="True" RowsPerPage="15" ClientEventItemChanged="DwListItemChange"
                        ClientEventClicked="DwListClick" PageNavigationBarSettings-Visible="True" PageNavigationBarSettings-NavigatorType="Numeric">
                        <PageNavigationBarSettings NavigatorType="NumericWithQuickGo">
                        </PageNavigationBarSettings>
                    </dw:WebDataWindowControl>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:HiddenField ID="HfContact" runat="server" />
</asp:Content>
