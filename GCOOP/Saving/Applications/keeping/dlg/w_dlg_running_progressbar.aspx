<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_running_progressbar.aspx.cs"
    Inherits="Saving.Applications.keeping.dlg.w_dlg_running_progressbar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ประมวลผล</title>

    <script src="../../../js/Ajax.js" type="text/javascript"></script>

    <style type="text/css">
        .blue
        {
            color: #FFF;
            background-color: #6A93D4;
            border-width: 1px;
        }
        .green
        {
            color: #FFF;
            background-color: #41A128;
            border-width: 1px;
        }
        div#progress_container
        {
            border: 6px double #ccc;
            width: 96%;
            margin-top: 15px;
            padding: 0;
        }
        div#progress
        {
            color: #FFF;
            background-color: #009999;
            height: 12px;
            padding-bottom: 2px;
            font-size: 12px;
            text-align: center;
            overflow: hidden;
            line-height: 1.2em;
        }
    </style>

    <script type="text/javascript">
    var iCount = 0;
    var statusProcess = 0;
    var ajaxUrl = "";
    
    function CompleteStatus() {
        Gcoop.GetEl("exitButton").onclick = function(){
            parent.Gcoop.ExtraFunction(Gcoop.GetEl("HdAjaxText").value);
        }
        Gcoop.GetEl("progress").style.width = "100%";
        Gcoop.GetEl("AjaxStatus").innerHTML = "ประมวลผลเสร็จสมบูรณ์";
        Gcoop.GetEl("exitButton").style.visibility = "visible";
        Gcoop.GetEl("exitButton").value = "กลับไปหน้าจอหลัก";
    }
    
    function ErrorStatus(errText){
        Gcoop.GetEl("progress").style.width = "100%";
        Gcoop.GetEl("progress").style.backgroundColor = "#FF0000";
        Gcoop.GetEl("AjaxStatus").innerHTML = "ประมวลผลเกิดข้อผิดผลาด: " + errText;
        Gcoop.GetEl("AjaxStatus").style.color = "#FF0000";
        Gcoop.GetEl("exitButton").style.visibility = "visible";
        Gcoop.GetEl("exitButton").value = "กลับไปหน้าจอหลัก";
    }
    
    function GetProgressData(text) {
        iCount++;
        try{
            text = Gcoop.Trim(text);
            var isException = false;
            try{
                var exceptionText = text.substring(0, 9);
                isException = exceptionText == "Exception";
            }catch(err_in){}
            if(isException){
                statusProcess = -1;
                ErrorStatus(text);
                return;
            }
            
            var arr = text.split(',');// Gcoop.Explode(",", text);
            if(arr[0] == 1){
                Gcoop.GetEl("HdAjaxText").value = text;
                Gcoop.GetEl("LbPercent").innerHTML = arr[4] + "/" + arr[3] + " &nbsp; 100%";
                statusProcess = 1;
                CompleteStatus();
            } else if(arr[0] == -1){
                statusProcess = -1;
                ErrorStatus(arr[6] + "<br /> " + arr[5]);
            } else {
                var maxProcess = Gcoop.ParseInt(arr[3]);
                var countProcess = Gcoop.ParseInt(arr[4]);
                var pc = Math.floor((countProcess / maxProcess)* 100);
                var dt3 = new Date();
                var displayTime = dt3.getHours() + ":" + dt3.getMinutes() + ":" + dt3.getSeconds();
                if( isNaN(pc) ){
                    Gcoop.GetEl("AjaxStatus").innerHTML = "Loading ... ";
                }else{
                    var subStatus = arr[7] == "" ? "" : "/" + arr[7];
                    statusProcess = Gcoop.ParseInt(arr[0]);
                    Gcoop.GetEl("progress").style.width = pc + "%";
                    Gcoop.GetEl("LbPercent").innerHTML = arr[4] + "/" + arr[3] + " &nbsp; " + pc + "%";
                    Gcoop.GetEl("AjaxStatus").innerHTML = arr[6] + subStatus;
                }
                var maxIndexProgress = Gcoop.ParseInt(arr[1]);
                var curIndexProgress = Gcoop.ParseInt(arr[2]);
                var totalCurrentIndex = "";
                if(maxIndexProgress > 1 && curIndexProgress >= 0){
                    totalCurrentIndex = curIndexProgress + "/" + maxIndexProgress + " ";
                }
                Gcoop.GetEl("LbDisplayTime").innerHTML = totalCurrentIndex + displayTime;
                setTimeout("CallAjax()", 1000);
            }
        }catch(eee){
            Gcoop.GetEl("progress").style.width = 0 + "%";
            Gcoop.GetEl("AjaxStatus").innerHTML = "ไม่มีสถานะการประมวลผล " + eee;
            Gcoop.GetEl("exitButton").style.visibility = "visible";
        }
    }
    
    function CallAjax() {
        try{
            if(statusProcess != 1 && statusProcess != -1){
                var currDate = new Date();
                Ajax.toPost(ajaxUrl + "&t2=" + currDate.getMinutes() + currDate.getSeconds(), "", GetProgressData);
            }
        }catch(ex){
            alert("Exception call Ajax urls >> " + ex);
        }
    }
    
    function DialogLoadComplete(){
        var currDate = new Date();
        Gcoop.GetEl("LbProgressName").innerHTML = "กำลัง" + parent.Gcoop.ProgressName + " ...";
        Gcoop.GetEl("exitButton").value = "หยุดการประมวลผล";
        if(parent.Gcoop.ExtraFunction == null){
            Gcoop.GetEl("exitButton").onclick = function(){
                parent.RemoveIFrame();
            }
        } else {
            Gcoop.GetEl("exitButton").onclick = function(){
                parent.Gcoop.ExtraFunction(Gcoop.GetEl("HdAjaxText").value);
            }
        }
        if(parent.Gcoop.DisplayProgressCloseButton){
            Gcoop.GetEl("exitButton").style.visibility = "visible";
        } else {
            Gcoop.GetEl("exitButton").style.visibility = "hidden";
        }
        var andQueryString = "&application="+ Gcoop.GetEl("HdProcessApplication").value +"&w_sheet_id=" + Gcoop.GetEl("HdProcessWebSheetId").value;
        ajaxUrl = parent.Gcoop.GetUrl() + "Applications/keeping/AjaxAllProcessing.aspx?t=" + currDate.getMinutes() + currDate.getSeconds() + andQueryString;
        setTimeout("CallAjax()", 3000);
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:Label ID="LbProgressName" runat="server" Text=""></asp:Label>
    <asp:Label ID="LbDisplayTime" runat="server" Font-Names="tahoma" Font-Size="9px"></asp:Label>
    <table width="100%">
        <tr>
            <td>
                <div id="progress_container">
                    <div id="progress" style="width: 0%">
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="LbPercent" runat="server" Font-Names="Tahoma" Font-Size="12px" ForeColor="#009999"
                    Font-Bold="True"></asp:Label>
            </td>
        </tr>
    </table>
    <br />
    <div align="center">
        <asp:Label ID="AjaxStatus" runat="server" ForeColor="#0099CC" Font-Size="14px" Font-Bold="False"></asp:Label>
        <br />
        <input id="exitButton" type="button" />
    </div>
    <asp:HiddenField ID="HdProcessApplication" runat="server" />
    <asp:HiddenField ID="HdProcessWebSheetId" runat="server" />
    <asp:HiddenField ID="HdAjaxText" runat="server" />
    </form>
</body>
</html>
