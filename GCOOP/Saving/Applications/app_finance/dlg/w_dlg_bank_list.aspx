<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_bank_list.aspx.cs"
    Inherits="Saving.Applications.app_finance.dlg.w_dlg_bank_list" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript">

        function DwMainOnClick(sender, rowNumber, objectName) {
            var accid = objDwBankList.GetItem(rowNumber, "account_no");
            var accname = objDwBankList.GetItem(rowNumber, "account_name");
            parent.GetAccid(accid, accname);
            parent.close();
        }
    
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dw:WebDataWindowControl ID="DwBankList" runat="server" DataWindowObject="d_banklist_bybankbranch"
            LibraryList="~/DataWindow/App_finance/paychq.pbl" AutoRestoreContext="False"
            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
            ClientFormatting="True" ClientEventClicked="DwMainOnClick">
        </dw:WebDataWindowControl>
    </div>
    </form>
</body>
</html>
