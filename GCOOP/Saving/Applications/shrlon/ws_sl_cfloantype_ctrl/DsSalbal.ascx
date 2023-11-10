<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsSalbal.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_cfloantype_ctrl.DsSalbal" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet" type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet" type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="20%">
                    <div> <span>ตรวจเงินเดือนคงเหลือ: </span> </div>
                </td>
                <td>
                    <asp:DropDownList ID="cksalarybal_status" runat="server">
                        <asp:ListItem Value="0">ไม่ตรวจ</asp:ListItem>
                        <asp:ListItem Value="1">ตรวจเงินเดือน</asp:ListItem>
                        <asp:ListItem Value="2">ตรวจเงินเดือนเป็นขั้น</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>