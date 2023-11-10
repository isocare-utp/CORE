<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMethodpayment.ascx.cs"
    Inherits="Saving.Applications.divavg.ws_divsrv_detail_day_ctrl.DsMethodpayment" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
    <table class="DataSourceRepeater" align="center">
        <tr>
            <th width="5%">
            </th>
            <th width="25%">
                ประเภทรายการ
            </th>
            <th width="10%">
                ธนาคาร
            </th>
            <th width="20%">
                สาขา
            </th>
            <th width="20%">
                เลขที่บัญชี
            </th>
            <th width="20%">
                ยอดเงิน
            </th>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" HorizontalAlign="Center">
    <table class="DataSourceRepeater" align="center">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="5%">
                        <asp:TextBox ID="running_number" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="25%">
                        <asp:TextBox ID="methpaytype_desc" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="expense_bank" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="branch_name" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="expense_accid" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="expense_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
