<%@ Page Title="" Language="C#" MasterPageFile="~/Report.Master" AutoEventWireup="true" CodeBehind="npl_contcoll.aspx.cs" Inherits="Saving.CriteriaIReport.u_cri_npl_contcoll.npl_contcoll" %>
<%@ Register src="DsList.ascx" tagname="DsList" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <uc1:DsList ID="dsList" runat="server" />
</asp:Content>
