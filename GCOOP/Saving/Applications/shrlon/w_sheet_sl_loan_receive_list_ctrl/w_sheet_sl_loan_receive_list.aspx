<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_sl_loan_receive_list.aspx.cs" Inherits="Saving.Applications.shrlon.w_sheet_sl_loan_receive_list_ctrl.w_sheet_sl_loan_receive_list" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        var dsMain = new DataSourceTool;
        var dsList = new DataSourceTool;

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

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }
        function MenubarOpen() {
            Gcoop.OpenIFrame3("530", "350", "w_dlg_loan_search_receive.aspx", "");
        }
        function OnDsMainClicked(s, r, c) {
            if (c == "b_show") {
                PostShowDataGroupEntry();
                //                if (dsMain.GetItem(0, "group") == "0") {
                //                    PostShowData();
                //                } else {
                //                    PostShowDataGroup();
                //                }
            } else if (c == "b_search") {
                Gcoop.OpenIFrame3("530", "350", "w_dlg_loan_search_receive.aspx", "");
            } else if (c == "b_loan") {
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

                // loans = Gcoop.EncodeUtf8(loans);

                //Gcoop.OpenIFrame2("830", "650", "w_dlg_loan_receive_order.aspx", "?loans=" + word);

                var sdate = dsMain.GetItem(0, "slip_date");
                var payadv_flag = dsMain.GetItem(0, "payadvance_flag");
                //alert(sdate + " : " + payadv_flag);
                Gcoop.OpenIFrame2("800", "780", "w_dlg_loan_receive.aspx", "?loans=" + word + "&sdate=" + sdate + "&pay_flag=" + payadv_flag);
            } else if (c == "b_loop_save") {
                if (confirm("ยืนยันการจ่ายทั้งหมด ?")) {
                    PostLoopSave();
                }

            }
        }

        function GetShowData() {
            PostShowDataGroupEntry();
        }

        function OnDsMainItemChanged(s, r, c, v) {

            if (c == "type_show") {
                // dsList.SetRowFocus(r);
                PostTypeShow();
            } else if (c == "member_no") {
                PostMemberNo();
            } else if (c == "payadvance_flag" || "slip_date") {
                PostCheck();
            } else if (c == "b_receipt_pay") {
                PostPrintReceiptPay();
            } else if (c == "b_receipt") {
                PostPrintReceipt();
            }

        }
        function GetItemLoan(sqlext_con, sqlext_req) {
            //Gcoop.RemoveIFrame(); // ปิด Iframe
            Gcoop.GetEl("HdCon").value = sqlext_con;
            Gcoop.GetEl("HdReq").value = sqlext_req;
            //alert(Gcoop.GetEl("HdCon").value);
            PostSearchLoan();


        }
        function OnDsListClicked(s, r, c) {
            if (c == "b_detail") {

                var loancontract_no = "";

                dsList.SetRowFocus(r);
                loancontract_no = dsList.GetItem(r, "loancontract_no");
                Gcoop.OpenIFrame3("650", "500", "w_dlg_loan_receive_detail.aspx", "?loancontract_no=" + loancontract_no);
            }
        }

        function GetValReport(ref_val) {
            Gcoop.GetEl("HdRefno").value = ref_val;
            PostReport();
        }
        function PrinSlip(payinslipno) {
            Gcoop.GetEl("HdPayinslipno").value = payinslipno;
            // Gcoop.GetEl("HdPayoutslipno").value = payoutslipno;
            jsPrintslip();

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <!--<div align="right" style="width: 724px" >
        <span class="NewRowLink" onclick="PostShowData();">แสดงข้อมูลทั้งหมด</span>
    </div>-->
    <div align="left">
        <uc1:DsMain ID="dsMain" runat="server" />
    </div>
    <uc2:DsList ID="dsList" runat="server" />
    <asp:HiddenField ID="HdRow" runat="server" />
    <asp:HiddenField ID="HdCon" runat="server" />
    <asp:HiddenField ID="HdReq" runat="server" />
    <asp:HiddenField ID="HdRefno" runat="server" />
     <asp:HiddenField ID="HdPayinslipno" runat="server" />
     <asp:HiddenField ID="HdPayoutNo" runat="server" />
    
</asp:Content>
