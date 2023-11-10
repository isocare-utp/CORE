<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true"
    CodeBehind="w_dlg_ln_collmaster_search.aspx.cs" Inherits="Saving.Applications._global.w_dlg_ln_collmaster_search_ctrl.w_dlg_ln_collmaster_search" %>

<%@ Register Src="DsCriteria.ascx" TagName="DsCriteria" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsCriteria = new DataSourceTool();
        var dsList = new DataSourceTool();
        function OnDsCriteriaItemChanged(s, r, c, v) {
        }

        function OnDsCriteriaClicked(s, r, c) {
        }

        function OnDsListItemChanged(s, r, c, v) {
        }

        function OnDsListClicked(s, r, c) {
            if (r >= 0) {
                var collmast_no = dsList.GetItem(r, "collmast_no");
                var collmast_refno = dsList.GetItem(r, "collmast_refno");
                parent.GetValueSearchCollmastNo(collmast_no, collmast_refno);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <table width="700">
        <tr>
            <td>
                <uc1:DsCriteria ID="dsCriteria" runat="server" />
            </td>
            <td width="70">
                <asp:Button ID="BtSearch" runat="server" Text="ค้นข้อมูล" Style="width: 68px; height: 50px;"
                    OnClick="BtSearch_Click" />
            </td>
        </tr>
    </table>
    <br />
    <div align="left" style="margin-left: 3px;">
        <span class="TitleSpan">ข้อมูลหลักทรัพย์</span>
        <uc2:DsList ID="dsList" runat="server" />
        <hr align="left" style="width: 700px;" />
        <asp:Label ID="LbCount" runat="server" ForeColor="Green" Font-Bold="True" Font-Names="Tahoma"
            Font-Size="14px"></asp:Label>
    </div>
</asp:Content>
