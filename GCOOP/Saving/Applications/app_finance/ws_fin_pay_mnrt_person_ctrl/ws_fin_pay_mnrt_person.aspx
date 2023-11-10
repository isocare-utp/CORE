<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_fin_pay_mnrt_person.aspx.cs" Inherits="Saving.Applications.app_finance.ws_fin_pay_mnrt_person_ctrl.ws_fin_pay_mnrt_person" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool();
        var dsList = new DataSourceTool();

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "member_no") {
                JsPostMembno();
            } else if (c == "expense_bank") {
                JsPostBank();
            }else if (c=="moneytype_code"){
                JsPostMoneytypeCode();
            }
        }

        function OnDsListItemChanged(s, r, c, v) {
            if (c == "operate_flag") {
                var operate_flag = dsList.GetItem(r, "operate_flag");
                var prn = dsList.GetItem(r, "principal_payamt");
                var int = dsList.GetItem(r, "interest_payamt");
                var sum = dsMain.GetItem(0, "payoutnet_amt");
                if (operate_flag == 1) {
                    sum = sum + prn + int;
                    dsMain.SetItem(0, "payoutnet_amt", sum);
                } else {
                    sum = sum - prn - int;
                    dsMain.SetItem(0, "payoutnet_amt", sum);
                }
            }
        }

        function OnDsMainClicked(s, r, c) {

        }

        function OnDsListClicked(s, r, c) {

        }

        function MenubarOpen() {
        }

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function SheetLoadComplete() {
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <uc1:DsMain ID="dsMain" runat="server" />
    <uc2:DsList ID="dsList" runat="server" />
</asp:Content>
