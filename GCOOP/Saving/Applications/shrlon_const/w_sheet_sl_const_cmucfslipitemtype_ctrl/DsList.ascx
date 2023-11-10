<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.shrlon_const.w_sheet_sl_const_cmucfslipitemtype_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <tr>
        <th width="10%">
            รหัส
        </th>
        <th>
            ชื่อประเภทการทำรายการประเภทหุ้น-หนี้
        </th>
        <th width="14%">
            VC Group
        </th>
        <th width="8%">
           เลือกตอนขอกู้
        </th>
        <th width="8%">
            เลือกตอนชำระ
        </th>
        <th width="8%">
            ลบ
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="SLIPITEMTYPE_CODE" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="SLIPITEMTYPE_DESC" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="SLIPITEMTYPE_VCGRP" runat="server"></asp:TextBox>
                </td>
                <td align="center">
                    <asp:CheckBox ID="ITEMLNREQCLR_FLAG" runat="server" />
                </td>
                <td align="center">
                    <asp:CheckBox ID="ITEMSLIPETC_FLAG" runat="server" />
                </td>
                <td >
                    <asp:Button ID="b_del" runat="server" Text="ลบ" />
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
