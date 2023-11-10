<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintingDialog.aspx.cs"
    Inherits="Saving.PrintingDialog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Printing Dialog</title>
    <script type="text/javascript">
        var myTotalPage = 0;
        var accepted = 0;
        var autoClose = false;
        var autoPrint = false;
        var printerName = "";
        var acceptClose = 0;
        var sessionFirst = '<%=sessionFirst%>';

        function acceptCloseIframe() {
            acceptClose++;
            if (acceptClose == myTotalPage) {
                if (this.autoClose) {
                    if (sessionFirst == '1') {
                        setTimeout('window.close()', 2500);
                    } else {
                        setTimeout('window.close()', 2500);
                    }
                }
            }
        }

        function acceptIframe(pageNumber, autoPrint, autoClose, printerName) {
            try {
                this.accepted++;
            } catch (err) {
            }
            this.autoClose = autoClose;
            this.autoPrint = autoPrint;
            this.printerName = printerName;
            if ((this.accepted == this.myTotalPage) && this.myTotalPage > 0) {
                if (this.autoPrint) {
                    printAllIFrame(-1);
                }
            }
        }

        function printAllIFrame(id) {
            if (id >= 0) {
                printIFrame(id);
                document.getElementById("u_print_" + id).style.visibility = "hidden";
            } else {
                for (i = 0; i < myTotalPage; i++) {
                    setTimeout('printIFrame(' + i + ')', i * 1000);
                    document.getElementById("u_print_" + i).style.visibility = "hidden";
                }
            }
        }

        function setPaperSize(dataName, width, height, in_or_mm) {
            if (dataName > 43) {
                jsPrintSetup.undefinePaperSize(dataName);
                jsPrintSetup.definePaperSize(dataName, dataName, "", "", "Isc paper size", width, height, in_or_mm);
            }
            jsPrintSetup.setPaperSizeData(dataName);
        }

        function printIFrame(id) {
            // set portrait orientation
            jsPrintSetup.setOption('orientation', jsPrintSetup.kPortraitOrientation);

            // set top margins in millimeters
            jsPrintSetup.setOption('marginTop', 1);
            jsPrintSetup.setOption('marginBottom', 1);
            jsPrintSetup.setOption('marginLeft', 1);
            jsPrintSetup.setOption('marginRight', 1);
            //alert(id);

            // set page header
            jsPrintSetup.setOption('headerStrLeft', '');
            jsPrintSetup.setOption('headerStrCenter', '');
            //jsPrintSetup.setOption('headerStrRight', '&PT');
            jsPrintSetup.setOption('headerStrRight', '');

            // set empty page footer
            jsPrintSetup.setOption('footerStrLeft', '');
            jsPrintSetup.setOption('footerStrCenter', '');
            jsPrintSetup.setOption('footerStrRight', '');

            if (this.printerName != "") {
                var printerNames = jsPrintSetup.getPrintersList().split(',');
                var fullPrinterName = "";
                for (var ii = 0; ii < printerNames.length; ii++) {
                    if (printerNames[ii].indexOf(printerName) >= 0) {
                        fullPrinterName = printerNames[ii];
                        break;
                    }
                }

                if (fullPrinterName != "") {
                    // clears user preferences always silent print value
                    // to enable using 'printSilent' option
                    jsPrintSetup.clearSilentPrint();

                    // Suppress print dialog (for this context only)
                    jsPrintSetup.setOption('printSilent', 1);

                    //DOYS add
                    var printName = fullPrinterName;
                    jsPrintSetup.setPrinter(printName);
                }
            }

            jsPrintSetup.printWindow(window.frames[id]);
            acceptCloseIframe();
        }

        function OnLoadPrinting() {
            var pageName = "<%=pageName%>";
            var totalPage = "<%=totalPage%>";
            var rowCount = "<%=rowCount%>";
            var rowPerPage = "<%=rowPerPage%>";
            var ipAddress = '<%=ipAddress%>';
            var reportName = '<%=reportName%>';
            var xmlConfigUrl = '<%=xmlConfigUrl%>';

            totalPage = parseInt(totalPage);
            myTotalPage = totalPage;

            for (i = 0; i < totalPage; i++) {
                //สร้าง Element ใหม่
                var elNew = document.createElement("iframe");
                var curDate = new Date();
                var curTime = curDate.getTime();
                elNew.src = pageName + "?rowCount=" + rowCount + "&rowPerPage=" + rowPerPage + "&currentPage=" + (i + 1) + "&ipAddress=" + ipAddress + "&reportName=" + reportName + "&xmlConfigUrl=" + xmlConfigUrl + "&ccTime=" + curTime;
                elNew.style.backgroundColor = "#FFFFFF";
                elNew.style.width = "800px";
                elNew.style.height = "550px";
                elNew.setAttribute('id', "iframe_" + i);

                //Add ใส่ Element ใหม่
                document.getElementById("div_main").innerHTML = document.getElementById("div_main").innerHTML + "<font color='white'>หน้า " + (i + 1) + "/" + totalPage + " <u id='u_print_" + i + "' style='cursor:pointer' onclick='printAllIFrame(" + i + ")'>พิมพ์</u></font><br />";
                document.getElementById("div_main").appendChild(elNew);
                document.getElementById("div_main").innerHTML = document.getElementById("div_main").innerHTML + "<br /><br /><br />";
            }
        }
    </script>
    <style type="text/css">
        body
        {
            background-color: #000000;
        }
    </style>
</head>
<body onload="OnLoadPrinting()">
    <form id="form1" runat="server">
    <div id="div_main">
    </div>
    </form>
</body>
</html>
