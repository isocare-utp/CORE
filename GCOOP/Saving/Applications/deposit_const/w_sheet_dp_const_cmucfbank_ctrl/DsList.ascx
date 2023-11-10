<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.deposit_const.w_sheet_dp_const_cmucfbank_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" Width="750px">
    <table class="DataSourceRepeater">
        <tr>
            <th width="10%">
                รหัส
            </th>
            <th width="70%">
                ชื่อธนาคาร
            </th>
            <th>
                ตัวย่อ
            </th>
            <th width="5%">
                ลบ!
            </th>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Width="750px" Height="435px" ScrollBars="Auto">
    <table class="DataSourceRepeater">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="10%">
                        <asp:TextBox ID="bank_code" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="70%">
                        <asp:TextBox ID="bank_desc" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="bank_shortname" runat="server"></asp:TextBox>
                    </td>
                    <td width="5%">
                        <asp:Button ID="b_del" runat="server" Text="ลบ" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
