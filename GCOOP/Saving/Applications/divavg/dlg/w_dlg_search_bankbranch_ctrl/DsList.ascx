﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.divavg.dlg.w_dlg_search_bankbranch_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<div align="left">
    <asp:Panel ID="Panel1" runat="server" Width="580px" HorizontalAlign="Left">
        <table class="DataSourceRepeater" style="width: 560px;">
            <tr>
                <th width="20%">
                    รหัสสาขา
                </th>
                <th width="80%">
                    ชื่อสาขา
                </th>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" Height="390px" ScrollBars="Auto" Width="580px"
        HorizontalAlign="Left">
        <table class="DataSourceRepeater" style="width: 560px;">
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <tr>
                        <td width="20%">
                            <asp:TextBox ID="branch_id" runat="server" ReadOnly="true" Style="cursor: pointer"></asp:TextBox>
                        </td>
                        <td width="80%">
                            <asp:TextBox ID="branch_name" runat="server" ReadOnly="true" Style="cursor: pointer"></asp:TextBox>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </asp:Panel>
    <hr width="580" align="left" />
</div>