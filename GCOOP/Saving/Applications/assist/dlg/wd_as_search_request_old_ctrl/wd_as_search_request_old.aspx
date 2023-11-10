<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true"
    CodeBehind="wd_as_search_request_old.aspx.cs" Inherits="Saving.Applications.assist.dlg.wd_as_search_request_old_ctrl.wd_as_search_request_old" %>

<%@ Register Src="DsCriteria.ascx" TagName="DsCriteria" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsList = new DataSourceTool();
        var dsCriteria = new DataSourceTool();

        function OnDsCriteriaItemChanged(s, r, c, v) {
            if (c == "assisttype_code") {
                InitAsssistpaytype();
            }
        }

        function OnDsCriteriaClicked(s, r, c) {
        }

        function OnDsListItemChanged(s, r, c, v) {
        }

        function OnDsListClicked(s, r, c) {
            var memberno = dsList.GetItem(r, "member_no");
            var reqdoc_no = dsList.GetItem(r, "assist_docno");
            var assisttype_code = dsList.GetItem(r, "assisttype_code");
            try {
                window.opener.GetMembNoFromDlgReqOld(memberno, reqdoc_no, assisttype_code);
                window.close();
            } catch (err) {
                parent.GetMembNoFromDlgReqOld(memberno, reqdoc_no, assisttype_code);
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
                <asp:Button ID="BtSearch" runat="server" Text="ค้นหา" Width="60px" Height="60px"
                    OnClick="BtSearch_Click" />
            </td>
        </tr>
    </table>
    <br />
    <uc2:DsList ID="dsList" runat="server" />
    <asp:Label ID="LbCount" runat="server" ForeColor="Green" Font-Bold="True" Font-Names="Tahoma"
        Font-Size="14px"></asp:Label>
    <br />
</asp:Content>
