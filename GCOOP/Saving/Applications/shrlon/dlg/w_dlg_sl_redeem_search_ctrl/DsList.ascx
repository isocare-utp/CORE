<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.shrlon.dlg.w_dlg_sl_redeem_search_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater" style="width: 610px;">
    <tr>
        <th width="16%">
            ทะเบียน ลท
        </th>        
        <th width="16%">
            ทะเบียนสมาชิก
        </th>       
        <th width="16%">
            ชื่อ
        </th>
        <th width="16%">
            นามสกุล
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
                    <asp:TextBox ID="member_no" runat="server" ReadOnly="true" Style="cursor: pointer;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="memb_name" runat="server" ReadOnly="true"
                        Style="text-align: right; cursor: pointer;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="memb_surname" runat="server" ReadOnly="true"
                        Style="text-align: right; cursor: pointer;"></asp:TextBox>
                </td>
                <!--<td>
                    <asp:TextBox ID="collmast_price" runat="server" ToolTip="#,##0.00" ReadOnly="true"
                        Style="text-align: right; cursor: pointer;"></asp:TextBox>
                </td>-->
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
