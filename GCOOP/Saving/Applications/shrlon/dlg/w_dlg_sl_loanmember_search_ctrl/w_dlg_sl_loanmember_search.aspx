<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true"
    CodeBehind="w_dlg_sl_loanmember_search.aspx.cs" Inherits="Saving.Applications.shrlon.dlg.w_dlg_sl_loanmember_search_ctrl.w_dlg_sl_loanmember_search" %>

<%@ Register Src="DsCriteria.ascx" TagName="DsCriteria" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsList = new DataSourceTool();
        var dsCriteria = new DataSourceTool();

        function OnDsCriteriaItemChanged(s, r, c, v) {
            if (c == "membgroup_nodd") {
                dsCriteria.SetItem(r, "membgroup_no", v);
            } else if (c == "membtype_desc") {
                dsCriteria.SetItem(r, "membtype_code", v);
            }
        }

        function OnDsCriteriaClicked(s, r, c) {
        }

        function OnDsListItemChanged(s, r, c, v) {
        }

        function OnDsListClicked(s, r, c) {
            var memberno = dsList.GetItem(r, "member_no");
            var fullname = dsList.GetItem(r, "fullname");
            try {
                window.opener.GetLoanMemberFromDlg(memberno, fullname);
                window.close();
            } catch (err) {
                parent.GetLoanMemberFromDlg(memberno, fullname);
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
            <td>
                <asp:Button ID="BtSearch" runat="server" Text="ค้น" Width="60px" Height="60px" OnClick="BtSearch_Click" />
            </td>
        </tr>
    </table>
    <br />
    <asp:Label ID="LbCount" runat="server" ForeColor="Green" Font-Bold="True" Font-Names="Tahoma"
        Font-Size="14px"></asp:Label>
    <uc2:DsList ID="dsList" runat="server" />
    <br />
</asp:Content>
