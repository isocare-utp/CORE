<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.shrlon.dlg.w_dlg_mg_mrtgmast_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
    <table class="DataSourceRepeater" style="width: 500px;">
    <tr>
        <th width="20%">
            ทะเบียนจำนอง
        </th>
        <th width="35%">
            ประเภทหลักทรัพย์
        </th>
        <th>
            ประเภทจำนอง
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="mrtgmast_no" runat="server" ReadOnly="true" Style="text-align: center;
                        cursor: pointer;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="assettype_desc" runat="server" ReadOnly="true" Style="text-align: center;
                        cursor: pointer;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="cp_mrtgtype" runat="server" ReadOnly="true" Style="cursor: pointer;"></asp:TextBox>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>