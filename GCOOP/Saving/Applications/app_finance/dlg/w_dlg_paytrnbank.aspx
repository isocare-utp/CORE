<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_paytrnbank.aspx.cs"
    Inherits="Saving.Applications.app_finance.dlg.w_dlg_paytrnbank" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>รายการเลขที่ธนาณัติ</title>

    <script type="text/javascript">

        function DwMainOnClick(sender, row, collumn) {
            var paymentdocno = objDwMain.GetItem(row, "paymentdoc_no") + "";
            opener.GetPaymentDocno(paymentdocno);
            window.close();
        }
    
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_fn_paytrnbank_docno"
            AutoRestoreContext="false" LibraryList="~/DataWindow/app_finance/paytrnbank.pbl"
            AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" ClientEventClicked="DwMainOnClick" AutoRestoreDataCache="True">
        </dw:WebDataWindowControl>
    </div>
    </form>
</body>
</html>
