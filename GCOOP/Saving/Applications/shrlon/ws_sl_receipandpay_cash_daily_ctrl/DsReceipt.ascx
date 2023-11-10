<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsReceipt.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_receipandpay_cash_daily_ctrl.DsReceipt" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater" style="width: 375px;">
    <tr>
        <th>
            ประเภท
        </th>
        <th>
            จำนวน
        </th>
        <th>
            เงินสด
        </th>
        <th>
            ดอกเบี้ย
        </th>
    </tr>
    <asp:Repeater ID="Repeater2" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="loantype_desc" runat="server" BackColor="#FFFFCC"></asp:TextBox>
                </td>
                <td width="10%">
                    <asp:TextBox ID="count_item" runat="server" Style="text-align: center;" ToolTip="#,##0" BackColor="#FFFFCC"></asp:TextBox>
                </td>
                <td width="25%">
                    <asp:TextBox ID="sum_prnamt" runat="server" Style="text-align: right;" ToolTip="#,##0.00" BackColor="#FFFFCC"></asp:TextBox>
                </td>
                <td width="25%">
                    <asp:TextBox ID="sum_intamt" runat="server" Style="text-align: right;" ToolTip="#,##0.00" BackColor="#FFFFCC"></asp:TextBox>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
<table class="DataSourceFormView" style="width: 375px;">
    <tr>
        <td align="right">
            <div>
                <strong>รวม:</strong>
            </div>
        </td>
        <td width="10%">
            <asp:TextBox ID="cp_sum_count_item" runat="server" Style="text-align: center;" 
                ToolTip="#,##0" BackColor="#FFFF99" Font-Bold="True"></asp:TextBox>
        </td>
        <td width="25%">
            <asp:TextBox ID="cp_sum_allreceiptprnc" runat="server" Style="text-align: right;"
                ToolTip="#,##0.00" BackColor="#FFFF99" Font-Bold="True"></asp:TextBox>
        </td>
        <td width="25%">
            <asp:TextBox ID="cp_sum_allreceiptint" runat="server" Style="text-align: right;"
                ToolTip="#,##0.00" BackColor="#FFFF99" Font-Bold="True"></asp:TextBox>
        </td>
    </tr>
</table>
