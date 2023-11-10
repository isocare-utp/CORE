<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.mbshr_const.w_sheet_mb_mbucfprename_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
    <table class="DataSourceRepeater">
        <tr>
            <th width="15%">
                รหัส
            </th>
            <th width="30%">
                คำอธิบาย
            </th>
            <th width="30%">
                อักษรย่อ
            </th>
            <th width="20%">
                เพศ
            </th>
            <th width="5%">
            </th>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Height="520px" ScrollBars="Auto" HorizontalAlign="Center">
    <table class="DataSourceRepeater">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="15%">
                        <asp:TextBox ID="PRENAME_CODE" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="30%">
                        <asp:TextBox ID="PRENAME_DESC" runat="server"></asp:TextBox>
                    </td>
                    <td width="30%">
                        <asp:TextBox ID="PRENAME_SHORT" runat="server"></asp:TextBox>
                    </td>
                    <td width="20%">
                        <asp:DropDownList ID="SEX" runat="server">
                            <asp:ListItem Value="M">ชาย</asp:ListItem>
                            <asp:ListItem Value="F">หญิง</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td width="5%">
                        <asp:Button ID="b_del" runat="server" Text="ลบ" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
