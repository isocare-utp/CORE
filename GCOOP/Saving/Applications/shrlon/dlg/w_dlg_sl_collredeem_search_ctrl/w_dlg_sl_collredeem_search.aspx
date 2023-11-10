<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true"
    CodeBehind="w_dlg_sl_collredeem_search.aspx.cs" Inherits="Saving.Applications.shrlon.dlg.w_dlg_sl_collredeem_search_ctrl.w_dlg_sl_collredeem_search" %>

<%@ Register Src="DsCriteria.ascx" TagName="DsCriteria" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsList = new DataSourceTool();
        var dsCriteria = new DataSourceTool();

        function OnDsCriteriaItemChanged(s, r, c, v) {

            if (c == "membgroup_desc") {
                var membgroup = dsCriteria.GetItem(0, "membgroup_desc");
                dsCriteria.SetItem(0, "membgroup_code", membgroup);
            }
        }

        function OnDsListClicked(s, r, c, v) {
            if (c == "collmast_no" || c == "collmasttype_code" || c == "collmast_desc" || c == "collmast_price") {
                dsList.SetRowFocus(r);
                var collmast_no = dsList.GetItem(r, "collmast_no");
                try {
                    window.opener.GetValueFromDlg(collmast_no);
                    window.close();
                } catch (err) {
                    parent.GetValueFromDlg(collmast_no);
                    window.close();
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <div align="center">
        <table>
            <tr>
                <td>
                    <uc1:DsCriteria ID="dsCriteria" runat="server" />
                </td>
                <td>
                    <asp:Button ID="BtSearch" runat="server" Text="ค้น" Width="60px" Height="60px" OnClick="BtSearch_Click" />
                </td>
            </tr>
        </table>
        <br />
        <uc2:DsList ID="dsList" runat="server" />
        <asp:Label ID="LbCount" runat="server" ForeColor="Green" Font-Bold="True" Font-Names="Tahoma"
            Font-Size="14px"></asp:Label>
    </div>
    <br />
</asp:Content>
