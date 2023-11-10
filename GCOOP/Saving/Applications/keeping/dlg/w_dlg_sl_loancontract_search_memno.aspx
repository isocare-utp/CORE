<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_sl_loancontract_search_memno.aspx.cs"
    Inherits="Saving.Applications.keeping.dlg.w_dlg_sl_loancontract_search_memno" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ค้นหาเลขสัญญา</title>
    <%=LoanContractSearch %>

    <script type="text/javascript">
        function selectRow(sender, rowNumber, objectName) {
            var loancontractno = objdw_detail.GetItem(rowNumber, "loancontract_no");
            window.opener.GetContFromDlg(loancontractno);
            window.close();
        }
        function OnDwDataLnContSrc(sender, rowNumber, objectName) {
            LoanContractSearch();
            objdw_data.AcceptText();
        }
        
    </script>

</head>
<body>
    <form id="form1" runat="server">
    รายการค้นหา
    <dw:WebDataWindowControl ID="dw_data" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
        AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_sl_lncontsrch_criteria"
        LibraryList="~/DataWindow/keeping/sl_loancontract_search.pbl" ClientEventButtonClicked="OnDwDataLnContSrc">
    </dw:WebDataWindowControl>
    รายละเอียด
    <asp:HiddenField ID="hidden_search" runat="server" />
    <dw:WebDataWindowControl ID="dw_detail" runat="server" DataWindowObject="d_sl_lncontsrch_list"
        LibraryList="~/DataWindow/keeping/sl_loancontract_search.pbl" RowsPerPage="14"
        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
        ClientScriptable="True" ClientEventClicked="selectRow">
        <PageNavigationBarSettings Position="Top" Visible="True" NavigatorType="Numeric">
            <BarStyle HorizontalAlign="Center" />
            <NumericNavigator FirstLastVisible="True" />
        </PageNavigationBarSettings>
    </dw:WebDataWindowControl>
    </form>
</body>
</html>
