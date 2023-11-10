<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsLoan.ascx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_member_detail_max_ctrl.DsLoan" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<span style="font-size: 13px;"><font color="#cc0000"><u><strong>เงินกู้</strong></u></font></span>
<%--<table class="DataSourceFormView" style="width: 710px;">
    <tr>
        <td>
            <asp:CheckBox ID="check_loan" runat="server" />สัญญาปัจจุบัน
        </td>
        
    </tr>
</table>--%>
<table class="TbStyle">
    <tr>
        <th width="4%">
        </th>
        <th width="12%">
            เลขที่สัญญา
        </th>
        <th width="15%">
            ยอดอนุมัติ
        </th>
        <th width="10%">
            เริ่มสัญญา
        </th>
        <th width="13%">
            ชำระ/งวด
        </th>
        <th width="9%">
            งวดส่ง
        </th>
        <th width="10%">
            ชำระล่าสุด
        </th>
        <th width="15%">
            คงเหลือ
        </th>
        <th width="8%">
            สถานะ
        </th>
        <th width="4%">
        </th>
    </tr>
</table>
<asp:Panel ID="Panel2" runat="server" Height="200px" ScrollBars="Auto">
    <table class="TbStyle">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="4%">
                        <asp:TextBox ID="loantype_code" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="12%">
                        <asp:TextBox ID="loancontract_no" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="loanapprove_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="startcont_date" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="13%">
                        <asp:TextBox ID="period_payment" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="9%">
                        <asp:TextBox ID="cp_period" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="lastpayment_date" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="principal_balance" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="8%">
                        <asp:TextBox ID="cp_contract_status" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="4%">
                        <asp:Button ID="bloan_detail" runat="server" Text=".."/>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td Style="text-align:right">ยอดรวม :</td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
    </table>
</asp:Panel>

