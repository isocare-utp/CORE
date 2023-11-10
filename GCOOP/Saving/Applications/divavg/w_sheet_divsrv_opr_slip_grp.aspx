<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_divsrv_opr_slip_grp.aspx.cs" Inherits="Saving.Applications.divavg.w_sheet_divsrv_opr_slip_grp" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=postInit %>
    <%=postRefresh %>
    <%=postNewClear %>
    <%=postSetMoneytype %>
    <script type="text/JavaScript">
        //Function Main
        //=================================================================
        function OnDwmainItemChange(s, r, c, v) {
            if (c == "methpaytype_code") {
                var row = Gcoop.GetEl("Hdrow").value;
                objDw_main.SetItem(1, "methpaytype_code", v);
                objDw_main.AcceptText();
                postSetMoneytype();
            }
        }

        function OnDw_mainButtonClick(s, r, b) {
            if (b == "b_ok") {
                objDw_main.AcceptText();
                postInit();
            }
            else if (b == "b_cancel") {
                postNewClear();
            }
        }
        //Function Detail
        //=================================================================
        function SetExpense(expense_bank, expense_branch, expense_accid) {
            var row = Gcoop.GetEl("Hdrow").value;
            objDw_list.SetItem(row, "expense_bank", expense_bank);
            objDw_list.SetItem(row, "expense_accid", expense_accid);
            objDw_list.AcceptText();
        }

        function OnDwlistButtonClick(s, r, b) {
            if (b == "b_bank") {
                Gcoop.GetEl("Hdrow").value = r + "";
                Gcoop.OpenIFrame("500", "300", "w_dlg_divsrv_search_bank.aspx", "");
            }
        }

        function OnDwlistItemChange(s, r, c, v) {
            if (c == "expense_amt") {
                objDw_list.SetItem(r, "expense_amt", v);
                objDw_list.AcceptText();
                postRefresh();
            }
        }
        //Function Default
        //=================================================================
        function Validate() {
            var tofrom_accid = "";
            tofrom_accid = objDw_main.GetItem(1, "tofrom_accid");
            if (tofrom_accid == "" || tofrom_accid == null) {
                alert("กรุณาระบุคู่บัญชีจ่ายปันผล !!!");
                return false;
            }
            return confirm("ยืนยันการจ่ายปันผล เฉลี่ยคืน รายสังกัด");
        }
        function MenubarOpen() {

        }
        function MenubarNew() {
            postNewClear();
        }

        function SheetLoadComplete() {
            if (Gcoop.GetEl("Hd_process").value == "true") {
                Gcoop.OpenProgressBar("ประมวลจ่ายปันผล - เฉลี่ยคืนแบบกลุ่มสังกัด", true, true, ProcSaveSlipGrpComplete);
            }
        }

        function ProcSaveSlipGrpComplete() {
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
                <td>
                    <dw:WebDataWindowControl ID="Dw_main" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                        AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" ClientScriptable="True"
                        DataWindowObject="d_divsrv_opr_grp_option" LibraryList="~/DataWindow/divavg/divsrv_opr_slip_grp.pbl"
                        ClientEventButtonClicked="OnDw_mainButtonClick" ClientEventItemChanged="OnDwmainItemChange">
                    </dw:WebDataWindowControl>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="Panel2" runat="server" Height="300px" ScrollBars="Auto">
                        <dw:WebDataWindowControl ID="Dw_report" runat="server" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True"
                            ClientScriptable="True" DataWindowObject="d_divsrv_opr_grp_rpt_sum" LibraryList="~/DataWindow/divavg/divsrv_opr_slip_grp.pbl">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="Hdrow" runat="server" />
                    <asp:HiddenField ID="Hd_process" runat="server" />
                    <asp:HiddenField ID="Hddiv_year" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <br />
    </p>
    <%=outputProcess%>
</asp:Content>
