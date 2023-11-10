<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.ap_deposit.w_sheet_dp_const_dpucfdeptaccgroup_ctrl.DsList" %>
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
        <th width="15%">
            ลำดับ
        </th>
        <th width="5%">
            ลบ!
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="depttype_accgrp" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="depttype_accdesc" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="sort_order" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="b_del" runat="server" Text="ลบ" />
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
