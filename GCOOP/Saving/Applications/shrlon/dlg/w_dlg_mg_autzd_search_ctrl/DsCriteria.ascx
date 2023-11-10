<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsCriteria.ascx.cs"
    Inherits="Saving.Applications.shrlon.dlg.w_dlg_mg_autzd_search_ctrl.DsCriteria" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 550px;">
            <tr>
                <td width="25%">
                    <div>
                        <span>ชื่อผู้รับมอบอำนาจ:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:TextBox ID="autzd_name" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
