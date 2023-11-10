<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_sl_collmaster_search_mb_.aspx.cs"
    Inherits="Saving.Applications.keeping.dlg.w_dlg_sl_collmaster_search_mb_" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ค้นหาทะเบียนหลักทรัพย์</title>
    <%=collmastersSearch%>

    <script type="text/javascript">
        function selectRow(sender, rowNumber, objectName) {
            var memco_no = objdw_list.GetItem(rowNumber, "memco_no");
            var mem_name = objdw_list.GetItem(rowNumber, "mem_name");

            window.opener.GetValueFromDlgMb( memco_no, mem_name);
                window.close();
            }
            function OnDwDataLnContSrc(sender, rowNumber, objectName) {
                collmastersSearch();
                dw_master.AcceptText();
            }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    รายการค้นหา
    <table style="width: 530px;">
        <tr>
            <td>
                <dw:WebDataWindowControl ID="dw_master" runat="server" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                    DataWindowObject="d_sl_collmaster_criteria" LibraryList="~/DataWindow/shrlon/sl_collmaster_search.pbl"
                    ClientEventButtonClicked="OnDwDataLnContSrc">
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
    รายละเอียด
    <asp:HiddenField ID="hidden_search" runat="server" />
    <dw:WebDataWindowControl ID="dw_list" runat="server" ClientEventClicked="selectRow"
        DataWindowObject="d_sl_collmaster_list" LibraryList="~/DataWindow/shrlon/sl_collmaster_search.pbl"
        RowsPerPage="14" ClientScriptable="True">
        <PageNavigationBarSettings Position="Top" Visible="True" NavigatorType="Numeric">
            <BarStyle HorizontalAlign="Center" />
            <NumericNavigator FirstLastVisible="True" />
        </PageNavigationBarSettings>
    </dw:WebDataWindowControl>
    </form>
</body>
</html>
