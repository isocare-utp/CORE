<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true"
    CodeBehind="w_dlg_dp_current_account_no.aspx.cs" Inherits="Saving.Applications._global.w_dlg_dp_current_account_no_ctrl.w_dlg_dp_current_account_no" %>

<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <div align="left" style="margin-left: 18px; margin-top: 10px;">
        <span class="TitleSpan">เลขที่บัญชีล่าสุด</span>
        <uc1:DsList ID="dsList" runat="server" />
    </div>
</asp:Content>
