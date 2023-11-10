<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsLoantypeSalbal.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_cfloantype_ctrl.DsLoantypeSalbal" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet" type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet" type="text/css" />
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
    <table class="DataSourceRepeater">
        <tr>
            <th rowspan="2" width="4%">
            </th>
            <th colspan="2">
                วงเงินกู้
            </th>
            <th rowspan="2" width="15%">
                เงินเดือนคงเหลือ (%)
            </th>
            <th rowspan="2" width="15%">
                เงินเดือนคงเหลือ (บาท)
            </th>
            <th width="4%" rowspan="2">
            </th>
        </tr>
        <tr>
            <th width="19%">
                ตั้งแต่
            </th>
            <th width="19%">
                ถึง
            </th>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Height="200px" Width="750px" ScrollBars="Auto">
    <table class="DataSourceRepeater">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="4%">
                        <asp:TextBox ID="running_number" runat="server" Style="text-align:center;"></asp:TextBox>
                    </td>
                    <td width="19%">
                        <asp:TextBox ID="money_from" runat="server" Style="text-align:right;" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="19%">
                        <asp:TextBox ID="money_to" runat="server" Style="text-align:right;" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="salbalmin_perc" runat="server" Style="text-align:right;" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="salbalmin_amt" runat="server" Style="text-align:right;" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="4%">
                        <asp:Button ID="b_dellnbal" runat="server" Text="-" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>