<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wd_main.ascx.cs" Inherits="Saving.Applications.mbshr.ws_sl_share_edit_ctrl.wd_main" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="10%">
                    <div>
                        <span>ทะเบียน:</span>
                    </div>
                </td>
                <td width="20%">
                    <asp:TextBox ID="member_no" runat="server" Style="width: 115px; text-align: center;"></asp:TextBox>
                    <asp:Button ID="b_search" runat="server" Text="..." Style="width: 20px;" />
                </td>
                <td width="10%">
                    <div>
                        <span>ชื่อ-ชื่อสกุล:</span>
                    </div>
                </td>
                <td width="30%">
                    <asp:TextBox ID="cp_name" runat="server"></asp:TextBox>
                </td>
                <td width="10%">
                    <div>
                        <span>ประเภท:</span>
                    </div>
                </td>
                <td width="20%">
                    <asp:TextBox ID="cp_membtype" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เป็นสมาชิก:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="member_date" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>หน่วย:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="cp_membgroup" runat="server"></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>สถานะ:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="member_status" runat="server">
                        <asp:ListItem Value="1">ปกติ</asp:ListItem>
                        <asp:ListItem Value="0">ลาออก</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
