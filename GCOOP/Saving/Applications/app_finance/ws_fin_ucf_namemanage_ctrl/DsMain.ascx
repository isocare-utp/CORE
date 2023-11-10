<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.app_finance.ws_fin_ucf_namemanage_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet" type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width:720px;">           
            <tr>
                <td  width="21%">
                    <span>เจ้าหน้าที่การเงิน:</span>                    
                </td>                
                <td width="50%">
                    <asp:TextBox ID="finanecial_name" runat="server" ></asp:TextBox>
                </td>                                            
            </tr>
            <tr>               
                <td>
                    <span>เจ้าหน้าที่บัญชี:</span>
                </td>
                <td>
                    <asp:TextBox ID="accountant_name" runat="server" ></asp:TextBox>
                </td>                               
            </tr>
            <tr>                
                <td>
                    <span>ผู้จัดการ:</span>
                </td>
                <td>
                    <asp:TextBox ID="manager_name" runat="server"></asp:TextBox>     
                </td>                                                    
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
