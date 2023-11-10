<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDetailEtc.ascx.cs" Inherits="Saving.Applications.shrlon.ws_convert_txt2db_ctrl.DsDetailEtc" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table>
    <tr>
        <td colspan="5">
            <strong style="font-size:12px;">รายการชำระอื่นๆ </strong>
        </td>
    </tr>
</table>
<table class="DataSourceRepeater" style="width: 770px;">
    <tr>
        <th width="5%">
        </th>
        <th width="15%" style="font-size: 12px;">
            รหัส
        </th>
        <th width="35%" style="font-size: 12px;">
            รายละเอียด
        </th>
        <th width="20%" style="font-size: 12px;">
            เงินชำระ
        </th>
        <th width="20%" style="font-size: 12px;">
            คงเหลือ
        </th>
        <th width="5%">
        </th>
    </tr>
    <asp:Repeater ID="Repeater3" runat="server">
        <ItemTemplate>
            <tr>
                <td align="center">
                    <asp:CheckBox ID="operate_flag" runat="server" />
                </td>
                <td>
                    <asp:DropDownList ID="slipitemtype_code" runat="server">
                    </asp:DropDownList>
                   
                </td>
                <td>
                    <asp:TextBox ID="slipitem_desc" runat="server" Style="text-align: left; font-size: 11px;" ></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="item_payamt" runat="server" Style="text-align: right; font-size: 11px; background-color:#CCFFFF;"
                        ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="cp_balance" runat="server" Style="text-align: right; font-size: 11px;"
                        ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="b_del" runat="server" Text="-" />
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
