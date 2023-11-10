<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDetail.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_estimated_payments_ctrl.DsDetail" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" Height="450px" ScrollBars="Auto" HorizontalAlign="Left">
    <table class="DataSourceRepeater" style="width: 765px;">
        <tr>
            <th>
                งวด
            </th>
            <th>
                วันที่ชำระ
            </th>
            <th>
                จำนวนวัน
            </th>
            <th>
                ชำระต้น
            </th>
            <th>
                ชำระ ด/บ
            </th>
            <th>
                รวมชำระ
            </th>
            <th>
                คงเหลือ
            </th>
        </tr>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="10%">
                        <asp:TextBox ID="PERIOD" runat="server" ReadOnly="True" Style="text-align: center;
                            background-color: #FFCCFF"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="RECV_DATE" runat="server" Style="text-align: center; background-color: #FFCCFF"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="INTPAY_DAY" runat="server" Style="text-align: center; background-color: #FFCCFF"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="PRNPAY_AMT" runat="server" Style="text-align: right; background-color: #FFCCFF"
                            ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="INTPAY_AMT" runat="server" Style="text-align: right; background-color: #FFCCFF"
                            ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="TOTAL_PERIOD" runat="server" Style="text-align: right; background-color: #FFCCFF"
                            ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="PRNBAL_AMT" runat="server" Style="text-align: right; background-color: #FFCCFF"
                            ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
<table class="DataSourceFormView" style="width: 765px;">
    <tr>
        <td width="10%">
        </td>
        <td width="15%">
        </td>
        <td width="15%">
        </td>
        <td width="15%">
            <asp:TextBox ID="cp_sumprnpayamt" runat="server" Style="text-align: right; background-color: Black"
                ForeColor="GreenYellow" ToolTip="#,##0.00"></asp:TextBox>
        </td>
        <td width="15%">
            <asp:TextBox ID="cp_sumintpayamt" runat="server" Style="text-align: right; background-color: Black"
                ForeColor="GreenYellow" ToolTip="#,##0.00"></asp:TextBox>
        </td>
        <td width="15%">
            <asp:TextBox ID="cp_sumtotalpay" runat="server" Style="text-align: right; background-color: Black"
                ForeColor="GreenYellow" ToolTip="#,##0.00"></asp:TextBox>
        </td>
        <td width="15%">
        </td>
    </tr>
</table>
