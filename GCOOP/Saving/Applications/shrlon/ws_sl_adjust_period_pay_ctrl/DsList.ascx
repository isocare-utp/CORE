<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_adjust_period_pay_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" Height="450px" ScrollBars="Auto" HorizontalAlign="Left">
    <table class="DataSourceRepeater">
        <tr>
            <th width="9%" rowspan="2">
                สมาชิก
            </th>
            <th width="22%" rowspan="2">
                ชื่อ - สกุล
            </th>
            <th width="11%" rowspan="2">
                สัญญา
            </th>
            <th width="11%" rowspan="2">
                ยอดอนุมัติ
            </th>
            <th width="11%" rowspan="2">
                คงเหลือ
            </th>
            <th width="6%" rowspan="2">
                งวด
            </th>
            <th colspan="2">
                งวดชำระ
            </th>
            <th width="6%" rowspan="2">
                ส่วนต่าง
            </th>
            <th width="6%" rowspan="2">
                ด/บ
            </th>
            <th width="4%" rowspan="2">
            </th>
        </tr>
        <tr>
            <th width="6%">
                เก่า
            </th>
            <th width="6%">
                ใหม่
            </th>
        </tr>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:TextBox ID="member_no" runat="server" Style="text-align: center" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="cp_name" runat="server" Style="text-align: left" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="loancontract_no" runat="server" Style="text-align: center" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="loanapprove_amt" runat="server" Style="text-align: right" ToolTip="#,##0.00"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="bfprnbal_amt" runat="server" Style="text-align: right" ToolTip="#,##0.00"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="bfperiod" runat="server" Style="text-align: center" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="oldperiod_payamt" runat="server" Style="text-align: center" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="period_payamt" runat="server" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="cp_div" runat="server" Style="text-align: right" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="INT_CONTINTRATE" runat="server" Style="text-align: right" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="b_del" runat="server" Text="ลบ" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>

