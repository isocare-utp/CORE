<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_sl_apvloan_docno.aspx.cs" Inherits="Saving.Applications.shrlon.dlg.w_dlg_sl_apvloan_docno" %>

<%@ Register assembly="WebDataWindow" namespace="Sybase.DataWindow.Web" tagprefix="dw" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <asp:Panel ID="Panel1" runat="server" Height="400px" ScrollBars="Auto">
            <dw:WebDataWindowControl ID="Dw_main" runat="server" AutoRestoreContext="False" 
            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" 
            ClientFormatting="True" ClientScriptable="True" 
            DataWindowObject="d_sl_apvloan_docno" 
            LibraryList="~/DataWindow/shrlon/sl_approve_loan.pbl" 
            style="top: 0px; left: -1px">
            </dw:WebDataWindowControl>
        </asp:Panel>
        <br />
        <table  border = 0 >
        <tr>
        <td>
        <asp:Button ID="B_save" runat="server" onclick="B_save_Click" 
            Text="บันทึกข้อมูล" UseSubmitBehavior="False" />
        </td>
        <td>
         <asp:Button ID="B_getdocno" runat="server" onclick="B_getdocno_Click" 
            Text="ดึงเลขล่าสุด" UseSubmitBehavior="False" 
                onclientclick="B_getdocno_Click" />
        </td>
        </tr>
        </table>
        
              
    
    </div>
    </form>
</body>
</html>
