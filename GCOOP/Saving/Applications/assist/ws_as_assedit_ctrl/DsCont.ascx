<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsCont.ascx.cs" Inherits="Saving.Applications.assist.ws_as_assedit_ctrl.DsCont" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView2" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td style="width: 13%">
                </td>
                <td style="width: 17%">
                </td>
                <td style="width: 13%">
                </td>
                <td style="width: 17%">
                </td>
                <td style="width: 13%">
                </td>
                <td style="width: 27%">
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ประเภทสวัสดิการ:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="assisttype_code" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <div>
                        <span>ประจำปี:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="assist_year" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ประเภทการจ่าย:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="assistpay_code" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <div>
                        <span>วันที่อนุมัติ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="approve_date" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ยอดอนุมัติ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="approve_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ยอดรอเบิก :</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="withdrawable_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ทุนที่รับไปแล้ว :</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="pay_balance" runat="server" Style="text-align: right; font-weight: bold;"
                        ToolTip="#,##0.00" ForeColor="Red" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>งวดรับเงิน:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="last_periodpay" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>รับเงินล่าสุด:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="lastpay_date" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>สถานะการจ่าย:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="payment_status" runat="server" Style="text-align: center;">
                        <asp:ListItem Value="1" Text="ปกติ"></asp:ListItem>
                        <asp:ListItem Value="-1" Text="งดจ่ายสวัสดิการ"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ผู้รับทุน:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="ass_rcvname" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>บัตรผู้รับทุน:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="ass_rcvcardid" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>บัตรผู้ปกครอง</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="ass_prcardid" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ลำดับที่ STM</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="last_stm" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>สถานะทะเบียน</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="asscont_status" runat="server">
                        <asp:ListItem Value="1" Text="ปกติ"></asp:ListItem>
                        <asp:ListItem Value="-9" Text="ยกเลิก"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>สถานะทุนต่อเนื่อง</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="stm_flag" runat="server">
                        <asp:ListItem Value="0" Text="ทำจ่ายเอง"></asp:ListItem>
                        <asp:ListItem Value="1" Text="ทุนต่อเนื่อง"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <div>
                        <span>รูปแบบ</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="stmpay_type" runat="server">
                        <asp:ListItem Value="1" Text="รับตามช่วงเดือน"></asp:ListItem>
                        <asp:ListItem Value="2" Text="รับทุกปี(ระบุเดือน)"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <div>
                        <span>ช่วงเดือน/เดือน</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="stmpay_num" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>การจ่ายเงิน:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="moneytype_code" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <div>
                        <span>ธนาคาร:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="expense_bank" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>สาขา:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="expense_branch" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <div>
                        <span>เลขธนาคาร:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="expense_accid" runat="server" OnKeyPress="return chkNumber(this)"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>โอนไประบบ:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="send_system" runat="server">
                        <asp:ListItem Value="">กรุณาเลือกระบบ</asp:ListItem>
                        <asp:ListItem Value="DEP">เงินฝาก</asp:ListItem>
                        <asp:ListItem Value="LON">สินเชื่อ</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <div>
                        <span>เลขที่บัญชี:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="deptaccount_no" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
