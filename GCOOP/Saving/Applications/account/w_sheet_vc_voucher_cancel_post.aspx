<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_vc_voucher_cancel_post.aspx.cs"
    Inherits="Saving.Applications.account.w_sheet_vc_voucher_cancel_post" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postCancelJournal%>
    <%=postSaveCancelJournal%>
    <%=postNewClear%>
  
 
 
<script type = "text/javascript" >

 function OnDwmasterItemChange(s,r,c,v)//เปลี่ยนวันที่
    {
        if (v == "" || v == null){
            alert("กรุณากรอกข้อมูลวันที่ Voucher");
        }else {
            if(c == "journal_d_tdate"){
                s.SetItem(1, "journal_d_tdate", v );
                s.AcceptText();
                s.SetItem(1, "journal_d", Gcoop.ToEngDate(v));
                s.AcceptText();
            }else if(c== "to_jour_tdate"){
                s.SetItem(1, "to_jour_tdate", v );
                s.AcceptText();
                s.SetItem(1, "to_jour_date", Gcoop.ToEngDate(v));
                s.AcceptText();
            }
        }          
        return 0;  
    }
    
    function OnDwListClick(s,r,c)
    {
        if (c == "posttoacc_flag"){
            Gcoop.CheckDw(s, r, c, "posttoacc_flag", 1, 0);
        }
        return 0
    }
    
    
  //คลิกปุ่ม ผ่านรายการ
  function B_CancelPostOnClicked() {
    var journal_date = "";
    var to_jour_date = "";
    journal_date = objDw_master.GetItem(1,"journal_d_tdate");// ส่งชื่อฟิวส์วันที่ไทยเข้าไป
    to_jour_date = objDw_master.GetItem(1,"to_jour_tdate");
      
    if (journal_date == "" || journal_date == null || to_jour_date == "" || to_jour_date == null){
        alert ("กรุณากรอกข้อมูลวันที่ให้ครบ")  
    }else{
      
        postCancelJournal();     
    }    
        return false;
 }  
    
    //ฟังก์ชันการเช็คค่าข้อมูลก่อน save
    function Validate() {
        try{
            var isconfirm = confirm("ยืนยันการยกเลิกผ่านรายการไปแยกประเภท ?");
            
            if (!isconfirm ){
                return false;        
            }
            
            var alertstr = "";
            var journal_date = "";
            var to_jour_tdate = "";
            journal_date = objDw_master.GetItem(1,"journal_d_tdate"); 
            to_jour_tdate =  objDw_master.GetItem(1,"to_jour_tdate");
            
            if (journal_date == "" || journal_date == null || to_jour_tdate == "" || to_jour_tdate == null){
                alertstr = alertstr + "_กรุณากรอกวันที่ให้ครบ\n";
            }
            if (alertstr == "")
            {
                return true;            
            }
            else {
                alert(alertstr);
                return false;
            }
        }catch (err){
            alert (err); 
            return false;
        }
    }
    
    function MenubarNew(){
        if(confirm("ยืนยันการล้างข้อมูลบนหน้าจอ")){
            postNewClear();
        }
    }
    
    
</script>
    <style type="text/css">
        .style1
        {
        }
        .style2
        {
            width: 573px;
        }
        .style4
        {
            font-size: small;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <p>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <table style="width: 100%;">
            <tr>
                <td class="style4">
                    รายการประจำวัน
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Panel ID="Panel1" runat="server">
                        <dw:WebDataWindowControl ID="Dw_master" runat="server" 
                            AutoRestoreContext="False" AutoRestoreDataCache="True" 
                            AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" 
                            ClientScriptable="True" DataWindowObject="d_acc_accjournalmjmaster" 
                            LibraryList="~/DataWindow/account/journalmaster_br.pbl">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
                <td>
                    <input id="B_nopost" type="button" value="ดึงข้อมูล" onclick="B_CancelPostOnClicked()" />
                </td>
            </tr>
            <tr>
                <td class="style4">
                    รายการ voucher
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" 
                        oncheckedchanged="CheckBox1_CheckedChanged" style="font-size: small" 
                        Text="ยกเลิกทั้งหมด" />
                </td>
            </tr>
            <tr>
                <td colspan="3" valign="top">
                    <asp:Panel ID="Panel2" runat="server" Height="399px" ScrollBars="Vertical" 
                        BorderStyle="Ridge">
                        <dw:WebDataWindowControl ID="Dw_list" runat="server" DataWindowObject="d_vc_vcedit_vclist_cancel_post"
                            LibraryList="~/DataWindow/account/vc_voucher_cancel_post.pbl" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" 
                            ClientScriptable="True" ClientEventClicked="OnDwListClick">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <br />
        <br />
        <table style="width: 100%;">
            <tr>
                <td class="style1" colspan="3">
                    <table style="width: 100%;">
                        <tr>
                            <td class="style2">
                                &nbsp;
                            </td>
                            <td align="center">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <br />
                </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;
                    <asp:HiddenField ID="HdIsFinished" runat="server" />
                    <asp:HiddenField ID="HdCheckValue" runat="server" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </p>
</asp:Content>
