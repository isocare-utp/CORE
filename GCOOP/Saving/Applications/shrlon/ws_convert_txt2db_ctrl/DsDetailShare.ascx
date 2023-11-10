<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDetailShare.ascx.cs" Inherits="Saving.Applications.shrlon.ws_convert_txt2db_ctrl.DsDetailShare" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<style type="text/css">
    .style3
    {
        width: 38%;
    }
</style>
<table style="width: 726px">
    <tr>
        <td colspan="7" class="style3">
            <strong style="font-size: 16px;"><u>รายละเอียดรายการที่รับชำระ </u></strong>
        </td>
    </tr>
    <tr>
        <td colspan="7" class="style3">
            <strong><span style="font-size: 12px;">รายการหุ้น</span> </strong>
        </td>
    </tr>
</table>
<table class="DataSourceRepeater" style="width: 770px;">
    <tr>
        <th width="5%">
        </th>
        <th width="20%" style="font-size: 12px;">
            รายละเอียด
        </th>
        <th width="10%" colspan="2" style="font-size: 12px;">
            งวด
        </th>
        <%-- <th width="15%" style="font-size: 12px;">
            วันที่ลงรายการ
        </th>--%>
        <th width="20%" style="font-size: 12px;">
            หุ้นสะสม(ก่อนทำรายการ)
        </th>
        <th width="20%" style="font-size: 12px;">
            ยอดซื้อหุ้น
        </th>
        <th width="20%" style="font-size: 12px;">
            หุ้นสะสม
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td align="center">
                    <asp:CheckBox ID="operate_flag" runat="server" />
                </td>
                <td>
                    <asp:TextBox ID="slipitem" runat="server" Style="text-align: left; font-size: 11px;
                        background-color: #EEEEEE;" ReadOnly="true"></asp:TextBox>
                </td>
                <td align="left">
                    <asp:CheckBox ID="periodcount_flag" runat="server" Style="width: 20px; text-align: center;
                        background-color: #EEEEEE;" />
                </td>
                <td>
                    <asp:TextBox ID="period" runat="server" Style="width: 55px; text-align: center; font-size: 11px;
                        background-color: #EEEEEE;"></asp:TextBox>
                </td>
                <%-- <td>
                    <asp:TextBox ID="calint_to" runat="server" Style="text-align: center; font-size: 11px; background-color:#CCFF99;"></asp:TextBox>
                </td>--%>
                <td>
                    <asp:TextBox ID="bfshrcont_balamt" runat="server" Style="text-align: right; font-size: 11px;
                        background-color: #EEEEEE;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="item_payamt" runat="server" Style="text-align: right; font-size: 11px;
                        background-color: #CCFF99;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="item_balance" runat="server" Style="text-align: right; font-size: 11px;
                        background-color: #EEEEEE;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
