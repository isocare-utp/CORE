<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_am_coopmaster.aspx.cs" Inherits="Saving.Applications.admin.w_sheet_am_coopmaster" %>

<%@ Register Src="w_sheet_am_coopmaster_ctrl/DsList.ascx" TagName="DsList" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function OnDsListItemChanged(s, r, c, v) {
            alert(v);
        }

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsList ID="dsList" runat="server" />
</asp:Content>
