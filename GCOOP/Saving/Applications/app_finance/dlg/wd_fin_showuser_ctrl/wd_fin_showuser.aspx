<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true" CodeBehind="wd_fin_showuser.aspx.cs" Inherits="Saving.Applications.app_finance.dlg.wd_fin_showuser_ctrl.wd_fin_showuser" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsList = new DataSourceTool();             
        function OnDsListClicked(s, r, c) {
            var username = dsList.GetItem(r, "user_name");
            try {
                window.opener.GetUsesNameDlg(username); 
                window.close();
            } catch (err) {
                parent.GetUsesNameDlg(username);
                parent.RemoveIFrame();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <uc1:DsList ID="dsList" runat="server" />
</asp:Content>
