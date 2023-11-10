<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.app_finance.dlg.ws_dlg_fin_extmember_search_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<div align="left">
    <asp:Panel ID="Panel1" runat="server" Width="500px" HorizontalAlign="Left">
        <table class="DataSourceRepeater" style="width: 500px;">
            <tr>
                <th width="25%">
                    เลขที่
                </th>
                <th width="50%">
                    ชื่อ - นามสกุล
                </th>
                <th width="25%">
                    เลขภาษี
                </th>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" Height="390px" ScrollBars="Auto" Width="500px"  HorizontalAlign="Left">
        <table class="DataSourceRepeater" style="width: 500px;">
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <tr>
                        <td width="25%">
                            <asp:TextBox ID="contack_no" runat="server" ReadOnly="true" Style="cursor: pointer"></asp:TextBox>
                        </td>
                        <td width="50%">
                            <asp:TextBox ID="fullname" runat="server" ReadOnly="true" Style="cursor: pointer"></asp:TextBox>
                        </td>
                        <td width="25%">
                            <asp:TextBox ID="tax_id" runat="server" ReadOnly="true" Style="cursor: pointer"></asp:TextBox>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </asp:Panel>
</div>
