<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.app_finance.ws_fin_pay_mnrt_person_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater" style="width: 770px;">
    <tr>
        <th>
        </th>
        <th>
            รายละเอียดการทำรายการ
        </th>
        <th>
            เงินต้น
        </th>
        <th>
            ดอกเบี้ย
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td style="text-align:center;">
                    <asp:CheckBox ID="operate_flag" runat="server"  />
                </td>
                <td>
                    <asp:TextBox ID="slipitem_desc" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="principal_payamt" runat="server" ReadOnly="true" ToolTip="#,##0.00" style="text-align:right;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="interest_payamt" runat="server" ReadOnly="true" ToolTip="#,##0.00" style="text-align:right;"></asp:TextBox>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
