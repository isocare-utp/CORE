<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.mbshr.ws_sl_share_withdraw_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
    <table class="DataSourceRepeater">
        <tr>
            <th width="4%">
            </th>
            <th width="5%">
                หุ้น
            </th>
            <th width="10%">
               ทะเบียน
            </th>
            <th width="30%">
                ชื่อ-ชื่อสกุล
            </th>
            <th width="10%">
                หน่วย
            </th>
            <th width="12%">
               วันที่ลาออก
            </th>
            <th width="14%">
                ยอดรอถอน
            </th>
            <th width="12%">
                สถานะ
            </th>
            <th width="5%">
            </th>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Height="700px" ScrollBars="Auto" HorizontalAlign="Center">
    <table class="DataSourceRepeater">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td align="center" width="4%">
                        <asp:CheckBox ID="operate_flag" runat="server" />
                    </td>
                    <td width="5%">
                        <asp:TextBox ID="sharetype_code" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="member_no" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="30%">
                        <asp:TextBox ID="cp_mbname" runat="server" Style="text-align: left;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="membgroup_code" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="12%">
                        <asp:TextBox ID="resign_date" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="14%">
                        <asp:TextBox ID="cp_shareamt" runat="server" Style="text-align: right;" ReadOnly="true" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="12%">
                        <asp:TextBox ID="cp_sharemasterstatus" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="5%">
                        <asp:Button ID="b_collwho" runat="server" Text="..." />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>