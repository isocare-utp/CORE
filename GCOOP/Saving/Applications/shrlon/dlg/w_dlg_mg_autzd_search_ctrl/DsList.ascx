<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.shrlon.dlg.w_dlg_mg_autzd_search_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
    <table class="DataSourceRepeater" style="width: 620px;">
    <tr>
        <th width="8%">
            ลำดับที่
        </th>
        <th width="32%">
            ชื่อผู้รับมอบอำนาจ
        </th>
        <th>
            ที่อยู่
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="template_no" runat="server" ReadOnly="true" Style="text-align: center;
                        cursor: pointer;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="autzd_name" runat="server" ReadOnly="true" Style="text-align: center;
                        cursor: pointer;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="cp_address" runat="server" ReadOnly="true" Style="cursor: pointer;"></asp:TextBox>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>