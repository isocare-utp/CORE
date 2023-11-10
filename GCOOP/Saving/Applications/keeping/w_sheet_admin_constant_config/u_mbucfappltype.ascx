<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="u_mbucfappltype.ascx.cs" Inherits="Saving.Applications.keeping.u_mbucfappltype" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>

<script type="text/javascript">
        function OnButtonClick(sender, row, name){
            if(name == "b_delete"){
                var detail = "รหัส " + objdw_main.GetItem(row, "appltype_code");
                detail += " : " + objdw_main.GetItem(row, "appltype_desc");
                
                if(confirm("คุณต้องการลบรายการ "+ detail +" ใช่หรือไม่?")){
                    objdw_main.DeleteRow(row);
                }
            }
            return 0;
        }
        
        function MenubarSave(){
            if(confirm("ยืนยันการบันทึกข้อมูลทั้งหมด?")){
                objdw_main.Update();
            }
        }
        
        function OnInsert(){
            objdw_main.InsertRow(objdw_main.RowCount() + 1);
        }
</script>

<dw:WebDataWindowControl ID="dw_main" runat="server"
    DataWindowObject="d_mbucfappltype" 
    LibraryList="~/DataWindow/keeping/admin_cm_constant_config.pbl" 
    ClientEventButtonClicked="OnButtonClick" ClientScriptable="True"
    RowsPerPage="20" OnBeginUpdate="PreUpdate" OnEndUpdate="PostUpdate">
    <PageNavigationBarSettings NavigatorType="NumericWithQuickGo" Visible="True">
    </PageNavigationBarSettings>
</dw:WebDataWindowControl>
<span class="linkSpan" onclick="OnInsert()"style="font-size: small; color:Green;
    float:right">เพิ่มแถว</span> <span style="font-size: small; color: #808080;">(หมายเหตุ
        - หลังจาก เพิ่มแถว/ลบแถว  แล้วกดปุ่ม save อีกครั้ง )</span>  
  