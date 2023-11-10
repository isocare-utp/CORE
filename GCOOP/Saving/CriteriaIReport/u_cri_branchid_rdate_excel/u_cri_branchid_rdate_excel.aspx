<%@ Page Title="" Language="C#" MasterPageFile="~/Report.Master" AutoEventWireup="true"
    CodeBehind="u_cri_branchid_rdate_excel.aspx.cs" Inherits="Saving.CriteriaIReport.u_cri_branchid_rdate_excel.u_cri_branchid_rdate_excel" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlace" runat="server">
    <span  id="ReportName" class="txtReportName" style="font-weight: bold;
        font-size: medium;"></span>
    <uc1:DsMain ID="dsMain" runat="server" />
</asp:Content>
