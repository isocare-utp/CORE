<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.fund.ws_fund_ucf_fundcollkeeptype_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <tr>
        <th width="10%">
            รหัสกองทุน
        </th>
        <th width="40%"> 
            คำอธิบาย
        </th>
        <th width="10%">
            ลำดับ
        </th>
         <th width="5%">
            ลบ!
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="FUNDKEEPTYPE" runat="server" Style="text-align: center;" MaxLength="2" ></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="FUNDKEEPDESC" runat="server"></asp:TextBox>
                </td>                
                <td>
                    <asp:TextBox ID="SORT" runat="server" Style="text-align: center;"></asp:TextBox>
                </td> 
                <td>
                    <asp:Button ID="b_del" runat="server" Text="ลบ" />
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
