<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_acc_ucf_accperiod.aspx.cs"
    Inherits="Saving.Applications.account.w_acc_ucf_accperiod" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postGetAccyear%>
    <%=postCanDelPeriod%>
    <%=postDwPeriodInsertRow %>
    <%=postDeleteRow %>
    <%=postNewClear%>
    <%=postRefresh %>
    <script type="text/javascript">
    function MenubarNew(){
        if(confirm("ยืนยันการล้างข้อมูลบนหน้าจอ")){
            postNewClear();
        }
    }
    
    function OnDwPeriodClick(s,r,c)
    {
        if(c=="close_flag")
        {
            Gcoop.CheckDw(s, r, c, "close_flag", 1, 0);
           
        }
        return 0;
    }
    
    function OnDwAccyearClick(s, r, c){
        if (c == "close_account_stat")
        {
            Gcoop.CheckDw(s, r, c, "close_account_stat", 1, 0);
        }
        else if (c=="account_year_t" || c=="begin_tdate" || c=="end_tdate") 
        {
            var Accyear = objDw_accyear.GetItem(r, "account_year");
            Gcoop.GetEl("HdAccyear").value = Accyear;
            Gcoop.GetEl("Hdrow").value = r + "";
            postGetAccyear();
        }
        return 0;
    }

 // ฟังก์ชันการลบข้อมูล
   // ฟังก์ชันการลบข้อมูล
     function Delete_DwPeriodClick(sender, row, bName) {
         if (bName == "b_del") { 
            var CheckText = objDw_accperiod.GetItem(row,"period");
            var period_end_tdate = objDw_accperiod.GetItem(row,"period_end_tdate");
            var account_year = objDw_accperiod.GetItem(row,"account_year");
            var period_prev = objDw_accperiod.GetItem(row,"period_prev");
            if (CheckText == null || CheckText == "" || period_end_tdate == null || account_year == null || period_prev == null){
                   Gcoop.GetEl("HdRowPeriod").value = row + "";
                   postDeleteRow();
                    
                   }else {
                            var period = objDw_accperiod.GetItem(row,"period");
                            var isConfirm = confirm("ต้องการลบข้อมูลงวดบัญชี งวดที่ : " + period + "ใช่หรือไม่ ?");
                             if (isConfirm) {
                                 try {
                                      var Accperiod = objDw_accperiod.GetItem(row, "period");    
                                      Gcoop.GetEl("HdRowPeriod").value = row ;
                                      postCanDelPeriod();
                       }catch (err)
                       {alert (err);}      
                    }
                 
                 }
             
         }
         return 0;
     }
     //เพิ่มแถว
     function B_Dw_periodInsert() {
        postDwPeriodInsertRow();  
     }
     
     function OnDwPeriodItemChange(s,r,c,v)//เปลี่ยนวันที่
    {
        if (c == "period_end_tdate") {
            objDw_accperiod.SetItem(r, "period_end_tdate", v);
            objDw_accperiod.AcceptText();
            objDw_accperiod.SetItem(r, "period_end_date", Gcoop.ToEngDate(v));
            objDw_accperiod.AcceptText();
            postRefresh();
        }
        else if (c == "acc_year_prev_t") 
        {
            s.SetItem(r,"acc_year_prev_t",v);
            s.AcceptText();
            s.SetItem(r, "account_year_prev", v - 543);
            s.AcceptText();
        }
        return 0;  
    }
    
    function Validate()
    {
     try{
            var isconfirm = confirm("ยืนยันการบันทึกการข้อมูล?");
            
            if (!isconfirm )
            {
                return false;        
            }
            var Rowlast = objDw_accperiod.RowCount();
//            if (Rowlast > "12"){
//                alert("ไม่สามารถบันทึกข้อมูลได้เนื่องจากงวดบัญชีในรอบปีมี 12 งวดเท่านั้น")
//            }else{
            var alertstr = "";
            var period = objDw_accperiod.GetItem(Rowlast,"period");
            var period_end_tdate = objDw_accperiod.GetItem(Rowlast,"period_end_tdate");
            var account_year = objDw_accperiod.GetItem(Rowlast,"account_year");
            var period_prev =  objDw_accperiod.GetItem(Rowlast,"period_prev");
            
//            if (period == "" || period == null){
//                alertstr = alertstr + "_กรุณากรอกงวดบัญชี\n";
//            }
//            if (period_end_tdate == "" || period_end_tdate == null){
//                alertstr = alertstr+ "_กรุณากรอกวันที่สิ้นงวด\n";
//            }
//            if (account_year == "" || account_year == null){
//                alertstr = alertstr+ "_กรุณากรอกปีบัญชี\n";
//            }
//            if(period_prev == "" || period_prev == null){
//                alertstr = alertstr+ "_กรุณากรอกงวดที่ยกมา\n"
//            }
            
            if (alertstr == "")
            {
                return true;
            }
            else 
            {
                alert(alertstr);
                return false;
            }
        }
           
        catch (err)
        {
            alert (err); 
            return false;
        }
    }
       
    </script>

    <style type="text/css">
        .style2
        {
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
  <p>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <table style="width: 100%; font-size: small;">
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td>
                <span class="linkSpan" onclick="B_Dw_periodInsert()" 
                    
                    style="font-family: Tahoma; font-size: small; float: left; color: #0000CC;">เพิ่มแถวงวดบัญชี</span></td>
            </tr>
            <tr>
                <td class="style2">
                    ปีบัญชี
                </td>
                <td>
                    <span class="style2">งวดบัญชีในรอบปี</span>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:Panel ID="Panel1" runat="server" BorderStyle="Ridge" ScrollBars="Auto" Height="400px">
                        <dw:WebDataWindowControl ID="Dw_accyear" runat="server" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" 
    AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                        DataWindowObject="d_acc_start_accyear_p" LibraryList="~/DataWindow/account/cm_constant_config.pbl"
                        ClientEventClicked="OnDwAccyearClick" 
    ClientFormatting="True">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
                <td valign="top">
                    <asp:Panel ID="Panel2" runat="server" BorderStyle="Ridge" ScrollBars="Auto" 
                        Height="400px" Width="380px">
                        <dw:WebDataWindowControl ID="Dw_accperiod" runat="server" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" 
    AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                        DataWindowObject="d_acc_start_period" LibraryList="~/DataWindow/account/cm_constant_config.pbl"
                        ClientEventButtonClicked="Delete_DwPeriodClick" 
                        ClientEventItemChanged="OnDwPeriodItemChange" ClientFormatting="True" 
                        ClientEventClicked="OnDwPeriodClick">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="HdAccyear" runat="server" />
                    <asp:HiddenField ID="HdIsFinished" runat="server" />
                </td>
                <td valign="top">
                    <br />
                    <asp:HiddenField ID="HdRowPeriod" runat="server" />
                    <asp:HiddenField ID="Hdrow" runat="server" />
                    <br />
                </td>
            </tr>
        </table>
        <br />
    </asp:Content>
