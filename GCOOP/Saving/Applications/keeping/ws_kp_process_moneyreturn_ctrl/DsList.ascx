<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.keeping.ws_kp_process_moneyreturn_ctrl.DsList" %>
<asp:Panel ID="Panel1" runat="server" Width="750px">
    <table class="DataSourceRepeater" style="width: 735px;">
        <thead>
            <tr>
                 <th width="10%">
                    เลขสมาชิก
                </th>
                <th width="25%">
                    ชื่อ - สกุล
                </th>
                <th width="20%">
                    สังกัด
                </th>
                <th width="15%">
                    เลขสัญญา
                </th>
                <th width="15%">
                    ยอดเงินคืน
                </th>
                <th width="15%">
                    เลขบัญชี
                </th>
            </tr>
        </thead>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Width="750px" Height="495px" ScrollBars="Vertical">
    <table class="DataSourceRepeater" style="width: 733px;">
        <tbody>
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <tr>
                         <td width="10%">
                            <asp:TextBox ID="member_no" runat="server" Style="text-align: center"></asp:TextBox>
                        </td>
                        <td width="25%">
                            <asp:TextBox ID="memb_name" runat="server" Style="text-align: center"></asp:TextBox>
                        </td>
                        <td width="20%">
                            <asp:TextBox ID="memb_group" runat="server" Style="text-align: center"></asp:TextBox>
                        </td>
                        <td width="15%">
                            <asp:TextBox ID="loancontract_no" runat="server" Style="text-align: center"></asp:TextBox>
                        </td>
                        <td width="15%">
                            <asp:TextBox ID="int_return" runat="server" ToolTip="#,##0.00" Style="text-align: right;" />
                        </td>
                        <td width="15%">
                            <asp:TextBox ID="deptaccount_no" runat="server" Style="text-align: center"></asp:TextBox>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
</asp:Panel>
