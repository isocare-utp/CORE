<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsGroup.ascx.cs" Inherits="Saving.Applications.admin.w_sheet_am_amsecwins_ctrl.DsGroup" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" width="500">
            <tr>
                <td width="15%">
                    <div>
                        <span>ชื่อระบบ:</span>
                    </div>
                </td>
                <td width="35">
                    <div>
                        <asp:DropDownList ID="application" runat="server">
                        </asp:DropDownList>
                    </div>
                </td>
                <td width="15%">
                    <div>
                        <span>ชื่อกลุ่ม:</span>
                    </div>
                </td>
                <td width="35">
                    <div>
                        <asp:DropDownList ID="group_code" runat="server">
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
