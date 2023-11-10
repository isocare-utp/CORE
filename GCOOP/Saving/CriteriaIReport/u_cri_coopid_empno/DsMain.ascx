<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.CriteriaIReport.u_cri_coopid_empno.DsMain" %>
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
                <td width="30%">
                    <div>
                        <span>รหัสพนักงาน:</span></div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="start_empno" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="30%">
                    <div>
                        <span>ถึงรหัสพนักงาน:</span></div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="end_empno" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
