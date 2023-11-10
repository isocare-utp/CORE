<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsRecieveList.ascx.cs"
    Inherits="Saving.Applications.keeping.w_sheet_kp_adjust_kptp_ctrl.DsRecieveList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" Height="200px" Width="720px" ScrollBars="Auto"
    HorizontalAlign="Left">
    <table class="DataSourceRepeater" style="width: 720px">
        <tr>
            <th width="2%">
            </th>
            <th width="5%">
                รหัส
            </th>
            <th width="12%">
                รายละเอียด
            </th>
            <th width="5%">
                งวดที่
            </th>
            <th width="9%">
                เงินต้น
            </th>
            <th width="9%">
                ดอกเบี้ย
            </th>
            <th width="9%">
                เงินชำระ
            </th>
            <th width="10%">
                คงเหลือ
            </th>
            <th width="3%">
                สถานะ
            </th>
            <th width="11%">
                เก็บได้
            </th>
            <th width="10%" class = 'El_hiddenF'>
                คงเหลือก่อนหน้า
            </th>
        </tr>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:TextBox ID="seq_no" runat="server" Style="text-align: center" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="keepitemtype_code" runat="server" Style="text-align: center" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="description" runat="server" Style="text-align: center" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="period" runat="server" Style="text-align: center" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="principal_payment" runat="server" Style="text-align: right" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="interest_payment" runat="server" Style="text-align: right" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="item_payment" runat="server" Style="text-align: right" ToolTip="#,##0.00"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="principal_balance" runat="server" Style="text-align: right" ToolTip="#,##0.00"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="posting_flag" runat="server" Style="text-align: center"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="receipt_imp" runat="server" Style="text-align: right" ToolTip="#,##0.00" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td class = 'El_hiddenF'>
                        <asp:TextBox ID="bfprinbalance_amt" runat="server" Style="text-align: right" ToolTip="#,##0.00"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
