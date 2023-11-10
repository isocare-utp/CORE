<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_divsrv_proc_estdivavg.aspx.cs" Inherits="Saving.Applications.divavg.w_sheet_divsrv_proc_estdivavg" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
    <%=postNewClear%>
    <%=postRefresh%>
    <%=postPrcEstdivavg%>
    <%=postSetAccDate%>
    <%=postShowReport%>
    <%=popupReport%>
    <%=postSetAvgpercent%>
    <script type="text/JavaScript">
        //Function Main
        //=================================================================
        function OnDwLoanClick(s, r, c) {
            if (c == "calavg_flag") {
                Gcoop.GetEl("Hd_row").value = r + "";
                Gcoop.CheckDw(s, r, c, "calavg_flag", 1, 0);
                postSetAvgpercent();
            }
        }

        function B_ProcessClick() {
            var isconfirm = confirm("ต้องการประมาณปันผล - เฉลี่ยคืน ใช่หรือไม่ ?");
            if (!isconfirm) {
                return false;
            }
            postPrcEstdivavg();
        }

        function OnDwOptionItemChange(s, r, c, v) {
            if (c == "proc_type") {
                objDw_option.SetItem(1, "proc_type", v);
                objDw_option.AcceptText();
                var proc_type = objDw_option.GetItem(1, "proc_type");
                if (proc_type == 1) {
                    objDw_option.SetItem(1, "proc_text", "");
                    objDw_option.AcceptText();
                }
                postRefresh();
            }
            else if (c == "div_year") {
                objDw_option.SetItem(1, "div_year", v);
                objDw_option.AcceptText();
                postSetAccDate();
            }
        }

        function OnDwOptionClick(s, r, c) {
            if (c == "div_flag") {
                Gcoop.CheckDw(s, r, c, "div_flag", 1, 0);
                var div_flag = objDw_option.GetItem(1, "div_flag");
                if (div_flag == 0) {
                    objDw_option.SetItem(1, "sdiv_rate", 0);
                    objDw_option.SetItem(1, "ediv_rate", 0);
                    objDw_option.SetItem(1, "divstep_rate", 0);
                    objDw_option.AcceptText();
                }
                postRefresh();
            }
            else if (c == "avg_flag") {
                Gcoop.CheckDw(s, r, c, "avg_flag", 1, 0);
                var avg_flag = objDw_option.GetItem(1, "avg_flag");
                if (avg_flag == 0) {
                    objDw_option.SetItem(1, "savg_rate", 0);
                    objDw_option.SetItem(1, "eavg_rate", 0);
                    objDw_option.SetItem(1, "avgstep_rate", 0);
                    objDw_option.AcceptText();
                }
                postRefresh();
            }
        }

        //Function Default
        //=================================================================
        function Validate() {

        }

        function MenubarOpen() {

        }

        function MenubarNew() {
            postNewClear();
        }

        function SheetLoadComplete() {
            if (Gcoop.GetEl("Hd_process").value == "true") {
                Gcoop.OpenProgressBar("ประมาณผลปันผล - เฉลี่ยคืน", true, true, ProcEstdivavgComplete);
            }
            else if (Gcoop.GetEl("HdOpenIFrame").value == "True") {
                Gcoop.OpenIFrame("220", "200", "../../../Criteria/dlg/w_dlg_report_progress.aspx?&app=<%=app%>&gid=<%=gid%>&rid=<%=rid%>&pdf=<%=pdf%>", "");
            }
        }

        function ProcEstdivavgComplete() {
            postShowReport();
        }   

    </script>
    <style type="text/css">
        .style1
        {
            width: 98px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <p>
        <asp:HiddenField ID="HDCoop_ID" runat="server" />
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <br />
        <table style="width: 100%;">
            <tr>
                <td colspan="5">
                    <asp:Panel ID="Panel1" runat="server">
                        <dw:WebDataWindowControl ID="Dw_option" runat="server" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                            ClientScriptable="True" DataWindowObject="d_divsrv_prc_estdivavg_option" LibraryList="~/DataWindow/divavg/divsrv_proc_estdivavg.pbl"
                            ClientEventClicked="OnDwOptionClick" ClientEventItemChanged="OnDwOptionItemChange">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:Panel ID="Panel2" runat="server" Height="300px" ScrollBars="Auto" Width="500px">
                        <dw:WebDataWindowControl ID="Dw_loan" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                            AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" ClientScriptable="True"
                            DataWindowObject="d_divsrv_prc_divavg_lntype_option" LibraryList="~/DataWindow/divavg/divsrv_proc_estdivavg.pbl"
                            ClientEventClicked="OnDwLoanClick">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td colspan="3">
                    <asp:Panel ID="Panel3" runat="server" Width="300px">
                        <table style="width: 100%;">
                            <tr>
                                <td class="style1">
                                    <asp:Button ID="Button1" runat="server" Text="Button" UseSubmitBehavior="False" Visible="False" />
                                </td>
                                <td>
                                    &nbsp;<asp:Button ID="B_next" runat="server" Text="ต่อไป &gt;" UseSubmitBehavior="False"
                                        OnClick="B_next_Click" />
                                </td>
                                <td>
                                    &nbsp;<asp:Button ID="B_cancel" runat="server" Text="ยกเลิก" UseSubmitBehavior="False"
                                        OnClick="B_cancel_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td colspan="3">
                    <asp:Panel ID="Panel4" runat="server" Width="300px">
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <asp:Button ID="B_previous" runat="server" Text="&lt; ย้อนกลับ" UseSubmitBehavior="False"
                                        OnClick="B_previous_Click" />
                                </td>
                                <td>
                                    <input id="B_process" type="button" value="ประมวลผล" onclick="B_ProcessClick()" />
                                </td>
                                <td>
                                    <asp:Button ID="B_close" runat="server" Text="ยกเลิก" UseSubmitBehavior="False" OnClick="B_close_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:Panel ID="Panel5" runat="server">
                        <dw:WebDataWindowControl ID="Dw_report" runat="server" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                            ClientScriptable="True" DataWindowObject="d_divsrv_prc_estdivavg_rate" LibraryList="~/DataWindow/divavg/divsrv_proc_estdivavg.pbl">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td rowspan="2">
                    &nbsp;
                </td>
                <td>
                    <asp:Button ID="B_report" runat="server" OnClick="B_report_Click" Text="ดึงข้อมูลรายงาน"
                        UseSubmitBehavior="False" />
                </td>
                <td>
                    <asp:Button ID="B_Print" runat="server" OnClick="B_Print_Click" Text="พิมพ์รายงาน"
                        UseSubmitBehavior="False" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td rowspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:HiddenField ID="Hd_process" runat="server" />
                    <asp:HiddenField ID="Hd_row" runat="server" />
                    <asp:HiddenField ID="HdOpenIFrame" runat="server" />
                </td>
            </tr>
        </table>
    </p>
    <%=outputProcess%>
</asp:Content>
