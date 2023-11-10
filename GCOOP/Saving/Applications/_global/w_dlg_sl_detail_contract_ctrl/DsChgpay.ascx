<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsChgpay.ascx.cs" Inherits="Saving.Applications._global.w_dlg_sl_detail_contract_ctrl.DsChgpay" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
    <table class="DataSourceRepeater" style="width: 720px;">
        <tr>
            <th width="9%">
                เลขที่เอกสาร
            </th>
            <th width="9%">
                วันที่เปลี่ยน
            </th>
            <th width="8%">
                รูปแบบเดิม
            </th>
            <th width="9%">
                ชำระ/งวดเดิม
            </th>
            <th width="8%">
                สถานะเดิม
            </th>
            <th width="8%">
                รูปแบบใหม่
            </th>
            <th width="9%">
                ชำระ/งวดใหม่
            </th>
            <th width="8%">
                สถานะใหม่
            </th>
            <th width="12%">
                ผู้ทำรายการ
            </th>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Height="374px" ScrollBars="Auto" HorizontalAlign="Left">
    <table class="DataSourceRepeater" style="width: 720px;">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="9%">
                        <asp:TextBox ID="contadjust_docno" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="9%">
                        <asp:TextBox ID="contadjust_date" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="8%">
                        <asp:TextBox ID="compute_1" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="9%">
                        <asp:TextBox ID="oldperiod_payment" runat="server" ReadOnly="true" Style="text-align: right;"
                            ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="8%">
                        <asp:TextBox ID="compute_2" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="8%">
                        <asp:TextBox ID="COMPUTE_3" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="9%">
                        <asp:TextBox ID="period_payment" runat="server" ReadOnly="true" Style="text-align: right;"
                            ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="8%">
                        <asp:TextBox ID="COMPUTE_4" runat="server" ReadOnly="true"  Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td width="12%">
                        <asp:TextBox ID="entry_id" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
