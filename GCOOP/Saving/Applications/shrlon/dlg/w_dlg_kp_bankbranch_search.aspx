<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_kp_bankbranch_search.aspx.cs"
    Inherits="Saving.Applications.shrlon.dlg.w_dlg_kp_bankbranch_search" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ค้นหาสมาชิก</title>
    <script type="text/javascript">
        function selectRow(sender, rowNumber, objectName) {
            if (objectName != "datawindow") {
                var branch_id = objdw_detail.GetItem(rowNumber, "branch_id");
                var branch_name = objdw_detail.GetItem(rowNumber, "branch_name");
                try {
                    window.opener.GetBankBranchFromDlg(branch_id, branch_name);
                    window.close();
                } catch (err) {
                    parent.GetBankBranchFromDlg(branch_id, branch_name);
                    parent.RemoveIFrame();
                }
            }
        }
        function ItemChangeDwData(sender, rowNumber, columnName, newValue) {
            if (columnName == "branch_id") {
                var branch_id = Gcoop.Trim(newValue);
                objdw_data.SetItem(1, "branch_id", branch_id);
                Gcoop.GetEl("hbranch_id").value = branch_id;
                objdw_data.AcceptText();
                // LoanContractSearch();
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
                <dw:WebDataWindowControl ID="dw_data" runat="server" DataWindowObject="d_kp_bankbranchserch_criteria"
                    LibraryList="~/DataWindow/mbshr/mb_req_chggroup.pbl" ClientScriptable="True"
                    ClientEventItemChanged="ItemChangeDwData" AutoRestoreContext="False" AutoRestoreDataCache="True"
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
    <asp:HiddenField ID="hbranch_id" runat="server" />
    <asp:HiddenField ID="hidden_search" runat="server" />
    <dw:WebDataWindowControl ID="dw_detail" runat="server" ClientEventClicked="selectRow"
        DataWindowObject="d_dlg_kp_bankbranch_list" LibraryList="~/DataWindow/mbshr/mb_req_chggroup.pbl"
        RowsPerPage="17" ClientScriptable="True">
        <PageNavigationBarSettings Position="Top" Visible="True" NavigatorType="Numeric">
            <BarStyle HorizontalAlign="Center" />
            <NumericNavigator FirstLastVisible="True" />
        </PageNavigationBarSettings>
    </dw:WebDataWindowControl>
    </form>
</body>
</html>
