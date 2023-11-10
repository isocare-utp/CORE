<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_acc_ucf_accsection.aspx.cs"
    Inherits="Saving.Applications.account.w_acc_ucf_accsection" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%= initJavaScript %>
<%= postInsertDetail %>
<%= postDeleteDetail %>


    <script type="text/javascript">
      
     function MenubarNew(){
        postInsertDetail();  
     }


    // ฟังก์ชันการเพิ่มแถวข้อมูล
     function Insert_ucf_accsection() {
       postInsertDetail();
     }


     // ฟังก์ชันการลบข้อมูล
     function Delete_ucf_accsection(sender, row, bName) {
         if (bName == "b_del") {
             var section_id = objDw_ucf_accsection.GetItem(row,"section_id");
             
             if (section_id == "" || section_id == null ){
                Gcoop.GetEl("Hd_row").value = row + "";
                postDeleteDetail(); 
             }else {
                var isConfirm = confirm("ต้องการลบข้อมูลชื่อย่อระบบ " + section_id + "ใช่หรือไม่ ?" );
                 if (isConfirm) {
                    Gcoop.GetEl("Hd_row").value = row + "";
                    postDeleteDetail(); 
                }
             }  
         }
         return 0;
     }
     
     
      //ฟังก์ชันการเช็คค่าข้อมูลก่อน save
    function Validate() {
        var isconfirm = confirm("ยืนยันการบันทึกข้อมูล ?");
        
        if (!isconfirm ){
            return false;        
        }
        var RowDetail = objDw_ucf_accsection.RowCount();
        var alertstr = "";
        var section_id = objDw_ucf_accsection.GetItem(RowDetail,"section_id");
        var section_desc = objDw_ucf_accsection.GetItem(RowDetail,"section_desc");
        
        if (section_id == "" || section_id == null){
            alertstr = alertstr + "_กรุณากรอกชื่อย่อระบบ\n";
        }
        if (section_desc == "" || section_desc == null){
            alertstr = alertstr+ "_กรุณากรอกชื่อระบบ\n";
        }
        if (alertstr == "")
        {
            return true;
        }
        else {
            alert(alertstr);
            return false;
        }
    }
    
      

   
    

    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
  
    <asp:Literal 
        ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <table style="width:100%;">
        <tr>
            <td>
                <span class="linkSpan" onclick="Insert_ucf_accsection()" 
                    
                    style="font-family: Tahoma; font-size: small; float: left; color: #0000CC;">เพิ่มแถว</span></td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="3">
    <dw:WebDataWindowControl ID="Dw_ucf_accsection" runat="server" ClientScriptable="True"
        DataWindowObject="d_acc_start_section" LibraryList="~/DataWindow/account/cm_constant_config.pbl"
        ClientEventButtonClicked="Delete_ucf_accsection" AutoRestoreContext="False" 
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True">
    </dw:WebDataWindowControl>
    
    
            </td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="Hd_row" runat="server" />
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <br />
    <br />
    <br />
    <br />
    
    
</asp:Content>
