<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.CriteriaIReport.u_cri_coopid_adate_membcode.DsMain" %>
<link id="css1" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="iReportDataSourceFormView">
        <tr>
                <td width="30%">
                    <div>
                        <span>สาขา:</span>
                    </div>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="coop_id" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>วันที่:</span></div>
                </td>
                <td>
                    <asp:TextBox ID="appl_startdate" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="appl_stopdate" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="30%">
                    <div>
                        <span>ตามสังกัด:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="membgroup_start" runat="server">
                    </asp:DropDownList>
                    -
                </td>
                <td width="35%">
                    <asp:DropDownList ID="membgroup_end" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
