<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsStatement.ascx.cs"
    Inherits="Saving.Applications.divavg.ws_divsrv_detail_ctrl.DsStatement" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<span style="font-size: 12px;"><font color="#3366FF"><u><strong>Statement</strong></u></font></span>
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center" Width="730px" ScrollBars="Auto">
    <table class="DataSourceRepeater" align="center" style="width: 1200px;">
        <tr>
            <th width="5%" rowspan="2">
                ลำดับ
            </th>
            <th width="14%" colspan="2">
                วันที่
            </th>
            <th width="14%" rowspan="2">
                ประเภทการทำรายการ
            </th>
            <th width="14%" rowspan="2">
                เลขที่ใบทำรายการอ้างอิง
            </th>
            <th width="7%">
                ปันผล
            </th>
            <th width="7%">
                เฉลี่ยคืน
            </th>
            <th width="7%">
                ปันผลค้างจ่าย
            </th>
            <th width="15%" rowspan="2">
                คงเหลือ
            </th>
            <th width="7%" rowspan="2">
                สถานะ
            </th>
            <th width="10%" rowspan="2">
                ผู้ทำรายการ
            </th>
        </tr>
        <tr>
            <th width="7%">
                ทำรายการ
            </th>
            <th width="7%">
                ระบบ
            </th>
            <th>
                ยอดจ่าย
            </th>
            <th>
                ยอดจ่าย
            </th>
            <th>
                ยอดจ่าย
            </th>
        </tr>
    </table>
    <table class="DataSourceRepeater" align="center" style="width: 1200px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="5%">
                        <asp:TextBox ID="running_number" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="7%">
                        <asp:TextBox ID="operate_date" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="7%">
                        <asp:TextBox ID="slip_date" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="14%">
                        <asp:TextBox ID="divitemtype_desc" runat="server" ReadOnly="true"></asp:TextBox>
                        <%--<asp:DropDownList ID="divitemtype_code" runat="server">
                        </asp:DropDownList>--%>
                    </td>
                    <td width="14%">
                        <asp:TextBox ID="ref_slipno" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="7%">
                        <asp:TextBox ID="div_payment" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="7%">
                        <asp:TextBox ID="avg_payment" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="7%">
                        <asp:TextBox ID="etc_payment" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="item_balamt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="7%">
                        <asp:TextBox ID="item_status" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="entry_id" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
