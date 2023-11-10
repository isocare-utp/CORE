<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_dp_deptstatement.aspx.cs"
    Inherits="Saving.Applications.ap_deposit.dlg.w_dlg_dp_deptstatement" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Statement</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <div align="center">
        <asp:Panel ID="Panel1" runat="server" Width="770px" ScrollBars="Horizontal">
            <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_dp_dept_statement"
                LibraryList="~/DataWindow/ap_deposit/dp_slip.pbl" RowsPerPage="20">
                <PageNavigationBarSettings NavigatorType="NumericWithQuickGo" Visible="True">
                </PageNavigationBarSettings>
            </dw:WebDataWindowControl>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
