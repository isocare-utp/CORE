<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_sl_interestpay_estimate_detail.aspx.cs" Inherits="Saving.Applications.shrlon.ws_sl_interestpay_estimate_detail_ctrl.ws_sl_interestpay_estimate_detail_ctrl" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsDetail.ascx" TagName="DsDetail" TagPrefix="uc2" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;
        var dsDetail = new DataSourceTool;
        var dsList = new DataSourceTool;


        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }
        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "member_no") {
                PostMemberNo();
            } else if (c == "operate_date") {
                dsDetail.SetRowFocus(r);
                var operate_flag = dsDetail.GetItem(r, "operate_flag");
                if (operate_flag == 1) {
                    PostOperateDate();
                } else { alert("กรุณาเลือกรายการหนี้"); }
            }
        }

        function OnDsMainClicked(s, r, c) {
            if (c == "b_memsearch") {
                Gcoop.OpenIFrame("630", "450", "w_dlg_sl_member_search_lite.aspx", "");
            } else if (c == "operate_date") {
                var row = dsDetail.GetRowCount();
                for (var i = 0; i < row; i++) {
                    if (dsDetail.GetItem(i, "operate_flag") == 1)
                    { var operate_flag = 1 }
                }
                if (operate_flag == 1) {
                    datePicker.PickDs(dsMain, r, c, PostOperateDate);
                } else { alert("กรุณาเลือกรายการหนี้"); }
            }
        }
        function GetValueFromDlg(member_no) {
            dsMain.SetItem(0, "member_no", member_no);
            PostMemberNo();

        }
        function OnDsDetailItemChanged(s, r, c, v) {
            if (c == "operate_flag") {
                dsDetail.SetRowFocus(r);
                PostOperateFlag();
            } else if (c == "periodcount_flag") {
                dsDetail.SetRowFocus(r);
                var periodcount_flag = dsDetail.GetItem(r, "periodcount_flag");
                var period = dsDetail.GetItem(r, "period");
                var sum = 0;
                if (periodcount_flag == 1) {
                    sum = period + 1;
                    dsDetail.SetItem(r, "period", sum);

                } else if (periodcount_flag == 0) {
                    dsDetail.SetItem(r, "period", period - 1);
                }
            } else if (c == "principal_payamt" || c == "interest_payamt") {
                dsDetail.SetRowFocus(r);
                var prin = dsDetail.GetItem(r, "principal_payamt");
                var inter = dsDetail.GetItem(r, "interest_payamt");
                var bfshrcont_balamt = dsDetail.GetItem(r, "bfshrcont_balamt");
                var total = prin + inter;
                var item_balance = bfshrcont_balamt - prin;
                dsDetail.SetItem(r, "item_payamt", total);
                dsDetail.SetItem(r, "item_balance", item_balance);

                var sum_total = 0;
                for (var i = 0; i < dsDetail.GetRowCount(); i++) {
                    sum_total = sum_total + dsDetail.GetItem(i, "item_payamt");
                }
                dsMain.SetItem(0, "slip_amt", sum_total);

            } else if (c == "item_payamt") {
                dsDetail.SetRowFocus(r);
                var item = dsDetail.GetItem(r, "item_payamt");
                var inter = dsDetail.GetItem(r, "interest_payamt");
                var total = item - inter;

                if (item <= inter) {
                    dsDetail.SetItem(r, "interest_payamt", item);
                    dsDetail.SetItem(r, "principal_payamt", 0);

                } else {
                    dsDetail.SetItem(r, "principal_payamt", total);
                }

                var bfshrcont_balamt = dsDetail.GetItem(r, "bfshrcont_balamt");
                var prin = dsDetail.GetItem(r, "principal_payamt");
                var total_itembal = bfshrcont_balamt - prin;
                dsDetail.SetItem(r, "item_balance", total_itembal);

                var sum_total = 0;
                for (var i = 0; i < dsDetail.GetRowCount(); i++) {
                    sum_total = sum_total + dsDetail.GetItem(i, "item_payamt");
                }
                dsMain.SetItem(0, "slip_amt", sum_total);

            }
        }
        function OnDsDetailClicked(s, r, c) {
            if (c == "b_detail") {

                var loancontract_no = "";

                dsDetail.SetRowFocus(r);
                loancontract_no = dsDetail.GetItem(r, "loancontract_no");
                Gcoop.OpenIFrame3("730", "400", "w_dlg_sl_detail.aspx", "?loancontract_no=" + loancontract_no);
            }
        }
     
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <uc1:DsMain ID="dsMain" runat="server" />
    <br />
    <uc2:DsDetail ID="dsDetail" runat="server" />
    <br />
    <uc3:DsList ID="dsList" runat="server" />
    <br />
</asp:Content>
