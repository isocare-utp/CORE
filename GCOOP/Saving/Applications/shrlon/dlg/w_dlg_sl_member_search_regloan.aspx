<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_sl_member_search_regloan.aspx.cs" Inherits="Saving.Applications.shrlon.dlg.w_dlg_sl_member_search_regloan" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ค้นหาสมาชิก</title>
    <script type="text/javascript">
       
        function selectRow(sender, rowNumber, objectName) {
            if (objectName != "datawindow") {
                var memberno_coll = objdw_detail.GetItem(rowNumber, "member_no");
                var prename_desc = objdw_detail.GetItem(rowNumber, "prename_desc");
                var memb_name = objdw_detail.GetItem(rowNumber, "memb_name");
                var memb_surname = objdw_detail.GetItem(rowNumber, "memb_surname");
                var membname_coll = prename_desc + memb_name + memb_surname;

                try {
                   
                    window.opener.GetValueMbColl(memberno_coll, membname_coll);
                    window.close();
                } catch (err) {
                    parent.GetValueMbColl(memberno_coll, membname_coll);
                    parent.RemoveIFrame();
                }
            }
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
                    LibraryList="~/DataWindow/shrlon/kp_recieve_return.pbl">
                </dw:WebDataWindowControl>
            </td>
            <td>
                <asp:Button ID="cb_find" runat="server" Text="ค้นหา" Height="80px" Width="55px" OnClick="cb_find_Click"  style="width:55px; height:80px;" />
            </td>
        </tr>
    </table>
    รายละเอียด
    <asp:HiddenField ID="hidden_search" runat="server" />
    <dw:WebDataWindowControl ID="dw_detail" runat="server" ClientEventClicked="selectRow"
        DataWindowObject="d_sl_membsrch_list_memno" LibraryList="~/DataWindow/shrlon/kp_recieve_return.pbl"
        RowsPerPage="15" ClientScriptable="True" Width="620px" Height="500px">
        <PageNavigationBarSettings Position="Top" Visible="True" NavigatorType="Numeric">
            <BarStyle HorizontalAlign="Center" />
            <NumericNavigator FirstLastVisible="True" />
        </PageNavigationBarSettings>
    </dw:WebDataWindowControl>
    </form>
</body>
</html>
