<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsSPayto.ascx.cs" Inherits="Saving.Applications.assist.ws_as_assistpaygroup_ctrl.DsSPayto" %>


<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceFormView">
    <tr>
        <td colspan="4">
            <strong style="font-size: 13px;">จ่ายโดย</strong>
        </td>
    </tr>
</table>
<table class="DataSourceRepeater">
    <tr align="center">
        <th style="width: 20%">
            <span>ประเภทเงิน</span>
        </th>
        <th style="width: 45%">
            <span>รายละเอียด</span>
        </th>
        <th style="width: 12%">
            <span>คู่บัญชี</span>
        </th>
        <th style="width: 6%">
            <span>FEE</span>
        </th>
        <th style="width: 12%">
            <span>จำนวนเงิน</span>
        </th>
        <th style="width: 3%">
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:DropDownList ID="moneytype_code" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="description" runat="server" ReadOnly="true" Style="background-color: #CCCCCC;"
                        Width="91%"></asp:TextBox>
                    <asp:Button ID="b_searchbank" runat="server" Text="..." Width="7%" />
                </td>
                <td>
                    <asp:DropDownList ID="tofrom_accid" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="fee_amt" runat="server" Style="text-align: right; font-size: 11px;"
                        ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="itempay_amt" runat="server" Style="text-align: right; font-size: 11px;"
                        ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td align="center">
                    <asp:Button ID="b_delpayto" runat="server" Text="-" />
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
