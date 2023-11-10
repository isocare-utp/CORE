<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_divsrv_opr_slip.aspx.cs" Inherits="Saving.Applications.divavg.w_sheet_divsrv_opr_slip" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=postInitMemberNo%>
    <%=postNewClear%>
    <%=postInit%>
    <%=postRefresh%>
    <%=postSlipDate%>
    <%=postDeldetail%>
    <script type="text/JavaScript">
        //Function Main
        //=================================================================
        function GetMemberNoFromDialog(member_no) {
            Gcoop.GetEl("Hdmember_no").value = member_no;
            postInitMemberNo();
        }

        function OnDwMainButtonClick(s, r, b) {
            if (b == "b_search_memno") {
                Gcoop.OpenIFrame("800", "500", "w_dlg_divsrv_search_mem.aspx", "");
            }

        }

        function OnDwMainItemChange(s, r, c, v) {
            if (c == "member_no") {
                objDw_main.SetItem(1, "member_no", v)
                objDw_main.AcceptText();
                postInit();
            }
            else if (c == "slip_tdate") {
                objDw_main.SetItem(r, c, v);
                objDw_main.AcceptText();
                postSlipDate();
            }
        }
        //Function Detail
        //=================================================================
        function OnDwDetailButtonClick(s, r, b) {
            if (b == "b_del") {
                Gcoop.GetEl("Hdrow").value = r;
                postDeldetail();
            }
        }
        //Function Default
        //=================================================================
        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }
        function MenubarOpen() {

        }
        function MenubarNew() {
            postNewClear();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <p>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <br />
        <table style="width: 100%;">
            <tr>
                <td colspan="2">
                    <dw:WebDataWindowControl ID="Dw_main" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                        AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" ClientScriptable="True"
                        DataWindowObject="d_divsrv_opr_main" LibraryList="~/DataWindow/divavg/divsrv_opr_slip.pbl"
                        ClientEventItemChanged="OnDwMainItemChange" ClientEventButtonClicked="OnDwMainButtonClick">
                    </dw:WebDataWindowControl>
                </td>
            </tr>
            <tr>
                <td valign="top" colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td valign="top" colspan="2">
                    <asp:Panel ID="Panel1" runat="server" Height="200px" ScrollBars="Auto" Width="730px">
                        <dw:WebDataWindowControl ID="Dw_detail" runat="server" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                            ClientScriptable="True" DataWindowObject="d_divsrv_opr_detail" LibraryList="~/DataWindow/divavg/divsrv_opr_slip.pbl"
                            ClientEventButtonClicked="OnDwDetailButtonClick">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;<asp:HiddenField ID="Hdmember_no" runat="server" />
                    <asp:HiddenField ID="Hddiv_year" runat="server" />
                    <asp:HiddenField ID="HdLastDate" runat="server" />
                    <asp:HiddenField ID="HdActiveDate" runat="server" />
                    <asp:HiddenField ID="Hdrow" runat="server" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </p>
    <%=outputProcess%>
</asp:Content>
