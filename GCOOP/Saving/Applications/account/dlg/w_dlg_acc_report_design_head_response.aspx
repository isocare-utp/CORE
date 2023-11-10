<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_acc_report_design_head_response.aspx.cs"
    Inherits="Saving.Applications.account.dlg.w_dlg_acc_report_design_head_response" %>

<%@ Register assembly="WebDataWindow" namespace="Sybase.DataWindow.Web" tagprefix="dw" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>งบการเงิน</title>

<script type ="text/javascript" >
    
      function OnClick(sender, rowNumber, objectName) {
        try{
            var moneysheet_code = objDw_list.GetItem(rowNumber, "moneysheet_code");
            var moneysheet_name = objDw_list.GetItem(rowNumber, "moneysheet_name");
            var isConfirm = confirm("ต้องการเลือก  " + Gcoop.Trim(moneysheet_code) + "  : "  + Gcoop.Trim(moneysheet_name) + "  ใช่หรือไม่");
        
            if(isConfirm)
            {
                window.opener.GetValueDlg(moneysheet_code); 
                //window.parent.GetValueDlg(moneysheet_code);  
                window.close();
                //parent.RemoveIFrame();
            }
            
            }catch(ex) {
                alert("Error for get value ");
            }
        }
        
</script>

</head>



<body>
    <form id="form1" runat="server">
    <div style="font-family: Tahoma; font-size: small; margin-bottom: 0px">
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <br />
        <table style="width: 100%;">
            <tr>
                <td style="font-weight: 700; font-size: medium" valign="top">
                    กรุณาเลือกรหัสงบการเงิน
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" BorderStyle="Ridge"
                        Height="400px" Width="500px" >
                    <dw:WebDataWindowControl ID="Dw_list" runat="server" AutoRestoreContext="False" 
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" 
                        ClientScriptable="True" DataWindowObject="d_acc_report_desing_head_response" 
                        LibraryList="~/DataWindow/account/acc_report_design.pbl" 
                        ClientEventClicked="OnClick">
                    </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
