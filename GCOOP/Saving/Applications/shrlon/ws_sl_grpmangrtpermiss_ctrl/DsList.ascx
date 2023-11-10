<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_grpmangrtpermiss_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <tr>
        <th rowspan="2" width="4%">
        </th>
        <th colspan="2">
            ทุนเรือนหุ้น
        </th>
        <th colspan="2">
            ระยะเวลา
        </th>
        <th colspan="2">
            เงินเดือน
        </th>
        <th rowspan="2" width="5%">
            หุ้น<br />
            (x เท่า)
        </th>
        <th rowspan="2" width="5%">
            เงินเดือน<br />
            (x เท่า)
        </th>
        <th rowspan="2" width="14%">
            วงเงินค้ำ<br />
            สูงสุด
        </th>
        <th rowspan="2" width="4%">
        </th>
    </tr>
    <tr>
        <th width="14%">
            ตั้งแต่ (>)
        </th>
        <th width="14%">
            (<=) ถึง
        </th>
        <th width="6%">
            ตั้งแต่ (>=)
        </th>
        <th width="6%">
            ถึง (<=)
        </th>
        <th width="14%">
            ตั้งแต่ (>=)
        </th>
        <th width="14%">
            (<=) ถึง
        </th>
    </tr>
</table>
<asp:Panel ID="Panel1" runat="server" Height="300px" Width="750px" ScrollBars="Auto">
    <table class="DataSourceRepeater">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="4%">
                        <asp:TextBox ID="running_number" runat="server" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td width="14%">
                        <asp:TextBox ID="startshare_amt" runat="server" Style="text-align: right" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="14%">
                        <asp:TextBox ID="endshare_amt" runat="server" Style="text-align: right" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="6%">
                        <asp:TextBox ID="startmember_time" runat="server" Style="text-align: right" ToolTip="#,##0"></asp:TextBox>
                    </td>
                    <td width="6%">
                        <asp:TextBox ID="endmember_time" runat="server" Style="text-align: right" ToolTip="#,##0"></asp:TextBox>
                    </td>
                    <td width="14%">
                        <asp:TextBox ID="start_salary" runat="server" Style="text-align: right" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="14%">
                        <asp:TextBox ID="end_salary" runat="server" Style="text-align: right" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="5%">
                        <asp:TextBox ID="multiple_share" runat="server" Style="text-align: center" ToolTip="#,##0"></asp:TextBox>
                    </td>
                    <td width="5%">
                        <asp:TextBox ID="multiple_salary" runat="server" Style="text-align: center" ToolTip="#,##0"></asp:TextBox>
                    </td>
                    <td width="14%">
                        <asp:TextBox ID="maxgrt_amt" runat="server" Style="text-align: right" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="4%">
                        <asp:Button ID="b_del" runat="server" Text="-" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
