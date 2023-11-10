<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_sl_member_detail.aspx.cs" Inherits="Saving.Applications.shrlon.w_sheet_sl_member_detail" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=sslcontno %>
    <%=chkTrue%>
    <%=chkFalse%>
    <%=postNew%>
    <%=popupReportshr%>
    <%=popupReportloan%>
    <%=runProcess%>
    <%=postMemberNo%>
    <%=CheckCoop %>
    <%=postSalaryId%>
    <script type="text/JavaScript">
        function showTabPage(tab) {
            var i = 1;
            var tabamount = 11;
            for (i = 1; i <= tabamount; i++) {
                document.getElementById("tab" + i).style.visibility = "hidden";
                document.getElementById("stab" + i).style.backgroundColor = "rgb(200,235,255)";
                if (i == tab) {
                    document.getElementById("tab" + i).style.visibility = "visible";
                    document.getElementById("stab" + i).style.backgroundColor = "rgb(211,213,255)";
                }
            }
        }

        function showTabPage2(tab) {
            var i = 1;
            var tabamount = 5;
            for (i = 1; i <= tabamount; i++) {
                document.getElementById("tab_" + i).style.visibility = "hidden";
                document.getElementById("stab_" + i).style.backgroundColor = "rgb(200,235,255)";
                if (i == tab) {
                    document.getElementById("tab_" + i).style.visibility = "visible";
                    document.getElementById("stab_" + i).style.backgroundColor = "rgb(211,213,255)";
                    Gcoop.GetEl("HiddenFieldTab").value = i + "";
                }
            }
        }



        function SheetLoadComplete() {
            if (Gcoop.GetEl("HdIsPostBack").value != "true") {
                //                 alert(Gcoop.GetEl("HdIsPostBack").value);
                Gcoop.SetLastFocus("member_no_0");
                Gcoop.Focus();
            }
            //            if (Gcoop.GetEl("HfOpenLnContDlg").value == "true") {
            //                OpenDialogDetailContract();
            //            }

            var CurTab = Gcoop.ParseInt(Gcoop.GetEl("HiddenFieldTab").value);
            //alert(CurTab);
            //            if (Gcoop.GetEl("Hloancheck").value == "true") {
            //                document.getElementById("cb1").checked = "checked";
            //            }
            //            else {
            //                document.getElementById("cb1").checked = "";
            //            }
            if (isNaN(CurTab)) {
                CurTab = 1;
            }

            showTabPage2(CurTab);

        }

        //function GetValueFromDlg(strvalue) {
        //    var str_temp = window.location.toString();
        //    var str_arr = str_temp.split("?", 2);
        //    window.location = str_arr[0] + "?strvalue=" + strvalue;
        //}

        function itemChanged(sender, rowNumber, columnName, newValue) {
            if (columnName == "member_no") {
                var str_temp = window.location.toString();
                var str_arr = str_temp.split("?", 2);
                //window.location = str_arr[0] + "?strvalue=" + Gcoop.StringFormat(newValue, "00000000");
                Gcoop.GetEl("HdcheckPdf").value = "False";
                sender.SetItem(rowNumber, columnName, newValue);
                sender.AcceptText();
                postMemberNo();
            }
            else if (columnName == "salary_id") {
                var str_temp = window.location.toString();
                var str_arr = str_temp.split("?", 2);
                //window.location = str_arr[0] + "?strvalue=" + Gcoop.StringFormat(newValue, "00000000");
                Gcoop.GetEl("HdcheckPdf").value = "False";
                sender.SetItem(rowNumber, columnName, newValue);
                sender.AcceptText();
                postSalaryId();
            }
            //            if (columnName == "dddw_coop") {
            //                sender.SetItem(rowNumber, columnName, newValue);
            //                sender.AcceptText();
            //                CoopSelect();
            //            }
            return 0;
        }

        function loancheckbox() {
            var chk = document.getElementById("cb1");
            if (chk.checked == true) {
                chkTrue();

            }
            else if (chk.checked == false) {
                chkFalse();
            }
        }


        function OpenDialogDetailShare(memno, shr_tcode) {
            Gcoop.OpenIFrame("650", "560", "w_dlg_sl_detail_share.aspx", "?memno=" + memno + "&shrtype=" + shr_tcode);
        }

        function dw_data_2_row_click(sender, rowNumber, objectName) {

            if (objectName == "b_statement") {
                var memno = objdw_main.GetItem(rowNumber, "member_no");
                if (rowNumber != 0) {
                    var lcontno = objdw_data_2.GetItem(rowNumber, "loancontract_no");
                    Gcoop.GetEl("HfLncontno").value = lcontno;
                    popupReportloan();

                }
            }

            else {
                var memno = objdw_main.GetItem(rowNumber, "member_no");
                if (rowNumber != 0) {
                    var lcontno = objdw_data_2.GetItem(rowNumber, "loancontract_no");
                    Gcoop.GetEl("HfLncontno").value = lcontno;
                    //                    sslcontno();
                    OpenDialogDetailContract(lcontno);
                }
            }
        }

        function dw_data_1_row_click(sender, rowNumber, objectName) {
            //if (objectName == "nameshare") {
            if (objectName != "datawindow") {
                var memno = objdw_main.GetItem(rowNumber, "member_no");
                var shr_tcode = objdw_data_1.GetItem(rowNumber, "sharetype_code");
                if (memno != "" && memno != null && memno != '-1') {
                    OpenDialogDetailShare(memno, shr_tcode);
                }
            }
        }

        function bshr_detail_click(sender, rowNumber, objectName) {
            if (objectName == "bshr_detail") {
                var memno = objdw_main.GetItem(rowNumber, "member_no");
                var shr_tcode = objdw_data_1.GetItem(rowNumber, "sharetype_code");
                if (memno != "" && memno != null && memno != '-1') {
                    OpenDialogDetailShare(memno, shr_tcode);
                }
            }
        }

        function OpenDialogDetailContract(lcontno) {
            Gcoop.OpenIFrame("650", "590", "w_dlg_sl_detail_contract.aspx", "?lcontno=" + lcontno);
        }

        function bloan_detail_click(sender, rowNumber, objectName) {
            if (objectName == "bloan_detail") {
                var memno = objdw_main.GetItem(rowNumber, "member_no");
                if (rowNumber != 0) {
                    var lcontno = objdw_data_2.GetItem(rowNumber, "loancontract_no");
                    Gcoop.GetEl("HfLncontno").value = lcontno;
                    OpenDialogDetailContract(lcontno);

                }
            } else if (objectName == "b_statement") {

                var memno = objdw_main.GetItem(rowNumber, "member_no");
                var lcontno = objdw_data_2.GetItem(rowNumber, "loancontract_no");
                // alert(lcontno);
                Gcoop.GetEl("HfLncontno").value = lcontno;
                popupReportloan();

            }
        }

        function MenubarNew() {
            Gcoop.SetLastFocus("member_no_0");
            Gcoop.Focus();
            postNew();
        }

        function keepdataClick(sender, rowNumber, objectName) {
            var memno = objdw_data_4.GetItem(rowNumber, "member_no");
            var recv_period = objdw_data_4.GetItem(rowNumber, "recv_period");
            Gcoop.OpenIFrame("720", "550", "w_dlg_keepdatadet.aspx", "?memno=" + memno + "&recv_period=" + recv_period);
        }

        function OnClickLinkNext() {
            var memberNoVal = objdw_main.GetItem(1, "member_no");
            Gcoop.OpenDlg(460, 250, "w_deptacc.aspx", "?member=" + memberNoVal);
        }


        function OnClickLinkNextReport() {
            //objdw_main.AcceptText();
            popupReportshr();
        }

        function MenubarOpen() {
            Gcoop.OpenIFrame2(650, 600, 'w_dlg_sl_member_search_tks.aspx', '')
        }

        function GetValueFromDlg(memberno) {
//            alert(memberno);
            objdw_main.SetItem(1, "member_no", memberno.trim());
            objdw_main.AcceptText();
            postMemberNo();
            Gcoop.GetEl("HdcheckPdf").value = "False";
        }

        function OnDwMainClick(s, r, c) {
            if (c == "check_coop") {
                Gcoop.CheckDw(s, r, c, "check_coop", 1, 0);
                CheckCoop();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <asp:HiddenField ID="HfLncontno" runat="server" />
    <asp:HiddenField ID="HiddenFieldTab" runat="server" />
    <asp:HiddenField ID="Hloancheck" runat="server" />
    <asp:HiddenField ID="HfOpenLnContDlg" runat="server" />
    <asp:HiddenField ID="HdOpenIFrame" runat="server" Value="False" />
    <asp:HiddenField ID="HdcheckPdf" runat="server" Value="False" />
    <asp:HiddenField ID="HdIsPostBack" runat="server" Value="false" />
    <table style="width: 100%;">
        <tr>
            <td align="left">
                <span style="cursor: pointer" onclick="OnClickLinkNextReport();">พิมพ์รายงานคุณสมบัติ
                </span>
            </td>
            <td align="right">
            </td>
        </tr>
    </table>
    <dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_sl_member_detail"
        LibraryList="~/DataWindow/shrlon/sl_member_detail.pbl" ClientScriptable="True"
        ClientEventButtonClicked="MenubarOpen" ClientEvents="true" ClientEventItemChanged="itemChanged"
        cAutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
        ClientFormatting="True" AutoRestoreContext="False" ClientEventClicked="OnDwMainClick">
    </dw:WebDataWindowControl>
    
    <%-- ประวัติการโอนสมาชิก
    <dw:WebDataWindowControl ID="dw_trnhistory" runat="server" DataWindowObject="d_mbshr_trnmb_history"
        LibraryList="~/DataWindow/shrlon/sl_member_detail.pbl" ClientScriptable="True"
        ClientEvents="true" cAutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
        ClientFormatting="True" AutoRestoreContext="False">
    </dw:WebDataWindowControl>
    --%>
  
    <table style="width: 100%;">
        <tr>
            <td align="left">
                <span style="cursor: pointer; font-size: 15px" onclick="OnClickLinkNext();">รายละเอียดบัญชี</span>
            </td>
            <td align="right">
            </td>
        </tr>
    </table>
    <table width="100%" border="0" style="height: 350px">
        <tr>
            <td width="15%" valign="top">
                <table width="100%" style="border: solid 1px;" class="dwtab">
                    <tr>
                        <td align="center" style="font-weight: bold; text-decoration: underline; border-bottom: solid 2px;
                            padding-bottom: 3px; font-size: 15px;">
                            เมนู
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="background-color: rgb(211,213,255); cursor: pointer; padding: 10px 0 10px 0;
                            font-size: 14px;" id="stab1" onclick="showTabPage(1);">
                            ข้อมูลสมาชิก
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="background-color: rgb(200,235,255); cursor: pointer; padding: 10px 0 10px 0;
                            font-size: 14px;" id="stab2" onclick="showTabPage(2);">
                            วิธีการรับจ่าย
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="background-color: rgb(200,235,255); cursor: pointer; padding: 10px 0 10px 0;
                            font-size: 14px;" id="stab3" onclick="showTabPage(3);">
                            สถานะ
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="background-color: rgb(200,235,255); cursor: pointer; padding: 10px 0 10px 0;
                            font-size: 14px;" id="stab4" onclick="showTabPage(4);">
                            ห้ามกู้
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="background-color: rgb(200,235,255); cursor: pointer; padding: 10px 0 10px 0;
                            font-size: 14px;" id="stab5" onclick="showTabPage(5);">
                            หักประจำเดือน
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="background-color: rgb(200,235,255); cursor: pointer; padding: 10px 0 10px 0;
                            font-size: 14px;" id="stab6" onclick="showTabPage(6);">
                            รายการหักอื่นๆ
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="background-color: rgb(200,235,255); cursor: pointer; padding: 10px 0 10px 0;
                            font-size: 14px;" id="stab7" onclick="showTabPage(7);">
                            การขอผ่อนผัน
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="background-color: rgb(200,235,255); cursor: pointer; padding: 10px 0 10px 0;
                            font-size: 14px;" id="stab8" onclick="showTabPage(8);">
                            ผู้รับผลประโยชน์
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="background-color: rgb(200,235,255); cursor: pointer; padding: 10px 0 10px 0;
                            font-size: 14px;" id="stab9" onclick="showTabPage(9);">
                            เบี้ยประกัน
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="background-color: rgb(200,235,255); cursor: pointer; padding: 10px 0 10px 0;
                            font-size: 14px;" id="stab10" onclick="showTabPage(10);">
                            บัญชีเงินฝาก
                        </td>
                    </tr>
                   <!-- <tr>
                        <td align="center" style="background-color: rgb(200,235,255); cursor: pointer; padding: 10px 0 10px 0;
                            font-size: 14px;" id="stab11" onclick="showTabPage(11);">
                            รูปภาพ
                        </td>
                    </tr> -->
                    <!-- <tr>
                        <td align="center" style="background-color: rgb(200,235,255); cursor: pointer; padding: 10px 0 10px 0;"
                            id="stab9" onclick="showTabPage(9);">
                            รูปสมาชิก
                        </td>
                    </tr> -->
                </table>
            </td>
            <td style="border: solid 1px;" valign="top" align="center" class="dwcontent">
                <div id="tab1" style="visibility: visible; position: absolute;">
                    <dw:WebDataWindowControl ID="dw_data" runat="server" DataWindowObject="d_sl_mbdetail"
                        LibraryList="~/DataWindow/shrlon/sl_member_detail.pbl" ClientScriptable="True"
                        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        ClientFormatting="True">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab2" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="dw_bank2" runat="server" DataWindowObject="d_sl_mbdetail_expense"
                        LibraryList="~/DataWindow/shrlon/sl_member_detail.pbl" ClientScriptable="True"
                        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        ClientFormatting="True">
                    </dw:WebDataWindowControl>
                    <dw:WebDataWindowControl ID="dw_bank3" runat="server" DataWindowObject="d_sl_mbdetail_expense"
                        LibraryList="~/DataWindow/shrlon/sl_member_detail.pbl" ClientScriptable="True"
                        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        ClientFormatting="True">
                    </dw:WebDataWindowControl>
                    <dw:WebDataWindowControl ID="dw_bank4" runat="server" DataWindowObject="d_sl_mbdetail_expense"
                        LibraryList="~/DataWindow/shrlon/sl_member_detail.pbl" ClientScriptable="True"
                        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        ClientFormatting="True">
                    </dw:WebDataWindowControl>
                    <dw:WebDataWindowControl ID="dw_bank" runat="server" DataWindowObject="d_sl_mbdetail_moneytr"
                        LibraryList="~/DataWindow/shrlon/sl_member_detail.pbl" ClientScriptable="True"
                        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        ClientFormatting="True">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab3" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="dw_data2" runat="server" DataWindowObject="d_sl_mbdetail_status"
                        LibraryList="~/DataWindow/shrlon/sl_member_detail.pbl" ClientScriptable="True"
                        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        ClientFormatting="True">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab4" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="dw_coll2" runat="server" Width="660px" Height="425px"
                        DataWindowObject="d_pauseloan" LibraryList="~/DataWindow/shrlon/sl_member_detail.pbl"
                        ClientScriptable="True" AutoRestoreContext="False" AutoRestoreDataCache="True"
                        AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab5" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="dw_data3" runat="server" DataWindowObject="d_sl_mbdetail_etc_paymonth"
                        LibraryList="~/DataWindow/shrlon/sl_member_detail.pbl" ClientScriptable="True"
                        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        ClientFormatting="True">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab6" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="dw_data4" runat="server" DataWindowObject="d_mb_mbdetail_otherkeep"
                        LibraryList="~/DataWindow/shrlon/sl_member_detail.pbl" ClientScriptable="True"
                        AutoRestoreContext="False" AutoSaveDataCacheAfterRetrieve="True" AutoRestoreDataCache="True"
                        ClientFormatting="True">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab7" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="dw_data5" runat="server" Width="660px" Height="425px"
                        DataWindowObject="d_sl_mbdetail_compound" LibraryList="~/DataWindow/shrlon/sl_member_detail.pbl"
                        ClientScriptable="True" AutoRestoreContext="False" AutoRestoreDataCache="True"
                        AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab8" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="dw_data8" runat="server" Width="660px" Height="425px"
                        DataWindowObject="d_mb_gain_detail_mbdetail" LibraryList="~/DataWindow/shrlon/sl_member_detail.pbl"
                        ClientScriptable="True" AutoRestoreContext="False" AutoRestoreDataCache="True"
                        AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True">
                    </dw:WebDataWindowControl>
                    <asp:Label ID="Label2" runat="server" Text="&nbsp;"></asp:Label>
                    <%--<asp:Image ID="Image1" runat="server" ImageAlign="Middle" Height="182px" Width="163px" />--%>
                </div>
                <div id="tab9" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="dw_data9" runat="server" Width="600px" Height="425px"
                        DataWindowObject="d_sl_mbdetail_insurance" LibraryList="~/DataWindow/shrlon/sl_member_detail.pbl"
                        ClientScriptable="True" AutoRestoreContext="False" AutoRestoreDataCache="True"
                        AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True">
                    </dw:WebDataWindowControl>
                    <asp:Label ID="Label1" runat="server" Text="&nbsp;"></asp:Label>
                    <%--<asp:Image ID="Image2" runat="server" ImageAlign="Middle" Height="182px" Width="163px" />--%>
                </div>
                <div id="tab10" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="dw_data10" runat="server" Width="660px" Height="425px"
                        DataWindowObject="d_sl_mbdetail_deptposit" LibraryList="~/DataWindow/shrlon/sl_member_detail.pbl"
                        ClientScriptable="True" AutoRestoreContext="False" AutoRestoreDataCache="True"
                        AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True">
                    </dw:WebDataWindowControl>
                    <asp:Label ID="Label3" runat="server" Text="&nbsp;"></asp:Label>
                    <%--<asp:Image ID="Image3" runat="server" ImageAlign="Middle" Height="182px" Width="163px" />--%>
                </div>
                <div id="tab11" style="visibility: hidden;" align="center">
                    <asp:Image ID="Img_member_profile" runat="server" Height="200px" Width="150px" /><br/>
                    <asp:Image ID="Img_member_signature" runat="server" Height="200px" Width="300px" />
                </div>
            </td>
        </tr>
    </table>
    <table style="width: 100%; border: solid 1px; margin-top: 5px">
        <tr align="center" class="dwtab">
            <td style="background-color: rgb(211,213,255); cursor: pointer; font-size: 14px;"
                id="stab_1" width="20%" onclick="showTabPage2(1);">
                รายการหุ้น
            </td>
            <td style="background-color: rgb(200,235,255); cursor: pointer; font-size: 14px;"
                id="stab_2" width="20%" onclick="showTabPage2(2);">
                เงินกู้
            </td>
            <td style="background-color: rgb(200,235,255); cursor: pointer; font-size: 14px;"
                id="stab_3" width="20%" onclick="showTabPage2(3);">
                หลักประกันคนค้ำ
            </td>
            <td style="background-color: rgb(200,235,255); cursor: pointer; font-size: 14px;"
                id="stab_4" width="20%" onclick="showTabPage2(4);">
                ติดค้ำประกัน
            </td>
            <td style="background-color: rgb(200,235,255); cursor: pointer; font-size: 14px;"
                id="stab_5" width="20%" onclick="showTabPage2(5);">
                รายการเรียกเก็บประจำเดือน
            </td>
        </tr>
    </table>
    <table style="width: 100%; border: solid 1px; margin-top: 2px" class="dwcontent">
        <tr>
            <td style="height: 200px;" valign="top">
                <div id="tab_1" style="visibility: visible; position: absolute;">
                    <dw:WebDataWindowControl ID="dw_data_1" runat="server" DataWindowObject="d_sl_mbdetail_share"
                        LibraryList="~/DataWindow/shrlon/sl_member_detail.pbl" ClientScriptable="True"
                        ClientEventClicked="dw_data_1_row_click" Height="190px" Width="780px" ClientEventButtonClicked="bshr_detail_click"
                        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        ClientFormatting="True">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab_2" style="visibility: hidden; position: absolute;">
                    <asp:CheckBox ID="CbCheckLoan" runat="server" AutoPostBack="True" Text="สัญญาปัจจุบัน" />
                    <%--<input id="cb1" type="checkbox" style="z-index: 100; position: absolute; margin: 15px 0 0 10px;"
                        onclick="loancheckbox();" checked="checked" />--%>
                    <dw:WebDataWindowControl ID="dw_data_2" runat="server" DataWindowObject="d_sl_mbdetail_loan"
                        LibraryList="~/DataWindow/shrlon/sl_member_detail.pbl" Width="770px" Height="190px"
                        ClientScriptable="True" ClientEventClicked="dw_data_2_row_click" ClientEventButtonClicked="bloan_detail_click"
                        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        ClientFormatting="True">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab_3" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="dw_coll" runat="server" DataWindowObject="d_sl_mbdetail_collall"
                        LibraryList="~/DataWindow/shrlon/sl_member_detail.pbl" ClientScriptable="True"
                        Width="750px" Height="190px" AutoRestoreContext="False" AutoSaveDataCacheAfterRetrieve="True"
                        AutoRestoreDataCache="True" ClientFormatting="True">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab_4" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="dw_data_3" runat="server" DataWindowObject="d_sl_mbdetail_collwho"
                        LibraryList="~/DataWindow/shrlon/sl_member_detail.pbl" Width="770px" Height="190px"
                        AutoRestoreContext="False" AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True"
                        ClientFormatting="True">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab_5" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="dw_data_4" runat="server" DataWindowObject="d_mb_detail_keepdata"
                        LibraryList="~/DataWindow/shrlon/sl_member_detail.pbl" Width="780px" Height="200px"
                        AutoRestoreDataCache="True" AutoRestoreContext="False" ClientFormatting="True"
                        AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" ClientEventClicked="keepdataClick">
                    </dw:WebDataWindowControl>
                </div>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HiddenField1" runat="server" value="False"/>
    <%=outputProcess%>
</asp:Content>
