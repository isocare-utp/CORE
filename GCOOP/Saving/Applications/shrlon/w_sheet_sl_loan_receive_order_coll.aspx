<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_sl_loan_receive_order_coll.aspx.cs" 
    Inherits="Saving.Applications.shrlon.w_sheet_sl_loan_receive_order_coll" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=openDialogDetail %>
    <%=initShareWithdrawList %>
    <%=popupReport %>
    <%=postcheckAll %>
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
                    Gcoop.OpenDlg(855, 620, 'w_dlg_sl_popup_loanreceive_.aspx', '');
                }
            }
        }
        function ClickCheckAll() {
            if (objdw_list.RowCount() > 0) {
                postcheckAll();
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
   
    <table>
        <tr>
            <td>
            <asp:CheckBox ID="CheckAll" runat="server" Text="เลือกทั้งหมด" onclick="ClickCheckAll()" />
                <input id="b_withdraw" style="float: right; width: 180px; cursor: pointer; height: 40px;
                    color: White; font-size: medium; font-weight: bold; background-color: Green;"
                    type="button" onclick="OpenLoanReceive()" value="จ่ายเงินกู้" />
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <dw:WebDataWindowControl ID="dw_list" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                    AutoSaveDataCacheAfterRetrieve="True" ClientEventClicked="OnDwListClicked" ClientFormatting="True"
                    ClientScriptable="True" DataWindowObject="d_loansrv_list_lnrcv" LibraryList="~/DataWindow/shrlon/sl_loansrv_slip_all_cen.pbl">
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
</asp:Content>
