<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsCmSalbal.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_cfloantype_ctrl.DsCmSalbal" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet" type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet" type="text/css" />
<table class="DataSourceRepeater">
    <tr>
        <th width="7%">
        </th>
        <th width="40%" colspan="2">
            ตรวจเงินเดือน
        </th>
        <th width="15%">
            เงินเดือนคงเหลือ (บาท)
        </th>
        <th width="15%">
            เงินเดือนคงเหลือ (%)
        </th>
    </tr>
</table>
<asp:Panel ID="Panel2" runat="server" Height="200px" Width="750px" ScrollBars="Auto">
    <table class="DataSourceRepeater">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="7%">
                        <asp:TextBox ID="running_number" runat="server" Style="text-align:center;"></asp:TextBox>
                    </td>
                    <td width="40%">
                        <asp:DropDownList ID="salarybal_code" runat="server"> </asp:DropDownList>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="salarybal_amt" runat="server" Style="text-align:right;" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="salarybal_percent" runat="server" Style="text-align:right;" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>