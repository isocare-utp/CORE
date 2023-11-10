<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.assist.dlg.ws_dlg_ass_edit_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="7%" valign="top">
                    <div>
                        <span>ทะเบียน:</span>
                    </div>
                </td>
                <td width="14%" valign="top">
                    <asp:TextBox ID="member_no" runat="server" Style="text-align: center; "></asp:TextBox> 
                </td>
               
                <td width="10%" valign="top">
                    <div>
                        <span>ประเภทสวัสดิการ:</span>
                    </div>
                </td>
                <td width="5%" valign="top">
                    <asp:TextBox ID="assist_code" runat="server"></asp:TextBox>
                </td>
                <td width="20%" valign="top">
                    <asp:DropDownList ID="assisttype_code" runat="server">
                    </asp:DropDownList>
                </td>
                <td width="20%" valign="top">
                    <asp:Button ID="b_search" runat="server" Text="ดึงข้อมูล" Width="60px" />   
                </td>
            </tr>
        </table>
        <br />

    </EditItemTemplate>
</asp:FormView>
