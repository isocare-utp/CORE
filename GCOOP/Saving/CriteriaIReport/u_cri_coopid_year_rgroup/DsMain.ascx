<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.CriteriaIReport.u_cri_coopid_year_rgroup.DsMain" %>
<link id="css1" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="iReportDataSourceFormView" >
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
                <td >
                    
                </td>
            </tr>
            <tr>
                <td width="10%">
                    <div>
                        <span>รหัสสังกัด:</span>
                    </div>
                </td>
                <td >
                    <div>
                        <asp:TextBox ID="membgroup_s_code" runat="server" disabled="false">
                        </asp:TextBox>
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
                    <div>
                        <asp:TextBox ID="membgroup_e_code" runat="server" disabled="false">
                        </asp:TextBox>
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
