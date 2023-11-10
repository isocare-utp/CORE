<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true"
    CodeBehind="w_dlg_mg_mrtgmast.aspx.cs" Inherits="Saving.Applications.shrlon.dlg.w_dlg_mg_mrtgmast_ctrl.w_dlg_mg_mrtgmast" %>

<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsList = new DataSourceTool();

        function OnDsListClicked(s, r, c) {
            var mrtgmast_no = dsList.GetItem(r, "mrtgmast_no");
            try {
                window.opener.GetMrtgmastNoFromDlg(mrtgmast_no);
                window.close();
            } catch (err) {
                parent.GetMrtgmastNoFromDlg(mrtgmast_no);
                parent.RemoveIFrame();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsList ID="dsList" runat="server" />
    <br />
</asp:Content>
