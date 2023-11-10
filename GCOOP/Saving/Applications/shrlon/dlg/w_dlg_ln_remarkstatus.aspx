<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_ln_remarkstatus.aspx.cs" Inherits="Saving.Applications.shrlon.dlg.w_dlg_ln_remarkstatus" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>รายละเอียดสถานะอื่นๆ</title>
</head>
<body>
    <form id="form1" runat="server">
     
   
    <table style="width: 460px;">
        <tr>
            <td>
                 <dw:WebDataWindowControl ID="DwMain" runat="server" ClientEventClicked="selectRow"
                     DataWindowObject="d_sl_remarkstat" LibraryList="~/DataWindow/shrlon/sl_loan_requestment_cen.pbl"
                     RowsPerPage="17" ClientScriptable="True" ClientValidation="False" AutoRestoreDataCache="True">
                     <PageNavigationBarSettings Position="Top" Visible="True" NavigatorType="Numeric">
                     <BarStyle HorizontalAlign="Center" />
                     <NumericNavigator FirstLastVisible="True" />
                     </PageNavigationBarSettings>
                 </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
    
    
    </form>
</body>
</html>
