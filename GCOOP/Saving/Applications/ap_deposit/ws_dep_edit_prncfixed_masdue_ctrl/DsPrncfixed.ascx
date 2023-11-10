<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsPrncfixed.ascx.cs" Inherits="Saving.Applications.ap_deposit.ws_dep_edit_prncfixed_masdue_ctrl.DsPrncfixed" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server"  Height="500px"  Width="700px" ScrollBars="auto">
    <table class="DataSourceRepeater" style="width:900px;">
        <tr>
            <th width="5%">
                เลขต้นเงิน
            </th>
            <th width="15%">
                จำนวนเงิน
            </th>
            <th width="10%">
                วันที่ฝาก
            </th>
            <th width="10%">
                วันที่ครบดิว
            </th>
            <th width="10%">
                rate ด/บ
            </th>
            <th width="10%">
                วันที่คิดดอกเบี้ยล่าสุด
            </th>
            <th width="15%">
                คงเหลือ
            </th>
        </tr>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:TextBox ID="prnc_no" runat="server"  Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="prnc_amt" runat="server" ToolTip="#,##0" Style="text-align: right"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="prnc_date" runat="server" Style="text-align: center;" ></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="prncdue_date" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>                   
                    <td>
                        <asp:TextBox ID="interest_rate" runat="server" ToolTip="#,##0.00" Style="text-align: right" ></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="lastcalint_date" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="prnc_bal" runat="server"   ToolTip="#,##0" Style="text-align: right" > </asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
