<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" 
Inherits="Saving.Applications.shrlon.ws_sl_fundcoll_statement_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <tr>
        <th width="3%">
            ลำดับ
        </th>
        <th width="10%">
            ประเภทรายการ
        </th>
        <th width="10%">
            วันที่ทำรายการ
        </th>
        <th width="10%">
            สถานะพิมพ์
        </th>
        <th width="15%">
            จำนวนเงิน
        </th>
        <th width="15%">
            ดอกเบี้ย
        </th>
<%--        <th width="15%">
            ดอกเบี้ยสะสม
        </th>--%>
    </tr>
</table>
<asp:Panel ID="Panel2" runat="server" Width="750px" ScrollBars="Auto">
    <table class="DataSourceRepeater">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="4%">
                        <asp:TextBox ID="seq_no" runat="server" Style="text-align:center" ReadOnly="true" />
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="typedesc" runat="server" Style="text-align:center" ReadOnly="true" />
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="operate_date" runat="server" Style="text-align:center" ReadOnly="true" />
                    </td>
                    <td width="10%">
                        <asp:TextBox ID="prntodesc" runat="server" Style="text-align:center" ReadOnly="true" />
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="itempay_amt" runat="server" Style="text-align: right" ToolTip="#,##0.00" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="int_amt" runat="server" Style="text-align: right" ToolTip="#,##0.00" ReadOnly="true"></asp:TextBox>
                    </td>
<%--                    <td width="15%">
                        <asp:TextBox ID="int_accum" runat="server" Style="text-align: right" ToolTip="#,##0.00" ReadOnly="true"></asp:TextBox>
                    </td>--%>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
