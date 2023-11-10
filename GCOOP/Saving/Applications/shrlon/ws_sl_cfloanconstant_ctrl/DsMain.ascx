<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_cfloanconstant_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="25%">
                    <strong><u>ข้อกำหนดเงินกู้</u></strong>
                </td>
                <td width="25%">
                </td>
                <td colspan="2">
                    <strong style="font-size: 12px"><u>การค้ำประกันของสมาชิกปกติ</u></strong>
                </td>
                <td colspan="2">
                    <strong style="font-size: 12px"><u>การค้ำประกันของสมาชิกสมทบ</u></strong>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>จำนวนเงินกู้สูงสุด</span></div>
                </td>
                <td>
                    <asp:TextBox ID="maxallloan_amount" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td width="17%">
                    <asp:CheckBox ID="grtright_contflag" runat="server" />
                    ไม่เกินสัญญา
                </td>
                <td width="8%">
                    <asp:TextBox ID="grtright_contract" runat="server" Width="50px" Style="text-align: right;"></asp:TextBox>
                </td>
                <td width="17%">
                    <asp:CheckBox ID="grtmemco_contflag" runat="server" />
                    ไม่เกินสัญญา
                </td>
                <td width="8%">
                    <asp:TextBox ID="grtmemco_contract" runat="server" Width="50px" Style="text-align: right;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <strong><u>รูปแบบเลขสัญญา</u></strong>
                </td>
                <td>
                </td>
                <td>
                    <asp:CheckBox ID="grtright_memflag" runat="server" />
                    ไม่เกินคน
                </td>
                <td>
                    <asp:TextBox ID="grtright_member" runat="server" Width="50px" Style="text-align: right;"></asp:TextBox>
                </td>
                <td>
                    <asp:CheckBox ID="grtmemco_memflag" runat="server" />
                    ไม่เกินคน
                </td>
                <td>
                    <asp:TextBox ID="grtmemco_member" runat="server" Width="50px" Style="text-align: right;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ปีในเลขสัญญา:</span></div>
                </td>
                <td>
                    <asp:DropDownList ID="formatyear_type" runat="server">
                        <asp:ListItem Value="1">ปีปฏิทิน</asp:ListItem>
                        <asp:ListItem Value="2">ปีบัญชี</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="4">
                    <strong><u>การชำระแบบคงยอด</u></strong><br />
                    <strong><u>งวดแรก</u></strong>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เดือนในเลขสัญญา:</span></div>
                </td>
                <td>
                    <asp:DropDownList ID="formatmonth_type" runat="server">
                        <asp:ListItem Value="1">เดือนปฏิทิน</asp:ListItem>
                        <asp:ListItem Value="2">เดือนบัญชี</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="2">
                    <div>
                        <span>ถ้าด/บเกินเดือน เก็บเงินต้น:</span></div>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="fixpayintoverfst_type" runat="server">
                        <asp:ListItem Value="1">คงต้นของเดือนนั้นไว้</asp:ListItem>
                        <asp:ListItem Value="2">ลดต้นของเดือนนั้นลง</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>รูปแบบเลขที่สัญญา:</span></div>
                </td>
                <td>
                    <asp:TextBox ID="contract_format" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td colspan="2">
                    <div>
                        <span>กรณีลดต้นแล้วติดลบ:</span></div>
                    <strong><u>งวดต่อไป</u></strong>
                </td>
                <td colspan="2" valign="top">
                    <asp:DropDownList ID="fixpayintoverfstprn_type" runat="server">
                        <asp:ListItem Value="0" Text=""></asp:ListItem>
                        <asp:ListItem Value="1">ปรับต้นเป็นศูนย์</asp:ListItem>
                        <asp:ListItem Value="2">กลับยอดติดลบให้เป็นบวก</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <strong><u>การนับวันการคิด ด/บ</u></strong>
                </td>
                <td>
                </td>
                <td colspan="2">
                    <div>
                        <span>ถ้าด/บเกินเดือน เก็บเงินต้น:</span></div>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="fixpayintovernxt_type" runat="server">
                        <asp:ListItem Value="1">คงต้นของเดือนนั้นไว้</asp:ListItem>
                        <asp:ListItem Value="2">ลดต้นของเดือนนั้นลง</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>จำนวนวันในรอบปี:</span></div>
                </td>
                <td>
                    <asp:TextBox ID="dayinyear" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td colspan="2">
                    <div>
                        <span>กรณีลดต้นแล้วติดลบ:</span></div>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="fixpayintovernxtprn_type" runat="server">
                        <asp:ListItem Value="0" Text=""></asp:ListItem>
                        <asp:ListItem Value="1">ปรับต้นเป็นศูนย์</asp:ListItem>
                        <asp:ListItem Value="2">กลับยอดติดลบให้เป็นบวก</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>รูปแบบการดูอัตราด/บ:</span></div>
                </td>
                <td>
                    <asp:DropDownList ID="intdateview_type" runat="server">
                        <asp:ListItem Value="1">ดูอัตราเมื่อวาน(มาตรฐาน)</asp:ListItem>
                        <asp:ListItem Value="2">ดูอัตราวันนี้</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="2">
                    <div>
                        <span>การประมาณด/บ แบบคงยอด:</span></div>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="fixpaycal_type" runat="server">
                        <asp:ListItem Value="1">ดอกเบี้ยเฉลี่ย 1 เดือน</asp:ListItem>
                        <asp:ListItem Value="2">ดอกเบี้ย 30 วัน</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <div>
                        <span style="font-size: 11px">รับเงินกู้งวดแรกคิด ด/บ เพิ่ม/ลด(วัน):</span></div>
                </td>
                <td valign="top">
                    <asp:TextBox ID="intdateinc_firstrcv" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td colspan="2">
                    <strong><u>อื่นๆ</u></strong>
                </td>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ชำระหมดคิด ด/บ เพิ่ม/ลด(วัน):</span></div>
                </td>
                <td>
                    <asp:TextBox ID="intdateinc_lastpay" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td colspan="2">
                    <div>
                        <span>กู้ใหม่หลังจัดเก็บคิดด/บล่วงหน้า:</span></div>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="calint_future" runat="server">
                        <asp:ListItem Value="0">ไม่คิดด/บล่วงหน้า</asp:ListItem>
                        <asp:ListItem Value="1">คิดด/บล่วงหน้า</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div><span>วันที่ปันผลสหกรณ์:</span></div>
                </td>
                <td>
                    <asp:TextBox ID="calloandiv_date" runat="server" Style="text-align: center" TextMode="DateTime"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <strong><u>การตรวจสอบการคุ้มหนี้ตอนเกษียณ</u></strong>
                </td>
                <td colspan="2">
                    <div>
                        <span style="font-size: 12px">การประมวลผลคืนเงินที่ให้สมาชิก:</span></div>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="kpprocmrt_type" runat="server">
                        <asp:ListItem Value="1">คืนทุกกรณี</asp:ListItem>
                        <asp:ListItem Value="2">เมื่อยอดเรียกเก็บมากกว่า</asp:ListItem>
                        <asp:ListItem Value="3">เมื่อยอดชำระหนี้มากกว่า</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>รูปแบบการคิดคำนวณ(หุ้น):</span></div>
                </td>
                <td>
                    <asp:DropDownList ID="lnoverretry_shrformat" runat="server">
                        <asp:ListItem Value="1">คำนวณเป็น %</asp:ListItem>
                        <asp:ListItem Value="2">คำนวณเป็น อัตราส่วน</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="2">
                    <strong><u>รูปแบบการปัดเศษ ด/บ</u></strong>
                </td>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>สัดส่วน :</span></div>
                </td>
                <td>
                    <asp:TextBox ID="lnoverretry_shrperc" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td colspan="2">
                    <div>
                        <span>การปัดทศนิยมหลักที่ 4 :</span></div>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="rdintdec_type" runat="server">
                        <asp:ListItem Value="1">ปัด 5/4</asp:ListItem>
                        <asp:ListItem Value="2">ปัดทิ้ง</asp:ListItem>
                        <asp:ListItem Value="3">ปัดขึ้น</asp:ListItem>
                        <asp:ListItem Value="0">ไม่ปัด</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span style="font-size: 12px">รูปแบบการคิดคำนวณ(หลักทรัพย์):</span></div>
                </td>
                <td>
                    <asp:DropDownList ID="lnoverretry_cmformat" runat="server">
                        <asp:ListItem Value="1">คำนวณเป็น %</asp:ListItem>
                        <asp:ListItem Value="2">คำนวณเป็น อัตราส่วน</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="2">
                    <div>
                        <span>รูปแบบการปัดเศษสตางค์ :</span></div>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="rdintsatang_type" runat="server">
                        <asp:ListItem Value="0">ไม่ปัด</asp:ListItem>
                        <asp:ListItem Value="1">ปัดขึ้นทีละสลึง</asp:ListItem>
                        <asp:ListItem Value="2">ปัดขึ้นทีละ 5 สตางค์</asp:ListItem>
                        <asp:ListItem Value="3">ปัดขึ้นทีละ 10 สตางค์</asp:ListItem>
                        <asp:ListItem Value="4">ปัดเต็มบาท</asp:ListItem>
                        <asp:ListItem Value="5">ปัดขึ้นเต็มบาททุกกรณี</asp:ListItem>
                        <asp:ListItem Value="6">ปัดเป็น 50 สตางค์และ 1 บาท</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>สัดส่วน :</span></div>
                </td>
                <td>
                    <asp:TextBox ID="lnoverretry_cmperc" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td colspan="2">
                    <div>
                        <span>ตารางการปัดเศษสตางค์ :</span></div>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="rdintsatang_tabcode" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td colspan="2">
                    <div>
                        <span>การปัดเศษสตางค์ดอกเบี้ยหลายขั้น :</span></div>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="rdintsatangsum_type" runat="server">
                        <asp:ListItem Value="1">ปัดทุกขั้นที่คำนวนได้</asp:ListItem>
                        <asp:ListItem Value="2">รวมทุกขั้นก่อนแล้วค่อยปัด</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
