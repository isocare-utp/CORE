<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_kp_slcls_year_shr.aspx.cs" Inherits="Saving.Applications.keeping.w_sheet_kp_slcls_year_shr" %>
<%@ Register assembly="WebDataWindow" namespace="Sybase.DataWindow.Web" tagprefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <%=postNewClear%>
 <%=postRefresh%>
 <%=postProcClsYear%>
 
    <script type="text/JavaScript">
        //Function Main
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
        <table style="width:100%;">
            <tr>
                <td colspan="3">
                    <dw:WebDataWindowControl ID="Dw_option" runat="server" 
                        AutoRestoreContext="False" AutoRestoreDataCache="True" 
                        AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" 
                        ClientScriptable="True" DataWindowObject="d_slclssrv_prc_year_shr_option" 
                        LibraryList="~/DataWindow/keeping/kp_slcls_year_shr.pbl" 
                        ClientEventButtonClicked="OnDwOptionButtonClick" 
                        style="top: 0px; left: 0px" ClientEventClicked="OnDwOptionClick">
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
</asp:Content>
