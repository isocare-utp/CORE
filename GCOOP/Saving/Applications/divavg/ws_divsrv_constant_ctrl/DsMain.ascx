<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.divavg.ws_divsrv_constant_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="20%">
                    <div>
                        <span>ปีปันผล :</span>
                    </div>
                </td>
                <td width="30%">
                    <asp:TextBox ID="current_year" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td width="20%">
                </td>
                <td width="30%">
                    <asp:DropDownList ID="memset_code" runat="server">
                        <asp:ListItem Value="REG">วันที่ลาออก</asp:ListItem>
                        <asp:ListItem Value="SHR">หุ้นคงเหลือ ณ ปัจจุบัน</asp:ListItem>
                        <asp:ListItem Value="CSM">ข้อมูล ณ ปิดสิ้นปี</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เงื่อนไขปันผล :</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="divtype_code" runat="server">
                        <asp:ListItem Value="DAY">รายวัน</asp:ListItem>
                        <asp:ListItem Value="MTH">รายเดือน</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <div id="text_daycaltype">
                        <span>วิธีคำนวณปันผล :</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="div_daycaltype_code" runat="server">
                        <asp:ListItem Value="AMT">แยกยอดหุ้นทำรายการ</asp:ListItem>
                        <asp:ListItem Value="SEQ">ตามลำดับหุ้นคงเหลือ</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="text_mth">
                        <span>รอบวันต่อเดือน :</span>
                    </div>
                    <asp:DropDownList ID="div_dayfix_flag" runat="server">
                        <asp:ListItem Value="1">ระบุวัน</asp:ListItem>
                        <asp:ListItem Value="0">ตามปีบัญชี</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="div_day_amt" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:CheckBox ID="div_daygrp_flag" runat="server"/>
                    <td  id="text_daygrp">คำนวนยอดหุ้นรวมแต่ละวัน</td>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เงื่อนไขเคลียร์ค่า :</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="divclrzero_flag" runat="server">
                        <asp:ListItem Value="1">เคลียร์ข้อมูลหุ้นสิ้นปีเป็น 0</asp:ListItem>
                        <asp:ListItem Value="0">เคลียร์ข้อมูลปันผลเฉลี่ยคืนเป็น 0</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เงื่อนไขเฉลี่ยคืน :</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="avgtype_code" runat="server">
                        <asp:ListItem Value="TYP">ประเภทหนี้</asp:ListItem>
                        <asp:ListItem Value="MEM">สมาชิก</asp:ListItem>
                        <asp:ListItem Value="CON">สัญญา</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เงื่อนไขวิธีการจ่าย :</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="methpay_type" runat="server">
                        <asp:ListItem Value="REQ">ใบคำขอ</asp:ListItem>
                        <asp:ListItem Value="MEM">สมาชิก</asp:ListItem>
                        <asp:ListItem Value="MIX">ใบคำขอ+สมาชิก</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
