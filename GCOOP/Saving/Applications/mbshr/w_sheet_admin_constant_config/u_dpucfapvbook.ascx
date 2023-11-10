<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="u_dpucfapvbook.ascx.cs"
    Inherits="Saving.Applications.mbshr.u_dpucfapvbook" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<% Page_LoadComplete(); %>

<script type="text/javascript">
        function OnButtonClick(sender, row, name){
            if(name == "b_delete"){
                var detail = "รหัส " + objDwMain.GetItem(row, "apv_code");
                detail += " : " + objDwMain.GetItem(row, "name_th");
                
                if(confirm("คุณต้องการลบรายการ "+ detail +" ใช่หรือไม่?")){
                    objDwMain.DeleteRow(row);
                }
            }
            return 0;
        }
        
        function MenubarSave(){
            if(confirm("ยืนยันการบันทึกข้อมูลทั้งหมด?")){
                objDwMain.Update();
            }
        }
        
        function OnInsert(){
            objDwMain.InsertRow(1);
        }
</script>

<dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_dpucfapvbook"
    LibraryList="~/DataWindow/mbshr/admin_cm_constant_config.pbl" BorderStyle="NotSet"
    ClientEventButtonClicked="OnButtonClick" ClientScriptable="True" OnBeginUpdate="PreUpdate"
    OnEndUpdate="PostUpdate"   Width="570px" Height="350px" >
</dw:WebDataWindowControl>
<span class="linkSpan" onclick="OnInsert()"style="font-size: small; color:Green;
    float:right">เพิ่มแถว</span> <span style="font-size: small; color: #808080;">(หมายเหตุ
        - หลังจาก เพิ่มแถว/ลบแถว  แล้วกดปุ่ม save อีกครั้ง )</span>  