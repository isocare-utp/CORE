<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_acc_ucf_acctype.aspx.cs"
    Inherits="Saving.Applications.account.w_acc_ucf_acctype" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%=initJavaScript%>
<%=postInsertRow%>
<%=postDeleteRow%>

    <script type="text/javascript">
     
     function MenubarNew(){
        postInsertRow();
     }
     
    // ฟังก์ชันการเพิ่มแถวข้อมูล
     function Insert_ucf_acctype() {
        postInsertRow();
     }

    // ฟังก์ชันการลบข้อมูล
     function Delete_ucf_acctype(sender, row, bName) {
         if (bName == "b_del") {
             var account_type_id = objDw_acc_start_acctype.GetItem(row,"account_type_id");
             
             if (account_type_id == "" || account_type_id == null ){
                Gcoop.GetEl("Hd_row").value = row + "";
                postDeleteRow(); 
             }else {
                var isConfirm = confirm("ต้องการลบข้อมูลรหัสประเภทบัญชี " + account_type_id + "ใช่หรือไม่ ?" );
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
        var RowDetail = objDw_acc_start_acctype.RowCount();
        var alertstr = "";
        var account_type_id = objDw_acc_start_acctype.GetItem(RowDetail,"account_type_id");
        var account_type_desc = objDw_acc_start_acctype.GetItem(RowDetail,"account_type_desc");
        
        if (account_type_id == "" || account_type_id == null){
            alertstr = alertstr + "_กรุณากรอกรหัสประเภทบัญชี\n";
        }
        if (account_type_desc == "" || account_type_desc == null){
            alertstr = alertstr+ "_กรุณากรอกชื่อประเภทบัญชี\n";
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
        <asp:Literal
            ID="LtServerMessage" runat="server"></asp:Literal>
        <table style="width:100%;">
            <tr>
                <td>
                <span class="linkSpan" onclick="Insert_ucf_acctype()" 
                    
                    style="font-family: Tahoma; font-size: small; float: left; color: #0000CC;">เพิ่มแถว</span></td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="3">
    <dw:WebDataWindowControl ID="Dw_acc_start_acctype" runat="server" ClientScriptable="True"
        DataWindowObject="d_acc_start_acctype" LibraryList="~/DataWindow/account/cm_constant_config.pbl"
        ClientEventButtonClicked="Delete_ucf_acctype" AutoRestoreContext="False" 
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
