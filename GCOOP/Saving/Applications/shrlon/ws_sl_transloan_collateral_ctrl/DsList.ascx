<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_transloan_collateral_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <tr>
        <th rowspan="2" width="3%">
        </th>
        <th rowspan="2" width="9%">
            เลขสมาชิก<br />
            รับโอน
        </th>
        <th rowspan="2" width="21%">
            ชื่อ-ชื่อสกุล
        </th>
        <th rowspan="2" width="13%">
            เลขสัญญา<br />
            รับโอน
        </th>
        <th colspan="2">
            ยอดรับโอน
        </th>
        <th colspan="2">
            ชำระต่องวด
        </th>
    </tr>
    <tr>
        <th width="14%">
            ต้นเงิน
        </th>
        <th width="14%">
            ด/บ ค้าง
        </th>
        <th width="14%">
            ต้นเงิน
        </th>
        <th width="14%">
            ด/บ ค้าง
        </th>
    </tr>
</table>
<asp:Panel ID="Panel1" runat="server" Height="500px" Width="750px" ScrollBars="Auto">
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <table class="DataSourceRepeater">
                <tr>
                    <td width="3%" align="center">
                        <asp:CheckBox ID="operate_flag" runat="server" />
                    </td>
                    <td width="9%">
                        <asp:TextBox ID="trn_memno" runat="server" Style="text-align: center" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="21%">
                        <asp:TextBox ID="memb_name" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="13%">
                        <asp:TextBox ID="trn_contractno" runat="server" Style="text-align: center" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="14%">
                        <asp:TextBox ID="trnprnbal_amt" runat="server" ToolTip="#,##0.00" Style="text-align: right"></asp:TextBox>
                    </td>
                    <td width="14%">
                        <asp:TextBox ID="trnintarrear_amt" runat="server" ToolTip="#,##0.00" Style="text-align: right"></asp:TextBox>
                    </td>
                    <td width="14%">
                        <asp:TextBox ID="periodpayprn_amt" runat="server" ToolTip="#,##0.00" Style="text-align: right"></asp:TextBox>
                    </td>
                    <td width="14%">
                        <asp:TextBox ID="periodpayintarr_amt" runat="server" ToolTip="#,##0.00" Style="text-align: right"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:Repeater>
</asp:Panel>
