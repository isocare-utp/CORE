<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.dlg.ws_dlg_sl_add_grpmangrtpermiss_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 500px">
            <tr>
                <td colspan="4">
                    <strong><u>วงเงินการค้ำประกันประเภทใหม่</u></strong>
                </td>
            </tr>
            <tr>
                <td  width="20%">
                    <div>
                        <span>รหัสกลุ่มการค้ำ:</span></div>
                </td>
                <td  width="80%">
                    <asp:TextBox ID="mangrtpermgrp_code" runat="server" Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ชื่อกลุ่มการค้ำ:</span></div>
                </td>
                <td>
                    <asp:TextBox ID="mangrtpermgrp_desc" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                    <br />
                    <asp:Button ID="b_clear" runat="server" Text="ล้างข้อมูล" Width="70px" />
                    &nbsp;
                    <asp:Button ID="b_add" runat="server" Text="ตกลง" Width="70px" />&nbsp;
                    <asp:Button ID="b_cancel" runat="server" Text="ยกเลิก" Width="70px" />
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
