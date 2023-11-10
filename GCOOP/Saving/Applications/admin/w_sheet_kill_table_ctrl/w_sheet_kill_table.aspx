<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_kill_table.aspx.cs" Inherits="Saving.Applications.admin.w_sheet_kill_table_ctrl.w_sheet_kill_table" %>

<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsList = new DataSourceTool();

        function Post_AllKill() {
            Post_AllKill();
        }
        function OnDsListClicked(s, r, c) {
            if (c == "b_del") {
                if (confirm("ยืนยันการ kill table")) {
                    JsKillTable();
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <table class="DataSourceFormView" style="width: 500px">
        <tr>
            <td width="30%">
                <input type="button" value="kill table lock" style="width: 250px" onclick="Post_AllKill()" />
            </td>
        </tr>
    </table>
    <br/>
    <uc1:DsList ID="dsList" runat="server" />
</asp:Content>
