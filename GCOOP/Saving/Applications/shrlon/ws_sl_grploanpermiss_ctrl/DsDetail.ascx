<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDetail.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_grploanpermiss_ctrl.DsDetail" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <tr>
    <th width="4%">ที่</th>
        <th width="8%">
            รหัส
        </th>
        <th width="64%">
            กลุ่มวงเงินกู้
        </th>
        <th width="20%">
            วงเงินกู้สูงสุด
        </th>
        <th width="4%">
        </th>
    </tr>
</table>
<asp:Panel ID="Panel1" runat="server" Height="500px" Width="750px" ScrollBars="Auto">
    <table class="DataSourceRepeater">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                <td width="4%">
                    <asp:TextBox ID="running_number" runat="server" Style="text-align: center"></asp:TextBox></td>
                    <td width="8%">
                        <asp:TextBox ID="loanpermgrp_code" runat="server" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td width="64%">
                        <asp:TextBox ID="loanpermgrp_desc" runat="server"></asp:TextBox>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="maxpermiss_amt" runat="server" ToolTip="#,##0.00" Style="text-align: right"></asp:TextBox>
                    </td>
                    <td width="4%">
                        <asp:Button ID="b_del" runat="server" Text="-" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
