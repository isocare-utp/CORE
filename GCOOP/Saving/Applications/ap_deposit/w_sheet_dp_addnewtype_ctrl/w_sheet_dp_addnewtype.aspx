<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_dp_addnewtype.aspx.cs" Inherits="Saving.Applications.ap_deposit.w_sheet_dp_addnewtype_ctrl.w_sheet_dp_addnewtype" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }
        function SheetLoadComplete() {
        }
        //ประกาศฟังก์ชันสำหรับ event ItemChanged
        function OnDsMainItemChange(s, r, c, v) {
            if (c == "depttype_group" || c == "depttype_code") {
                Gcoop.GetEl("HdDeptType").value = v;
                PostDeptType();
            }
        }

        //ประกาศฟังก์ชันสำหรับ event Clicked
        function OnDsListClicked(s, r, c) {
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <uc1:DsMain ID="dsMain" runat="server" />
    <br />
    <uc2:DsList ID="dsList" runat="server" />
    <asp:HiddenField ID="HdDeptType" runat="server" />
</asp:Content>
