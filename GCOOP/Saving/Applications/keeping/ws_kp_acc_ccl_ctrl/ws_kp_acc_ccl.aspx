<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_kp_acc_ccl.aspx.cs" Inherits="Saving.Applications.keeping.ws_kp_acc_ccl_ctrl.ws_kp_acc_ccl" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool();
        var dsList = new DataSourceTool();

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล?");
        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "member_no") {
                postMemberNo();
            }
        }

        function OnDsMainClicked(s, r, c) {

        }

        function OnDsListItemChanged(s, r, c, v) {

        }

        function OnDsListClicked(s, r, c) {

        }

        function SheetLoadComplete() {
            document.getElementById("ctl00_ContentPlace_dsMain_FormView1_member_no").focus();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
<asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
    <br />
    <uc2:DsList ID="dsList" runat="server" />
</asp:Content>
