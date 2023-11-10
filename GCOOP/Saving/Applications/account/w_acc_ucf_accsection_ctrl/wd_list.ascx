﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wd_list.ascx.cs" Inherits="Saving.Applications.account.w_acc_ucf_accsection_ctrl.wd_list" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
    <table class="DataSourceRepeater" align="center" style="width: 700px;">
        <tr>
            <th width="20%">
                ชื่อย่อระบบ
            </th>
            <th>
                ชื่อระบบ
            </th>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Height="500px" ScrollBars="Auto" HorizontalAlign="Center">
    <table class="DataSourceRepeater" align="center" style="width: 700px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="20%">
                        <asp:TextBox ID="section_id" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="section_desc" runat="server"></asp:TextBox>
                    </td>
                    <td width="5%">
                        <asp:Button ID="b_delete" runat="server" Text="ลบ" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
