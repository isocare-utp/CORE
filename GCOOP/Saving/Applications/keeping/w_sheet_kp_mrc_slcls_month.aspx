<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_kp_mrc_slcls_month.aspx.cs" Inherits="Saving.Applications.keeping.w_sheet_kp_mrc_slcls_month" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=postNewClear%>
    <%=postRefresh%>
    <%=postProcClsMonth%>
    <script type="text/JavaScript">
        //Function Main
        function OnDwOptionItemChange(s, r, c, v) {
            if (c == "clsmth_year") {
                objDw_option.SetItem(1, "clsmth_year", v);
                objDW_option.AcceptText();
            }
            else if (c == "clsmth_month") {
                objDw_option.SetItem(1, "clsmth_month", v);
                objDW_option.AcceptText();
            }
        }

        function OnDwOptionClick(s, r, c) {
            if (c == "shr_status") {
                Gcoop.CheckDw(s, r, c, "shr_status", 1, 0);
            }
            else if (c == "loan_status") {
                Gcoop.CheckDw(s, r, c, "loan_status", 1, 0);
            }
        }


        function OnDwOptionButtonClick(s, r, b) {
            if (b == "b_process") {
                B_ProcessClick();
            }
        }

        function B_ProcessClick() {
            var isconfirm = confirm("ต้องการประมวลผลปิดสิ้นเดือน ใช่หรือไม่ ?");
            if (!isconfirm) {
                return false;
            }
            postProcClsMonth();
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
                Gcoop.OpenProgressBar("ประมวลผลปิดสิ้นเดือน", true, true, postProcClsMonthComplete);
            }
        }

        function postProcClsMonthComplete() {
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
                        ClientScriptable="True" DataWindowObject="d_slclssrv_prc_mth_option" LibraryList="~/DataWindow/keeping/kp_mrc_slcls_month.pbl"
                        ClientEventButtonClicked="OnDwOptionButtonClick" Style="top: 0px; left: 0px"
                        ClientEventClicked="OnDwOptionClick" ClientEventItemChanged="OnDwOptionItemChange">
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
        </table>
    </p>
</asp:Content>
