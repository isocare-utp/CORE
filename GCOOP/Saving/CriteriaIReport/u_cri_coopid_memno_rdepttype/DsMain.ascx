<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.CriteriaIReport.u_cri_coopid_memno_rdepttype.DsMain" %>
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
                        <span>ทะเบียนสมาชิก:</span></div>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="memno" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td>
                    <div>
                        <span>ตั้งแต่ประเภท:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="sdepttype" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
             <tr>
                <td>
                    <div>
                        <span>ถึงประเภท:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="edepttype" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
