<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsStatement.ascx.cs"
    Inherits="Saving.Applications.mbshr.ws_mbshr_member_detail_max_ctrl.w_dlg_sl_detail_contract_ctrl.DsStatement" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
    <asp:Panel ID="Panel2" runat="server" Height="425px" ScrollBars="Auto" HorizontalAlign="Center">
<table class="TbStyle" style="width: 1300px;">
    <tr>
        <th rowspan="2" width="2%">
            
        </th>
        <th rowspan="2" width="6%">
            วันทำรายการ
        </th>
        <th rowspan="2" width="6%">
            วันที่ใบเสร็จ
        </th>
        <th rowspan="2" width="6%">
            เลขที่ใบเสร็จ
        </th>
        <th rowspan="2" width="4%">
            รายการ
        </th>
        <th rowspan="2" width="3%">
            งวด
        </th>
        <th colspan="2" width="14%">
            CR
        </th>
        <th colspan="2" width="14%">
            DR
        </th>
        <th rowspan="2" width="6%">
            คงเหลือ
        </th>
        <th rowspan="2" width="6%">
            ด/บ ตั้งแต่
        </th>
        <th rowspan="2" width="6%">
            ด/บ ถึง
        </th>
        <th rowspan="2" width="6%">
            ด/บ งวด
        </th>
        <th rowspan="2" width="6%">
            ด/บ ค้าง
        </th>
        <th rowspan="2" width="6%">
            ด/บ คืน
        </th>
        <th rowspan="2" width="4%">
            ประเภทเงิน
        </th>
        <th rowspan="2" width="5%">
            หมายเหตุ
        </th>
    </tr>
    <tr>
        <th width="7%">
            เงินต้น
        </th>
        <th width="7%">
            ดอกเบี้ย
        </th>
        <th width="7%">
            เงินต้น
        </th>
        <th width="7%">
            ดอกเบี้ย
        </th>
    </tr>
</table>

    <table class="TbStyle" style="width: 1300px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="2%">
                        <asp:TextBox ID="running_number" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="6%">
                        <asp:TextBox ID="operate_date" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="6%">
                        <asp:TextBox ID="slip_date" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="6%">
                        <asp:TextBox ID="ref_docno" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="4%">
                        <asp:TextBox ID="loanitemtype_code" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="3%">
                        <asp:TextBox ID="period" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="7%">
                        <asp:TextBox ID="cp_principal_cr" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="7%">
                        <asp:TextBox ID="cp_interest_cr" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="7%">
                        <asp:TextBox ID="cp_principal_dr" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="7%">
                        <asp:TextBox ID="cp_interest_dr" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="6%">
                        <asp:TextBox ID="principal_balance" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="6%">
                        <asp:TextBox ID="calint_from" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="6%">
                        <asp:TextBox ID="calint_to" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="6%">
                        <asp:TextBox ID="interest_period" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="6%">
                        <asp:TextBox ID="interest_arrear" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="6%">
                        <asp:TextBox ID="interest_return" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="4%">
                        <asp:TextBox ID="moneytype_code" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="5%">
                        <asp:TextBox ID="entry_id" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
