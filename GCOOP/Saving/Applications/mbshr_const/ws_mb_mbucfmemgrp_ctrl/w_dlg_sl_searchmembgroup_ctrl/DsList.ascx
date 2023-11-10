<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.mbshr_const.ws_mb_ucfmemgrp_ctrl.w_dlg_sl_searchmembgroup_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater" style="width: 600px;">
    <tr>
        <th>
            รหัสหน่วย
        </th>
        <th>
            ชื่อหน่วย
        </th>
        <th>
            หน่วยคุม
        </th>
        <th>
            ลูกหนี้ตัวแทน
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td width="20%">
                    <asp:TextBox ID="membgroup_code" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td width="40%">
                    <asp:TextBox ID="membgroup_desc" runat="server"></asp:TextBox>
                </td>
                <td width="20%">
                    <asp:TextBox ID="membgroup_control" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td width="20%">
                    <asp:TextBox ID="membgroup_agentgrg" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
