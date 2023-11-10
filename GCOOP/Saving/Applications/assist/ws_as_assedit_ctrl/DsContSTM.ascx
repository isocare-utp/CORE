<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsContSTM.ascx.cs" Inherits="Saving.Applications.assist.ws_as_assedit_ctrl.DsContSTM" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <tr>
        <th style="width: 4%">
        </th>
        <th style="width: 12%">
            วันที่ใบสำคัญ
        </th>
        <th style="width: 12%">
            วันที่รายการ
        </th>
        <th style="width: 21%">
            รายการ
        </th>
        <th style="width: 15%">
            อ้างอิง
        </th>
        <th style="width: 7%">
            งวด
        </th>
        <th style="width: 12%">
            จำนวนเงิน
        </th>
        <th style="width: 12%">
            ยอดรับแล้ว
        </th>
        <th style="width: 5%">
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="seq_no" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="slip_date" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="operate_date" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <asp:DropDownList ID="item_code" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="ref_slipno" runat="server" Style="text-align: left;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="period" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="pay_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="pay_balance" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="b_del" runat="server" Text="ลบ" />
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
