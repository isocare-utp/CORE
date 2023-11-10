<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_dp_upbook.aspx.cs" 
Inherits="Saving.Applications.ap_deposit.ws_dp_upbook_ctrl.ws_dp_upbook" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool();

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "deptaccount_no") {
                PostDepAccount();
            }
        }
        function OnDsMainClicked(s, r, c) {
            if (c == "b_print") {
                var r = confirm("ต้องการอัพเดทสมุดเงินฝาก");
                if (r == true) {
                    PostUpBook();
                } else {
                    return false ;
                }
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
<asp:Literal ID="LtServerMessege" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
    <script language="javascript" type="text/javascript">
        var timeId;
        var active = false;

        function actionEvent() {
            if ($("input[id*='DropDownListAutoRefresh']").val() == "" || $("input[id*='DropDownListAutoRefresh']").val() == null) {

            }
        }

        function reloadPage() {
            var AccountNoAutoRefresh = $("input[id*='deptaccount_no']").val();
            if (AccountNoAutoRefresh == "" || AccountNoAutoRefresh == null) {
                time = 1000 * 600;
                if (active) actionEvent(); else active = true;
                timeID = setTimeout("reloadPage()", time);
            } else {
                clearTimeout(timeId);
            }
        }
        reloadPage();
    </script>
</asp:Content>
