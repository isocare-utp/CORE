<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_sl_member_new_search_coopid.aspx.cs"
    Inherits="Saving.Applications.mbshr.dlg.w_dlg_sl_member_new_search_coopid" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ค้นหาสมาชิก</title>
    <%=LoanContractSearch %>
    <script type="text/javascript">
        function selectRow(sender, rowNumber, objectName) {
            var docno = objdw_detail.GetItem(rowNumber, "appl_docno");
            var coop_id = objdw_detail.GetItem(rowNumber, "coop_id");
            window.opener.GetValueFromDlgCoopId(docno,coop_id);
            window.close();
        }
        function ItemChangeDwData(sender, rowNumber, columnName, newValue) {
            if (columnName == "docno") {
                var docNoT = Gcoop.Trim(newValue);
                var docNo = Gcoop.StringFormat(docNo, "0000000000");
                
                objdw_data.SetItem(1, "docno", appl_docno);
                Gcoop.GetEl("hmember_no").value = docNo;
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
                <dw:WebDataWindowControl ID="dw_data" runat="server" 
                    DataWindowObject="d_sl_member_new_search_criteria_coopid"
                    LibraryList="~/DataWindow/mbshr/sl_member_new_search.pbl" 
                    ClientScriptable="True"
                    ClientEventItemChanged="ItemChangeDwData" 
                    AutoRestoreContext="False" 
                    AutoRestoreDataCache="True"
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
        DataWindowObject="d_sl_member_new_list" LibraryList="~/DataWindow/mbshr/sl_member_new_search.pbl"
        RowsPerPage="17" ClientScriptable="True">
        <PageNavigationBarSettings Position="Top" Visible="True" NavigatorType="Numeric">
            <BarStyle HorizontalAlign="Center" />
            <NumericNavigator FirstLastVisible="True" />
        </PageNavigationBarSettings>
    </dw:WebDataWindowControl>
    </form>
</body>
</html>
