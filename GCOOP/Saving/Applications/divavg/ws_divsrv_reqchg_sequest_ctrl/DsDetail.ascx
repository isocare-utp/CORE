<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDetail.ascx.cs" Inherits="Saving.Applications.divavg.ws_divsrv_reqchg_sequest_ctrl.DsDetail" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="15%">
                    <div>
                        <span>อายัด:</span>
                    </div>
                </td>
                <td width="20%">
                    <asp:DropDownList ID="sequest_flag" runat="server" Enabled="False">
                        <asp:ListItem Value="0">ไม่อายัด</asp:ListItem>
                        <asp:ListItem Value="1">อายัด</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td></td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
