<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_reprint_payout_ctrl.DsList" %>

<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server"  Height="450px" ScrollBars="Auto" HorizontalAlign="Left">
    <table class="DataSourceRepeater">
        <tr>
            <th width="4%">                
            </th>
            <th width="10%">
                รายการ
            </th>
            <th width="12%">
                เลขที่ใบสำคัญ
            </th>
            <th width="12%">
                วันที่ใบสำคัญ
            </th>
            <th width="12%">
                ทะเบียน
            </th>
            <th width="40%">
                ชื่อ-สกุล
            </th>
            <th width="10%">
                ยอดเงินจ่าย
            </th>
        </tr>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td align="center">
                        <asp:CheckBox ID="checkselect" runat="server" Style="text-align: center" />
                    </td>
                    <td align="center">
                        <asp:TextBox ID="sliptype_code" runat="server" ReadOnly="True" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="payoutslip_no" runat="server" ReadOnly="True" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="slip_date" runat="server" ReadOnly="True" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="member_no" runat="server" ReadOnly="True" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="mbname" runat="server" ReadOnly="True" Style="text-align: left"></asp:TextBox>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="payout_amt" runat="server" ReadOnly="True" Style="text-align: right"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
