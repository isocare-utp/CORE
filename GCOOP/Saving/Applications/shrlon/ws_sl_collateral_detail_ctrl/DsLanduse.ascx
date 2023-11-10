<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsLanduse.ascx.cs" Inherits="Saving.Applications.shrlon.ws_sl_collateral_detail_ctrl.DsLanduse" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="FormStyle">
            <tr>
                <td width="20%">
                    <div>
                        <span>สถานที่ตั้ง</span></div>
                </td>
                <td width="80%">
                    <asp:TextBox ID="desc_position" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>สาธาณูปโภค</span></div>
                </td>
                <td>
                    <asp:TextBox ID="desc_utility" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เส้นทางคมนาคม</span></div>
                </td>
                <td>
                    <asp:TextBox ID="desc_travel" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>อื่นๆ</span></div>
                </td>
                <td>
                    <asp:TextBox ID="desc_etc" runat="server"></asp:TextBox>
                </td>
            </tr>
           <%-- <tr>
                <td>
                    <div>
                        <span>สถานะการครอบครอง</span></div>
                </td>
                <td>
                    <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                </td>
            </tr>--%>
        </table>
    </EditItemTemplate>
</asp:FormView>
