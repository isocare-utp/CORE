<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_dp_deptadjust.aspx.cs" Inherits="Saving.Applications.ap_deposit.w_sheet_dp_deptadjust_ctrl.w_sheet_dp_deptadjust" %>
<%@ Register src="DsMain.ascx" tagname="DsMain" tagprefix="uc1" %>
<%@ Register src="DsDetail.ascx" tagname="DsDetail" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    function OnDsMainItemChanged(s, r, c, v) {
        if (c == "deptaccount_no") {
            JsPostDeptAcc();
        }
    }

    function OnDsDetailItemChanged(s, r, c, v) {

    }

    function OnDsDetailItemClicked(s, r, c) {

    }

    function OnDsMainClicked(s, r, c) {

    }

    function Validate() {
        return confirm("ยืนยันการบันทึกข้อมูล");
    
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <uc1:DsMain ID="dsMain" runat="server" />
    <uc2:DsDetail ID="dsDetail" runat="server" />
    
    <asp:HiddenField ID="Hdas_apvdoc" runat="server" />
</asp:Content>
