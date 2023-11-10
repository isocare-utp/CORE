<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsShare.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_receipandpay_cash_daily_ctrl.DsShare" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater" style="width: 750px;">
    <tr>
        <th>
            ประเภท
        </th>
        <th>
            จำนวน
        </th>
        <th>
            เงิน
        </th>
    </tr>
    <asp:Repeater ID="Repeater2" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="sharetype_desc" runat="server"></asp:TextBox>
                </td>
                <td width="10%">
                    <asp:TextBox ID="count_item" runat="server" style="text-align:center;" ToolTip="#,##0"></asp:TextBox>
                </td>
                <td width="25%">
                    <asp:TextBox ID="sum_prinamt" runat="server" style="text-align:right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>