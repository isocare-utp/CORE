<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.ap_deposit.ws_dep_closedayprocess_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet" type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit" Style="width: 300px;">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 400px">
            <tr>
                <td width="5%" >
                        <div>
                            <span>วันที่ :<span>
                       </div>
                </td>
                <td width="8%">
                            <asp:TextBox ID="atm_date" runat="server" Style="text-align:center;" ReadOnly="true"></asp:TextBox>
                </td>  
                <td width="5%">
                        <asp:Button ID="b_closedate" runat="server" Text="ปิดงานสิ้นวัน" Style="text-align:center; height:25px; "/> 
                </td>
            </tr>       
        </table>
    </EditItemTemplate>
</asp:FormView>