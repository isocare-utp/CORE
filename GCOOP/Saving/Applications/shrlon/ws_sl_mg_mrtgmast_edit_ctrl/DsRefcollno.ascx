<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsRefcollno.ascx.cs"
    Inherits="Saving.Applications.shrlon.ws_sl_mg_mrtgmast_edit_ctrl.DsRefcollno" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater" style="width: 750px;">
    <tr>
        <th width="18%">
            ทะเบียนหลักทรัพย์
        </th>
        <th width="41%">
            รายละเอียดหลักทรัพย์
        </th>
        <th width="18%">
            ราคาประเมิน
        </th>
        <th width="5%">            
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="collmast_no" runat="server" Style="width: 95px; text-align: center;"></asp:TextBox>
                    <asp:Button ID="b_collmast" runat="server" Text="..." Style="width: 28px; margin-left: 1px;" />
                </td>
                <td>
                    <asp:TextBox ID="collmast_desc" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="estimate_price" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="b_del" runat="server" Text="ลบ" />
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
