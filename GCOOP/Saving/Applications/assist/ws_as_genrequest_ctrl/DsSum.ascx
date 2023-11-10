<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsSum.ascx.cs" Inherits="Saving.Applications.assist.ws_as_genrequest_ctrl.DsSum" %>


<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />


<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit" 
    style="margin-right: 10px" Width="1113px" >
    <EditItemTemplate>
    <table cellspacing="0" rules="all" class="DataSourceFormView" border="0" style="border-collapse: collapse; width:1113px"> 
    <td style="border-style: none;background-color:#FFFF99; text-align: right; font-size:14px;" width="16%">
            <strong> <center> จำนวนรายการที่เลือก</center></strong>
        </td>
        <td width="16%" style="border-style: none;">
                        <asp:TextBox ID="count_flag" runat="server" ToolTip="#,##0" Style="text-align: right;background-color:#FFE4E1" ReadOnly="true" Width="100%"></asp:TextBox>
                  
                </td> 
                 <td style="border-style: none;background-color:#FFFF99; text-align: right; font-size:14px;" width="29%">
            <strong> <center> รวมจำนวนเงิน</center></strong>
        </td>
              <td width="10%" style="border-style: none;">
                        <asp:TextBox ID="sum_maxpayamt" runat="server" ToolTip="#,##0.00" Style="text-align: right;background-color:#FFE4E1" ReadOnly="true" Width="99%"></asp:TextBox>
                  
                </td> 
                <td width="10%" style="border-style: none;">
                        <asp:TextBox ID="sum_assistcutamt" runat="server" ToolTip="#,##0.00" Style="text-align: right;background-color:#FFE4E1" ReadOnly="true" Width="99%"></asp:TextBox>
                  
                </td> 
                  <td width="10%" style="border-style: none;">
                        <asp:TextBox ID="sum_itempayamt" runat="server" ToolTip="#,##0.00" Style="text-align: right;background-color:#B0C4DE" ReadOnly="true" Width="99%"></asp:TextBox>
               
                </td>
                <td width="5%" style="border-style: none;">
                </td> 
                <td width="13%" style="border-style: none;">
                </td> 
      
    </table>
 </EditItemTemplate>


</asp:FormView>
