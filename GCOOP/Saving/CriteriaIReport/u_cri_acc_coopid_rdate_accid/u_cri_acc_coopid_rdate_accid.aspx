﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Report.Master" AutoEventWireup="true"
    CodeBehind="u_cri_acc_coopid_rdate_accid.aspx.cs" Inherits="Saving.CriteriaIReport.u_cri_acc_coopid_rdate_accid.u_cri_acc_coopid_rdate_accid" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <span type="text" id="ReportName" class="txtReportName" style="font-weight: bold;
        font-size: medium;"></span>
    <uc1:DsMain ID="dsMain" runat="server" />
</asp:Content>
