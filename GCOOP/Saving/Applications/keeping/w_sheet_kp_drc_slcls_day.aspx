<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_kp_drc_slcls_day.aspx.cs" Inherits="Saving.Applications.keeping.w_sheet_kp_drc_slcls_day" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=postNewClear%>
    <%=postRefresh%>
    <%=postProcClsDay%>
    <script type="text/JavaScript">
        //Function Main
        function OnDwOptionItemChange(s, r, c, v) {
            if (c == "clsday_tdate") {
                objDw_option.SetItem(1, "clsday_tdate", v);
                objDw_option.AcceptText();
                objDw_option.SetItem(1, "clsday_date", Gcoop.ToEngDate(v));
                objDw_option.AcceptText();
            }
        }

        function OnDwOptionClick(s, r, c) {
            if (c == "mem_status") {
                Gcoop.CheckDw(s, r, c, "mem_status", 1, 0);
            }
            else if (c == "shr_status") {
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
            var isconfirm = confirm("ต้องการประมวลผลปิดสิ้นวัน ใช่หรือไม่ ?");
            if (!isconfirm) {
                return false;
            }
            postProcClsDay();
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
                Gcoop.OpenProgressBar("ประมวลผลปิดสิ้นวัน", true, true, postProcClsDayComplete);
            }
        }

        function postProcClsDayComplete() {
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
                        ClientScriptable="True" DataWindowObject="d_slclssrv_prc_day_option" LibraryList="~/DataWindow/keeping/kp_drc_slcls_day.pbl"
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
    <%=outputProcess%>
</asp:Content>
