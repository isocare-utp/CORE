<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DwTax.ascx.cs" Inherits="Saving.Applications.app_finance.w_sheet_finslip_spc_ctrl.DwTax" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet" type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width:720px;">
            <tr>
                <td>
                    <span>เลขที่ผู้เสียภาษี :</span>                    
                </td>
                 <td Width="20%">
                    <asp:TextBox ID="TAXPAY_ID" runat="server" ></asp:TextBox>           
                </td>
                <td>
                    <span>ที่อยู่ :</span>                    
                </td>
                <td>
                    <asp:TextBox ID="TAXPAY_ADDR" runat="server"  Width="100%"></asp:TextBox>
                </td>                                              
            </tr>    
            <tr>
                <td>
                    <span>หมายเหตุ :</span>                    
                </td>
                 <td colspan="3">
                    <asp:TextBox ID="TAXPAY_DESC" runat="server"  Width="100%"></asp:TextBox>
                </td>                                                         
            </tr>            
        </table>
    </EditItemTemplate>
</asp:FormView>
