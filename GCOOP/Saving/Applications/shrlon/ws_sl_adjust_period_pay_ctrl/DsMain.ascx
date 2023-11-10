<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_adjust_period_pay_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit" Width="480px">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="20%">
                    <div>
                        <span>ทะเบียน:</span>
                    </div>
                </td>
                <td width="20%">
                    <div>
                        <asp:TextBox ID="member_no" runat="server" Style="text-align: center"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>ชื่อ - สกุล:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="cp_name" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
        </table>
        <table class="DataSourceFormView">
            <tr>
                <td>
                    <table>
                        <tr>
                            <td width="15%">
                                <div>
                                    <span>สัญญา:</span>
                                </div>
                            </td>
                            <td width="20%">
                                <div>
                                    <asp:DropDownList ID="loancontract_no" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>ยอดอนุมัติ:</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="loanapprove_amt" runat="server" Style="text-align: right" ToolTip="#,##0.00"
                                        ReadOnly="true"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>คงเหลือ:</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="bfprnbal_amt" runat="server" Style="text-align: right" ToolTip="#,##0.00"
                                        ReadOnly="true"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>งวด:</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="cp_period" runat="server" Style="text-align: center" ReadOnly="true"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>ดอกเบี้ย:</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="INT_CONTINTRATE" runat="server" Style="text-align: right" ToolTip="#,##0.00"
                                        ReadOnly="true"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table>
                        <tr>
                            <td width="15%">
                                <div>
                                    <span>งวดชำระเก่า:</span>
                                </div>
                            </td>
                            <td width="20%">
                                <div>
                                    <asp:TextBox ID="oldperiod_payment" runat="server" Style="text-align: center" ToolTip="#,##0.00" ReadOnly="true"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>งวดชำระใหม่:</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="period_payment" runat="server" Style="text-align: center" ToolTip="#,##0.00"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>ส่วนต่าง:</span>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:TextBox ID="cp_div" runat="server" Style="text-align: center" ReadOnly="true" ToolTip="#,##0.00"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        </td> </tr> </table>
    </EditItemTemplate>
</asp:FormView>
