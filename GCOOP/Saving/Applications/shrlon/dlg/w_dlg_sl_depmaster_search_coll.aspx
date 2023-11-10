﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_sl_depmaster_search_coll.aspx.cs"
    Inherits="Saving.Applications.shrlon.dlg.w_dlg_sl_depmaster_search_coll" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ค้นหาหลักประกัน</title>
    <%=DeptMasterSearch%>
    <script type="text/javascript">
        function selectRow(sender, rowNumber, objectName) {
            var collmast_refno = objdw_detail.GetItem(rowNumber, "deptaccount_no");
            try {
                window.opener.GetContFromDepmasterCollDlg(collmast_refno);
                window.close();
            }
            catch (err) {
                parent.GetContFromDepmasterCollDlg(collmast_refno);
                parent.RemoveIFrame();
            }

        }
        function OnDwDataLnContSrc(sender, rowNumber, objectName) {
            DeptMasterSearch();
            objdw_data.AcceptText();
        }
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    รายการค้นหา
    <dw:WebDataWindowControl ID="dw_data" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
        AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_sl_deptmaster_search_criteria"
        LibraryList="~/DataWindow/shrlon/sl_deptmaster_search.pbl" ClientEventButtonClicked="OnDwDataLnContSrc">
    </dw:WebDataWindowControl>
    รายละเอียด
    <asp:HiddenField ID="hidden_search" runat="server" />
    <dw:WebDataWindowControl ID="dw_detail" runat="server" ClientEventClicked="selectRow"
        DataWindowObject="d_sl_deptmaster_search_list" LibraryList="~/DataWindow/shrlon/sl_deptmaster_search.pbl"
        RowsPerPage="14" ClientScriptable="True">
        <PageNavigationBarSettings Position="Top" Visible="True" NavigatorType="Numeric">
            <BarStyle HorizontalAlign="Center" />
            <NumericNavigator FirstLastVisible="True" />
        </PageNavigationBarSettings>
    </dw:WebDataWindowControl>
    </form>
</body>
</html>
