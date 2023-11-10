<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_member_search.aspx.cs" Inherits="Saving.Applications.shrlon.dlg.w_dlg_member_search" %>


<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ค้นหาสมาชิก</title>

    <script type="text/javascript">
        function selectRow(sender, rowNumber, objectName) {
            var memberno = objdw_detail.GetItem(rowNumber, "member_no");
            var prename_desc = objdw_detail.GetItem(rowNumber, "prename_desc");
            var memb_name = objdw_detail.GetItem(rowNumber, "memb_name");
            var memb_surname = objdw_detail.GetItem(rowNumber, "memb_surname");
            var card_person = objdw_detail.GetItem(rowNumber, "card_person");
            window.opener.GetMemDetFromDlg(memberno, prename_desc, memb_name, memb_surname, card_person);
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
                <dw:WebDataWindowControl ID="dw_data" runat="server" ClientScriptable="True" DataWindowObject="d_sl_membsrch_criteria"
                    LibraryList="~/DataWindow/shrlon/sl_member_search.pbl">
                </dw:WebDataWindowControl>
            </td>
            <td>
                <asp:Button ID="cb_find" runat="server" Text="ค้นหา" Height="80px" Width="55px" OnClick="cb_find_Click" />
            </td>
        </tr>
    </table>
    รายละเอียด
    <asp:HiddenField ID="hidden_search" runat="server" />
    <dw:WebDataWindowControl ID="dw_detail" runat="server" ClientEventClicked="selectRow"
        DataWindowObject="d_sl_membsrch_list_memno2" LibraryList="~/DataWindow/shrlon/sl_member_search.pbl"
        RowsPerPage="17" ClientScriptable="True">
        <PageNavigationBarSettings Position="Top" Visible="True" NavigatorType="Numeric">
            <BarStyle HorizontalAlign="Center" />
            <NumericNavigator FirstLastVisible="True" />
        </PageNavigationBarSettings>
    </dw:WebDataWindowControl>
    </form>
</body>
</html>

