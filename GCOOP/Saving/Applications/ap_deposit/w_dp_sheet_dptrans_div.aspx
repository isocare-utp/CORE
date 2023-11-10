<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_dp_sheet_dptrans_div.aspx.cs" Inherits="Saving.Applications.ap_deposit.w_dp_sheet_dptrans_div" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=PostRetrive%>
    <%=PostRetriveTrans%>
    <%=PostSearchmemno%>
    <script type="text/javascript">
        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function DwmainChang(s, row, c, v) {
            s.SetItem(row, c, v);
            s.AcceptText();
            if (c == "year") {
                PostRetrive();
            }
        }

        function DwDetailClick(s, r, c) {
            if (c == "deptaccount_no") {
                var deptaccount_no = objDwDetail.GetItem(r, "deptaccount_no");
                Gcoop.GetEl("Hd_deptaccountno").value = deptaccount_no;
                PostRetriveTrans();
            }
        }
        function OnDwHeadClick(s, row, c, v) {
            s.SetItem(row, c, v);
            s.AcceptText();
            if (c == "b_1") {
                PostSearchmemno();
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <table style="width: 100%;">
        <tr>
            <td>
                <asp:Panel ID="Panel1" runat="server">
                    <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_dp_depttrans_div_head"
                        LibraryList="~/DataWindow/ap_deposit/dp_depttrans.pbl" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                        ClientScriptable="True" ClientEventClicked="OnDwmainClick" ClientEventItemChanged="DwmainChang">
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
            <td>
                <asp:Panel ID="Panel4" runat="server">
                    <dw:WebDataWindowControl ID="DwHead" runat="server" DataWindowObject="d_dp_depttrans_headmem"
                        LibraryList="~/DataWindow/ap_deposit/dp_depttrans.pbl" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                        ClientScriptable="True" ClientEventClicked="OnDwHeadClick" ClientEventItemChanged="DwHeadChang">
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <asp:Panel ID="Panel2" runat="server" Width="250" Height="500" ScrollBars="Auto">
                    <dw:WebDataWindowControl ID="DwDetail" runat="server" DataWindowObject="d_dp_depttrans_div"
                        LibraryList="~/DataWindow/ap_deposit/dp_depttrans.pbl" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                        ClientScriptable="True" ClientEventClicked="DwDetailClick" ClientEventItemChanged="DwDetailChang">
                    </dw:WebDataWindowControl>
                </asp:Panel>
                <asp:HiddenField ID="Hd_type" runat="server" />
            </td>
            <td>
                <%--<asp:Panel ID="Panel3" runat="server" Width="250" Height="500" ScrollBars="Auto">--%>
                <dw:WebDataWindowControl ID="DwList" runat="server" DataWindowObject="d_dp_depttrans_div_1"
                    LibraryList="~/DataWindow/ap_deposit/dp_depttrans.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                    ClientScriptable="True" ClientEventClicked="DwListClick" ClientEventItemChanged="DwListChang">
                </dw:WebDataWindowControl>
                <%--</asp:Panel>--%>
                <asp:HiddenField ID="Hd_deptaccountno" runat="server" />
                <asp:HiddenField ID="Hd_row" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
