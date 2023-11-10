<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsPics.ascx.cs" Inherits="Saving.Applications.ap_deposit.ws_dep_deptdetail_ctrl.DsPics" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="TbStyle" >
    <tr>
        <th rowspan="2" width="14%" style="border:2px;">
            ลำดับ
        </th>
        <td rowspan="2" style="border:0px;">
        </td>
             
    </tr>
</table>

    <table class="TbStyle" >
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td style="width: 88px;">
                        <asp:TextBox ID="SEQ_NO" runat="server" Style="text-align: center;" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td style="border:0px;">
                    </td>
                </tr>
                
            </ItemTemplate>      
        </asp:Repeater>
    </table>

