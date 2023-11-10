<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_sl_checkpermiss_reprint.aspx.cs" Inherits="Saving.Applications.shrlon.ws_sl_checkpermiss_reprint_ctrl.ws_sl_checkpermiss_reprint" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        var dsMain = new DataSourceTool;
        var dsList = new DataSourceTool;

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "member_no") {
                PostMemberNo();
            }
        }
        function OnDsMainClicked(s, r, c) {
            if (c == "b_1") {
                Reprint();
            }
        }
        function OnDsListItemChanged(s, r, c, v) {
            if (c == "print_flag") {
                Gcoop.GetEl("hdrow").value = r;
                PostPrintFlag();
            }
        }
        function OnDsListClicked(s, r, c) {
            
        }

        function SheetLoadComplete() {

        }      
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <uc1:DsMain ID="dsMain" runat="server" />
    <uc2:DsList ID="dsList" runat="server" />
    <asp:HiddenField ID="hdrow" runat="server" Value="false" />
</asp:Content>
