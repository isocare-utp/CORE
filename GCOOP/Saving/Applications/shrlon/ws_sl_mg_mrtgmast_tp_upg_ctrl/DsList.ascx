<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_mg_mrtgmast_tp_upg_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table>
    <tr>
        <td colspan="2">
            <strong style="font-size: 13px;"><u>ครั้งที่ขึ้นเงิน</u></strong>
        </td>
    </tr>
</table>
<table class="DataSourceRepeater" style="width: 200px; border: 1px dotted black;">
    <tr>
        <th>
            ขึ้นเงินครั้งที่
        </th>
        <th>
            วันที่ขึ้นเงิน
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td width="40%">
                    <asp:TextBox ID="upgrade_no" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td width="80%">
                    <asp:TextBox ID="upgrade_date" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
