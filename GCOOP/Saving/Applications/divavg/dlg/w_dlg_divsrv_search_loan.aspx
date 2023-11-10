<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_divsrv_search_loan.aspx.cs" Inherits="Saving.Applications.divavg.dlg.w_dlg_divsrv_search_loan" %>
<%@ Register assembly="WebDataWindow" namespace="Sybase.DataWindow.Web" tagprefix="dw" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <br />
        <dw:WebDataWindowControl ID="Dw_main" runat="server" AutoRestoreContext="False" 
            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" 
            ClientFormatting="True" ClientScriptable="True" 
            DataWindowObject="d_divsrv_search_loan" 
            LibraryList="~/DataWindow/divavg/divsrv_req_methpay.pbl">
        </dw:WebDataWindowControl>
        <br />
    
    </div>
    </form>
</body>
</html>

