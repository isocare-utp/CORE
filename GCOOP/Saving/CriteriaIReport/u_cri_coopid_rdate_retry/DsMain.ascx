<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.CriteriaIReport.u_cri_coopid_rdate_retry.DsMain" %>
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
                <td>
                    <asp:DropDownList ID="coop_id" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="30%">
                    <div>
                        <span>เกษียณ:</span>
                    </div>
                </td>
                <td>
                     <asp:TextBox ID="as_year" runat="server"  ReadOnly="true" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>เกิดตั้งแต่:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="adtm_sdate" runat="server"  ReadOnly="true" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ถึงวันที่:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="adtm_edate" runat="server"  ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
