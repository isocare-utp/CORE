<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_adt_mbaudit_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="12%">
                    <div>
                        <span style="font-size: 12px;">ทะเบียน:</span>
                    </div>
                </td>
                <td width="21%">
                    <asp:TextBox ID="member_no" runat="server" Style="width: 120px; text-align: center;
                        font-size: 12px;"></asp:TextBox>
                    <asp:Button ID="b_search" runat="server" Text="..." Style="width: 20px; margin-right: 0px;
                        font-size: 12px;" />
                </td>
                <td width="12%">
                    <div>
                        <span style="font-size: 12px;">สังกัด:</span>
                    </div>
                </td>
                <td width="21%">
                    <asp:TextBox ID="cp_membgroup_name" runat="server" Style="font-size: 12px;" ReadOnly="true"></asp:TextBox>
                </td>
                <td width="12%">
                    <div>
                        <span style="font-size: 12px;">ประเภทสมาชิก:</span>
                    </div>
                </td>
                <td width="22%">
                    <asp:DropDownList ID="membtype_code" runat="server" Style="font-size: 12px;">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span style="font-size: 12px;">คำนำหน้าชื่อ:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="prename_code" runat="server" Style="font-size: 12px;">
                    </asp:DropDownList>
                </td>
                <td>
                    <div>
                        <span style="font-size: 12px;">ชื่อ(ไทย):</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="memb_name" runat="server" Style="font-size: 12px;"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span style="font-size: 12px;">นามสกุล(ไทย):</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="memb_surname" runat="server" Style="font-size: 12px;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span style="font-size: 12px;">เพศ:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="sex" runat="server" Style="font-size: 12px;">
                        <asp:ListItem Value=""></asp:ListItem>
                        <asp:ListItem Value="M">ชาย</asp:ListItem>
                        <asp:ListItem Value="F">หญิง</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <div>
                        <span style="font-size: 12px;">ชื่อ(Eng):</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="memb_ename" runat="server" Style="font-size: 12px;"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span style="font-size: 12px;">นามสกุล(Eng):</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="memb_esurname" runat="server" Style="font-size: 12px;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div> <span style="font-size: 12px;">สัญชาติ:</span> </div>
                </td>
                <td>
                    <asp:TextBox ID="nationality" runat="server" Style="font-size: 12px;"></asp:TextBox>
                </td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td colspan="4">
                    <u><b>ข้อมูลส่วนตัว</b></u>
                </td>
                <td colspan="2">
                    <u><b>ข้อมูลสมาชิก</b></u>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span style="font-size: 12px;">วันเกิด:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="birth_date" runat="server" Style="width: 100px; font-size: 12px;
                        text-align: center;"></asp:TextBox>
                    <asp:TextBox ID="age" runat="server" Style="width: 40px; font-size: 12px; text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span style="font-size: 12px;">บัตรประชาชน:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="card_person" runat="server" Style="font-size: 12px; text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span style="font-size: 12px;">วันที่เป็นสมาชิก:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="member_date" runat="server" Style="width: 95px; font-size: 12px;
                        text-align: center;"></asp:TextBox>
                    <asp:TextBox ID="memb_age" runat="server" Style="width: 50px; font-size: 12px; text-align: center;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span style="font-size: 12px;">สถานะสมรส:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="mariage_status" runat="server" Style="font-size: 12px;">
                        <asp:ListItem Value=""></asp:ListItem>
                        <asp:ListItem Value="1">โสด</asp:ListItem>
                        <asp:ListItem Value="2">สมรส</asp:ListItem>
                        <asp:ListItem Value="3">หย่า</asp:ListItem>
                        <asp:ListItem Value="4">หม้าย</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <div>
                        <span style="font-size: 12px;">เลขพนักงานคู่สมรส:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="mate_salaryid" runat="server" Style="font-size: 12px; text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span style="font-size: 12px;">สถานะสมาชิก:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="member_type" runat="server" Style="font-size: 12px;">
                        <asp:ListItem Value=""></asp:ListItem>
                        <asp:ListItem Value="1">ปกติ</asp:ListItem>
                        <asp:ListItem Value="2">สมทบ</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span style="font-size: 12px;">ชื่อคู่สมรส:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="mate_name" runat="server" Style="font-size: 12px;"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span style="font-size: 12px;">บัตรฯคู่สมรส:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="mate_cardperson" runat="server" Style="font-size: 12px; text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span style="font-size: 12px;">อ้างอิงสมาชิก:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="member_ref" runat="server" Style="font-size: 12px; text-align: center;"></asp:TextBox>
                </td>
            </tr> 
            <tr>
                <td>
                    <div>
                        <span style="font-size: 12px;">วันที่ยืนยันเงินกู้:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="mateconfirmloan_date" runat="server" Style="font-size: 12px; text-align: center;"></asp:TextBox>
                </td>
                <td>
                   
                </td>
                <td>
                   
                </td>
                <td>
                    
                </td>
                <td>
                   
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <u><b>ที่อยู่ตามทะเบียนบ้าน</b></u>
                </td>
                <td colspan="2">
                    <u><b>ข้อมูลต้นสังกัด</b></u>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span style="font-size: 12px;">ที่อยู่:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="addr_no" runat="server" Style="font-size: 12px; text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span style="font-size: 12px;">หมู่:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="addr_moo" runat="server" Style="font-size: 12px; text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span style="font-size: 12px;">ตำแหน่ง:</span>
                    </div>
                </td> 
                <td >
                     <asp:TextBox ID="position_desc" runat="server"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td>
                    <div>
                        <span style="font-size: 12px;">ซอย:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="addr_soi" runat="server" Style="font-size: 12px; text-align: left;"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span style="font-size: 12px;">หมู่บ้าน:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="addr_village" runat="server" Style="font-size: 12px; text-align: left;"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span style="font-size: 12px;">ระดับ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="level_code" runat="server" Style="font-size: 12px; text-align: center;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span style="font-size: 12px;">ถนน:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="addr_road" runat="server" Style="font-size: 12px; text-align: left;"></asp:TextBox>
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
                <td>
                    <div>
                        <span style="font-size: 12px;">รหัสพนักงาน:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="salary_id" runat="server" Style="font-size: 12px; text-align: center;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span style="font-size: 12px;">เขต/อำเภอ:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="amphur_code" runat="server" Style="font-size: 12px;">
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
                <td>
                    <div>
                        <span style="font-size: 12px;">วันที่บรรจุ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="work_date" runat="server" Style="width: 95px; font-size: 12px; text-align: center;"></asp:TextBox>
                    <asp:TextBox ID="work_age" runat="server" Style="width: 50px; font-size: 12px; text-align: center;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span style="font-size: 12px;">รหัสไปรณีย์:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="addr_postcode" runat="server" Style="font-size: 12px; text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span style="font-size: 12px;">Email:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="addr_email" runat="server" Style="font-size: 12px;"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span style="font-size: 12px;">วันที่เกษียณ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="retry_date" runat="server" Style="width: 95px; font-size: 12px;
                        text-align: center;"></asp:TextBox>
                    <asp:TextBox ID="retry_age" runat="server" Style="width: 50px; font-size: 12px; text-align: center;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span style="font-size: 12px;">โทรศัพท์:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="addr_phone" runat="server" Style="font-size: 12px; text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span style="font-size: 12px;">มือถือ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="addr_mobilephone" runat="server" Style="font-size: 12px; text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span style="font-size: 12px;">ประเภทเกษียณ:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="retry_status" runat="server" Style="font-size: 12px;">
                        <asp:ListItem Value="0">พนักงานปกติ</asp:ListItem>
                        <asp:ListItem Value="1">เกษียณ</asp:ListItem>
                        <asp:ListItem Value="2">เกษียณก่อนเกณฑ์</asp:ListItem>
                        <asp:ListItem Value="3">ลาออกจากองค์กร</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Button ID="b_linkaddress" runat="server" Text="ที่อยู่ตามทะเบียนบ้าน -&gt; ที่อยู่ที่ติดต่อได้" />
                </td>
                <td>
                    <div>
                        <span style="font-size: 12px;">เงินเดือน:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="salary_amount" runat="server" Style="font-size: 12px; text-align: right;"
                        ToolTip="#,##0.00" ReadOnly="true"></asp:TextBox>
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
                <td>
                    <div>
                        <span style="font-size: 12px;">ค่าหุ้นฐาน:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="periodbase_value" runat="server" Style="font-size: 12px; text-align: right;"
                        ToolTip="#,##0.00"></asp:TextBox>
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
                <td>
                    <div>
                        <span style="font-size: 12px;">ค่าหุ้นต่อเดือน:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="periodshare_value" runat="server" Style="font-size: 12px; text-align: right;"
                        ToolTip="#,##0.00"></asp:TextBox>
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
                <td colspan="2" rowspan="3">
                    <table>
                        <tr>
                            <td>
                                <asp:CheckBox ID="klongtoon_flag" runat="server" />กองทุนสำรองเลี้ยงชีพ
                            </td>
                            <td>
                                <asp:CheckBox ID="transright_flag" runat="server" />หนังสือโอนสิทธ์เรียกร้อง
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="allowloan_flag" runat="server" />ใบยินยอมคู่สมรส
                            </td>
                            <td>
                                <asp:CheckBox ID="droploanall_flag" runat="server" />งดกู้เงินทุกประเภท
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="dropgurantee_flag" runat="server" />งดค้ำประกันเงินกู้
                            </td>
                            <td>
                                <asp:CheckBox ID="pausekeep_flag" runat="server" />งดออกใบเสร็จ
                            </td>
                        </tr>
                    </table>
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
            <tr>
                <td colspan="4">
                    <b>หมายเหตุสมาชิก:</b>
                </td>
                <td colspan="2">
                    <u><b>บัญชีหลัก</b></u>
                </td>
            </tr>
            <tr>
                <td colspan="4" rowspan="4" valign="top">
                    <asp:TextBox ID="remark" runat="server" TextMode="MultiLine" Width="460px" Height="100px"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span style="font-size: 12px;">รายการ:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="expense_code" runat="server" Style="font-size: 12px;">
                        <asp:ListItem Value=""></asp:ListItem>
                        <asp:ListItem Value="CSH">CSH - เงิดสด</asp:ListItem>
                        <asp:ListItem Value="CBT">CBT - บัญชีธนาคาร</asp:ListItem>
                        <asp:ListItem Value="TRN">TRN - บัญชีเงินฝากสหกรณ์</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span style="font-size: 12px;">ธนาคาร:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="expense_bank" runat="server" Style="width: 30px; font-size: 12px;
                        text-align: center;"></asp:TextBox>
                    <asp:DropDownList ID="bank_name" runat="server" Style="width: 100px; font-size: 12px;">
                    </asp:DropDownList>
                    <asp:Button ID="b_bank" runat="server" Text="..." Style="width: 15px;" />
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span style="font-size: 12px;">สาขา:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="expense_branch" runat="server" Style="width: 30px; font-size: 12px;
                        text-align: center;"></asp:TextBox>
                    <asp:DropDownList ID="branch_name" runat="server" Style="width: 100px; font-size: 12px;">
                    </asp:DropDownList>
                    <asp:Button ID="b_branch" runat="server" Text=".." Style="width: 15px;" />
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span style="font-size: 12px;">เลขที่บัญชี:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="expense_accid" runat="server" Style="font-size: 12px; text-align: center;"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
