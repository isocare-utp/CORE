<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_mb_mbucfprename.aspx.cs" Inherits="Saving.Applications.mbshr.w_sheet_mb_mbucfprename_ctrl.w_sheet_mb_mbucfprename" %>
<%@ Register src="DsMain.ascx" tagname="DsMain" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    var dsMain = new DataSourceTool();
    function Validate() {
        return confirm("ยืนยันการบันทึกข้อมูล");
    }

    function OnDsMainClicked(s, r, c) {
        if (c == "b_del") {
            dsMain.SetRowFocus(r);
            PostDeleteRow();
        }
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>


    <uc1:DsMain ID="dsMain" runat="server" />


</asp:Content>
