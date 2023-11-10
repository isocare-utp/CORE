<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsOtherkeep.ascx.cs"
    Inherits="Saving.Applications.mbshr.ws_mbshr_member_detail_ctrl.DsOtherkeep" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<span style="font-size: 13px;"><font color="#cc0000"><u><strong>รายการหักอื่นๆ</strong></u></font></span>
<table class="TbStyle">
    <tr>
        <th width="6%">
            ลำดับ
        </th>
        <th width="67%">
            รายละเอียดการหักรายเดือน
        </th>
        <th width="27%">
            จำนวนเงินหัก
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="running_number" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="keepothitemtype_desc" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="item_payment" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                        ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
<table class="TbStyle">
    <tr>
        <td width="6%" style="border-style: none">
        </td>
        <td style="text-align: right;border-style: none" width="67%" >
            <strong>รวม:</strong>
        </td>
        <td width="27%" style="border-top-style: none">
            <asp:TextBox ID="cp_sum_item_payment" runat="server" Style="font-size: 12px; text-align: right;
                font-weight: bold;" ReadOnly="true"></asp:TextBox>
        </td>
    </tr>
</table>
