<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="u_dpucfdeptitemtype.ascx.cs"
    Inherits="Saving.Applications.ap_deposit.u_dpucfdeptitemtype" %>
<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<% Page_LoadComplete(); %>

<script type="text/javascript">
        function OnButtonClick(sender, row, name){
            if(name == "b_delete"){
                var detail = "รหัส " + objDwMain.GetItem(row, "deptitemtype_code");
                detail += " : " + objDwMain.GetItem(row, "deptitemtype_desc");
                
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

<dw:WebDataWindowControl ID="DwMain" runat="server" BorderStyle="NotSet" ClientScriptable="True"
    DataWindowObject="d_dpucfdeptitemtype" LibraryList="~/DataWindow/ap_deposit/cm_constant_config.pbl"
    ClientEventButtonClicked="OnButtonClick" RowsPerPage="20" OnBeginUpdate="PreUpdate"
    OnEndUpdate="PostUpdate">
    <PageNavigationBarSettings NavigatorType="NumericWithQuickGo" Visible="True">
    </PageNavigationBarSettings>
</dw:WebDataWindowControl>
<span class="linkSpan" onclick="OnInsert()">เพิ่มแถว</span> 