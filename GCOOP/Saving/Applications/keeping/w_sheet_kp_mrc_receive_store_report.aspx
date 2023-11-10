<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_kp_mrc_receive_store_report.aspx.cs"
    Inherits="Saving.Applications.keeping.w_sheet_kp_mrc_receive_store_report" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
    <%=postProcStatus%>
    <%=getXml%>
    <%=getAccid %>
    <%=chgProcDate%>
    <style type="text/css">
        .style3
        {
            width: 198px;
        }
        .style4
        {
            width: 47px;
        }
    </style>
    <script type="text/javascript">
        function OnButtonClick(s, r, c) {
            if (c == "b_process") {
                if (confirm("คุณต้องการประมวลผลรายงานเรียกเก็บใช่หรือไม่ ?")) {
                    getXml();
                }
            }
        }

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล?");
        }
        function ItemChanged(s, r, c, v) {
            //tofrom_accid
            if (c == "proc_status" || c == "recpno_status") {
                objDwMain.SetItem(r, c, v);
                objDwMain.AcceptText();
                postProcStatus();
            }
            else if (c == "moneytype_code") {
                objDwMain.SetItem(r, c, v);
                objDwMain.AcceptText();
                getAccid();
            }
            else if ((c == "receive_month") || (c == "receive_year")) {
                objDwMain.SetItem(r, c, v);
                objDwMain.AcceptText();
                chgProcDate();
            }
        }

        function OnEventClick(s, r, c) {
            //deposit_status
            if (c == "ffee_status") {
                Gcoop.CheckDw(s, r, c, "ffee_status", 1, 0);
            } else if (c == "share_status") {
                Gcoop.CheckDw(s, r, c, "share_status", 1, 0);
            } else if (c == "loan_status") {
                Gcoop.CheckDw(s, r, c, "loan_status", 1, 0);
            } else if (c == "deposit_status") {
                Gcoop.CheckDw(s, r, c, "deposit_status", 1, 0);
            } else if (c == "recpno_status") {
                Gcoop.CheckDw(s, r, c, "recpno_status", 1, 0);

            }
            else if (c == "moneyret_status") {
                Gcoop.CheckDw(s, r, c, "moneyret_status", 1, 0);

            }
            else if (c == "other_status") {
                Gcoop.CheckDw(s, r, c, "other_status", 1, 0);
            }

            else if (c == "emertrn_status") {
                Gcoop.CheckDw(s, r, c, "emertrn_status", 1, 0);
            }
        }

        function SheetLoadComplete() {
            //if (Gcoop.GetEl("HdRunProcess").value == "true") {
                //Gcoop.OpenProgressBar("ประมวลผลเรียกเก็บ", true, true, endProc);
            //}
        }

        function endProc() {
            RemoveIFrame();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table style="width: 100%;">
        <tr>
            <td colspan="3">
                <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_kp_option_proc_report"
                    LibraryList="~/DataWindow/keeping/kp_mrc_receive_store_report.pbl" AutoSaveDataCacheAfterRetrieve="True"
                    AutoRestoreDataCache="True" AutoRestoreContext="False" ClientScriptable="True"
                    ClientEventItemChanged="ItemChanged" ClientEventClicked="OnEventClick" ClientFormatting="True"
                    ClientEventButtonClicked="OnButtonClick">
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
            <td align="center">
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdRunProcess" runat="server" />
    <%=outputProcess%>
</asp:Content>
