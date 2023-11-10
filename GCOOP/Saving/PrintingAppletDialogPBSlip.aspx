<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintingAppletDialogPBSlip.aspx.cs" Inherits="Saving.PrintingAppletDialogPBSlip" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Applet Printing PB Slip</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script type="text/javascript" language="javascript">
        function GetOnLoad() {
            //window.close();
        }
    </script>
</head>
<body onload="GetOnLoad()" bgcolor="#666666">
    <form id="form1" runat="server">
    <div align="center">
        <applet code="slippb_applet.AppletPrinting" archive="<%=appletPath%>" width="300" height="300"
            align="middle">
            <param name="printingName" value="<%=printingName%>" />
            <param name="urlXmlData" value="<%=urlXmlData%>" />
            <param name="urlXmlMaster" value="<%=urlXmlMaster%>" />
            <param name="serverPBPath" value="<%=serverPBPath%>" />
            <param name="clientPBPath" value="<%=clientPBPath%>" />
            <param name="autoUpdate" value="<%=autoUpdate%>" />
            <param name="serverVersion" value="<%=serverVersion%>" />
            <param name="serverFiles" value="<%=serverFiles%>" />
        </applet>
    </div>
    </form>
</body>
</html>