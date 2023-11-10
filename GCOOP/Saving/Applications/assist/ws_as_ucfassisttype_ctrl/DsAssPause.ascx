<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsAssPause.ascx.cs" Inherits="Saving.Applications.assist.ws_as_ucfassisttype_ctrl.DsAssPause" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater" style="width: 98%">
    <tr>
        <th colspan="2">
            ประเภทสวัสดิการ
        </th>
    </tr>
</table>
<asp:Panel ID="Panel2" runat="server" Height="450px" Width="744px" ScrollBars="Auto">
    <table class="DataSourceRepeater" style="width: 98%">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="4%" align="center">
                        <asp:CheckBox ID="check_flag" runat="server" />
                    </td>
                    <td width="96%">
                        <asp:TextBox ID="asstypepausedesc" ReadOnly="true" runat="server" style="text-indent: 10px;"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
