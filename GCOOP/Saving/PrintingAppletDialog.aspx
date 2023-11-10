<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintingAppletDialog.aspx.cs"
    Inherits="Saving.PrintingAppletDialog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Applet Printing</title>
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
        <applet code="slipapp.AppletPrinting" archive="<%=appletPath%>" width="820" height="670"
            align="middle">
            <param name="printingName" value="<%=printingName%>" />
            <param name="urlXmlData" value="<%=urlXmlData%>" />
            <param name="urlXmlMaster" value="<%=urlXmlMaster%>" />
            <param name="urlXmlDetail" value="<%=urlXmlDetail%>" />
        </applet>
    </div>
    </form>
</body>
</html>