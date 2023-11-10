<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsRightdet.ascx.cs"
    Inherits="Saving.Applications.shrlon.ws_sl_cfloantype_ctrl.DsRightdet" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td colspan="3">
                    <strong><u>กำหนดวงเงินกู้</u></strong>
                </td>
            </tr>
            <tr>
                <td style="width: 20%">
                    <div>
                        <span>กลุ่มวงเงินกู้:</span>
                    </div>
                </td>
                <td style="width: 30%">
                    <asp:DropDownList ID="loanpermgrp_code" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:CheckBox ID="lngrpcutright_flag" runat="server" />
                    ขอกู้ได้ไม่เกินวงเงินกู้กลุ่มที่กำหนดไว้
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>วงเงินกู้สูงสุด:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="maxloan_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                    <asp:CheckBox ID="lngrpkeepsum_flag" runat="server" />วงเงินกู้กลุ่มไม่นับยอดรอเรียกเก็บประจำเดือน
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>การกำหนดวงเงินกู้:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="loanright_type" runat="server">
                        <asp:ListItem Value="1">ตามหลักประกัน</asp:ListItem>
                        <asp:ListItem Value="2">แบบกำหนดเอง</asp:ListItem>
                        <asp:ListItem Value="3">ดูจากสัญญาหลัก</asp:ListItem>
                        <asp:ListItem Value="4">ดูจากบัญชีเงินฝาก</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:CheckBox ID="notmoreshare_flag" runat="server" />ขอกู้ได้ไม่เกินหุ้นที่มี
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ดูช่วงเวลาจาก:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="customtime_type" runat="server">
                        <asp:ListItem Value="1">อายุการเป็นสมาชิก</asp:ListItem>
                        <asp:ListItem Value="2">อายุการทำงาน</asp:ListItem>
                        <asp:ListItem Value="3">งวดหุ้น</asp:ListItem>
                        <asp:ListItem Value="4">อายุสมาชิก+อายุงาน</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:CheckBox ID="showright_flag" runat="server" />แสดงสิทธิกู้ตอนขอกู้
                </td>
            </tr>
            <tr>
                <td><div>
                        <span>ดูมูลค่าหลักทรัพย์จาก:</span>
                    </div>
                </td>
                <td> <asp:DropDownList ID="collmastprice_type" runat="server">
                        <asp:ListItem Value="1">ราคาประเมิน</asp:ListItem>
                        <asp:ListItem Value="2">ราคาจำนอง</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
