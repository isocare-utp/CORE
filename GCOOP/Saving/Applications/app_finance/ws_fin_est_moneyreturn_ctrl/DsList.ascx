<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.app_finance.ws_fin_est_moneyreturn_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
    <table class="DataSourceRepeater" style="width: 770px;">
        <tr>
            <th width="5%">
            <input type="checkbox" id="chk_all" />
            </th>
            <th width="30%">
                ทะเบียนสมาชิก
            </th>
            <th width="35%">
                รายละเอียดการทำรายการ
            </th>
            <th width="10%">
                รหัสรายการ
            </th>
            <th width="20%">
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
                    <td width="5%">
                        <asp:CheckBox ID="operate_flag" runat="server" />
                    </td>
                    <td width="30%">
                        <asp:TextBox ID="member_no" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="35%">
                        <asp:TextBox ID="description" runat="server"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="returnitemtype_code" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="return_amount" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
