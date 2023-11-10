<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.mbshr_const.ws_mb_mbucfmemgrp_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
    <table class="DataSourceRepeater" align="center" style="width: 700px;">
        <tr>
            <th width="15%">
                รหัสหน่วย
            </th>
            <th width="45%">
                ชื่อหน่วย
            </th>
            <th width="15%">
                หน่วยคุม
            </th>
            <th width="15%">
                ลูกหนี้ตัวแทน
            </th>
            <th width="5%">
            </th>
            <th width="5%">
            </th>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Height="500px" ScrollBars="Auto" HorizontalAlign="Center">
    <table class="DataSourceRepeater" align="center" style="width: 700px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="15%">
                        <asp:TextBox ID="membgroup_code" runat="server"></asp:TextBox>
                    </td>
                    <td width="45%">
                        <asp:TextBox ID="membgroup_desc" runat="server"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="membgroup_control" runat="server"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="membgroup_agentgrg" runat="server"></asp:TextBox>
                    </td>
                    <td width="5%">
                        <asp:Button ID="b_delete" runat="server" Text="ลบ" />
                    </td>
                    <td width="5%">
                        <asp:Button ID="b_detail" runat="server" Text="..." />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
