<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_as_cancelpay.aspx.cs" Inherits="Saving.Applications.assist.ws_as_cancelpay_ctrl.ws_as_cancelpay" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;
        var dsList = new DataSourceTool;

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "assisttype_code") {
                dsMain.SetItem(0, "assist_code", v);
            } else if (c == "assist_code") {
                dsMain.SetItem(0, "assisttype_code", v);
            } else if (c == "select_check") {
                if (v == 0) {
                     for (var i = 0; i < dsList.GetRowCount(); i++) {
                        dsList.SetItem(i, "choose_flag", v);
                        dsList.GetElement(i, "assistslip_no").style.background = "#FFFFFF";
                        dsList.GetElement(i, "slip_date").style.background = "#FFFFFF";
                        dsList.GetElement(i, "member_no").style.background = "#FFFFFF";
                        dsList.GetElement(i, "assisttype_code").style.background = "#FFFFFF";
                        dsList.GetElement(i, "mbname").style.background = "#FFFFFF";
                        dsList.GetElement(i, "payout_amt").style.background = "#FFFFFF";
                        dsList.GetElement(i, "pay_period").style.background = "#FFFFFF";
                    }
                } else {
                    for (var i = 0; i < dsList.GetRowCount(); i++) {
                        dsList.SetItem(i, "choose_flag", v);
                        dsList.GetElement(i, "assistslip_no").style.background = "#FFFF99";
                        dsList.GetElement(i, "slip_date").style.background = "#FFFF99";
                        dsList.GetElement(i, "member_no").style.background = "#FFFF99";
                        dsList.GetElement(i, "assisttype_code").style.background = "#FFFF99";
                        dsList.GetElement(i, "mbname").style.background = "#FFFF99";
                        dsList.GetElement(i, "payout_amt").style.background = "#FFFF99";
                        dsList.GetElement(i, "pay_period").style.background = "#FFFF99";
                    }
                }
            } else if (c == "req_status") {
                PostChangeStatus();
            }
        }

        function OnDsMainClicked(s, r, c) {
            if (c == "b_clear") {
                dsMain.SetItem(0, "member_no", "");
                dsMain.SetItem(0, "assist_code", "");
                dsMain.SetItem(0, "assisttype_code", "");
                PostSearch();
            } else if (c == "b_search") {
                PostSearch();
            } else if (c == "b_searchmem") {
                Gcoop.OpenIFrame2(650, 600, 'wd_as_member_search.aspx', '')
            }
        }

        function GetMembNoFromDlg(memberno) {
            dsMain.SetItem(0, "member_no", memberno.trim());
        }

        function OnDsDsListItemChanged(s, r, c, v) {
            if (c == "choose_flag") {                
                if (v == 0) {
                    dsMain.SetItem(0, "select_check", 0);
                    dsList.SetItem(r, "choose_flag", v);
                    dsList.GetElement(r, "assistslip_no").style.background = "#FFFFFF";
                    dsList.GetElement(r, "slip_date").style.background = "#FFFFFF";
                    dsList.GetElement(r, "member_no").style.background = "#FFFFFF";
                    dsList.GetElement(r, "assisttype_code").style.background = "#FFFFFF";
                    dsList.GetElement(r, "mbname").style.background = "#FFFFFF";
                    dsList.GetElement(r, "payout_amt").style.background = "#FFFFFF";
                    dsList.GetElement(r, "pay_period").style.background = "#FFFFFF";
                } else {
                    dsList.SetItem(r, "choose_flag", v);
                    dsList.GetElement(r, "assistslip_no").style.background = "#FFFF99";
                    dsList.GetElement(r, "slip_date").style.background = "#FFFF99";
                    dsList.GetElement(r, "member_no").style.background = "#FFFF99";
                    dsList.GetElement(r, "assisttype_code").style.background = "#FFFF99";
                    dsList.GetElement(r, "mbname").style.background = "#FFFF99";
                    dsList.GetElement(r, "payout_amt").style.background = "#FFFF99";
                    dsList.GetElement(r, "pay_period").style.background = "#FFFF99";
                }
            } else if (c == "req_status") {
                if (v != 8) {
                    dsList.SetItem(r, "choose_flag", 1);
                    dsList.GetElement(r, "assistslip_no").style.background = "#FFFF99";
                    dsList.GetElement(r, "slip_date").style.background = "#FFFF99";
                    dsList.GetElement(r, "member_no").style.background = "#FFFF99";
                    dsList.GetElement(r, "assisttype_code").style.background = "#FFFF99";
                    dsList.GetElement(r, "mbname").style.background = "#FFFF99";
                    dsList.GetElement(r, "payout_amt").style.background = "#FFFF99";
                    dsList.GetElement(r, "pay_period").style.background = "#FFFF99";
                } else {
                    dsList.SetItem(r, "choose_flag", 0);
                    dsList.GetElement(r, "assist_docno").style.background = "#FFFFFF";
                    dsList.GetElement(r, "assistpay_code").style.background = "#FFFFFF";
                    dsList.GetElement(r, "assisttype_code").style.background = "#FFFFFF";
                    dsList.GetElement(r, "mbname").style.background = "#FFFFFF";
                    dsList.GetElement(r, "approve_amt").style.background = "#FFFFFF";
                    dsList.GetElement(r, "pay_period").style.background = "#FFFFFF";
                }
            }

        }

        function OnDsListClicked(s, r, c) {
        }

        function SheetLoadComplete() {
            for (var i = 0; i < dsList.GetRowCount(); i++) {
                if (dsList.GetItem(i, "choose_flag") == 1) {
                    dsList.GetElement(i, "assistslip_no").style.background = "#FFFF99";
                    dsList.GetElement(i, "slip_date").style.background = "#FFFF99";
                    dsList.GetElement(i, "member_no").style.background = "#FFFF99";
                    dsList.GetElement(i, "assisttype_code").style.background = "#FFFF99";
                    dsList.GetElement(i, "mbname").style.background = "#FFFF99";
                    dsList.GetElement(i, "payout_amt").style.background = "#FFFF99";
                }
            }
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
   
    <uc1:DsMain ID="dsMain" runat="server" />
    <uc2:DsList ID="dsList" runat="server" />
</asp:Content>


