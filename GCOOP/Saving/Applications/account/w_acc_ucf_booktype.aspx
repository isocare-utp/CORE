<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_acc_ucf_booktype.aspx.cs" Inherits="Saving.Applications.account.w_acc_ucf_booktype" Title="Untitled Page" %>
<%@ Register assembly="WebDataWindow" namespace="Sybase.DataWindow.Web" tagprefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<%=initJavaScript%>
<%=postInsertRow%>
<%=postDeleteRow%>

<script type ="text/javascript" >
    
    function MenubarNew(){
        postInsertRow();
    }
    
    // ฟังก์ชันการเพิ่มแถวข้อมูล
     function Insert_acc_start_booktype() {
        postInsertRow();
     }

     // ฟังก์ชันการลบข้อมูล
     function Delete_acc_start_booktype(sender, row, bName) {
         if (bName == "b_del") {
             var acc_book_type_id = objDw_acc_start_booktype.GetItem(row,"acc_book_type_id");
             
             if (acc_book_type_id == "" || acc_book_type_id == null ){
                Gcoop.GetEl("Hd_row").value = row + "";
                postDeleteRow(); 
             }else {
                var isConfirm = confirm("ต้องการลบข้อมูลรหัสสมุดรายวัน " + acc_book_type_id + "ใช่หรือไม่ ?" );
                 if (isConfirm) {
                    Gcoop.GetEl("Hd_row").value = row + "";
                    postDeleteRow(); 
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
        var RowDetail = objDw_acc_start_booktype.RowCount();
        var alertstr = "";
        var acc_book_type_id = objDw_acc_start_booktype.GetItem(RowDetail,"acc_book_type_id");
        var acc_book_type_desc = objDw_acc_start_booktype.GetItem(RowDetail,"acc_book_type_desc");
        
        if (acc_book_type_id == "" || acc_book_type_id == null){
            alertstr = alertstr + "_กรุณากรอกรหัสสมุดรายวัน\n";
        }
        if (acc_book_type_desc == "" || acc_book_type_desc == null){
            alertstr = alertstr+ "_กรุณากรอกชื่อสมุดรายวัน\n";
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
    <p>
        <asp:Literal ID="LtServerMessage" 
            runat="server"></asp:Literal>
        <table style="width:100%;">
            <tr>
                <td>
                <span class="linkSpan" onclick="Insert_acc_start_booktype()" 
                    
                    style="font-family: Tahoma; font-size: small; float: left; color: #0000CC;">เพิ่มแถว</span></td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="3">
    <dw:WebDataWindowControl ID="Dw_acc_start_booktype" runat="server" 
        ClientScriptable="True" DataWindowObject="d_acc_start_booktype" 
        LibraryList="~/DataWindow/account/cm_constant_config.pbl" 
        ClientEventButtonClicked="Delete_acc_start_booktype" AutoRestoreContext="False" 
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True">
    </dw:WebDataWindowControl>
                </td>
            </tr>
        </table>
        <table style="width:100%;">
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
    </p>
</asp:Content>
