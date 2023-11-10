<%@ Page Title="" Language="C#" MasterPageFile="~/Report.Master" AutoEventWireup="true" CodeBehind="report_mem_loan_maximum.aspx.cs" Inherits="Saving.CriteriaIReport.u_cri_mis.report_mem_loan_maximum" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
        type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <center>
        <asp:Label ID="ReportName" runat="server" Text="ชื่อรายงาน" Enabled="False" EnableTheming="False"
            Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Size="Large"
            Font-Underline="False"></asp:Label></center>
    <br />
    <table class="iReportDataSourceFormView">
        <tr>
            <td style="width: 45%">
                <div>
                    <span>จำนวนลำดับที่ต้องการดูข้อมูล :</span></div>
            </td>
            <td>
                <asp:TextBox ID="doc_no" runat="server" Width="120px" MaxLength="10"></asp:TextBox>
            </td>
        </tr>
    </table>
</asp:Content>

