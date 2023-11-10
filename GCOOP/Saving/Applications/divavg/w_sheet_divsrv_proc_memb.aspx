<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_divsrv_proc_memb.aspx.cs" Inherits="Saving.Applications.divavg.w_sheet_divsrv_proc_memb" %>
<%@ Register assembly="WebDataWindow" namespace="Sybase.DataWindow.Web" tagprefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=postNewClear%>
 <%=postRefresh%>
 <%=postProcMember%>
 <%=postSetAccDate%>
    <script type="text/JavaScript">
        //Function Main
        function OnDwOptionClick(s, r, c) {
            if (c == "methold_flag") {
                Gcoop.CheckDw(s, r, c, "methold_flag", 1, 0);
            }
        }
        function OnDwOptionButtonClick(s, r, b) {
            if (b == "b_process") {
                B_ProcessClick();
            }
        }

        function B_ProcessClick() {
            var isconfirm = confirm("ต้องการประมวลสมาชิกที่ได้รับปันผล เฉลี่ยคืน ใช่หรือไม่ ?");
            if (!isconfirm) {
                return false;
            }
            postProcMember();
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



        //Function Default
        //=============================================================
        function Validate() {

        }

        function MenubarOpen() {

        }

        function MenubarNew() {
            postNewClear();
        }

        function SheetLoadComplete() {
            if (Gcoop.GetEl("Hd_process").value == "true") {
                Gcoop.OpenProgressBar("ประมวลผลสมาชิกที่ได้รับปันผล - เฉลี่ยคืน", true, true, ProcMembComplete);
            }
        }

        function ProcMembComplete() {
            postNewClear();
        }   

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <p>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <br />
        <table style="width:100%;">
            <tr>
                <td colspan="3">
                    <dw:WebDataWindowControl ID="Dw_option" runat="server" 
                        AutoRestoreContext="False" AutoRestoreDataCache="True" 
                        AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" 
                        ClientScriptable="True" DataWindowObject="d_divsrv_prc_memb_option" 
                        LibraryList="~/DataWindow/divavg/divsrv_proc_memb.pbl" 
                        ClientEventItemChanged="OnDwOptionItemChange" 
                        ClientEventButtonClicked="OnDwOptionButtonClick">
                    </dw:WebDataWindowControl>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:HiddenField ID="Hd_process" runat="server" /> &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </p>
    <%=outputProcess%>
</asp:Content>

