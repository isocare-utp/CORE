<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="u_cmcoopmaster.ascx.cs"
    Inherits="Saving.Applications.u_cmcoopmaster" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<% Page_LoadComplete(); %>
<script type="text/javascript">


    function OnUpdate() {
        if (confirm("ยืนยันการบันทึกข้อมูลทั้งหมด?")) {

            objdw_main.Update();
        }
    }

    function OnInsert() {
        objdw_main.InsertRow(0);
    }

    function MenubarSave() {
        if (confirm("บันทึกการแก้ไขข้อมูล?")) {
            objdw_main.Update();
        }
    }
</script>
<dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_cmcoopmaster"
    LibraryList="~/DataWindow/mbshr/admin_cm_constant_config.pbl" ClientScriptable="True"
    OnBeginUpdate="dw_main_BeginUpdate" OnEndUpdate="dw_main_EndUpdate">
</dw:WebDataWindowControl>
