<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_acc_ucf_edit_member.aspx.cs" Inherits="Saving.Applications.account.w_acc_ucf_edit_member" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=jsPostGetlist%>
    <%=jsProcessMember%>
    <script type="text/javascript">
        function Validate() {
            var isconfirm = confirm("ยืนยันการบันทึกข้อมูล?");

            if (!isconfirm) {
                return false;
            }
            var alert = "";


            if (alert != "") {
                confirm(alert);
                return false;
            }
            else {
                return true;
            }
        }

        function OnDwmainClicked(sender, row, bName) {
            if (bName == "b_get") {
                var year = objDwmain.GetItem(1, "year");
                var period = objDwmain.GetItem(1, "period");
                if (year == "" || year == null || period == "" || period == null) {
                    alert("กรุณาระบุปีบัญชีและงวดบัญชีให้ครบถ้วน");
                }
                else {
                    jsPostGetlist();
                }
            }

            if (bName == "b_process") {
                jsProcessMember();

            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <dw:WebDataWindowControl ID="Dwmain" runat="server" DataWindowObject="d_acc_year_period_process"
        LibraryList="~/DataWindow/account/cm_constant_config.pbl" AutoRestoreContext="False"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientEventButtonClicked="OnDwmainClicked"
        ClientScriptable="True" ClientEventItemChanged="OnDwmainChange" ClientFormatting="True"
        TabIndex="1">
    </dw:WebDataWindowControl>
    <dw:WebDataWindowControl ID="Dwlist" runat="server" DataWindowObject="d_acc_report_editmember"
        LibraryList="~/DataWindow/account/cm_constant_config.pbl" AutoRestoreContext="False"
        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
        ClientFormatting="True" TabIndex="100" Height="400px" Width="700px">
    </dw:WebDataWindowControl>
</asp:Content>
