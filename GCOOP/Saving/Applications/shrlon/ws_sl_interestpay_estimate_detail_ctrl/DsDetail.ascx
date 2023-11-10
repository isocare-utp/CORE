<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDetail.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_interestpay_estimate_detail_ctrl.DsDetail" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table>
    <tr>
        <td colspan="7">
            <u><strong style="font-size: 12px;">รายการหนี้</strong></u>
        </td>
    </tr>
</table>
<table class="DataSourceRepeater" style="width: 770px;">
    <tr>
        <th rowspan="2">
        </th>
        <th style="font-size: 12px;" rowspan="2">
            เลขสัญญา
        </th>
        <th style="font-size: 12px;" rowspan="2">
            ต้นเงิน
            <th colspan="2" style="font-size: 12px;">
                ดอกเบี้ย
            </th>
            <th colspan="2" style="font-size: 12px;">
                การชำระ
            </th>
            <th style="font-size: 12px;" rowspan="2">
                รวมชำระ
            </th>
            <th style="font-size: 12px;" rowspan="2">
                ต้นคงเหลือ
            </th>
             <th style="font-size: 12px;" rowspan="2">
               
            </th>
    </tr>
    <tr>
        <th style="font-size: 12px;">
            ด/บ ตั้งแต่
        </th>
        <th style="font-size: 12px;">
            ด/บ ต้องชำระ
        </th>
       <%-- <th colspan="2" style="font-size: 12px;">
            งวด
        </th>--%>
        <th style="font-size: 12px;">
            ต้นเงิน
        </th>
        <th style="font-size: 12px;">
            ด/บ
        </th>
    </tr>
    <asp:Repeater ID="Repeater2" runat="server">
        <ItemTemplate>
            <tr>
                <td align="center" width="3%">
                    <asp:CheckBox ID="operate_flag" runat="server" />
                </td>
                <td width="8%">
                    <asp:TextBox ID="loancontract_no" runat="server" Style="text-align: center; font-size: 11px;
                        background-color: #EEEEEE;"></asp:TextBox>
                </td>
                <td width="11%">
                    <asp:TextBox ID="bfshrcont_balamt" runat="server" Style="text-align: right; font-size: 11px;
                        background-color: #EEEEEE;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td width="9%">
                    <asp:TextBox ID="bflastcalint_date" runat="server" Style="text-align: center; font-size: 11px;
                        background-color: #EEEEEE;"></asp:TextBox>
                </td>
                <td width="10%">
                    <asp:TextBox ID="COMPUTE1" runat="server" Style="text-align: right; font-size: 11px;
                        background-color: #EEEEEE;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <%--<td width="5%" align="center">
                    <asp:CheckBox ID="periodcount_flag" runat="server" />
                   
                </td>--%>
               <%-- <td width="7%">
                    <asp:TextBox ID="period" runat="server" Style="text-align: right; font-size: 11px;
                        background-color: #EEEEEE;" ToolTip="#,##0.00" ReadOnly="true"></asp:TextBox>
                </td>--%>
                <td width="11%">
                    <asp:TextBox ID="principal_payamt" runat="server" Style="text-align: right; font-size: 11px;
                        background-color: #CCFF66;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td width="11%">
                    <asp:TextBox ID="interest_payamt" runat="server" Style="text-align: right; font-size: 11px;
                        background-color: #CCFF66;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td width="11%">
                    <asp:TextBox ID="item_payamt" runat="server" Style="text-align: right; font-size: 11px;
                        background-color: #CCFF66;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td width="11%">
                    <asp:TextBox ID="item_balance" runat="server" Style="text-align: right; font-size: 11px;
                        background-color: #EEEEEE;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                 <td width="3%">
                     <asp:Button ID="b_detail" runat="server" Text="..." />
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
