<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsPayment.ascx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_member_detail_main_ctrl.w_dlg_sl_detail_share_ctrl.DsPayment" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel2" runat="server" Height="200px" ScrollBars="Auto" HorizontalAlign="Center">
    <table class="TbStyle" style="width: 700px;">
        <tr>
            <th width="15%" rowspan="2">
                เลขที่เอกสาร
            </th>
            <th width="11%" rowspan="2">
                วันที่เปลี่ยน
            </th>
            <th width="8%" rowspan="2">
                งวดหุ้น
            </th>
            <th width="11%" rowspan="2">
                ทุนเรือนหุ้น
            </th>
            <th width="20%" colspan="2">
                ก่อนเปลี่ยนแปลง
            </th>
            <th width="20%" colspan="2">
                หลังเปลี่ยนแปลง
            </th>
            <th width="15%" rowspan="2">
                ผู้ทำรายการ
            </th>
        </tr>
        <tr>
            <th width="13%" style="font-size: 12px;">
                หุ้น/เดือน
            </th>
            <th width="7%" style="font-size: 12px;">
                สถานะ
            </th>
            <th width="13%" style="font-size: 12px;">
                หุ้น/เดือน
            </th>
            <th width="7%" style="font-size: 12px;">
                สถานะ
            </th>
        </tr>
    </table>
    <table class="TbStyle" style="width: 700px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="15%">
                        <asp:TextBox ID="payadjust_docno" runat="server" Style="text-align: center; font-size: 12px;"></asp:TextBox>
                    </td>
                    <td width="11%">
                        <asp:TextBox ID="approve_date" runat="server" Style="text-align: center; font-size: 12px;"></asp:TextBox>
                    </td>
                    <td width="8%">
                        <asp:TextBox ID="shrlast_period" runat="server" Style="text-align: center; font-size: 12px;"></asp:TextBox>
                    </td>
                    <td width="11%">
                        <asp:TextBox ID="sharestk_value" runat="server" Style="text-align: right; font-size: 12px;" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="13%">
                        <asp:TextBox ID="old_periodvalue" runat="server" Style="text-align: right; font-size: 12px;" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="7%">
                        <asp:TextBox ID="old_paystatus" runat="server" Style="text-align: center; font-size: 12px;"></asp:TextBox>
                    </td>
                    <td width="13%">
                        <asp:TextBox ID="new_periodvalue" runat="server" Style="text-align: right; font-size: 12px;" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="7%">
                        <asp:TextBox ID="new_paystatus" runat="server" Style="text-align: center; font-size: 12px;"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="approve_id" runat="server" Style="text-align: center; font-size: 12px;"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
