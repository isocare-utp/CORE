<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_sl_member_detail.aspx.cs" Inherits="Saving.Applications.lawsys.w_sheet_sl_member_detail" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <%=sslcontno %>
    <%=chkTrue%>
    <%=chkFalse%>
    <%=postNew%>

    <script type="text/JavaScript">
        //setTimeout("location.reload(true)", 3000);
        function showTabPage(tab) {
            var i = 1;
            var tabamount = 9;
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

        function OpenDialogDetailContract() {
            Gcoop.OpenIFrame("640", "570", "w_dlg_sl_detail_contract.aspx", "");
        }

        function SheetLoadComplete() {
            if (Gcoop.GetEl("HfOpenLnContDlg").value == "true") {
                OpenDialogDetailContract();
            }

            var CurTab = Gcoop.ParseInt(Gcoop.GetEl("HiddenFieldTab").value);
            if (Gcoop.GetEl("Hloancheck").value == "true") {
                document.getElementById("cb1").checked = "checked";
            }
            else {
                document.getElementById("cb1").checked = "";
            }
            if (isNaN(CurTab)) {
                CurTab = 1;
            }
            showTabPage2(CurTab);
        }

        function MenubarOpen() {
            Gcoop.OpenIFrame('580', '590', 'w_dlg_sl_member_search.aspx', '')
        }

        function GetValueFromDlg(strvalue) {
            var str_temp = window.location.toString();
            var str_arr = str_temp.split("?", 2);
            window.location = str_arr[0] + "?strvalue=" + strvalue;
        }

        function itemChanged(sender, rowNumber, columnName, newValue) {
            if (columnName == "member_no") {
                var str_temp = window.location.toString();
                var str_arr = str_temp.split("?", 2);
                window.location = str_arr[0] + "?strvalue=" + Gcoop.StringFormat(newValue, "000000");
            }
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
            //if (objectName == "loancontract_no") {
            if (objectName != "datawindow") {
                var memno = objdw_main.GetItem(rowNumber, "member_no");
                if (rowNumber != 0) {
                    var lcontno = objdw_data_2.GetItem(rowNumber, "loancontract_no");
                    Gcoop.GetEl("HfLncontno").value = lcontno;
                    sslcontno();

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
        
        function bloan_detail_click(sender, rowNumber, objectName) {
            if (objectName == "bloan_detail") {
                var memno = objdw_main.GetItem(rowNumber, "member_no");
                if (rowNumber != 0) {
                    var lcontno = objdw_data_2.GetItem(rowNumber, "loancontract_no");
                    Gcoop.GetEl("HfLncontno").value = lcontno;
                    sslcontno();
                }
            }
        }
        
        function MenubarNew() {
            if (confirm("ยืนยันการล้างข้อมูลบนหน้าจอ")) {
                postNew();
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
    <dw:WebDataWindowControl ID="dw_main" runat="server" DataWindowObject="d_sl_member_detail"
        LibraryList="~/DataWindow/keeping/sl_member_detail.pbl" ClientScriptable="True"
        ClientEvents="true" ClientEventItemChanged="itemChanged">
    </dw:WebDataWindowControl>
    <table width="100%" border="0" style="height: 250px">
        <tr>
            <td width="20%" valign="top">
                <table width="100%" style="border: solid 1px;" class="dwtab">
                    <tr>
                        <td align="center" style="font-weight: bold; text-decoration: underline; border-bottom: solid 2px;
                            padding-bottom: 3px;">
                            เมนู
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="background-color: rgb(211,213,255); cursor: pointer; padding: 10px 0 10px 0;"
                            id="stab1" onclick="showTabPage(1);">
                            ข้อมูลสมาชิก
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="background-color: rgb(200,235,255); cursor: pointer; padding: 10px 0 10px 0;"
                            id="stab2" onclick="showTabPage(2);">
                            ธนาคาร
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="background-color: rgb(200,235,255); cursor: pointer; padding: 10px 0 10px 0;"
                            id="stab3" onclick="showTabPage(3);">
                            สถานะภาพ
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="background-color: rgb(200,235,255); cursor: pointer; padding: 10px 0 10px 0;"
                            id="stab4" onclick="showTabPage(4);">
                            เบี้ยประกัน
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="background-color: rgb(200,235,255); cursor: pointer; padding: 10px 0 10px 0;"
                            id="stab5" onclick="showTabPage(5);">
                            หักประจำเดือน
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="background-color: rgb(200,235,255); cursor: pointer; padding: 10px 0 10px 0;"
                            id="stab6" onclick="showTabPage(6);">
                            รายการหักอื่นๆ
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="background-color: rgb(200,235,255); cursor: pointer; padding: 10px 0 10px 0;"
                            id="stab7" onclick="showTabPage(7);">
                            การขอผ่อนผัน
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="background-color: rgb(200,235,255); cursor: pointer; padding: 10px 0 10px 0;"
                            id="stab8" onclick="showTabPage(8);">
                            หมายเหตุ-สถานะ
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="background-color: rgb(200,235,255); cursor: pointer; padding: 10px 0 10px 0;"
                            id="stab9" onclick="showTabPage(9);">
                            รูปสมาชิก
                        </td>
                    </tr>
                </table>
            </td>
            <td style="border: solid 1px;" valign="top" align="center" class="dwcontent">
                <div id="tab1" style="visibility: visible; position: absolute;">
                    <dw:WebDataWindowControl ID="dw_data" runat="server" DataWindowObject="d_sl_mbdetail"
                        LibraryList="~/DataWindow/keeping/sl_member_detail.pbl" ClientScriptable="True">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab2" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="dw_bank" runat="server" DataWindowObject="d_sl_mbdetail_moneytr"
                        LibraryList="~/DataWindow/keeping/sl_member_detail.pbl" ClientScriptable="True">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab3" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="dw_data2" runat="server" DataWindowObject="d_sl_mbdetail_status"
                        LibraryList="~/DataWindow/keeping/sl_member_detail.pbl" ClientScriptable="True">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab4" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="dw_coll2" runat="server" DataWindowObject="d_sl_mbdetail_insurance"
                        LibraryList="~/DataWindow/keeping/sl_member_detail.pbl" ClientScriptable="True">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab5" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="dw_data3" runat="server" DataWindowObject="d_sl_mbdetail_etc_paymonth"
                        LibraryList="~/DataWindow/keeping/sl_member_detail.pbl" ClientScriptable="True">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab6" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="dw_data4" runat="server" DataWindowObject="d_mb_mbdetail_otherkeep"
                        LibraryList="~/DataWindow/keeping/sl_member_detail.pbl" ClientScriptable="True">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab7" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="dw_data5" runat="server" Width="600px" Height="330px"
                        DataWindowObject="d_sl_mbdetail_compound" LibraryList="~/DataWindow/keeping/sl_member_detail.pbl"
                        ClientScriptable="True">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab8" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="dw_remarkstat" runat="server" Width="600px" Height="330px"
                        DataWindowObject="d_mb_adjust_remarkstat_detail" LibraryList="~/DataWindow/keeping/sl_member_detail.pbl"
                        ClientScriptable="True">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab9" style="visibility: hidden; position: absolute; ">
                    <asp:Label ID="Label2" runat="server" Text="&nbsp;"></asp:Label>
                    <asp:Image ID="Image1" runat="server" Height="182px" Width="163px" />
                </div>
            </td>
        </tr>
    </table>
    <table style="width: 100%; border: solid 1px; margin-top: 5px">
        <tr align="center" class="dwtab">
            <td style="background-color: rgb(211,213,255); cursor: pointer;" id="stab_1" width="25%"
                onclick="showTabPage2(1);" width="20%">
                รายการหุ้น
            </td>
            <td style="background-color: rgb(200,235,255); cursor: pointer;" id="stab_2" width="25%"
                onclick="showTabPage2(2);" width="20%">
                เงินกู้
            </td>
            <td style="background-color: rgb(200,235,255); cursor: pointer;" id="stab_3" width="25%"
                onclick="showTabPage2(3);" width="20%">
                ใครค้ำให้เรา
            </td>
            <td style="background-color: rgb(200,235,255); cursor: pointer;" id="stab_4" width="25%"
                onclick="showTabPage2(4);" width="20%">
                เราค้ำให้ใคร
            </td>
            <%--            <td style="background-color: rgb(200,235,255); cursor: pointer;" id="stab_4" width="25%"
                onclick="showTabPage2(4);" width="20%">
                กองทุนช่วยเหลือผู้ค้ำ ฯ
            </td>--%>
        </tr>
    </table>
    <table style="width: 100%; border: solid 1px; margin-top: 2px" class="dwcontent">
        <tr>
            <td style="height: 200px;" valign="top">
                <div id="tab_1" style="visibility: visible; position: absolute;">
                    <dw:WebDataWindowControl ID="dw_data_1" runat="server" DataWindowObject="d_sl_mbdetail_share"
                        LibraryList="~/DataWindow/keeping/sl_member_detail.pbl" ClientScriptable="True"
                        ClientEventClicked="dw_data_1_row_click" Height="190px" Width="760px" ClientEventButtonClicked="bshr_detail_click">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab_2" style="visibility: hidden; position: absolute;">
                    <input id="cb1" type="checkbox" style="z-index: 100; position: absolute; margin: 15px 0 0 10px;"
                        onclick="loancheckbox();" />
                    <dw:WebDataWindowControl ID="dw_data_2" runat="server" DataWindowObject="d_sl_mbdetail_loan"
                        LibraryList="~/DataWindow/keeping/sl_member_detail.pbl" Width="760px" Height="190px"
                        ClientScriptable="True" ClientEventClicked="dw_data_2_row_click" ClientEventButtonClicked="bloan_detail_click">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab_3" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="dw_coll" runat="server" DataWindowObject="d_sl_mbdetail_collall"
                        LibraryList="~/DataWindow/keeping/sl_member_detail.pbl" ClientScriptable="True">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab_4" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="dw_data_3" runat="server" DataWindowObject="d_sl_mbdetail_collwho"
                        LibraryList="~/DataWindow/keeping/sl_member_detail.pbl" Width="760px" Height="190px">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab_5" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="dw_data_4" runat="server" DataWindowObject="d_sl_mbdetail_welfare_helpcolldet"
                        LibraryList="~/DataWindow/keeping/sl_member_detail.pbl" Width="760px" Height="190px">
                    </dw:WebDataWindowControl>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>

