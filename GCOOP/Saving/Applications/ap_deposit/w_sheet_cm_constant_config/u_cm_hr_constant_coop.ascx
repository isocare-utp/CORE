<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="u_cm_hr_constant_coop.ascx.cs"
    Inherits="Saving.Applications.u_cm_hr_constant_coop" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<% Page_LoadComplete(); %>
<script type="text/javascript">
    function OnButtonClick(sender, row, name) {
        if (name == "b_delete") {
            var detail = "รหัส " + objDwMain.GetItem(row, "dateid");
            detail += " : " + objDwMain.GetItem(row, "workdayname");

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
    กำหนดชื่อสหกรณ์</h2>
<div style="height: 18px; vertical-align: top">
    <span class="linkSpan" onclick="OnUpdate()">บันทึกข้อมูล</span> &nbsp;
</div>
<dw:WebDataWindowControl ID="DwMain" runat="server" BorderStyle="Solid" BorderWidth="0px"
    ClientScriptable="True" DataWindowObject="dw_hr_config_coop" LibraryList="~/DataWindow/hr/hr_constant.pbl"
    ClientEventButtonClicked="OnButtonClick" Style="top: 0px; left: 0px; width: 625px">
</dw:WebDataWindowControl>
<span class="linkSpan" onclick="OnUpdate()">บันทึกข้อมูล</span> &nbsp; 