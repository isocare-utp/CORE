<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMemcoll.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_collateral_check_ctrl.DsMemcoll" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater" style="width: 270px;">
    <tr>
        <th>
            กลุ่มการค้ำประกัน
        </th>
        <th>
            สิทธิการค้ำ
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td width="60%">
                    <asp:TextBox ID="mangrtpermgrp_code" runat="server" Style="width: 20px; text-align: center;"></asp:TextBox>&nbsp;
                    <asp:TextBox ID="mangrtpermgrp_desc" runat="server" Style="width: 120px"></asp:TextBox>
                </td>
                <td width="40%">
                    <asp:TextBox ID="coll_amt" runat="server" ToolTip="#,##0.00" Style="text-align: right;"></asp:TextBox>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
