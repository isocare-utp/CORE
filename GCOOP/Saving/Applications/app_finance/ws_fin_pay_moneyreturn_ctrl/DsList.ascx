<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.app_finance.ws_fin_pay_moneyreturn_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
    <table class="DataSourceRepeater" style="width: 770px;">
        <tr>
            <th width="3%">
            </th>
            <th width="5%">
                ระบบ
            </th>
            <th width="10%">
                ทะเบียนสมาชิก
            </th>
            <th width="25%">
                ชื่อ-สกุล
            </th>
            <th width="6%">
                รายการ
            </th>
            <th width="6%">
                โดย
            </th>
            <th width="25%" colspan="2">
                ธนาคาร
            </th>
            <th width="10%">
                เลข บ/ช
            </th>
            <th width="10%">
                จำนวนเงิน
            </th>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Height="770px" ScrollBars="Auto" HorizontalAlign="Center">
    <table class="DataSourceRepeater" style="width: 770px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="3%">
                        <asp:CheckBox ID="operate_flag" runat="server" />
                    </td>
                    <td width="5%">
                        <asp:TextBox ID="system_from" runat="server" ReadOnly="true" style="text-align:center"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="member_no" runat="server" ReadOnly="true" style="text-align:center"></asp:TextBox>
                    </td>
                    <td width="25%">
                        <asp:TextBox ID="full_name" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="6%">
                        <asp:TextBox ID="returnitemtype_code" runat="server" ReadOnly="true" style="text-align:center"></asp:TextBox>
                    </td>
                    <td width="6%">
                        <asp:TextBox ID="moneytype_code" runat="server" style="text-align:center"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:DropDownList ID="bank_code" runat="server" Enabled=false>
                        </asp:DropDownList>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="branch_name" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="bank_accid" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="return_amount" runat="server" ReadOnly="true" style="text-align:right" ToolTip="#,#0.00"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
