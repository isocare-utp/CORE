<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMthExpense.ascx.cs" 
Inherits="Saving.Applications.mbshr.ws_mbshr_member_detail_main_ctrl.DsMthExpense" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="TbStyle" style="width: 710px">
    <tr>
        <th width="10%">
            ลำดับ
        </th>
        <th width="70%">
            รายละเอียดการหักรายเดือน
        </th>
        <th width="20%">
            จำนวนเงินหัก
        </th>
    </tr>
</table>
<asp:Panel ID="Panel2" runat="server" Height="500px" ScrollBars="Auto" HorizontalAlign="Center">
    <table class="TbStyle" style="width: 710px">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="10%">
                        <asp:TextBox ID="SEQ_NO" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="70%">
                        <asp:TextBox ID="MTHEXPENSE_DESC" runat="server" Style="text-align: left;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="MTHEXPENSE_AMT" runat="server" Style="text-align: right;" ToolTip="#,##0.00" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
            <tr>
        <td style="border-style: none; text-align: right" colspan="2">
            <strong>รวม:</strong>
        </td>
        <td style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #000000;">
            <asp:TextBox ID="cp_sum_expense" runat="server" Style="text-align: right; font-weight: bold;"></asp:TextBox>
        </td>
        <td style="border-style: none">
        </td>
    </tr>
    </table>
</asp:Panel>
