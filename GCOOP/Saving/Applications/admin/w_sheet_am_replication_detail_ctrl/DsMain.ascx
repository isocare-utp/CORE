<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.admin.w_sheet_am_replication_detail_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 700;">
            <tr>
                <td width="13%">
                    <div>
                        <span>SERVER</span>
                    </div>
                </td>
                <td width="24%">
                    <div>
                        <asp:TextBox ID="sid" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td width="13%">
                    <div>
                        <span>Username</span>
                    </div>
                </td>
                <td width="14%">
                    <div>
                        <asp:TextBox ID="username" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td width="13%">
                    <div>
                        <span>Password</span>
                    </div>
                </td>
                <td width="14%">
                    <div>
                        <asp:TextBox ID="password" runat="server" TextMode="Password"></asp:TextBox>
                    </div>
                </td>
                <td width="9%">
                    <div>
                        <asp:Button ID="b_try" runat="server" Text="ดึงข้อมูล" />
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
