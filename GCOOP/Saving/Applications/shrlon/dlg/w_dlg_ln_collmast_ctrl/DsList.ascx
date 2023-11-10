<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.shrlon.dlg.w_dlg_ln_collmast_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater" style="width: 600px;">
    <tr>
        <th width="14%">
            ทะเบียนหลักทรัพย์
        </th>
        <th width="14%">
            เลขหลักทรัพย์
        </th>
        <th width="14%">
            ประเภท
        </th>
        <th>
            รายละเอียด
        </th>
        <th width="14%">
            ราคาประเมิน
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="collmast_no" runat="server" ReadOnly="true" Style="text-align: center;
                        cursor: pointer;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="collmast_refno" runat="server" ReadOnly="true" Style="text-align: center;
                        cursor: pointer;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="collmasttype_desc" runat="server" ReadOnly="true" Style="cursor: pointer;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="collmast_desc" runat="server" ReadOnly="true" Style="cursor: pointer;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="estimate_price" runat="server" ReadOnly="true" Style="cursor: pointer;
                        text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
