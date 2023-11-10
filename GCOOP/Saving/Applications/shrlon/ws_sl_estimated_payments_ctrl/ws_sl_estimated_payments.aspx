<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_sl_estimated_payments.aspx.cs" Inherits="Saving.Applications.shrlon.ws_sl_estimated_payments_ctrl.ws_sl_estimated_payments" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsDetail.ascx" TagName="DsDetail" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;
        var dsDetail = new DataSourceTool;

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล")
        }
        function OnDsMainClicked(s, r, c, v) {

            if (c == "b_gen") {

                PostGen();
            } else if (c == "b_pay") {

                PostTypePayment();
            } else if (c == "b_print") {

                PostPrint();
            }
        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "loantype_code") {
                PostLoantype();
            }
        }
      
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <%--<div align="right">
        <span class="NewRowLink" onclick="RunPrint();">พิมพ์รายงาน</span>
    </div>--%>
    <uc1:DsMain ID="dsMain" runat="server" />
    <uc2:DsDetail ID="dsDetail" runat="server" />
</asp:Content>
