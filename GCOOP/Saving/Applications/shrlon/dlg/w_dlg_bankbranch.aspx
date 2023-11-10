<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_bankbranch.aspx.cs" Inherits="Saving.Applications.shrlon.dlg.w_dlg_bankbranch" %>

<%@ Register assembly="WebDataWindow" namespace="Sybase.DataWindow.Web" tagprefix="dw" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>

<script type ="text/javascript" >
    function OnDwMasterClick(s,r,c)
    {
        if(c == "branch_id" || c == "branch_name")
        {
            var BranchID = objDw_main.GetItem(r,"branch_id");
            var BranchName = objDw_main.GetItem(r,"branch_name");
            window.opener.SetBranch(BranchID);
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
                <td>
                    <asp:Panel ID="Panel1" runat="server">
                        <dw:WebDataWindowControl ID="Dw_main" runat="server" AutoRestoreContext="False" 
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" 
                            ClientFormatting="True" ClientScriptable="True" 
                            DataWindowObject="dddw_divavg_bankbranch" 
                            LibraryList="~/DataWindow/shrlon/div_avg.pbl" style="top: 0px; left: 0px" 
                            ClientEventClicked="OnDwMasterClick"></dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
