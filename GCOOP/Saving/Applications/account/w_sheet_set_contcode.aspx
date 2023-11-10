<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_set_contcode.aspx.cs" Inherits="Saving.Applications.account.w_sheet_set_contcode" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=jsPostGetlist%>
    <script type="text/javascript">
        function Validate() {
            var isconfirm = confirm("ยืนยันการบันทึกข้อมูล?");

            if (!isconfirm) {
                return false;
            }
            var cnt_short_amt = "";
            var cnt_long_amt = "";
            var alert = "";
//            var rowcount = objDwlist.RowCount();

//            for (var i = 1; i <= rowcount; i++) {
//                cnt_short_amt = objDwlist.GetItem(i, "cnt_short_amt");
//                cnt_long_amt = objDwlist.GetItem(i, "cnt_long_amt");
//                if (cnt_short_amt == "" || cnt_short_amt == null || cnt_long_amt == "" || cnt_long_amt == null) {
//                    if (alert == "") {
//                        alert = "กรุณาระบุข้อมูลให้ครบถ้วน แถวที่ ";
//                    }
//                    alert += i + ":" + cnt_short_amt + ",";
//                }
//            }

            if (alert != "") {
                confirm(alert);
                return false;
            }
            else {
                return true;
            }
        }

        function OnDwmainClicked(sender, row, bName) {
            var year = objDwmain.GetItem(1, "year");
            var period = objDwmain.GetItem(1, "period");
            if (year == "" || year == null || period == "" || period == null) {
                alert("กรุณาระบุปีบัญชีและงวดบัญชีให้ครบถ้วน");
            }
            else {
                jsPostGetlist();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <dw:WebDataWindowControl ID="Dwmain" runat="server" DataWindowObject="d_acc_year_period"
        LibraryList="~/DataWindow/account/cm_constant_config.pbl" AutoRestoreContext="False"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientEventButtonClicked="OnDwmainClicked"
        ClientScriptable="True" ClientEventItemChanged="OnDwmainChange" ClientFormatting="True"
        TabIndex="1">
    </dw:WebDataWindowControl>
    <dw:WebDataWindowControl ID="Dwlist" runat="server" DataWindowObject="d_acc_set_const_code"
        LibraryList="~/DataWindow/account/cm_constant_config.pbl" AutoRestoreContext="False"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
        ClientFormatting="True" TabIndex="100" Height="400px" Width="700px">
    </dw:WebDataWindowControl>
</asp:Content>
