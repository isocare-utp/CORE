<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsCollwholnreq.ascx.cs"
    Inherits="Saving.Applications.shrlon.ws_sl_collateral_check_ctrl.DsCollwholnreq" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table>
    <tr>
        <td>
            <strong>รายการค้ำประกันใบคำขอ</strong>
        </td>
    </tr>
</table>
<table class="DataSourceRepeater" style="width: 500px;">
    <tr>
        <th>
            เลขที่ใบคำขอ
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
                <td width="18%">
                    <asp:TextBox ID="loanrequest_docno" runat="server" Style="font-size: 11px;text-align: center;"></asp:TextBox>
                </td>
                <td width="32%">
                    <asp:TextBox ID="full_name" runat="server" Style="font-size: 11px;"></asp:TextBox>
                </td>
                <td width="20%">
                    <asp:TextBox ID="loanrequest_amt" runat="server" Style="font-size: 11px; text-align: right;"
                        ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td width="10%">
                    <asp:TextBox ID="collactive_percent" runat="server" Style="font-size: 11px; text-align: right;"></asp:TextBox>
                </td>
                <td width="20%">
                    <asp:TextBox ID="cp_collamt" runat="server" Style="font-size: 11px; text-align: right;"
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
            <asp:TextBox ID="cp_sumcp_collamt" runat="server" Style="font-size: 11px; text-align: right;
                font-weight: bold;"></asp:TextBox>
        </td>
    </tr>
</table>
