<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_cremation_detail_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <tr>
        <th width="5%">
            ลำดับ
        </th>
            <th width="40%">
            รหัส
        </th>
         <th width="20%">
            ยอดเงิน
        </th>   
           <th width="5%">
            ลบ
        </th>   

    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
            <td width="5%">
                        <asp:TextBox ID="SEQ_NO" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>                  
                    </td>
            <td width="40%">
                    <asp:DropDownList ID="CMTTYPE_CODE" runat="server">
                    </asp:DropDownList>                   
                 </td > 
                <%--           <td width="20%">
                        <asp:TextBox ID="DESCRIPTION" runat="server"></asp:TextBox>
                    </td> --%>
                           <th width="20%">
                    <asp:TextBox ID="ITEM_AMOUNT" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </th>       
                <th width="5%">
                    <asp:Button ID="b_del" runat="server" Text="ลบ" />
                </th>

            </tr>
       </ItemTemplate>
    </asp:Repeater>
</table>
