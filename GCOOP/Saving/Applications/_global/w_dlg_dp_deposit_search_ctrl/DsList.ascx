<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications._global.w_dlg_dp_deposit_search_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<div align="left">
    <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left" Width="666px">
        <table class="DataSourceRepeater" style="width: 650px;">
            <tr>
                <th width="15%">
                    เลขที่บัญชี
                </th>
                <th width="20%">
                    ชื่อบัญชี
                </th>
                <th width="15%">
                    เลขสมาชิก
                </th>
                <th width="20%">
                    ขื่อ - สกุล
                </th>
                <th width="15%">
                    คงเหลือ
                </th>
                <th width="15%">
                    สาขา
                </th>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" Height="262px" ScrollBars="Auto" HorizontalAlign="Left"
        Width="670px">
        <table class="DataSourceRepeater" style="width: 650px;">
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <tr>
                        <td width="15%">
                            <asp:TextBox ID="deptaccount_no" runat="server" ReadOnly="true" Style="cursor: pointer"></asp:TextBox>
                        </td>
                        <td width="20%">
                            <asp:TextBox ID="deptaccount_name" runat="server" ReadOnly="true" Style="cursor: pointer"></asp:TextBox>
                        </td>
                        <td width="15%">
                            <asp:TextBox ID="member_no" runat="server" ReadOnly="true" Style="cursor: pointer"></asp:TextBox>
                        </td>
                        <td width="20%">
                            <asp:TextBox ID="member_name" runat="server" ReadOnly="true" Style="cursor: pointer"></asp:TextBox>
                        </td>
                        <td width="15%">
                            <asp:TextBox ID="PRNCBAL" runat="server" ReadOnly="true" Style="cursor: pointer;
                                text-align: right" ToolTip="#,##0.00"></asp:TextBox>
                        </td>
                        <td width="15%">
                            <asp:TextBox ID="coop_id" runat="server" ReadOnly="true" Style="cursor: pointer"></asp:TextBox>
                        </td>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </asp:Panel>
</div>
