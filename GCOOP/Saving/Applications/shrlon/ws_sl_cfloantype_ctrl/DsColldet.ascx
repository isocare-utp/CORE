<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsColldet.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_cfloantype_ctrl.DsColldet" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td colspan="2">
                    <strong><u>ข้อกำหนดหลักประกัน</u></strong>
                </td>
                <td colspan="2">
                    <strong><u>ข้อกำหนดเวลาโดนสอบตรวจสอบคนค้ำประกัน</u></strong>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:CheckBox ID="grtneed_flag" runat="server" />
                    ขอกู้ต้องใช้หลักประกัน
                </td>
                <td width="17%">
                    <div>
                        <span>วงเงินการค้ำ(ปกติ):</span>
                    </div>
                </td>
                <td width="33%">
                    <asp:DropDownList ID="mangrtpermgrp_code" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:CheckBox ID="usemangrt_status" runat="server" />
                    ใช้คนค้ำประกันได้
                </td>
                <td>
                    <div>
                        <span>วงเงินการค้ำ(สมทบ):</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="mangrtpermgrpco_code" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:CheckBox ID="retrycollchk_flag" runat="server" />
                    ตรวจเกษียณคนค้ำประกันด้วย
                </td>
                <td>
                    <div>
                        <span>การนับคนค้ำประกัน:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="cntmangrtnum_flag" runat="server">
                        <asp:ListItem Value="0">ไม่นับรวมเป็นค้ำ</asp:ListItem>
                        <asp:ListItem Value="1">นับรวมทั้งหมด</asp:ListItem>
                        <asp:ListItem Value="2">นับเฉพาะประเภทเงินกู้นี้</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:CheckBox ID="chklockshare_flag" runat="server" />
                    ใช้หุ้นค้ำต้องตรวจว่าหุ้นมีการ Lock ไว้หรือไม่
                </td>
                <td>
                    <div>
                        <span>การนับยอดค้ำประกัน:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="cntmangrtval_flag" runat="server">
                        <asp:ListItem Value="1">นับยอดค้ำ</asp:ListItem>
                        <asp:ListItem Value="0">ไม่นับยอดค้ำ</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <strong><u>(กรณีคนค้ำ) การค้ำได้สูงสุดของเงินกู้ประเภทนี้</u></strong>
                </td>
                <td colspan="2">
                    <asp:CheckBox ID="lockshare_flag" runat="server" />
                    Lock หุ้นไว้ในกรณีใช้คนค้ำประกัน
                </td>
            </tr>
            <tr>
                <td width="17%">
                    <div>
                        <span>สมาชิกปกติ:</span>
                    </div>
                </td>
                <td width="33%">
                    <asp:TextBox ID="usemangrt_mainmaxvalue" runat="server" ToolTip="#,###0.00" Style="text-align: right;"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>สมาชิกสมทบ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="usemangrt_comaxvalue" runat="server" ToolTip="#,###0.00" Style="text-align: right;"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
