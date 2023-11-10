<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="u_dpucfcancelchqreson.ascx.cs"
    Inherits="Saving.Applications.ap_deposit.u_dpucfcancelchqreson" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<% Page_LoadComplete(); %>

<script type="text/javascript">
        function OnButtonClick(sender, row, name){
            if(name == "b_delete"){
                var detail = "รหัส " + objDwMain.GetItem(row, "cancel_id");
                detail += " : " + objDwMain.GetItem(row, "cancel_desc");
                
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
            objDwMain.InsertRow(0);
        }
</script>

<dw:WebDataWindowControl ID="DwMain" runat="server" ClientScriptable="True" DataWindowObject="d_dpucfcancelchqreson"
    LibraryList="~/DataWindow/ap_deposit/cm_constant_config.pbl" ClientEventButtonClicked="OnButtonClick"
    BorderStyle="NotSet" RowsPerPage="20" OnBeginUpdate="PreUpdate"
    OnEndUpdate="PostUpdate">
    <PageNavigationBarSettings NavigatorType="NumericWithQuickGo" Visible="True">
    </PageNavigationBarSettings>
</dw:WebDataWindowControl>
<span class="linkSpan" onclick="OnInsert()">เพิ่มแถว</span>