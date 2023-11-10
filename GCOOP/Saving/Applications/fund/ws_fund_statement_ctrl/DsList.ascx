<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" 
Inherits="Saving.Applications.fund.ws_fund_statement_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater" style="width:750px">
    <tr>
        <th width="3%">
            ลำดับ
        </th>
        <th width="7%">
            รายการ
        </th>
        <th width="11%">
            วันที่ทำรายการ
        </th>        
        <th width="15%">
            จำนวนเงิน
        </th>
        <th width="15%">
            ยกมา
        </th>
        <th width="15%">
            คงเหลือ
        </th>
     <th width="10%">
            เลขสัญญา
        </th>
        <th width="8%">
            สถานะ
        </th>
    </tr>
<asp:Panel ID="Panel2" runat="server" Width="750px" ScrollBars="Auto">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="3%">
                        <asp:TextBox ID="seq_no" runat="server" Style="text-align:center" ReadOnly="true" />
                    </td>
                    <td width="7%">
                        <asp:TextBox ID="ITEMTYPE_CODE" runat="server" Style="text-align:center" ReadOnly="true" />
                    </td>
                    <td width="11%">
                        <asp:TextBox ID="OPERATE_DATE" runat="server" Style="text-align:center" ReadOnly="true" />
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="ITEMPAY_AMT" runat="server" Style="text-align:right" ReadOnly="true" ToolTip="#,##0.00"  />
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="BALANCE_FORWARD" runat="server" Style="text-align: right" ToolTip="#,##0.00" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="BALANCE" runat="server" Style="text-align: right" ToolTip="#,##0.00" ReadOnly="true"></asp:TextBox>
                    </td>                    
                    <td width="15%">
                        <asp:TextBox ID="LOANCONTRACT_NO" runat="server" Style="text-align: right" ToolTip="#,##0.00" ReadOnly="true"></asp:TextBox>
                    </td>
                     <td width="8%">
                        <asp:TextBox ID="STATUSDISPLAY" runat="server" Style="text-align: center" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
