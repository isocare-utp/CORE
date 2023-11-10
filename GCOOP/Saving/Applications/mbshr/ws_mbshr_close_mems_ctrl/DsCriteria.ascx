<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsCriteria.ascx.cs"
    Inherits="Saving.Applications.mbshr.ws_mbshr_close_mems_ctrl.DsCriteria" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<link id="css2" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="15%">
                    <div>
                        <span>บัญชี:</span>
                    </div>
                </td>
                <td>
                    <div>
                        <asp:DropDownList ID="close_type" runat="server">
                            <asp:ListItem Value="0">รายชื่อสมาชิกที่สามาถปิดบัญชีได้</asp:ListItem>
                            <asp:ListItem Value="1">รายชื่อสมาชิกที่มีภาระกับสหกรณ์</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </td>
                <td width="15%">
                    <div>
                        <span>ทะเบียน:</span>
                    </div>
                </td>
                <td width="15%">
                    <div>
                        <asp:TextBox ID="member_no" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td width="10%">
                    <div>
                        <asp:Button ID="b_search" runat="server" Text="ค้นหา" />
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
