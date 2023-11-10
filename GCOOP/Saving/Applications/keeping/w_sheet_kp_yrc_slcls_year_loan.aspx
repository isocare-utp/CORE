<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_kp_yrc_slcls_year_loan.aspx.cs" Inherits="Saving.Applications.keeping.w_sheet_kp_yrc_slcls_year_loan" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
    <%=postNewClear%>
    <%=postRefresh%>
    <%=postProcClsYear%>
    <script type="text/JavaScript">
        //Function Main
        function OnDwListClick(s, r, c) {
            if (c == "clear_type") {
                Gcoop.CheckDw(s, r, c, "clear_type", 1, 0);
            }
        }
        function OnDwOptionClick(s, r, c) {
            if (c == "shrmas_status") {
                Gcoop.CheckDw(s, r, c, "shrmas_status", 1, 0);
            }
            else if (c == "shrstm_status") {
                Gcoop.CheckDw(s, r, c, "shrstm_status", 1, 0);
            }
        }


        function OnDwOptionButtonClick(s, r, b) {
            if (b == "b_process") {
                B_ProcessClick();
            }
        }

        function B_ProcessClick() {
            var isconfirm = confirm("ต้องการประมวลผลปิดสิ้นปี ใช่หรือไม่ ?");
            if (!isconfirm) {
                return false;
            }
            postProcClsYear();
        }


        //Function Default
        //=============================================================
        function Validate() {

        }



        function MenubarNew() {
            postNewClear();
        }

        function SheetLoadComplete() {
            if (Gcoop.GetEl("Hd_process").value == "true") {
                Gcoop.OpenProgressBar("ประมวลผลปิดสิ้นปี", true, true, ProcClsYearComplete);
            }
        }

        function ProcClsYearComplete() {
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
                <td colspan="3">
                    <dw:WebDataWindowControl ID="Dw_option" runat="server" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                        ClientScriptable="True" DataWindowObject="d_slclssrv_prc_year_lon_option" LibraryList="~/DataWindow/keeping/kp_yrc_slcls_year_loan.pbl"
                        ClientEventButtonClicked="OnDwOptionButtonClick" Style="top: 0px; left: 0px"
                        ClientEventClicked="OnDwOptionClick">
                    </dw:WebDataWindowControl>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:HiddenField ID="Hd_process" runat="server" />
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Panel ID="Panel1" runat="server" Height="200px" ScrollBars="Auto">
                        <dw:WebDataWindowControl ID="Dw_list" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                            AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" ClientScriptable="True"
                            DataWindowObject="d_slclssrv_prc_year_lon_docctrl_list" LibraryList="~/DataWindow/keeping/kp_yrc_slcls_year_loan.pbl"
                            Style="top: 0px; left: 0px" ClientEventClicked="OnDwListClick">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </p>
</asp:Content>
