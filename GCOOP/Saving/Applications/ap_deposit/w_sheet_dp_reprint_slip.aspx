<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_dp_reprint_slip.aspx.cs"
    Inherits="Saving.Applications.ap_deposit.w_sheet_dp_reprint_slip" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postAccount%>
    <%=PrintSlip%>

    <script type="text/javascript">
    
    function OnDwDataButtonClicked(sender, rowNumber, buttonName){
        if(buttonName == "cb_search"){
            postAccount();
        }
    }
    
    function OnDwDetailClicked(sender, rowNumber, objectName){
        var deptSlip = objDwDetail.GetItem(rowNumber, "deptslip_no");
        Gcoop.GetEl("HdDeptSlip").value = deptSlip;
        if (confirm("ต้องการพิมพ์สลิปเลขที่ " + deptSlip + " ใช่หรือไม่?")){
            PrintSlip();
        }
    }
    
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <dw:WebDataWindowControl ID="DwData" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
        AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_dp_depmaster_criteria1"
        LibraryList="~/DataWindow/ap_deposit/dp_reprint_slip.pbl" ClientEventButtonClicked="OnDwDataButtonClicked">
    </dw:WebDataWindowControl>
    <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Width="780px">
        <dw:WebDataWindowControl ID="DwDetail" runat="server" AutoRestoreContext="False"
            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
            DataWindowObject="d_dp_deptslip_reprint" LibraryList="~/DataWindow/ap_deposit/dp_reprint_slip.pbl"
            Width="780px" RowsPerPage="13" ClientEventClicked="OnDwDetailClicked">
            <PageNavigationBarSettings NavigatorType="NumericWithQuickGo" Visible="True">
            </PageNavigationBarSettings>
        </dw:WebDataWindowControl>
    </asp:Panel>
    <asp:HiddenField ID="HdDeptSlip" runat="server" />
</asp:Content>
