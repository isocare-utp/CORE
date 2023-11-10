<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="u_mbucfposition.ascx.cs" Inherits="Saving.Applications.mbshr.u_mbucfposition" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<% Page_LoadComplete(); %>

<script type="text/javascript">
        function OnButtonClick(sender, row, name){
            if(name == "b_delete"){
                var detail = "รหัส " + objdw_main.GetItem(row, "position_code");
                detail += " : " + objdw_main.GetItem(row, "position_desc");
                
                if(confirm("คุณต้องการลบรายการ "+ detail +" ใช่หรือไม่?")){
                    objdw_main.DeleteRow(row);
                }
            }
            return 0;
        }
        
        function MenubarSave(){
            if (confirm("ยืนยันการบันทึกข้อมูลทั้งหมด?")) {
                var a = objdw_main.GetItem(1, "coop_id");
                for (var j = 1; j <= objdw_main.RowCount(); j++) {
                    objdw_main.SetItem(j, "coop_id", a);
                }
                objdw_main.AcceptText();
                objdw_main.Update();
            }
        }
        
        function OnInsert(){
            objdw_main.InsertRow(objdw_main.RowCount() + 1);
        }
</script>

<dw:WebDataWindowControl ID="dw_main" runat="server"
    DataWindowObject="d_mbucfposition" 
    LibraryList="~/DataWindow/mbshr/admin_cm_constant_config.pbl" 
    ClientEventButtonClicked="OnButtonClick" ClientScriptable="True"
    RowsPerPage="20" OnBeginUpdate="PreUpdate" OnEndUpdate="PostUpdate" >
    <PageNavigationBarSettings NavigatorType="NumericWithQuickGo" Visible="True">
    </PageNavigationBarSettings>
</dw:WebDataWindowControl>
<span class="linkSpan" onclick="OnInsert()"style="font-size: small; color:Green;
    float:right">เพิ่มแถว</span> <span style="font-size: small; color: #808080;">(หมายเหตุ
        - หลังจาก เพิ่มแถว/ลบแถว  แล้วกดปุ่ม save อีกครั้ง )</span>  