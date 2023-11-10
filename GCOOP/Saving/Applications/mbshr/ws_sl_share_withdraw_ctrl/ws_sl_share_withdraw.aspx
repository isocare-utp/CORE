<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_sl_share_withdraw.aspx.cs" Inherits="Saving.Applications.mbshr.ws_sl_share_withdraw_ctrl.ws_sl_share_withdraw" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;
        var dsList = new DataSourceTool;

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }

        function OnDsMainClicked(s, r, c) {
            if (c == "b_withdraw") {
                var member_no = "";
                var word = "";

                for (var i = 0; i < dsList.GetRowCount(); i++) {
                    if (dsList.GetItem(i, "operate_flag") == 1) {

                        member_no = dsList.GetItem(i, "member_no");
                        word += "," + member_no
                    }
                }

                if (word != "") {
                    word = word.substring(1);
                }

                Gcoop.OpenIFrame3("830", "650", "ws_dlg_share_withdraw.aspx", "?share=" + word);
            } else if (c == "b_search") {
                var member_no_search = formatmember(dsMain.GetItem(r, "member_no"));
                for (var i = 0; i < dsList.GetRowCount(); i++) {
                    if (dsList.GetItem(i, "member_no") == member_no_search) {
                        Gcoop.GetEl("Hdrow_member").value = i;
                        Gcoop.GetEl("Hdmember_no").value = member_no_search;
                        Postmember_serach();
                    }
                }
            } else if (c == "b_print") {
                //alert(Gcoop.GetEl("HdSlipno").value);
                PostPrintSlippayin();
            }
        }


        function PrintSlipout(payinslip_no, payoutslip_no) {

            Gcoop.GetEl("HdPayoutNo").value = payoutslip_no;
            Gcoop.GetEl("HdPayinNo").value = payinslip_no;
            PostPrint();

        }


        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "checkselect") {
                PostCheckSelect();
            } else if (c == "coop_id") {
                PostShowData();
            }
        }

        function GetShowData(slip_no) {
            Gcoop.GetEl("HdSlipno").value = slip_no;
            PostShowData();
        }

        function OnDsListClicked(s, r, c) {
            if (c == "b_collwho") {
                var member_no = "";
                dsList.SetRowFocus(r);
                member_no = dsList.GetItem(r, "member_no");
                Gcoop.OpenIFrame3("680", "320", "ws_dlg_info_collwho.aspx", "?member_no=" + member_no);
            }
        }
        function formatmember(str) {
            var count = str.length;
            var strres = str;
            if (count < 8) {
                var count2 = 8 - count;

                for (var i = 0; i < count2; i++) {
                    strres = '0' + strres;
                }

            } else if (count > 8) {
                alert('รูปแบบทะเบียนไม่ถูก');
            }
            return strres;
        }

        function SheetLoadComplete() {

            //            var row_member = Gcoop.GetEl("Hdrow_member").value;
            //            alert(row_member);
            //            if(row_member != "-1"){
            //                var select = $('.DataSourceRepeater tbody').children();
            //                var index = parseInt(GetEl("Hdrow_member")) + 1;
            //                var row = select.eq(index);
            //                $(row).children().each(function(){
            //                    $(this).css('background-color', '#5CACEE');
            //                });
            //            }

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
    <uc2:DsList ID="dsList" runat="server" />
    <asp:HiddenField ID="Hdrow_member" runat="server" />
    <asp:HiddenField ID="Hdmember_no" runat="server" />
    <asp:HiddenField ID="HdSlipno" runat="server" Value="" />
     <asp:HiddenField ID="HdPayoutNo" runat="server" Value="" />
    <asp:HiddenField ID="HdPayinNo" runat="server" Value="" />
</asp:Content>
