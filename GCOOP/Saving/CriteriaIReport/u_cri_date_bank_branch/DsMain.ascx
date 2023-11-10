<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.CriteriaIReport.u_cri_date_bank_branch.DsMain" %>
<link id="css1" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="iReportDataSourceFormView">
            <tr>
                <td width="30%">
                    <div>
                        <span>วันที่:</span>
                    </div>
                </td width="35%" colspan="2">
                <td>
                    <asp:TextBox ID="recieve_date" runat="server" Style="width: 130px;"></asp:TextBox>
                </td>

            </tr>
            <tr>    
                 <td width="30%">
                    <div>
                        <span>ธนาคาร :</span>
                    </div>
                </td width="35%" colspan="2">
                <td>
                    <asp:DropDownList ID="bank_code" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                 <td width="30%">
                    <div>
                        <span>สาขา :</span>
                    </div>
                </td>
                <td width="35%" colspan="2">
                    <asp:DropDownList ID="bank_branch" runat="server"></asp:DropDownList>
                </td>
            </tr>
        </table>
     
    </EditItemTemplate>
</asp:FormView>
