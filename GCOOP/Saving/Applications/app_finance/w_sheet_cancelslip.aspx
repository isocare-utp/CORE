<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_cancelslip.aspx.cs"
    Inherits="Saving.Applications.app_finance.w_sheet_cancelslip" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postRetreiveSlip%>
    <%=postProtect %>
    <script type="text/javascript">

        function Validate() {
            objDwList.AcceptText();
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function MenubarNew() {
            window.location = state.SsUrl + "Applications/app_finance/w_sheet_cancelslip.aspx";
        }

        function DwListChecked(sender, row, col, nValue) {
            objDwList.SetItem(row, col, nValue);
            objDwList.AcceptText();
        }

        function DwListClick(sender, rowNumber, objectName) {
            Gcoop.CheckDw(sender, rowNumber, objectName, "ai_select", 1, 0);
            objDwList.AcceptText();
        }

        function DwHeadButtonClicked(sender, row, oName) {
            objDwHead.AcceptText();
            postRetreiveSlip();
        }

        function DwHeadClick(sender, rowNumber, objectName) {
            if (objectName == "fixcash_type") {
                Gcoop.CheckDw(sender, rowNumber, objectName, "fixcash_type", 1, 0);
                objDwHead.AcceptText();
                postProtect();
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table>
        <tr>
            <td>
                <dw:WebDataWindowControl ID="DwHead" runat="server" DataWindowObject="d_cancelslip_head"
                    LibraryList="~/DataWindow/App_finance/cancelslip.pbl" AutoRestoreContext="false"
                    ClientScriptable="true" AutoRestoreDataCache="true" AutoSaveDataCacheAfterRetrieve="True"
                    ClientEventButtonClicked="DwHeadButtonClicked" ClientEventClicked="DwHeadClick">
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
    <hr />
    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Height="500px">
        <dw:WebDataWindowControl ID="DwList" runat="server" DataWindowObject="d_cancelslip_list"
            LibraryList="~/DataWindow/App_finance/cancelslip.pbl" ClientScriptable="true"
            AutoRestoreContext="false" AutoRestoreDataCache="true" ClientEventItemChanged="DwListChecked"
            AutoSaveDataCacheAfterRetrieve="True" ClientEventClicked="DwListClick">
            <PageNavigationBarSettings NavigatorType="NumericWithQuickGo">
            </PageNavigationBarSettings>
        </dw:WebDataWindowControl>
    </asp:Panel>
</asp:Content>
