<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_am_cmcoop.aspx.cs" Inherits="Saving.Applications.admin_const.ws_am_cmcoop_ctrl.ws_am_cmcoop" %>
<%@ Register src="DsMain.ascx" tagname="DsMain" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool();

            function Validate() {
                return confirm("ยืนยันการบันทึกข้อมูล");
            }
            function SheetLoadComplete() {
            }

            function OnDsMainItemChanged(s, r, c, v) {

            }
            function OnDsMainClicked(s, r, c) {

            }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <uc1:DsMain ID="dsMain" runat="server" />
</asp:Content>
