<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_kp_yrc_confirmbalance.aspx.cs" Inherits="Saving.Applications.keeping.w_sheet_kp_yrc_confirmbalance" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
    <%=postRefresh%>
    <%=getXml%>
    <%=postNewClear%>
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
        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล?");
        }

        function ItemChanged(s, r, c, v) {
            if (c == "random_type") {
                objDwMain.SetItem(r, c, v);
                objDwMain.AcceptText();
                postRefresh();
            }
            else if (c == "proc_type") {
                objDwMain.SetItem(r, c, v);
                objDwMain.AcceptText();
                postRefresh();
            }
            else if (c == "balance_tdate") {
                objDwMain.SetItem(1, "balance_tdate", v);
                objDwMain.AcceptText();
                objDwMain.SetItem(1, "balance_date", Gcoop.ToEngDate(v));
                objDwMain.AcceptText();
            }
            else if (c == "proc_tdate") {
                objDwMain.SetItem(1, "proc_tdate", v);
                objDwMain.AcceptText();
                objDwMain.SetItem(1, "proc_date", Gcoop.ToEngDate(v));
                objDwMain.AcceptText();
            }
            else if (c == "proc_text") {
                objDwMain.SetItem(r, "proc_text", v);
                objDwMain.AcceptText();
            }
            else if (c == "random_percent") {
                objDwMain.SetItem(r, "random_percent", v);
                objDwMain.AcceptText();
            }
        }

        function OnEventClick(s, r, c) {
            if (c == "prc_ran_flag") {
                Gcoop.CheckDw(s, r, c, "prc_ran_flag", 1, 0);
                postRefresh();
            } 
            else if (c == "prc_mem_flag") {
                Gcoop.CheckDw(s, r, c, "prc_mem_flag", 1, 0);
            } 
            else if (c == "prc_shr_flag") {
                Gcoop.CheckDw(s, r, c, "prc_shr_flag", 1, 0);
            } 
            else if (c == "prc_dep_flag") {
                Gcoop.CheckDw(s, r, c, "prc_dep_flag", 1, 0);
            } 
            else if (c == "prc_lon_flag") {
                Gcoop.CheckDw(s, r, c, "prc_lon_flag", 1, 0);
            }
            else if (c == "prc_coll_flag") {
                Gcoop.CheckDw(s, r, c, "prc_coll_flag", 1, 0);
            }
        }

        function SheetLoadComplete() {
            if (Gcoop.GetEl("HdRunProcess").value == "true") {
                Gcoop.OpenProgressBar("ประมวลผลหนังสือยืนยันยอด", true, true, endProc);
            }
        }

        function endProc() {
            RemoveIFrame();
            postNewClear();
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table style="width: 100%;">
        <tr>
            <td colspan="3">
                <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_yrcfbal_prc_option"
                    LibraryList="~/DataWindow/keeping/kp_yrc_confirmbalance.pbl" AutoSaveDataCacheAfterRetrieve="True"
                    AutoRestoreDataCache="True" AutoRestoreContext="False" ClientScriptable="True"
                    ClientEventItemChanged="ItemChanged" ClientEventClicked="OnEventClick" 
                    ClientFormatting="True">
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr><td align="center"> <asp:Button ID="cb_process" runat="server" Text="ประมวล" 
                Height="60px" Width="105px" OnClick="cb_process_Click"  
                style="width:105px; height:60px;" Font-Names="Tahoma" Font-Size="Large" 
                ForeColor="#009933" UseSubmitBehavior="False"/></td></tr>
    </table>
   
    <asp:HiddenField ID="HdRunProcess" runat="server" />
        <%=outputProcess%>
</asp:Content>

