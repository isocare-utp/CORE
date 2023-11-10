<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true" CodeBehind="ws_dlg_fin_accid.aspx.cs" Inherits="Saving.Applications.app_finance.dlg.ws_dlg_fin_accid_ctrl.ws_dlg_fin_accid" %>
<%@ Register Src="DsCriteria.ascx" TagName="DsCriteria" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsList = new DataSourceTool();
        var dsCriteria = new DataSourceTool();

        function OnDsCriteriaItemChanged(s, r, c, v) {
            if (c == "slipitemtype_code" || c == "item_desc") {
                PostSearch();
            }
        }

        function OnDsCriteriaClicked(s, r, c) {
            if (c == "b_search") {
                PostSearch();
            }
        }

        function OnDsListItemChanged(s, r, c, v) {
        }

        function OnDsListClicked(s, r, c) {
            var accid = dsList.GetItem(r, "slipitemtype_code");
            try {
                window.opener.GetAccidFromDlg(accid);
                window.close();
            } catch (err) {
                parent.GetAccidFromDlg(accid);
                parent.RemoveIFrame();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table>
        <tr>
            <td>
                <uc1:DsCriteria ID="dsCriteria" runat="server" />
            </td>
        </tr>
    </table>
    <br />
    <uc2:DsList ID="dsList" runat="server" />
    <asp:Label ID="LbCount" runat="server" ForeColor="Green" Font-Bold="True" Font-Names="Tahoma"
        Font-Size="14px"></asp:Label>
    <br />
</asp:Content>
