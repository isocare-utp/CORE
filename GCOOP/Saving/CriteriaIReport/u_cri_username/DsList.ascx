<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.CriteriaIReport.u_cri_username.DsList" %>
<link id="css1" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" Width="520px">
    <table class="DataSourceRepeater" style="width: 500px;">
        <thead>
            <tr>
                <th width="5%">
                    รหัสผู้ใช้งาน
                </th>
                <th width="15%">
                    ชื่อผู้ใช้งาน
                </th>
            </tr>
        </thead>
    </table>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Width="520px" Height="235px" ScrollBars="Auto">
    <table class="DataSourceRepeater" style="width: 500px;">
        <tbody>
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <tr>
                        <td width="15%">
                            <asp:TextBox ID="user_name" runat="server"></asp:TextBox>
                        </td>
                        <td width="35%">
                            <asp:TextBox ID="FULL_NAME" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
</asp:Panel>
