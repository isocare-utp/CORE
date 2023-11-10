<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsFollow.ascx.cs" Inherits="Saving.Applications.shrlon.w_sheet_ln_nplmaster_ctrl.DsFollow" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server">
    <table class="DataSourceRepeater" style="width: 686px;">
        <tr>
            <th width="10%">
                ดำเนินแล้ว
            </th>
            <th width="13%">
                วันที่
            </th>
            <th width="20%">
                หัวข้อ
            </th>
            <th>
                รายละเอียด
            </th>
            <th width="8%">
                <span style="color: Blue; text-decoration: underline; cursor: pointer;" onclick="<%=OnClickAddRow%>">
                    เพิ่มแถว</span>
            </th>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Width="705px" Height="141px" ScrollBars="Vertical">
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <table class="DataSourceRepeater" style="width: 686px;">
                <tr>
                    <td align="center" width="10%">
                        <asp:CheckBox ID="done" runat="server" ToolTip="Y:N" />
                    </td>
                    <td width="13%">
                        <asp:TextBox ID="follow_date" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="topic" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="follow_desc" runat="server"></asp:TextBox>
                    </td>
                    <td width="8%">
                        <asp:Button ID="b_del" runat="server" Text="ลบ" />
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:Repeater>
</asp:Panel>
