<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDetail.ascx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_member_detail_max_ctrl.w_dlg_mb_detail_keepdatadet_ctrl.DsDetail" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />

    <table class="TbStyle" style="width: 650px;">
        <tr>
            <th width="4%">
            </th>
            <th width="21%">
                รายละเอียด
            </th>
            <th width="7%">
                งวด
            </th>
            <th width="17%">
                ชำระต้น
            </th>
            <th width="17%">
                ชำระ ด/บ
            </th>
            <th width="17%">
                รวมชำระ
            </th>
            <th width="17%">
                คงเหลือ
            </th>
        </tr>
    </table>
    <asp:Panel ID="Panel2" runat="server" Height="300px" ScrollBars="Auto" HorizontalAlign="Center">
    <table class="TbStyle" style="width: 650px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="4%">
                        <asp:TextBox ID="running_number" runat="server" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td width="21%">
                        <asp:TextBox ID="cp_keepitem" runat="server"></asp:TextBox>
                    </td>
                    <td width="7%">
                        <asp:TextBox ID="period" runat="server" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td width="17%">
                        <asp:TextBox ID="principal_payment" runat="server" Style="text-align: right" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="17%">
                        <asp:TextBox ID="interest_payment" runat="server" Style="text-align: right" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="17%">
                        <asp:TextBox ID="item_payment" runat="server" Style="text-align: right" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="17%">
                        <asp:TextBox ID="item_balance" runat="server" Style="text-align: right" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
