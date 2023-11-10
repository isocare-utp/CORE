<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsIntdetail.ascx.cs"
    Inherits="Saving.Applications.shrlon.ws_sl_cfloantype_ctrl.DsIntdetail" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="40%">
                    <strong><u>ข้อกำหนดอัตราดอกเบี้ย</u></strong>
                </td>
                <td width="60%">
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>กรณีคิดดอกเบี้ยเป็นขั้น ให้ดูขั้นดอกเบี้ยจาก :</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="intstep_type" runat="server">
                        <asp:ListItem Value="1">ไม่คิดเป็นขยัก</asp:ListItem>
                        <asp:ListItem Value="2">คิดเป็นขยัก</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>รูปแบบการคำนวณดอกเบี้ย (รายวัน/รายเดือน) :</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="interest_method" runat="server">
                        <asp:ListItem Value="1">คิดเป็นรายวัน</asp:ListItem>
                        <asp:ListItem Value="2">คิดเป็นรายเดือน</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>รูปแบบอัตราดอกเบี้ยที่เรียกเก็บ :</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="contint_type" runat="server">
                        <asp:ListItem Value="0">ไม่คิดดอกเบี้ย</asp:ListItem>
                        <asp:ListItem Value="1">อัตราดอกเบี้ยคงที่ตลอดอายุสัญญา</asp:ListItem>
                        <asp:ListItem Value="2">อัตราดอกเบี้ยตามตารางดอกเบี้ยสินเชื่อ</asp:ListItem>
                        <asp:ListItem Value="3">อัตราดอกเบี้ยพิเศษคิดเป็นช่วง</asp:ListItem>
                        <asp:ListItem Value="4">อัตราดอกเบี้ยตามตารางดอกเบี้ยเงินฝาก</asp:ListItem>
                        <asp:ListItem Value="5">อัตราดอกเบี้ยตามเงินฝากที่ใช้ค้ำ</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ดอกเบี้ยคงที่ดูอัตราขณะนั้นจากตาราง :</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="inttabfix_code" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ดอกเบี้ยจากตาราง :</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="inttabrate_code" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>อัตราดอกเบี้ยเพิ่ม/ลด ต่างหาก :</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="intrate_increase" runat="server" ToolTip="#,##0.0000" Style="text-align: center;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <strong><u>ข้อกำหนด ด/บ รายเดือน</u></strong>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table class="DataSourceRepeater">
                        <tr>
                            <th rowspan="2" width="20%">
                                ด/บ รายเดือน
                            </th>
                            <th colspan="2">
                                การรับเงินกู้
                            </th>
                            <th colspan="2">
                                การชำระเงินกู้
                            </th>
                        </tr>
                        <tr>
                            <th width="30%">
                                เงื่อนไข
                            </th>
                            <th width="10%">
                                วัน
                            </th>
                            <th width="30%">
                                เงื่อนไข
                            </th>
                            <th width="10%">
                                วัน
                            </th>
                        </tr>
                        <tr>
                            <th>
                                ช่วงเวลาไม่คิด ด/บ
                            </th>
                            <td>
                                <asp:DropDownList ID="calintrcv_nottype" runat="server">
                                    <asp:ListItem Value="0">ไม่มีช่วงการไม่คิด</asp:ListItem>
                                    <asp:ListItem Value="1">ระบุวันที่</asp:ListItem>
                                    <asp:ListItem Value="2">นับจากสิ้นเดือน</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="calintrcv_notdate" runat="server" Style="text-align: center;"></asp:TextBox>
                            </td>
                            <td>
                                <asp:DropDownList ID="calintpay_nottype" runat="server">
                                    <asp:ListItem Value="0">ไม่มีช่วงที่ไม่คิด</asp:ListItem>
                                    <asp:ListItem Value="1">ระบุวันที่</asp:ListItem>
                                    <asp:ListItem Value="2">นับจากต้นเดือน</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="calintpay_notdate" runat="server" Style="text-align: center;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                ช่วงคิด ด/บ ครึ่งเดือน
                            </th>
                            <td>
                                <asp:DropDownList ID="calintrcv_halftype" runat="server">
                                    <asp:ListItem Value="0">ไม่มีช่วงการไม่คิด</asp:ListItem>
                                    <asp:ListItem Value="1">ระบุวันที่</asp:ListItem>
                                    <asp:ListItem Value="2">นับจากสิ้นเดือน</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="calintrcv_halfdate" runat="server" Style="text-align: center;"></asp:TextBox>
                            </td>
                            <td>
                                <asp:DropDownList ID="calintpay_halftype" runat="server">
                                    <asp:ListItem Value="0">ไม่มีช่วงที่ไม่คิด</asp:ListItem>
                                    <asp:ListItem Value="1">ระบุวันที่</asp:ListItem>
                                    <asp:ListItem Value="2">นับจากต้นเดือน</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="calintpay_halfdate" runat="server" Style="text-align: center;"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
