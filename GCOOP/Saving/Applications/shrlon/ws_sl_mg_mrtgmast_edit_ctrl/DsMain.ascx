<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_mg_mrtgmast_edit_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 770px;">
            <tr>
                <td colspan="2">
                    <strong style="font-size: 14px;"><u>รายการรับชำระพิเศษ</u></strong>
                </td>
            </tr>
            <tr>
                <td width="13%">
                    <div>
                        <span>ทะเบียนสมาชิก:</span>
                    </div>
                </td>
                <td width="17%">
                    <asp:TextBox ID="member_no" runat="server" Style="width: 90px; text-align: center;"></asp:TextBox>
                    <asp:Button ID="b_search" runat="server" Text="..." Style="width: 25px; margin-left: 2px;" />
                </td>
                <td width="13%">
                    <div>
                        <span>ชื่อ-สกุล:</span>
                    </div>
                </td>
                <td width="27%">
                    <asp:TextBox ID="cp_name" runat="server"></asp:TextBox>
                </td>
                <td width="13%">
                    <div>
                        <span>สถานะสมรส:</span>
                    </div>
                </td>
                <td width="17%">
                    <asp:DropDownList ID="mariage_status" runat="server">
                        <asp:ListItem Value="0">ไม่ระบุ</asp:ListItem>
                        <asp:ListItem Value="1">โสด</asp:ListItem>
                        <asp:ListItem Value="2">สมรส</asp:ListItem>
                        <asp:ListItem Value="3">หย่า</asp:ListItem>
                        <asp:ListItem Value="4">หม้าย</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>บัตรประชาชน:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="card_person" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>สังกัด:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="cp_memgroup" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ชื่อคู่สมรส:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="mate_name" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
