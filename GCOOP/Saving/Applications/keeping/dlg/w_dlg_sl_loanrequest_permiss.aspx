<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_sl_loanrequest_permiss.aspx.cs"
    Inherits="Saving.Applications.keeping.dlg.w_dlg_sl_loanrequest_permiss" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dw:WebDataWindowControl ID="dw_lrq_permiss" runat="server" DataWindowObject="d_sl_loanrequest_permiss"
            LibraryList="~/DataWindow/shrlon/sl_loan_requestment.pbl" AutoRestoreContext="False"
            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True">
        </dw:WebDataWindowControl>
    </div>
    </form>
</body>
</html>
