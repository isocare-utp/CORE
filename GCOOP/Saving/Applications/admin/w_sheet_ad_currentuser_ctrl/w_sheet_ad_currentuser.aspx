<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_ad_currentuser.aspx.cs" Inherits="Saving.Applications.admin.w_sheet_ad_currentuser_ctrl.w_sheet_ad_currentuser" %>
<%@ Register src="DsList.ascx" tagname="DsList" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">


    <%--<input id="printBtn" type="button" value="พิมพ์หน้าจอ" onclick="printPage();"   />--%>

    <uc1:DsList ID="dsList" runat="server" />
</asp:Content>
