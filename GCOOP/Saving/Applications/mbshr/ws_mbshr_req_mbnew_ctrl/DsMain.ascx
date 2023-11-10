<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_req_mbnew_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="13%">
                    <div>
                        <span>เลขที่ใบสมัคร:</span>
                    </div>
                </td>
                <td width="13%">
                    <asp:TextBox ID="appl_docno" runat="server" Style="background-color: InfoBackground;
                        text-align: center;" ReadOnly="true"></asp:TextBox>
                </td>
                <td width="12%">
                    <div>
                        <span>วันที่สมัคร:</span>
                    </div>
                </td>
                <td width="24%">
                    <asp:TextBox ID="apply_date" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td width="20%">
                    <div>
                        <span>
                            <asp:CheckBox ID="membdatefix_flag" runat="server" Text="ระบุวันที่เป็นสมาชิกเอง" />
                        </span>
                    </div>
                </td>
                <td width="18%">
                    <div>
                        <asp:TextBox ID="membdatefix_date" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
            </tr>
        </table>
        <table class="DataSourceFormView">
            <tr>
                <td width="13%">
                    <div>
                        <span>ครั้งที่สมัคร:</span>
                    </div>
                </td>
                <td width="13%">
                    <asp:DropDownList ID="appltype_code" runat="server">
                    </asp:DropDownList>
                </td>
                <td width="12%">
                    <div>
                        <span>ปกติ/สมทบ:</span>
                    </div>
                </td>
                <td width="24%">
                    <asp:DropDownList ID="member_type" runat="server">
                        <asp:ListItem Value="0">ไม่ระบุ</asp:ListItem>
                        <asp:ListItem Value="1">ปกติ</asp:ListItem>
                        <asp:ListItem Value="2">สมทบ</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td width="14%">
                    <div>
                        <span>ประเภทสมาชิก:</span>
                    </div>
                </td>
                <td width="24%">
                    <asp:DropDownList ID="membtype_code" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>คำนำหน้า:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="prename_code" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <div>
                        <span>ชื่อ(ไทย):</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="memb_name" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>สกุล(ไทย):</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="memb_surname" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เพศ:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="sex" runat="server">
                        <asp:ListItem Value="M">ชาย</asp:ListItem>
                        <asp:ListItem Value="F">หญิง</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <div>
                        <span>ชื่อ(Eng):</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="memb_ename" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>สกุล(Eng):</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="memb_esurname" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table class="DataSourceFormView">
            <tr>
                <td width="13%">
                    <div>
                        <span>สังกัด:</span>
                    </div>
                </td>
                <td width="13%">
                    <asp:TextBox ID="membgroup_code" runat="server"></asp:TextBox>
                </td>
                <td width="36%" colspan="2">
                    <asp:DropDownList ID="membgroup" runat="server" Style="width: 230px;">
                    </asp:DropDownList>
                    <asp:Button ID="b_search" runat="server" Text="..." Style="width: 20px; margin-right: 0px;" />
                </td>
                <td width="20%">
                    <div>
                        <span>
                            <asp:CheckBox ID="memnofix_flag" runat="server" Text="กำหนดเลขสมาชิกเอง" />
                        </span>
                    </div>
                </td>
                <td width="18%">
                    <div>
                        <asp:TextBox ID="member_no" runat="server" Style="text-align: center" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <table class="DataSourceFormView" style="width: 370px;">
                        <tr>
                            <td colspan="2">
                                <u><b>ข้อมูลส่วนตัว:</b></u>
                            </td>
                        </tr>
                        <tr>
                            <td width="35%">
                                <div>
                                    <span>วันที่เกิด:</span>
                                </div>
                            </td>
                            <td width="65%">
                                <asp:TextBox ID="birth_date" runat="server" Style="width: 133px; text-align: center;"></asp:TextBox>
                                <asp:TextBox ID="age" runat="server" Style="width: 90px; text-align: center;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>สัญชาติ:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="nationality" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>เลขบัตรประชาชน:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="card_person" runat="server" Style="text-align: center;"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table class="DataSourceFormView" style="width: 350px;">
                        <tr>
                            <td colspan="4">
                                <u><b>ที่อยู่ตามทะเบียนบ้าน:</b></u>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                <div>
                                    <span style="font-size: 12px;">ที่อยู่:</span>
                                </div>
                            </td>
                            <td width="30%">
                                <asp:TextBox ID="memb_addr" runat="server" Style="font-size: 12px; text-align: center;"></asp:TextBox>
                            </td>
                            <td width="20%">
                                <div>
                                    <span style="font-size: 12px;">หมู่:</span>
                                </div>
                            </td>
                            <td width="30%">
                                <asp:TextBox ID="addr_group" runat="server" Style="font-size: 12px; text-align: center;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span style="font-size: 12px;">ซอย:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="soi" runat="server" Style="font-size: 12px; text-align: left;"></asp:TextBox>
                            </td>
                            <td>
                                <div>
                                    <span style="font-size: 12px;">หมู่บ้าน:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="mooban" runat="server" Style="font-size: 12px; text-align: left;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span style="font-size: 12px;">ถนน:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="road" runat="server" Style="font-size: 12px; text-align: left;"></asp:TextBox>
                            </td>
                            <td>
                                <div>
                                    <span style="font-size: 12px;">จังหวัด:</span>
                                </div>
                            </td>
                            <td>
                                <asp:DropDownList ID="province_code" runat="server" Style="font-size: 12px;">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span style="font-size: 12px;">เขต/อำเภอ:</span>
                                </div>
                            </td>
                            <td>
                                <asp:DropDownList ID="district_code" runat="server" Style="font-size: 12px;">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <div>
                                    <span style="font-size: 12px;">แขวง/ตำบล:</span>
                                </div>
                            </td>
                            <td>
                                <asp:DropDownList ID="tambol_code" runat="server" Style="font-size: 12px;">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span style="font-size: 12px;">รหัสไปรณีย์:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="postcode" runat="server" Style="font-size: 12px; text-align: center;"></asp:TextBox>
                            </td>
                            <td>
                                <div>
                                    <span style="font-size: 12px;">Email:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="email_address" runat="server" Style="font-size: 12px;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span style="font-size: 12px;">โทรศัพท์:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="mem_tel" runat="server" Style="font-size: 12px; text-align: center;"></asp:TextBox>
                            </td>
                            <td>
                                <div>
                                    <span style="font-size: 12px;">มือถือ:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="mem_telmobile" runat="server" Style="font-size: 12px; text-align: center;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Button ID="b_linkaddress" runat="server" Text="ที่อยู่ตามทะเบียนบ้าน -> ที่อยู่ปัจจุบัน" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <u><b>ที่อยู่ปัจจุบัน:</b></u>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span style="font-size: 12px;">ที่อยู่:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="curraddr_no" runat="server" Style="font-size: 12px; text-align: center;"></asp:TextBox>
                            </td>
                            <td>
                                <div>
                                    <span style="font-size: 12px;">หมู่:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="curraddr_moo" runat="server" Style="font-size: 12px; text-align: center;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span style="font-size: 12px;">ซอย:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="curraddr_soi" runat="server" Style="font-size: 12px; text-align: left;"></asp:TextBox>
                            </td>
                            <td>
                                <div>
                                    <span style="font-size: 12px;">หมู่บ้าน:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="curraddr_village" runat="server" Style="font-size: 12px; text-align: left;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span style="font-size: 12px;">ถนน:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="curraddr_road" runat="server" Style="font-size: 12px; text-align: left;"></asp:TextBox>
                            </td>
                            <td>
                                <div>
                                    <span style="font-size: 12px;">จังหวัด:</span>
                                </div>
                            </td>
                            <td>
                                <asp:DropDownList ID="currprovince_code" runat="server" Style="font-size: 12px;">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span style="font-size: 12px;">เขต/อำเภอ:</span>
                                </div>
                            </td>
                            <td>
                                <asp:DropDownList ID="curramphur_code" runat="server" Style="font-size: 12px;">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <div>
                                    <span style="font-size: 12px;">แขวง/ตำบล:</span>
                                </div>
                            </td>
                            <td>
                                <asp:DropDownList ID="currtambol_code" runat="server" Style="font-size: 12px;">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span style="font-size: 12px;">รหัสไปรณีย์:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="curraddr_postcode" runat="server" Style="font-size: 12px; text-align: center;"></asp:TextBox>
                            </td>
                            <td>
                                <div>
                                    <span style="font-size: 12px;">โทรศัพท์:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="curraddr_phone" runat="server" Style="font-size: 12px; text-align: center;"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table class="DataSourceFormView" style="width: 370px;">
                        <tr>
                            <td colspan="2">
                                <u><b>ข้อมูลอื่นๆ:</b></u>
                            </td>
                        </tr>
                        <tr>
                            <td width="35%">
                                <div>
                                    <span>ชื่อบิดา :</span>
                                </div>
                            </td>
                            <td width="65%">
                                <asp:TextBox ID="father_name" runat="server" Style="font-size: 12px;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>ชื่อมารดา :</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="mather_name" runat="server" Style="font-size: 12px;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>อ้างอิงสมาชิก:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="member_ref" runat="server" Style="font-size: 12px; width: 200px; text-align:center;"></asp:TextBox>
                                <asp:Button ID="b_membsearch" runat="server" Text="..." Style="width: 20px;" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>ข้อมูลการค้ำประกัน:</span>
                                </div>
                            </td>
                            <td>
                                <asp:CheckBox ID="lndropgrantee_flag" Text="ไม่ประสงค์ค้ำประกัน" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table class="DataSourceFormView" style="width: 370px;">
                        <tr>
                            <td colspan="4">
                                <u><b>ข้อมูลการทำงาน:</b></u>
                            </td>
                        </tr>
                        <tr>
                            <td width="23%">
                                <div>
                                    <span>ตำแหน่ง:</span>
                                </div>
                            </td>
                            <td width="27%">
                                <asp:TextBox ID="position_desc" runat="server"></asp:TextBox>
                            </td>
                            <td width="23%">
                                <div>
                                    <span>ระดับ:</span>
                                </div>
                            </td>
                            <td width="27%">
                                <asp:TextBox ID="level_code" runat="server" Style="text-align: center;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>เลขพนักงาน:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="salary_id" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <div>
                                    <span style="font-size: 12px;">เบอร์ที่ทำงาน:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="mem_telwork" runat="server" Style="font-size: 12px; text-align: center;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span style="font-size: 12px;">เงินเดือน:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="salary_amount" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                            </td>
                            <td>
                                <div>
                                    <span style="font-size: 12px;">เงินอื่นๆ:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="incomeetc_amt" runat="server" Style="font-size: 12px; text-align: right;"
                                    ToolTip="#,##0.00"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span style="font-size: 12px;">วันบรรจุ:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="work_date" runat="server" Style="text-align: center;"></asp:TextBox>
                            </td>
                            <td>
                                <div>
                                    <span style="font-size: 12px;">เกษียณอายุ:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="retry_date" runat="server" Style="font-size: 12px; text-align: center;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <u><b>ข้อมูลการส่งหุ้น:</b></u>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span style="font-size: 12px;">มูลค่าหุ้น/เดือน:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="periodshare_value" runat="server" Style="text-align: right" ToolTip="#,##0.00"></asp:TextBox>
                            </td>
                            <td>
                                <div>
                                    <span style="font-size: 12px;">หุ้นตามฐาน งด:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="periodbase_value" runat="server" Style="font-size: 12px; background-color: Silver;
                                    text-align: right;" ReadOnly="true" ToolTip="#,##0.00"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                             <div>
                                    <span style="font-size: 12px;">มูลค่าหุ้นรับโอน:</span>
                               </div>
                            </td>
                            <td>
                                <asp:TextBox ID="rcv_sharevalue" runat="server" Style="text-align: right" ToolTip="#,##0.00"></asp:TextBox>
                            </td>
                            <td>
                             <div>
                                    <span style="font-size: 12px;">วันที่รับโอนหุ้น:</span>
                               </div>
                            </td>
                             <td>
                                <asp:TextBox ID="rcvshare_date" runat="server" Style="text-align: right" ></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table class="DataSourceFormView" style="width: 370px;">
                        <tr>
                            <td colspan="2">
                                <u><b>ข้อมูลการสมรส:</b></u>
                            </td>
                        </tr>
                        <tr>
                            <td width="35%">
                                <div>
                                    <span>สถานะสมรส:</span>
                                </div>
                            </td>
                            <td width="65%">
                                <asp:DropDownList ID="mariage" runat="server">
                                <asp:ListItem Value="0" Text=""></asp:ListItem>
                                    <asp:ListItem Value="1">โสด</asp:ListItem>
                                    <asp:ListItem Value="2">สมรส</asp:ListItem>
                                    <asp:ListItem Value="3">หย่า</asp:ListItem>
                                    <asp:ListItem Value="4">หม้าย</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>ชื่อคู่สมรส:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="mate_name" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>เลขบัตรฯ คู่สมรส:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="mate_cardperson" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span style="font-size: 12px;">เลขพนักงาน:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="mate_salaryid" runat="server" Style="font-size: 12px; text-align: center;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="b_linkaddress2" runat="server" Text="ที่อยู่เดียวกับคู่สมรส" />
                            </td>
                        </tr>
                    </table>
                    <table class="DataSourceFormView" style="width: 370px;">
                        <tr>
                            <td width="20%">
                                <div>
                                    <span style="font-size: 12px;">ที่อยู่:</span>
                                </div>
                            </td>
                            <td width="30%">
                                <asp:TextBox ID="mateaddr_no" runat="server" Style="font-size: 12px; text-align: center;"></asp:TextBox>
                            </td>
                            <td width="20%">
                                <div>
                                    <span style="font-size: 12px;">หมู่:</span>
                                </div>
                            </td>
                            <td width="30%">
                                <asp:TextBox ID="mateaddr_moo" runat="server" Style="font-size: 12px; text-align: center;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span style="font-size: 12px;">ซอย:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="mateaddr_soi" runat="server" Style="font-size: 12px; text-align: left;"></asp:TextBox>
                            </td>
                            <td>
                                <div>
                                    <span style="font-size: 12px;">หมู่บ้าน:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="mateaddr_village" runat="server" Style="font-size: 12px; text-align: left;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span style="font-size: 12px;">ถนน:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="mateaddr_road" runat="server" Style="font-size: 12px; text-align: left;"></asp:TextBox>
                            </td>
                            <td>
                                <div>
                                    <span style="font-size: 12px;">จังหวัด:</span>
                                </div>
                            </td>
                            <td>
                                <asp:DropDownList ID="mateprovince_code" runat="server" Style="font-size: 12px;">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span style="font-size: 12px;">เขต/อำเภอ:</span>
                                </div>
                            </td>
                            <td>
                                <asp:DropDownList ID="mateamphur_code" runat="server" Style="font-size: 12px;">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <div>
                                    <span style="font-size: 12px;">แขวง/ตำบล:</span>
                                </div>
                            </td>
                            <td>
                                <asp:DropDownList ID="matetambol_code" runat="server" Style="font-size: 12px;">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span style="font-size: 12px;">รหัสไปรณีย์:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="mateaddr_postcode" runat="server" Style="font-size: 12px; text-align: center;"></asp:TextBox>
                            </td>
                            <td>
                                <div>
                                    <span style="font-size: 12px;">โทรศัพท์:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="mateaddr_phone" runat="server" Style="font-size: 12px; text-align: center;"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <hr />
                    <table class="DataSourceFormView" style="width: 370px;">
                        <tr>
                            <tr>
                                <td colspan="2">
                                    <u><b>หมายเหตุ:</b></u>
                                </td>
                            </tr>
                            <td colspan="2">
                                <textarea id="remark" rows="6" cols="49" style="font-size: 12px;"></textarea>
                            </td>
                        </tr>
                        <tr>
                            <td width="35%">
                                <div>
                                    <span>ผู้ทำรายการ:</span>
                                </div>
                            </td>
                            <td width="65%">
                                <asp:TextBox ID="entry_id" runat="server" Style="background-color: Silver; text-align: center;"
                                    ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="DataSourceFormView" style="width: 370px;">
                        <tr>
                            <td colspan="2">
                                <u><b>บัญชีหลัก:</b></u>
                            </td>
                        </tr>
                        <tr>
                            <td width="35%">
                                <div>
                                    <span>ประเภทรายการ:</span>
                                </div>
                            </td>
                            <td width="65%">
                                <asp:DropDownList ID="expense_code" runat="server">
                                    <asp:ListItem Value=""></asp:ListItem>
                                    <asp:ListItem Value="CSH">เงินสด</asp:ListItem>
                                    <asp:ListItem Value="CBT">บัญชีธนาคาร</asp:ListItem>
                                    <asp:ListItem Value="TRN">บัญชีสหกรณ์</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>ธนาคาร:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="expense_bank" runat="server" Style="font-size: 12px;" Width="50px"></asp:TextBox>
                                <asp:DropDownList ID="bank_desc" runat="server" Width="150px">
                                </asp:DropDownList>
                                <asp:Button ID="b_bank" runat="server" Text="..." Style="width: 20px;" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>สาขา:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="expense_branch" runat="server" Style="font-size: 12px;" Width="50px"></asp:TextBox>
                                <asp:DropDownList ID="branch_name" runat="server" Width="150px">
                                </asp:DropDownList>
                                <asp:Button ID="b_branch" runat="server" Text="..." Style="width: 20px;" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>เลขบัญชี:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="expense_accid" runat="server" Style="font-size: 12px; text-align: center;"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table class="DataSourceFormView" style="width: 370px;">
                        <tr>
                            <td colspan="2">
                                <u><b>ข้อมูลสำหรับสมาชิกเก่า:</b></u>
                            </td>
                        </tr>
                        <tr>
                            <td width="35%">
                                <div>
                                    <span>วันที่ลาออก:</span>
                                </div>
                            </td>
                            <td width="65%">
                                <asp:TextBox ID="date_resign" runat="server" Style="font-size: 12px; background-color: Silver;
                                    text-align: center;" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>ทะเบียนครั้งก่อน:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="old_member_no" runat="server" Style="font-size: 12px; background-color: Silver;
                                    text-align: center;" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <span>สาเหตุลาออก:</span>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="resigncause_code" runat="server" Style="font-size: 12px; background-color: Silver;"
                                    ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
