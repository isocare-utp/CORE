<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDetail.ascx.cs" Inherits="Saving.Applications.mbshr.ws_sl_sharetype_detail_ctrl.DsDetail" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td colspan="2">
                    <strong><u>รายละเอียดหุ้น</u></strong>
                </td>
                <td colspan="2">
                    <strong><u>ลดส่งค่าหุ้น</u></strong>
                </td>
            </tr>
            <tr>
                <td width="30%">
                    <div>
                        <span>กลุ่มของหุ้น:</span></div>
                </td>
                <td width="20%">
                    <asp:DropDownList ID="sharegroup_code" runat="server">
                        <asp:ListItem Value=""> </asp:ListItem>
                        <asp:ListItem Value="01">หุ้นสามัญ</asp:ListItem>
                        <asp:ListItem Value="02">หุ้นสมทบ</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td width="30%">
                    <div>
                        <span>งวดต่ำสุดที่ลดส่งได้:</span></div>
                </td>
                <td width="20%">
                    <asp:TextBox ID="timeminshare_low" runat="server" Style="text-align: right" ToolTip="#,##0"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>มูลค่า/หุ้น:</span></div>
                </td>
                <td>
                    <asp:TextBox ID="share_value" runat="server" Style="text-align: right" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>จำนวนหุ้นขั้นต่ำที่ลดส่งได้:</span></div>
                </td>
                <td>
                    <asp:TextBox ID="minshare_low" runat="server" Style="text-align: right" ToolTip="#,##0"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>การใช้หุ้นคำนวณสิทธิเงินกู้:</span></div>
                </td>
                <td>
                    <asp:DropDownList ID="callonprms_status" runat="server">
                        <asp:ListItem Value="0">ไม่ใช้คำนวณ</asp:ListItem>
                        <asp:ListItem Value="1">ใช้คำนวณ</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <strong><u>งดส่งค่าหุ้น</u></strong>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <strong><u>การถือครองหุ้น</u></strong>
                </td>
                <td>
                    <div>
                        <span>ต้องส่งหุ้นมาแล้วงวด:</span></div>
                </td>
                <td>
                    <asp:TextBox ID="timeminshare_stop" runat="server" Style="text-align: right" ToolTip="#,##0"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>จำนวนหุ้นขั้นต่ำที่ต้องถือ:</span></div>
                </td>
                <td>
                    <asp:TextBox ID="minshare_hold" runat="server" Style="text-align: right" ToolTip="#,##0"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>จำนวนหุ้นขั้นต่ำที่งดส่งได้:</span></div>
                </td>
                <td>
                    <asp:TextBox ID="minshare_stop" runat="server" Style="text-align: right" ToolTip="#,##0"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>จำนวนหุ้นสูงสุดที่ถือได้:</span></div>
                </td>
                <td>
                    <asp:TextBox ID="maxshare_hold" runat="server" Style="text-align: right" ToolTip="#,##0"></asp:TextBox>
                </td>
                <td colspan="2">
                    <strong><u>การนับการเปลี่ยนแปลงการส่งหุ้นภายใน 1 ปี</u></strong>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <strong><u>การชำระพิเศษ</u></strong>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="chgcount_type" runat="server">
                        <asp:ListItem Value="1">นับรวมทุกการเปลี่ยนแปลง</asp:ListItem>
                        <asp:ListItem Value="2">นับแยกรายการ</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ซื้อหุ้นก่อนวันที่นับเป็นเดือนปัจจุบัน:</span></div>
                </td>
                <td>
                    <asp:TextBox ID="buyshare_before" runat="server" Style="text-align: right" ToolTip="#,##0"></asp:TextBox>
                </td>
                <td>
                    <u>นับรวมทุกการเปลี่ยนแปลง</u>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>นับงวดสำหรับการซื้อหุ้นซื้อหุ้นพิเศษ:</span></div>
                </td>
                <td>
                    <asp:DropDownList ID="countperiod_status" runat="server">
                        <asp:ListItem Value="0">ไม่นับงวด</asp:ListItem>
                        <asp:ListItem Value="1">นับงวด</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <div>
                        <span>รวมแล้วไม่เกิน:</span></div>
                </td>
                <td>
                    <asp:TextBox ID="chgcountall_amt" runat="server" Style="text-align: right" ToolTip="#,##0"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <strong><u>การคิดเงินปันผล</u></strong>
                </td>
                <td>
                    <u>นับแยกรายการ</u>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>จำนวนผิดนัดชำระที่ไม่ได้ปันผล:</span></div>
                </td>
                <td>
                    <asp:TextBox ID="maxmiss_pay" runat="server" Style="text-align: right" ToolTip="#,##0"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ขอเพิ่มหุ้นไม่เกิน:</span></div>
                </td>
                <td>
                    <asp:TextBox ID="chgcountadd_amt" runat="server" Style="text-align: right" ToolTip="#,##0"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <strong><u>การอัพเดทฐานหุ้นตามเงินเดือน</u></strong>
                </td>
                <td>
                </td>
                <td>
                    <div>
                        <span>ขอลดหุ้นไม่เกิน:</span></div>
                </td>
                <td>
                    <asp:TextBox ID="chgcountlow_amt" runat="server" Style="text-align: right" ToolTip="#,##0"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div> <span> เปลี่ยนฐานหุ้นเมื่อมีการปรับเงินเดือน: </span></div>
                </td>
                <td>
                    <asp:DropDownList ID="adjsalarychgshrperiod_flag" runat="server">
                        <asp:ListItem Value="0">ไม่เปลี่ยนแปลง</asp:ListItem>
                        <asp:ListItem Value="1">เปลี่ยนแปลง</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <div>
                        <span>ขอหยุดส่งหุ้นไม่เกิน:</span></div>
                </td>
                <td>
                    <asp:TextBox ID="chgcountstop_amt" runat="server" Style="text-align: right" ToolTip="#,##0"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td>
                    <div>
                        <span>ขอส่งหุ้นต่อไม่เกิน:</span></div>
                </td>
                <td>
                    <asp:TextBox ID="chgcountcont_amt" runat="server" Style="text-align: right" ToolTip="#,##0"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
