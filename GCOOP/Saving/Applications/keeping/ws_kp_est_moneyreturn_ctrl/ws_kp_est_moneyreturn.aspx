<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_kp_est_moneyreturn.aspx.cs" Inherits="Saving.Applications.keeping.ws_kp_est_moneyreturn_ctrl.ws_kp_est_moneyreturn" %>
<%@ Register src="DsMain.ascx" tagname="DsMain" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool();

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }

        function OnDsMainClicked(s, r, c, v) {
            if (c == "b_post") {
                PostSetMoneyreturn();
            }
        }

        function SheetLoadComplete() {

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <uc1:DsMain ID="dsMain" runat="server" />
</asp:Content>
