<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_sl_loanmember_search.aspx.cs"
    Inherits="Saving.Applications.shrlon.dlg.w_dlg_sl_loanmember_search" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ค้นหาสมาชิก</title>
    <%=LoanContractSearch %>
    <script type="text/javascript">
        function selectRow(sender, rowNumber, objectName) {
            if (objectName != "datawindow") {
                var memberno = objdw_detail.GetItem(rowNumber, "member_no");
                try {
                    window.opener.GetValueFromDlgloanMemberSearch(memberno);
                    window.close();
                } catch (err) {
                    parent.GetValueFromDlgloanMemberSearch(memberno);
                    parent.RemoveIFrame();
                }
            }
        }
        function ItemDwDataChanged(sender, rowNumber, columnName, newValue) {
            if (columnName == "member_no") {

                objdw_data.SetItem(1, "member_no", newValue);
                Gcoop.GetEl("HdMemberNo").value = newValue;
                objdw_data.AcceptText();
                LoanContractSearch();
            }
            if (columnName == "salary_id") {

                objdw_data.SetItem(1, "salary_id", newValue);
                Gcoop.GetEl("HdSalaryId").value = newValue;
                objdw_data.AcceptText();
                LoanContractSearch();
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    รายการค้นหา
    <table style="width: 530px;">
        <tr>
            <td>
                <dw:WebDataWindowControl ID="dw_data" runat="server" DataWindowObject="d_sl_membsrch_criteria"
                    LibraryList="~/DataWindow/shrlon/sl_loan_requestment_pea.pbl" ClientScriptable="True"
                    ClientEventItemChanged="ItemDwDataChanged" AutoRestoreContext="False" AutoRestoreDataCache="True"
                    AutoSaveDataCacheAfterRetrieve="True">
                </dw:WebDataWindowControl>
            </td>
            <td>
                <asp:Button ID="cb_find" runat="server" Text="ค้นหา" Height="80px" Width="55px" OnClick="cb_find_Click"
                    Style="width: 55px; height: 80px;" />
            </td>
        </tr>
    </table>
    รายละเอียด
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:HiddenField ID="HdMemberNo" runat="server" />
    <asp:HiddenField ID="HdSalaryId" runat="server" />
    <asp:HiddenField ID="hidden_search" runat="server" />
    <dw:WebDataWindowControl ID="dw_detail" runat="server" ClientEventClicked="selectRow"
        DataWindowObject="d_sl_membsrch_list_memno" LibraryList="~/DataWindow/shrlon/sl_loan_requestment_cen.pbl"
        RowsPerPage="17" ClientScriptable="True" Width="650px" Height="450px">
        <PageNavigationBarSettings Position="Top" Visible="True" NavigatorType="Numeric">
            <BarStyle HorizontalAlign="Center" />
            <NumericNavigator FirstLastVisible="True" />
        </PageNavigationBarSettings>
    </dw:WebDataWindowControl>
    </form>
</body>
</html>
