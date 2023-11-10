<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsFollowMast.ascx.cs"
    Inherits="Saving.Applications.shrlon.w_sheet_ln_nplmaster_ctrl.DsFollowMast" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 686px;">
            <tr>
                <td width="15%">
                    <div>
                        <span>เลือกการติดตาม</span>
                    </div>
                </td>
                <td width="25%">
                    <div>
                        <asp:DropDownList ID="follow_seq" runat="server">
                        </asp:DropDownList>
                    </div>
                </td>
                <td width="13%">
                    <div>
                        <span>ชื่อการติดตาม</span>
                    </div>
                </td>
                <td width="25%">
                    <div>
                        <asp:TextBox ID="DESCRIPTION" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td width="15%">
                    <div>
                        <asp:Button ID="b_del" runat="server" Text="ลบการติดตามนี้" />
                    </div>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
