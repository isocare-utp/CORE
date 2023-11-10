<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_estimated_payments_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 770px;">
            <tr>
                <td colspan="6">
                    <u>รายละเอียดเงินกู้</u>
                </td>
            </tr>
            <tr>
                <td>
                    <span>ประเภทการกู้</span>
                </td>
                <td colspan="3">
                    <div>
                        <asp:DropDownList ID="loantype_code" runat="server">
                        </asp:DropDownList>
                    </div>
                </td>
                <td>
                    <div>
                        <span>จำนวนวันในรอบปี :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="an_year" runat="server" Style="text-align: center;"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td width="13%">
                    <div>
                        <span>จำนวนเงินที่กู้ :</span>
                    </div>
                </td>
                <td width="19%">
                    <div>
                        <asp:TextBox ID="PRINCIPAL_BALANCE" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </div>
                </td>
                <td width="13%">
                    <div>
                        <span>อัตรา ด/บ :</span>
                    </div>
                </td>
                <td width="19%">
                    <div>
                        <asp:TextBox ID="CONTINT_RATE" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </div>
                </td>
                <td width="17%">
                    <div>
                        <span>เริ่มชำระ (yyyy/mm) :</span>
                    </div>
                </td>
                <td width="19%">
                    <div>
                        <asp:TextBox ID="STARTPAY_PERIOD" runat="server" Style="text-align: center;"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>แบบการชำระ :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:DropDownList ID="PAYMENT_TYPE" runat="server">
                            <asp:ListItem Value="0">ต้นเท่ากันทุกงวด+ดอก</asp:ListItem>
                            <asp:ListItem Value="1">ต้น+ดอกเท่ากันทุกงวด</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </td>
                <td>
                    <div>
                        <span>จำนวนงวด :</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="PERIOD_INSTALLMENT" runat="server" Style="text-align: center;"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:Button ID="b_pay" runat="server" Text="ชำระ/งวด" Style="width: 125px;" />
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="PERIOD_PAYMENT" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Button ID="b_print" runat="server" Text="พิมพ์ตาราง" Style="margin-right: 3px;
                        background-color: #C2CFDF; width: 150px; border-color: #D1DCEB" />
                </td>
                <td align="right" colspan="5">
                    <asp:Button ID="b_gen" runat="server" Text="Gen ตารางการรับชำระ" Style="margin-right: 3px;
                        background-color: #C2CFDF; width: 150px; border-color: #D1DCEB" />
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
