<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.assist.ws_as_cancelrequest_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="10%" valign="top">
                    <div>
                        <span>เลขสมาชิก:</span>
                    </div>
                </td>
                <td width="14%" valign="top">
                    <asp:TextBox ID="member_no" runat="server"></asp:TextBox> 
                </td>
                <td width="3%" valign="top">
                    <asp:Button ID="b_searchMem" runat="server" Text="..." Width="20px" />
                </td>
                <td width="17%" valign="top">
                    <div>
                        <span>ประเภทสวัสดิการ:</span>
                    </div>
                </td>
                <td width="7%" valign="top">
                    <asp:TextBox ID="assist_code" runat="server"></asp:TextBox>
                </td>
                <td width="30%" valign="top">
                    <asp:DropDownList ID="assisttype_code" runat="server">
                    </asp:DropDownList>
                </td>
                <td width="20%" valign="top">
                    <asp:Button ID="b_search" runat="server" Text="ดึงข้อมูล" Width="60px" />    <asp:Button ID="b_clear" runat="server" Text="ล้างข้อมูล" Width="60px" />
                </td>
            </tr>
        </table>
        <br />
        <table class="DataSourceFormView">
            <tr>
                <td width="15%" colspan="2">
                    <asp:CheckBox ID="select_check" runat="server" Text=" เลือกทั้งหมด" />
                </td>                             
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
