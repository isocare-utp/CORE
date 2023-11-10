<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsColluse.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_collateral_master_detail_ctrl.DsColluse" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater" style="width: 700px;">
    <tr>
        <th>
        </th>
        <th>
            เลขสัญญา
        </th>
        <th>
            ชื่อ-ชื่อสกุล
        </th>
        <th>
            เงินกู้คงเหลือ
        </th>
        <th>
            % การค้ำ
        </th>
        <th>
            เงินที่ใช้ค้ำ
        </th>
    </tr>
    <asp:Repeater ID="Repeater2" runat="server">
        <ItemTemplate>
            <tr>
                <td width="5%">
                    <asp:TextBox ID="prefix" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td width="20%">
                    <asp:TextBox ID="loancontract_no" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td width="30%">
                    <asp:TextBox ID="cp_name" runat="server"></asp:TextBox>
                </td>
                <td width="15%">
                    <asp:TextBox ID="principal_balance" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td width="15%">
                    <asp:TextBox ID="coll_percent" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td width="15%">
                    <asp:TextBox ID="cp_colluse" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
