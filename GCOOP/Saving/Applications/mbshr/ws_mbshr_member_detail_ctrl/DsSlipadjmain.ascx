<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsSlipadjmain.ascx.cs"
    Inherits="Saving.Applications.mbshr.ws_mbshr_member_detail_ctrl.DsSlipadjmain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 300px;">
            <tr>
                <td width="33%">
                    <div>
                        <sapn>งวด:</sapn>
                    </div>
                </td>
                <td width="66%">
                    <asp:DropDownList ID="adjslip_no" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
