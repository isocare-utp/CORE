<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsExpense.ascx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_member_detail_max_ctrl.DsExpense" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <span style="font-size: 13px;"><font color="#cc0000"><u><strong>บัญชีหลักสมาชิก</strong></u></font></span>
        <table class="FormStyle">
            <tr>
                <td width="11%">
                    ประเภทชำระ:
                </td>
                <td width="35%">
                    <asp:TextBox ID="cp_expense_code" runat="server" Style="text-align: center;" ReadOnly="true"
                        Width="150px"></asp:TextBox>
                    <%-- <asp:DropDownList ID="expense_code" runat="server" Enabled="false">
                        <asp:ListItem Value="0" Text=""></asp:ListItem>
                        <asp:ListItem Value="CSH">เงินสด</asp:ListItem>
                        <asp:ListItem Value="TRN">บัญชีสหกรณ์</asp:ListItem>
                        <asp:ListItem Value="CBT">บัญชีธนาคาร</asp:ListItem>
                    </asp:DropDownList>--%>
                </td>
                <td width="11%">
                    ธนาคาร:
                </td>
                <td width="43%">
                    <asp:TextBox ID="expense_bank" runat="server" Width="40px" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    <asp:TextBox ID="bank_desc" runat="server" Width="250px" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    สาขา:
                </td>
                <td>
                    <asp:TextBox ID="expense_branch" runat="server" Width="40px" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    <asp:TextBox ID="branch_name" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    เลขที่บัญชี:
                </td>
                <td>
                    <asp:TextBox ID="expense_accid" runat="server" ReadOnly="true" Style="text-align: center;"
                        Width="150px"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
