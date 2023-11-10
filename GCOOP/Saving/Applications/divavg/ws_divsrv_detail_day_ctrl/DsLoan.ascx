<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsLoan.ascx.cs" Inherits="Saving.Applications.divavg.ws_divsrv_detail_day_ctrl.DsLoan" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<span style="font-size: 12px;"><font color="#CC0066"><u><strong>รายการเฉลี่ยคืน</strong></u></font></span>
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
    <table class="DataSourceRepeater" align="center">
        <tr>
            <th width="5%">
                ลำดับ
            </th>
            <th width="15%">
                เลขที่สัญญา
            </th>
            <th width="10%">
                อัตรา
            </th>
            <th width="20%">
                ดอกเบี้ยสะสม
            </th>
            <th width="25%">
                เฉลี่ยคืน(บาท)
            </th>
            <th width="25%">
                เฉลี่ยคืนจริง(บาท)
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
                    <td width="15%">
                        <asp:TextBox ID="loancontract_no" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="avgpercent_rate" runat="server" Style="text-align: right;" ToolTip="#,##0.00%"></asp:TextBox>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="interest_accum" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="25%">
                        <asp:TextBox ID="avg_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.0000000"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="25%">
                        <asp:TextBox ID="ravg_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
