<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_sl_fundcoll_payment.aspx.cs" 
Inherits="Saving.Applications.shrlon.ws_sl_fundcoll_payment_ctrl.ws_sl_fundcoll_payment" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;
        var dsList = new DataSourceTool;

        function Validate() {
//            var moneytype_code = dsMain.GetItem(0, "moneytype_code");
//            var tofrom_accid = dsMain.GetItem(0, "tofrom_accid");
//            if (!moneytype_code || !tofrom_accid) {
//                alert("กรุณาเลือการทำรายการและคู่บัญชี");
//            } else {
//                return confirm("ยืนยันการบันทึก?");
//            }
            return confirm("ยืนยันการบันทึก?");
        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "member_no") {
                JsPostMember();
            }
            else if (c == "slip_date") {
                JsPostCalint();
            }
            else if (c == "moneytype_code") {
                JsPostTofromaccid();
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <uc1:DsMain ID="dsMain" runat="server" />
    <br />
    <uc2:DsList ID="dsList" runat="server" />
</asp:Content>
