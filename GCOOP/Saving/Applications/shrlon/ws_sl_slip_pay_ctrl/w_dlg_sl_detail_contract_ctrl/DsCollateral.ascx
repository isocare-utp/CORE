<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsCollateral.ascx.cs"
    Inherits="Saving.Applications.shrlon.ws_sl_slip_pay_ctrl.w_dlg_sl_detail_contract_ctrl.DsCollateral" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel2" runat="server" Height="425px" ScrollBars="Auto" HorizontalAlign="Center">
    <table class="TbStyle" style="width: 820px;">
        <tr>
            <th width="15%">
                ประเภทหลักประกัน
            </th>
            <th width="12%">
                เลขหลักประกัน
            </th>
            <th width="50%">
                รายละเอียด
            </th>
            <th width="8%">
                %การค้ำ
            </th>
            <th width="15%">
                ยอดเงินค้ำประกัน
            </th>
        </tr>
    </table>
    <table class="TbStyle" style="width: 820px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="15%">
                        <asp:TextBox ID="loancolltype_desc" runat="server"></asp:TextBox>
                    </td>
                    <td width="12%">
                        <asp:TextBox ID="ref_collno" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="50%">
                        <asp:TextBox ID="description" runat="server"></asp:TextBox>
                    </td>
                    <td width="8%">
                        <asp:TextBox ID="collactive_percent" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="collactive_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
