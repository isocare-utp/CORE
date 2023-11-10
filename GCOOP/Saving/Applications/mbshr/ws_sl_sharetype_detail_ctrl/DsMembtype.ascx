<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMembtype.ascx.cs"
    Inherits="Saving.Applications.mbshr.ws_sl_sharetype_detail_ctrl.DsMembtype" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="15%"  >
                    <div>
                        <span>ประเภทสมาชิก</span></div>
                </td>
                <td>
                    <asp:DropDownList ID="membtype" runat="server">
                        <asp:ListItem Value="0">--เลือกประเภท--</asp:ListItem>
                        <asp:ListItem Value="1">สมาชิกปกติ</asp:ListItem>
                        <asp:ListItem Value="2">สมาชิกสมทบ</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
