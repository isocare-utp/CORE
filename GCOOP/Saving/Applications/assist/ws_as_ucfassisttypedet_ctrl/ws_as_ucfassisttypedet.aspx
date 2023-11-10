<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_as_ucfassisttypedet.aspx.cs" Inherits="Saving.Applications.assist.ws_as_ucfassisttypedet_ctrl.ws_as_ucfassisttypedet" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<%@ Register Src="DsCopy.ascx" TagName="DsCopy" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        //ประกาศตัวแปร ควรอยู่บริเวณบนสุดใน tag <script>
        var dsMain = new DataSourceTool;
        var dsList = new DataSourceTool();
        var dsCopy = new DataSourceTool();
        function chkNumber(ele) {
            var vchar = String.fromCharCode(event.keyCode);
            if ((vchar < '0' || vchar > '9') && (vchar != '.')) return false;
            ele.onKeyPress = vchar;
        }
        //ประกาศฟังก์ชันสำหรับ event ItemChanged
        function OnDsListItemChanged(s, r, c, v) {
            if (c == "membcat_code") {
                var ls_mbcat = dsList.GetItem(r, "membcat_code");
                Gcoop.GetEl("Hdrow").value = r + "";
                if (ls_mbcat == "AL" || ls_mbcat == null) {
                    dsList.GetElement(r, "membtype_code").disabled = true;
                    dsList.GetElement(r, "membtype_code").style.background = "#CCCCCC";
                    Postmoneytype();
                }
                else {
                    dsList.GetElement(r, "membtype_code").disabled = false;
                    dsList.GetElement(r, "membtype_code").style.background = "#FFFFFF";
                    Postmoneytype();
                }
            }
        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "assisttype_code") {
                PostSeleteData();
            } 
        }

        //ประกาศฟังก์ชันสำหรับ event Clicked
        function OnDsListClicked(s, r, c) {
            if (c == "b_del") {
                dsList.SetRowFocus(r);
                if (confirm("ลบข้อมูลแถวที่ " + (r + 1) + " ?") == true) {
                    PostDelRow();
                }  
            }
        }

        //ประกาศฟังก์ชันสำหรับ copy ประเภทการจ่ายสวัสดิการ

        function OnClickCopyAss() {
            var ls_assyear = dsMain.GetItem(0, "process_year");
            if (ls_assyear != "" || ls_assyear != null) {
                Gcoop.OpenIFrame2(370, 30, "wd_ass_getyear.aspx", "");
            }
        }


        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function SheetLoadComplete() {
        }

        function OnClickNewRow() {
            PostNewRow();
            var ls_mbcat = dsList.GetItem(r, "membcat_code");
            Gcoop.GetEl("Hdrow").value = r + "";
            if (ls_mbcat == "AL" || ls_mbcat == null) {
                dsList.GetElement(r, "membtype_code").disabled = true;
                dsList.GetElement(r, "membtype_code").style.background = "#CCCCCC";
            }
            else {
                dsList.GetElement(r, "membtype_code").disabled = false;
                dsList.GetElement(r, "membtype_code").style.background = "#FFFFFF";
                Postmoneytype();
            }
        }

        function GetValueyear(year) {
            Gcoop.GetEl("Hd_year").value = year;
            jsPostYear();
        }
   </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
<asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
   <center>
        <uc1:DsMain ID="dsMain" runat="server" />
    </center>
    <span class="NewRowLink" onclick="OnClickNewRow()">เพิ่มแถว</span> 
    <span class="NewRowLink" onclick="OnClickCopyAss()">คัดลอกเงื่อนไขการจ่าย</span> 
    <uc2:DsList ID="dsList" runat="server" />   
    <uc3:DsCopy ID="dsCopy" runat="server" />   
     <asp:HiddenField ID="Hdmb_type" runat="server" />
     <asp:HiddenField ID="Hdrow" runat="server" />
     <asp:HiddenField ID="Hd_year" runat="server" /> 
</asp:Content>

