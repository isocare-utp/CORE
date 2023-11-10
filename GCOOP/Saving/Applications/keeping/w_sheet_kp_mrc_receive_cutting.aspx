<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_kp_mrc_receive_cutting.aspx.cs"
    Inherits="Saving.Applications.keeping.w_sheet_kp_mrc_receive_cutting" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
    <%=getXml%>    
    <%=postProcType%>    
    <%=postProcType%>
    <%=postInitReport %>
    <script type="text/javascript">
        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล?");
        }
        function ItemChanged(s, r, c, v) {
            if (c == "proc_status" || c == "recpno_status") {
                objDwMain.SetItem(r, c, v);
                objDwMain.AcceptText();
                postProcStatus();
            } else if (c == "receipt_tdate") {
                objDwMain.SetItem(1, "receipt_tdate", v);
                objDwMain.AcceptText();
                objDwMain.SetItem(1, "receipt_date", Gcoop.ToEngDate(v));
                objDwMain.AcceptText();
            } else if (c == "calint_tdate") {
                objDwMain.SetItem(1, "calint_tdate", v);
                objDwMain.AcceptText();
                objDwMain.SetItem(1, "calint_date", Gcoop.ToEngDate(v));
                objDwMain.AcceptText();
            }
            else if (c == "proc_type") {
                objDwMain.SetItem(1, "proc_type", v);
                objDwMain.AcceptText();
                postProcType();
            }
        }
        function OnButtonClick(s, r, c) {
            if (c == "b_process") {
                if (confirm("คุณต้องการประมวลผลตัดยอดใช่หรือไม่ ?")) {
                    getXml();
                }
            }
            else if (c == "b_init_rpt") {
                postInitReport();
            }
        }
        function OpenProgressDlg() {
            Gcoop.OpenDlg(250, 180, "w_dlg_RunPostProcess_Progress.aspx", "");
        }
        function OnEventClick(s, r, c) {
            if (c == "ffee_status") {
                Gcoop.CheckDw(s, r, c, "ffee_status", 1, 0);
            }
            else if (c == "share_status") {
                Gcoop.CheckDw(s, r, c, "share_status", 1, 0);
            }
            else if (c == "loan_status") {
                Gcoop.CheckDw(s, r, c, "loan_status", 1, 0);
            }
            else if (c == "deposit_status") {
                Gcoop.CheckDw(s, r, c, "deposit_status", 1, 0);
            }
            else if (c == "other_status") {
                Gcoop.CheckDw(s, r, c, "other_status", 1, 0);

            }
            else if (c == "trntodeptmthcut_status") {
                Gcoop.CheckDw(s, r, c, "trntodeptmthcut_status", 1, 0);

            }
            else if (c == "moneyret_status") {
                Gcoop.CheckDw(s, r, c, "moneyret_status", 1, 0);

            }
            else if (c == "emertrn_status") {
                Gcoop.CheckDw(s, r, c, "emertrn_status", 1, 0);

            }
        }
        function SheetLoadComplete() {
            if (Gcoop.GetEl("HdRunProcess").value == "true") {
                Gcoop.OpenProgressBar("ประมวลผลตัดยอด", true, true, endProc);
            }
            else if (Gcoop.GetEl("HdOpenIFrame").value == "True") {
                Gcoop.OpenIFrame("220", "200", "../../../Criteria/dlg/w_dlg_report_progress.aspx?&app=<%=app%>&gid=<%=gid%>&rid=<%=rid%>&pdf=<%=pdf%>", "");
            }
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
            <td>
                <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_kp_option_post"
                    LibraryList="~/DataWindow/keeping/kp_mrc_receive_cutting.pbl" AutoSaveDataCacheAfterRetrieve="True"
                    AutoRestoreDataCache="True" AutoRestoreContext="False" ClientScriptable="True"
                    ClientEventItemChanged="ItemChanged" ClientEventClicked="OnEventClick" ClientEventButtonClicked="OnButtonClick"
                    ClientFormatting="True" style="top: 1px; left: 0px">
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr><td align="center"> 
                <dw:WebDataWindowControl ID="Dw_Report" runat="server" DataWindowObject="d_kp_rpt_bfrcv_post"
                    LibraryList="~/DataWindow/keeping/kp_mrc_receive_cutting.pbl" AutoSaveDataCacheAfterRetrieve="True"
                    AutoRestoreDataCache="True" AutoRestoreContext="False" ClientScriptable="True"
                    ClientFormatting="True" style="top: 0px; left: 0px">
                </dw:WebDataWindowControl>
            </td></tr>
             <tr><td align="center" style="text-align: left"> 
                
                 <asp:Button ID="B_Print" runat="server" onclick="B_Print_Click" Text="Print" 
                     UseSubmitBehavior="False" />
                
            </td></tr>
    </table>
     <asp:HiddenField ID="HdRunProcess" runat="server" />
     <asp:HiddenField ID="HdOpenIFrame" runat="server" />
     <%=outputProcess%>
    </asp:Content>
