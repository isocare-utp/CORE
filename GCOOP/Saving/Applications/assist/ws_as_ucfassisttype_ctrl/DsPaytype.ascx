<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsPaytype.ascx.cs" Inherits="Saving.Applications.assist.ws_as_ucfassisttype_ctrl.DsPaytype" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater" style="width: 98%">
    <tr>
        <th width="10%">
            รหัส
        </th>
        <th>
            คำอธิบาย
        </th>        
        <th width="5%">
            ลบ!
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server" >
        <ItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="ASSISTPAY_CODE" runat="server" Style="text-align: center;" MaxLength="2" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="ASSISTPAY_DESC" runat="server" style="text-indent: 10px;"></asp:TextBox>
                </td>                
                <td>
                    <asp:Button ID="b_del" runat="server" Text="ลบ" />
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
