<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsInsurance.ascx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_crenation_ctrl.DsInsurance" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<span class="TitleSpan">ข้อมูลประกัน</span>
<table class="DataSourceRepeater">
    <tr>       
        <th width="25%">
            ประเภท
        </th>
         <th width="20%">
            รายละเอียด
        </th> 
         <th width="15%">
            วันที่คุ้มครอง
        </th>          
        <th width="20%">
            วงเงิน
        </th>     
        <th width="20%">
            เบี้ยประกัน
        </th>  
        <th  width="5%">
        </th>       
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
    <ItemTemplate>
    <tr>
        <td>
            <asp:DropDownList ID="cmttype_code" runat="server" >
            </asp:DropDownList>                  
        </td>
        <td>
            <asp:TextBox ID="cmtaccount_name" runat="server"></asp:TextBox>              
        </td >    
        <td>
            <asp:TextBox ID="CREMATION_DATE" runat="server"  Style="text-align: center;" ></asp:TextBox>              
        </td >                     
        <td>
            <asp:TextBox ID="cremation_amt" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
        </td>  
         <td>
            <asp:TextBox ID="INS_AMT" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
        </td>                   
        <th>
            <asp:Button ID="b_del" runat="server" Text="ลบ" />
        </th>
    </tr>
    </ItemTemplate>
    </asp:Repeater>
</table>
