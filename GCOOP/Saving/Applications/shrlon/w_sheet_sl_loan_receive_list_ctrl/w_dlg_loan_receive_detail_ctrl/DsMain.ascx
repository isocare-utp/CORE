<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.w_sheet_sl_loan_receive_list_ctrl.w_dlg_loan_receive_detail_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" border="0" style="width: 600px;">
            <tr>
                <td width="20%">
                    <div>
                        <span>ชื่อ-ชื่อสกุล :</span>
                    </div>
                </td>
                <td width="80%" colspan="3">
                    <asp:TextBox ID="compute1" runat="server" Style="width: 490px;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    <div>
                        <span>หน่่วย :</span>
                    </div>
                </td>
                <td width="80%" colspan="3">
                    <asp:TextBox ID="compute2" runat="server" Style="width: 490px;"></asp:TextBox>
                </td>
                 <td width="20%">
                    <asp:Button ID="b_receipt_pay" runat="server" Text="พิมพ์ใบสำคัญจ่าย" />
                </td>
                <td width="20%">
                    <asp:Button ID="b_receipt" runat="server" Text="พิมพ์ใบเสร็จ" />
                </td>
            </tr>
            <tr>
                <td width="20%">
                    <div>
                        <span>เลขสัญญา :</span>
                    </div>
                </td>
                <td width="30%">
                    <asp:TextBox ID="loancontract_no" runat="server"></asp:TextBox>
                </td>
                <td width="50%" colspan="2">
                    <asp:TextBox ID="compute3" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    <div>
                        <span>วัตถุประสงค์ :</span>
                    </div>
                </td>
                <td width="80%" colspan="3">
                    <asp:TextBox ID="loanobjective_desc" runat="server" Style="width: 490px;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <u>วงเงิน</u>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    <div>
                        <span>ยอดอนุมัติ :</span>
                    </div>
                </td>
                <td width="30%">
                    <asp:TextBox ID="loanapprove_amt" runat="server" ToolTip="#,##0.00" Style="text-align: right"></asp:TextBox>
                </td>
                <td width="20%">
                    <div>
                        <span>วันที่อนุมัติ :</span>
                    </div>
                </td>
                <td width="30%">
                    <asp:TextBox ID="startcont_date" runat="server"  Style="text-align: center"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    <div>
                        <span>รับเงินแล้ว :</span>
                    </div>
                </td>
                <td width="30%">
                    <asp:TextBox ID="compute4" runat="server" ToolTip="#,##0.00" Style="text-align: right"></asp:TextBox>
                </td>
                <td width="20%">
                    <div>
                        <span>ยอดคงเหลือ :</span>
                    </div>
                </td>
                <td width="30%">
                    <asp:TextBox ID="principal_balance" runat="server" ToolTip="#,##0.00" Style="text-align: right"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    <div>
                        <span>ยอดรอเบิก :</span>
                    </div>
                </td>
                <td width="30%">
                    <asp:TextBox ID="withdrawable_amt" runat="server" ToolTip="#,##0.00" Style="text-align: right"></asp:TextBox>
                </td>
                <td width="20%">
                    <div>
                        <span>งวดรับเงิน :</span>
                    </div>
                </td>
                <td width="30%">
                    <asp:TextBox ID="last_periodrcv" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <u>การชำระ</u>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    <div>
                        <span>รูปแบบชำระ :</span>
                    </div>
                </td>
                <td width="30%">
                    <asp:TextBox ID="PAYMENT_TYPE" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
                <td width="20%">
                    <div>
                        <span>การชำระ :</span>
                    </div>
                </td>
                <td width="30%">
                    <asp:TextBox ID="pay_status" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    <div>
                        <span>งวดชำระ :</span>
                    </div>
                </td>
                <td width="30%">
                    <asp:TextBox ID="period_payamt" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
                <td width="20%">
                    <div>
                        <span>ชำระ/งวด :</span>
                    </div>
                </td>
                <td width="30%">
                    <asp:TextBox ID="period_payment" runat="server" ToolTip="#,##0.00" Style="text-align: right"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <u>การรับเงินกู้</u>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    <div>
                        <span>การรับเงินกู้ :</span>
                    </div>
                </td>
                <td width="80%" colspan="3">
                    <asp:TextBox ID="expense_code" runat="server" Style="width: 490px;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    <div>
                        <span>ธนาคาร :</span>
                    </div>
                </td>
                <td width="80%" colspan="3">
                    <asp:TextBox ID="compute5" runat="server" Style="width: 490px;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <u>อื่นๆ</u>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    <div>
                        <span>อัตรา ด/บ :</span>
                    </div>
                </td>
                <td width="80%" colspan="3">
                    <asp:TextBox ID="compute6" runat="server" Style="width: 490px;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    <div>
                        <span>ผู้อนุมัติ :</span>
                    </div>
                </td>
                <td width="80%" colspan="3">
                    <asp:TextBox ID="approve_id" runat="server" Style="width: 490px;"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
