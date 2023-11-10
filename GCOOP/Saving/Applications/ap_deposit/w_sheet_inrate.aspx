<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_inrate.aspx.cs" Inherits="Saving.Applications.ap_deposit.w_sheet_inrate" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=PostRetrive%>
    <script type="text/javascript">
        function DwmainChang(s, row, c, v) {
            s.SetItem(row, c, v);
            s.AcceptText();
            if (c == "depttype_select") {
                PostRetrive();
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <table style="width: 100%;">
        <tr>
            <td>
                <asp:Panel ID="Panel1" runat="server">
                    <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_dp_inrate_head"
                        LibraryList="~/DataWindow/ap_deposit/dp_deptintrate.pbl" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                        ClientScriptable="True" ClientEventClicked="OnDw_listClick" ClientEventItemChanged="DwmainChang">
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="Panel2" runat="server" Width="750" Height="500" ScrollBars="Auto">
                    <dw:WebDataWindowControl ID="DwDetail" runat="server" DataWindowObject="d_dp_dept_intrate"
                        LibraryList="~/DataWindow/ap_deposit/dp_deptintrate.pbl" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                        ClientScriptable="True" ClientEventClicked="DwDetailClick" ClientEventItemChanged="DwDetailChang">
                    </dw:WebDataWindowControl>
                </asp:Panel>
                <asp:HiddenField ID="Hd_type" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
