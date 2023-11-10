<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_ucf_contcode.aspx.cs" Inherits="Saving.Applications.account.w_sheet_ucf_contcode" Title="Untitled Page" %>
<%@ Register assembly="WebDataWindow" namespace="Sybase.DataWindow.Web" tagprefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%=initJavaScript%>
<%=postInsertRow %>
<%=postDeleteRow %>

<script type ="text/javascript" >
    function MenubarNew() {
        postInsertRow();
    }
      
    // ฟังก์ชันการเพิ่มแถวข้อมูล
     function Insert_acc_const_code() {
        postInsertRow();
     }

   function Delete_acc_cntcode(sender, row, bName) {
         if (bName == "b_del") {
             var cnt_code = objDw_acc_const_code.GetItem(row,"cnt_code");
             
             if (cnt_code == "" || cnt_code == null ){
                Gcoop.GetEl("Hdrow").value = row + "";
                postDeleteRow(); 
             }else {
                var isConfirm = confirm("ต้องการลบข้อมูลรหัส " + cnt_code + "ใช่หรือไม่ ?" );
                 if (isConfirm) {
                    Gcoop.GetEl("Hdrow").value = row + "";
                    postDeleteRow(); 
                }
             }  
         }
         return 0;
     }
     
     //ฟังก์ชันการเช็คค่าข้อมูลก่อน save
    function Validate() {
        var isconfirm = confirm("ยืนยันการบันทึกข้อมูล ?");
        if (!isconfirm){
            return false;        
        }
        var RowDetail = objDw_acc_const_code.RowCount();
        var alertstr = "";
        var cnt_code = objDw_acc_const_code.GetItem(RowDetail,"cnt_code");
        var cnt_desc = objDw_acc_const_code.GetItem(RowDetail,"cnt_desc");     
        if (cnt_code == "" || cnt_code == null){
            alertstr = alertstr + "_กรุณากรอกรหัส\n";
        }
        if (cnt_desc == "" || cnt_desc == null){
            alertstr = alertstr+ "_กรุณากรอกรายการ\n";
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
                <span class="linkSpan" onclick="Insert_acc_const_code()" 
                    
                    style="font-family: Tahoma; font-size: small; float: left; color: #0000CC;">เพิ่มแถว</span></td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="3">
    <dw:WebDataWindowControl ID="Dw_acc_const_code" runat="server" 
        ClientScriptable="True" DataWindowObject="d_acc_const_code" 
        LibraryList="~/DataWindow/account/cm_constant_config.pbl" 
        ClientEventButtonClicked="Delete_acc_cntcode" AutoRestoreContext="False" 
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True">
    </dw:WebDataWindowControl>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="Hdrow" runat="server" />
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
    </p>
    </asp:Content>
