<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_chgstatuschq.aspx.cs" Inherits="Saving.Applications.app_finance.w_sheet_chgstatuschq" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postDetail %>

    <script type="text/javascript">

        function Validate() {
            objDwMain.AcceptText();
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function DwMainClicked(sender, rowNumber, objectName) {
            if (objectName == "cheque_no" || objectName == "date_tonchq" || objectName == "bank_desc" || objectName == "to_whom") {
                Gcoop.GetEl("HfRow").value = rowNumber;
                objDwMain.AcceptText();
                postDetail();
            }
        }
        
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:Panel ID="Panel1" runat="server" Width="720px" Height="250px" ScrollBars="Auto">
        <table>
            <tr>
                <td>
                    <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_change_chqstatus"
                        LibraryList="~/DataWindow/App_finance/chgstatuschq.pbl" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                        ClientEventClicked="DwMainClicked">
                    </dw:WebDataWindowControl>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <dw:WebDataWindowControl ID="DwDetail" runat="server" DataWindowObject="d_change_chqstatus_detail1"
        LibraryList="~/DataWindow/App_finance/chgstatuschq.pbl" VerticalScrollBar="Auto"
        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
        ClientScriptable="True" RowsPerPage="15" ClientEventItemChanged="DwListItemChange">
        <PageNavigationBarSettings NavigatorType="NumericWithQuickGo">
        </PageNavigationBarSettings>
    </dw:WebDataWindowControl>
    <asp:HiddenField ID="HfRow" runat="server" />
</asp:Content>
