<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_dlg_closeday.aspx.cs"
    Inherits="Saving.Applications.app_finance.w_dlg_closeday" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>

    <script type="text/javascript">

        function Validate() {
            return confirm("ยืนยันการปิดงานประจำวัน");
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table width="100%">
        <tr>
            <td align="center">
                <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_close_day"
                    LibraryList="~/DataWindow/App_finance/closeday.pbl" ClientScriptable="True" ClientEventClicked="selectRow"
                    AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True">
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
            <td align="center">
                <dw:WebDataWindowControl ID="DwChqlist" runat="server" DataWindowObject="d_waitchq_list"
                    LibraryList="~/DataWindow/App_finance/closeday.pbl" ClientScriptable="True" ClientEventClicked="selectRow"
                    AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True">
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
</asp:Content>
