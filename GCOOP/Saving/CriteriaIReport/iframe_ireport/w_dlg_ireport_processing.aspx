<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true"
    CodeBehind="w_dlg_ireport_processing.aspx.cs" Inherits="Saving.CriteriaIReport.iframe_ireport.w_dlg_ireport_processing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <asp:Literal ID="LtJsCss" runat="server">
        <script src="../../JsCss/Ajax.js" type="text/javascript"></script>
    </asp:Literal>
    <script type="text/javascript">
        var reload = 1;
        var split1 = '<%=spliter1%>';
        var img1 = '<%=Image1.ClientID%>';
        var savingUrl = '<%=savingUrl%>';
        var iconFin = '';
        var iconErr = '';

        function sRefresh(text) {
            try {
                text = Gcoop.Trim(text);
                var arr = text.split(split1);
                if (arr[0] == "none") {
                    reload = 0;
                    document.getElementById("stext").innerHTML = "พบข้อผิดผลาด, (" + arr[1] + ")";
                } else if (arr[0] == "-1") {
                    reload = 0;
                    document.getElementById(img1).src = iconErr;
                    document.getElementById("stext").innerHTML = "ออกรายงาน" + arr[1] + "ไม่สำเร็จ, (" + arr[2] + ")";
                } else if (arr[0] == "1") {
                    reload = 0;
                    document.getElementById(img1).src = iconFin;
                    document.getElementById("stext").innerHTML = "ออกรายงาน " + arr[1] + " สำเร็จ";
                    setTimeout("success()", 1000);
                } else if (arr[0] == "0") {
                    document.getElementById("stext").innerHTML = "กำลังเตรียมการออกรายงาน " + arr[1] + "";
                } else if (arr[0] == "8") {
                    document.getElementById("stext").innerHTML = "กำลังสร้างรายงาน " + arr[1] + ", โปรดรอสักครู่";
                }
            } catch (i6969) {
                document.getElementById(img1).src = iconErr;
                Gcoop.GetEl("stext").innerHTML = "JS" + i6969;
            }
            ++secCount;
            document.getElementById("scount").innerHTML = secCount.toString() + " times";
            setTimeout("sReload()", 1000);
        }

        function sReload() {
            if (reload == 0) return;
            try {
                var currDate = new Date();
                var urls = '<%=processUrl%>';
                var dataX = "?rand=" + currDate.getMinutes() + currDate.getSeconds() + "&pid=<%=pid%>&cid=<%=cid%>";
                Ajax.toPost(urls, dataX, sRefresh);
            } catch (err) {
            }
        }

        function success() {
            parent.OpenReportSuccessful();
        }

        function DialogLoadComplete() {
            iconFin = savingUrl + "Image/success_48.png";
            iconErr = savingUrl + "Image/error_48.png";
            sReload();
        }

        var secCount = 0;
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <div align="center" style="font-family: Tahoma; font-size: 10px;">
        <span>Counting ... </span><span id="scount"></span>
    </div>
    <div align="center">
        <table style="font-family: Tahoma; font-size: 16px;" align="center">
            <tr>
                <td valign="middle" width="32px;" align="left">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Image/processing.gif" Width="32px"
                        Height="32px" />
                </td>
                <td valign="middle" align="left">
                    <span id="stext" style="margin-left: 20px;"></span>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <input type="button" value="ปิด" onclick="parent.RemoveIFrame()" style="width: 80px;" />
    </div>
</asp:Content>
