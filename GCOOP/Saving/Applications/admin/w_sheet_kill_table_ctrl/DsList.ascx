<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.admin.w_sheet_kill_table_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server"  ScrollBars="Horizontal">
    <table class="DataSourceRepeater" >
        <thead>
            <tr>
                <th width="30%">
                    TABLENAME
                </th>
                <th width="30%">
                    RESOURCE_TYPE
                </th>
                <th width="20%">
                    RESOURCE_DESCRIPTION
                </th>
                <th width="15%">
                    request_session_id
                </th>
                <th width="5%">
                </th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:TextBox ID="TABLENAME" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="RESOURCE_TYPE" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="RESOURCE_DESCRIPTION" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="request_session_id" runat="server"></asp:TextBox>
                        </td>
                        <td align="center">
                            <asp:Button ID="B_DEL" runat="server" Text="KILL" />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
</asp:Panel>
