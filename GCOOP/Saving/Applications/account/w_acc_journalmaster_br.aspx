<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_acc_journalmaster_br.aspx.cs"
    Inherits="Saving.Applications.account.w_acc_journalmaster_br" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
    <%=postSaveJournal%>
    <%=postNewClear %>
    <%=postJournal%>

    <script type="text/javascript">
  
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
  function B_PostOnClicked() {
    var journal_date = "";
    var to_jour_date = "";
    journal_date = objDw_master.GetItem(1,"journal_d_tdate");// ส่งชื่อฟิวส์วันที่ไทยเข้าไป
    to_jour_date = objDw_master.GetItem(1,"to_jour_tdate");
    var mm1 = journal_date.substring(2, 4);
    var mm2 = to_jour_date.substring(2, 4);
    var yy1 = journal_date.substring(4, 8);
    var yy2 = to_jour_date.substring(4, 8);

    if (journal_date == "" || journal_date == null || to_jour_date == "" || to_jour_date == null || mm1 != mm2 || yy1 != yy2) {
        alert("ไม่สามารถผ่านรายการข้ามเดือนได้ กรุณาระบุวันที่ให้ถูกต้อง")
    }else{
      
        postJournal();     
    }    
        return false;
 }  
    
    //ฟังก์ชันการเช็คค่าข้อมูลก่อน save
    function Validate() {
        try{
            var isconfirm = confirm("ยืนยันการผ่านรายการไปแยกประเภท ?");
            
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
        .style4
        {
        }
        .style5
        {
            width: 611px;
            font-weight: bold;
        }
        .style6
        {
            height: 498px;
        }
        .style7
        {
            font-size: small;
            font-weight: bold;
        }
        .style8
        {
            font-size: small;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <p>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <table style="width: 100%;">
            <tr>
                <td class="style4">
                    <span class="style7">รายการประจำวัน</span>
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
                    <asp:Panel ID="Panel1" runat="server" Width="651px">
                        <dw:WebDataWindowControl ID="Dw_master" runat="server" DataWindowObject="d_acc_accjournalmjmaster"
                            LibraryList="~/DataWindow/account/journalmaster_br.pbl" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                            ClientEventItemChanged="OnDwmasterItemChange" ClientFormatting="True">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
                <td>
                    <input id="b_post" type="button" value="ดึงข้อมูล" onclick="B_PostOnClicked()" />
                </td>
            </tr>
            <tr>
                <td class="style5">
                    <span class="style8">รายการVoucher</span>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style4" colspan="3">
                    <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" 
                        oncheckedchanged="CheckBox1_CheckedChanged" style="font-size: small" 
                        Text="เลือกทั้งหมด" />
                </td>
            </tr>
            <tr>
                <td class="style6" colspan="3" valign="top">
                    <asp:Panel ID="Panel2" runat="server" Height="400px" ScrollBars="Vertical" 
                        BorderStyle="Ridge">
                        <dw:WebDataWindowControl ID="Dw_list" runat="server" DataWindowObject="d_vc_vcedit_vclist_post"
                            LibraryList="~/DataWindow/account/journalmaster_br.pbl" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" 
                            ClientScriptable="True" ClientEventClicked="OnDwListClick">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td class="style4">
                    &nbsp;<asp:HiddenField ID="HdIsFinished" runat="server" />
                </td>
                <td>
                    &nbsp;<asp:HiddenField ID="HdCheckValue" runat="server" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <br />
    </p>
</asp:Content>
