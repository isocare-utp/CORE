<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.CriteriaIReport.u_cri_coopid_memno_rdeptgroup_rdeptaccountno_user.w_dlg_deptno_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit" Width="500px" >
    <EditItemTemplate>
        <table class="iReportDataSourceFormView" style="width: 500px;">
            <tr>
                <td>  
                    <div>
                        <span>บัญชีเงินฝาก:</span>
                    </div>       
                </td>
                <td>  
                    <asp:TextBox ID="deptno" runat="server" ReadOnly="true"></asp:TextBox>          
                </td>                           
            </tr> 
            <tr>
                <td>  
                    <asp:Button ID="b_choose" runat="server" Text="เลือกบัญชี"/> 
                    <asp:Button ID="b_confrim" runat="server" Text="ตกลง"/>    
                    <asp:Button ID="b_cancel" runat="server" Text="ยกเลิก"/>    
                </td>                           
            </tr> 
        </table>
    </EditItemTemplate>
</asp:FormView>

    
