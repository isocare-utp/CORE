<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsCollwho.ascx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_close_mems_ctrl.DsCollwho" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater" style="width: 720px;">
    <tr>
        <th width="4%">            
        </th>
        <th width="11%">
            เลขสัญญาผู้กู้
        </th>
        <th width="10%">
            ทะเบียนผู้กู้
        </th>
        <th width="25%">
            ชื่อ - สกุลผู้กู้
        </th>
        <th width="10%">
            หุ้นสะสมผู้กู้
        </th>
        <th width="10%">
            วันจ่ายเงินกู้
        </th>
        <th width="12%">
            คงเหลือ/รอเบิก
        </th>
        <th width="7%">
            % การค้ำ
        </th>
        <th width="11%">
            เงินค้ำประกัน
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td align="center">
                    <asp:TextBox ID="running_number" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td align="center">
                    <asp:TextBox ID="loancontract_no" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="member_no" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="cp_name" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="cp_share_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="startcont_date" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="cp_prnwithdraw_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="collactive_percent" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="cp_collamt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
