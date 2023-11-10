<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDetail.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_auditloan_ctrl.DsDetail" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit" Width="510px">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 510px">
            <tr>
                <td colspan="6">
                    <strong style="font-size: 12px;">รายละเอียดสัญญา</strong>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เลขที่สัญญา:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="loancontract_no" runat="server" Style="text-align: center;" Enabled="false"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>ประเภทเงินกู้:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="loantype_code" runat="server" Enabled="False">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="16%">
                    <div>
                        <span>เริ่มสัญญา:</span>
                    </div>
                </td>
                <td width="17%">
                    <div>
                        <asp:TextBox ID="startcont_date" runat="server" Style="text-align: center;"></asp:TextBox>
                    </div>
                </td>
                <td width="16%">
                    <div>
                        <span>อายุสัญญา:</span>
                    </div>
                </td>
                <td width="15%">
                    <div>
                        <asp:TextBox ID="contract_time" runat="server" Style="text-align: center;"></asp:TextBox>
                    </div>
                </td>
                <td width="19%">
                    <div>
                        <span>สัญญาหมดอายุ:</span>
                    </div>
                </td>
                <td width="16%">
                    <div>
                        <asp:TextBox ID="expirecont_date" runat="server" Style="text-align: center;"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>วัตถุประสงค์:</span>
                    </div>
                </td>
                <td colspan="5">
                    <asp:DropDownList ID="loanobjective_code" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="1">
                    <div>
                        <span>ยอดอนุมัติ:</span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="loanapprove_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00">
                    </asp:TextBox>
                </td>
                
                <td >
                    <div>
                        <span>คงเหลือ:</span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="principal_balance" runat="server" Style="text-align: right;" ToolTip="#,##0.00">
                    </asp:TextBox>
                </td>
            
            
               <tr>
                <td colspan="1">
                    <div>
                        <span>ยอดรอเบิก:</span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="withdrawable_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00">
                    </asp:TextBox>
                </td>
            
           
                <td colspan="1">
                    <div>
                        <span>สถานะสัญญา:</span>
                    </div>
                </td>
                <td colspan="5">
                    <asp:DropDownList ID="contract_status" runat="server" Rows="5" SelectionMode="Single" Style="text-align: center;" ToolTip="#,##0.00">
                    <asp:ListItem Text="ปกติ" Value="1"></asp:ListItem>
                    <asp:ListItem Text="จบ" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="รับโอน" Value="11"></asp:ListItem>
                    <asp:ListItem Text="โอนให้ผู้ค้ำ" Value="-11"></asp:ListItem>
                    <asp:ListItem Text="ยกเลิก" Value="-9"></asp:ListItem>
                    
                    </asp:DropDownList>
                </td>
            
            </tr>
               <tr>
                <td colspan="1">
                   
                </td>
                <td colspan="2">
                </td>
            
           
                <td colspan="1"> <div>
                       <span>  บัญชีธนาคาร: </span>
                    </div>
                </td>
                <td colspan="5">
                    <asp:TextBox ID="expense_accid" runat="server" Style="text-align: right;" ToolTip="">
                    </asp:TextBox>
                </td>
            
            </tr>
        </table>
        <br />
        <table>
            <tr>
                <td>
                    <table class="DataSourceFormView" style="width: 252px">
                        <tr>
                            <td colspan="4">
                                <strong style="font-size: 12px; text-decoration: underline;">วันที่ล่าสุด:</strong>
                            </td>
                        </tr>
                        <tr>
                            <td width="35%">
                                <div>
                                    <span>จ่ายเงินกู้ล่าสุด:</span></div>
                            </td>
                            <td width="65%">
                                <asp:TextBox ID="lastreceive_date" runat="server" Style="text-align: center;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>วันชำระล่าสุด:</span></div>
                            </td>
                            <td>
                                <asp:TextBox ID="lastpayment_date" runat="server" Style="text-align: center;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>คิด ด/บ ล่าสุด:</span></div>
                            </td>
                            <td>
                                <asp:TextBox ID="lastcalint_date" runat="server" Style="text-align: center;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>เก็บได้ล่าสุด:</span></div>
                            </td>
                            <td>
                                <asp:TextBox ID="lastkeeping_date" runat="server" Style="text-align: center;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>เรียกเก็บล่าสุด:</span></div>
                            </td>
                            <td>
                                <asp:TextBox ID="lastprocess_date" runat="server" Style="text-align: center;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <strong style="font-size: 12px; text-decoration: underline;">ด/บ สะสม:</strong>
                            </td>
                        </tr>
                        <tr>
                            <td width="35%">
                                <div>
                                    <span>ด/บ ชำระแล้ว:</span></div>
                            </td>
                            <td width="65%">
                                <asp:TextBox ID="intpayment_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>ด/บ สะสมปีนี้:</span></div>
                            </td>
                            <td>
                                <asp:TextBox ID="interest_accum" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>สะสมปีก่อน:</span></div>
                            </td>
                            <td>
                                <asp:TextBox ID="intaccum_lastyear" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <strong style="font-size: 12px; text-decoration: underline;">ต้น/ดอกเบี้ยคืน:</strong>
                            </td>
                        </tr>
                        <tr>
                            <td width="35%">
                                <div>
                                    <span>ต้นคืน:</span></div>
                            </td>
                            <td width="65%">
                                <asp:TextBox ID="principal_return" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>ดอกเบี้ยคืน:</span></div>
                            </td>
                            <td>
                                <asp:TextBox ID="interest_return" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table class="DataSourceFormView" style="width: 252px">
                        <tr>
                            <td colspan="4">
                                <strong style="font-size: 12px; text-decoration: underline;">ค้างชำระ:</strong>
                            </td>
                        </tr>
                        <tr>
                            <td width="35%">
                                <div>
                                    <span>ด/บ ค้างชำระ:</span></div>
                            </td>
                            <td width="65%">
                                <asp:TextBox ID="interest_arrear" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>ด/บ ค้างเดือน:</span></div>
                            </td>
                            <td>
                                <asp:TextBox ID="intmonth_arrear" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>ด/บ ค้างรับ(ปี):</span></div>
                            </td>
                            <td>
                                <asp:TextBox ID="intyear_arrear" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>ต้นค้างชำระ:</span></div>
                            </td>
                            <td>
                                <asp:TextBox ID="principal_arrear" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <strong style="font-size: 12px; text-decoration: underline;">อื่นๆ:</strong>
                            </td>
                        </tr>
                         <tr>
                            <td width="35%">
                                <div>
                                    <span>งวดสูงสุด:</span></div>
                            </td>
                            <td width="65%">
                                <asp:TextBox ID="period_payamt" runat="server" Style="text-align: center;" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>งวดรับเงิน:</span></div>
                            </td>
                            <td>
                                <asp:TextBox ID="last_periodrcv" runat="server" Style="text-align: center;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>งวดชำระ:</span></div>
                            </td>
                            <td>
                                <asp:TextBox ID="last_periodpay" runat="server" Style="text-align: center;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>ลำดับ stm:</span></div>
                            </td>
                            <td>
                                <asp:TextBox ID="last_stm_no" runat="server" Style="text-align: center;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>ลำดับการโอน:</span></div>
                            </td>
                            <td>
                                <asp:TextBox ID="last_transcont_no" runat="server" Style="text-align: center;"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <b>หมายเหตุ:</b>
                </td>
            </tr>
            <tr>
                <td colspan="4" rowspan="4" valign="top">
                    <asp:TextBox ID="remark" runat="server" TextMode="MultiLine" Width="500px" Height="100px"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
