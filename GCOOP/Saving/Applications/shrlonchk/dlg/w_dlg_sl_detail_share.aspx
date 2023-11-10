<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_sl_detail_share.aspx.cs"
    Inherits="Saving.Applications.shrlonchk.dlg.w_dlg_sl_detail_share" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>รายละเอียดหุ้น</title>
    <script type="text/javascript">
        function showTabPage(tab) {
            var i = 1;
            var tabamount = 2;
            for (i = 1; i <= tabamount; i++) {
                document.getElementById("tab_" + i).style.visibility = "hidden";
                document.getElementById("stab_" + i).style.backgroundColor = "rgb(200,235,255)";
                if (i == tab) {
                    document.getElementById("tab_" + i).style.visibility = "visible";
                    document.getElementById("stab_" + i).style.backgroundColor = "rgb(211,213,255)";
                }
            }
        }
        function IFrameClose(){
            parent.RemoveIFrame();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table style="width: 530px;" border="0">
        <tr>
            <td>
                รายละเอียด<span style="float:right; cursor:pointer;color:Red;" onclick="IFrameClose();">[X]</span>
                <dw:WebDataWindowControl ID="dw_data" runat="server" ClientScriptable="True" DataWindowObject="d_sl_share_detail_main"
                    LibraryList="~/DataWindow/keeping/sl_member_detail.pbl">
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%; border: solid 1px; margin-top: 5px">
                    <tr align="center" class="dwtab">
                        <td style="background-color: rgb(211,213,255); cursor: pointer;" id="stab_1" width="25%"
                            onclick="showTabPage(1);">
                            Statement
                        </td>
                        <td style="background-color: rgb(200,235,255); cursor: pointer;" id="stab_2" width="25%"
                            onclick="showTabPage(2);">
                            การส่งงวด
                        </td>
                    </tr>
                </table>
                </td>
        </tr>
        <tr>
            <td style="height: 250px; border:solid 1px black;" valign="top">
                <div id="tab_1" style="visibility: visible; position: absolute;">
                <dw:WebDataWindowControl ID="dw_data_1" runat="server" ClientScriptable="True"
                    DataWindowObject="d_sl_share_detail_statement" 
                    LibraryList="~/DataWindow/keeping/sl_member_detail.pbl" Width="620px" Height="250px">
                </dw:WebDataWindowControl>
                </div>
                <div id="tab_2" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="dw_data_2" runat="server" DataWindowObject="d_sl_mb_detail_adjust_share_payment"
                        LibraryList="~/DataWindow/keeping/sl_member_detail.pbl" ClientScriptable="True"
                        Width="620px" Height="250px">
                    </dw:WebDataWindowControl>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <table style=" width:100%; font-size:x-small">
                <tr>
                <td>B/F : ยกยอดมาต้นปี</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                </tr>
                <tr>
                <td>SPX : ซื้อหุ้นพิเศษ</td>
                <td>RPX : ยกเลิกซื้อหุ้นพิเศษ</td>
                <td>SPM : ส่งหุ้นประจำเดือน</td>
                <td>RPM : ยกเลิกส่งหุ้นประจำเดือน</td>
                </tr>
                <tr>
                <td>STL : โอนหุ้นชำระหนี้</td>
                <td>RTL : ยกเลิกโอนหุ้นชำระหนี้</td>
                <td>SWD : ถอนหุ้น</td>
                <td>RWD : ยกเลิกถอนหุ้น</td>
                </tr>
                <tr>
                <td>SSR : เก็บเกินจ่ายคืน</td>
                <td>RSR : ยกเลิกเก็บเกินจ่ายคืน</td>
                <td>STR : รับโอนหุ้นสมัครใหม่</td>
                <td>RTR : รับโอนหุ้นสมัครใหม่</td>
                </tr>                
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
