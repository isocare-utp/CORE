<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_divsrv_detail.aspx.cs" Inherits="Saving.Applications.divavg.w_sheet_divsrv_detail" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 171px;
        }
        .style2
        {
            height: 1px;
        }
    </style>
    <%=initJavaScript %>
    <%=postInit%>
    <%=postNewClear%>
    <%=postInitMember%>
    <%=postrefresh%>
    <%=postFilter%>
    <script type="text/javascript">
        function OnDwYearClick(s, r, c) {
            if (c == "div_year") {
                var div_year = objDw_year.GetItem(r, "div_year");
                Gcoop.GetEl("HdDivyear").value = div_year;
                Gcoop.GetEl("Hdrow").value = r + "";
                postFilter();
            }

        }
        function OnDwmainButtonClick(s, r, b) {
            if (b == "b_search_memno") {
                Gcoop.OpenIFrame("800", "500", "w_dlg_divsrv_search_mem.aspx", "");
            }
        }

        function GetMemberNoFromDialog(member_no) {
            Gcoop.GetEl("Hdmember_no").value = member_no;
            postInitMember();
        }

        function OnDwMainItemChange(s, r, c, v) {
            if (c == "member_no") {
                s.SetItem(1, "member_no", v);
                s.AcceptText();
                postInit();
            }
            return 0;
        }

        function Validate() {
            //            objDw_main.AcceptText();
            //            objDw_detail.AcceptText();
            //            return confirm("ยืนยันการบันทึกข้อมูล");
        }
        function MenubarNew() {
            if (confirm("ยืนยันการล้างข้อมูลบนหน้าจอ")) {
                postNewClear();
            }
        }

        function showTabPage(tab) {
            var i = 1;
            var tabamount = 6;

            for (i = 1; i <= tabamount; i++) {
                if (i == 3) {
                    document.getElementById("tab" + i).style.visibility = "hidden";
                    document.getElementById("stab_" + i).style.backgroundColor = "rgb(200,235,255)";

                }
                else {
                    document.getElementById("tab" + i).style.visibility = "hidden";
                    document.getElementById("stab_" + i).style.backgroundColor = "rgb(200,235,255)";

                }


                if (i == tab) {
                    document.getElementById("tab" + i).style.visibility = "visible";
                    document.getElementById("stab_" + i).style.backgroundColor = "rgb(211,213,255)";
                    Gcoop.GetEl("HdTab").value = i + "";
                }
            }
        }

        function SheetLoadComplete() {
            var CurTab = Gcoop.ParseInt(Gcoop.GetEl("HdTab").value);
            if (isNaN(CurTab)) {
                CurTab = 1;
            }
            showTabPage(CurTab);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <table style="width: 100%;">
        <tr>
            <td colspan="2">
                รายละเอียดสมาชิก
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="Panel1" runat="server" Height="150px" Width="735px">
                    <dw:WebDataWindowControl ID="Dw_main" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
                        AutoSaveDataCacheAfterRetrieve="True" ClientFormatting="True" ClientScriptable="True"
                        DataWindowObject="d_divsrv_det_main" LibraryList="~/DataWindow/divavg/divsrv_detail.pbl"
                        ClientEventItemChanged="OnDwMainItemChange" ClientEventButtonClicked="OnDwmainButtonClick">
                    </dw:WebDataWindowControl>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <table style="width: 100%;">
        <tr>
            <td style="background-color: rgb(200,235,255);" id="Td1" align="center" width="16%">
                ปีปันผล
            </td>
            <td style="background-color: rgb(200,235,255); cursor: pointer;" id="stab_1" onclick="showTabPage(1);"
                align="center" width="21%">
                รายการปันผล เฉลี่ยคืนปีปัจจุบัน
            </td>
            <td style="background-color: rgb(200,235,255); cursor: pointer;" id="stab_2" onclick="showTabPage(2);"
                align="center" width="21%">
                รายการจ่ายปันผล เฉลี่ยคืน
            </td>
            <td style="background-color: rgb(200,235,255); cursor: pointer;" id="stab_3" onclick="showTabPage(3);"
                align="center" width="21%">
                รายการเคลือนไหวปันผล เฉลี่ยคืน
            </td>
            <td style="background-color: rgb(200,235,255); cursor: pointer;" id="stab_4" onclick="showTabPage(4);"
                align="center" width="21%">
                รายละเอียดปันผลแบบวัน
            </td>
            <td style="background-color: rgb(200,235,255); cursor: pointer;" id="stab_5" onclick="showTabPage(5);"
                align="center" width="21%">
                รายละเอียดปันผลแบบเดือน
            </td>
            <td style="background-color: rgb(200,235,255); cursor: pointer;" id="stab_6" onclick="showTabPage(6);"
                align="center" width="21%">
                รายละเอียดเฉลี่ยคืนตามสัญญา

            </td>
        </tr>
        <tr>
            <td valign="top">
                <dw:WebDataWindowControl ID="Dw_year" runat="server" DataWindowObject="d_divsrv_det_listyear"
                    LibraryList="~/DataWindow/divavg/divsrv_detail.pbl" AutoRestoreContext="False"
                    AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                    ClientEventClicked="OnDwYearClick" Height="330px" Width="200px" BorderStyle="Ridge">
                </dw:WebDataWindowControl>
            </td>
            <td colspan="4" valign="top">
                <div id="tab1" style="visibility: visible; position: absolute;">
                    <dw:WebDataWindowControl ID="Dw_master" runat="server" DataWindowObject="d_divsrv_det_master"
                        LibraryList="~/DataWindow/divavg/divsrv_detail.pbl" ClientScriptable="True" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" Height="330px"
                        Width="520px" BorderStyle="Ridge" ClientFormatting="True">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab2" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="Dw_methodpayment" runat="server" DataWindowObject="d_divsrv_det_methpay"
                        LibraryList="~/DataWindow/divavg/divsrv_detail.pbl" ClientScriptable="True" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" Height="330px"
                        Width="520px" BorderStyle="Ridge" ClientFormatting="True">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab3" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="Dw_statement" runat="server" DataWindowObject="d_divsrv_det_stm"
                        LibraryList="~/DataWindow/divavg/divsrv_detail.pbl" ClientScriptable="True" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" Height="330px"
                        Width="520px" BorderStyle="Ridge" ClientFormatting="True">
                    </dw:WebDataWindowControl>
                </div>
                 <div id="tab4" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="Dw_shrday" runat="server" DataWindowObject="d_divsrv_det_shr_day"
                        LibraryList="~/DataWindow/divavg/divsrv_detail.pbl" ClientScriptable="True" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" Height="330px"
                        Width="520px" BorderStyle="Ridge" ClientFormatting="True">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab5" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="Dw_shrmonth" runat="server" DataWindowObject="d_divsrv_det_shr_mth"
                        LibraryList="~/DataWindow/divavg/divsrv_detail.pbl" ClientScriptable="True" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" Height="330px"
                        Width="520px" BorderStyle="Ridge" ClientFormatting="True">
                    </dw:WebDataWindowControl>
                </div>
                 <div id="tab6" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="Dw_loan" runat="server" DataWindowObject="d_divsrv_det_lon_con"
                        LibraryList="~/DataWindow/divavg/divsrv_detail.pbl" ClientScriptable="True" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" Height="330px"
                        Width="520px" BorderStyle="Ridge" ClientFormatting="True">
                    </dw:WebDataWindowControl>
                </div>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="Hdmember_no" runat="server" />
    <asp:HiddenField ID="HdTab" runat="server" />
    <asp:HiddenField ID="HdYear" runat="server" />
    <asp:HiddenField ID="HiddenFieldTab" runat="server" />
    <asp:HiddenField ID="Hd_control" runat="server" />
    <asp:HiddenField ID="HdDivyear" runat="server" />
    <asp:HiddenField ID="Hdrow" runat="server" />
</asp:Content>
