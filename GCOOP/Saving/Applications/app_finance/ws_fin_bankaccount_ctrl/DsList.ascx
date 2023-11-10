<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.app_finance.ws_fin_bankaccount_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" Width="99%"  Height="450px" ScrollBars="Auto" HorizontalAlign="Left">
    <table class="DataSourceRepeater">
        <tr>
            <th width="5%">
                ลำดับ                
            </th>
            <th width="15%">
                วันที่
            </th>
            <th width="40%">
                รายการ
            </th>
            <th width="20%">
                จำนวนเงิน
            </th>
            <th width="20%">
                คงเหลือ
            </th>
        </tr>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td align="center">
                        <asp:TextBox ID="SEQ_NO" runat="server" Style="text-align: center"  ReadOnly="True" />
                    </td>
                    <td align="center">
                        <asp:TextBox ID="OPERATE_DATE" runat="server" ReadOnly="True" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="DETAIL_DESC" runat="server" ReadOnly="True" ></asp:TextBox>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="ITEM_AMT" runat="server" ReadOnly="True" Style="text-align: right" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="BALANCE" runat="server" ReadOnly="True" Style="text-align: right" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>