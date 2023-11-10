<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsWrt.ascx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_member_detail_main_ctrl.DsWrt" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<span style="font-size: 13px;"><font color="#cc0000"><u><strong>รายการหุ้น</strong></u></font></span>
<table class="TbStyle">
    <tr>
        <th width="3%">
        </th>
        <th width="12%">
            วันที่
        </th>
        <th width="8%">
            รายการ
        </th>
        <th width="10%">
            จำนวน
        </th>
        <th width="10%">
            คงเหลือ
        </th>
        <th width="14%">
            อ้างอิงสัญญา
        </th>
        <th width="9%">
            ทำรายการโดย
        </th>
        <th width="17%">
            ผู้ทำรายการ
        </th>
        <th width="12%">
            วันที่ทำรายการ
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="running_number" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="operate_date" runat="server" ReadOnly="true" style="text-align:center;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="wrtitemtype_code" runat="server" ReadOnly="true" style="text-align:center;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="wrtfund_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                        ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="wrtfund_balance" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                        ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="ref_contno" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="moneytype_code" runat="server" ReadOnly="true" style="text-align:center;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="entry_id" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="entry_date" runat="server" ReadOnly="true" style="text-align:center;"></asp:TextBox>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
