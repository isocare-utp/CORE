<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_acc_ucf_accyear.aspx.cs" Inherits="Saving.Applications.account.w_acc_ucf_accyear" Title="Untitled Page" %>
<%@ Register assembly="WebDataWindow" namespace="Sybase.DataWindow.Web" tagprefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postDeleteRow%>
    <%=postInsertRow%>
    <%=postCanDelYearRow%>
    <%=postRefresh %>
<script type ="text/javascript" >
    
    function MenubarNew()
    {
        postInsertRow();
    }
    
    // ฟังก์ชันการเพิ่มแถวข้อมูล
     function Insert_ucf_accyear() {
        postInsertRow();
     }
     
    function OnDwmasterClick(s, r, c){
        Gcoop.CheckDw(s, r, c, "close_account_stat", 1, 0);
    }
    
   

// ฟังก์ชันการลบข้อมูล
     function Delete_DwmainClick(sender, row, bName) {
         if (bName == "b_del") {
             var account_year = objDw_main.GetItem(row,"account_year");
             
             if (account_year == "" || account_year == null ){
                Gcoop.GetEl("Hdrow").value = row + "";
                postDeleteRow(); 
             }else {
                var isConfirm = confirm("ต้องการลบข้อมูลปีบัญชี " + account_year + "ใช่หรือไม่ ?" );
                 if (isConfirm) {
                    Gcoop.GetEl("Hdrow").value = row + "";
                    postCanDelYearRow(); 
                }
             }  
         }
         return 0;
     }
     

    function OnDwMainItemChange(s,r,c,v)//เปลี่ยนวันที่
    {
        if (v == "" || v == null){
            alert("กรุณากรอกข้อมูลวันที่เริ่มต้นรอบปี/วันที่สิ้นสุดรอบบัญชีให้ครบ");
        }else {
        if (c == "begin_tdate") {
            s.SetItem(r, "begin_tdate", v);
            s.AcceptText();
            s.SetItem(r, "begining_of_accou", Gcoop.ToEngDate(v));
            s.AcceptText();
            postRefresh();
            
        } else if (c == "end_tdate") {
            s.SetItem(r, "end_tdate", v);
            s.AcceptText();
            s.SetItem(r, "ending_of_account", Gcoop.ToEngDate(v));
            s.AcceptText();
            postRefresh();
        }
        else if (c == "account_year_t") {
            s.SetItem(r, "account_year_t",v);
            s.AcceptText();
            s.SetItem(r, "account_year", v - 543);
            s.AcceptText();
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
        var RowDetail = objDw_main.RowCount();
        var alertstr = "";
        var account_year = objDw_main.GetItem(RowDetail,"account_year");
        var begin_tdate = objDw_main.GetItem(RowDetail,"begin_tdate");
        var end_tdate = objDw_main.GetItem(RowDetail,"end_tdate");
        //var close_account_stat =  objDw_main.GetItem(RowDetail,"close_account_stat");
            
        if (account_year == "" || account_year == null){
           alertstr = alertstr + "_กรุณากรอกปีบัญชี\n";
        }
        if (begin_tdate == "" || begin_tdate == null){
            alertstr = alertstr+ "_กรุณากรอกวันที่เริ่มต้นรอบปี\n";
        }
        if (end_tdate == "" || end_tdate == null){
           alertstr = alertstr+ "_กรุณากรอกวันที่สิ้นสุดรอบบัญชี\n";
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
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
      <table style="width:100%;">
        <tr>
            <td>
                <span class="linkSpan" onclick="Insert_ucf_accyear()" 
                    
                    style="font-family: Tahoma; font-size: small; float: left; color: #0000CC;">เพิ่มแถว</span></td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="3">
                    <dw:WebDataWindowControl ID="Dw_main" runat="server" 
                        AutoRestoreContext="False" AutoRestoreDataCache="True" 
                        AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" 
                        DataWindowObject="d_acc_start_accyear" 
                        LibraryList="~/DataWindow/account/cm_constant_config.pbl" 
                        ClientEventItemChanged="OnDwMainItemChange" 
                        ClientEventButtonClicked="Delete_DwmainClick" ClientFormatting="True" 
                        ClientEventClicked="OnDwmasterClick">
                    </dw:WebDataWindowControl>
                </td>
        </tr>
        <tr>
            <td>
                    <asp:HiddenField ID="HdIsFinished" runat="server" />
                    </td>
            <td>
                    <asp:HiddenField ID="HdDelrow" runat="server" />
                    </td>
            <td>
                    <asp:HiddenField ID="Hdrow" runat="server" />
                </td>
        </tr>
    </table>
    <p>
        &nbsp;</p>
</asp:Content>
