<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsLoan2.ascx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_member_detail_main_ctrl.DsLoan2" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="FormStyle">
            <tr>
                <td>
                    <asp:CheckBox ID="check_loan" runat="server" />สัญญาปัจจุบัน
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>