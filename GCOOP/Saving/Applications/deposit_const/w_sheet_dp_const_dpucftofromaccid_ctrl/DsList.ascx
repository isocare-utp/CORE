<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.deposit_const.w_sheet_dp_const_dpucftofromaccid_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" Width="750px">
    <table class="DataSourceRepeater">
        <tr>
            <th width="10%">
                ประเภทเงิน
            </th>
            <th>
                ชื่อคู่บัญชี
            </th>
            <th width="15%">
                เลขคู่บัญชี
            </th>
            <th width="13%">
                ประเภทเงินฝาก
            </th>
            <th width="20%">
                บัญชีธนาคาร
            </th>
            <th width="10%">
                รหัสธนาคาร
            </th>
            <th width="6%">
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
                        <asp:TextBox ID="CASH_TYPE" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="ACCOUNT_DESC" runat="server"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="ACCOUNT_ID" runat="server"></asp:TextBox>
                    </td>
                    <td width="13%">
                        <asp:TextBox ID="DEPTTYPE_CODE" runat="server"></asp:TextBox>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="BANK_ACCNO" runat="server"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="BANK_CODE" runat="server"></asp:TextBox>
                    </td>
                    <td width="6%">
                        <asp:Button ID="b_del" runat="server" Text="ลบ" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
