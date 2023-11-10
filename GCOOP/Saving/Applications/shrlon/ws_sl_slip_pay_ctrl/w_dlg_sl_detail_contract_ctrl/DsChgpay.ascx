<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsChgpay.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_slip_pay_ctrl.w_dlg_sl_detail_contract_ctrl.DsChgpay" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel2" runat="server" Height="425px" ScrollBars="Auto" HorizontalAlign="Center">
    <table class="TbStyle" style="width: 820px;" horizontalalign="Center">
        <tr>
            <th width="15%" rowspan="2">
                เลขที่เอกสาร
            </th>
            <th width="15%" rowspan="2">
                วันที่เปลี่ยน
            </th>
            <th width="30%" colspan="3">
                การส่งเดิม
            </th>
            <th width="30%" colspan="3">
                การส่งใหม่
            </th>
            <th width="10%" rowspan="2">
                ผู้ทำรายการ
            </th>
        </tr>
        <tr>
            <th width="10%">
                รูปแบบ
            </th>
            <th width="10%">
                ชำระ/งวด
            </th>
            <th width="10%">
                สถานะ
            </th>
            <th width="10%">
                รูปแบบ
            </th>
            <th width="10%">
                ชำระ/งวด
            </th>
            <th width="10%">
                สถานะ
            </th>
        </tr>
    </table>
    <table class="TbStyle" style="width: 820px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="15%">
                        <asp:TextBox ID="contadjust_docno" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="contadjust_date" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="oldpayment_type" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="oldperiod_payment" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="oldpayment_status" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="loanpayment_type" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="period_payment" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="payment_status" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="entry_id" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
