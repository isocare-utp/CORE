<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_cfinttable_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" Width="500px">
    <table class="DataSourceRepeater" style="width: 480px;">
        <tr>
            <th colspan="2">
                ช่วงวันที่
            </th>
            <th colspan="2">
                วงเงินอนุมัติ
            </th>
            <th rowspan="2" width="12%">
                อัตราดอกเบี้ย
            </th>
            <th rowspan="2" width="4%">
            </th>
        </tr>
        <tr>
            <th width="21%">
                วันที่เริ่มต้น
            </th>
            <th width="21%">
                วันที่สิ้นสุด
            </th>
            <th width="21%">
                ตั้งแต่
            </th>
            <th width="21%">
                ถึง
            </th>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Height="450px" Width="500px" ScrollBars="Auto">
    <table class="DataSourceRepeater" style="width: 480px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="21%">
                        <asp:TextBox ID="effective_date" runat="server" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td width="21%">
                        <asp:TextBox ID="expire_date" runat="server" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td width="21%">
                        <asp:TextBox ID="lower_amt" runat="server" Style="text-align: right" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="21%">
                        <asp:TextBox ID="upper_amt" runat="server" Style="text-align: right" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="12%">
                        <asp:TextBox ID="interest_rate" runat="server" Style="text-align: right" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="4%">
                        <asp:Button ID="b_del" runat="server" Text="-" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
