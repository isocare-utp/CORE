<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_sl_proc_chgfixperiodpay.aspx.cs" Inherits="Saving.Applications.shrlon.ws_sl_proc_chgfixperiodpay_ctrl.ws_sl_proc_chgfixperiodpay" %>
<%@ Register src="DsMain.ascx" tagname="DsMain" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    var dsMain = new DataSourceTool();

    function OnDsMainItemChanged(s, r, c, v) {
        
    }

    function OnDsMainClicked(s, r, c) {
        if (c == "b_proc") {
            Post();
        } 
    }

    function Validate() {
        return confirm("ยืนยันการบันทึกข้อมูล");
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
</asp:Content>
