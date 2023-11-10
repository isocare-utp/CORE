<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.dlg.w_dlg_sl_receive_ref_slip_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 560px;">
            <tr>
                <td width="100px">
                    <div>
                        <span>ทะเบียนสมาชิก:</span>
                    </div>
                </td>
                <td width="150px">
                    <div>
                        <asp:TextBox ID="member_no" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td width="100px">
                    <div>
                        <span>ระบบ:</span>
                    </div>
                </td>
                <td width="150px">
                    <div>
                        <asp:DropDownList ID="ref_system" runat="server">
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="LON">LON - ระบบสินเชื่อ</asp:ListItem>
                            <asp:ListItem Value="DEP">DEP - ระบบเงินฝาก</asp:ListItem>
                            <asp:ListItem Value="DIV">DIV - ระบบปันผล-เฉลี่ยคืน</asp:ListItem>
                            <asp:ListItem Value="ASS">ASS - ระบบสวัสดิการ</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
