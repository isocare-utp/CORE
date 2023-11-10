<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_transfer_new_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <tr>
        <th width="10%">
            วันที่รับโอน
        </th>
        <th width="5%">
            งวดรับโอน
        </th> 
        <th width="10%">
            ยอดหุ้นรับโอน
        </th> 
         <th width="10%">
            เงินกู้รับโอน
        </th>   
        <th width="40%">
            รายละเอียดรับโอน
        </th>   
<%--           <th width="5%">
            ลบ
        </th>  --%> 

    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
<%--            <td width="5%">
                        <asp:TextBox ID="SEQ_NO" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>                  
                    </td>--%>
<%--            <td width="40%">
                    <asp:DropDownList ID="CMTTYPE_CODE" runat="server">
                    </asp:DropDownList>                   
                 </td > --%>
                <td width="10%">
                     <asp:TextBox ID="TRAN_DATE"  runat="server" Style="text-align: center;"></asp:TextBox>
                </td> 
                <th width="5%">
                    <asp:TextBox ID="TRAN_PERIOD" runat="server" Style="text-align: center;" ToolTip="#,##0"></asp:TextBox>
                </th>    
                <th width="10%">
                    <asp:TextBox ID="TRAN_SHARE" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </th>   
                  <th width="10%">
                    <asp:TextBox ID="TRAN_LOAN" runat="server" Style="text-align: right;" ToolTip="#,##0.00"></asp:TextBox>
                </th>    
                  <th width="40%">
                    <asp:TextBox ID="TRAN_DETAIL" runat="server"  ></asp:TextBox>
                </th>   
               <%-- <th width="5%">
                    <asp:Button ID="b_del" runat="server" Text="ลบ" />
                </th>--%>

            </tr>
       </ItemTemplate>
    </asp:Repeater>
</table>
