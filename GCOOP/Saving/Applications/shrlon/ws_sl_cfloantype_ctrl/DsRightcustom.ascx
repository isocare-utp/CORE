<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsRightcustom.ascx.cs"
    Inherits="Saving.Applications.shrlon.ws_sl_cfloantype_ctrl.DsRightcustom" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<div align="left">
    <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
        <table class="DataSourceRepeater">
            <tr>
                <th rowspan="2" width="3%">
                </th>
                <th colspan="2">
                    ระยะเวลา(เดือน)
                </th>
                <th colspan="2">
                    มีหุ้น(บาท)
                </th>
                <th colspan="2">
                    เงินเดือน
                </th>
                <th colspan="2">
                    สิทธิการกู้
                </th>
                <th rowspan="2" width="11%">
                    วงเงินกู้สูงสุด
                </th>
                <th rowspan="2" width="4%">
                </th>
            </tr>
            <tr>
                <th width="7%">
                    ตั้งแต่
                </th>
                <th width="7%">
                    ถึง
                </th>
                <th width="14%">
                    ตั้งแต่(>=)
                </th>
                <th width="14%">
                    (<=)ถึง
                </th>
                <th width="12%">
                    ตั้งแต่(>=)
                </th>
                <th width="12%">
                    (<=)ถึง
                </th>
                <th width="8%">
                    ง/ด (เท่า)
                </th>
                <th width="8%">
                    หุ้น (เท่า)
                </th>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" Height="200px" Width="750px" ScrollBars="Auto">
        <table class="DataSourceRepeater">
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <tr>
                        <td width="3%">
                            <asp:TextBox ID="running_number" runat="server" Style="text-align: center;"></asp:TextBox>
                        </td>
                        <td width="7%">
                            <asp:TextBox ID="startmember_time" runat="server" Style="text-align: right;" ToolTip="#,##0"></asp:TextBox>
                        </td>
                        <td width="7%">
                            <asp:TextBox ID="endmember_time" runat="server" Style="text-align: right;" ToolTip="#,##0"></asp:TextBox>
                        </td>
                        <td width="14%">
                            <asp:TextBox ID="startshare_amt" runat="server" Style="text-align: right;" ToolTip="#,##0"></asp:TextBox>
                        </td>
                        <td width="14%">
                            <asp:TextBox ID="endshare_amt" runat="server" Style="text-align: right;" ToolTip="#,##0"></asp:TextBox>
                        </td>
                        <td width="12%">
                            <asp:TextBox ID="startsalary_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                        </td>
                        <td width="12%">
                            <asp:TextBox ID="endsalary_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                        </td>
                        <td width="8%">
                            <asp:TextBox ID="multiple_salary" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                        </td>
                        <td width="8%">
                            <asp:TextBox ID="multiple_share" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                        </td>
                        <td width="11%">
                            <asp:TextBox ID="maxloan_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                        </td>
                        <td width="4%">
                            <asp:Button ID="b_del" runat="server" Text="-" />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </asp:Panel>
</div>
