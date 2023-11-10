<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsPaymentdet.ascx.cs"
    Inherits="Saving.Applications.shrlon.ws_sl_cfloantype_ctrl.DsPaymentdet" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="20%">
                    <strong><u>กำหนดการชำระ</u></strong>
                </td>
                <td width="42%">
                </td>
                <td width="38%">
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>รูปแบบการชำระคืน:</span></div>
                </td>
                <td>
                    <asp:DropDownList ID="loanpayment_type" runat="server">
                        <%--<asp:ListItem Value="0">ไม่มีการเรียกเก็บรายเดือน</asp:ListItem>--%>
                        <asp:ListItem Value="1">ต้นเท่ากันทุกงวด + ดอก</asp:ListItem>
                        <asp:ListItem Value="2">ต้น + ดอก เท่ากันทุกงวด</asp:ListItem>
                        <%--<asp:ListItem Value="3">เก็บแต่ดอกเบี้ย</asp:ListItem>--%>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:CheckBox ID="dropprncpay_flag" runat="server" />
                    งดเรียกเก็บต้นถ้ายังรับเงินกู้ไม่ครบ
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>การชำระงวดสุดท้าย:</span></div>
                </td>
                <td>
                    <asp:DropDownList ID="lastpayment_type" runat="server">
                        <asp:ListItem Value="" Text=""></asp:ListItem>
                        <asp:ListItem Value="0">จำนวนเงินที่เหลือ</asp:ListItem>
                        <asp:ListItem Value="1">ต้องเท่ากับทุกงวด</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:CheckBox ID="loanpayment_count" runat="server" />
                    ยอดชำระเงินกู้หักออกจากเงินเดือน
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <strong><u>รูปแบบการชำระหลังเกษียณ</u></strong>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>การชำระหลังเกษียณ:</span></div>
                </td>
                <td>
                    <asp:DropDownList ID="retryloansend_type" runat="server">
                        <asp:ListItem Value="" Text=""></asp:ListItem>
                        <asp:ListItem Value="0">ห้ามส่งเกินเกษียณ</asp:ListItem>
                        <asp:ListItem Value="1">ส่งเกินได้ตามงวดที่ระบุ - ไม่ตรวจสอบเงื่อนไข</asp:ListItem>
                        <asp:ListItem Value="2">ส่งเกินได้ตามงวดที่ระบุ - ตรวจสอบเงื่อนไข</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="retryloansend_time" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
