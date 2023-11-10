<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_dp_slip_search_reqdeptran.aspx.cs"
    Inherits="Saving.Applications.ap_deposit.dlg.w_dlg_dp_slip_search_reqdeptran" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ค้นหาใบทำรายการเงินฝาก</title>
    <%=SlipSearch%>
    <%=postDwMain%>
    <script type="text/javascript">
        function selectRow(sender, rowNumber, objectName) {
            var deptSlipNo = objDwList.GetItem(rowNumber, "deptslip_no");
            var deptSlipNetAmt = objDwList.GetItem(rowNumber, "dpdeptslip_deptslip_netamt");
            window.opener.GetDeptSlipNoFromDlg(deptSlipNo, deptSlipNetAmt);
            window.close();
        }
        
        function OnDwMainButtonClicked(sender, rowNumber, buttonName){  
            if(buttonName == "cb_search"){
                SlipSearch();
            }
        }
        
        function OnDwMainItemChanged(sender, rowNumber, columnName, newValue){
            if(columnName == "slip_tdate"){
                objDwMain.SetItem(rowNumber, columnName, newValue);
                objDwMain.SetItem(rowNumber, "slip_date", Gcoop.ToEngDate(newValue));
                objDwMain.AcceptText();
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dw:WebDataWindowControl ID="DwMain" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
            AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_dp_depmaster_criteria"
            LibraryList="~/DataWindow/ap_deposit/dp_slip_search_tran.pbl" 
            ClientEventButtonClicked="OnDwMainButtonClicked" 
            ClientEventItemChanged="OnDwMainItemChanged" ClientFormatting="True">
        </dw:WebDataWindowControl>
        <asp:HiddenField ID="hidden_search" runat="server" />
        <dw:WebDataWindowControl ID="DwList" runat="server" DataWindowObject="d_dp_deptslip_list1"
            LibraryList="~/DataWindow/ap_deposit/dp_slip_search_tran.pbl" 
            RowsPerPage="14" AutoRestoreContext="False"
            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
            ClientEventClicked="selectRow">
            <PageNavigationBarSettings Position="Top" Visible="True" NavigatorType="Numeric">
                <BarStyle HorizontalAlign="Left" />
                <NumericNavigator FirstLastVisible="True" />
            </PageNavigationBarSettings>
        </dw:WebDataWindowControl>
    </div>
    </form>
</body>
</html>
