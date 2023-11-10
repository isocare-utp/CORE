<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsCollwho.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_collateral_check_ctrl.DsCollwho" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table>
    <tr>
        <td>
            <strong style="font-size: 14px;">รายการค้ำประกันสัญญา</strong>
        </td>
    </tr>
</table>
<table class="DataSourceRepeater" style="width: 500px;">
    <tr>
        <th>
            เลขที่สัญญา
        </th>
        <th>
            ชื่อ-ชื่อสกุล
        </th>
        <th>
            จำนวนเงินกู้
        </th>
        <th>
            % การค้ำ
        </th>
        <th>
            เงินที่ใช้ค้ำ
        </th>
    </tr>
    <asp:Repeater ID="Repeater3" runat="server">
        <ItemTemplate>
            <tr>
                <td width="20%">
                    <asp:TextBox ID="prefix" runat="server" Style="width: 20px; font-size: 11px;"></asp:TextBox>
                    <asp:TextBox ID="loancontract_no" runat="server" Style="width: 65px; font-size: 11px;"></asp:TextBox>
                </td>
                <td width="35%">
                    <asp:TextBox ID="cp_name" runat="server" Style="font-size: 11px;"></asp:TextBox>
                </td>
                <td width="17%">
                    <asp:TextBox ID="loanapprove_amt" runat="server" Style="font-size: 11px; text-align: right;"
                        ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td width="12%">
                    <asp:TextBox ID="collactive_percent" runat="server" Style="font-size: 11px; text-align: right;"
                        ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td width="17%">
                    <asp:TextBox ID="collactive_amt" runat="server" Style="font-size: 11px; text-align: right;"
                        ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
<table class="DataSourceFormView" style="width: 500px;">
    <tr>
        <td style="text-align:right;">
            <strong>รวม:</strong>
        </td>
        <td width="16%">
            <asp:TextBox ID="cp_sumcp_colluse" runat="server" Style="font-size: 11px; text-align: right;
                font-weight: bold;"></asp:TextBox>
        </td>
    </tr>
</table>
