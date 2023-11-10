<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDetail.ascx.cs" Inherits="Saving.Applications._global.w_dlg_sl_detail_contract_ctrl.DsDetail" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit" OnPageIndexChanging="FormView1_PageIndexChanging"
    Style="margin-right: 98px">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 700px;">
            <tr>
                <td width="13%">
                    <div>
                        <span>ประเภทสัญญา:</span>
                    </div>
                </td>
                <td width="20%">
                    <div>
                        <asp:TextBox ID="CONTRACTTYPE_DESC" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
                <td width="13%">
                    <div>
                        <span>วัตถุประสงค์:</span>
                    </div>
                </td>
                <td colspan="3">
                    <div>
                        <asp:TextBox ID="LOANOBJECTIVE_DESC" runat="server" ReadOnly="true" Width="99%"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <u>เริ่มสัญญา: </u>
                </td>
                <td colspan="2">
                    <u>การชำระเงินกู้: </u>
                </td>
            </tr>
            <tr>
                <td width="13%">
                    <div>
                        <span>เริ่มสัญญา:</span>
                    </div>
                </td>
                <td width="20%">
                    <div>
                        <asp:TextBox ID="startcont_date" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </div>
                </td>
                <td width="13%">
                    <div>
                        <span>ประเภทการส่ง:</span>
                    </div>
                </td>
                <td width="20%">
                    <div>
                        <asp:TextBox ID="COMPUTE_4" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
                <td width="13%">
                    <div>
                        <span>ชำระ/งวด:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="period_payment" runat="server" ReadOnly="true" Style="text-align: right;"
                            ToolTip="#,##0.00"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td width="13%">
                    <div>
                        <span>สิ้นุสุดสัญญา:</span>
                    </div>
                </td>
                <td width="20%">
                    <div>
                        <asp:TextBox ID="expirecont_date" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </div>
                </td>
                <td width="13%">
                    <div>
                        <span>งวดชำระล่าสุด:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="COMPUTE_1" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td width="13%">
                    <div>
                        <span>เริ่มเก็บ:</span>
                    </div>
                </td>
                <td width="20%">
                    <div>
                        <asp:TextBox ID="startkeep_period" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td width="13%">
                    <div>
                        <span>จำนวนอนุมัติ:</span>
                    </div>
                </td>
                <td width="20%">
                    <div>
                        <asp:TextBox ID="loanapprove_amt" runat="server" ReadOnly="true" Style="text-align: right;"
                            ToolTip="#,##0.00"></asp:TextBox>
                    </div>
                </td>
                <td width="13%">
                    <div>
                        <span>ต้นค้างชำระ:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="principal_arrear" runat="server" ReadOnly="true" Style="text-align: right;"
                            ToolTip="#,##0.00"></asp:TextBox>
                    </div>
                </td>
                <td width="13%">
                    <div>
                        <span>ด/บ ค้างชำระ:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="interest_arrear" runat="server" ReadOnly="true" Style="text-align: right;"
                            ToolTip="#,##0.00"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <u>คงเหลือ/รอเบิก:</u>
                </td>
                <td width="13%">
                    <div>
                        <span>ด/บ เดือนค้าง:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="intmonth_arrear" runat="server" ReadOnly="true" Style="text-align: right;"
                            ToolTip="#,##0.00"></asp:TextBox>
                    </div>
                </td>
                <td width="13%">
                    <div>
                        <span>ด/บ ปีค้าง:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="intyear_arrear" runat="server" ReadOnly="true" Style="text-align: right;"
                            ToolTip="#,##0.00"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td width="13%">
                    <div>
                        <span>งวดรับเงินล่าสุด:</span>
                    </div>
                </td>
                <td width="20%">
                    <div>
                        <asp:TextBox ID="last_periodrcv" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </div>
                </td>
                <td colspan="3">
                    <u>ยอดรอเรียกเก็บ:</u>
                </td>
            </tr>
            <tr>
                <td width="13%">
                    <div>
                        <span>จำนวนรอเบิก:</span>
                    </div>
                </td>
                <td width="20%">
                    <div>
                        <asp:TextBox ID="withdrawable_amt" runat="server" ReadOnly="true" Style="text-align: right"></asp:TextBox>
                    </div>
                </td>
                <td width="13%">
                    <div>
                        <span>ต้นรอเรียกเก็บ:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="rkeep_principal" runat="server" ReadOnly="true" Style="text-align: right;"
                            ToolTip="#,##0.00"></asp:TextBox>
                    </div>
                </td>
                <td width="13%">
                    <div>
                        <span>ด/บเรียกเก็บ:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="rkeep_interest" runat="server" ReadOnly="true" Style="text-align: right;"
                            ToolTip="#,##0.00"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td width="13%">
                    <div>
                        <span>คงเหลือ:</span>
                    </div>
                </td>
                <td width="20%">
                    <div>
                        <asp:TextBox ID="principal_balance" runat="server" ReadOnly="true" Style="text-align: right;"
                            ToolTip="#,##0.00"></asp:TextBox>
                    </div>
                </td>
                <td colspan="3" valign="bottom">
                    <u>อื่นๆ:</u>
                </td>
                <td valign="bottom">
                    <asp:CheckBox ID="od_flag" runat="server" Text=" วงเงินกู้บัญชี OD" />
                </td>
            </tr>
            <tr>
                <td>
                    <u>วันที่ล่าสุด: </u>
                </td>
            </tr>
            <tr>
                <td width="13%">
                    <div>
                        <span>รับเงินกู้ล่าสุด:</span>
                    </div>
                </td>
                <td width="20%">
                    <div>
                        <asp:TextBox ID="lastreceive_date" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </div>
                </td>
                <td width="13%">
                    <div>
                        <span>รับโอนจาก:</span>
                    </div>
                </td>
                <td colspan="3">
                    <div>
                        <asp:TextBox ID="COMPUTE_2" runat="server" ReadOnly="true" Width="99%"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td width="13%">
                    <div>
                        <span>ชำระล่าสุด:</span>
                    </div>
                </td>
                <td width="20%">
                    <div>
                        <asp:TextBox ID="lastpayment_date" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </div>
                </td>
                <td width="13%">
                    <div>
                        <span>การผ่อนผัน:</span>
                    </div>
                </td>
                <td colspan="3">
                    <div>
                        <asp:TextBox ID="COMPUTE_3" runat="server" ReadOnly="true" Width="99%"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td width="13%">
                    <div>
                        <span>คิดด/บล่าสุด:</span>
                    </div>
                </td>
                <td width="20%">
                    <div>
                        <asp:TextBox ID="lastcalint_date" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </div>
                </td>
                <td width="13%">
                    <div>
                        <span>ยอดเงินโอนไป:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="principal_trans" runat="server" ReadOnly="true" Style="text-align: right;"
                            ToolTip="#,##0.00"></asp:TextBox>
                    </div>
                </td>
                <td width="13%">
                    <div>
                        <span>ด/บ สะสม:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="interest_accum" runat="server" ReadOnly="true" Style="text-align: right;"
                            ToolTip="#,##0.00" Width="97%"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td width="13%">
                    <div>
                        <span>เรียกเก็บล่าสุด:</span>
                    </div>
                </td>
                <td width="20%">
                    <div>
                        <asp:TextBox ID="lastprocess_date" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                    </div>
                </td>
                <td width="13%">
                    <div>
                        <span>สถานะการส่ง:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="COMPUTE_5" runat="server" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
                <td width="13%">
                    <div>
                        <span>สถานะสัญญา:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="COMPUTE_6" runat="server" ReadOnly="true" Width="97%"></asp:TextBox>
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
