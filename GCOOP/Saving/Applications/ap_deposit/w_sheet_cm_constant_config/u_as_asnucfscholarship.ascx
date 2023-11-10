﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="u_as_asnucfscholarship.ascx.cs"
    Inherits="Saving.Applications.u_as_asnucfscholarship" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<% Page_LoadComplete(); %>
<style type="text/css">
    .style1
    {
        font-family: Tahoma;
        font-size: x-small;
        font-weight: bold;
        color: #003399;
    }
</style>
<script type="text/javascript">
    function OnButtonClick(sender, row, name) {
        if (name == "b_delete") {
            var detail = "รหัสหน่วยนับ " + objDwMain.GetItem(row, "unit_id");
            //                detail += " รหัสบัญชี " + objDwMain.GetItem(row, "account_id");
            if (confirm("คุณต้องการลบรายการ " + detail + " ใช่หรือไม่?")) {
                objDwMain.DeleteRow(row);
            }
        }
        return 0;
    }

    function OnUpdate() {
        if (confirm("ยืนยันการบันทึกข้อมูลทั้งหมด?")) {
            objDwMain.Update();
        }
    }

    function OnInsert() {
        objDwMain.InsertRow(0);
    }
</script>
<div style="height: 18px; vertical-align: top">
    <span class="linkSpan" onclick="OnUpdate()" style="font-size: small; color: Green;
        float: right">บันทึกข้อมูล</span> <span class="linkSpan" onclick="OnInsert()" style="font-size: small;
            color: Red; float: left">เพิ่มแถว</span>
</div>
<dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_as_scholarship_ucf"
    LibraryList="~/DataWindow/app_assist/as_asnucf.pbl" ClientScriptable="True" Width="560px"
    ClientEventButtonClicked="OnButtonClick" Height="750px" UseCurrentCulture="True"
    RowsPerPage="30">
    <PageNavigationBarSettings Visible="True" NavigatorType="QuickGo">
        <BarStyle HorizontalAlign="Center" />
        <NumericNavigator FirstLastVisible="True" />
    </PageNavigationBarSettings>
</dw:WebDataWindowControl>
