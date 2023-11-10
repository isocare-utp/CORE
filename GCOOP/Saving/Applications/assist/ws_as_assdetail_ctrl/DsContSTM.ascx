<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsContSTM.ascx.cs" Inherits="Saving.Applications.assist.ws_as_assdetail_ctrl.DsContSTM" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater" style="width: 520px;">    
    <tr>  
        <th width="5%">
        </th>        
        <th  width="15%">
            วันที่รายการ
        </th>
        <th  width="24%">
            รายการ
        </th>
        <th  width="18%">
            จำนวนเงิน
        </th> 
        <th  width="18%">
            ยอดรับแล้ว
        </th> 
        <th width="20%">
            ผู้ทำรายการ
        </th>  
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="seq_no" runat="server" Style="text-align: center;" ReadOnly="True"></asp:TextBox>       
                </td>
                <td>
                    <asp:TextBox ID="operate_date" runat="server" Style="text-align: center;" ReadOnly="True"></asp:TextBox>                 
                </td> 
                <td>
                    <asp:TextBox ID="itemdesc" runat="server" Style="text-align: left;"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="payvalue" runat="server" Style="text-align: right;" ToolTip="#,##0.00" ReadOnly="True"></asp:TextBox>
                </td>   
                <td>
                    <asp:TextBox ID="pay_balance" runat="server" Style="text-align: right;" ToolTip="#,##0.00" ReadOnly="True"></asp:TextBox>
                </td>   
                <td>
                    <asp:TextBox ID="entry_id" runat="server" Style="text-align: center;" ReadOnly="True"></asp:TextBox>
                </td>               
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
