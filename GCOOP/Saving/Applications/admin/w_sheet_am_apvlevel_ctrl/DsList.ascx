<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.admin.w_sheet_am_apvlevel_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
    <span style="text-decoration:underline; font-size:13px; font-weight:bold;">รายการระดับการอนุมัติ</span>


<table class="DataSourceRepeater" style="width: 270px;">

    <tr>
        <th width="35%">
            รหัส
        </th>
        <th width="65%">
            คำอธิบาย
        </th>
    </tr>
    <tbody>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:TextBox ID="APVLEVEL_ID" runat="server" ReadOnly="true" style="text-align:center"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="DESCRIPTION" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
</table>
