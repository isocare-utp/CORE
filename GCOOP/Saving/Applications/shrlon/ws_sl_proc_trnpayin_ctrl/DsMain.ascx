<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_proc_trnpayin_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit" Width="480px">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 500px">
        <tr>
                <td width="25%">
                    <div>
                        <span>ระบบ :</span></div>
                </td>
                <td width="50%">
                    <asp:DropDownList ID="source_system" runat="server">
                    <asp:ListItem Value="DIV">ระบบปันผล</asp:ListItem>
                    <asp:ListItem Value="DEP">ระบบเงินฝาก</asp:ListItem>
                    <asp:ListItem Value="SGT">หุ้นของขวัญ</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>วันที่ผ่านรายการ :</span></div>
                </td>
                <td>
                    <asp:TextBox ID="trans_date" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="b_check" runat="server" Text="ตรวสอบก่อนการทำรายการ" />
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
