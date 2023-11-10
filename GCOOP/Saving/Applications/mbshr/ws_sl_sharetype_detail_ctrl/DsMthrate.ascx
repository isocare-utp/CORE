<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMthrate.ascx.cs" Inherits="Saving.Applications.mbshr.ws_sl_sharetype_detail_ctrl.DsMthrate" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <tr>
        <th rowspan="2" width="4%">
            ที่
        </th>
        <th colspan="2">
            เงินเดือน
        </th>
        <th rowspan="2" width="12%">
            % หุ้น/<br />เงินเดือน
        </th>
        <th colspan="2">
            จำนวนหุ้น/เดือน
        </th>
        <th colspan="2">
            แก้ไขล่าสุด
        </th>
        <th rowspan="2" width="4%">
        </th>
    </tr>
    <tr>
        <th width="14%">
            ตั้งแต่
        </th>
        <th width="14%">
            ถึง
        </th>
        <th width="13%">
            ต่ำสุด
        </th>
        <th width="13%">
            สูงสุด
        </th>
        <th width="13%">
            ผู้ทำรายการ
        </th>
        <th width="13%">
            วันที่ทำรายการ
        </th>
    </tr>
</table>
<asp:Panel ID="Panel1" runat="server" Width="750px" Height="650px" ScrollBars="Auto">
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <table class="DataSourceRepeater">
                <tr>
                    <td width="4%">
                        <asp:TextBox ID="running_number" runat="server" ReadOnly="true" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td width="14%">
                        <asp:TextBox ID="start_salary" runat="server" Style="text-align: right" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="14%">
                        <asp:TextBox ID="end_salary" runat="server" Style="text-align: right" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="12%">
                        <asp:TextBox ID="sharerate_percent" runat="server" Style="text-align: right" ToolTip="##0.00"></asp:TextBox>
                    </td>
                    <td width="13%">
                        <asp:TextBox ID="minshare_amt" runat="server" Style="text-align: right" ToolTip="#,##0"></asp:TextBox>
                    </td>
                    <td width="13%">
                        <asp:TextBox ID="maxshare_amt" runat="server" Style="text-align: right" ToolTip="#,##0"></asp:TextBox>
                    </td>
                    <td width="13%">
                        <asp:TextBox ID="entry_id" runat="server" BackColor="#CCCCCC" ReadOnly="true" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td width="13%">
                        <asp:TextBox ID="entry_date" runat="server" BackColor="#CCCCCC" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="4%">
                        <asp:Button ID="b_del" runat="server" Text="-" />
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:Repeater>
</asp:Panel>
