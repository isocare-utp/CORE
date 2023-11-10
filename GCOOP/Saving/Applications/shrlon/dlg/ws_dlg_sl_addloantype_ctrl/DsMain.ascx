<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.dlg.ws_dlg_sl_addloantype_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 500px">
            <tr>
                <td colspan="4">
                    <strong><u>โครงการเงินกู้ประเภทใหม่</u></strong>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    <div>
                        <span>รหัสเงินกู้:</span></div>
                </td>
                <td>
                    <asp:TextBox ID="loantype_code" runat="server"></asp:TextBox>
                </td>
                <td width="20%">
                    <div>
                        <span>ตัวย่อเงินกู้:</span></div>
                </td>
                <td>
                    <asp:TextBox ID="prefix" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ชื่อเงินกู้:</span></div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="loantype_desc" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>กลุ่มเงินกู้:</span></div>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="loangroup_code" runat="server">
                        <asp:ListItem Value="" Text=""></asp:ListItem>
                        <asp:ListItem Value="01">กลุ่มเงินกู้ฉุกเฉิน</asp:ListItem>
                        <asp:ListItem Value="02">กลุ่มเงินกู้สามัญ</asp:ListItem>
                        <asp:ListItem Value="03">กลุ่มเงินกู้พิเศษ</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <strong>คัดลอกข้อกำหนดจากประเภทเงินกู้   </strong><asp:CheckBox ID="usepattern_flag" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ประเภทเงินกู้:</span></div>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="usepattern_lncode" runat="server" Enabled="False">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="4">
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
