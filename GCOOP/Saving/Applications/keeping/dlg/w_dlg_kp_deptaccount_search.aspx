<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_kp_deptaccount_search.aspx.cs"
    Inherits="Saving.Applications.keeping.dlg.w_dlg_kp_deptaccount_search" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ค้นหาสมาชิก</title>
    <script type="text/javascript">
        function selectRow(sender, rowNumber, objectName) {
            if (objectName != "datawindow") {
                var deptaccount_no = objdw_detail.GetItem(rowNumber, "deptaccount_no");
                try {
                    window.opener.GetDeptNoFromDlg(deptaccount_no);
                    window.close();
                } catch (err) {
                    parent.GetDeptNoFromDlg(deptaccount_no);
                    parent.RemoveIFrame();
                }
            }
        }
        function ItemChangeDwData(sender, rowNumber, columnName, newValue) {
            if (columnName == "deptaccount_no") {
                var branch_id = Gcoop.Trim(newValue);
                objdw_data.SetItem(1, "deptaccount_no", deptaccount_no);
                Gcoop.GetEl("hdeptaccount_no").value = deptaccount_no;
                objdw_data.AcceptText();
                // LoanContractSearch();
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    รายละเอียด
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:HiddenField ID="hdeptaccount_no" runat="server" />
    <asp:HiddenField ID="hidden_search" runat="server" />
    <dw:WebDataWindowControl ID="dw_detail" runat="server" ClientEventClicked="selectRow"
        DataWindowObject="d_dlg_kp_deptaccount_list" LibraryList="~/DataWindow/keeping/kp_dlg_debtaccount_search.pbl"
        RowsPerPage="17" ClientScriptable="True">
        <PageNavigationBarSettings Position="Top" Visible="True" NavigatorType="Numeric">
            <BarStyle HorizontalAlign="Center" />
            <NumericNavigator FirstLastVisible="True" />
        </PageNavigationBarSettings>
    </dw:WebDataWindowControl>
    </form>
</body>
</html>
