<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_RunPostEmrcont.aspx.cs"
    Inherits="Saving.Applications.keeping.dlg.w_dlg_RunPostEmrcont" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>สถานะประมวลผลจัดเก็บฉุกเฉินโอน</title>

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
            width: 200px;
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
            //Gcoop.GetEl("AjaxStatus").innerHTML = arr[6] + "<br />" + "pc=" +pc + " count=" + iCount + "::::::::" + text;
            if(statusProcess!=1){
                if( isNaN(pc) ){
                    Gcoop.GetEl("AjaxStatus").innerHTML = "Loading ... ";
                }else{
                    statusProcess = Gcoop.ParseInt(arr[0]);
                    Gcoop.GetEl("progress").style.width = pc + "%";
                    Gcoop.GetEl("AjaxStatus").innerHTML = arr[6] + " "  + pc + "%";
                }
            }
            if(arr[0] == 1){
                statusProcess = 1;
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
            var urls = opener.Gcoop.GetUrl() + "Applications/keeping/AjaxKpPostEmrcont.aspx"; 
            Ajax.toPost(urls, "", GetProgressData);
            if(statusProcess!=1){
                setTimeout("CallAjax()", 1500);
            }
            if(iCount < 4){
                setTimeout("CallAjax()", 2500);
            }
        }catch(ex){
            //alert("Exception call Ajax urls >> ");
        }
    }
    </script>

</head>
<body onload="CallAjax();">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    การประมวลผลจัดเก็บฉุกเฉินโอน ...
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
