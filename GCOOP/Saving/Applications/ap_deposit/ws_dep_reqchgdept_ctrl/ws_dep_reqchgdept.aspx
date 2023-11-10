<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_dep_reqchgdept.aspx.cs" Inherits="Saving.Applications.ap_deposit.ws_dep_reqchgdept_ctrl.ws_dep_reqchgdept" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;
        var dsList = new DataSourceTool();
        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "deptaccount_no") {
                PostDeptaccountno();
            } else if (c == "deptmonth_newamt") {
                dsMain.SetItem(0, "deptmonth_appamt", v);
            } 
        }
        function OnDsMainClicked(s, r, c) {
           
        }
        function SheetLoadComplete() {
        }
        function Validate() {
            var alertstr = "";
            var deptmonth_oldamt = dsMain.GetItem(0, "deptmonth_oldamt");
            var deptmonth_newamt = dsMain.GetItem(0, "deptmonth_newamt");
            if (Gcoop.ParseFloat(deptmonth_oldamt) == Gcoop.ParseFloat(deptmonth_newamt)) {
                alertstr = "จำนวนเงินส่งต่อเดือนใหม่ไม่การเปลี่ยนแปลงจากเดิม!!";
            }
            if (alertstr == "") {
                return confirm("ยืนยันการบันทึกข้อมูล");
            } else {
                alert(alertstr);
                return false;
            }
        }
        function MenubarOpen() {
            Gcoop.OpenIFrame(735, 700, 'w_dlg_dp_account_search.aspx', '')
        }
        function NewAccountNo(coop_id, deptno) {
            dsMain.SetItem(0, "deptaccount_no", deptno);
            PostDeptaccountno();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
    <br />
    <uc2:DsList ID="dsList" runat="server" />
</asp:Content>
