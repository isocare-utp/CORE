<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true"  CodeBehind="ws_dlg_fin_extmember_search.aspx.cs" Inherits="Saving.Applications.app_finance.dlg.ws_dlg_fin_extmember_search_ctrl.ws_dlg_fin_extmember_search" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsList = new DataSourceTool();
        function OnDsListClicked(s, r, c) {
            var contack_no = dsList.GetItem(r, "contack_no");
            try {
                window.opener.GetContackFromDlg(contack_no);
                window.close();
            } catch (err) {
                parent.GetContackFromDlg(contack_no);
                parent.RemoveIFrame();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <uc1:DsList ID="dsList" runat="server" />
</asp:Content>