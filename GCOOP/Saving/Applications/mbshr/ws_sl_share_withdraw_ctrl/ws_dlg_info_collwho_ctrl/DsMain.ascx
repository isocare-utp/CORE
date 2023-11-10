<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.mbshr.ws_sl_share_withdraw_ctrl.ws_dlg_info_collwho_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<div align="left">
    <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
        <table class="DataSourceRepeater" style="width: 680px;">
            <tr>
                <th width="4%">
                </th>
                <th width="10%">
                    เลขสัญญาผู้กู้
                </th>
                <th width="10%">
                    ทะเบียนผู้กู้
                </th>
                <th width="20%">
                    ชื่อ - สกุลผู้กู้
                </th>
                <th width="12%">
                    หุ้นสะสมผู้กู้
                </th>
                <th width="10%">
                    คงเหลือ/รอเบิก
                </th>
                <th width="7%">
                    % การค้ำ
                </th>
                <th width="10%">
                    เงินค้ำประกัน
                </th>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Left">
        <table class="DataSourceRepeater" style="width: 680px;">
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <tr>
                        <td width="4%">
                            <asp:TextBox ID="running_number" runat="server" Style="text-align: center;" ReadOnly="true" />
                        </td>
                        <td width="10%">
                            <asp:TextBox ID="loancontract_no" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td width="10%">
                            <asp:TextBox ID="member_no" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                        </td>
                        <td width="20%">
                            <asp:TextBox ID="cp_name" runat="server" Style="text-align: left;" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td width="12%">
                            <asp:TextBox ID="cp_sharestkamt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                                ReadOnly="true"></asp:TextBox>
                        </td>
                        <td width="10%">
                            <asp:TextBox ID="cp_prinbalwithamt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                                ReadOnly="true"></asp:TextBox>
                        </td>
                        <td width="7%">
                            <asp:TextBox ID="coll_percent" runat="server" Style="text-align: right;" ToolTip="#,##0.00%"
                                ReadOnly="true"></asp:TextBox>
                        </td>
                        <td width="10%">
                            <asp:TextBox ID="cp_collamt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                                ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </asp:Panel>
</div>
