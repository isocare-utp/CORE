<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_checkpermiss_reprint_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
    <asp:Button ID="b_1" runat="server" Text="พิมพ์ใบปะหน้าขอกู้" />
    &nbsp;&nbsp;&nbsp;
        <table class="DataSourceFormView">
           <tr>
                <td width="10%">
                    <div>
                        <span>เลขสมาชิก</span></div>
                </td>
                <td width="20%">
                    <asp:TextBox ID="member_no" runat="server"></asp:TextBox>
                </td>
                <td width="10%">
                    <div>
                        <span>ชื่อ-นามสกุล</span></div>
                </td>
                <td width="20%">
                    <asp:TextBox ID="membname" runat="server" ReadOnly></asp:TextBox>
                </td>
                <td>
                    <div>
                        <span>ผู้ทำรายการ</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="entry_id" runat="server" ReadOnly></asp:TextBox>
                </td>
                <td>
                    
                </td>
                <td colspan="3">
                   
                </td>
                <td>
                   
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
