<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_sl_loantype_detail.aspx.cs" Inherits="Saving.Applications.shrlon.w_sheet_sl_loantype_detail" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/JavaScript">
//setTimeout("location.reload(true);", 1000);
function showTabPage(tab) {
    var i = 1;
    for (i = 1; i <= 7; i++) {
        document.getElementById("tab" + i).style.visibility = "hidden";
        if (i == tab) {
            document.getElementById("tab" + i).style.visibility = "visible";
        }  
    }
}
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <h1>
        <asp:Label ID="lbl_pagename_w_sheet_sl_member_new" runat="server" Text="ข้อกำหนดประเภทหนี้"></asp:Label></h1>
    <table style="width: 100%;">
        <tr>
            <td>
                <h3>
                    ประเภทเงินกู้</h3>
                <dw:WebDataWindowControl ID="dw_main" runat="server" ClientScriptable="True" DataWindowObject="d_sl_loantype"
                    LibraryList="~/DataWindow/Shrlon/sl_loantype_detail.pbl">
                </dw:WebDataWindowControl>
            </td>
        </tr>
    </table>
    <br />
    <table class="tabpage" style="width: 100%;" border="0">
        <tr align="center">
            <td style="background-color: Silver;" id="stab1">
                <span id="stab" onclick="showTabPage(1);" style="cursor: pointer;">รายละเอียด</span>
            </td>
            <td style="background-color: Silver;" id="stab2">
                <span onclick="showTabPage(2);" style="cursor: pointer;">วงเงินกู้</span>
            </td>
            <td style="background-color: Silver;" id="stab3">
                <span onclick="showTabPage(3);" style="cursor: pointer;">อัตราดอกเบี้ย</span>
            </td>
            <td style="background-color: Silver;" id="stab4">
                <span onclick="showTabPage(4);" style="cursor: pointer;">หลักประกัน</span>
            </td>
            <td style="background-color: Silver;" id="stab5">
                <span onclick="showTabPage(5);" style="cursor: pointer;">วัตถุประสงค์</span>
            </td>
            <td style="background-color: Silver;" id="stab6">
                <span onclick="showTabPage(6);" style="cursor: pointer;">อื่นๆ</span>
            </td>
            <td style="background-color: Silver;" id="stab7">
                <span onclick="showTabPage(7);" style="cursor: pointer;">ประกันเงินกู้</span>
            </td>
        </tr>
    </table>
    <table style="width: 100%; height: 400px;" border="1">
        <tr>
            <td valign="top">
                <div id="tab1" style="visibility: visible; position: absolute;">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <dw:WebDataWindowControl ID="dw_detail" runat="server" DataWindowObject="d_sl_loantype_detail"
                                    LibraryList="~/DataWindow/Shrlon/sl_loantype_detail.pbl" ClientScriptable="True">
                                </dw:WebDataWindowControl>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="tab2" style="visibility: hidden; position: absolute;">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <dw:WebDataWindowControl ID="dw_right" runat="server" DataWindowObject="d_sl_loantype_right"
                                    LibraryList="~/DataWindow/Shrlon/sl_loantype_detail.pbl" ClientScriptable="True">
                                </dw:WebDataWindowControl>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dw:WebDataWindowControl ID="dw_custom" runat="server" DataWindowObject="d_sl_loantype_custom"
                                    LibraryList="~/DataWindow/Shrlon/sl_loantype_detail.pbl" ClientScriptable="True">
                                </dw:WebDataWindowControl>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="tab3" style="visibility: hidden; position: absolute;">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <dw:WebDataWindowControl ID="dw_interest" runat="server" DataWindowObject="d_sl_loantype_interest"
                                    LibraryList="~/DataWindow/Shrlon/sl_loantype_detail.pbl">
                                </dw:WebDataWindowControl>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="tab4" style="visibility: hidden; position: absolute;">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <dw:WebDataWindowControl ID="dw_reqgrt" runat="server" DataWindowObject="d_sl_loantype_reqgrt"
                                    LibraryList="~/DataWindow/Shrlon/sl_loantype_detail.pbl">
                                </dw:WebDataWindowControl>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dw:WebDataWindowControl ID="dw_colluse" runat="server" DataWindowObject="d_sl_loantype_colluse"
                                    LibraryList="~/DataWindow/Shrlon/sl_loantype_detail.pbl">
                                </dw:WebDataWindowControl>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="tab5" style="visibility: hidden; position: absolute;">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <dw:WebDataWindowControl ID="dw_objective" runat="server" DataWindowObject="d_sl_loantype_objective"
                                    LibraryList="~/DataWindow/Shrlon/sl_loantype_detail.pbl">
                                </dw:WebDataWindowControl>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="tab6" style="visibility: hidden; position: absolute;">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <dw:WebDataWindowControl ID="dw_buyshr" runat="server" DataWindowObject="d_sl_loantype_buyshare"
                                    LibraryList="~/DataWindow/Shrlon/sl_loantype_detail.pbl">
                                </dw:WebDataWindowControl>
                            </td>
                            <td>
                                <dw:WebDataWindowControl ID="dw_period" runat="server" DataWindowObject="d_sl_loantype_maxperiod"
                                    LibraryList="~/DataWindow/Shrlon/sl_loantype_detail.pbl">
                                </dw:WebDataWindowControl>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dw:WebDataWindowControl ID="dw_clear" runat="server" DataWindowObject="d_sl_loantype_clear"
                                    LibraryList="~/DataWindow/Shrlon/sl_loantype_detail.pbl">
                                </dw:WebDataWindowControl>
                            </td>
                            <td>
                                <dw:WebDataWindowControl ID="dw_pause" runat="server" DataWindowObject="d_sl_loantype_pause"
                                    LibraryList="~/DataWindow/Shrlon/sl_loantype_detail.pbl">
                                </dw:WebDataWindowControl>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="tab7" style="visibility: hidden; position: absolute;">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <dw:WebDataWindowControl ID="dw_insurance" runat="server" DataWindowObject="d_sl_loantype_insurance"
                                    LibraryList="~/DataWindow/Shrlon/sl_loantype_detail.pbl">
                                </dw:WebDataWindowControl>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
