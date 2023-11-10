<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsLoan.ascx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_close_mems_ctrl.DsLoan" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater" style="width: 720px;">
    <tr>
        <th width="5%" rowspan="2">
        </th>
        <th width="10%" rowspan="2">
            เลขที่สัญญา
        </th>
        <th width="10%" rowspan="2">
            ยอดอนุมัติ
        </th>
        <th width="10%" rowspan="2">
            เริ่มสัญญา
        </th>
        <th width="10%" rowspan="2">
            งวดชำระ
        </th>
        <th width="10%" rowspan="2">
            สถานะการส่ง
        </th>
        <th colspan="2">
            การรับชำระล่าสุด
        </th>
        <th width="10%" rowspan="2">
            คงเหลือ
        </th>
        <th width="10%" rowspan="2">
            สถานะสัญญา
        </th>
    </tr>
    <tr>
        <th width="5%">
            งวด
        </th>
        <th width="10%">
            วันที่
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td align="center">
                    <asp:TextBox ID="prefix" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td align="center">
                    <asp:TextBox ID="loancontract_no" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="loanapprove_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="startcont_date" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="cp_period" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <asp:DropDownList ID="payment_status" runat="server">
                        <asp:ListItem Value="0" Text=""></asp:ListItem>
                        <asp:ListItem Value="1">ปกติ</asp:ListItem>
                        <asp:ListItem Value="-11">งดต้น</asp:ListItem>
                        <asp:ListItem Value="-12">งด ด/บ</asp:ListItem>
                        <asp:ListItem Value="-13">งดเก็บ</asp:ListItem>
                    </asp:DropDownList>                    
                </td>
                <td>
                    <asp:TextBox ID="last_periodpay" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="lastpayment_date" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="principal_balance" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="cp_contract_status" runat="server"></asp:TextBox>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
