﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_mbshr_ucf_cremation.aspx.cs" Inherits="Saving.Applications.mbshr.ws_mbshr_ucf_cremation_ctrl.ws_mbshr_ucf_cremation" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc1" %>
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
                if (confirm("ลบข้อมูลแถวที่ " + (r + 1) + " ?") == true) {
                    PostDelRow();
                }                
            }
        }
        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
                                     
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

