<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_kp_mastreceive_search.aspx.cs"
    Inherits="Saving.Applications.keeping.dlg.w_dlg_kp_mastreceive_search" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript">
        function selectRow(s, r, c) {
            var recv_period = objDw_detail.GetItem(r, "recv_period");
            var kpslip_no = objDw_detail.GetItem(r, "kpslip_no");
            var receipt_date = objDw_detail.GetItem(r, "receipt_date");
            try {
                window.opener.GetValueKpSlipno(recv_period, kpslip_no, receipt_date);
                window.close();
            }
            catch (err) {
                parent.GetValueKpSlipno(recv_period, kpslip_no, receipt_date);
                parent.RemoveIFrame();
            }
        }
     
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dw:WebDataWindowControl ID="Dw_detail" runat="server" DataWindowObject="d_kp_mastreceive_search"
            LibraryList="~/DataWindow/keeping/kp_adjust_monthly.pbl" ClientScriptable="True"
            AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
            ClientFormatting="True" ClientEventClicked="selectRow" Width="570px" Height="350px">
        </dw:WebDataWindowControl>
    </div>
    </form>
</body>
</html>
