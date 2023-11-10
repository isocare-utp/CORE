<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_mb_search_bank.aspx.cs" Inherits="Saving.Applications.mbshr.dlg.w_dlg_mb_search_bank" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>ค้นหาธนาคาร</title>
      <script type="text/javascript">
          function selectRow(sender, rowNumber, objectName) {
            
                  var Bank_code = sender.GetItem(rowNumber,"bank_code");
                  var Bank_desc = sender.GetItem(rowNumber,"bank_desc");
                  //alert(Bank_code + Bank_desc);
                  window.opener.GetValueFromDlgSeachBank(Bank_code, Bank_desc);
                  window.close();

              
          }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>        
     รายการค้นหา
    <table style="width: 450px;">
        <tr>
            <td>
                 <dw:WebDataWindowControl ID="DwBank" runat="server" ClientEventClicked="selectRow"
                     DataWindowObject="d_ucfbank_searchdesc" LibraryList="~/DataWindow/mbshr/sl_member_new.pbl"
                     RowsPerPage="17" ClientScriptable="True" ClientValidation="False" AutoRestoreDataCache="False">
                     <PageNavigationBarSettings Position="Top" Visible="True" NavigatorType="Numeric">
                     <BarStyle HorizontalAlign="Center" />
                     <NumericNavigator FirstLastVisible="True" />
                     </PageNavigationBarSettings>
                 </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
