<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.dlg.w_dlg_loan_receive_order_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView2" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table border="0">
            <tr>
                <td>
                    <div align="left">
                        <asp:CheckBox ID="print" runat="server" />
                        บันทึก&พิมพ์ใบเสร็จ
                    </div>
                </td>
            </tr>
            <tr>
                <table class="DataSourceFormView" style="width: 750px;" border="0">
                    <tr>
                        <td width="13%">
                            <div>
                                <span style="font-size: 11px;">ทะเบียนสมาชิก:</span>
                            </div>
                        </td>
                        <td width="17%">
                            <asp:TextBox ID="member_no" runat="server" Style="text-align: center; background-color: #FFFFCC;"></asp:TextBox>
                        </td>
                        <td width="13%">
                            <div>
                                <span style="font-size: 11px;">ชื่อ-นามสกุล:</span>
                            </div>
                        </td>
                        <td width="27%">
                            <asp:TextBox ID="name" runat="server" ReadOnly="true" Style="background-color: #FFFFCC;"></asp:TextBox>
                        </td>
                        <td width="13%">
                            <div>
                                <span style="font-size: 11px;">ประเภทสมาชิก :</span></div>
                        </td>
                        <td width="17%">
                            <asp:TextBox ID="COMPUTE_1" runat="server" Style="background-color: #FFFFCC;" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div>
                                <span style="font-size: 11px;">สังกัด :</span>
                            </div>
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="MEMBGROUP" runat="server" Style="width: 640px; background-color: #FFFFCC;"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </tr>
            <tr>
                <td>
                    <table class="DataSourceFormView" style="width: 500px;" border="0">
                        <tr>
                            <td colspan="4">
                                <u>รายละเอียดสัญญา</u>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <div>
                                    <span style="font-size: 11px;">เลขสัญญาที่ได้:</span>
                                </div>
                            </td>
                            <td width="25%">
                                <asp:TextBox ID="loancontract_no" runat="server" Style="text-align: center; background-color:Lime;"></asp:TextBox>
                            </td>
                            <td width="20%">
                                <div>
                                    <span style="font-size: 11px;">ประเภทเงินกู้ :</span>
                                </div>
                            </td>
                            <td colspan="3">
                                <%--<asp:TextBox ID="shrlontype_code" runat="server" Style="width: 40px;"></asp:TextBox>--%>
                                <asp:DropDownList ID="shrlontype_code" runat="server" Style="margin-left: 1px; width: 170px;"
                                    BackColor="#FFFFCC">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span style="font-size: 11px;">ยอดอนุมัติ :</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="bfloanapprove_amt" runat="server" Style="text-align: right; background-color: #FFFFCC;"
                                    ToolTip="#,##0.00"></asp:TextBox>
                            </td>
                            <td>
                                <div>
                                    <span style="font-size: 11px;">งวดส่ง :</span></div>
                            </td>
                            <td>
                                <asp:TextBox ID="PERIOD_PAYAMT" runat="server" Style="text-align: center; background-color: #FFFFCC;width: 50px;"></asp:TextBox>
                                <asp:Label ID="Label1" runat="server" Text="X" Style="text-align: center; width: 15px; " BorderColor="White" BackColor="White"></asp:Label>                                
                                <asp:TextBox ID="PERIOD_PAYMENT" runat="server" Style="text-align: center; background-color: #FFFFCC;width: 95px;"
                                    ToolTip="#,##0.00"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="15%">
                                <div>
                                    <span style="font-size: 11px;">ยอดรอเบิก :</span>
                                </div>
                            </td>
                            <td width="15%">
                                <asp:TextBox ID="bfwithdraw_amt" runat="server" Style="text-align: right; background-color: #FFFFCC"
                                    ToolTip="#,##0.00"></asp:TextBox>
                            </td>
                            <td colspan="2">
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table class="DataSourceFormView" style="width: 250px;" border="0">
                        <tr>
                            <td colspan="2">
                                <u>รายละเอียดการจ่ายเงิน</u>
                            </td>
                        </tr>
                        <tr>
                            <td width="45%">
                                <div>
                                    <span style="font-size: 11px;">วันที่จ่ายเงินกู้ :</span>
                                </div>
                            </td>
                            <td width="55%">
                                <asp:TextBox ID="slip_date" runat="server" Style="text-align: center; background-color: #FFFFCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span style="font-size: 11px;">ประเภทการรับเงิน :</span></div>
                            </td>
                            <td>
                                <asp:DropDownList ID="rcvperiod_flag" runat="server" Style="width: 130px;" BackColor="#FFFFCC">
                                    <asp:ListItem Value="0">รับทั้งหมด</asp:ListItem>
                                    <asp:ListItem Value="1">รับบางส่วน</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span style="font-size: 11px;">งวดจ่ายเงิน :</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="rcv_period" runat="server" Style="text-align: center; width: 30px;
                                    background-color: #FFFFCC;" ReadOnly="true"></asp:TextBox>
                                <asp:DropDownList ID="LNPAYMENT_STATUS" runat="server" Style="width: 97px;" BackColor="#FFFFCC">
                                    <asp:ListItem Value="1">เก็บต้นปกติ</asp:ListItem>
                                    <asp:ListItem Value="-11">งดส่งต้นเงิน</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <%-- <td>
                                <div>
                                    <span style="font-size: 11px;">งวดชำระ :</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="period_installment" runat="server" Style="text-align: center; background-color: #FFFFCC"></asp:TextBox>
                            </td>--%>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="DataSourceFormView" style="width: 500px;" border="0">
                        <tr>
                            <td colspan="4">
                                <u>จ่ายเงินกู้โดย</u>
                            </td>
                </td>
                <td width="15%">
                </td>
            </tr>
            <tr>
                <td width="20%">
                    <div>
                        <span style="font-size: 11px;">ประเภทเงิน :</span>
                    </div>
                </td>
                <td>
                    <%--<asp:TextBox ID="moneytype_code" runat="server" Style="width: 40px;"></asp:TextBox>--%>
                    <asp:DropDownList ID="moneytype_code" runat="server" Style="margin-left: 1px; width: 115px;"
                        BackColor="#FFFFCC">
                    </asp:DropDownList>
            </tr>
            <tr>
                <td colspan="4">
                    <u>บัญชีสมาชิก</u>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span style="font-size: 11px;">ธนาคาร :</span></div>
                </td>
                <td colspan="2">
                    <%-- <asp:TextBox ID="bank_code" runat="server" Style="width: 40px;"></asp:TextBox>--%>
                    <asp:DropDownList ID="expense_bank" runat="server" Style="width: 120px;" BackColor="#FFFFCC">
                    </asp:DropDownList>
                </td>
                <td width="17%">
                    <div>
                        <span style="font-size: 11px;">ค่าธรรมเนียม :</span></div>
                </td>
                <td>
                    <asp:TextBox ID="BANKFEE_AMT" runat="server" Style="text-align: right; background-color: #FFFFCC;"
                        ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span style="font-size: 11px;">สาขา :</span></div>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="expense_branch" runat="server" Style="width: 120px;" BackColor="#FFFFCC">
                    </asp:DropDownList>
                </td>
                <td>
                    <div>
                        <span style="font-size: 11px;">ค่าบริการ :</span></div>
                </td>
                <td width="20%">
                    <asp:TextBox ID="BANKSRV_AMT" runat="server" Style="text-align: right; background-color: #FFFFCC;"
                        ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span style="font-size: 11px;">เลขที่บัญชี :</span></div>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="expense_accid" runat="server" Style="width: 120px; background-color: #FFFFCC"></asp:TextBox>
                </td>
                <td colspan="2">
                </td>
            </tr>
        </table>
        </td>
        <td>
            <table class="DataSourceFormView" style="width: 250px;" border="0">
                <tr>
                    <td colspan="2">
                        <u>จำนวนเงิน</u>
                    </td>
                </tr>
                <tr>
                    <td width="40%">
                        <div>
                            <span style="font-size: 11px;">ยอดจ่ายเงินกู้ :</span>
                        </div>
                    </td>
                    <td width="60%">
                        <asp:TextBox ID="payout_amt" runat="server" Style="text-align: right; background-color: Black;"
                            ToolTip="#,##0.00" ForeColor="#CCFF33"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div>
                            <span style="font-size: 11px;">ยอดหักชำระ :</span>
                        </div>
                    </td>
                    <td>
                        <asp:TextBox ID="payoutclr_amt" runat="server" Style="text-align: right; background-color: Black;"
                            ToolTip="#,##0.00" ForeColor="#CCFF33"></asp:TextBox>
                    </td>
                </tr>
                
                <tr>
                    <td>
                        <div>
                            <span style="font-size: 11px;">ยอดคืนอื่นๆ :</span>
                        </div>
                    </td>
                    <td>
                        <asp:TextBox ID="returnetc_amt" runat="server" Style="text-align: right; background-color: Black;"
                            ToolTip="#,##0.00" ForeColor="#CCFF33"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div>
                            <span style="font-size: 11px;">ยอดรับเงินสุทธิ :</span>
                        </div>
                    </td>
                    <td>
                        <asp:TextBox ID="payoutnet_amt" runat="server" Style="text-align: right; background-color: Black;"
                            ToolTip="#,##0.00" ForeColor="#CCFF33"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
        </tr>
        <tr>
            <td>
                <table class="DataSourceFormView" style="width: 500px;" border="0">
                    <tr>
                        <td colspan="2">
                            <u>การเชื่อมโยงบัญชี</u>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            <div>
                                <span style="font-size: 11px;">รหัสบัญชี :</span>
                            </div>
                        </td>
                        <td width="80%">
                            <asp:DropDownList ID="tofrom_accid" runat="server" Style="width: 390px;" BackColor="#FFFFCC">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </EditItemTemplate>
</asp:FormView>
