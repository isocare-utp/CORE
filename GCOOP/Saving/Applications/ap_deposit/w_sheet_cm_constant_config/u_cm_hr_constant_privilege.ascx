<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="u_cm_hr_constant_privilege.ascx.cs"
    Inherits="Saving.Applications.u_cm_hr_constant_privilege" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<% Page_LoadComplete(); %>
<style type="text/css">
    .linkSpan
    {
        float: none;
    }
</style>
<script type="text/javascript">
    function OnButtonClick(sender, row, name) {
        if (name == "b_delete") {
            var detail = "รหัส " + objDwMain.GetItem(row, "posiwid");
            detail += " : " + objDwMain.GetItem(row, "privilege");

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
<h2>
    กำหนดสิทธิการลา</h2>
<div style="height: 18px; vertical-align: top">
    <span class="linkSpan" onclick="OnUpdate()">บันทึกข้อมูล</span> &nbsp; <span class="linkSpan"
        onclick="OnInsert()">เพิ่มแถว</span>
</div>
<dw:WebDataWindowControl ID="DwMain" runat="server" BorderStyle="Solid" BorderWidth="0px"
    ClientScriptable="True" DataWindowObject="dw_hr_config_privilege" LibraryList="~/DataWindow/hr/hr_constant.pbl"
    Width="560px" ClientEventButtonClicked="OnButtonClick">
</dw:WebDataWindowControl>
<span class="linkSpan" onclick="OnUpdate()">บันทึกข้อมูล</span> &nbsp; <span class="linkSpan"
    onclick="OnInsert()">เพิ่มแถว</span> 