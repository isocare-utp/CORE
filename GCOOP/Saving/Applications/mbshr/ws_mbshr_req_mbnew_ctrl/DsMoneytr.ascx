<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMoneytr.ascx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_req_mbnew_ctrl.DsMoneytr" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
    <table class="DataSourceRepeater" style="width: 760px;">
        <tr>
            <th rowspan="2" width="5%">
            </th>
            <th rowspan="2" width="20%">
                ประเภทรายการ
            </th>
            <th rowspan="2" width="15%">
                ทำรายการโดย
            </th>
            <th rowspan="2" width="15%">
                รหัสธนาคาร
            </th>
            <th colspan="2" width="25%">
                สาขา
            </th>
            <th rowspan="2" width="15%">
                เลขที่บัญชี
            </th>
            <th rowspan="2" width="5%">
            </th>
        </tr>
        <tr>
            <th width="10%">
                รหัส
            </th>
            <th width="15%">
                ชื่อ
            </th>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Height="700px" ScrollBars="Auto" HorizontalAlign="Center">
    <table class="DataSourceRepeater" style="width: 760px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td align="center" width="5%">
                        <asp:TextBox ID="running_number" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="20%">
                        <asp:DropDownList ID="trtype_code" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td width="15%">
                        <asp:DropDownList ID="moneytype_code" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td width="15%">
                        <asp:DropDownList ID="bank_code" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="bank_branch" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="branch_name" runat="server"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="bank_accid" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="5%">
                        <asp:Button ID="b_del" runat="server" Text="ลบ" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
