<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.shrlon_const.w_sheet_sl_const_cmucftofromaccid_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" Width="750px">
    <table class="DataSourceRepeater">
        <tr>
            <th width="12%">
                sliptype_code
            </th>
            <th width="12%">
                รหัสเงิน
            </th>
            <th width="12%">
                รหัสบัญชี
            </th>
            <th width="34%">
                รหัสบัญชี
            </th>
            <th width="10%">
                default_flag
            </th>
            <th width="15%">
                defaultpayout_flag
            </th>
            <th width="5%">
            </th>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Width="750px" Height="500px" ScrollBars="Auto">
    <table class="DataSourceRepeater">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="12%">
                        <asp:TextBox ID="sliptype_code" runat="server"></asp:TextBox>
                    </td>
                    <td width="12%">
                        <asp:TextBox ID="moneytype_code" runat="server"></asp:TextBox>
                    </td>
                    <td width="12%">
                        <asp:TextBox ID="account_id" runat="server"></asp:TextBox>
                    </td>
                    <td width="34%">
                        <asp:TextBox ID="account_name" runat="server"></asp:TextBox>
                    </td>
                    <td width="10%" align="center">
                        <asp:CheckBox ID="default_flag" runat="server" />
                    </td>
                    <td width="15%" align="center">
                        <asp:CheckBox ID="defaultpayout_flag" runat="server" />
                    </td>
                    <td width="5%">
                        <asp:Button ID="b_del" runat="server" Text="-" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>

