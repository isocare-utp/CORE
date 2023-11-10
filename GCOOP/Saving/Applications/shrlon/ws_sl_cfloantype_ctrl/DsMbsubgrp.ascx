<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMbsubgrp.ascx.cs"
    Inherits="Saving.Applications.shrlon.ws_sl_cfloantype_ctrl.DsMbsubgrp" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <tr>
        <th colspan="2">
            กลุ่มสมาชิกสมทบ
        </th>
    </tr>
</table>
<asp:Panel ID="Panel2" runat="server" Height="450px" Width="750px" ScrollBars="Auto">
    <table class="DataSourceRepeater">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="4%" align="center">
                        <asp:CheckBox ID="operate_flag" runat="server" />
                    </td>
                    <td width="96%">
                        <asp:TextBox ID="compute_1" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
