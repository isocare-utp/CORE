<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.fund.ws_fund_process_payfundcoll_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">          
             <tr>
                <td>
                    <div><u>รายการคืนกองทุน</u></div>
                </td>
            </tr>
            <tr>
                <td width="55%" colspan="2">
                    <asp:CheckBox ID="all_check" runat="server" Text=" เลือกทั้งหมด" />
                </td>
        </table>
    </EditItemTemplate>
</asp:FormView>