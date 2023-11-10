<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsClearbuyshr.ascx.cs"
    Inherits="Saving.Applications.shrlon.ws_sl_cfloantype_ctrl.DsClearbuyshr" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <tr>
        <th rowspan="2" width="5%">
        </th>
        <th rowspan="2" width="24%">
            ตั้งแต่ ( >= )
        </th>
        <th rowspan="2" width="24%">
            ถึง ( < )
        </th>
        <th colspan="2">
            ทุนเรือนหุ้น
        </th>
        <th width="4%" rowspan="2"></th>
    </tr>
    <tr>
        <th width="19%">
            % ขั้นต่ำ
        </th>
        <th width="24%">
            มูลค่าขั้นต่ำ
        </th>
    </tr>
</table>
<asp:Panel ID="Panel2" runat="server" Height="110px" Width="750px" ScrollBars="Auto">
    <table class="DataSourceRepeater">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="5%">
                        <asp:TextBox ID="running_number" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="24%">
                        <asp:TextBox ID="startloan_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="24%">
                        <asp:TextBox ID="endloan_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="19%">
                        <asp:TextBox ID="sharestk_percent" runat="server" Style="text-align: center;" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="24%">
                        <asp:TextBox ID="sharestk_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="4%">
                            <asp:Button ID="b_del" runat="server" Text="-"/>
                        </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
