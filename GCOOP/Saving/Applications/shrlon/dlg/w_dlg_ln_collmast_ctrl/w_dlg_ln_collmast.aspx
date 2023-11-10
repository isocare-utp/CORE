<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true"
    CodeBehind="w_dlg_ln_collmast.aspx.cs" Inherits="Saving.Applications.shrlon.dlg.w_dlg_ln_collmast_ctrl.w_dlg_ln_collmast" %>

<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsList = new DataSourceTool();

        function OnDsListClicked(s, r, c) {
            var collmast_no = dsList.GetItem(r, "collmast_no");
            try {
                window.opener.GetCollmastNoFromDlg(collmast_no);
                window.close();
            } catch (err) {
                parent.GetCollmastNoFromDlg(collmast_no);
                parent.RemoveIFrame();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsList ID="dsList" runat="server" />
</asp:Content>
