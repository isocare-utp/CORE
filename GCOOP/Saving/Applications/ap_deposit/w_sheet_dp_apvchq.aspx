<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_dp_apvchq.aspx.cs" Inherits="Saving.Applications.ap_deposit.w_sheet_dp_apvchq" Title="Untitled Page" %>
<%@ Register assembly="WebDataWindow" namespace="Sybase.DataWindow.Web" tagprefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%=initJavaScript%>
<%=postCheckList%>
<%=postCheckStatus%>
<%=postCheckDetail%>
<%=RejectDate%>
<script type ="text/javascript" >
    function OnDw_dateButtonClick(s,r,c){
        if(c == "b_post")
        {
            postCheckList();
        }
    }
    
    function OnDw_listClick(s,r,c)
    {
        if(c == "compute_1" || c == "check_no" || c == "compute_2" || c == "compute_3" || c == "cheque_amt")
        {    
            Gcoop.GetEl("Hd_RowClick").value = r + "";
            postCheckDetail();
        }
        return 0;
    }
    
    function OnDWDateItemChange(s,r,c,v)//เปลี่ยนวันที่
    {
        if (v == "" || v == null){
            alert("กรุณากรอกข้อมูลวันที่ Voucher")
        }else{
            s.SetItem(1, "start_tdate", v );
            s.AcceptText();
            s.SetItem(1, "start_date", Gcoop.ToEngDate(v));
            s.AcceptText();
        }          
        return 0;
    }
    
    function OnDWListItemChange(s,r,c,v)//เปลี่ยนวันที่
    {
        if(c == "checkclear_status")
        {
            s.SetItem(r,c,v);
            s.AcceptText();
            Gcoop.GetEl("Hd_RowCheckStatus").value = r + "";
            postCheckStatus();
        }
        
        else if (c == "p_r_tdate")
        {
            objDw_list.SetItem(r,c,v);
            var InputDate = Gcoop.ToEngDate(v);
            var workDate = Gcoop.GetEl("HdWorkDate").value;
            var entryDate = Gcoop.GetEl("HdEntryDate").value;
            var resultWork = DiffDateCheck(workDate, InputDate);
            var resultEntry = DiffDateCheck(InputDate, entryDate);
            
            if( resultWork == "1" && resultEntry == "1"){
                objDw_list.SetItem(r,"p_r_date",Gcoop.ToEngDate(v));
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
        
    function Validate()
    {
      return confirm("ต้องการบันทึกข้อมูลหรือไม่");
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
            result = "0";
        }else if(monthInput > monthWork){
            result = "0";
        } else if(dayInput > dayWork){
            result = "0";
        }
        return result == "1";
    }
</script> 
    <style type="text/css">
        .style2
        {
            font-size: small;
            font-weight: bold;
        }
        .style5
        {
            font-size: small;
            font-weight: bold;
            height: 259px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <table style="width:100%;">
        <tr>
            <td class="style2">
                แสดงรายละเอียดข้อมูลวันที่</td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="Panel3" runat="server" Height="38px">
                    <dw:WebDataWindowControl ID="Dw_date" runat="server" AutoRestoreContext="False" 
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" 
                        ClientFormatting="True" ClientScriptable="True" 
                        DataWindowObject="d_dp_approve_chqdate" 
                        LibraryList="~/DataWindow/ap_deposit/dp_apvchq.pbl" 
                        ClientEventButtonClicked="OnDw_dateButtonClick" 
                        ClientEventItemChanged="OnDWDateItemChange">
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="style2">
                รายการ</td>
        </tr>
        <tr>
            <td class="style5">
                <asp:Panel ID="Panel4" runat="server" Height="255px" ScrollBars="Vertical" 
                    BorderStyle="Ridge">
                    <dw:WebDataWindowControl ID="Dw_list" runat="server" 
                        DataWindowObject="d_dp_apvchq_list" 
                        LibraryList="~/DataWindow/ap_deposit/dp_apvchq.pbl" 
                        AutoRestoreContext="False" AutoRestoreDataCache="True" 
                        AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" 
                        ClientScriptable="True" ClientEventClicked="OnDw_listClick" 
                        ClientEventItemChanged="OnDWListItemChange">
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="style2">
                รายละเอียด</td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Panel ID="Panel5" runat="server" Height="85px">
                    <dw:WebDataWindowControl ID="Dw_detail" runat="server" 
                        DataWindowObject="d_dp_apvchq_detail" 
                        LibraryList="~/DataWindow/ap_deposit/dp_apvchq.pbl">
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:HiddenField ID="Hd_RowCheckStatus" runat="server" />
                <asp:HiddenField ID="Hd_RowClick" runat="server" />
                <asp:HiddenField ID="HdWorkDate" runat="server" />
                <asp:HiddenField ID="HdEntryDate" runat="server" />
                <asp:HiddenField ID="HdWorkDate2" runat="server" />
            </td>
        </tr>
    </table>
    <br />
</asp:Content>
