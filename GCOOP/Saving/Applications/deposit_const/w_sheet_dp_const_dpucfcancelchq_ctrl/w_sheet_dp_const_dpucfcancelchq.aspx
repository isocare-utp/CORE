<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_dp_const_dpucfcancelchq.aspx.cs" Inherits="Saving.Applications.deposit_const.w_sheet_dp_const_dpucfcancelchq_ctrl.w_sheet_dp_const_dpucfcancelchq" %>
<%@ Register src="DsMain.ascx" tagname="DsMain" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    var dsMain = new DataSourceTool();
    function OnDsMainClicked(s, r, c) {
        if (c == "b_del") {
            if (confirm("ยืนยันการลบข้อมูล")) {
                dsMain.SetRowFocus(r);
                PostDel();
            }
        }

    }
    function Validate() {
        return confirm("ยืนยันการบันทึกข้อมูล");
    }

    function SheetLoadComplete() {
    }

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
 <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
 <br />

 <span class="NewRowLink" onclick="PostInsertRow()">เพิ่มแถว</span>
 <uc1:DsMain ID="dsMain" runat="server" />

</asp:Content>
