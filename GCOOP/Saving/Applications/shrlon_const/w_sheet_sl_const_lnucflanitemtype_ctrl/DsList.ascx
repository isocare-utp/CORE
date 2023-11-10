<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.shrlon_const.w_sheet_sl_const_lnucflanitemtype_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" Width="750px">
    <table class="DataSourceRepeater">
        <tr>
            <th width="15%">
                รหัสรายการ
            </th>
            <th>
                ชื่อประเภทการทำรายการเงินกู้
            </th>
            <th width="12%">
                รหัสพิมพ์
            </th>
            <th width="12%">
                ฝั่งรายการ
            </th>
            <th width="12%">
                รหัสกระทบ
            </th>
            <th width="5%">
                ลบ
            </th>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Width="750px" Height="435px" ScrollBars="Auto">
    <table class="DataSourceRepeater">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="15%">
                        <asp:TextBox ID="LOANITEMTYPE_CODE" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="LOANITEMTYPE_DESC" runat="server"></asp:TextBox>
                    </td>
                    <td width="12%">
                        <asp:TextBox ID="PRINT_CODE" runat="server"></asp:TextBox>
                    </td>
                    <td width="12%">
                        <asp:TextBox ID="SIGN_FLAG" runat="server"></asp:TextBox>
                    </td>
                    <td width="12%">
                        <asp:TextBox ID="REVERSE_ITEMTYPE" runat="server"></asp:TextBox>
                    </td>
                    <td width="5%">
                        <asp:Button ID="b_del" runat="server" Text="ลบ" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
