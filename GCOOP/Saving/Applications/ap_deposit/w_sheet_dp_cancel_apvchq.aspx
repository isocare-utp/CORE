<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_dp_cancel_apvchq.aspx.cs"
    Inherits="Saving.Applications.ap_deposit.w_sheet_dp_cancel_apvchq" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript %>
    <%=postCancelPastChq%>
    <%=postSaveCancelPastChq%>
    <%=postCheckStatus%>
    <%=postCheckDetail%>
    <%=RejectDate%>
    <script type="text/javascript">
    
    function OnDw_dateClick(s,r,c)
    {
        if(c == "b_post")
        {
         postCancelPastChq();
        }
        return  0;
    }
    
    function OnDw_dateItemChange(s,r,c,v)//เปลี่ยนวันที่
    {
        if (c=="start_tdate")
        {
            s.SetItem(1, "start_tdate", v );
            s.AcceptText();
            s.SetItem(1, "start_date", Gcoop.ToEngDate(v));
            s.AcceptText();
        }
    }
    
    function OnDw_listClick(s,r,c)
    {  
        if (c == "checkclear_status") 
        {
           Gcoop.CheckDw(s, r, c, "checkclear_status", -9, 0);
           var RowDw_list = r;
           Gcoop.GetEl("HdRowDw_list").value = RowDw_list;
           Gcoop.GetEl("Hdaiflag").value = objDw_list.GetItem(r,c) + "";
           postCheckStatus();
        } 
        
        else if(c == "compute_1" || c == "check_no" || c == "compute_2" || c == "compute_3" || c == "cheque_amt")
        {    
            Gcoop.GetEl("Hd_RowClick").value = r + "";
            postCheckDetail();
        }
        return 0;
    }
    
    function OnDwListItemChanged(sender, rowNumber, colunmName, newValue){
        if (colunmName == "p_r_tdate"){
            objDw_list.SetItem(rowNumber,colunmName,newValue);
            var InputDate = Gcoop.ToEngDate(newValue);
            var workDate = Gcoop.GetEl("HdWorkDate").value;
            var entryDate = Gcoop.GetEl("HdEntryDate").value;
            var resultWork = DiffDateCheck(workDate, InputDate);
            var resultEntry = DiffDateCheck(InputDate, entryDate);
            
            if( resultWork == "1" && resultEntry == "1"){
                objDw_list.SetItem(rowNumber,"p_r_date",Gcoop.ToEngDate(newValue));
                objDw_list.AcceptText();
            }
            else if(resultWork == "0"){
                alert("ไม่อนุญาติให้เกินวันทำการ");      
                RejectDate();
            }            
            else if(resultEntry == "0"){
                alert("ไม่อนุญาติให้น้อยกว่าวันที่นำส่ง");      
                RejectDate();   
            }
        }
        return 0;
    }
    
    function Validate(){
        var alertstr = "";
        var start_tdate = objDw_date.GetItem(1,"start_tdate"); 
        var ai_flag = objDw_date.GetItem(1,"ai_flag");
        var Season = "";
        try {
            var row = Gcoop.GetEl("HdRowDw_list").value;
            Season = objDw_list.GetItem(Gcoop.ParseInt(row), "return_season");
        }catch(Err){Season = "";}
        
        if (start_tdate == "" || start_tdate == null ){
            alertstr = alertstr + "_กรุณากรอกวันที่ \n";
        }
        if (ai_flag == "" || ai_flag == null){
            alertstr = alertstr + "_กรุณาเลือกสถานะเช็ค \n";
        }
        if (Season == "" || Season == null){
            alertstr = alertstr + "กรุณาเลือกเหตุผลคืนเช็ค";
        }
        if (alertstr == "")
        {
            return confirm("ต้องการบันทึกข้อมูลหรือไม่");
        }
        else {
            alert(alertstr);
            return false;
        }
    }
    
    function DiffDateCheck(workDate, inputDate){
        var result = "1";
        var arrWork = new Array();
        var arrInpur = new Array();
        arrWork = workDate.split('-');
        arrInput = inputDate.split('-');
        var yearWork = Gcoop.ParseInt(arrWork[0]);
        var monthWork = Gcoop.ParseInt(arrWork[1]);
        var dayWork = Gcoop.ParseInt(arrWork[2]);
        
        var yearInput = Gcoop.ParseInt(arrInput[0]);
        var monthInput = Gcoop.ParseInt(arrInput[1]);
        var dayInput = Gcoop.ParseInt(arrInput[2]);
 
        if(yearInput > yearWork){
            result = "Y = flase";
        }else if(monthInput > monthWork){
            result = "M = false";
        } else if(dayInput > dayWork){
            result = "D = false";
        }
        return result == "1";
    }
    
    </script>

    <style type="text/css">
        .style2
        {
            font-weight: bold;
        }
        .style3
        {
            width: 2px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <p>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <table style="width: 100%; font-size: small;">
            <tr>
                <td class="style2">
                    แสดงรายละเอียดข้อมูลวันที่
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <dw:WebDataWindowControl ID="Dw_date" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                        AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_dp_approve_cancel_chqdate"
                        LibraryList="~/DataWindow/ap_deposit/dp_apvchq.pbl" ClientEventItemChanged="OnDw_dateItemChange"
                        ClientFormatting="True" ClientEventButtonClicked="OnDw_dateClick">
                    </dw:WebDataWindowControl>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    รายการ
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Panel ID="Panel1" runat="server" BorderStyle="Ridge" Height="271px">
                        <dw:WebDataWindowControl ID="Dw_list" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                            AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" DataWindowObject="d_dp_cancel_apvchq_list"
                            LibraryList="~/DataWindow/ap_deposit/dp_apvchq.pbl" ClientEventClicked="OnDw_listClick"
                            ClientFormatting="True" ClientEventItemChanged="OnDwListItemChanged">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    รายละเอียด
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Panel ID="Panel5" runat="server" Height="85px">
                        <dw:WebDataWindowControl ID="Dw_detail" runat="server" DataWindowObject="d_dp_apvchq_detail"
                            LibraryList="~/DataWindow/ap_deposit/dp_apvchq.pbl">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    &nbsp;<asp:HiddenField ID="Hd_dwdate" runat="server" />
                    <asp:HiddenField ID="HdIsFinished" runat="server" />
                    <asp:HiddenField ID="Hdaiflag" runat="server" />
                    <asp:HiddenField ID="HdRowDw_list" runat="server" />
                    <asp:HiddenField ID="Hd_RowClick" runat="server" />
                    <asp:HiddenField ID="HdWorkDate" runat="server" />
                    <asp:HiddenField ID="HdEntryDate" runat="server" />
                </td>
            </tr>
        </table>
    </p>
</asp:Content>
