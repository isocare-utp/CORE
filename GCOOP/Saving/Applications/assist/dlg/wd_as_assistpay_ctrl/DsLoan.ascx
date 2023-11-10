<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsLoan.ascx.cs" Inherits="Saving.Applications.assist.dlg.wd_as_assistpay_ctrl.DsLoan" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"    type="text/css" />


<table class="DataSourceFormView">
    <tr>  
        <td colspan="4">
            <strong style="font-size: 13px;">รายการหักชำระ</strong>
        </td> 
    </tr>
</table>
<table class="DataSourceRepeater">
    <tr align="center">  
        <th width="20%">
            <span>ประเภทหักชำระ</span>
        </th>   
        <th width="52%">
            <span>รายละเอียด</span>
        </th>
        <th width="25%">
            <span>จำนวนเงิน</span>
        </th>
        <th width="3%">
            
        </th>
    </tr>
   
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>    
            <tr>
                <td>
                    <asp:DropDownList ID="methpaytype_code" runat="server" >
                        <asp:ListItem Value="LON">LON</asp:ListItem>
                        <asp:ListItem Value="ETC">ETC</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="description" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="itempay_amt" runat="server" Style="text-align: right; font-size: 11px;" ToolTip="#,##0.00" ></asp:TextBox>
                </td>
                <td align="center">
                    <asp:Button ID="b_delloan" runat="server" Text="-" />
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
