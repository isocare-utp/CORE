<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsSum.ascx.cs" Inherits="Saving.Applications.mbshr.ws_mthexpense_ctrl.DsSum" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet" type="text/css" />

<asp:FormView ID="FormViewMain" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width:auto;">
            <tr>
              <td style="width:220px"></td> 
            <td align="center">
            รวมจำนวนเงินจ่าย 
            </td>
              <td></td>  <td></td>   
                <td width="0%" style="font-size: 20px;">
                    <asp:TextBox ID="Sum_amt" runat="server" Style="text-align: right ; Width:90px; font-size: 15px; background-color:#DDDDDD;"
                    ToolTip="#,##0.00" ReadOnly="True"></asp:TextBox>
                </td>            
            </tr>
            <tr>
              <td style="width:520px"></td> 
            <td align="center">
            รวมจำนวนเงินรับ 
            </td>
              <td></td>  <td></td>   
                <td width="0%" style="font-size: 20px;">
                    <asp:TextBox ID="SUM_RECV" runat="server" Style="text-align: right ; Width:90px; font-size: 15px; background-color:#DDDDDD;"
                    ToolTip="#,##0.00" ReadOnly="True"></asp:TextBox>
                </td>            
            </tr>
        </table>
    </EditItemTemplate> 
</asp:FormView>