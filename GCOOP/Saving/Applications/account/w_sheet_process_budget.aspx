<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_process_budget.aspx.cs" Inherits="Saving.Applications.account.w_sheet_process_budget" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=jsPostProcess%>
    <script type="text/javascript">
        function OnDwmainButtonClick(sender, row, bName) {
            var year = objDwmain.GetItem(1, "year");
            var period = objDwmain.GetItem(1, "period");
            var alert = "";

            if (year == "" || year == null) {
                alert += "กรุณาระบุปีบัญชี\n";
            }
            if (period == "" || period == null) {
                alert += "กรุณาระบุงวด";
            }

            if (alert != "") {
                confirm(alert);
            }
            else {
                jsPostProcess();
            }
        }

        function Validate() {
            try {
                var isconfirm = confirm("ยืนยันการบันทึกการข้อมูล?");

                if (!isconfirm) {
                    return false;
                }
                var alertstr = "";
                if (alertstr == "") {
                    return true;
                }
                else {
                    alert(alertstr);
                    return false;
                }
            }

            catch (err) {
                alert(err);
                return false;
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <dw:WebDataWindowControl ID="Dwmain" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
        AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_acc_year_period"
        LibraryList="~/DataWindow/account/sumbudget_on_tks.pbl" ClientFormatting="True"
        ClientEventItemChanged="OnDwmainItemChanged" ClientEventButtonClicked="OnDwmainButtonClick">
    </dw:WebDataWindowControl>
    <asp:Panel ID="Panel1" runat="server" BorderStyle="Ridge" Height="420px" Width="750px">
        <dw:WebDataWindowControl ID="DwDetail" runat="server" AutoRestoreContext="False"
            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
            DataWindowObject="d_acc_sumbudget_on_tks" LibraryList="~/DataWindow/account/sumbudget_on_tks.pbl"
            ClientFormatting="True" ClientEventItemChanged="OnDwListItemChanged" Height="420px"
            Width="749px" ClientEventButtonClicked="OnDwListButtonClick">
        </dw:WebDataWindowControl>
    </asp:Panel>
</asp:Content>
