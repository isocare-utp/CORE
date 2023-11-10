<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsNPL.ascx.cs" Inherits="Saving.Applications.shrlon.w_sheet_ln_nplmaster_ctrl.DsNPL" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table>
            <tr>
                <td valign="top" align="center">
                    <asp:Button ID="b_getdata" runat="server" Text="ดึงข้อมูล" Style="width: 345px; height: 24px;" />
                    <table class="DataSourceFormView" style="width: 350px;">
                        <tr>
                            <td style="width: 25%;">
                                <div>
                                    <span>เป็นหนี้</span>
                                </div>
                            </td>
                            <td style="width: 25%;">
                                <div>
                                    <asp:DropDownList ID="lawtype_code" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </td>
                            <td style="width: 25%;">
                                <div>
                                    <span>ชำระ/งวด</span>
                                </div>
                            </td>
                            <td style="width: 25%;">
                                <div>
                                    <asp:TextBox ID="period_payment" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>ชั้นหนี้เดิม</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:DropDownList ID="lawtype_code_old" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <span>คิด ด/บ ล่าสุด</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="lastcalint_date" runat="server" Style="text-align: center; background-color: #000000;
                                        color: #00FF00;" ReadOnly="True"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>ศาล</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="court_name" runat="server"></asp:TextBox>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <span>ด/บ ค้าง</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="interest_arrear" runat="server" Style="text-align: right; background-color: #000000;
                                        color: #00FF00;" ToolTip="#,##0.00" ReadOnly="True"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>เงินต้นฟ้อง</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="indict_prnamt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <span>ด/บ ประมาณ</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="compute_2" runat="server" Style="text-align: right; background-color: #000000;
                                        color: #00FF00;" ToolTip="#,##0.00" ReadOnly="True"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>ดอกเบี้ยฟ้อง</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="indict_intamt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                                </div>
                            </td>
                            <td colspan="2" rowspan="5" valign="bottom" align="left">
                                <fieldset style="width: 164px; font-family: Tahoma; font-size: 13px;">
                                    <legend style="font-family: Tahoma; font-size: 14px; height: 17px;">สรุปสาระสำคัญ</legend>
                                    <asp:TextBox ID="result_require" runat="server" TextMode="MultiLine" Style="width: 160px;
                                        height: 80px; border: none; font-family: Tahoma; font-size: 13px;"></asp:TextBox>
                                </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>วันที่ฟ้อง</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="indict_date" runat="server" Style="text-align: center;" ReadOnly="True"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>วันตัดสิน</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="judge_date" runat="server" Style="text-align: center;" ReadOnly="True"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>คดีดำที่</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="case_blackno" runat="server"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>คดีแดงที่</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="case_redno" runat="server"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <fieldset style="margin-top: 4px;">
                        <legend style="font-family: Tahoma; font-size: 14px;">หมายเหตุ</legend>
                        <asp:TextBox ID="remark" runat="server" TextMode="MultiLine" Style="width: 345px;
                            height: 50px; border: none; font-family: Tahoma; font-size: 13px;"></asp:TextBox>
                    </fieldset>
                </td>
                <td valign="top">
                    <table class="DataSourceFormView" style="width: 350px;">
                        <tr>
                            <td width="28%">
                                <div>
                                    <span style="background-color: #009900; color: Yellow;">ต้นเงินยกมา</span>
                                </div>
                            </td>
                            <td width="22%">
                                <div>
                                    <asp:TextBox ID="prince_last_year" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                                </div>
                            </td>
                            <td width="28%">
                                <div>
                                    <span style="background-color: #009900; color: Yellow;">ดอกเบี้ยยกมา</span>
                                </div>
                            </td>
                            <td width="22%">
                                <div>
                                    <asp:TextBox ID="int_last_year" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span style="background-color: #009900; color: Yellow;">ต้นเงินปัจจุบัน</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="principal_balance" runat="server" Style="text-align: right; background-color: #000000;
                                        color: #00FF00;" ToolTip="#,##0.00" ReadOnly="True"></asp:TextBox>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <span style="background-color: #009900; color: Yellow;">ดอกเบี้ยปัจจุบัน</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="int_balance" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span style="background-color: #009900; color: Yellow;">ชำระระหว่างปี</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="compute_3" runat="server" Style="text-align: right; background-color: #000000;
                                        color: #00FF00;" ToolTip="#,##0.00" ReadOnly="True"></asp:TextBox>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <span style="background-color: #009900; color: Yellow;">ชำระระหว่างปี</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="compute_4" runat="server" Style="text-align: right; background-color: #000000;
                                        color: #00FF00;" ToolTip="#,##0.00" ReadOnly="True"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>การตั้งค่าเผื่อ</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:DropDownList ID="percent_settingpayment" runat="server">
                                        <asp:ListItem Value="0" Text="0%"></asp:ListItem>
                                        <asp:ListItem Value="20" Text="20%"></asp:ListItem>
                                        <asp:ListItem Value="50" Text="50%"></asp:ListItem>
                                        <asp:ListItem Value="70" Text="70%"></asp:ListItem>
                                        <asp:ListItem Value="100" Text="100%"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <span>การตั้งค่าเผื่อ</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="setting_payment" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span style="background-color: #009900; color: Yellow;">ลงวันที่รับเรื่อง</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="received_date" runat="server" Style="text-align: center;" ReadOnly="True"></asp:TextBox>
                                </div>
                            </td>
                            <td colspan="2">
                                <div>
                                    <span style="text-align: center; background-color: #000099; color: #66FFFF; width: 98%;">
                                        *เงินทดรอง</span>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span style="background-color: #009900; color: Yellow;">ลงวันที่สัญญา</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="loancontract_date" runat="server" Style="text-align: center;" ReadOnly="True"></asp:TextBox>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <span>เงินยืมทดรอง</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="margin" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>ลำดับงาน</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:DropDownList ID="work_order" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <span>ประมาณการชำระ</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="advance_payamt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>ชั้นลูกหนี้</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:DropDownList ID="debtor_class" runat="server">
                                        <asp:ListItem Value="" Text=""></asp:ListItem>
                                        <asp:ListItem Value="A" Text="A"></asp:ListItem>
                                        <asp:ListItem Value="B" Text="B"></asp:ListItem>
                                        <asp:ListItem Value="C" Text="C"></asp:ListItem>
                                        <asp:ListItem Value="D" Text="D"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <span>ยอดหนี้คงเหลือ</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="compute_5" runat="server" Style="text-align: right; background-color: #000000;
                                        color: #00FF00;" ToolTip="#,##0.00" ReadOnly="True"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>สถานะ</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:DropDownList ID="status" runat="server">
                                        <asp:ListItem Value="0" Text=""></asp:ListItem>
                                        <asp:ListItem Value="1" Text="ผู้กู้ชำระหนี้"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="ผู้ค้ำชำระหนี้"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <span>หนี้พิพากษาลดลง</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="compute_6" runat="server" Style="text-align: right; background-color: #000000;
                                        color: #00FF00; width: 50px;" ToolTip="#,##0.00" ReadOnly="True"></asp:TextBox>
                                    <input type="text" readonly="readonly" value="%" style="text-align: center; background-color: #000000;
                                        color: #00FF00; width: 15px" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>ออกหมายบังคับ</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="enforcement_date" runat="server" Style="text-align: center;" ReadOnly="True"></asp:TextBox>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <span>ด/บ พิพากษา</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="judge_intrate" runat="server" Style="text-align: right; background-color: #000000;
                                        color: #00FF00; width: 50px;" ToolTip="#,##0.00" ReadOnly="True"></asp:TextBox>
                                    <input type="text" readonly="readonly" value="%" style="text-align: center; background-color: #000000;
                                        color: #00FF00; width: 15px" />
                                </div>
                            </td>
                        </tr>
                    </table>
                    <fieldset>
                        <legend style="font-family: Tahoma; font-size: 14px;">ผลคำพิพากษา</legend>
                        <asp:TextBox ID="judge_desc" runat="server" TextMode="MultiLine" Style="width: 345px;
                            height: 50px; border: none; font-family: Tahoma; font-size: 13px;"></asp:TextBox>
                    </fieldset>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
