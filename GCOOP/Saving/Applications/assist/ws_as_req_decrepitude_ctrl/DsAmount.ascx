<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsAmount.ascx.cs" Inherits="Saving.Applications.assist.ws_as_req_decrepitude_ctrl.DsAmount" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<script type="text/javascript">
    function chkNumber(ele) {
        var vchar = String.fromCharCode(event.keyCode);
        if ((vchar < '0' || vchar > '9') && (vchar != '.')) return false;
        ele.onKeyPress = vchar;
    }

</script>
<br />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table>
            <tr>
                <td>
                    <table class="DataSourceFormView" style="width: 450px;">
                        <tr>
                            <td width="20%">
                                <div>
                                    <span>การจ่ายเงิน:</span>
                                </div>
                            </td>
                            <td width="25%">
                                <asp:DropDownList ID="moneytype_code" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td width="20%">
                                <div>
                                    <span>ธนาคาร:</span>
                                </div>
                            </td>
                            <td width="25%">
                                <asp:DropDownList ID="expense_bank" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>เลขธนาคาร:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="expense_accid" runat="server" OnKeyPress="return chkNumber(this)"></asp:TextBox>
                            </td>
                            <td>
                                <div>
                                    <span>สาขา:</span>
                                </div>
                            </td>
                            <td>
                                <asp:DropDownList ID="expense_branch" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>โอนไประบบ:</span>
                                </div>
                            </td>
                            <td>
                                <asp:DropDownList ID="send_system" runat="server">
                                    <asp:ListItem Value="MRT">เงินรอจ่ายคืน</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <div>
                                    <span>เลขที่บัญชี:</span>
                                </div>
                            </td>
                            <td width="15%">
                                <asp:DropDownList ID="deptaccount_no" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                         <tr>
                            <td>
                                <div>
                                    <span>หมายเหตุ:</span>
                                </div>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="remark" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>หนี้ทั้งหมด:</span>
                                </div>
                            </td>
                            <td >
                                <asp:TextBox ID="principal_balance" runat="server" Style="text-align: right" ToolTip="#,##0.00" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td>
                                <div>
                                    <span>หนี้ที่คำนวณหัก:</span>
                                </div>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="principal_cal" runat="server" Style="text-align: right" ToolTip="#,##0.00" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>จำนวนหุ้น:</span>
                                </div>
                            </td>
                            <td >
                                <asp:TextBox ID="sharestk_amt" runat="server" Style="text-align: right" ToolTip="#,##0.00" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td>
                                <div>
                                    <span>มูลค่าหุ้น:</span>
                                </div>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="share_value" runat="server" Style="text-align: right" ToolTip="#,##0.00" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                        <td colspan="2"style="color:white;">.</td>
                        </tr>
                        <tr>
                        <td colspan="2"style="color:white;">.</td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table class="DataSourceFormView" style="width: 300px;">
                        <tr>
                            <td>
                                <div>
                                    <span>ยอดเงินที่อนุมัติ:</span>
                                </div>
                            </td>
                            <td width="150px">
                                <asp:TextBox ID="assist_amt" runat="server" BackColor="#00000" ForeColor="#CCFF33"
                                     ToolTip="#,##0.00" Style="font-size: 20px; text-align: right;"
                                    OnKeyPress="return chkNumber(this)" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>ยอดเงินที่เคยได้:</span>
                                </div>
                            </td>
                            <td width="150px">
                                <asp:TextBox ID="assistever_amt" runat="server" BackColor="#00000" ForeColor="#ff66ff"
                                    ReadOnly="true" ToolTip="#,##0.00" Style="font-size: 20px; text-align: right;"
                                    OnKeyPress="return chkNumber(this)"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>ยอดเงินที่หักออก:</span>
                                </div>
                            </td>
                            <td width="150px">
                                <asp:TextBox ID="assistcut_amt" runat="server" BackColor="#00000" ForeColor="#ff0000"
                                     ToolTip="#,##0.00" Style="font-size: 20px; text-align: right;"
                                    OnKeyPress="return chkNumber(this)" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr >
                            <td >
                                <div >
                                    <span >ยอดเงินสุทธิ:</span>
                                </div>
                            </td>
                            <td width="150px" >
                                <asp:TextBox ID="assistnet_amt" runat="server" BackColor="#00000" ForeColor="#66ccff"
                                    ToolTip="#,##0.00" Style="font-size: 20px; text-align: right;" OnKeyPress="return chkNumber(this)" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <!--<tr>
                        <td colspan="2"style="color:white;">.</td>
                        </tr>
                        <tr>
                        <td colspan="2"style="color:white;">.</td>
                        </tr>
                        <tr>
                        <td colspan="2"style="color:white;">.</td>
                        </tr>-->
                    </table>
                        <br>
                        <br>
                        <br>
                        <br>
                        <br>
                        
                        
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
