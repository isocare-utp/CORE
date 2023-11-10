<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_bankstatement.aspx.cs"
    Inherits="Saving.Applications.app_finance.dlg.w_dlg_bankstatement" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ระบบงานการเงิน</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dw:WebDataWindowControl ID="d_banklist_statement" runat="server" DataWindowObject="d_banklist_statement"
            LibraryList="~/DataWindow/app_finance.pbl" ClientScriptable="True">
        </dw:WebDataWindowControl>
    </div>
    </form>
</body>
</html>
