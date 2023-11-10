<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="webmsgframe.aspx.cs" Inherits="Saving.webmsgframe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <script language="javascript">
    
        var baseURL=window.location.protocol + "//" + window.location.host ;

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
                        if (confirm(result + "\r\nต้องการดึงรายการเลยหรือไม่?")) {
                            //กรณีต้องการให้ Refresh หน้าจอ
                            //location.reload();
                            //กรณีให้เด้งไปหน้าที่ต้องการ
                            parent.location.href = baseURL+"/GSB/GCOOP/Saving/Applications/shrlon/ws_sl_approve_req_to_contract.aspx?setApp=shrlon&setGroup=LON&setWinId=SL-LON0031";
                        }
                    }
                }
            }
            //alert(baseURL +"/CORE/GCOOP/Saving/webmsg.aspx?u=<%=Request["u"]%>&d=<%=Request["d"]%>");
            var URL=(baseURL +"/CORE/GCOOP/Saving/webmsg.aspx?u=<%=Request["u"]%>&d=<%=Request["d"]%>");
            //alert(URL);
            xmlhttp.open("GET",URL, false);
            xmlhttp.send();
            setTimeout("getMsg()", 2000);
        }

        getMsg();
        

    </script>
    </div>
    </form>
</body>
</html>
