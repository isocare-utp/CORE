<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsGeneral.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_cfloantype_ctrl.DsGeneral" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table>
            <tr>
                <td>
                    <table class="DataSourceFormView">
                        <tr>
                            <td width="15%">
                                <div>
                                    <span>รหัสเงินกู้:</span>
                                </div>
                            </td>
                            <td width="15%">
                                <asp:TextBox ID="loantype_code" runat="server" Style="text-align: center;"></asp:TextBox>
                            </td>
                            <td width="15%">
                                <div>
                                    <span>ชื่อเงินกู้:</span>
                                </div>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="loantype_desc" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>ตัวย่อเงินกู้:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="prefix" runat="server" Style="text-align: center;"></asp:TextBox>
                            </td>
                            <td>
                                <div>
                                    <span>กลุ่มเงินกู้:</span>
                                </div>
                            </td>
                            <td width="20%">
                                <asp:DropDownList ID="loangroup_code" runat="server">
                                    <asp:ListItem Value="01">กลุ่มเงินกู้ฉุกเฉิน</asp:ListItem>
                                    <asp:ListItem Value="02">กลุ่มเงินกู้สามัญ</asp:ListItem>
                                    <asp:ListItem Value="03">กลุ่มเงินกู้พิเศษ</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td width="15%">
                                <div>
                                    <span>สมาชิกที่กู้ได้:</span>
                                </div>
                            </td>
                            <td>
                                <asp:DropDownList ID="member_type" runat="server">
                                    <asp:ListItem Value="0">&lt;ไม่ระบุ&gt;</asp:ListItem>
                                    <asp:ListItem Value="1">สมาชิกปกติเท่านั้น</asp:ListItem>
                                    <asp:ListItem Value="2">สมาชิกสมทบเท่านั้น</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="DataSourceFormView">
                        <tr>
                            <td colspan="2">
                                <strong><u>ตรวจสอบตอนกู้</u></strong>
                            </td>
                            <td colspan="2">
                                <strong><u>กำหนดอายุสัญญา</u></strong>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <div>
                                    <span>ต้องเป็นสมาชิก(ด):</span>
                                </div>
                            </td>
                            <td width="30%">
                                <asp:TextBox ID="member_time" runat="server" Style="text-align: center;"></asp:TextBox>
                            </td>
                            <td width="16%">
                                <div>
                                    <span>อายุสัญญา(ด):</span>
                                </div>
                            </td>
                            <td width="34%">
                                <asp:DropDownList ID="counttimecont_type" runat="server" Style="width: 180px;">
                                    <asp:ListItem Value="0">ไม่กำหนด</asp:ListItem>
                                    <asp:ListItem Value="1">นับชนวัน</asp:ListItem>
                                    <asp:ListItem Value="2">นับชนเดือน</asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="contract_time" runat="server" Style="width: 56px; text-align: center;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                <!--<span> ตรวจ ง/ด คงเหลือ: </span> -->  
                                <asp:DropDownList ID="salarybal_status" runat="server" >
                                    <asp:ListItem Value="0">ไม่ตรวจ</asp:ListItem>
                                    <asp:ListItem Value="1">ตรวจเงินเดือน</asp:ListItem>
                                    <asp:ListItem Value="2">ตรวจเงินเดือนเป็นขั้น</asp:ListItem>
                                </asp:DropDownList>
                                </div>
                            </td>
                            <td>
                                <asp:DropDownList ID="salarybal_code" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <div>
                                    <span>เตือนล่วงหน้า(วัน):</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="contalert_time" runat="server" Style="text-align: center;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="DataSourceFormView">
                        <tr>
                            <td>
                                <strong><u>การปัดเศษ</u></strong>
                            </td>
                        </tr>
                        <tr>
                            <td width="27%">
                                <div>
                                    <span>factor ที่ต้องการปัดยอดขอกู้:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="reqround_factor" runat="server" Style="text-align: right;" ToolTip="#,##0"
                                    Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>factor ที่ต้องการปัดยอดชำระ:</span></div>
                            </td>
                            <td>
                                <asp:TextBox ID="payround_factor" runat="server" Style="text-align: right;" ToolTip="#,##0"
                                    Width="200px"></asp:TextBox>
                                ** ใส่ + ปัดขึ้น, 0 ไม่ปัด, - ปัดลง
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong><u>ชำระพิเศษ</u></strong>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>ชำระพิเศษคิด ด/บ จาก:</span>
                                </div>
                            </td>
                            <td>
                                <asp:DropDownList ID="payspec_method" runat="server">
                                    <asp:ListItem Value="1">ยอดคงเหลือ</asp:ListItem>
                                    <asp:ListItem Value="2">ยอดชำระ</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong><u>อื่นๆ</u></strong>
                            </td>
                            <td>
                                <asp:CheckBox ID="od_flag" runat="server" />เป็นสัญญา OD
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>เลขที่สัญญาอ้างอิงจากรหัส:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="document_code" runat="server" Style="width: 490px;"></asp:TextBox>
                                <asp:Button ID="b_searchdoc" runat="server" Text="..." Style="width: 30px;" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:CheckBox ID="contnosplitbranch_flag" runat="server" />
                                เลขสัญญาแยกสาขา
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>ค่าตั้งต้นของประเภทการจ่ายเงินกู้:</span>
                                </div>
                            </td>
                            <td>
                                <asp:DropDownList ID="defaultpay_type" runat="server">
                                    <asp:ListItem Value="1">เงินสด</asp:ListItem>
                                    <asp:ListItem Value="2">เงินโอน</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>ค่าตั้งต้นของวัตถุประสงค์การกู้:</span>
                                </div>
                            </td>
                            <td>
                                <asp:DropDownList ID="defaultobj_code" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>การอนุมัติทันที:</span>
                                </div>
                            </td>
                            <td>
                                <asp:DropDownList ID="approve_flag" runat="server">
                                    <asp:ListItem Value="0">ไม่อนุมัติทันที</asp:ListItem>
                                    <asp:ListItem Value="1">อนุมัติ+สร้างเลขสัญญา+จ่ายเงินกู้</asp:ListItem>
                                    <asp:ListItem Value="2">อนุมัติ+สร้างเลขสัญญา</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>การลดหย่อนภาษี:</span>
                                </div>
                            </td>
                            <td>
                                <asp:DropDownList ID="intcertificate_status" runat="server">
                                    <asp:ListItem Value="0">ด/บ เงินกู้ไม่สามารถลดหย่อนภาษีได้</asp:ListItem>
                                    <asp:ListItem Value="1">ด/บ เงินกู้สามารถลดหย่อนภาษีได้</asp:ListItem>
                                    <asp:ListItem Value="2">เลือกลดหย่อนจากหน้าจอขอกู้เอง</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>ชำระพิเศษหลังเรียกเก็บ:</span>
                                </div>
                            </td>
                            <td>
                                <asp:DropDownList ID="pxaftermthkeep_type" runat="server">
                                    <asp:ListItem Value="1">หักยอดรอเรียกเก็บ และไม่คิดดอกเบี้ย</asp:ListItem>
                                    <asp:ListItem Value="2">คิดเต็มจำนวน ไม่ยกเลิกยอดเรียกเก็บอัตโนมัติ</asp:ListItem>
                                    <asp:ListItem Value="3">คิดเต็มจำนวน ยกเลิกยอดเรียกเก็บอัตโนมัติ</asp:ListItem>
                                    <asp:ListItem Value="4">คิดเต็มจำนวน คืนเฉพาะชำระหมด</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
