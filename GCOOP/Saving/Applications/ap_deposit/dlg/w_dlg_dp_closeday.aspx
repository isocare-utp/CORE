<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_dp_closeday.aspx.cs"
    Inherits="Saving.Applications.ap_deposit.dlg.w_dlg_dp_closeday" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ประมวลผลปิดสิ้นวัน</title>

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
    
    function CompleteStatus() {
        Gcoop.GetEl("progress").style.width = "100%";
        Gcoop.GetEl("AjaxStatus").innerHTML = "ประมวลผลเสร็จสมบูรณ์";
        Gcoop.GetEl("exitButton").style.visibility = "visible";
    }
    
    function ErrorStatus(errText){
        Gcoop.GetEl("progress").style.width = "100%";
        Gcoop.GetEl("progress").style.backgroundColor = "#FF0000";
        Gcoop.GetEl("AjaxStatus").innerHTML = "ประมวลผลเกิดข้อผิดผลาด: " + errText;
        Gcoop.GetEl("AjaxStatus").style.color = "#FF0000";
        Gcoop.GetEl("exitButton").style.visibility = "visible";
    }
    
    function GetProgressData(text) {
        iCount++;
        try{
            var arr = Gcoop.Explode(",", text);
            var maxProcess = Gcoop.ParseInt(arr[3]);
            var countProcess = Gcoop.ParseInt(arr[4]);
            var pc = (countProcess / maxProcess  )* 100;
            pc = Math.floor(pc);
            if(statusProcess != 1){
                if( isNaN(pc) ){
                    Gcoop.GetEl("AjaxStatus").innerHTML = "Loading ... ";
                }else{
                    statusProcess = Gcoop.ParseInt(arr[0]);
                    Gcoop.GetEl("progress").style.width = pc + "%";
                    Gcoop.GetEl("AjaxStatus").innerHTML = arr[6] + " " + arr[4] + "/" + arr[3] + " &nbsp; " + pc + "%";
                }
            }
            if(arr[0] == 1){
                statusProcess = 1;
                CompleteStatus();
            } else if(arr[0] == -1){
                statusProcess = -1;
                ErrorStatus(arr[6] + "<br /> " + arr[5]);
            } else {
                if(iCount < 4){
                    setTimeout("CallAjax()", 1000);
                } else {
                    setTimeout("CallAjax()", 1000);
                }
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
                var currDate = new Date()
                var urls = parent.Gcoop.GetUrl() + "Applications/ap_deposit/AjaxDpCloseDay.aspx?t=" + currDate.getMinutes() + currDate.getSeconds();
                Ajax.toPost(urls, "", GetProgressData);
            }
        }catch(ex){
            alert("Exception call Ajax urls >> " + ex);
        }
    }
    
    function DialogLoadComplete(){
        CallAjax();
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    การประมวลผลปิดสิ้นวัน ...
    <div id="progress_container">
        <div id="progress" style="width: 0%">
        </div>
    </div>
    <br />
    <div align="center">
        <asp:Label ID="AjaxStatus" runat="server" Text="" ForeColor="#0099CC" Font-Size="14px"
            Font-Bold="False"></asp:Label>
        <br />
        <input id="exitButton" type="button" value="กลับไปหน้าจอหลัก" style="visibility: hidden;"
            onclick="parent.CloseDayFinish()" />
    </div>
    <asp:HiddenField ID="HdIsLoop" Value="false" runat="server" />
    <asp:HiddenField ID="HdMaxLoop" runat="server" />
    <asp:HiddenField ID="HdCurrentLoop" runat="server" />
    <asp:HiddenField ID="HdCloseDate" runat="server" />
    </form>
</body>
</html>
