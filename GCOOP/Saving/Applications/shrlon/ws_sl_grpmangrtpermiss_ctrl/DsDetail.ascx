<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDetail.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_grpmangrtpermiss_ctrl.DsDetail" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td colspan="4">
                    <strong><u>รายละเอียด</u></strong>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>รหัสกลุ่มการค้ำ:</span></div>
                </td>
                <td>
                    <asp:TextBox ID="mangrtpermgrp_code" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ชื่อกลุ่มการค้ำ:</span></div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="mangrtpermgrp_desc" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="15%">
                    <div>
                        <span>ต้องเป็นสมาชิก(ด):</span></div>
                </td>
                <td width="15%">
                    <asp:TextBox ID="member_time" runat="server"></asp:TextBox>
                </td>
                <td width="15%">
                    <div>
                        <span>ดูระยะเวลาจาก:</span></div>
                </td>
                <td width="20%">
                    <asp:DropDownList ID="mangrttime_type" runat="server">
                        <asp:ListItem Value="0" Text=""></asp:ListItem>
                        <asp:ListItem Value="1">การเป็นสมาชิก</asp:ListItem>
                        <asp:ListItem Value="2">การทำงาน</asp:ListItem>
                        <asp:ListItem Value="3">งวดหุ้น</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td width="15%">
                    <div>
                        <span>ประเภทสมาชิก:</span></div>
                </td>
                <td width="20%">
                    <asp:DropDownList ID="member_type" runat="server">
                        <asp:ListItem Value="0" Text=""></asp:ListItem>
                        <asp:ListItem Value="1">สมาชิกปกติ</asp:ListItem>
                        <asp:ListItem Value="2">สมาชิกสมทบ</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <strong style="font-size: small"><u>การนับการค้ำประกัน</u></strong>
        <table class="DataSourceFormView" style="border: 0.5px solid #000000; background: #D3E7FF;">
            <tr>
                <td width="27%">
                    <asp:CheckBox ID="grtright_contflag" runat="server" />
                    &nbsp;&nbsp; ค้ำประกันสัญญาได้ไม่เกิน
                </td>
                <td width="15%">
                    <asp:TextBox ID="grtright_contract" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
                <td width="5%">
                    สัญญา
                </td>
                <td width="6%">
                </td>
                <td width="27%">
                    <asp:CheckBox ID="grtright_memflag" runat="server" />&nbsp;&nbsp; ค้ำประกันบุคคลได้ไม่เกิน
                </td>
                <td width="15%">
                    <asp:TextBox ID="grtright_member" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
                <td width="5%">
                    คน
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
