<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_ad_premissionreport.aspx.cs" Inherits="Saving.Applications.admin.w_sheet_ad_premissionreport" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>

                <dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_agentsrv_mem_main"
                    LibraryList="~/DataWindow/agency/agent.pbl" AutoRestoreContext="False" AutoRestoreDataCache="True"
                    AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" Width="720px" ClientEventItemChanged="MainChanged"
                    TabIndex="1" ClientFormatting="True" ClientEventButtonClicked="Click_memsearch">
                </dw:WebDataWindowControl>
</asp:Content>
