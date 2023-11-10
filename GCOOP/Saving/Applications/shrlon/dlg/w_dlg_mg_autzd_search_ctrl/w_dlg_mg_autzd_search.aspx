<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true"
    CodeBehind="w_dlg_mg_autzd_search.aspx.cs" Inherits="Saving.Applications.shrlon.dlg.w_dlg_mg_autzd_search_ctrl.w_dlg_mg_autzd_search" %>

<%@ Register src="DsList.ascx" tagname="DsList" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsList = new DataSourceTool();

        function OnDsListClicked(s, r, c, v) {
            if (c == "template_no" || c == "autzd_name" || c == "pc_address") {
                dsList.SetRowFocus(r);
                var template_no = dsList.GetItem(r, "template_no");
                try {
                    window.opener.GetTemplateNoFromDlg(template_no);
                    window.close();
                } catch (err) {
                    parent.GetTemplateNoFromDlg(template_no);
                    window.close();
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
        type="text/css" />
    <table class="DataSourceFormView" style="width: 620px;">
        <tr>
            <td width="25%">
                <div>
                    <span>ชื่อผู้รับมอบอำนาจ:</span>
                </div>
            </td>
            <td>
                <div>
                    <asp:TextBox ID="autzd_name" runat="server"></asp:TextBox>
                    <br />
                </div>
            </td>
            <td width="10%">
                <asp:Button ID="BtSearch" runat="server" Text="ค้นหา" OnClick="BtSearch_Click" />
            </td>
        </tr>
    </table>
    <uc1:DsList ID="dsList" runat="server" />
    <br />
</asp:Content>
