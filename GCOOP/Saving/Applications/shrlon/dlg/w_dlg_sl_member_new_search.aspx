<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_sl_member_new_search.aspx.cs" Inherits="Saving.Applications.shrlon.dlg.w_dlg_sl_member_new_search" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">
    <title>ค้นหาคำร้องสมัครเป็นสมาชิก</title>
    <script type="text/javascript">
        function selectRow(sender, rowNumber, objectName) {
                var docno = objdw_detail.GetItem(rowNumber, "appl_docno");
                window.opener.GetValueFromDlg(docno);
                window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    รายการค้นหา
    <table style="width: 530px;">
        <tr>
            <td>
                <dw:WebDataWindowControl ID="dw_data" runat="server" ClientScriptable="True" 
                    DataWindowObject="d_sl_member_new_search_criteria" 
                    LibraryList="~/DataWindow/Shrlon/sl_member_new_search.pbl">
                </dw:WebDataWindowControl>
            </td>
            <td>
                <asp:Button ID="pb_search" runat="server" Text="ค้นหา" Height="80px" 
                    Width="55px" onclick="pb_search_Click"/>
            </td>
        </tr>
    </table>
    รายละเอียด
    <asp:HiddenField ID="hidden_search" runat="server" />
    <dw:WebDataWindowControl ID="dw_detail" runat="server" ClientEventClicked="selectRow"
        DataWindowObject="d_sl_member_new_list" LibraryList="~/DataWindow/Shrlon/sl_member_new_search.pbl"
        RowsPerPage="14" ClientScriptable="True">
        <PageNavigationBarSettings Position="Top" Visible="True" NavigatorType="Numeric">
            <BarStyle HorizontalAlign="Center" />
            <NumericNavigator FirstLastVisible="True" />
        </PageNavigationBarSettings>
    </dw:WebDataWindowControl>
    </form>
</body>
</html>
