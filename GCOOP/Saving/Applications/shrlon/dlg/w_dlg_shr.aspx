<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_shr.aspx.cs" Inherits="Saving.Applications.shrlon.dlg.w_dlg_shr" %>

<%@ Register assembly="WebDataWindow" namespace="Sybase.DataWindow.Web" tagprefix="dw" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<script type ="text/javascript" >
    function OnDwMainClick(s,r,c)
    {
        if(c=="sharetype_code" || c=="sharetype_desc" || c == "sharestk_amt" || c == "cp_sharestk") 
        {
            var sharetype_code = objDw_main.GetItem(r,"sharetype_code");
            var sharetype_desc = objDw_main.GetItem(r,"sharetype_desc");
            var sharestk_amt = objDw_main.GetItem(r,"sharestk_amt");
            window.opener.TypeSHR(sharetype_code,sharetype_desc);
            window.close();
        }
        return 0;
    }
</script> 
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <table style="width:100%;">
            <tr>
                <td colspan="3">
                    <asp:Panel ID="Panel1" runat="server" BorderStyle="Ridge" Height="500px" 
                        ScrollBars="Auto" Width="400px">
                        <dw:WebDataWindowControl ID="Dw_main" runat="server" AutoRestoreContext="False" 
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" 
                            ClientFormatting="True" ClientScriptable="True" 
                            DataWindowObject="d_divavgsrv_dlg_methodpayment_shr" 
                            LibraryList="~/DataWindow/shrlon/div_avg.pbl">
                        </dw:WebDataWindowControl>
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
