<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_cancel_all_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit" Width="480px">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="15%">
                    <div>
                        <span>เลขสมาชิก:</span></div>
                </td>
                <td width="20%">
                    <asp:TextBox ID="member_no" runat="server" Width="120px"></asp:TextBox>
                    <asp:Button ID="b_search" runat="server" Text=".." Width="20px" />
                </td>
                <td width="15%">
                    <div>
                        <span>ชื่อ-สกุล:</span></div>
                </td>
                <td width="50%">
                    <asp:TextBox ID="memb_name" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
