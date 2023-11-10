<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDetailDeed.ascx.cs"
    Inherits="Saving.Applications.shrlon.ws_sl_mg_mrtgmast_edit_ctrl.DsDetailDeed" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 750px; background-color: #CCFF99;
            border: 1px double black;">
            <tr>
                <td colspan="3">
                    <strong><u>ตำแหน่งที่ดิน</u></strong>
                </td>
                <td colspan="2">
                    <strong><u>โฉนดที่ดิน</u></strong>
                </td>
            </tr>
            <tr>
                <td width="15%">
                    <div>
                        <span>ระวาง:</span>
                    </div>
                </td>
                <td width="45%" colspan="2">
                    <asp:TextBox ID="land_ravang" runat="server"></asp:TextBox>
                </td>
                <td width="15%">
                    <div>
                        <span>เลขที่:</span>
                    </div>
                </td>
                <td width="25%">
                    <asp:TextBox ID="land_docno" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เลขที่ดิน:</span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="land_landno" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>เล่ม:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="land_bookno" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>หน้าสำรวจ:</span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="land_survey" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>หน้า:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="land_pageno" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ตำบล:</span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="pos_tambol" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>อำเภอ:</span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="pos_amphur" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>จังหวัด:</span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="pos_province" runat="server"></asp:TextBox>
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
                <td width="25%">
                    <asp:TextBox ID="size_rai" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td width="10%">
                    <div>
                        <span>งาน:</span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="size_ngan" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>วา:</span>
                    </div>
                </td>
                <td colspan="4">
                    <asp:TextBox ID="size_wa" runat="server" Style="width: 615px;"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
