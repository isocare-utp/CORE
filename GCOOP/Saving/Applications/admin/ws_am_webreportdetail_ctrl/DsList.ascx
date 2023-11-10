<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.admin.ws_am_webreportdetail_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" Width="750px"  ScrollBars="Horizontal">
    <table class="DataSourceRepeater" style="width:900px;">
        <thead>
            <tr>
                <th width="10%">
                    report_id
                </th>
                <th width="14%">
                    report_name
                </th>
                <th width="14%">
                    report_object
                </th>
                <th width="14%">
                    cri_object
                </th>
                <th width="14%">
                    cre_object
                </th>
                <th width="2%">
                    use_flag
                </th>
                <th width="2%">
                    open_type
                </th>
                <th width="2%">
                    core_flag
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
                            <asp:TextBox ID="REPORT_ID" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="REPORT_NAME" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="REPORT_DWOBJECT" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="REPORT_CRIOBJECT" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="REPORT_CREOBJECT" runat="server"></asp:TextBox>
                        </td>
                        <td style="text-align:center;">
                            <asp:CheckBox ID="USED_FLAG" runat="server" />
                        </td>
                        <td >
                            <asp:TextBox ID="OPEN_TYPE" runat="server" style="text-align:center;"></asp:TextBox>
                        </td>
                        <td style="text-align:center;">
                            <asp:CheckBox ID="CORE_FLAG" runat="server" />
                        </td>
                        <td align="center">
                            <asp:Button ID="B_DEL" runat="server" Text="ลบ" />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
</asp:Panel>
