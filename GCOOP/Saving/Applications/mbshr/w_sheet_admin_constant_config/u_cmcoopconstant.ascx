<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="u_cmcoopconstant.ascx.cs"
    Inherits="Saving.Applications.mbshr.u_cmcoopconstant" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>

<script type="text/javascript">


    function OnUpdate() {
        if (confirm("ยืนยันการบันทึกข้อมูลทั้งหมด?")) {
        
            objdw_main.Update();
        }
    }

    function OnInsert() {
        objdw_main.InsertRow(objdw_main.RowCount() + 1);
    }
    function MenubarSave() {
        if (confirm("บันทึกการแก้ไขข้อมูล?")) {
         
            objdw_main.Update();
        }
    }
</script>

<dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_cmcoopconstant"
    LibraryList="~/DataWindow/mbshr/admin_cm_constant_config.pbl" ClientScriptable="True"
    OnBeginUpdate="dw_main_BeginUpdate" OnEndUpdate="dw_main_EndUpdate">
</dw:WebDataWindowControl>
<span class="linkSpan" onclick="OnUpdate()" style="font-size: small; color: #808080;
    float: left">บันทึกข้อมูล</span> 