<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_principal_balance_progress.aspx.cs"
    Inherits="Saving.Applications.keeping.dlg.w_dlg_principal_balance_progress" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ผลการประมวลผลหุ้นหนี้คงเหลือจาก statement</title>

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
            width: 200px;
            margin-top: 15px;
            padding: 0;
        }
        div#mprogress
        {
            color: #FFF;
            background-color: #267BCA;
            height: 12px;
            padding-bottom: 2px;
            font-size: 12px;
            text-align: center;
            overflow: hidden;
            line-height: 1.2em;
        }
        div#progress_container2
        {
            border: 6px double #ccc;
            width: 200px;
            margin-top: 15px;
            padding: 0;
        }
        div#sprogress
        {
            color: #FFF;
            background-color: #267BCA;
            height: 12px;
            padding-bottom: 2px;
            font-size: 12px;
            text-align: center;
            overflow: hidden;
            line-height: 1.2em;
        }
    </style>

    <script type="text/javascript">
    var lastStatus = 0;
    
    function ChangeTextColor(el,color){
        var tmp = Gcoop.GetEl(el).innerHTML;
        Gcoop.GetEl(el).innerHTML = '<font color="'+color+'">' + tmp + '</font>';
    }
    
    function GetProgressData(text)
    {
        /**
        str[0] = p.status.ToString();
        str[1] = p.progress_max.ToString();
        str[2] = p.progress_index.ToString();
        str[3] = p.subprogress_max.ToString();
        str[4] = p.subprogress_index.ToString();
        str[5] = p.error_text;
        str[6] = p.progress_text;
        str[7] = p.subprogress_text;
        **/
        try{
            text = Gcoop.Trim(text);
            var arr = text.split(",");
            var maxProcess = Gcoop.ParseInt(arr[1]);
            var countProcess = Gcoop.ParseInt(arr[2]);
            var mpc = (countProcess / maxProcess)* 100; mpc = Math.floor(mpc);
            maxProcess = Gcoop.ParseInt(arr[3]);
            countProcess = Gcoop.ParseInt(arr[4]);
            var spc = (countProcess / maxProcess)* 100; spc = Math.floor(spc);
            if( !(isNaN(mpc) && lastStatus!=1)){
                lastStatus = Gcoop.ParseInt(arr[0]);
                if( arr[6] != undefined ){
                    Gcoop.GetEl("mprogress").style.width = mpc + "%";
                    Gcoop.GetEl("mpercent").innerHTML = mpc +"%";
                    Gcoop.GetEl("mtext").innerHTML = arr[6];
                    Gcoop.GetEl("sprogress").style.width = spc + "%";
                    Gcoop.GetEl("spercent").innerHTML = spc +"%";
                    Gcoop.GetEl("stext").innerHTML = arr[7];
                }
            }
            ChangeTextColor("mtext","black");
            ChangeTextColor("stext","black");
            if(arr[0] == 1){
                lastStatus = 1;
                ChangeTextColor("mtext","blue");
                Gcoop.GetEl("processing").style.visibility = "collapse";
                Gcoop.GetEl("btnClose").style.visibility = "visible";
            } else if(arr[0] == -1){
                lastStatus = -1;
                ChangeTextColor("mtext","red");
                Gcoop.GetEl("processing").style.visibility = "collapse";
                Gcoop.GetEl("btnClose").style.visibility = "visible";
            }
        }catch(eee){
            Gcoop.GetEl("mtext").innerHTML = "ไม่มีสถานะการประมวลผล";
            Gcoop.GetEl("stext").innerHTML = "";
            ChangeTextColor("mtext","gray");
        }
    }
    
    function CallAjax()
    {
        try{
            var currDate = new Date();
            var urls = parent.Gcoop.GetUrl() + "Applications/shrlon/AjaxSlPrincipalBalance.aspx?rand=" + currDate.getMinutes() + currDate.getSeconds();
            Ajax.toPost(urls, "", GetProgressData);
            setTimeout("CallAjax()", 1000);
        }catch(err){
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
    <div id="progress_container">
        <asp:Label ID="mpercent" runat="server" Text="" ForeColor="#0099CC" Font-Size="10px" Font-Bold="True"></asp:Label>
        <div id="mprogress" style="width: 0%"></div>
    </div>
    <div align="center">
        <asp:Label ID="mtext" runat="server" Text="" ForeColor="#267BCA" Font-Size="12px" Font-Bold="False"></asp:Label>
    </div>
    <div id="progress_container2">
        <asp:Label ID="spercent" runat="server" Text="" ForeColor="#0099CC" Font-Size="10px" Font-Bold="True"></asp:Label>
        <div id="sprogress" style="width: 0%"></div>
    </div>
    <div align="center">
        <asp:Label ID="stext" runat="server" Text="" ForeColor="#267BCA" Font-Size="12px" Font-Bold="False"></asp:Label>
    </div>
    <div align="center">
        <img id="processing" style="visibility:visible" alt="" src="../../../img/processing.gif"/>
    </div>
    <div align="center">
        <input id="btnClose" style="visibility:collapse" type="button" value="เสร็จสิ้น" onclick="parent.RemoveIFrame()"/>
    </div>
    </form>
</body>
</html>
