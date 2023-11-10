<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_acc_ucf_group.aspx.cs" Inherits="Saving.Applications.account.w_acc_ucf_group" Title="Untitled Page" %>
<%@ Register assembly="WebDataWindow" namespace="Sybase.DataWindow.Web" tagprefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%=initJavaScript%>
<%=postInsertRow%>
<%=postDeleteRow%>

<script type ="text/javascript" >

    // ฟังก์ชันการเพิ่มแถวข้อมูล
     function Insert_acc_start_group() {
        postInsertRow();
     }

      // ฟังก์ชันการลบข้อมูล
     function Delete_ucf_start_group(sender, row, bName) {
         if (bName == "b_del") {
             var account_group_id = objDw_acc_start_group.GetItem(row,"account_group_id");
             
             if (account_group_id == "" || account_group_id == null ){
                Gcoop.GetEl("Hd_row").value = row + "";
                postDeleteRow(); 
             }else {
                var isConfirm = confirm("ต้องการลบข้อมูลรหัสหมวดบัญชี " + account_group_id + "ใช่หรือไม่ ?" );
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
        var account_group_id = objDw_acc_start_group.GetItem(1,"account_group_id");
        var account_group_desc = objDw_acc_start_group.GetItem(1,"account_group_desc");
        
        if (account_group_id == "" || account_group_id == null){
            alertstr = alertstr + "_กรุณากรอกรหัสหมวดบัญชี\n";
        }
        if (account_group_desc == "" || account_group_desc == null){
            alertstr = alertstr+ "_กรุณากรอกชื่อหมวดบัญชี\n";
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


    //ฟังก์ชันการเช็คค่าข้อมูลก่อน save
    function Validate() {
        var isconfirm = confirm("ยืนยันการบันทึกข้อมูล ?");
        if (!isconfirm ){
            return false;        
        }
        var RowDetail = objDw_acc_start_group.RowCount();
        var alertstr = "";
        var account_group_id = objDw_acc_start_group.GetItem(RowDetail,"account_group_id");
        var account_group_desc = objDw_acc_start_group.GetItem(RowDetail,"account_group_desc");
        
        if (account_group_id == "" || account_group_id == null){
            alertstr = alertstr + "_กรุณากรอกรหัสหมวดบัญชี\n";
        }
        if (account_group_desc == "" || account_group_desc == null){
            alertstr = alertstr+ "_กรุณากรอกชื่อหมวดบัญชี\n";
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
    <style type="text/css">
        .style1
        {
            height: 19px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal 
            ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <table style="width:100%;">
        <tr>
            <td class="style1">
                <span class="linkSpan" onclick="Insert_acc_start_group()" 
                    
                    style="font-family: Tahoma; font-size: small; float: left; color: #0000CC;">เพิ่มแถว</span></td>
            <td class="style1">
            </td>
            <td class="style1">
            </td>
        </tr>
        <tr>
            <td colspan="3">
    <dw:WebDataWindowControl ID="Dw_acc_start_group" runat="server" 
        ClientScriptable="True" DataWindowObject="d_acc_start_group" 
        LibraryList="~/DataWindow/account/cm_constant_config.pbl" 
        ClientEventButtonClicked="Delete_ucf_start_group" AutoRestoreContext="False" 
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
