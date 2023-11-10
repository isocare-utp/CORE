<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsPayment.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_contract_adjust_ctrl.DsPayment" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 365px;">
            <tr>
                <td width="30%">
                    <div>
                        <span style="font-size: 12px">ชำระแบบ:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="loanpayment_type" runat="server" Style="width: 245px; font-size: 12px;">
                        <asp:ListItem Value="1">ต้นเท่ากัน</asp:ListItem>
                        <asp:ListItem Value="2">ยอดเท่ากัน</asp:ListItem>
                        <asp:ListItem Value="3">เก็บแต่ดอกเบี้ย</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span style="font-size: 12px">งวดชำระ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="period_payamt" runat="server" Style="text-align: center; font-size: 12px;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span style="font-size: 12px">ชำระ/งวด:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="period_payment" runat="server" Style="text-align: right; font-size: 12px;"
                        ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span style="font-size: 12px">สูงสุดไม่เกิน:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="period_payment_max" runat="server" Style="text-align: right; font-size: 12px;"
                        ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span style="font-size: 12px">สถานะ:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="payment_status" runat="server" Style="width: 245px; font-size: 12px;">
                        <asp:ListItem Value="1">ปกติ</asp:ListItem>
                        <asp:ListItem Value="-11">งดต้น</asp:ListItem>
                        <asp:ListItem Value="-12">งดดอก</asp:ListItem>
                        <asp:ListItem Value="-13">งดเก็บ</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
