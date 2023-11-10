<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_dp_const_dpucfapvbook.aspx.cs" Inherits="Saving.Applications.ap_deposit.w_sheet_dp_show_intrate.w_sheet_dp_const_dpucfapvbook" %>
<%@ Register src="DsList.ascx" tagname="DsList" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <script type="text/javascript">

     //ประกาศตัวแปร ควรอยู่บริเวณบนสุดใน tag <script>
     var dsList = new DataSourceTool();


     //ประกาศฟังก์ชันสำหรับ event Clicked
     function OnDsListClicked(s, r, c) {
         if (c == "b_del") {
             dsList.SetRowFocus(r);
             PostDelRow();
         }
     }


     function Validate() {
         return confirm("ยืนยันการบันทึกข้อมูล");
     }

     function OnClickNewRow() {
         PostNewRow();
     }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <uc1:DsList ID="dsList" runat="server" />
</asp:Content>
