<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_sl_extmember_search.aspx.cs"
    Inherits="Saving.Applications.app_finance.dlg.w_dlg_sl_extmember_search" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ค้นหาบุคคลภายนอก</title>

    <script type="text/javascript">

        function selectRow(sender, rowNumber, objectName) {
                var contack_no = objd_dp_extmember_search_memno_list.GetItem(rowNumber, "contack_no");
                parent.GetValueFromDlg(contack_no);
                parent.close();         
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 350px" >
       <%-- <dw:WebDataWindowControl ID="d_sl_extmembsrch_criteria" runat="server" DataWindowObject="d_sl_extmembsrch_criteria"
            LibraryList="~/DataWindow/App_finance/editcontack.pbl" ClientScriptable="True"
            AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True">
        </dw:WebDataWindowControl>--%>
        <td>
        <asp:Panel ID="Panel1" runat="server" BorderStyle="Ridge" Height="400px"  Width="500px" ScrollBars="Auto">
        <dw:WebDataWindowControl ID="d_dp_extmember_search_memno_list" runat="server" DataWindowObject="d_dp_extmember_search_memno_list"
            LibraryList="~/DataWindow/App_finance/editcontack.pbl" ClientScriptable="True"
            ClientEventClicked="selectRow" 
            AutoRestoreContext="False" AutoSaveDataCacheAfterRetrieve="True" AutoRestoreDataCache="True" ClientFormatting="True">
            <%--<PageNavigationBarSettings Position="bottom" Visible="True" NavigatorType="Numeric">
            <BarStyle HorizontalAlign="Center" />
            <NumericNavigator FirstLastVisible="True" />
        </PageNavigationBarSettings>--%>
        </dw:WebDataWindowControl>
        </asp:Panel>
        </td>

    </div>
    </form>
</body>
</html>
