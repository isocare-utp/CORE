<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_sl_loan_receive_list.aspx.cs" Inherits="Saving.Applications.shrlon.ws_sl_loan_receive_list_ctrl.ws_sl_loan_receive_list" %>

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
            Gcoop.OpenDlg2("530", "350", "w_dlg_loan_search_receive.aspx", "");
        }

        function OnDsMainClicked(s, r, c) {
            if (c == "b_loan") {
                var loans = "";
                var lnrcvfrom_code = "";
                var word = "";

                for (var i = 0; i < dsList.GetRowCount(); i++) {
                    if (dsList.GetItem(i, "operate_flag") == 1) {

                        loans = dsList.GetItem(i, "loancontract_no");
                        lnrcvfrom_code = dsList.GetItem(i, "lnrcvfrom_code");
                        coop_id = dsList.GetItem(i, "coop_id");

                        word += "," + loans + "@" + lnrcvfrom_code + "@" + coop_id;
                    }
                }
                if (word != "") {
                    word = word.substring(1, word.length);
                }
               
                Gcoop.OpenIFrame3("800", "780", "w_dlg_loan_receive.aspx", "?loans=" + word);

            }
        }

        //from dlg w_dlg_loan_receive
        function GetShowData() {
            PostShowData();
        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "member_no") {
                PostMemberNo();
            } else if (c == "group") {
                PostShowData();
            }
        }

        function OnDsListClicked(s, r, c) {
            if (c == "b_detail") {
                var loancontract_no = "";

                dsList.SetRowFocus(r);
                loancontract_no = dsList.GetItem(r, "loancontract_no");
                Gcoop.OpenIFrame3("650", "500", "w_dlg_loan_receive_detail.aspx", "?loancontract_no=" + loancontract_no);
            }
        }

        function SheetLoadComplete() {
            $("#chk_all").click(function () {
                if ($("#chk_all").prop('checked')) {
                    for (var i = 0; i < dsList.GetRowCount(); i++) {
                        dsList.SetItem(i, "operate_flag", 1);
                    }
                } else {
                    for (var i = 0; i < dsList.GetRowCount(); i++) {
                        dsList.SetItem(i, "operate_flag", 0);
                    }
                }
            });
        }
        function GetValReport(payout_no, payin_no) {
            Gcoop.GetEl("HdPayoutNo").value = payout_no;
            Gcoop.GetEl("HdPayinNo").value = payin_no;
            PostReport();
        }

        function PrintSlipoutGsb(payoutslip_no, payinslip_no) {
            Gcoop.GetEl("HdPayoutNo").value = payoutslip_no;
            Gcoop.GetEl("HdPayinNo").value = payinslip_no;
            //PostPrintSlip();
        }

        function PrintSlipoutstk(slipout_no) {
            Gcoop.GetEl("HdPayoutNo").value = slipout_no;
            PostPrintSlip();

        }

        function PrintSlipout(payoutslip_no) {
            Gcoop.GetEl("HdPayoutNo").value = payoutslip_no;
            PostPrintSlip();
        }

        function PrintSlipAll(payout_no, payin_no) {
            Gcoop.GetEl("HdPayoutNo").value = payout_no;
            Gcoop.GetEl("HdPayinNo").value = payin_no;
            PostPrintSlip();
        }

        function GetValueFromDlg(slip_no) {
            /*alert(slip_no)
            Gcoop.GetEl("HdPayoutNo").value = slip_no;
           // Gcoop.GetEl("HdPayinNo").value = payin_no;
            PostPrintSlip();
            */
        }
        function Setfocus() {
            dsMain.Focus(0, "member_no");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <div align="right" style="width: 724px" >
        <span class="NewRowLink" onclick="PostPrintSlip();">พิมพ์ใบสำคัญจ่าย</span>
    </div>
    <div align="left">
        <uc1:DsMain ID="dsMain" runat="server" />
    </div>
    <uc2:DsList ID="dsList" runat="server" />
    <asp:HiddenField ID="HdRow" runat="server" />
    <asp:HiddenField ID="HdPayoutNo" runat="server" Value="" />
    <asp:HiddenField ID="HdPayinNo" runat="server" Value="" />
    <asp:HiddenField ID="HdDocument_no" runat="server" />

    <asp:HiddenField ID="Hdslipno" runat="server" />
</asp:Content>
