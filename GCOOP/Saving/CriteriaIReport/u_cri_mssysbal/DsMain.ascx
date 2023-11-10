<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.CriteriaIReport.u_cri_mssysbal.DsMain" %>
<link id="css1" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="iReportDataSourceFormView">
            <tr>
            <td width="20%">
            </td>
                <td>
                    <div>
                        <span style="width: 90px;">วันที่:</span>
                        <asp:TextBox ID="work_date" runat="server" Style="width: 170px;"></asp:TextBox>
                    </div>
                </td>
            </tr>
           
        </table>
      
    </EditItemTemplate>
</asp:FormView>
