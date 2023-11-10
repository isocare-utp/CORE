<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsLand.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_collateral_master_ctrl.DsLand" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="FormStyle">
            <tr>
                <td>
                    <div>
                        <span>ประเภทที่ดิน</span></div>
                </td>
                <td colspan="6">
                    <asp:DropDownList ID="collmasttype_code" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                </td>
                <td colspan="6">
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ระวาง</span></div>
                </td>
                <td colspan="6">
                    <asp:TextBox ID="land_ravang" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>โฉนดเลขที่</span></div>
                </td>
                <td colspan="6">
                    <asp:TextBox ID="land_docno" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เลขที่ดิน</span></div>
                </td>
                <td colspan="6">
                    <asp:TextBox ID="land_landno" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>เล่ม</span></div>
                </td>
                <td>
                    <asp:TextBox ID="land_bookno" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>หน้า</span></div>
                </td>
                <td>
                    <asp:TextBox ID="land_pageno" runat="server" Width="83px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>หน้าสำรวจ</span></div>
                </td>
                <td colspan="6">
                    <asp:TextBox ID="land_survey" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ตำบล</span></div>
                </td>
                <td colspan="6">
                    <asp:TextBox ID="pos_tambol" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>อำเภอ</span></div>
                </td>
                <td colspan="6">
                    <asp:TextBox ID="pos_amphur" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>จังหวัด</span></div>
                </td>
                <td colspan="6">
                    <asp:TextBox ID="pos_province" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="15%">
                    <div>
                        <span>เนื้อที่(ที่ดิน/ตึก)</span></div>
                </td>
                <td width="11%">
                    <asp:TextBox ID="size_rai" runat="server" Width="75px"  Style="text-align: center;"></asp:TextBox>
                </td>
                <td width="3%">
                    ไร่
                </td>
                <td width="10%">
                    <asp:TextBox ID="size_ngan" runat="server"  Style="text-align: center;"></asp:TextBox>
                </td>
                <td width="4%">
                    งาน
                </td>
                <td width="11%">
                    <asp:TextBox ID="size_wa" runat="server"  Style="text-align: center;"></asp:TextBox>
                </td>
                <td width="13%" colspan="2">
                    ตารางวา
                </td>
                <%--<td width="10%">
                </td>--%>
                <td width="14%">
                </td>
                <td width="6%">
                </td>
                <td width="13%">
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ภาพภ่ายทางอากาศ</span></div>
                </td>
                <td colspan="6">
                    <asp:TextBox ID="photoair_province" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>หมายเลข</span></div>
                </td>
                <td>
                    <asp:TextBox ID="photoair_number" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>แผ่นที่</span></div>
                </td>
                <td>
                    <asp:TextBox ID="photoair_page" runat="server" Width="83px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ราคา ตร.วา ละ</span></div>
                </td>
                <td colspan="6">
                    <asp:TextBox ID="priceper_unit" runat="server" ToolTip="#,##0.00" Style="text-align: right;"></asp:TextBox>
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
        </table>
    </EditItemTemplate>
</asp:FormView>
