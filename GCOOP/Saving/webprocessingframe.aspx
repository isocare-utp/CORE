<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="webprocessingframe.aspx.cs"
    Inherits="Saving.webprocessingframe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript">
        var autoClose=false;//กรณีให้ตรวจสอบว่าประมวลนานไป
        var maxTime=60*60*1000;//จำกัด เวลาประมวลถ้าเกิน 1 ชม. แสดงว่า น่าจะมีปัญหาให้ยกเลิกการประมวล
        var curTime=0;
        var reloanTime=5*1000;//ให้ตรวจสอบทุกๆ 5 วืนาที

        var baseURL=window.location.protocol + "//" + window.location.host +(window.location.port ? (':'+window.location.port): '');        
        var cancelURL=baseURL +"/CORE/GCOOP/Saving/webprocessing.aspx?p=<%=Request["p"]%>&u=<%=Request["u"]%>&d=<%=Request["d"]%>&c=1";
        
        var img="<img src=\""+baseURL +"/CORE/GCOOP/Saving/Image/processing.gif\" border=0 />";
        var closeInput="<input type=\"button\" name=\"close\" id=\"closeProcess\" value=\"ปิด\" onclick=\"Gcoop.RemoveIFrame();CloseFrame()\" />";
        var cancelInput="<input type=\"button\" name=\"cancel\" id=\"cancelProcess\" value=\"ยกเลิก\" onclick=\"if(confirm('ต้องการยกเลิกใช่หรือไม่?')){Gcoop.RemoveIFrame();document.getElementById('processFrame').src='"+cancelURL+"'}\" />";
        var openReport="<a href=\"\" name=\"openReport\" id=\"openReport\" target=\"_blank\" style=\"display:none;\">เปิดรายงาน</a>";

        function checkURLexists(URL){
                var request;
                if(window.XMLHttpRequest)
                    request = new XMLHttpRequest();
                else
                    request = new ActiveXObject("Microsoft.XMLHTTP");
                request.open('GET', URL, false);
                request.send(); // there will be a 'pause' here until the response to come.
                // the object request will be actually modified
                if (request.status === 404) {
                    //alert("The page you are trying to reach is not available.");
                    return false;
                }else{
                    return true;
                }
        }

        function getCurrentDate(){
           date = new Date;
            h = date.getHours();
            if (h < 10) {
                h = "0" + h;
            }
            m = date.getMinutes();
            if (m < 10) {
                m = "0" + m;
            }
            s = date.getSeconds();
            if (s < 10) {
                s = "0" + s;
            }
         return date.getDate() + "-" +  (parseInt(date.getMonth())+1) + "-" + (date.getFullYear()+543) +" "+h+":"+m+":"+s ;
        }

        function CloseFrame() {
            RemoveIFrame();
        }
        function CancelFrame() {
            cancelMsg();
            RemoveIFrame();
        }
        function RemoveIFrame() {
            try {
                var ni = parent.getElementById('iFrameMaster');
                var elNew = parent.document.getElementById('iFrameChild');
                ni.removeChild(elNew);
            } catch (err) { }
        }
        function AddIFrameOnProcess(msg) {
            try {
                RemoveIFrame();
                var ni = parent.document.getElementById('iFrameMaster');
                var newdiv = parent.document.createElement('div');
                var divIdName = 'iFrameChild';
                newdiv.setAttribute('id', divIdName);
                ni.appendChild(newdiv);
                var elNew = parent.document.getElementById(divIdName);

                elNew.style.top = 0;
                elNew.style.left = 0;
                elNew.style.width = (screen.width - 20) + "px";
                elNew.style.height = "2500px";
                elNew.style.zIndex = 999;
                elNew.style.position = "absolute";
                elNew.style.backgroundImage = "url('" + baseURL + "/CORE/GCOOP/Saving/Image/tranparent.png')";
                elNew.innerHTML = "<input style='border:none;width:1px;height:1px;' type='text' id='ttttOnSubmit'><br /><br /><br /><br /><br /><br /><br /><br /><div align='center' ><table border=0 style='background:#FFFFFF;' cellspacing=10 cellpadding=10 ><tr><td  align='center'><br/><font size='4' color='blue'>&nbsp;&nbsp;" + msg + "&nbsp;&nbsp;</font><br/><br/><br/></td></tr></table></div>";
                parent.window.document.getElementById("tempElementEnter").focus();
            } catch (error) { }
        }

        function strip_tags(html) {
            var find = "LbServerMessage\">"
            var pos = html.indexOf(find);
            if (pos > 0) {
                html = html.substring(pos + find.length, html.length);
                pos = html.indexOf("<");
                html = html.substring(0, pos);
                return html;
            } else {
                return "";
            }
        }
        
        function postShowMsg() {
            
            try{parent.document.getElementById("cancelProcess").style.display="none"; }catch (error) { }
            if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
                xmlhttp = new XMLHttpRequest();
            }
            else {// code for IE6, IE5
                xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
            }
            xmlhttp.onreadystatechange = function () {
                if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                    var regex = "/<(.|\n)*?>/";
                    var body = xmlhttp.responseText;
                    var result = strip_tags(body.replace(regex, ""));
                    if (result != "") {
                         RemoveIFrame();                        
                    }
                }
            }
            var URL=(baseURL +"/CORE/GCOOP/Saving/webprocessing.aspx?p=<%=Request["p"]%>&u=<%=Request["u"]%>&d=<%=Request["d"]%>&s=1");
            //alert(URL);
            xmlhttp.open("GET",URL, false);
            xmlhttp.send();
        }

        function cancelMsg() {
            if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
                xmlhttp = new XMLHttpRequest();
            }
            else {// code for IE6, IE5
                xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
            }
            xmlhttp.onreadystatechange = function () {
                if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                    var regex = "/<(.|\n)*?>/";
                    var body = xmlhttp.responseText;
                    var result = strip_tags(body.replace(regex, ""));
                    if (result != "") {
                         RemoveIFrame();                        
                    }
                }
            }
            var URL=(baseURL +"/CORE/GCOOP/Saving/webprocessing.aspx?p=<%=Request["p"]%>&u=<%=Request["u"]%>&d=<%=Request["d"]%>&c=1");
            //alert(URL);
            xmlhttp.open("GET",URL, false);
            xmlhttp.send();
        }

        function getMsg() {
            if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
                xmlhttp = new XMLHttpRequest();
            }
            else {// code for IE6, IE5
                xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
            }
            xmlhttp.onreadystatechange = function () {
                if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                    var regex = "/<(.|\n)*?>/";
                    var body = xmlhttp.responseText;
                    var result = strip_tags(body.replace(regex, ""));
                    //var re = new RegExp(/^.*\//);
                    //var baseURL= re.exec(window.location.href);
                    //alert();
                    if (result != "") {  
                       if(result.indexOf("1;") >=0){  
                         result_finish = false;          
                         result_data=result.replace("1;","");  
                         if(result.indexOf(".xls") >=0){ result_data="";   result_finish=true; }
                         if(result.indexOf(".pdf") >=0){ result_data="";   result_finish=true; }  
                         if(result.indexOf(".txt") >=0){ result_data="";   result_finish=true; }    
                         if(result.indexOf(".csv") >=0){ result_data="";   result_finish=true; }          
                         //AddIFrameOnProcess("ประมวลผลสำเร็จ<br/>"+"รหัสทำรายการ : <%=Request["p"]%> <br/>"+getCurrentDate()+"<br/>"+((result.indexOf(".pdf") >=0)?"":((result.indexOf(".xls") >=0)?"":result.replace("1;","")))+"<br/>"+openReport+"<br/><br/>"+closeInput);
                         AddIFrameOnProcess("ประมวลผลสำเร็จ<br/>"+"รหัสทำรายการ : <%=Request["p"]%> <br/>"+getCurrentDate()+"<br/>"+result_data+"<br/>"+openReport+"<br/><br/>"+closeInput);
                         document.getElementById("output").innerHTML=result;
                         try{ parent.processNext(); }catch(errors){ }        
                         //if(result.indexOf(".pdf") >=0||result.indexOf(".xls") >=0){
                         if(result_finish){
                           tmp=result.replace("1;","");
                           pos=tmp.indexOf(":");
                           report_filename=tmp.substring(0,pos).replace(":","");
                           printer=tmp.substring(pos,tmp.length).replace(":","");
                           report_filename_url= baseURL+"/<%=coreURI%>/GCOOP/Saving/WSRPDF/"+report_filename;
                           report_filename_url_= baseURL+"/<%=currentURI%>/GCOOP/Saving/WSRPDF/"+report_filename;
                           try{
                            printer_finish = false; 
                            if(printer.toLowerCase()=="pdf"){ printer_finish=true; }  
                            if(printer.toLowerCase()=="xls"){ printer_finish=true; }  
                            if(printer.toLowerCase()=="txt"){ printer_finish=true; }  
                            if(printer.toLowerCase()=="csv"){ printer_finish=true; }  
                            if(printer_finish){
                                 parent.document.getElementById("openReport").href  = report_filename_url;
                                 parent.document.getElementById("openReport").style.display=""; 
                                 //alert(report_filename_url);
                                 parent.document.getElementById("openReport").innerHTML="เปิดรายงาน";
                                 if(checkURLexists(report_filename_url)){
                                    parent.document.getElementById("openReport").click();
                                 }else if(checkURLexists(report_filename_url_)){
                                    parent.document.getElementById("openReport").href  = report_filename_url_;
                                    parent.document.getElementById("openReport").click();
                                 }else{
                                    parent.document.getElementById("openReport").href  ="javascript:function() { return false; }";
                                    parent.document.getElementById("openReport").innerHTML="<span style=\"color:red\">พบข้อผิดพลาด : ไม่สามารถเปิดรายงาน "+report_filename+" ได้</span>";
                                 }
                             }else{
                                 parent.document.getElementById("openReport").href  ="javascript:function() { return false; }";
                                 parent.document.getElementById("openReport").style.display=""; 
                                 parent.document.getElementById("openReport").innerHTML="พิมพ์งานไปที่ : "+printer;
                             }
                           }catch (error) { }
                         }
                         RemoveIFrame();
                         postShowMsg();
                         /*
                         if (confirm("ประมวลผลสำเร็จ")) {
                            parent.document.getElementById("closeProcess").click();
                            //กรณีต้องการให้ Refresh หน้าจอ
                            parent.location.reload();
                            //กรณีให้เด้งไปหน้าที่ต้องการ
                            //parent.location.href = baseURL+"/CORE/GCOOP/Saving/Applications/shrlon/ws_sl_approve_req_to_contract.aspx?setApp=shrlon&setGroup=LON&setWinId=SL-LON0031";
                        }
                        */
                       }else{                       
                         AddIFrameOnProcess(result+"<br/>"+"รหัสทำรายการ : <%=Request["p"]%> <br/>"+getCurrentDate()+"<br/>"+img+"<br/>"+cancelInput+"&nbsp;"+"&nbsp;"+closeInput);
                         document.getElementById("output").innerHTML=result;
                         }
                    }else{
                         
                    }
                }
            }
            //alert(baseURL +"/CORE/GCOOP/Saving/webmsg.aspx?u=<%=Request["u"]%>&d=<%=Request["d"]%>");
            var URL=(baseURL +"/CORE/GCOOP/Saving/webprocessing.aspx?p=<%=Request["p"]%>&u=<%=Request["u"]%>&d=<%=Request["d"]%>");
            //alert(URL);
            xmlhttp.open("GET",URL, false);
            xmlhttp.send();
            curTime+=reloanTime;
            if(autoClose&&curTime>maxTime){
                parent.document.getElementById("closeProcess").click();
                alert("พบว่าเกิดการประมวล นานกว่าปกติ กรุณาตรวจสอบการเชื่อมต่อระบบ!!!");
            }else{
                setTimeout("getMsg()", reloanTime);
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="output">
    </div>
    <input type="button" name="close" id="closeProcess" value="ปิด" onclick="CloseFrame()" />
    <input type="button" name="cancel" id="cancelProcess" value="ยกเลิก" onclick="CancelFrame()" />
    <div>
        <script language="javascript">            getMsg();</script>
    </div>
    </form>
</body>
</html>
