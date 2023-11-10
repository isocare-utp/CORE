<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_sl_member_search.aspx.cs"
    Inherits="Saving.Applications.mbshr.dlg.w_dlg_sl_member_search" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ค้นหาสมาชิก</title>
    <%=LoanContractSearch %>
    <%=postSalaryId%>
    <script type="text/javascript">
        function selectRow(sender, rowNumber, objectName) {
            if (objectName != "datawindow") {
                var memberno = objdw_detail.GetItem(rowNumber, "member_no");
                try {
                    window.opener.GetValueFromDlg(memberno);
                    window.close();
                } catch (err) {
                    parent.GetValueFromDlg(memberno);
                    parent.RemoveIFrame();
                }
            }
        }

        function ItemChangeDwData(sender, rowNumber, columnName, newValue) {
            if (columnName == "member_no") {
                var memberNoT = Gcoop.Trim(newValue);
                var memberNo = memberNoT;
                objdw_data.SetItem(1, "member_no", memberNo);
                Gcoop.GetEl("hmember_no").value = memberNo;
                objdw_data.AcceptText();
                // LoanContractSearch();
            }
            else if (columnName == "salary_id") {
                var str_temp = window.location.toString();
                var str_arr = str_temp.split("?", 2);
                //window.location = str_arr[0] + "?strvalue=" + Gcoop.StringFormat(newValue, "00000000");
                sender.SetItem(rowNumber, columnName, newValue);
                sender.AcceptText();
                postSalaryId();
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
                    LibraryList="~/DataWindow/mbshr/kp_recieve_return.pbl" ClientScriptable="True"
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
    <asp:HiddenField ID="hmember_no" runat="server" />
    <asp:HiddenField ID="hidden_search" runat="server" />
    <dw:WebDataWindowControl ID="dw_detail" runat="server" ClientEventClicked="selectRow"
        DataWindowObject="d_sl_membsrch_list_memno" LibraryList="~/DataWindow/mbshr/kp_recieve_return.pbl"
        RowsPerPage="17" ClientScriptable="True">
        <PageNavigationBarSettings Position="Top" Visible="True" NavigatorType="Numeric">
            <BarStyle HorizontalAlign="Center" />
            <NumericNavigator FirstLastVisible="True" />
        </PageNavigationBarSettings>
    </dw:WebDataWindowControl>
    </form>
</body>
</html>
