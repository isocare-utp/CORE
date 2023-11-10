﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="u_cm_cmdconfig_statdel.ascx.cs"
    Inherits="Saving.Applications.u_cm_cmdconfig_statdel" %>
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
            var detail = "รหัสประเภทการคิดค่าเสื่อม " + objDwMain.GetItem(row, "del_id");
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
<dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="ptucfstatusdel"
    LibraryList="~/DataWindow/Cmd/cm_constant_config.pbl" ClientScriptable="True"
    Width="560px" ClientEventButtonClicked="OnButtonClick" Height="300px">
</dw:WebDataWindowControl>
<%--</asp:Panel>--%>
<%--<span class="linkSpan" onclick="onclick="OnUpdate()">บันทึกข้อมูล</span> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  
<span class="linkSpan" onclick="OnInsert()">เพิ่มแถว</span>
--%>