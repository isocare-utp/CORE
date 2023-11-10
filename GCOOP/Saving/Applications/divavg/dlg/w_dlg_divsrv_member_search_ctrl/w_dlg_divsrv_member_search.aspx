<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true" CodeBehind="w_dlg_divsrv_member_search.aspx.cs" Inherits="Saving.Applications.divavg.dlg.w_dlg_divsrv_member_search_ctrl.w_dlg_divsrv_member_search" %>
<%@ Register src="DsCriteria.ascx" tagname="DsCriteria" tagprefix="uc1" %>
<%@ Register src="DsList.ascx" tagname="DsList" tagprefix="uc2" %>
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
        try {
            window.opener.GetValueFromDlg(memberno);
            window.close();
        } catch (err) {
            parent.GetValueFromDlg(memberno);
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
    <uc2:DsList ID="dsList" runat="server" />
    <br />
  
    <asp:Label ID="LbCount" runat="server" ForeColor="Green" Font-Bold="True" Font-Names="Tahoma"
        Font-Size="14px"></asp:Label>
    <br />
</asp:Content>
