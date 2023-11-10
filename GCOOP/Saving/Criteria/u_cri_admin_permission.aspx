<%@ Page Title="" Language="C#" MasterPageFile="~/Report.Master" AutoEventWireup="true" CodeBehind="u_cri_admin_permission.aspx.cs" Inherits="Saving.Criteria.u_cri_admin_permission" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>

                <dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="ad_report_promission"
                    LibraryList="~/DataWindow/admin/ad_report.pbl" AutoRestoreContext="False" AutoRestoreDataCache="True"
                    AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" Width="720px" ClientEventItemChanged="MainChanged"
                    TabIndex="1" ClientFormatting="True" ClientEventButtonClicked="Click_memsearch">
                </dw:WebDataWindowControl>
</asp:Content>
