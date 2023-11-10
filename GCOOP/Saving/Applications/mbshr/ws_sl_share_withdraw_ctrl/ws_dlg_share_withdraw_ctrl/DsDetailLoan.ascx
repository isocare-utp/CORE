<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDetailLoan.ascx.cs"
    Inherits="Saving.Applications.mbshr.ws_sl_share_withdraw_ctrl.ws_dlg_share_withdraw_ctrl.DsDetailLoan" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<div align="left">
    <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
        <table class="DataSourceRepeater" style="width: 720px;">
            <tr>
                <th width="5%">
                </th>
                <th width="10%">
                    สัญญา
                </th>
                <th width="5%">
                    งวด
                </th>
                <th width="10%">
                    เงินต้น
                </th>
                <th width="10%">
                    ด/บ ตั้งแต่
                </th>
                <th width="10%">
                    ดอกเบี้ยงวด
                </th>
                <th width="10%">
                    ชำระต้น
                </th>
                <th width="10%">
                    ชำระด/บ
                </th>
                <th width="10%">
                    ชำระรวม
                </th>
                <th width="10%">
                    คงเหลือ
                </th>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Left">
        <table class="DataSourceRepeater" style="width: 720px;">
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <tr>
                        <td width="5%">
                            <asp:CheckBox ID="operate_flag" runat="server" />
                        </td>
                        <td width="10%">
                            <asp:TextBox ID="loancontract_no" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td width="5%">
                            <asp:TextBox ID="period" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                        </td>
                        <td width="10%">
                            <asp:TextBox ID="bfshrcont_balamt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                                ReadOnly="true"></asp:TextBox>
                        </td>
                        <td width="10%">
                            <asp:TextBox ID="calint_from" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td width="10%">
                            <asp:TextBox ID="interest_period" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                                ReadOnly="true"></asp:TextBox>
                        </td>
                        <td width="10%">
                            <asp:TextBox ID="principal_payamt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                        </td>
                        <td width="10%">
                            <asp:TextBox ID="interest_payamt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                        </td>
                        <td width="10%">
                            <asp:TextBox ID="item_payamt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                        </td>
                        <td width="10%">
                            <asp:TextBox ID="item_balance" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                                ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </asp:Panel>
</div>
