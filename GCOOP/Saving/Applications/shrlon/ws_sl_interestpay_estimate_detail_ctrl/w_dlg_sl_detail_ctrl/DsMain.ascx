<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_interestpay_estimate_detail_ctrl.w_dlg_sl_detail_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" border="0" style="width: 690px;">
            <tr>
                <td colspan="4">
                    <u>รายละเอียดสัญญา</u>
                </td>
            </tr>
            <tr>
                <td width="22%">
                    <div>
                        <span>เลขสัญญา :</span>
                    </div>
                </td>
                <td width="28%">
                    <asp:TextBox ID="loancontract_no" runat="server" Style="text-align: center; background-color: #FFFFCC;"></asp:TextBox>
                </td>
                <td width="22%">
                    <div>
                        <span>เริ่มสัญญา :</span>
                    </div>
                </td>
                <td width="28%">
                    <asp:TextBox ID="startcont_date" runat="server" Style="text-align: center; background-color: #FFFFCC;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ยอดอนุมัติ :</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="loanapprove_amt" runat="server" Style="text-align: right; background-color: #FFFFCC;"
                        ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ยอดคองเหลือ :</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="principal_balance" runat="server" Style="text-align: right; background-color: #FFFFCC;"
                        ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <u>การชำระ</u>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ชำระล่าสุด :</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="LASTPAYMENT_DATE" runat="server" Style="text-align: center; background-color: #FFFFCC;"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>การชำระ :</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="payment_type" runat="server" Style="text-align: center; background-color: #FFFFCC;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>จำนวนงวด :</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="period_installment" runat="server" Style="text-align: center; background-color: #FFFFCC;"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ชำระ/งวด :</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="period_payment" runat="server" Style="text-align: right; background-color: #FFFFCC;"
                        ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <u>ดอกเบี้ย</u>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>อัตราดอกเบี้ยที่เรียกเก็บ :</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="int_conttype" runat="server" Style="text-align: center; background-color: #FFFFCC;"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ด/บ คงที่ตลอดอายุสัญญา :</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="int_contintrate" runat="server" Style="text-align: right; background-color: #FFFFCC;"
                        ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ดอกเบี้ยจากตาราง :</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="int_continttabcode" runat="server" Style="text-align: center; background-color: #FFFFCC;"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ดอกเบี้ย เพิ่ม/ลด ต่างหาก :</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="int_contintincrease" runat="server" Style="text-align: right; background-color: #FFFFCC;"
                        ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>คิด ด/บ ล่าสุด :</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="lastcalint_date" runat="server" Style="text-align: center; background-color: #FFFFCC;"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ด/บ ค้างชำระ :</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="interest_arrear" runat="server" Style="text-align: right; background-color: #FFFFCC;"
                        ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ด/บ สะสม :</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="interest_accum" runat="server" Style="text-align: right; background-color: #FFFFCC;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
