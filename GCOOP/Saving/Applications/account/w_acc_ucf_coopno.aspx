<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_acc_ucf_coopno.aspx.cs" Inherits="Saving.Applications.account.w_acc_ucf_coopno" Title="Untitled Page" %>
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
     function Insert_acc_start_coopno() {
        postInsertRow();
     }

        function Delete_acc_start_coopno(sender, row, bName) {
         if (bName == "b_del") {
             var coop_registered_no = objDw_acc_start_coopno.GetItem(row,"coop_registered_no");
             if (coop_registered_no == "" || coop_registered_no == null ){
                Gcoop.GetEl("Hd_row").value = row + "";
                postDeleteRow(); 
             }else {
                var isConfirm = confirm("ต้องการลบข้อมูลปีบัญชี " + coop_registered_no + "ใช่หรือไม่ ?" );
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
        var RowDetail = objDw_acc_start_coopno.RowCount();
        var alertstr = "";
        var coop_registered_no = objDw_acc_start_coopno.GetItem(RowDetail,"coop_registered_no");
        var coop_desc = objDw_acc_start_coopno.GetItem(RowDetail,"coop_desc");
        
        if (coop_registered_no == "" || coop_registered_no == null){
            alertstr = alertstr + "_กรุณากรอกเลขที่ทะเบียนสหกรณ์\n";
        }
        if (coop_desc == "" || coop_desc == null){
            alertstr = alertstr+ "_กรุณากรอกชื่อสหกรณ์\n";
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
                <span class="linkSpan" onclick="Insert_acc_start_coopno()" 
                    
                    style="font-family: Tahoma; font-size: small; float: left; color: #0000CC;">เพิ่มแถว</span></td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
        <table style="width:100%;">
            <tr>
                <td colspan="3">
    <dw:WebDataWindowControl ID="Dw_acc_start_coopno" runat="server" 
        ClientScriptable="True" DataWindowObject="d_acc_start_coopno" 
        LibraryList="~/DataWindow/account/cm_constant_config.pbl" 
        ClientEventButtonClicked="Delete_acc_start_coopno" AutoRestoreContext="False" 
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
    </p>
    </asp:Content>
