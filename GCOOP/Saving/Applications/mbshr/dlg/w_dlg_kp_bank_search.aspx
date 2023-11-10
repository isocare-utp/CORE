<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_kp_bank_search.aspx.cs"
    Inherits="Saving.Applications.mbshr.dlg.w_dlg_kp_bank_search" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ค้นหาสมาชิก</title>
    
    <script type="text/javascript">
        function selectRow(sender, rowNumber, objectName) {
            if (objectName != "datawindow") {
                var bank_code = objdw_detail.GetItem(rowNumber, "bank_code");
                try {
                    window.opener.GetBankFromDlg(bank_code);
                    window.close();
                } catch (err) {
                    parent.GetBankFromDlg(bank_code);
                    parent.RemoveIFrame();
                }
            }
        }
        function ItemChangeDwData(sender, rowNumber, columnName, newValue) {
            if (columnName == "bank_code") {
                var bank_code = Gcoop.Trim(newValue);
                objdw_data.SetItem(1, "bank_code", bank_code);
                Gcoop.GetEl("hbank_code").value = bank_code;
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
                    DataWindowObject="d_kp_bankserch_criteria"
                    LibraryList="~/DataWindow/mbshr/mb_req_chggroup.pbl"
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
    <asp:HiddenField ID="hbank_code" runat="server" />
    <asp:HiddenField ID="hidden_search" runat="server" />
    <dw:WebDataWindowControl ID="dw_detail" runat="server" ClientEventClicked="selectRow"
        DataWindowObject="d_dlg_kp_bank_list" LibraryList="~/DataWindow/mbshr/mb_req_chggroup.pbl"
        RowsPerPage="17" ClientScriptable="True">
        <PageNavigationBarSettings Position="Top" Visible="True" NavigatorType="Numeric">
            <BarStyle HorizontalAlign="Center" />
            <NumericNavigator FirstLastVisible="True" />
        </PageNavigationBarSettings>
    </dw:WebDataWindowControl>
    </form>
</body>
</html>
