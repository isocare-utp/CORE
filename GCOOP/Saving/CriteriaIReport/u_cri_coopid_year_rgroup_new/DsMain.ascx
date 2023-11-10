<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.CriteriaIReport.u_cri_coopid_year_rgroup_new.DsMain" %>
<link id="css1" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="iReportDataSourceFormView" >
        <tr>
                <td>
                    <div>
                        <span>สหกรณ์:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="coop_id" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    <div>
                        <span>ปี พ.ศ. :</span>
                    </div>
                </td>
                <td width="20%">
                   <div>
                        <asp:TextBox ID="year" runat="server" maxlength="4">
                        </asp:TextBox>
                    </div>
                </td>               
            </tr>
            <tr>
                <td width="10%">
                    <div>
                        <span>รหัสสังกัด:</span>
                    </div>
                </td>
                <td >
                    <asp:DropDownList ID="membgroup_start" runat="server">
                    </asp:DropDownList>
                </td>
                </tr>
                <tr>
                <td width="10%">
                 <div>
                        <span>ถึง:</span>
                    </div>
                    </td>
                <td >
                    <asp:DropDownList ID="membgroup_end" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
