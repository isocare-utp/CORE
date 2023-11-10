<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDpcancoll.ascx.cs"
    Inherits="Saving.Applications.shrlon.ws_sl_cfloantype_ctrl.DsDpcancoll" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" Width="300px">
    <table class="DataSourceRepeater" style="width: 280px;">
        <tr>
            <th>
                เงินฝากที่ค้ำได้
            </th>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Height="350px" Width="300px" ScrollBars="Auto">
    <table class="DataSourceRepeater" style="width: 350px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="5%" align="center">
                        <asp:CheckBox ID="operate_flag" runat="server" />
                    </td>
                    <td width="95%">
                        <asp:TextBox ID="compute_1" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
