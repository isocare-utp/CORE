<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true"
    CodeBehind="w_dlg_finconfrim.aspx.cs" Inherits="Saving.Applications.app_finance.dlg.w_dlg_finconfrim_ctrl.w_dlg_finconfrim" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
    <script type="text/javascript">
        var dsMain = new DataSourceTool();
        var dsList = new DataSourceTool();
        function OnDsMainClicked(s, r, c) {
            if (c == "b_search") {
                PostSearch();
            }
        }
        function OnDsListClicked(s, r, c) {
            var slipno = dsList.GetItem(r, "slip_no");
            var coopid = dsList.GetItem(r, "coop_id");
            try {
                window.opener.GetSlipNoFromDlg(slipno, coopid);
                window.close();
            } catch (err) {
                parent.GetSlipNoFromDlg(slipno, coopid);
                parent.RemoveIFrame();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" /><br />
    <uc2:DsList ID="dsList" runat="server" />
    <asp:Label ID="LbCount" runat="server" ForeColor="Green" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px"></asp:Label>
</asp:Content>
