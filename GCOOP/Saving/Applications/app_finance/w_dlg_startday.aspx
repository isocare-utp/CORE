<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_dlg_startday.aspx.cs"
    Inherits="Saving.Applications.app_finance.w_dlg_startday" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>

    <script type="text/javascript">
        function Validate() {
            return confirm("ยืนยันการเปิดงานประจำวัน");
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table width="100%">
        <tr align="center">
            <td align="center">
                <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_start_day"
                    LibraryList="~/DataWindow/App_finance/start_day.pbl" ClientScriptable="True"
                    ClientEventClicked="selectRow" AutoRestoreContext="False" AutoRestoreDataCache="True">
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
</asp:Content>
