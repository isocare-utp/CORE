<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_bankaccount_search.aspx.cs"
    Inherits="Saving.Applications.app_finance.dlg.w_dlg_bankaccount_search" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <script type="text/javascript">

        function DwMainClick(sender, rowNumber, objectName) {
            var account_no = objDwMain.GetItem(rowNumber, "account_no");
            var account_name = objDwMain.GetItem(rowNumber, "account_name");
            var bank_code = objDwMain.GetItem(rowNumber, "bank_code");
            var bankbranch_code = objDwMain.GetItem(rowNumber, "bankbranch_code");
            parent.GetDlgBankAccount(account_no, account_name, bank_code, bankbranch_code);
            parent.close();
        }

    </script>

    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 350px">
        <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="dddw_bankname"
            LibraryList="~/DataWindow/app_finance/bankaccount.pbl" AutoRestoreContext="False"
            AutoRestoreDataCache="True" ClientFormatting="False" ClientScriptable="True"
            Width="500px" AutoSaveDataCacheAfterRetrieve="True" RowsPerPage="15" ClientEventClicked="DwMainClick">
        </dw:WebDataWindowControl>
    </div>
    </form>
</body>
</html>
