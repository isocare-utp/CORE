<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.CriteriaIReport.u_cri_period_rgrp_memtype_memno_slipno.DsList" %>
<link id="css1" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater" style="width: 350px">
    <tr>
        <th colspan="2">
            ประเภทสมาชิก
        </th>
    </tr>
</table>
<asp:Panel ID="Panel2" runat="server" Height="210px" Width="350px" ScrollBars="Auto">
    <table class="DataSourceRepeater" style="width: 330px">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="8%" align="center">
                        <asp:CheckBox ID="operate_flag" runat="server" />
                    </td>
                    <td width="92%">
                        <asp:TextBox ID="cp_membtype" runat="server" style="width: 200px"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
