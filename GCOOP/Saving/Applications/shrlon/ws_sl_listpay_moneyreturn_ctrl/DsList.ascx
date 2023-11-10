<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_listpay_moneyreturn_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" Width="750px">
    <table class="DataSourceRepeater" style="width: 735px;">
        <thead>
            <tr>
                <th width="2%">
                    <input type="checkbox" id="chk_all" />
                </th>
                <th width="4%">
                    ระบบ
                </th>
                <th width="8%">
                    ทะเบียน
                </th>
                <th width="9%">
                    วันที่
                </th>
                <th width="24%">
                    ชื่อ-สกุล
                </th>
                <th width="5%">
                    รายการ
                </th>
                <th width="5%">
                    โดย
                </th>
                <th width="15%" colspan="2">
                    ธนาคาร
                </th>
                <th width="10%">
                    เลขบัญชี
                </th>
                <th width="10%">
                    รวม
                </th>
            </tr>
        </thead>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Width="750px" Height="495px" ScrollBars="Vertical">
    <table class="DataSourceRepeater" style="width: 733px;">
        <tbody>
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <tr>
                        <td align="center" width="2%">
                            <asp:CheckBox ID="operate_flag" runat="server" />
                        </td>
                        <td width="4%">
                            <asp:TextBox ID="from_system" runat="server" Style="text-align: center"></asp:TextBox>
                        </td>
                        <td width="8%">
                            <asp:TextBox ID="member_no" runat="server" Style="text-align: center"></asp:TextBox>
                        </td>
                        <td width="9%">
                            <asp:TextBox ID="entry_date" runat="server" Style="text-align: center"></asp:TextBox>
                        </td>
                        <td width="24%">
                            <asp:TextBox ID="full_name" runat="server"></asp:TextBox>
                        </td>
                        <td width="5%">
                            <asp:TextBox ID="returnitemtype_desc" runat="server"></asp:TextBox>
                        </td>
                        <td width="5%">
                            <asp:TextBox ID="moneytype_code" runat="server" Style="text-align: center"></asp:TextBox>
                        </td>
                        <td width="8%">
                            <asp:DropDownList ID="bank_code" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td width="7%">
                            <asp:DropDownList ID="bank_branch" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td width="10%">
                            <asp:TextBox ID="bank_accid" runat="server" Style="text-align: center"></asp:TextBox>
                        </td>
                        <td width="10%">
                            <asp:TextBox ID="sum_return" runat="server" ToolTip="#,##0.00" Style="text-align: right;" />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
</asp:Panel>
