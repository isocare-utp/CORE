<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_slip_pay_ctrl.w_dlg_sl_detail_share_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <span style="font-size: 12px;"><font color="#cc0000"><u><strong>รายละเอียด</strong></u></font></span>
        <table class="FormStyle" style="width: 700px;">
            <tr>
                <td width="15%">
                    <div>
                        <span style="font-size: 12px;">ชื่อ-ชื่อสกุล:</span>
                    </div>
                </td>
                <td colspan="2" width="35%">
                    <asp:TextBox ID="cp_name" runat="server" Style="font-size: 12px;"></asp:TextBox>
                </td>
                <td width="15%">
                    <div>
                        <span style="font-size: 12px;">ประเภทหุ้น:</span>
                    </div>
                </td>
                <td colspan="2" width="35%">
                    <asp:TextBox ID="cp_sharetype" runat="server" Style="font-size: 12px;"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table class="FormStyle" style="width: 700px;">
            <tr>
                <td colspan="2">
                    <span style="font-size: 12px;"><font color="#0066cc"><u><strong>ทุนเรือนหุ้น</strong></u></font></span>
                </td>
                <td>
                    <%--<span style="font-size: 12px;"><font color="#0066cc"><u><strong>หุ้นค้างจ่าย</strong></u></font></span>--%>
                </td>
            </tr>
            <tr>
                <td width="15%">
                    <div>
                        <span style="font-size: 12px;">ยกมาต้นปี:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="cp_sharebegin" runat="server" Style="text-align: right; font-size: 12px;"
                        ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td width="16%">
                    <span style="font-size: 12px;">หุ้นต่อเดือน:</span>
                </td>
                <td>
                    <asp:TextBox ID="cp_periodshare" runat="server" Style="text-align: right; font-size: 12px;"
                        ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td width="15%">
                    <div>
                        <span style="font-size: 12px;">งวดล่าสุด:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="last_period" runat="server" Style="text-align: center; font-size: 12px;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span style="font-size: 12px;">ค่าหุ้นคงเหลือ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="cp_sharestk" runat="server" Style="text-align: right; font-size: 12px;"
                        ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                    <span style="font-size: 12px;">หุ้นฐาน งด:</span>
                </td>
                <td>
                    <asp:TextBox ID="cp_periodbase" runat="server" Style="text-align: right; font-size: 12px;"
                        ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                    <span style="font-size: 12px;">การส่งหุ้น:</span>
                </td>
                <td>
                    <asp:TextBox ID="cp_payment_status" runat="server" Style="text-align: center; font-size: 12px;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <u style="font-size: 12px;">ผ่อนผัน</u>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span style="font-size: 12px;">การผ่อนผัน:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="cp_compound_status" runat="server" Style="font-size: 12px;"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span style="font-size: 12px;">สถานะหุ้น:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="sharemaster_status" runat="server" Style="font-size: 12px;">
                        <asp:ListItem Value="0" Text=""></asp:ListItem>
                        <asp:ListItem Value="1">ปกติ</asp:ListItem>
                        <asp:ListItem Value="5">หุ้นค้าง</asp:ListItem>
                        <asp:ListItem Value="-1">ปิดบัญชีหุ้น</asp:ListItem>
                        <asp:ListItem Value="8">หุ้นรอจัดสรร</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
