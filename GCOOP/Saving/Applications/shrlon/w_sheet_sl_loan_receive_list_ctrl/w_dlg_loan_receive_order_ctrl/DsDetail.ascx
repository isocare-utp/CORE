<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDetail.ascx.cs" Inherits="Saving.Applications.shrlon.w_sheet_sl_loan_receive_list_ctrl.w_dlg_loan_receive_order_ctrl.DsDetail" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater" style="width: 750px;">
    <tr>
        <th rowspan="2">
        </th>
        <th rowspan="2" style="font-size: 11px;">
            เลขสัญญา
        </th>
        <th rowspan="2" style="font-size: 11px;">
            ต้นเงิน
        </th>
        <th colspan="3" style="font-size: 11px;">
            ดอกเบี้ย
        </th>
        <th colspan="2" style="font-size: 11px;">
            การชำระ
        </th>
        <th rowspan="2" style="font-size: 11px;">
            รวมชำระ
        </th>
        <th rowspan="2" style="font-size: 11px;">
            ต้นคงเหลือ
        </th>
    </tr>
    <tr>
        <th style="font-size: 11px;">
            ตั้งแต่
        </th>
        <th style="font-size: 11px;">
            ด/บาท ที่คิดได้
        </th>
        <th style="font-size: 11px;">
            ด/บ ค้าง
        </th>
        <th style="font-size: 11px;">
            ต้นเงิน
        </th>
        <th style="font-size: 11px;">
            ด/บ
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td width="5%" align="center">
                    <asp:CheckBox ID="operate_flag" runat="server" Style="font-size: 11px;" />
                </td>
                <td width="10%">
                    <asp:TextBox ID="loancontract_no" runat="server" Style="font-size: 11px; text-align: center;"></asp:TextBox>
                </td>
                <td width="10%">
                    <asp:TextBox ID="bfshrcont_balamt" runat="server" Style="font-size: 11px; text-align: right;"
                        ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td width="10%">
                    <asp:TextBox ID="bflastcalint_date" runat="server" Style="font-size: 11px; text-align: center;"></asp:TextBox>
                </td>
                <td width="10%">
                    <asp:TextBox ID="interest_period" runat="server" Style="font-size: 11px; text-align: right;"
                        ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td width="10%">
                    <asp:TextBox ID="bfintarr_amt" runat="server" Style="font-size: 11px; text-align: right;"
                        ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td width="10%">
                    <asp:TextBox ID="principal_payamt" runat="server" Style="font-size: 11px; text-align: right;"
                        ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td width="10%">
                    <asp:TextBox ID="interest_payamt" runat="server" Style="font-size: 11px; text-align: right;"
                        ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td width="15%">
                    <asp:TextBox ID="item_payamt" runat="server" Style="font-size: 11px; text-align: right;"
                        ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td width="10%">
                    <asp:TextBox ID="item_balance" runat="server" Style="font-size: 11px; text-align: right;"
                        ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
