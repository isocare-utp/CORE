<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsStatement.ascx.cs"
    Inherits="Saving.Applications.mbshr.ws_mbshr_member_detail_ctrl.w_dlg_sl_detail_share_ctrl.DsStatement" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
    <table class="TbStyle" style="width: 700px;">
        <tr>
            <th width="4%" rowspan="2" style="font-size: 12px;">

            </th>
            <th width="11%" rowspan="2" style="font-size: 12px;">
                วันทำรายการ
            </th>
            <th width="11%" rowspan="2" style="font-size: 12px;">
                วันที่ใบเสร็จ
            </th>
            <th width="14%" rowspan="2" style="font-size: 12px;">
                เลขที่ใบเสร็จ
            </th>
            <th width="7%" rowspan="2" style="font-size: 12px;">
                รายการ
            </th>
            <th width="5%" rowspan="2" style="font-size: 12px;">
                งวด
            </th>
            <th  colspan="2" style="font-size: 12px;" width="30%">
                ยอดทำรายการ
            </th>
            <th width="16%" rowspan="2" style="font-size: 12px;">
                หุ้นคงเหลือ
            </th>
        </tr>
        <tr>
            <th width="16%" style="font-size: 12px;">
                รับ
            </th>
            <th width="16%" style="font-size: 12px;">
                จ่าย/<br />
                ยกเลิก
            </th>
        </tr>
    </table>
    <asp:Panel ID="Panel2" runat="server" Height="300px" ScrollBars="Auto" HorizontalAlign="Center">
    <table class="TbStyle" style="width: 700px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="4%">
                        <asp:TextBox ID="running_number" runat="server" Style="text-align: center; font-size:12px;"></asp:TextBox>
                    </td>
                    <td width="11%">
                        <asp:TextBox ID="operate_date" runat="server" Style="text-align: center; font-size:12px;"></asp:TextBox>
                    </td>
                    <td width="11%">
                        <asp:TextBox ID="slip_date" runat="server" Style="text-align: center; font-size:12px;"></asp:TextBox>
                    </td>
                    <td width="14%">
                        <asp:TextBox ID="ref_docno" runat="server" Style="text-align: center; font-size:12px;"></asp:TextBox>
                    </td>
                    <td width="7%">
                        <asp:TextBox ID="shritemtype_code" runat="server" Style="text-align: center; font-size:12px;"></asp:TextBox>
                    </td>
                    <td width="5%">
                        <asp:TextBox ID="period" runat="server" Style="text-align: center; font-size:12px;"></asp:TextBox>
                    </td>
                    <td width="16%">
                        <asp:TextBox ID="cp_sign_flag_in" runat="server" Style="text-align: right; font-size:12px;" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="16%">
                        <asp:TextBox ID="cp_sign_flag_out" runat="server" Style="text-align: right; font-size:12px;" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="16%">
                        <asp:TextBox ID="cp_sharestk" runat="server" Style="text-align: right; font-size:12px;" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
