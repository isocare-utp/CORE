<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.mbshr.ws_sl_share_withdraw_ctrl.ws_dlg_share_withdraw_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table>
            <tr>
                <td colspan="2">
                    <table class="DataSourceFormView" style="width: 750px;" border="0">
                        <tr>
                            <td colspan="2">
                                <u>รายละเอียดการถอนหุ้น</u>
                            </td>
                        </tr>
                        <tr>
                            <td width="15%">
                                <div>
                                    <span>ทะเบียน: </span>
                                </div>
                            </td>
                            <td width="40%">
                                <asp:TextBox ID="member_no" runat="server" Style="text-align: center; width: 80px;
                                    background-color: ButtonFace;" ReadOnly="true"></asp:TextBox>
                                <asp:TextBox ID="cp_mbname" runat="server" ReadOnly="true" Style="text-align: left;
                                    background-color: ButtonFace; width: 200px; margin-left: 2px;"></asp:TextBox>
                            </td>
                            <td width="20%">
                                <div>
                                    <span>วันทำการ: </span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="operate_date" runat="server" Style="text-align: center; background-color: ButtonFace;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>หน่วย: </span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="membgroup_code" runat="server" Style="text-align: center; width: 80px;
                                    background-color: ButtonFace;" ReadOnly="true"></asp:TextBox>
                                <asp:TextBox ID="membgroup_desc" runat="server" ReadOnly="true" Style="text-align: left;
                                    background-color: ButtonFace; width: 200px; margin-left: 2px;"></asp:TextBox>
                            </td>
                            <td>
                                <div>
                                    <span>วันที่ถอนหุ้น: </span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="slip_date" runat="server" Style="text-align: center;"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="DataSourceFormView" style="width: 300px;" border="0">
                        <tr>
                            <td colspan="2">
                                <u>รายละเอียดหุ้น</u>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>ประเภทหุ้น: </span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="cp_sharetype" runat="server" Style="text-align: left; background-color: ButtonFace"
                                    ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>สถานะหุ้น: </span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="cp_bfsharestatus" runat="server" Style="text-align: center; background-color: ButtonFace;"
                                    ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>หุ้นคงเหลือ: </span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="bfshrcont_balamt" runat="server" Style="text-align: right; background-color: ButtonFace;"
                                    ReadOnly="true" ToolTip="#,##0.00"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <u>ยอดถอนหุ้น</u>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>ยอดถอนหุ้น: </span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="payout_amt" runat="server" Style="text-align: right; background-color: Aqua"
                                    ReadOnly="true" ToolTip="#,##0.00"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>ยอดโอนชำระ: </span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="payoutclr_amt" runat="server" Style="text-align: right; background-color: Red;"
                                    ForeColor="Yellow" ReadOnly="true" ToolTip="#,##0.00"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>คงเหลือสุทธิ: </span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="payoutnet_amt" runat="server" Style="text-align: right; background-color: Black;"
                                    ForeColor="Lime" ReadOnly="true" ToolTip="#,##0.00"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table class="DataSourceFormView" style="width: 390px;" border="0">
                        <tr>
                            <td colspan="4">
                                <u>ทำรายการโดย</u>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <div>
                                    <span>การจ่าย: </span>
                                </div>
                            </td>
                            <td width="30%">
                                <div>
                                    <asp:DropDownList ID="moneytype_code" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </td>
                            <td width="22%">
                                <div>
                                    <span>คู่บัญชี: </span>
                                </div>
                            </td>
                            <td>
                                <asp:DropDownList ID="tofrom_accid" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <u>โอนธนาคาร:</u>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>ธนาคาร: </span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:DropDownList ID="expense_bank" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <span>สาขา: </span>
                                </div>
                            </td>
                            <td>
                                <asp:DropDownList ID="expense_branch" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div>
                                    <span>เลขบัญชี/ เลขที่เช็ค: </span>
                                </div>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="expense_accid" runat="server" Style="text-align: center;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>ค่าบริการ: </span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="banksrv_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                            </td>
                            <td>
                                <div>
                                    <span>ค่าธรรมเนียม: </span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="bankfee_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <u>ผู้รับโอนผลประโยชน์</u>
                            </td>
                            <td colspan="2">
                                <asp:CheckBox ID="cp_gainflag" runat="server" Text="มีผู้รับโอนผลประโยชน์" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
    </EditItemTemplate>
</asp:FormView>
