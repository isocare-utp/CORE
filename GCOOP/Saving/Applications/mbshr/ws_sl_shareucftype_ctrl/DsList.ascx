<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.mbshr.ws_sl_shareucftype_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <tr>
        <th width="10%">
            รหัสประเภทหุ้น:
        </th>
        <th>
            ชื่อประเภทหุ้น:
        </th>            
        <th width="5%">
            ลบ!
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="SHARETYPE_CODE" runat="server" Style="text-align: center;" MaxLength="2" ></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="SHARETYPE_DESC" runat="server"></asp:TextBox>
                </td>                                 
                <td>
                    <asp:Button ID="b_del" runat="server" Text="ลบ" />
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
