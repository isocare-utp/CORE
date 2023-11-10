<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsCollDetail.ascx.cs"
    Inherits="Saving.Applications.shrlon.ws_sl_collateral_master_new_ctrl.DsCollDetail" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" width="500">
            <tr>
                <td width="15%">
                        <u>ตำแหน่งที่ดิน</u>
                </td>
                <td width="35">
                </td>
                <td width="15%">
                        <u>โฉนดที่ดิน</u>
                </td>
                <td width="35">
                        <u>ลำดับที่</u>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>หมู่ที่:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="pos_moo" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>เลขที่โฉนด:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="land_docno" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ตำบล:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="pos_tumbol" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>เล่ม:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="land_bookno" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>อำเภอ:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="pos_district" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>หน้า:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="land_pageno" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>จังหวัด:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="pos_provice" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>ระวาง:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="land_ravang" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                        <u>อาคารชุด</u>
                </td>
                <td>
                    <div>
                    </div>
                </td>
                <td>
                    <div>
                        <span>เลขที่ดิน:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="land_landno" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ชื่ออาคารชุด:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="condo_name" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>หน้าสำรวจ:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="land_survey" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ห้องชุดเลขที่:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="condo_roomno" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>เนื้อที่ดิน:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="size_rai" runat="server" style="width:30%;"></asp:TextBox>
                        <asp:TextBox ID="size_ngan" runat="server" style="width:30%;"></asp:TextBox>
                        <asp:TextBox ID="size_wa" runat="server" style="width:32%;"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ชั้นที่:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="condo_floor" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td>
                        <u>ระวางทางอากาศ</u>
                </td>
                <td>
                    <div>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>อาคารเลขที่:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="condo_towerno" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>ระวาง:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="photoair_province" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ทะเบียนอาคารชุด:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="condo_registno" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>หมายเลข:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="photoair_number" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เนื้อที่(ต.ร.ว.):</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="condo_roomsize" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div>
                        <span>แผ่นที่:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="photoair_page" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
