<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_sl_insurfire.aspx.cs" Inherits="Saving.Applications.shrlon.ws_sl_insurfire_ctrl.ws_sl_insurfire"
    ValidateRequest="false" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<%@ Register Src="DsDetail.ascx" TagName="DsDetail" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;
        var dsList = new DataSourceTool;
        var dsDetail = new DataSourceTool;
        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "member_no") {
                dsMain.SetItem(r, c, v);
                PostMemberNo();
            }
        }

        function OnDsListClicked(s, r, c) {
            if (c == "insurance_no" || c == "insure_date" || c == "loancontract_no" || c == "insurance_amt" || c == "pay_status") {

                dsList.SetRowFocus(r);
                PostGetRow();
            }
        }

        function OnDsDetailItemChanged(s, r, c, v) {
            if (c == "insurance_no") {
                PostInsuranceNo();
            }
            else if (c == "loancontract_no") {
                dsDetail.Focus(0, 'operate_date');
            }
            else if (c == "operate_date") {
                dsDetail.Focus(0, 'startinsure_date');
            }
            else if (c == "startinsure_date") {
                var sinsure_date = dsDetail.GetItem(0, "startinsure_date");

                date1 = new Date(sinsure_date);

                var mkMonth = date1.getMonth() + 1;
                mkMonth = new String(mkMonth);
                if (mkMonth.length == 1) {
                    mkMonth = "0" + mkMonth;
                }
                var mkDay = date1.getDate();
                mkDay = new String(mkDay);
                if (mkDay.length == 1) {
                    mkDay = "0" + mkDay;
                }

                date1.setFullYear(date1.getFullYear() + 1);

                var einsure_date = date1.getFullYear() + '-' + mkMonth + '-' + mkDay;

                dsDetail.SetItem(0, "expireinsure_date", einsure_date);
                dsDetail.Focus(0, 'expireinsure_date');
            }
            else if (c == "expireinsure_date") {
                dsDetail.Focus(0, 'insurance_amt');
            }
            else if (c == "insurance_amt") {
                CalInsuranceAmt(v);
                CalStampdutyAmt();
                CalFloodinsureAmt();
                CalInsurenetAmt();
                dsDetail.Focus(0, 'stampduty_amt');
                //                PostInsur();
            }
            else if (c == "stampduty_amt") {
                CalStampdutyAmt();
                CalFloodinsureAmt();
                dsDetail.Focus(0, 'vat_percent');
                //                PostInsur();                
            }
            else if (c == "vat_percent") {
                dsDetail.Focus(0, 'discount_percent');
            }
            else if (c == "discount_percent") {
                dsDetail.Focus(0, 'floodinsure_amt');
            }
            else if (c == "floodinsure_amt") {
                CalFloodinsureAmt();
                CalInsurenetAmt()
                dsDetail.Focus(0, 'insurenet_amt');
            }
            else if (c == "insurenet_amt") {
                CalInsurenetAmt();
                dsDetail.Focus(0, 'coordinate_percent');
            }
            else if (c == "coordinate_percent") {
                dsDetail.Focus(0, 'coordinate_percent');
            }
        }

        function CalInsuranceAmt(insurance_amt) {
            var stampduty_amt = dsDetail.GetItem(0, "stampduty_amt");
            var vat_percent = dsDetail.GetItem(0, "vat_percent");
            var c_vatamt = dsDetail.GetItem(0, "c_vatamt");
            var c_vatamt = (insurance_amt + stampduty_amt) * vat_percent;
            dsDetail.SetItem(0, "c_vatamt", c_vatamt);

            var insurance_amt = dsDetail.GetItem(0, "insurance_amt");
            var c_vatamt = dsDetail.GetItem(0, "c_vatamt");
            var vat_sum = insurance_amt + stampduty_amt + c_vatamt;
            dsDetail.SetItem(0, "vat_sum", vat_sum);

            var discount_percent = dsDetail.GetItem(0, "discount_percent");
            var dp_sum = insurance_amt * discount_percent;
            dsDetail.SetItem(0, "dp_sum", dp_sum);

            var coordinate_percent = dsDetail.GetItem(0, "coordinate_percent");
            var coor_sum = insurance_amt * coordinate_percent;
            dsDetail.SetItem(0, "coor_sum", coor_sum);
        }

        function CalStampdutyAmt() {
            var stampduty_amt = dsDetail.GetItem(0, "stampduty_amt");            
            var insurance_amt = dsDetail.GetItem(0, "insurance_amt");
            var vat_percent = dsDetail.GetItem(0, "vat_percent");
            var c_vatamt = (insurance_amt + stampduty_amt) * vat_percent;
            dsDetail.SetItem(0, "c_vatamt", c_vatamt);

            var c_vatamt = dsDetail.GetItem(0, "c_vatamt");
            var vat_sum = insurance_amt + stampduty_amt + c_vatamt;
            dsDetail.SetItem(0, "vat_sum", vat_sum);
        }

        function CalFloodinsureAmt() {
            var floodinsure_amt = dsDetail.GetItem(0, "floodinsure_amt");
            var vat_sum = dsDetail.GetItem(0, "vat_sum");
            var dp_sum = dsDetail.GetItem(0, "dp_sum");
            var insurenet_amt = (vat_sum - dp_sum) + floodinsure_amt;
            dsDetail.SetItem(0, "insurenet_amt", insurenet_amt);
        }

        function CalInsurenetAmt() {
            var insurenet_amt = dsDetail.GetItem(0, "insurenet_amt");
            var coor_sum = dsDetail.GetItem(0, "coor_sum");
            var txt_sum = insurenet_amt - coor_sum;
            dsDetail.SetItem(0, "txt_sum", txt_sum);
        }

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }
        function MenubarOpen() {
            Gcoop.OpenIFrame("580", "720", "w_dlg_sl_member_search_lite.aspx", "")
        }
        function OnDsMainClicked(s, r, c) {
            if (c == "b_memsearch") {
                Gcoop.OpenIFrame("580", "720", "w_dlg_sl_member_search_lite.aspx", "")
            }
        }
        function GetValueFromDlg(member_no) {
            dsMain.SetItem(0, "member_no", member_no);
            PostMemberNo();
        }
        function OnDsDetailClicked(s, r, c) {
            if (c == "b_contsearch") {
                var membno = "";
                membno = dsMain.GetItem(0, "member_no");
                Gcoop.OpenIFrame2("600", "500", "w_dlg_ln_collmast.aspx", "?member_no=" + membno)
                dsDetail.Focus(0, 'operate_date');
            }
        }
        function GetCollmastNoFromDlg(collmast_no) {
            dsDetail.SetItem(0, "collmast_no", collmast_no);
            dsDetail.Focus(0, 'collmast_no');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <uc1:DsMain ID="dsMain" runat="server" />
    <uc2:DsList ID="dsList" runat="server" />
    <uc3:DsDetail ID="dsDetail" runat="server" />
    <br />
</asp:Content>
