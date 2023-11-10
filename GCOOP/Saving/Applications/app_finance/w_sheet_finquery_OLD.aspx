<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_finquery_OLD.aspx.cs" Inherits="Saving.Applications.app_finance.w_sheet_finquery_OLD" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <script type="text/javascript">

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
        <dw:WebDataWindowControl ID="DwMain" runat="server" AutoRestoreContext="False"
            AutoRestoreDataCache="True" ClientScriptable="True" DataWindowObject="d_fn_showuserfdet"
            Height="80px" LibraryList="~/DataWindow/app_finance/finquery.pbl" AutoSaveDataCacheAfterRetrieve="True"
            ClientEventClicked="DwUserLIstClick">
        </dw:WebDataWindowControl>
</asp:Content>
