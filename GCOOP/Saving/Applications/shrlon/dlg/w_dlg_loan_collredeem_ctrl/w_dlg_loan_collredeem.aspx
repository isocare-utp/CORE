<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true"
    CodeBehind="w_dlg_loan_collredeem.aspx.cs" Inherits="Saving.Applications.shrlon.dlg.w_dlg_loan_collredeem" %>

<%@ Register Src="w_dlg_loan_collredeem_ctrl/DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="w_dlg_loan_collredeem_ctrl/DsDetail.ascx" TagName="DsDetail" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        var dsMain = new DataSourceTool;
        var dsDetail = new DataSourceTool;

        function OnDsMainItemChanged(s, r, c) {
            if (c == "member_no") {
                RetrieveMemberName();

            }
        }
        
        function OnDsMainClicked(s, r, c) {
            if (c == "b_search") {
                PostSearch();
            }
        }

        function OnDsDetailtemChanged(s, r, c) {
        }

        function OnDsDetailClicked(s, r, c) {
            parent.GetCollmastNoIFrame(dsDetail.GetItem(r, "collmast_no"));
        }

        function Validate() {
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <uc1:DsMain ID="dsMain" runat="server" />
    <br />
    <uc2:DsDetail ID="dsDetail" runat="server" />
</asp:Content>
