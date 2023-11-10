<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.admin.w_sheet_ad_currentuser_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" Width="750px">
    <table class="DataSourceRepeater">
        <tr>
            <th width="5%">
                ลำดับ
            </th>
            <th width="15%">
                รหัสผู้ใช้งาน
            </th>
            <th width="20%">
                ชื่อผู้ใช้งาน
            </th>
            <th width="25%">
                ระบบ
            </th>
            <th width="15%">
                หมายเลขไอพี
            </th>
            <th width="20%">
                เวลาเข้าใช้งาน
            </th>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Width="750px" Height="500px" ScrollBars="Auto">
    <table class="DataSourceRepeater">
        <tbody>
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <tr>
                        <td  width="5%">
                            <asp:TextBox ID="running_number" runat="server" ReadOnly="true" Style="text-align: center;"></asp:TextBox>
                        </td>
                        <td width="15%">
                            <asp:TextBox ID="username" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td width="20%">
                            <asp:TextBox ID="full_name" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td width="25%">
                            <asp:TextBox ID="description" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td  width="15%">
                            <asp:TextBox ID="client_ip" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td  width="20%">
                            <asp:TextBox ID="create_time" runat="server" Style="text-align: center;" ReadOnly="true"
                                ToolTip="dd/MM/yyyy HH:mm:ss"></asp:TextBox>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
</asp:Panel>
