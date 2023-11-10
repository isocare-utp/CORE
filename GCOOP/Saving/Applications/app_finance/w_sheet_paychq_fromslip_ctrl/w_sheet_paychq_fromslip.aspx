<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_paychq_fromslip.aspx.cs" Inherits="Saving.Applications.app_finance.w_sheet_paychq_fromslip_ctrl.w_sheet_paychq_fromslip" %>

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
                }
                if (dsList.GetItem(i - 1, "pay_towhom") == null) {
                    alertstr = alertstr + "- กรุณาระบุการสั่งจ่าย\n";
                }
                if (dsList.GetItem(i - 1, "item_amtnet") == 0) {
                    alertstr = alertstr + "- กรุณาระบุจำนวนเงิน\n";
                }
            }
            if (dsMain.GetItem(0, "as_bank") == null || dsMain.GetItem(0, "as_bankbranch") == null || dsMain.GetItem(0, "as_chqbookno") == null || dsMain.GetItem(0, "as_chqtype") == null || dsMain.GetItem(0, "onchq_date") == null) {
                alertstr = alertstr + "- กรุณาเลือกข้อมูลการพิมพ์เช็คให้ครบ\n";
            }
            if (dsMain.GetItem(0, "as_fromaccno") == null) {
                alertstr = alertstr + "- กรุณาเลือกเลขที่บัญชี\n";
            }

            if (save_flag != 1) {
                alertstr = alertstr + "- กรุณาเลือกรายการที่จะพิมพ์เช็ค\n"; 
            }
            if (save_flag == 1 && alertstr == "") {
                if (confirm("ยืนยันการบันทึกข้อมูล")) {
                    SaveWebSheet();
                }
            }
            else { alert(alertstr); return false; }
        }

        function OnDsMainItemChanged(s, r, c, v) {
            
            if (c == "as_bank") {
                PostGetBank();
            } else if (c == "as_bankbranch") {
                PostGetBankBranch();
            } else if (c == "as_chqbookno") {
                PostGetChqBookNo();
            } else if (c == "all_check") {
                for (var ii = 0; ii < dsList.GetRowCount(); ii++) {
                    dsList.SetItem(ii, "choose_flag", v);
                }
            }
        }
//        $(function () {
//            $("input[type='radio']").on('click', function (e) {
//                //getCheckedRadio($(this).attr("name"), $(this).val(), this.checked);
//                getCheckedRadio($(this).attr("name"), $(this).val(), this.checked);
//                // alert(dsMain.GetItem(0, "ai_prndate"));
//            });

//        });
//        function getCheckedRadio(group, item, value) {
//            alert(group + '   ' + item + '   ' + value);
//            //            $(".ai_prndate").text(item));
//            $(".ctl00$ContentPlace$dsMain$FormView1$ai_prndate").text(group);
//                        $(".item-label").text(item);
//                        $(".item-value").text(value);
//        }
        function OnDsMainClicked(s, r, c, v) {
            if (c == "b_search") {
                PostSearchData();
            }
        }
//        function PostSetRadio() {
//            dsMain.
//        
//         }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
    <uc2:DsList ID="dsList" runat="server" />
</asp:Content>
