<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsCremation.ascx.cs" 
Inherits="Saving.Applications.mbshr.ws_mbshr_member_detail_main_ctrl.DsCremation" %>
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

    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
            <td width="5%">
                        <asp:TextBox ID="SEQ_NO" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>                  
                    </td>
      <%--       <td width="40%">
                    <asp:DropDownList ID="CMTTYPE_CODE" runat="server">
                    </asp:DropDownList>                   
                 </td > --%>
                <td width="40%">
                        <asp:TextBox ID="DESCRIPTION" runat="server"></asp:TextBox>
                    </td> 
                           <th width="20%">
                    <asp:TextBox ID="ITEM_AMOUNT" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </th>       
    <%--             <td style="border-style: none; text-align: right" colspan="2">
            <strong>รวม:</strong>
        </td>
        <td style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #000000;">
            <asp:TextBox ID="cp_sum_item" runat="server" Style="text-align: right; font-weight: bold;"></asp:TextBox>
        </td>--%>


            </tr>
       </ItemTemplate>
    </asp:Repeater>
</table>