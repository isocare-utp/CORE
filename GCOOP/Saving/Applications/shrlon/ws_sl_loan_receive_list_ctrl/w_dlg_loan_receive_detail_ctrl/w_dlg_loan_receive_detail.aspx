<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true"
    CodeBehind="w_dlg_loan_receive_detail.aspx.cs" Inherits="Saving.Applications.shrlon.ws_sl_loan_receive_list_ctrl.w_dlg_loan_receive_detail_ctrl.w_dlg_loan_receive_detail" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;

        function Validate() {
        }
        function OnDsMainItemChanged(s, r, c, v) {

        }
        function OnDsMainClicked(s, r, c) {



        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
<br />
    <div align="center">
        <uc1:DsMain ID="dsMain" runat="server" />
    </div>
</asp:Content>
