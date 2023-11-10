<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_dp_const_cmucfbankbranch.aspx.cs" Inherits="Saving.Applications.deposit_const.w_sheet_dp_const_cmucfbankbranch_ctrl.w_sheet_dp_const_cmucfbankbranch" %>

<%@ Register src="DsDetail.ascx" tagname="DsDetail" tagprefix="uc2" %>
<%@ Register src="DsMain.ascx" tagname="DsMain" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }
        function SheetLoadComplete() {
        }
        //ประกาศฟังก์ชันสำหรับ event ItemChanged
        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "bank_code") {
                PostBankId();
            }
        }

        //ประกาศฟังก์ชันสำหรับ event Clicked
        function OnDsMainClicked(s, r, c) {
          
        }

        //ประกาศฟังก์ชันสำหรับ event ItemChanged
        function OnDsDetailItemChanged(s, r, c, v) {

        }

        //ประกาศฟังก์ชันสำหรับ event Clicked
        function OnDsDetailClicked(s, r, c) {
            if (c == "b_delete") {
                dsDetail.SetRowFocus(r);
                PostDeleteRow();
            }
        }

        function OnClickInsertRow() {
            PostInsertRow();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <uc1:DsMain ID="dsMain" runat="server" />
    <br />
    <span class="NewRowLink" onclick="OnClickInsertRow()">เพิ่มแถว</span>
    <uc2:DsDetail ID="dsDetail" runat="server" />
</asp:Content>
