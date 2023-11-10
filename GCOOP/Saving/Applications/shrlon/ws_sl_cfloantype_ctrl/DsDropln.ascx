<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDropln.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_cfloantype_ctrl.DsDropln" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <tr>
        <th width="4%">
        </th>
        <th colspan="2">
            ประเภทเงินกู้ที่ห้ามกู้
        </th>
        <th width="4%">
        </th>
    </tr>
</table>
<asp:Panel ID="Panel2" runat="server" Height="200px" Width="750px" ScrollBars="Auto">
    <table class="DataSourceRepeater">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="4%">
                        <asp:TextBox ID="running_number" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="7%">
                        <asp:TextBox ID="loantype_pause_1" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="85%">
                        <asp:DropDownList ID="loantype_pause" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td width="4%">
                        <asp:Button ID="b_del" runat="server" Text="-" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
