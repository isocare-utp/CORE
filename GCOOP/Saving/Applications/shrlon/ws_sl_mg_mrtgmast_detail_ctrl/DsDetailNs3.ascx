<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDetailNs3.ascx.cs"
    Inherits="Saving.Applications.shrlon.ws_sl_mg_mrtgmast_detail_ctrl.DsDetailNs3" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 720px; background-color: #CCFF99;border:1px double black;">
            <tr>
                <td colspan="2">
                    <strong><u>ตำแหน่งที่ดิน</u></strong>
                </td>
                <td colspan="2">
                    <strong><u>ทะเบียน</u></strong>
                </td>
            </tr>
            <tr>
                <td width="17%">
                    <div>
                        <span>หมู่ที่:</span>
                    </div>
                </td>
                <td width="33%">
                    <asp:TextBox ID="pos_moo" runat="server"></asp:TextBox>
                </td>
                <td width="17%">
                    <div>
                        <span>เลขที่:</span>
                    </div>
                </td>
                <td width="33%">
                    <asp:TextBox ID="ns3_docno" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ตำบล:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="pos_tambol" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>เล่ม:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="ns3_bookno" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>อำเภอ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="pos_amphur" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>หน้า:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="ns3_pageno" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>จังหวัด:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="pos_province" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>เลขที่ดิน:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="ns3_landno" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <strong><u>ระวางรูปถ่ายทางอากาศ</u></strong>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ชื่อ:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="photoair_province" runat="server" Style="width: 418px;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>หมายเลข:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="photoair_number" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>แผ่นที่:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="photoair_page" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <strong><u>ขนาดที่ดิน</u></strong>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ไร่:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="size_rai" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>งาน:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="size_ngan" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>วา:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="size_wa" runat="server" Style="width: 418px;"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
