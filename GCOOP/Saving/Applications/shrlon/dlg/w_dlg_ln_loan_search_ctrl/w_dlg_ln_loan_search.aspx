<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true"
    CodeBehind="w_dlg_ln_loan_search.aspx.cs" Inherits="Saving.Applications.shrlon.dlg.w_dlg_ln_loan_search_ctrl.w_dlg_ln_loan_search" %>

<%@ Register Src="DsCriteria.ascx" TagName="DsCriteria" TagPrefix="uc1" %>
<%@ Register Src="DsSearch.ascx" TagName="DsSearch" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsCriteria = new DataSourceTool();
        var dsSearch = new DataSourceTool();

        function OnDsCriteriaItemChanged(s, r, c, v) {
        }

        function OnDsCriteriaClicked(s, r, c) {
        }

        function OnDsSearchItemChanged(s, r, c, v) {
        }

        function OnDsSearchClicked(s, r, c) {
            if (r >= 0) {
                var loancontract_no = dsSearch.GetItem(r, "loancontract_no");
                parent.GetValueSearch(loancontract_no);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <table>
        <tr>
            <td valign="middle">
                <uc1:DsCriteria ID="dsCriteria" runat="server" />
            </td>
            <td valign="middle">
                <asp:Button ID="b_search" runat="server" Text="ค้นข้อมูล" Style="width: 60px; height: 50px;"
                    OnClick="b_search_Click" />
            </td>
        </tr>
    </table>
    <br />
    <span class="TitleSpan" style="margin-left: 4px;">ข้อมูลสัญญาเงินกู้</span>
    <uc2:DsSearch ID="dsSearch" runat="server" />
    <hr align="left" style="margin-left: 3px; width: 690px" />
    <asp:Label ID="LbCount" runat="server" Text="" ForeColor="Green" Font-Bold="True"
        Font-Names="Tahoma" Font-Size="14px" Style="margin-left: 3px; margin-top: 20px;"></asp:Label>
</asp:Content>
