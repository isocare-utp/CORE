<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_lon.aspx.cs" Inherits="Saving.Applications.shrlon.dlg.w_dlg_lon" %>

<%@ Register assembly="WebDataWindow" namespace="Sybase.DataWindow.Web" tagprefix="dw" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<script type ="text/javascript" >
    function OnDwMainClick(s,r,c)
    {
        if(c=="loantype_code" || c=="loantype_desc" || c == "loancontract_no" || c == "principal_balance") 
        {
            var loancontract_no = objDw_main.GetItem(r,"loancontract_no");
            var loantype_desc = objDw_main.GetItem(r,"loantype_desc");
            window.opener.TypeLON(loancontract_no,loantype_desc);
            window.close();
        }
        return 0;
    }
</script> 
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <br />
        <table style="width:100%;">
            <tr>
                <td colspan="3">
                    <asp:Panel ID="Panel1" runat="server" BorderStyle="Ridge" Height="400px" 
                        ScrollBars="Auto" Width="450px">
                        <dw:WebDataWindowControl ID="Dw_main" runat="server" 
                            DataWindowObject="d_divavgsrv_dlg_methodpayment_lon" 
                            LibraryList="~/DataWindow/shrlon/div_avg.pbl" AutoRestoreContext="False" 
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" 
                            ClientEventClicked="OnDwMainClick" ClientFormatting="True" 
                            ClientScriptable="True"></dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
