<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_dp_approve.aspx.cs"
    Inherits="Saving.Applications.ap_deposit.dlg.w_dlg_dp_approve" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>อนุมัติรายการ</title>
    <script type = "text/javascript">
        function OnDwHeadButtonClicked(sender, rowNumber, buttonName ){
            var appr = 0;
            if(buttonName == "cb_ok"){
                appr = 1;
//                window.opener.GetValueFromDlg(appr);
//                window.close();
                parent.GetValueFromDlg(appr);
                alert("อนุมัติเรียบร้อยแล้ว");
                return;
            }
            else if(buttonName == "cb_cancel"){
                appr = 9;
//                window.opener.GetValueFromDlg(appr);
//                window.close();
                parent.GetValueFromDlg(appr);
                alert("ไม่อนุมัติ");
                return;
            }
            else if(buttonName == "cb_close"){
//                window.close();
                parent.RemoveIFrame();
                return;
            }
        }
    </script>
</head>
    
<body>
    <form id="form1" runat="server">
    <div>
        <dw:WebDataWindowControl ID="DwMain" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
            AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" 
            LibraryList="~/DataWindow/ap_deposit/dp_apvpopup_list_tks.pbl" 
            DataWindowObject="d_apv_approve_msg_tks" 
            ClientEventButtonClicked="OnDwHeadButtonClicked">
        </dw:WebDataWindowControl>
    </div>
    </form>
</body>
</html>
