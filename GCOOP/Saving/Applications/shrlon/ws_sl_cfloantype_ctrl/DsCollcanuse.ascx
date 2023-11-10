<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsCollcanuse.ascx.cs"
    Inherits="Saving.Applications.shrlon.ws_sl_cfloantype_ctrl.DsCollcanuse" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" Width="450px">
    <table class="DataSourceRepeater" style="width: 430px;">
        <tr>
            <th width="36%">
                กลุ่ม
            </th>
            <th width="40%">
                ประเภท
            </th>
            <th width="20%">
                % การค้ำได้
            </th>
            <th valign="4%">
            </th>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Height="350px" Width="450px" ScrollBars="Auto">
    <table class="DataSourceRepeater" style="width: 430px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="36%">
                        <asp:DropDownList ID="loancolltype_code" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td width="40%">
                        <asp:DropDownList ID="collmasttype_code" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="coll_percent" runat="server" ToolTip="#,##0.00" Style="text-align: right;"></asp:TextBox>
                    </td>
                    <td width="4%">
                        <asp:Button ID="b_del" runat="server" Text="-" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
