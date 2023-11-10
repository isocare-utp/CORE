<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsPaymentlist.ascx.cs"
    Inherits="Saving.Applications.shrlon.ws_sl_cfloantype_ctrl.DsPaymentlist" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <tr>
        <th rowspan="2" width="4%">
        </th>
        <th colspan="3">
            วงเงินกู้
        </th>
        <th width="4%" rowspan="2"></th>
    </tr>
    <tr>
        <th width="32%">
            ตั้งแต่ ( >= )
        </th>
        <th width="32%">
            ( < ) ถึง
        </th>
        <th width="28%">
            งวดการส่งชำระสูงสุด
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
                    <td width="32%">
                        <asp:TextBox ID="money_from" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="32%">
                        <asp:TextBox ID="money_to" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="28%">
                        <asp:TextBox ID="max_period" runat="server" Style="text-align: right;" ToolTip="#,##0"></asp:TextBox>
                    </td>
                    <td width="4%">
                            <asp:Button ID="b_del" runat="server" Text="-"/>
                        </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>