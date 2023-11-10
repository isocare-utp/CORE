﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.shrlon.dlg.w_dlg_sl_collredeem_search_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater" style="width: 610px;">
    <tr>
        <th width="16%">
            ทะเบียน ลท
        </th>
        <th width="10%">
            ประเภท
        </th>
        <th>
            รายละเอียด
        </th>
        <%-- <th width="16%">
            ราคาจริง
        </th>--%>
        <th width="16%">
            ราคาที่ใช้ค้ำ
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="collmast_no" runat="server" ReadOnly="true" Style="text-align: center;
                        cursor: pointer;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="collmasttype_code" runat="server" ReadOnly="true" Style="text-align: center;
                        cursor: pointer;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="collmast_desc" runat="server" ReadOnly="true" Style="cursor: pointer;"></asp:TextBox>
                </td>
                <%--  <td>
                    <asp:TextBox ID="collreal_price" runat="server" ToolTip="#,##0.00" ReadOnly="true"
                        Style="text-align: right; cursor: pointer;"></asp:TextBox>
                </td>--%>
                <td>
                    <asp:TextBox ID="collmast_price" runat="server" ToolTip="#,##0.00" ReadOnly="true"
                        Style="text-align: right; cursor: pointer;"></asp:TextBox>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
