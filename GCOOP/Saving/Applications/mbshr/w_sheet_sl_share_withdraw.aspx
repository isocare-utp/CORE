<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_sl_share_withdraw.aspx.cs"
    Inherits="Saving.Applications.mbshr.w_sheet_sl_share_withdraw" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=openDialogDetail %>
    <%=initShareWithdrawList %>
    <%=popupReport %>
    <script type="text/javascript">
        function OnDwListClicked(sender, rowNumber, objectName) {
            Gcoop.CheckDw(sender, rowNumber, objectName, "operate_flag", "1", "0");
        }
        function OpenLoanReceive() {
            if (CheckSelected()) {
                objdw_list.AcceptText();
                openDialogDetail();
            } else {
                return;
            }
        }

        function CheckSelected() {
            var allRow = objdw_list.RowCount();
            var i = 0;
            var haveChecked = false;

            for (i; i < allRow; i++) {
                var indexRow = i + 1;
                var check = objdw_list.GetItem(indexRow, "operate_flag");
                if (check == 1) {
                    haveChecked = true;
                }
            }
            if (haveChecked == true) {
                return true;
            } else {
                return false;
            }

        }


        function RefreshByDlg() {
            initShareWithdrawList();
        }

        function OnClickLinkNext() {
            //objdw_main.AcceptText();
            popupReport();
        }
        function SheetLoadComplete() {
            var pageCommand = Gcoop.GetEl("HfPageCommand").value;
            var check_dept = Gcoop.GetEl("Hidchkdept").value;
            if (check_dept == "TRUE")
            { }
            else {
                if (CheckSelected() && pageCommand == "opendialog") {
                    Gcoop.OpenDlg(855, 620, 'w_dlg_sl_popup_withdraw.aspx', '');
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:HiddenField ID="HfPageCommand" runat="server" />
    <asp:HiddenField ID="Hidchkdept" runat="server" />
     <asp:HiddenField ID="HdOpenIFrame" runat="server" Value="False" />
    <asp:HiddenField ID="HdcheckPdf" runat="server" Value="False" />
     <table style="width: 100%;">
        <tr>
            <td align="left">
                <span style="cursor: pointer" onclick="OnClickLinkNext();">พิมพ์ใบแจ้งปิดบัญชีเงินฝาก
                    ></span>
            </td>
            <td align="right">
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <input id="b_withdraw" style="float: right; width: 180px; cursor: pointer; height: 40px;
                    color: White; font-size: medium; font-weight: bold; background-color: Green;"
                    type="button" onclick="OpenLoanReceive()" value="ถอนหุ้น" />
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <dw:WebDataWindowControl ID="dw_list" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                    AutoSaveDataCacheAfterRetrieve="True" ClientEventClicked="OnDwListClicked" ClientFormatting="True"
                    ClientScriptable="True" DataWindowObject="d_sl_list_shrwtd" LibraryList="~/DataWindow/mbshr/sl_slipall.pbl">
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
</asp:Content>
