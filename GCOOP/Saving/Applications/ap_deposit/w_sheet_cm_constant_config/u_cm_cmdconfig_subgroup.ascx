<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="u_cm_cmdconfig_subgroup.ascx.cs"
    Inherits="Saving.Applications.u_cm_cmdconfig_subgroup" %>
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
            var detail = "รหัสประเภท" + objDwMain.GetItem(row, "itemtype_id");
            detail += " รหัสหมวด " + objDwMain.GetItem(row, "domain_id");
            detail += " รหัสกลุ่ม " + objDwMain.GetItem(row, "group_id");
            detail += " รหัสกลุ่มย่อย " + objDwMain.GetItem(row, "subgroup_id");
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
<dw:WebDataWindowControl ID="DwMain" runat="server" ClientScriptable="True" DataWindowObject="dw_cmd_subgroup_mas"
    LibraryList="~/DataWindow/Cmd/cm_constant_config.pbl" Width="19cm" ClientEventButtonClicked="OnButtonClick"
    Height="300px" Style="top: 0px; left: 0px">
</dw:WebDataWindowControl>
<%--</asp:Panel>--%><%--<span class="linkSpan" onclick="onclick="OnUpdate()">บันทึกข้อมูล</span> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  
<span class="linkSpan" onclick="OnInsert()">เพิ่มแถว</span>--%>