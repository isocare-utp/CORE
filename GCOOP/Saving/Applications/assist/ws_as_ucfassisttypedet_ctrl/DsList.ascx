<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.assist.ws_as_ucfassisttypedet_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />

<table class="DataSourceRepeater">    
    <tr>  
        <th width="15%">
            กลุ่มสมาชิก
        </th>   
        <th width="15%">
            ประเภทสมาชิก
        </th>        
        <th  width="20%">
            ประเภทการจ่าย
        </th>
        <th  width="20%">
            เงื่อนไขต่ำสุด
        </th>
        <th  width="20%">
            เงื่อนไขสูงสุด
        </th> 
        <th  width="20%">
            ยอดเงินที่จ่าย
        </th>                 
        <th width="5%">
            ลบ!
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:DropDownList ID="membcat_code" runat="server" >
                    </asp:DropDownList>        
                </td>
                <td>
                    <asp:DropDownList ID="membtype_code" runat="server" >
                    </asp:DropDownList>        
                </td>
                <td>
                    <asp:DropDownList ID="assistpay_code" runat="server">
                    </asp:DropDownList>                   
                </td> 
                <td>
                    <asp:TextBox ID="min_check" runat="server" Style="text-align: right;" OnKeyPess="return chkNumber(this)"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="max_check" runat="server" Style="text-align: right;" OnKeyPess="return chkNumber(this)"></asp:TextBox>
                </td>   
                <td>
                    <asp:TextBox ID="max_payamt" runat="server" Style="text-align: right;" ToolTip="#,##0.00" OnKeyPess="return chkNumber(this)"></asp:TextBox>
                </td> 
                <td>
                    <asp:Button ID="b_del" runat="server" Text="ลบ" />
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
