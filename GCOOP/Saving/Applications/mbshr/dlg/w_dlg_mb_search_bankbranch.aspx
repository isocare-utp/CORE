<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_mb_search_bankbranch.aspx.cs" Inherits="Saving.Applications.mbshr.dlg.w_dlg_mb_search_bankbranch" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>รายการค้นหาสาขา</title>
    <script type="text/javascript">
        function selectRow(sender, rowNumber, objectName) {
            if (objectName != "datawindow") {
                var BankBranch_code = sender.GetItem(rowNumber, "branch_id");
                var BankBranch_desc = sender.GetItem(rowNumber, "branch_name");
                try {
                    window.opener.GetValueFromDlgSeachBankBranch(BankBranch_code, BankBranch_desc);
                    window.close();
                } catch (err) {
                    parent.GetValueFromDlgSeachBankBranch(BankBranch_code, BankBranch_desc);
                    parent.RemoveIFrame();
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       รายการค้นหา
    <table style="width: 450px;">
        <tr>
            <td>
                 <dw:WebDataWindowControl ID="DwBankBranch" runat="server" ClientEventClicked="selectRow"
                     DataWindowObject="d_ucfbankbranch_searchdesc" LibraryList="~/DataWindow/mbshr/sl_member_new.pbl"
                     RowsPerPage="17" ClientScriptable="True" ClientValidation="False" AutoRestoreDataCache="False">
                     <PageNavigationBarSettings Position="Top" Visible="True" NavigatorType="Numeric">
                     <BarStyle HorizontalAlign="Center" />
                     <NumericNavigator FirstLastVisible="True" />
                     </PageNavigationBarSettings>
                 </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
