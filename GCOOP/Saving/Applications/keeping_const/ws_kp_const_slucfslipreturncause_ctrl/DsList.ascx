﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.keeping_const.ws_kp_const_slucfslipreturncause_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />

    <table class="DataSourceRepeater">
    <tr>
        <th width="10%">
            รหัส
        </th>
        <th>
            คำอธิบาย
        </th>        
        <th width="4%">
            ลบ
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="slipretcause_code" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="slipretcause_desc" runat="server"></asp:TextBox>
                </td>               
                <td>
                    <asp:Button ID="b_del" runat="server" Text="ลบ" />
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>