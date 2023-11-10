<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_RunShgiftProcess_progressbar.aspx.cs" Inherits="Saving.Applications.shrlon.dlg.w_dlg_RunShgiftProcess_progressbar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
   <title>ผลการประมวลหุ้น โบนัสหุ้น(หุ้นของขวัญ)</title>

    <script src="../../../js/Ajax.js" type="text/javascript"></script>
    <script src="../../../js/Gcoop.js" type="text/javascript"></script>

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
            width: 90%;
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
    function CompleteStatus()
    {
        Gcoop.GetEl("progress").style.width = "100%";
        Gcoop.GetEl("AjaxStatus").innerHTML = "ประมวลผลเสร็จสมบูรณ์";
    }
    
    function GetProgressData(text)
    {
        iCount++;
        try{
            var arr = Gcoop.Explode(",", text);
            var maxProcess = Gcoop.ParseInt(arr[3]);
            var countProcess = Gcoop.ParseInt(arr[4]);
            var pc = (countProcess/ maxProcess  )* 100;
            pc = Math.floor(pc);
            
            Gcoop.GetEl("AjaxStatus").innerHTML = "text : " + text+ " percent : " + pc;//arr[6] + "<br />" + "pc=" +pc + " count=" + iCount + "::::::::" + text;
            //alert("text : " + text+" percent : "+pc);
            if(statusProcess!=1){
                if( isNaN(pc) ){
                    Gcoop.GetEl("AjaxStatus").innerHTML = "Loading ... "+ "Text ";
                }else{
                    statusProcess = Gcoop.ParseInt(arr[0]);
                    Gcoop.GetEl("progress").style.width = pc + "%";
                    Gcoop.GetEl("AjaxStatus").innerHTML = arr[6] + " "  + pc + "%";
                }
            }
            if(arr[0] == 1){
                statusProcess = 1;
                //alert("statuscomplete : "+statusProcess);
                CompleteStatus();
            }
        }catch(eee){
                Gcoop.GetEl("progress").style.width = 0 + "%";
                Gcoop.GetEl("AjaxStatus").innerHTML = "ไม่มีสถานะการประมวลผล";
        }
    }
    
    function CallAjax()
    {
        try{
            var dt1 = new Date();
            var queryStr1 = "?t=" + dt1.getMinutes() + "" + dt1.getSeconds();

            var urls = opener.Gcoop.GetUrl() + "Applications/shrlon/AjaxSlShrgiftProcess.aspx" + queryStr1; 
            Ajax.toPost(urls, "", GetProgressData);
            if(statusProcess!=1){
                setTimeout("CallAjax()", 1500);
            }
            if(iCount < 4){
                setTimeout("CallAjax()", 2500);
            }
        }catch(ex){
                Gcoop.GetEl("progress").style.width = 0 + "%";
                Gcoop.GetEl("AjaxStatus").innerHTML = "การประมวลผลไม่สมบูรณ์: " + ex;
        }
    }
    
    function DialogLoadComplete(){
        //alert("55555++");
        CallAjax();
    }
    </script>
</head>
<body onload="DialogLoadComplete()">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    การประมวลผลหุ้น โบนัสหุ้น(หุ้นของขวัญ) ...
    <div id="progress_container">
        <div id="progress" style="width: 0%">
        </div>
    </div>
    <br />
    <div align="center">
        <asp:Label ID="AjaxStatus" runat="server" Text="" ForeColor="#0099CC" Font-Size="14px"
            Font-Bold="False"></asp:Label>
    </div>
</body>
</html>
