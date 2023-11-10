﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_collateral_master_new_ctrl.ws_dlg_sl_editcollateral_master_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table>
    <tr>
        <td colspan="5">
            <strong style="font-size: 14px;">รายการหลักทรัพย์ </strong>
        </td>
    </tr>
</table>
<table class="DataSourceRepeater" style="width: 600px;">
    <tr>
        <th width="50%">
            เลขที่หลักทรัพย์
        </th>
        <th width="50%">
            จดจำนอง
        </th>
    </tr>
    <asp:Repeater ID="Repeater3" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="collmast_refno" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="cp_redeem" runat="server" ToolTip="#,##0.00" Style="text-align: right;"></asp:TextBox>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
