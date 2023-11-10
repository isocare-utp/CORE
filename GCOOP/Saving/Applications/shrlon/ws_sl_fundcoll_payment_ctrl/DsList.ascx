<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" 
Inherits="Saving.Applications.shrlon.ws_sl_fundcoll_payment_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <tr>
        <th width="3%">
        </th>
        <th width="31%">
            รายละเอียดการจ่าย
        </th>
        <th width="22%">
            ดอกเบี้ย
        </th>
        <th width="22%">
            ยอดกองทุน
        </th>
        <th width="22%">
            รวมจ่ายสุทธิ์
        </th>
    </tr>
</table>
<asp:Panel ID="Panel2" runat="server" Height="200px" Width="750px" ScrollBars="Auto">
    <table class="DataSourceRepeater">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="3%" align="center">
                        <asp:CheckBox ID="operate_flag" runat="server" />
                    </td>
                    <td width="31%">
                        <asp:TextBox ID="slipitem_desc" runat="server"></asp:TextBox>
                    </td>
                    <td width="22%">
                        <asp:TextBox ID="int_amt" runat="server" Style="text-align: right" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="22%">
                        <asp:TextBox ID="fundbalance" runat="server" Style="text-align: right" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="22%">
                        <asp:TextBox ID="itempay_amt" runat="server" Style="text-align: right" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
