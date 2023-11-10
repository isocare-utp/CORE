<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsCollprop.ascx.cs"
    Inherits="Saving.Applications.shrlon.ws_sl_collateral_master_ctrl.DsCollprop" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="TbStyle" style="width: 520px">
    <tr>
        <th width="5%">
        </th>
        <th width="89%">
            ชื่อ-ชื่อสกุลผู้มีกรรมสิทธิ์
        </th>
        <th width="6%">
        </th>
    </tr>
</table>
<asp:Panel ID="Panel1" runat="server" Height="150px" ScrollBars="Auto">
    <table class="TbStyle" style="width: 520px">
        <asp:Repeater ID="Repeater2" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="5%">
                        <asp:TextBox ID="running_number" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="89%">
                        <asp:TextBox ID="prop_desc" runat="server"></asp:TextBox>
                    </td>
                    <td width="6%">
                        <asp:Button ID="b_del" runat="server" Text="ลบ" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
