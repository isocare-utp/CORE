<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_dept_acclist.aspx.cs"
    Inherits="Saving.Applications.app_finance.dlg.w_dlg_dept_acclist" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript">

        function DwMainOnClick(sender, row, collumn) {
            var deptaccount_no = objDwMain.GetItem(row, "deptaccount_no");
            opener.GetDeptAcc(deptaccount_no);
            window.close();
        }
    
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_dept_account_list"
            AutoRestoreContext="false" LibraryList="~/DataWindow/app_finance/paytrnbank.pbl"
            AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" ClientEventClicked="DwMainOnClick">
        </dw:WebDataWindowControl>
    </div>
    </form>
</body>
</html>
