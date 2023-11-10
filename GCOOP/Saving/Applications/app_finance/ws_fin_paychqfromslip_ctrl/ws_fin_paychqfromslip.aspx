<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_fin_paychqfromslip.aspx.cs" Inherits="Saving.Applications.app_finance.ws_fin_paychqfromslip_ctrl.ws_fin_paychqfromslip" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool();
        var dsList = new DataSourceTool();

        function Validate() {
            var save_flag = 0;
            var alertstr = "";
            for (var i = 1; i <= dsList.GetRowCount(); i++) {
                if (dsList.GetItem(i - 1, "choose_flag") == "1") {
                    save_flag = 1;
                    if (dsList.GetItem(i - 1, "nonmember_detail") == null) {
                        alertstr = alertstr + "- กรุณาระบุการสั่งจ่าย\n";
                    }
                    if (dsList.GetItem(i - 1, "item_amtnet") == 0) {
                        alertstr = alertstr + "- กรุณาระบุจำนวนเงิน\n";
                    }
                }
                
            }
            if (dsMain.GetItem(0, "as_bank") == null || dsMain.GetItem(0, "as_bankbranch") == null || dsMain.GetItem(0, "as_chqbookno") == null || dsMain.GetItem(0, "as_chqstartno") == null || dsMain.GetItem(0, "as_chqstartno") == ""|| dsMain.GetItem(0, "as_chqtype") == null || dsMain.GetItem(0, "onchq_date") == null) {
                alertstr = alertstr + "- กรุณาเลือกข้อมูลการพิมพ์เช็คให้ครบ\n";
            }
            if (dsMain.GetItem(0, "as_fromaccno") == null) {
                alertstr = alertstr +"- กรุณาเลือกเลขที่บัญชี\n";
            }

             if (save_flag != 1) {
                alertstr = alertstr + "- กรุณาเลือกรายการที่จะทำการพิมพ์เช็ค\n";
            }
             if (save_flag == 1 && alertstr == "") {
                if (confirm("ยืนยันการบันทึกข้อมูล")) {
                    SaveWebSheet();
                }
            }
            else { alert(alertstr); return false; }
        }
        function SheetLoadComplete() {
            if (dsMain.GetItem(0, "as_bank") == null) {
                dsMain.GetElement(0, "as_bankbranch").disabled = true;
                dsMain.GetElement(0, "as_chqbookno").disabled = true;
                dsMain.GetElement(0, "as_chqstartno").disabled = true;
                dsMain.GetElement(0, "b_accno").disabled = true;
                dsMain.GetElement(0, "as_fromaccno").disabled = true;
//                dsMain.GetElement(0, "all_check").disabled = true;                
//                for (var i = 0; i < dsList.GetRowCount(); i++) {
//                    dsList.GetElement(i, "choose_flag").disabled = true;
//                }             
            } else {
                dsMain.GetElement(0, "as_bankbranch").disabled = false;
                if (dsMain.GetItem(0, "as_bankbranch") == null) {
                    dsMain.GetElement(0, "as_chqbookno").disabled = true;
                    dsMain.GetElement(0, "as_chqstartno").disabled = true;
                    dsMain.GetElement(0, "b_accno").disabled = true;
                    dsMain.GetElement(0, "as_fromaccno").disabled = true;
//                    dsMain.GetElement(0, "all_check").disabled = true;
//                    for (var i = 0; i < dsList.GetRowCount(); i++) {
//                        dsList.GetElement(i, "choose_flag").disabled = true;
//                    }  
                } else {
                    dsMain.GetElement(0, "as_chqbookno").disabled = false;
                    if (dsMain.GetItem(0, "as_chqbookno") == null) {
                        dsMain.GetElement(0, "as_chqstartno").disabled = true;
                        dsMain.GetElement(0, "b_accno").disabled = true;
                        dsMain.GetElement(0, "as_fromaccno").disabled = true;
                    } else {
                        dsMain.GetElement(0, "as_chqstartno").disabled = false;
                        dsMain.GetElement(0, "b_accno").disabled = false;
                        dsMain.GetElement(0, "as_fromaccno").disabled = false;
                    }
//                    dsMain.GetElement(0, "all_check").disabled = false;
//                    for (var i = 0; i < dsList.GetRowCount(); i++) {
//                        dsList.GetElement(i, "choose_flag").disabled = false;
//                    }  
                }                
            }

        }
        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "as_bank") {
                dsMain.SetItem(0, "as_bankbranch", "");
                dsMain.SetItem(0, "as_chqbookno", "");
                dsMain.SetItem(0, "as_chqstartno", "");
                dsMain.SetItem(0, "as_fromaccno", "");
                PostGetBank();
            } else if (c == "as_bankbranch") {
                dsMain.SetItem(0, "as_chqbookno", "");
                dsMain.SetItem(0, "as_chqstartno", "");
                dsMain.SetItem(0, "as_fromaccno", "");
                PostGetBankBranch();
            } else if (c == "as_chqbookno") {
                PostGetChqBookNo();
            } 
//            else if (c == "all_check") {
//                for (var ii = 0; ii < dsList.GetRowCount(); ii++) {
//                    dsList.SetItem(ii, "choose_flag", v);
//                }
//            }
        }
        function OnDsMainClicked(s, r, c, v) {
            if (c == "b_search") {
                //dsMain.SetItem(0, "all_check",0);;
                for (var i = 1; i <= dsList.GetRowCount(); i++) {
                    dsList.SetItem(i - 1, "choose_flag") == "0";
                }
                PostSearchData();
            }
            else if (c == "b_accno") {
                var bank_code = dsMain.GetItem(0, "as_bank");
                var bank_branch = dsMain.GetItem(0, "as_bankbranch").trim();
                Gcoop.OpenIFrame2(650, 600, 'wd_fin_deptaccount_search.aspx', "?frombank=" + bank_code + "&frombranch=" + bank_branch);
            }
        }

        function GetDeptNoFromDlg(deptno) {
            dsMain.SetItem(0, "as_fromaccno", deptno);
        }
        function OnDsListItemChanged(s, r, c, v) {
            if (c == "choose_flag") {
                for (var i = 1; i <= dsList.GetRowCount(); i++) {
                    dsList.SetItem(i - 1, "choose_flag") == "0";
                }
                dsList.SetItem(r, "choose_flag", v);
//                if (v == "0") {
//                    dsMain.SetItem(0, "all_check") == "0";
//                }
            }
        }
//        function change_ai_killer() {
//            var ai_killer = document.querySelectorAll('input[type="radio"]:checked');
//            var ai_killertxt = ai_killer.length > 0 ? ai_killer[1].value : null;
//            alert(ai_killertxt);
//            dsMain.SetItem(0, "ai_killer", ai_killertxt);
//        }

//        function change_ai_prndate() {
//            var ai_prndate = document.querySelectorAll('input[type="radio"]:checked');
//            var ai_prndatetxt = ai_prndate.length > 0 ? ai_prndate[1].value : null;
//            alert(ai_prndatetxt);
//            dsMain.SetItem(0, "ai_prndate", ai_prndatetxt);
//        }

//        function change_ai_payee() {
//            var ai_payee = document.querySelectorAll('input[type="radio"]:checked');
//            var ai_payeetxt = ai_payee.length > 0 ? ai_payee[0].value : null;
//            alert(ai_payeetxt);
//            dsMain.SetItem(0, "ai_payee", ai_payeetxt);
//        }

     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
<asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" /><br/>
    <uc2:DsList ID="dsList" runat="server" />
</asp:Content>