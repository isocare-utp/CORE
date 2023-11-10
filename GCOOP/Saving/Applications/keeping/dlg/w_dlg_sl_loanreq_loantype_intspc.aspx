<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_sl_loanreq_loantype_intspc.aspx.cs"
    Inherits="Saving.Applications.keeping.dlg.w_dlg_sl_loanreq_loantype_intspc" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dw:WebDataWindowControl ID="dw_intspc" runat="server" AutoRestoreContext="false"
            AutoRestoreDataCache="true" AutoSaveDataCacheAfterRetrieve="true" ClientScriptable="false"
            DataWindowObject="d_sl_loanrequest_intratespc" LibraryList="~/DataWindow/shrlon/sl_loan_requestment.pbl">
        </dw:WebDataWindowControl>
    </div>
    </form>
</body>
</html>
