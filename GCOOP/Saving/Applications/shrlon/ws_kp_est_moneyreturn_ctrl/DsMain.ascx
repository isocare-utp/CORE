<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" 
Inherits="Saving.Applications.shrlon.ws_kp_est_moneyreturn_ctrl.DsMain" %>
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="15%">
                    <div>
                        <span>วันที่ทำรายการ : </span>
                    </div>
                </td>
                <td width="35%">
                    <asp:TextBox ID="slip_date" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
           
                <td width="15%">
                    <div>
                        <span>งวด : </span>
                    </div>
                </td>
                <td width="35%">
                    <asp:TextBox ID="recv_period" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="b_post" runat="server" Text="ตั้งข้อมูล" />                
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
