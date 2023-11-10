<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_sl_detail_contract.aspx.cs"
    Inherits="Saving.Applications.app_finance.dlg.w_dlg_sl_detail_contract" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <script type="text/javascript">
        function showTabPage(tab) {
            var i = 1;
            var tabamount = 4;
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

    <title>รายละเอียดสัญญาเงินกู้</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table style="width: 630px;" border="0">
        <tr>
            <td>
                รายละเอียดสัญญา<span style="float: right; cursor: pointer; color: Red;" onclick="IFrameClose();">[X]</span>
                <dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_sl_cont_detail_main"
                    LibraryList="~/DataWindow/app_finance/sl_member_detail.pbl" ClientScriptable="True"
                    ClientEvents="true">
                </dw:WebDataWindowControl>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%; border: solid 1px; margin-top: 5px">
                    <tr align="center" class="dwtab">
                        <td style="background-color: rgb(211,213,255); cursor: pointer;" id="stab_1" width="25%"
                            onclick="showTabPage(1);">
                            รายละเอียดสัญญา
                        </td>
                        <td style="background-color: rgb(200,235,255); cursor: pointer;" id="stab_2" width="25%"
                            onclick="showTabPage(2);">
                            Statement
                        </td>
                        <td style="background-color: rgb(200,235,255); cursor: pointer;" id="stab_3" width="25%"
                            onclick="showTabPage(3);">
                            หลักประกัน
                        </td>
                        <td style="background-color: rgb(200,235,255); cursor: pointer;" id="stab_4" width="25%"
                            onclick="showTabPage(4);">
                            การส่งงวด
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 350px; border: solid 1px black;" valign="top">
                <div id="tab_1" style="visibility: visible; position: absolute;">
                    <dw:WebDataWindowControl ID="dw_data_1" runat="server" DataWindowObject="d_sl_cont_detail_data"
                        LibraryList="~/DataWindow/app_finance/sl_member_detail.pbl" ClientScriptable="True">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab_2" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="dw_data_2" runat="server" DataWindowObject="d_sl_cont_detail_statement"
                        LibraryList="~/DataWindow/app_finance/sl_member_detail.pbl" ClientScriptable="True"
                        Width="620px" Height="350px">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab_3" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="dw_data_3" runat="server" DataWindowObject="d_sl_cont_detail_collateral"
                        LibraryList="~/DataWindow/app_finance/sl_member_detail.pbl" ClientScriptable="True">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab_4" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="dw_data_4" runat="server" DataWindowObject="d_sl_cont_detail_chgpay"
                        LibraryList="~/DataWindow/app_finance/sl_member_detail.pbl" Width="620px" Height="350px"
                        ClientScriptable="True">
                    </dw:WebDataWindowControl>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%; font-size: x-small" border="0">
                    <tr>
                        <td>
                            B/F : ยกยอดมาต้นปี
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            LPM : ชำระรายเดือน
                        </td>
                        <td>
                            RPM : ยกเลิกชำระรายเดือน
                        </td>
                        <td>
                            LPX : ชำระหนี้พิเศษ
                        </td>
                        <td>
                            RPX : ยกเลิกชำระหนี้พิเศษ
                        </td>
                    </tr>
                    <tr>
                        <td>
                            LRC : รับเงินกู้
                        </td>
                        <td>
                            RRC : ยกเลิกการรับเงินกู้
                        </td>
                        <td>
                            LCL : หักกลบกู้สัญญาใหม่
                        </td>
                        <td>
                            RCL : ยกเลิกหักกลบกู้สัญญาใหม่
                        </td>
                    </tr>
                    <tr>
                        <td>
                            LTL : โอนหุ้นมาชำระหนี้
                        </td>
                        <td>
                            RTL : ยกเลิกโอนหุ้นมาชำระหนี้
                        </td>
                        <td>
                            LRT : คืนเงินต้น,ดอกเบี้ย
                        </td>
                        <td>
                            RRT : ยกเลิกคืนเงินต้น,ดอกเบี้ย
                        </td>
                    </tr>
                    <tr>
                        <td>
                            TLG : โอนหนี้ให้ผู้ค้ำ
                        </td>
                        <td>
                            RLG : ยกเลิกโอนหนี้ให้ผู้ค้ำ
                        </td>
                        <td>
                            TRG : รับโอนหนี้ค้ำประกัน
                        </td>
                        <td>
                            RRG : ยกเลิกรับโอนหนี้ค้ำประกัน
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
