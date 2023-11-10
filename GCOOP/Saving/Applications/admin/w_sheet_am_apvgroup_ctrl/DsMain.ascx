<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.admin.w_sheet_am_apvgroup_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <thead>
        <tr>
            <th width="10%">
            </th>
            <th width="20%">
                ระดับการอนุมัติ
            </th>
            <th width="65%">
                กลุ่มการอนุมัติ
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
                        <asp:TextBox ID="APVLEVEL_TID" runat="server" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList ID="APVLEVEL_ID" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:TextBox ID="GROUP_APV" runat="server"></asp:TextBox>
                    </td>
                    <td align="center">
                        <asp:Button ID="B_DEL" runat="server" Text="ลบ" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
</table>
