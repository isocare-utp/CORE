<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" 
    CodeBehind="ws_am_accountyear.aspx.cs" Inherits="Saving.Applications.admin.ws_am_accountyear_ctrl.ws_am_accountyear" %>

<%@ Register src="DsList.ascx" tagname="DsList" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        //ประกาศตัวแปร ควรอยู่บริเวณบนสุดใน tag <script>
        var dsList = new DataSourceTool();

        //ประกาศฟังก์ชันสำหรับ event ItemChanged
        function OnDsListItemChanged(s, r, c, v) {
            
        }

        //ประกาศฟังก์ชันสำหรับ event Clicked
        function OnDsListClicked(s, r, c) {
            if (c == "b_del") {
                dsList.SetRowFocus(r);
                if (confirm("ลบข้อมูลแถวที่ " + (r + 1) + " ใช่หรือไม่") == true) {
                    PostDelRow();
                }
            }
            
        }

        function Validate() {
            var i = dsList.GetRowCount()-1;
            //alert(i);
            var accstart_date = dsList.GetItem(i, "accstart_date");
            var accend_date = dsList.GetItem(i, "accend_date");
                if (accstart_date == "1500-01-01 00:00:00" || accstart_date == null) {
                    alert("กรุณากรอกวันที่เริ่ม");
                } else if (accend_date == "1500-01-01 00:00:00" || accend_date == null) {
                    alert("กรุณากรอกวันที่สิ้นสุด");                     
                } else {
                return confirm("ยืนยันการบันทึกข้อมูล");
                }
        }
            
        

        function SheetLoadComplete() {
           
        }

        function OnClickNewRow() {
            PostNewRow();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <span class="NewRowLink" onclick="OnClickNewRow()">เพิ่มแถว</span>
    <uc1:DsList ID="dsList" runat="server" />
</asp:Content>
