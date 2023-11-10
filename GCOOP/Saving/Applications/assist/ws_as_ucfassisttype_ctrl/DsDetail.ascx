<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDetail.ascx.cs" Inherits="Saving.Applications.assist.ws_as_ucfassisttype_ctrl.DsDetail" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="20%">
                    <div>
                        <span>รหัสสวัสดิการ:</span>
                    </div>
                </td>
                <td width="25%">
                    <asp:TextBox ID="ASSISTTYPE_CODE" runat="server" Style="text-align: center;" ReadOnly="True"></asp:TextBox>
                </td>
                <td width="18%">
                    <div>
                        <span>ชื่อสวัสดิการ:</span>
                    </div>
                </td>
                <td colspan="4">
                    <asp:TextBox ID="ASSISTTYPE_DESC" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="10%">
                    <div>
                        <span>กลุ่มสวัสดิการ:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="ASSISTTYPE_GROUP" runat="server">
                        <asp:ListItem Value="01">01</asp:ListItem>
                        <asp:ListItem Value="02">02</asp:ListItem>
                        <asp:ListItem Value="03">03</asp:ListItem>
                        <asp:ListItem Value="04">04</asp:ListItem>
                        <asp:ListItem Value="05">05</asp:ListItem>
                        <asp:ListItem Value="06">06</asp:ListItem>
                        <asp:ListItem Value="07">07</asp:ListItem>
                    </asp:DropDownList>
                </td>
               <%-- <td>
                    <asp:CheckBox ID="membtype_flag" runat="server" />
                    แยกประเภทสมาชิก
                </td>--%>
                <td>
                    <asp:CheckBox ID="formula_flag" runat="server" />
                    เพิ่มการคำนวณพิเศษ
                </td>
                <td>
                    <asp:Button ID="b_cal" runat="server" Text="..." />
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ตัวคำนวณ:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="calculate_flag" runat="server">
                        <asp:ListItem Value="1">เกรดเฉลี่ย</asp:ListItem>
                        <asp:ListItem Value="2">อายุ(เดือน)</asp:ListItem>
                        <asp:ListItem Value="3">อายุการเป็นสมาชิก(เดือน)</asp:ListItem>
                        <asp:ListItem Value="4">เงินเดือน</asp:ListItem>
                        <asp:ListItem Value="5">ค่าเสียหาย</asp:ListItem>
                        <asp:ListItem Value="6">วันที่รักษาพยาบาล</asp:ListItem>
                    </asp:DropDownList>
                </td>
                  <td>
                    <asp:CheckBox ID="membdate_flag" runat="server" />
                    เป็นสมาชิกก่อนวันที่
                </td>
                 <td colspan="2" width="20%">
                    <asp:TextBox ID="member_date" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
             <td>
                    <div>
                        <span>การปัดอายุ:</span>
                    </div>
                    <td>
                    <asp:DropDownList ID="ageround_flag" runat="server">
                        <asp:ListItem Value="0">ไม่ปัด</asp:ListItem>
                        <asp:ListItem Value="1">ปัดเต็มปี</asp:ListItem>
                        <asp:ListItem Value="2">ปัดทิ้ง</asp:ListItem>
                        
                    </asp:DropDownList>
                </td>
                </td>

                 <td>
                  <%--  <asp:CheckBox ID="age_flag" runat="server" />
                    ตรวจอายุสมาชิก(ปี) >= --%>

                     <asp:DropDownList ID="age_flag" runat="server">
                        <asp:ListItem Value="0">ไม่ตรวจอายุ</asp:ListItem>
                        <asp:ListItem Value="1">ตรวจอายุ (ปี) >= </asp:ListItem>
                        <asp:ListItem Value="2">ตรวจอายุการเป็นสมาชิก(ปี) >= </asp:ListItem>
                    </asp:DropDownList>
                </td>
                 <td colspan="2" width="20%">
                    <asp:TextBox ID="age_num" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
            </tr>
             <tr>
             <td>
                    <div>
                        <span>การหักชำระหนี้:</span>
                    </div>
                <td>
                    <asp:DropDownList ID="loan_flag" runat="server">
                        <asp:ListItem Value="0">ไม่มี</asp:ListItem>
                        <asp:ListItem Value="1">มี</asp:ListItem>
                    </asp:DropDownList>
                </td>
                </td>
                <td>
                    <div>
                        <span>มูลค่าหนี้มากกว่าหุ้น(%):</span>
                    </div>

                 <td colspan="2" width="20%">
                    <asp:TextBox ID="loan_percent" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เช็คครอบครัว:</span>
                    </div>
                </td>
                <td >
                    <asp:DropDownList ID="family_flag" runat="server">
                        <asp:ListItem Value="0">ไม่เช็ค</asp:ListItem>
                        <asp:ListItem Value="1">เช็ค</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <div>
                        <span>จำนวนวันยื่นคำขอไม่เกิน:</span>
                    </div>
                </td>
                <td >
                    <asp:TextBox ID="docdate_num" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
            </tr>
            <tr>
              <td>
                    <div>
                        <span>วันที่คำนวณสิทธิ์:</span>
                    </div>
                <td >
                    <asp:DropDownList ID="DropDownList1" runat="server">
                        <asp:ListItem Value="0">วันที่เป็นสมาชิก</asp:ListItem>
                        <asp:ListItem Value="1">วันที่รับโอนย้าย</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <br />
        <table class="DataSourceFormView">
            <tr>
                <td colspan="5">
                    จำนวนครั้งการขอสวัสดิการ
                </td>
            </tr>
            <tr>
                <td width="20%">
                    <div>
                        <span>สมาชิกขอได้(ครั้ง): </span>
                    </div>
                </td>
                <td width="20%">
                    <asp:TextBox ID="limitreq_num" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td width="20%">
                    <div>
                        <span>ต่อระยะเวลา: </span>
                    </div>
                </td>
                <td width="20%">
                    <asp:TextBox ID="limitper_num" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td width="20%">
                    <asp:DropDownList ID="limitper_unit" runat="server">
                        <asp:ListItem Value="1">ปี</asp:ListItem>
                        <asp:ListItem Value="2">เดือน(ชนเดือน)</asp:ListItem>
                        <asp:ListItem Value="3">เดือน(ชนวัน)</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    <div>
                        <span>ผู้รับรับได้(ครั้ง): </span>
                    </div>
                </td>
                <td width="20%">
                    <asp:TextBox ID="limrcvreq_num" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td width="20%">
                    <div>
                        <span>ต่อระยะเวลา: </span>
                    </div>
                </td>
                <td width="20%">
                    <asp:TextBox ID="limrcvper_num" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td width="20%">
                    <asp:DropDownList ID="limrcvper_unit" runat="server">
                        <asp:ListItem Value="1">ปี</asp:ListItem>
                        <asp:ListItem Value="2">เดือน(ชนเดือน)</asp:ListItem>
                        <asp:ListItem Value="3">เดือน(ชนวัน)</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    กรณีรับหลายครั้งต่อรอบเวลา
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>สูงสุดไม่เกิน(บาท): </span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="limitmax_amt" runat="server" Style="text-align: right" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>การหัก: </span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="limitcut_type" runat="server">
                        <asp:ListItem Value="0">ไม่หักยอดที่รับไปแล้ว</asp:ListItem>
                        <asp:ListItem Value="1">หักยอดที่รับไปแล้วในรอบระยะเวลา</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="3">
                </td>
            </tr>
            <tr>
                <td colspan="5">
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    การจ่ายทุน
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>รูปแบบการจ่าย: </span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="stm_flag" runat="server">
                        <asp:ListItem Value="0">ทำจ่ายเอง</asp:ListItem>
                        <asp:ListItem Value="1">จ่ายต่อเนื่องทุกเดือน</asp:ListItem>
                        <asp:ListItem Value="2">จ่ายต่อเนื่องทุกปี</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <div>
                        <span>รูปแบบการรับ: </span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="stmpay_type" runat="server">
                        <asp:ListItem Value="0" Text="รับตามช่วงเดือน(ตรงวันตามเงื่อนไข)"></asp:ListItem>
                        <asp:ListItem Value="1" Text="รับตามช่วงเดือน(ระบุวัน)"></asp:ListItem>
                        <asp:ListItem Value="2" Text="รับทุกปี(ระบุเดือน)"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="stmpay_num" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td>
                    <div>
                        <span>ประเภทการจ่าย: </span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="default_paytype" runat="server">
                        <asp:ListItem Value="CSH">เงินสด</asp:ListItem>
                        <asp:ListItem Value="CBT">โอนธนาคาร</asp:ListItem>
                        <asp:ListItem Value="CHQ">เช็ค</asp:ListItem>
                        <asp:ListItem Value="TRN">โอนบัญชีสหกรณ์</asp:ListItem>
                        <asp:ListItem Value="GIF">ของขวัญ</asp:ListItem>
                    </asp:DropDownList>
                </td>
              </tr>
        </table>
        <br />
        <table class="DataSourceFormView">
            <tr>
                <td colspan="9">
                    เงื่อนไขการจ่ายเงิน:
                </td>
            </tr>
            <tr>
                <td style="width: 10%">
                    <div>
                        <span>งวดแรก:</span>
                    </div>
                </td>
                <td style="width: 15%">
                    <div>
                        <asp:TextBox ID="first_payamt" runat="server" ToolTip="#,##0.00" Style="text-align: right;"></asp:TextBox>
                    </div>
                </td>
                <td style="width: 10%">
                    <div>
                        <asp:DropDownList ID="first_typepay" runat="server">
                            <asp:ListItem Value="1">บาท</asp:ListItem>
                            <asp:ListItem Value="2">%</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </td>
                <td style="width: 10%">
                    <div>
                        <span>ไม่ต่ำกว่า:</span>
                    </div>
                </td>
                <td style="width: 10%">
                    <div>
                        <asp:TextBox ID="min_firstpayamt" runat="server" ToolTip="#,##0.00" Style="text-align: right;"></asp:TextBox>
                    </div>
                </td>
                 <td style="width: 5%">
                    <div>
                        <span style="text-align: center">บาท</span>
                    </div>
                </td>
                <td style="width: 11%">
                    <div>
                        <span>สูงสุดไม่เกิน:</span>
                    </div>
                </td>
                <td style="width: 15%">
                    <div>
                        <asp:TextBox ID="max_firstpayamt" runat="server" ToolTip="#,##0.00" Style="text-align: right;"></asp:TextBox>
                    </div>
                </td>
                <td style="width: 8%">
                    <div>
                        <span style="text-align: center">บาท</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>งวดถัดไป:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="next_payamt" runat="server" ToolTip="#,##0.00" Style="text-align: right;"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:DropDownList ID="next_typepay" runat="server">
                            <asp:ListItem Value="1">บาท</asp:ListItem>
                            <asp:ListItem Value="2">%</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </td>
                <td>
                    <div>
                        <span>ไม่ต่ำกว่า:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="min_nextpayamt" runat="server" ToolTip="#,##0.00" Style="text-align: right;"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span style="text-align: center">บาท</span>
                    </div>
                </td>
                <td>
                    <div>
                        <span>สูงสุดไม่เกิน:</span>
                    </div>
                </td>

                <td>
                    <div>
                        <asp:TextBox ID="max_nextpayamt" runat="server" ToolTip="#,##0.00" Style="text-align: right;"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span style="text-align: center">บาท</span>
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
