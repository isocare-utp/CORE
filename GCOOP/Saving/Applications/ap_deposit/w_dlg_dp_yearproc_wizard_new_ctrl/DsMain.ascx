<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.ap_deposit.w_dlg_dp_yearproc_wizard_new_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 400px">
            <tr>
                <td width="15%">
                    <div>
                        <span style="text-align: center">ปี</span>
                    </div>
                </td>
                <td width="50%">
                    <div>
                        <asp:TextBox ID="closeyear" runat="server" MaxLength="4" Style="text-align: center"></asp:TextBox>
                    </div>
                </td>
                <td width="30%">
                    <div>
                        <asp:Button ID="b_closeyear" runat="server" Text="ปิดงานสิ้นปี" />
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
