<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_proc_paymoneyreturn_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet" type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit" Width="480px">
    <EditItemTemplate>
        <table class="DataSourceFormView" width="500px">
            <tr>
                <td > <div> <span> วันที่คืนเงิน :</span> </div> </td>
                <td > <asp:TextBox ID="paymrt_date" runat="server" Style="text-align:center;"></asp:TextBox></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Button ID="b_proc" runat="server" Text="ประมวล" Width="200px" Height="30px" style="margin-left:300px" />
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
