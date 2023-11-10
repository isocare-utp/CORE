<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_interestpay_estimate_detail_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table>
    <tr>
        <td colspan="3">
            <u><strong style="font-size: 12px;">รายละเอียดการกู้</strong></u>
        </td>
        <td colspan="4">
            <asp:Button ID="b_cal" runat="server" Text="คำนวน" />
        </td>
    </tr>
</table>
<table class="DataSourceRepeater" style="width: 770px;">
    <tr>
        <th width="5%">
            ชำระ
        </th>
        <th width="11%">
            เลขสัญญา
        </th>
        <th width="10%">
            วันที่กู้
        </th>
        <th width="10%">
            วงเงินกู้
        </th>
        <th width="6%">
            งวด
        </th>
        <th width="11%">
            งวด/ชำระ
        </th>
        <th width="11%">
            คงเหลือ
        </th>
        <th width="10%">
            ด/บ ปัจจุบัน
        </th>
        <th width="10%">
            ต้นเรียกเก็บ
        </th>
        <th width="10%">
            ดบ.เรียกเก็บ
        </th>
        <th width="6%">
            %ชำระ
        </th>
    </tr>
    <asp:Repeater ID="Repeater2" runat="server">
        <ItemTemplate>
            <tr>
                <td align="center" width="5%">
                    <asp:CheckBox ID="operate_flag" runat="server" />
                </td>
                <td width="11%">
                    <asp:TextBox ID="loancontract_no" runat="server" Style="text-align: center; font-size: 11px;
                        background-color: #EEEEEE;"></asp:TextBox>
                </td>
                <td width="10%">
                    <asp:TextBox ID="bfshrcont_balamt" runat="server" Style="text-align: right; font-size: 11px;
                        background-color: #EEEEEE;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td width="10%">
                    <asp:TextBox ID="bflastcalint_date" runat="server" Style="text-align: center; font-size: 11px;
                        background-color: #EEEEEE;"></asp:TextBox>
                </td>
                <td width="6%">
                    <asp:TextBox ID="COMPUTE1" runat="server" Style="text-align: right; font-size: 11px;
                        background-color: #EEEEEE;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td width="11%">
                    <asp:TextBox ID="principal_payamt" runat="server" Style="text-align: right; font-size: 11px;
                        background-color: #CCFF66;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td width="11%">
                    <asp:TextBox ID="interest_payamt" runat="server" Style="text-align: right; font-size: 11px;
                        background-color: #CCFF66;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td width="10%">
                    <asp:TextBox ID="item_payamt" runat="server" Style="text-align: right; font-size: 11px;
                        background-color: #CCFF66;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td width="10%">
                    <asp:TextBox ID="item_balance" runat="server" Style="text-align: right; font-size: 11px;
                        background-color: #EEEEEE;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td width="10%">
                    <asp:TextBox ID="TextBox1" runat="server" Style="text-align: right; font-size: 11px;
                        background-color: #EEEEEE;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td width="6%">
                    <asp:Button ID="b_detail" runat="server" Text="..." />
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
