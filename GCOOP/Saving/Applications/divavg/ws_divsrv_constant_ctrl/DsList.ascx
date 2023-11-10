<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.divavg.ws_divsrv_constant_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
    <table class="DataSourceRepeater" align="center">
        <tr>
            <th width="7%">
            </th>
            <th width="13%">
                ปีปันผล
            </th>
            <th width="35%">
                อัตราปันผล(%)
            </th>
            <th width="35%">
                อัตราเฉลี่ยคืน(%)
            </th>
            <th width="10%">
                ห้ามประมวล
            </th>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Height="500px" ScrollBars="Auto" HorizontalAlign="Center">
    <table class="DataSourceRepeater" align="center">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="7%">
                        <asp:TextBox ID="running_number" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="13%">
                        <asp:TextBox ID="div_year" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="35%">
                        <asp:TextBox ID="divpercent_rate" runat="server" Style="text-align: right;" ToolTip="#,##0.000"></asp:TextBox>
                    </td>
                    <td width="35%">
                        <asp:TextBox ID="avgpercent_rate" runat="server" Style="text-align: right;" ToolTip="#,##0.000"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:CheckBox ID="lockproc_flag" runat="server" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
