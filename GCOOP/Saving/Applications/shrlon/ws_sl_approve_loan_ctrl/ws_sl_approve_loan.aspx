<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_sl_approve_loan.aspx.cs" Inherits="Saving.Applications.shrlon.ws_sl_approve_loan_ctrl.ws_sl_approve_loan" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;
        var dsList = new DataSourceTool;

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function MenubarOpen() {
            Gcoop.OpenDlg('600', '500', 'w_dlg_sl_apvloan_docno.aspx', '');
        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "select_check") {
                if (v == 0) {
                    for (var i = 0; i < dsList.GetRowCount(); i++) {
                        dsList.SetItem(i, "choose_flag", v);
                        dsList.GetElement(i, "loanrequest_docno").style.background = "#FFFFFF";
                        dsList.GetElement(i, "cp_type").style.background = "#FFFFFF";
                        dsList.GetElement(i, "cp_name").style.background = "#FFFFFF";
                        dsList.GetElement(i, "loanrequest_amt").style.background = "#FFFFFF";
                        dsList.GetElement(i, "loanrequest_status").style.background = "#FFFFFF";
                        dsList.GetElement(i, "loancontract_no").style.background = "#FFFFFF";
                    }
                } else {
                    for (var i = 0; i < dsList.GetRowCount(); i++) {
                        dsList.SetItem(i, "choose_flag", v);
                        dsList.GetElement(i, "loanrequest_docno").style.background = "#FFFF99";
                        dsList.GetElement(i, "cp_type").style.background = "#FFFF99";
                        dsList.GetElement(i, "cp_name").style.background = "#FFFF99";
                        dsList.GetElement(i, "loanrequest_amt").style.background = "#FFFF99";
                        dsList.GetElement(i, "loanrequest_status").style.background = "#FFFF99";
                        dsList.GetElement(i, "loancontract_no").style.background = "#FFFF99";
                    }
                }
            } else if (c == "appv_status") {
                for (var i = 0; i < dsList.GetRowCount(); i++) {
                    if (dsList.GetItem(i, "choose_flag") == 1) {
                        dsList.SetItem(i, "loanrequest_status", v);
                    }
                }
            } else if (c == "loantype_code_txt") {
                dsMain.SetItem(0, "loantype_code", v);
            } else if (c == "loantype_code") {
                dsMain.SetItem(0, "loantype_code_txt", v);
            }

        }

        function OnDsMainClicked(s, r, c) {
            if (c == "b_gencontno") {
                PostGenContNo();
            } else if (c == "b_clear") {
                dsMain.SetItem(0, "entry_id", "");
                dsMain.SetItem(0, "member_no", "");
                dsMain.SetItem(0, "loantype_code_txt", "");
                dsMain.SetItem(0, "loantype_code", "");
                PostSearch();
            } else if (c == "b_search") {
                PostSearch();
            } else if (c == "b_cont") {
                PrintCont();
            } else if (c == "b_coll") {
                PrintColl();
            } else if (c == "b_int") {
                PrintIns();
            } else if (c == "b_spc") {
                PrintContSpc();
            }
        }

        function OnDsDsListItemChanged(s, r, c, v) {
            if (c == "choose_flag") {
                if (v == 0) {
                    dsMain.SetItem(0, "select_check", 0);
                    dsList.SetItem(r, "choose_flag", v);
                    dsList.GetElement(r, "loanrequest_docno").style.background = "#FFFFFF";
                    dsList.GetElement(r, "cp_type").style.background = "#FFFFFF";
                    dsList.GetElement(r, "cp_name").style.background = "#FFFFFF";
                    dsList.GetElement(r, "loanrequest_amt").style.background = "#FFFFFF";
                    dsList.GetElement(r, "loanrequest_status").style.background = "#FFFFFF";
                    dsList.GetElement(r, "loancontract_no").style.background = "#FFFFFF";
                } else {
                    dsList.SetItem(r, "choose_flag", v);
                    dsList.GetElement(r, "loanrequest_docno").style.background = "#FFFF99";
                    dsList.GetElement(r, "cp_type").style.background = "#FFFF99";
                    dsList.GetElement(r, "cp_name").style.background = "#FFFF99";
                    dsList.GetElement(r, "loanrequest_amt").style.background = "#FFFF99";
                    dsList.GetElement(r, "loanrequest_status").style.background = "#FFFF99";
                    dsList.GetElement(r, "loancontract_no").style.background = "#FFFF99";
                }
            } else if (c == "loanrequest_status") {
                if (v != 8) {
                    dsList.SetItem(r, "choose_flag", 1);
                    dsList.GetElement(r, "loanrequest_docno").style.background = "#FFFF99";
                    dsList.GetElement(r, "cp_type").style.background = "#FFFF99";
                    dsList.GetElement(r, "cp_name").style.background = "#FFFF99";
                    dsList.GetElement(r, "loanrequest_amt").style.background = "#FFFF99";
                    dsList.GetElement(r, "loanrequest_status").style.background = "#FFFF99";
                    dsList.GetElement(r, "loancontract_no").style.background = "#FFFF99";
                } else {
                    dsList.SetItem(r, "choose_flag", 0);
                    dsList.SetItem(r, "loancontract_no", "");
                    dsList.GetElement(r, "loanrequest_docno").style.background = "#FFFFFF";
                    dsList.GetElement(r, "cp_type").style.background = "#FFFFFF";
                    dsList.GetElement(r, "cp_name").style.background = "#FFFFFF";
                    dsList.GetElement(r, "loanrequest_amt").style.background = "#FFFFFF";
                    dsList.GetElement(r, "loanrequest_status").style.background = "#FFFFFF";
                    dsList.GetElement(r, "loancontract_no").style.background = "#FFFFFF";
                }
            }

        }

        function OnDsListClicked(s, r, c) {
        }

        function SheetLoadComplete() {
            for (var i = 0; i < dsList.GetRowCount(); i++) {
                if (dsList.GetItem(i, "choose_flag") == 1) {
                    dsList.GetElement(i, "loanrequest_docno").style.background = "#FFFF99";
                    dsList.GetElement(i, "cp_type").style.background = "#FFFF99";
                    dsList.GetElement(i, "cp_name").style.background = "#FFFF99";
                    dsList.GetElement(i, "loanrequest_amt").style.background = "#FFFF99";
                    dsList.GetElement(i, "loanrequest_status").style.background = "#FFFF99";
                    dsList.GetElement(i, "loancontract_no").style.background = "#FFFF99";
                }
            }
        }
        function Setfocus() {
            dsMain.Focus(0, "member_no");
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
   
    <uc1:DsMain ID="dsMain" runat="server" />
    <uc2:DsList ID="dsList" runat="server" />
</asp:Content>
