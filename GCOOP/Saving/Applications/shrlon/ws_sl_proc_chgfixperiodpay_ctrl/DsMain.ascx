<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_proc_chgfixperiodpay_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit" Width="480px" 
    Height="38px">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 500px">
            <tr>
                <td width="25%">
                    <div>
                        <span>วันที่ประมวลผล :</span></div>
                </td>
                <td width="50%">
                    <asp:TextBox ID="date" runat="server"></asp:TextBox>
                </td>
                <td width="25%">
                    <asp:Button ID="b_proc" runat="server" Text="ประมวลผล" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Label ID="Lbwarring" Font-Size="14pt" BackColor="White" BorderColor="White" Font-Bold="true" Text="**เป็นการประมวลผลสำหรับเงินกู้ประเภทคงยอด" runat="server"/>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
