<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_dp_cancle_deptedit.aspx.cs"
    Inherits="Saving.Applications.ap_deposit.w_sheet_dp_cancle_deptedit" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postDeptAccountNo%>
    <%=postCancelDetail%>
    <%=postSaveNoCheckApv%>
    <%=RejectDate%>
    <script type="text/javascript"> 
    
    function GetValueCheckApv(valueCheckApv,nameApv){
        
        if(valueCheckApv == "success"){
            var row = Gcoop.GetEl("HdDwDetailRow").value;
            objDwDetail.SetItem(Gcoop.ParseInt(row), "authorize_id", nameApv);
            objDwDetail.AcceptText();
            postSaveNoCheckApv();
        }
        else{
            Gcoop.RemoveIFrame();
        }
    }
    
    function OnDwDetailClick(s, r, c){
        for(i = 1; i <= s.RowCount(); i++){
            s.SelectRow(i, false);
        }
        s.SelectRow(r, true);
        s.SetRow(r);
        Gcoop.GetEl("HdDwDetailRow").value = r + "";
    }
    
    function OnDwMasterClick(s, r, c){
        //----------------------------
    }

    function OnDwMasterItemChanged(s, r, c, v){
        if(c == "deptaccount_no"){
            //ctl00_ContentPlace_HfDwDetailRow
            objDwMaster.SetItem(r, c, v);
            objDwMaster.AcceptText();
            postDeptAccountNo();
        }
        else if (c == "work_tdate"){
            objDwMaster.SetItem(r, c, v);
            var inputDate = Gcoop.ToEngDate(v);
            var workDate = Gcoop.GetEl("HdWorkDate").value;
            var result = DiffDateCheck(workDate,inputDate);
            if(result == "1"){
                objDwMaster.SetItem(r, "work_date" , Gcoop.ToEngDate(v));
                objDwMaster.AcceptText();
                postCancelDetail();
            }
            else
            {
                alert("กรุณากรอกวันทำรายการใหม่");
                RejectDate();
            }
        }
        return 0;
    }
    
    function SheetLoadComplete(){
        if(Gcoop.GetEl("HdCheckApvAlert").value == "true"){
            var processId = Gcoop.GetEl("HdProcessId").value;
            var itemType = Gcoop.GetEl("HdItemType").value;
            var avpCode = Gcoop.GetEl("HdAvpCode").value;
            var avpAmt = Gcoop.GetEl("HdAvpAmt").value;
            Gcoop.OpenIFrame(240, 170, "w_iframe_dp_addapv_task.aspx", "?processId=" + processId + "&avpCode=" + avpCode + "&itemType=" + itemType + "&avpAmt=" + avpAmt);       
        }
    }
    
    function Validate(){
        var r = Gcoop.ParseInt(Gcoop.GetEl("HdDwDetailRow").value);
        return confirm("ยืนยันการย้อนใบรายการเลขที่ " + objDwDetail.GetItem(r, "deptslip_no"));
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

        if (inputDate > workDate) {
            return result == "false";
        }
       
       // if (yearInput > yearWork) {
         //   result = "Y = flase";
       // }else if(monthInput > monthWork){
        //    result = "M = false";
       // } else if(dayInput > dayWork){
      //      result = "D = false";
        // }

        return result == "1";
    }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <dw:WebDataWindowControl ID="DwMaster" runat="server" ClientScriptable="True" AutoSaveDataCacheAfterRetrieve="True"
        AutoRestoreDataCache="True" ClientEventItemChanged="OnDwMasterItemChanged" AutoRestoreContext="False"
        ClientEventClicked="OnDwMasterClick" DataWindowObject="d_dept_cancle_master"
        LibraryList="~/DataWindow/ap_deposit/dp_cancle_deptedit.pbl" 
        ClientFormatting="True">
    </dw:WebDataWindowControl>
    <br />
    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="12px"
        Text="รายละเอียดรายการ"></asp:Label>
    <br />
    <dw:WebDataWindowControl ID="DwDetail" runat="server" ClientScriptable="True" AutoSaveDataCacheAfterRetrieve="True"
        AutoRestoreDataCache="True" AutoRestoreContext="False" ClientEventRowFocusChanged="OnDwDetailRowChanged"
        DataWindowObject="d_dp_cancel_detail_new" LibraryList="~/DataWindow/ap_deposit/dp_cancle_deptedit.pbl"
        ClientEventClicked="OnDwDetailClick" ClientFormatting="True">
    </dw:WebDataWindowControl>
    <asp:HiddenField ID="HdDwDetailRow" runat="server" />
    <asp:HiddenField ID="HdCheckApvAlert" runat="server" />
    <asp:HiddenField ID="HdProcessId" runat="server" />
    <asp:HiddenField ID="HdAvpCode" runat="server" />
    <asp:HiddenField ID="HdItemType" runat="server" />
    <asp:HiddenField ID="HdAvpAmt" runat="server" />
    <asp:HiddenField ID="HdCheckApv" runat="server" />
    <asp:HiddenField ID="HdWorkDate" runat="server" />
    <asp:HiddenField ID="Hdas_apvdoc" runat="server" />
</asp:Content>
