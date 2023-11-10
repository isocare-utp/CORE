<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.CriteriaIReport.u_cri_coopid_rdepartment.DsMain" %>
<link id="css1" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="iReportDataSourceFormView">
            <tr>
                <td>
                    <div>
                        <span>สาขา:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="coop_id" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ตั้งแต่เครื่อข่าย:</span></div>
                </td>
                <td>
                    <asp:TextBox ID="sdepartment_code" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="sdepartment_desc" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ถึงเครือข่าย:</span></div>
                </td>
                <td>
                    <asp:TextBox ID="edepartment_code" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="edepartment_desc" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
