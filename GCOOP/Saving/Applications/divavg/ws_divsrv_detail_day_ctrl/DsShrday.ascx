<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsShrday.ascx.cs" Inherits="Saving.Applications.divavg.ws_divsrv_detail_day_ctrl.DsShrday" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<span style="font-size: 12px;"><font color="#CC3300"><u><strong>รายการปันผลแบบวัน</strong></u></font></span>
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center" Width="730px" ScrollBars="Auto">
    <table class="DataSourceRepeater" align="center" style="width: 1000px;">
        <tr>
            <th width="4%">
                ลำดับ
            </th>
            <th width="8%">
                วันที่มีผลหุ้น
            </th>
            <th width="4%">
                วัน
            </th>
            <th width="12%">
                รับหุ้น(จำนวน)
            </th>
            <th width="12%">
                จ่ายหุ้น(จำนวน)
            </th>
            <th width="12%">
                รวมหุ้น(จำนวน)
            </th>
            <th width="12%">
                หุ้นสะสม(จำนวน)
            </th>
            <th width="12%">
                มูลค่าหุ้น(คำนวณ)
            </th>
            <th width="12%">
                ปันผล(บาท)
            </th>
            <th width="12%">
                ปันผลจริง(บาท)
            </th>
        </tr>
    </table>
    <table class="DataSourceRepeater" align="center" style="width: 1000px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="4%">
                        <asp:TextBox ID="running_number" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="8%">
                        <asp:TextBox ID="share_date" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="4%">
                        <asp:TextBox ID="day" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="12%">
                        <asp:TextBox ID="share_in_amount" runat="server" Style="text-align: right;" ToolTip="#,##0.000"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="12%">
                        <asp:TextBox ID="share_out_amount" runat="server" Style="text-align: right;" ToolTip="#,##0.000"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="12%">
                        <asp:TextBox ID="share_amount" runat="server" Style="text-align: right;" ToolTip="#,##0.000"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="12%">
                        <asp:TextBox ID="sharestk_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.000"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="12%">
                        <asp:TextBox ID="sharecal_value" runat="server" Style="text-align: right;" ToolTip="#,##0.000"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="12%">
                        <asp:TextBox ID="div_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.0000000"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="12%">
                        <asp:TextBox ID="rdiv_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.0000000"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
