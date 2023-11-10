<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_as_genrequest.aspx.cs" Inherits="Saving.Applications.assist.ws_as_genrequest_ctrl.ws_as_genrequest" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<%@ Register Src="DsSum.ascx" TagName="DsSum" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script type="text/javascript">
        var dsMain = new DataSourceTool;
        var dsList = new DataSourceTool;
        var dsSum = new DataSourceTool;

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function OnDsMainClicked(s, r, c, v) {
            if (c == "b_process") {
                if (dsMain.GetItem(0, "process_month") == "1") { dsMain.SetItem(0, "process_month", "01"); }
                var chk_assistcode = dsMain.GetItem(0, "assisttype_code");
                var moneytype_code = dsMain.GetItem(0, "moneytype_code");
                var trtype_code = dsMain.GetItem(0, "trtype_code");
                var depttype_code = dsMain.GetItem(0, "depttype_code");

                if (chk_assistcode == "00" || chk_assistcode == null) {
                    alert("กรุณาเลือก ประเภทสวัสดิการ!!!"); return;
                } else {
                    if (moneytype_code == "TRN" && trtype_code == "DEP" && (depttype_code == "" || depttype_code == null)) {
                        alert("กรุณาเลือก ประเภทบัญชีเงินฝาก!!!"); return;
                    }
                    else {
                        PostProcess();
                    }
                }
            } else if (c == "b_save") {
                if (confirm("ยืนยันการบันทึก")) {
                    PostSave();
                }
            }
        }
        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "all_check") {
                JsCheckBoxAll();

            } else if (c == "moneytype_code") {
                if (v == "CSH") {
                    dsMain.SetItem(0, "trtype_code", '');
                    dsMain.GetElement(0, "trtype_code").style.background = "#CCCCCC";
                    dsMain.GetElement(0, "trtype_code").readOnly = true;
                    dsMain.GetElement(0, "trtype_code").disabled = true;
                    dsMain.SetItem(0, "depttype_code", '');
                    dsMain.GetElement(0, "depttype_code").style.background = "#CCCCCC";
                    dsMain.GetElement(0, "depttype_code").readOnly = true;
                    dsMain.GetElement(0, "depttype_code").disabled = true;
                } else if (v == "TRN") {
                    dsMain.SetItem(0, "trtype_code", '');
                    dsMain.GetElement(0, "trtype_code").style.background = "#FFFFFF";
                    dsMain.GetElement(0, "trtype_code").readOnly = false;
                    dsMain.GetElement(0, "trtype_code").disabled = false;
                    dsMain.SetItem(0, "depttype_code", '');
                    dsMain.GetElement(0, "depttype_code").style.background = "#FFFFFF";
                    dsMain.GetElement(0, "depttype_code").readOnly = false;
                    dsMain.GetElement(0, "depttype_code").disabled = false;
                } else {
                    dsMain.SetItem(0, "trtype_code", '');
                    dsMain.GetElement(0, "trtype_code").style.background = "#FFFFFF";
                    dsMain.GetElement(0, "trtype_code").readOnly = false;
                    dsMain.GetElement(0, "trtype_code").disabled = false;
                    dsMain.SetItem(0, "depttype_code", '');
                    dsMain.GetElement(0, "depttype_code").style.background = "#CCCCCC";
                    dsMain.GetElement(0, "depttype_code").readOnly = true;
                    dsMain.GetElement(0, "depttype_code").disabled = true;
                }
            } else if (c == "trtype_code") {
                var moneytype_code = dsMain.GetItem(0, "moneytype_code");
                if (v == "DEP" && moneytype_code != "TRN") {
                    alert("บัญชีเงินฝาก ใช้สำหรับประเภทเงินโอนภายในสหกรณ์เท่านั้น");
                    dsMain.SetItem(0, "trtype_code", '');
                    return;
                }
            }
            else if (c == "process_month") {
                SetCaldate();
            }
            else if (c == "assisttype_code") {
                PostSeleteData();
            }
            else if (c == "search_account") {
                dsMain.SetItem(0, "all_check", 0);
                JsSearchaccount();
            }
        }
        $(document).ready(function () {

            var sumappo_amt = 0;
            var summax_amt = 0;
            var approve_amt = 0;
            var max_payamt = 0;
            var account = "";
            $(".choose_flag").change(function () {
                dsMain.SetItem(0, "all_check", 0);
                var expense_accid = $(this).closest('tr').find('td .expense_accid').val()
                account = $(this).closest('tr').find('td .member_no').val()
                var trtype_code = dsMain.GetItem(0, "trtype_code");
                if (trtype_code === "DEP" && expense_accid != "" || trtype_code != "DEP") {
                    sumappo_amt = 0;
                    summax_amt = 0;

                    $('#ctl00_ContentPlace_GridView1 > tbody  > tr').each(function () {
                        var ischecked = $(this).find("td:eq(0)  input:checkbox")[0].checked;
                        if (ischecked) {
                            var cksumappo_amt = $(this).find("td:eq(7) input").val()
                            var cksummax_amt = $(this).find("td:eq(8) input").val()
                            sumappo_amt += Number(cksumappo_amt.replace(/[^0-9.-]+/g, ""));
                            summax_amt += Number(cksummax_amt.replace(/[^0-9.-]+/g, ""));
                        }
                    });


                    if ($(this).closest('tr').find('td input[type="checkbox"]').is(':checked') === true) {
                        $(this).closest('tr').find('td input').toggleClass("highlight", this.checked);
                        approve_amt = Number($(this).closest('tr').find('td .ap_amt').val().replace(/[^0-9.-]+/g, ""));
                        max_payamt = Number($(this).closest('tr').find('td .max_amt').val().replace(/[^0-9.-]+/g, ""));
                        approve_amt = fncToFixed(approve_amt, 2);
                        sumappo_amt = fncToFixed(sumappo_amt, 2);
                        max_payamt = fncToFixed(max_payamt, 2);
                        summax_amt = fncToFixed(summax_amt, 2);
                        //                        sumappo_amt += approve_amt;
                        //                        summax_amt += max_payamt;

                    }
                    else {
                        $(this).closest('tr').find('td input').removeClass("highlight");
                        approve_amt = Number($(this).closest('tr').find('td .ap_amt').val().replace(/[^0-9.-]+/g, ""));
                        max_payamt = Number($(this).closest('tr').find('td .max_amt').val().replace(/[^0-9.-]+/g, ""));
                        approve_amt = fncToFixed(approve_amt, 2);
                        sumappo_amt = fncToFixed(sumappo_amt, 2);
                        max_payamt = fncToFixed(max_payamt, 2);
                        summax_amt = fncToFixed(summax_amt, 2);
                        //                        sumappo_amt -= approve_amt;
                        //                        summax_amt -= max_payamt;
                    }
                    Gcoop.GetEl("Hd_setsum").value = 1;
                    dsSum.SetItem(0, "sumapprove_amt", sumappo_amt);
                    dsSum.SetItem(0, "sum_maxpayamt", summax_amt);
                } else {
                    $(this).closest('tr').find('td input[type="checkbox"]').prop("checked", false)
                    alert("ทะเบียน " + account + " ไม่มีเลขบัญชี กรุณาตรวจสอบ");
                }
            });

            $('.number').text(function () {
                var str = $(this).html() + '';
                x = str.split('.');
                x1 = x[0]; x2 = x.length > 1 ? '.' + x[1] : '';
                var rgx = /(\d+)(\d{3})/;
                while (rgx.test(x1)) {
                    x1 = x1.replace(rgx, '$1' + ',' + '$2');
                }
                $(this).html(x1 + x2);
            });

        });

        function OnDsListItemChanged(s, r, c, v) {
            if (c == "choose_flag") {
                var account_no = dsList.GetItem(r, "account_no");
                var member_no = dsList.GetItem(r, "member_no");
                if (account_no == "-") {
                    alert("ทะเบียน " + member_no + " ไม่มีเลขบัญชี กรุณาตรวจสอบ !!");
                    dsList.SetItem(r, "choose_flag", 0);
                }
                else {
                    dsList.SetRowFocus(r);
                    dsMain.SetItem(0, "all_check", 0);
                    JsPostCheckBoxRow();
                }

            }
        }
     
    </script>
    <style>
        .highlight
        {
            background-color: PaleGreen !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
    <br />
    <uc3:DsSum ID="dsSum" runat="server" />
    <uc2:DsList ID="dsList" runat="server" />
    
    <asp:HiddenField ID="Hd_rowcount" runat="server" />
    <asp:HiddenField ID="Hd_setsum" runat="server" />
    <asp:HiddenField ID="Hd_type_code" runat="server" />
</asp:Content>
