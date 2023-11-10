<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.mbshr_const.w_sheet_mb_mbucfresigncause_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
    <table class="DataSourceRepeater">
        <tr>
            <th width="5%">
                รหัส
            </th>
            <th width="50%">
                สาเหตุการลาออก
            </th>
            <th width="20%">
                กลุ่มการลาออก
            </th>
            <th width="20%">
                รหัสสมัครใหม่
            </th>
            <th width="5%">
                ลบ
            </th>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Height="520px" ScrollBars="Auto" HorizontalAlign="Center">
    <table class="DataSourceRepeater">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="5%">
                        <asp:TextBox ID="RESIGNCAUSE_CODE" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="50%">
                        <asp:TextBox ID="RESIGNCAUSE_DESC" runat="server"></asp:TextBox>
                    </td>
                    <td width="20%">
                        <asp:DropDownList ID="GROUPRESIGN_CODE" runat="server">
                            <asp:ListItem Value="01">ลาออก</asp:ListItem>
                            <asp:ListItem Value="02">ให้ออก</asp:ListItem>
                            <asp:ListItem Value="03">เสียชีวิต</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="NEWAPPL_STATUS" runat="server"></asp:TextBox>
                    </td>
                    <td width="5%">
                        <asp:Button ID="b_del" runat="server" Text="ลบ" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
