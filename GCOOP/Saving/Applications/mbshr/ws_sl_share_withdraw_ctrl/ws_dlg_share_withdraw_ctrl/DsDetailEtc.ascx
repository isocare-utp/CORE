<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDetailEtc.ascx.cs" Inherits="Saving.Applications.mbshr.ws_sl_share_withdraw_ctrl.ws_dlg_share_withdraw_ctrl.DsDetailEtc" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table>
    <tr>
        <td colspan="5">
            <strong style="font-size:12px;">รายการชำระอื่นๆ </strong>
        </td>
    </tr>
</table>
<table class="DataSourceRepeater" style="width: 720px;">
    <tr>
        <th width="5%">
        </th>
        <th width="15%">
            รหัส
        </th>
        <th width="35%">
            รายละเอียด
        </th>
        <th width="20%">
            ยอดชำระ
        </th>
        <th width="5%">
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
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
                    <asp:TextBox ID="slipitem_desc" runat="server" Style="text-align: left;" ></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="item_payamt" runat="server" Style="text-align: right; background-color:#CCFFFF;"
                        ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="b_del" runat="server" Text="-" />
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>