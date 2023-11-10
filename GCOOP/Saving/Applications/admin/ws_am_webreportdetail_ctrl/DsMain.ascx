<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.admin.ws_am_webreportdetail_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="15%">
                    <div>
                        <span>application:</span>
                    </div>
                </td>
                <td width="35%">
                    <div>
                        <asp:DropDownList ID="application" runat="server">
                        </asp:DropDownList>
                    </div>
                </td>
                <td width="15%">
                    <div>
                        <span>group:</span>
                    </div>
                </td>
                <td width="35%">
                    <div>
                        <asp:DropDownList ID="group_id" runat="server">
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
