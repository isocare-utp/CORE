<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsData.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_slip_pay_ctrl.w_dlg_sl_detail_contract_ctrl.DsData" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <%-- <span class="TitleSpan">รายละเอียดสัญญา</span>--%>
        <table class="FormStyle" style="width: 830px;">
            <tr>
                <td width="16%">
                    <div>
                        <span>ประเภทสัญญา:</span>
                    </div>
                </td>
                <td width="18%">
                    <asp:TextBox ID="contracttype_desc" runat="server"></asp:TextBox>
                </td>
                <td width="15%">
                    <div>
                        <span>วัตุประสงค์:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="loanobjective_desc" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <u>เริ่มสัญญา</u>
                </td>
                <td>
                    <u>การชำระเงินกู้</u>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เริ่มสัญญา:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="startcont_date" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ประเภทการส่ง:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="loanpayment_type" runat="server">
                        <asp:ListItem Value="0" Text=""></asp:ListItem>
                        <asp:ListItem Value="1">คงต้น</asp:ListItem>
                        <asp:ListItem Value="2">คงยอด</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td width="15%">
                    <div>
                        <span>ชำระ/งวด:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="period_payment" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>สิ้นสุดสัญญา:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="expirecont_date" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>งวดชำระล่าสุด:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="cp_last_periodpay" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เริ่มเก็บ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="startkeep_period" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>จำนวนอนุมัติ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="loanapprove_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ต้นค้างชำระ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="principal_arrear" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ด/บ ค้างชำระ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="interest_arrear" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <u>คงเหลือ/รอเบิก</u>
                </td>
                <td>
                    <div>
                        <span>ด/บ เดือนค้าง:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="intmonth_arrear" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ด/บ ปีค้าง:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="intyear_arrear" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>งวดรับเงินล่าสุด:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="last_periodrcv" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <u>ยอดรอเรียกเก็บ</u>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>จำนวนรอเบิก:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="withdrawable_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ต้นรอเรียกเก็บ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="rkeep_principal" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ด/บ เรียกเก็บ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="rkeep_interest" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>คงเหลือ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="principal_balance" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td colspan="3">
                    <u>อื่นๆ</u>
                </td>
                <td>
                    <asp:CheckBox ID="od_flag" runat="server" />วงเงินกู้บัญชี OD
                </td>
            </tr>
            <tr>
                <td>
                    <u>วันที่ล่าสุด</u>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>รับเงินกู้ล่าสุด:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="lastreceive_date" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>รับโอนจาก:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="cp_trnfrom_memno" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ชำระล่าสุด:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="lastpayment_date" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>การผ่อนผัน:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="cp_compound_status" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>คิด ด/บ ล่าสุด:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="lastcalint_date" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ยอดเงินโอนไป</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="principal_trans" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ด/บ สะสม:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="interest_accum" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เรียกเก็บล่าสุด:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="lastprocess_date" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>สถานะการส่ง:</span>
                    </div>
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
                    <div>
                        <span>สถานะสัญญา:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="contract_status" runat="server">
                        <asp:ListItem Value="0" Text=""></asp:ListItem>
                        <asp:ListItem Value="1">ปกติ</asp:ListItem>
                        <asp:ListItem Value="11">รับโอน</asp:ListItem>
                        <asp:ListItem Value="-1">จบสัญญา</asp:ListItem>
                        <asp:ListItem Value="-11">โอนไป</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
