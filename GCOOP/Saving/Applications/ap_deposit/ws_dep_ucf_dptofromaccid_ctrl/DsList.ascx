<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.ap_deposit.ws_dep_ucf_dptofromaccid_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" Height="550px" ScrollBars="Auto" Width="830px">
    <table class="DataSourceRepeater"
        <tr>
            <th width="10%">
                ประเภทเงิน
            </th>
            <th width="26%">
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
            <th width="20%">
                ลบ!
            </th>
        </tr>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="10%">
                        <asp:DropDownList ID="cash_type" runat="server" Style="text-align: center;" >
                        </asp:DropDownList>
                    </td>
                    <td width="26%">
                        <asp:TextBox ID="account_Desc" runat="server"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:DropDownList ID="account_id" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td width="13%">
                        <asp:TextBox ID="moneytype_code" runat="server"></asp:TextBox>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="BANK_ACCNO" runat="server"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="BANK_CODE" runat="server"></asp:TextBox>
                    </td>
                    <td width="20%">
                        <asp:Button ID="b_del" runat="server" Text="ลบ" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
