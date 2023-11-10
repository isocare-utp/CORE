<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.mbshr.dlg.w_dlg_mbshr_mbgain_history_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 300px;">
            <tr>
                <td width="15%">
                    <div>
                        <span>เลขสมาชิก :</span>
                    </div>
                </td>
                <td width="16%">
                    <asp:TextBox ID="member_no" runat="server"></asp:TextBox>
                </td>
                
                <td width="6%">
                    <asp:Button ID="b_search" runat="server" Text="ค้นหา" />
                </td>
  
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
