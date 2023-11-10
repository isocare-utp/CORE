<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_dpdepttypecond.aspx.cs"
    Inherits="Saving.Applications.ap_deposit.w_sheet_dpdepttypecond" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%=initJavaScript%>
    <%=postType%>
    <%=postAddRow%>
    <%=postChange%>
    <%=bfUpdateDw%>
    <%=FilterBookType%>
    <%=postRate %>
    <%=delinterestrate%>>
    <script type="text/javascript">

        function showTabPage2(tab) {
            var i = 1;
            var tabamount = 5;
            for (i = 1; i <= tabamount; i++) {
                document.getElementById("tab_" + i).style.visibility = "hidden";
                document.getElementById("stab_" + i).className = "tabTypeTdDefault";
                if (i == tab) {
                    document.getElementById("tab_" + i).style.visibility = "visible";
                    document.getElementById("stab_" + i).className = "tabTypeTdSelected";
                    Gcoop.GetEl("HDCurrentTab").value = i + "";
                }
            }
        }

        function MenubarNew() {
            window.location = state.SsUrl + "Applications/ap_deposit/w_sheet_dpdepttypecond.aspx";
        }

        function SheetLoadComplete() {
            var tab = Gcoop.ParseInt(Gcoop.GetEl("HDCurrentTab").value);
            showTabPage2(tab);
        }

        //Check Event Tab ข้อกำหนดและการปรับ
        function onDwFixItemChange(sender, row, columnName, newValue) {
            if (columnName == "timedue_flag" || columnName == "havetax_status" || columnName == "clsbfduefee_flag" || columnName == "profit_flag" || columnName == "feenomove_flag" || columnName == "timedue_meth") {
                objDwFix.SetItem(row, columnName, newValue);
                objDwFix.AcceptText();
                postChange();
                return 0;
            }
            else if (columnName == "havetax_prefer") {
                objDwFix.SetItem(row, columnName, newValue);
                var haveTax = objDwFix.GetItem(1, "havetax_prefer") + "";
                var taxWay = objDwFix.GetItem(1, "taxway_compute") + "";
                if (taxWay == 1) {
                    if (haveTax == 0 || haveTax == 1) {
                        alert("ไม่สามารถเลือกรายการประเภทนี้ได้");
                        setTimeout("objDwFix.SetItem(" + row + ", '" + columnName + "', '2')", 500);
                    }
                }
                objDwFix.AcceptText();
            }


            else if (columnName == "taxway_compute") {
                objDwFix.SetItem(row, columnName, newValue);
                var haveTax = objDwFix.GetItem(1, "havetax_prefer") + "";
                var taxWay = objDwFix.GetItem(1, "taxway_compute") + "";
                if (haveTax == 0 || haveTax == 1) {
                    if (taxWay == 1) {
                        alert("ไม่สามารถเลือกรายการประเภทนี้ได้");
                        setTimeout("objDwFix.SetItem(" + row + ", '" + columnName + "', '0')", 500);
                    }
                }
                objDwFix.AcceptText();
            }

            else if (columnName == "f_minmonth_period") {
                var fminMonth = objDwFix.GetItem(row, columnName);
                objDwFix.SetItem(row, columnName, newValue);
                var timeDue = objDwFix.GetItem(row, "timedue_meth");
                if (timeDue == 1) {
                    objDwFix.SetItem(row, "f_monthdue_period", newValue);
                }
                else {
                    var minMonth = objDwFix.GetItem(row, columnName);
                    var monthDue = objDwFix.GetItem(row, "f_monthdue_period");
                    if (minMonth > monthDue) {
                        alert("ระยะเวลาเริ่มต้นควรมีค่าน้อยกว่าระยะเวลาสิ้นสุด");
                        setTimeout("objDwFix.SetItem(" + row + ", '" + columnName + "', '" + fminMonth + "')", 500);
                    }
                }
                objDwFix.AcceptText();
            }

            else if (columnName == "f_monthdue_period") {
                var fmonthDue = objDwFix.GetItem(row, columnName);
                objDwFix.SetItem(row, columnName, newValue);
                var timeDue = objDwFix.GetItem(row, "timedue_meth");
                if (timeDue == 0) {
                    var minMonth = objDwFix.GetItem(row, "f_minmonth_period");
                    var monthDue = objDwFix.GetItem(row, columnName);
                    if (monthDue < minMonth) {
                        alert("ระยะเวลาสิ้นสุดควรมีค่ามากกว่าระยะเวลาเริ่มต้น");
                        setTimeout("objDwFix.SetItem(" + row + ", '" + columnName + "', '" + fmonthDue + "')", 500);
                    }
                }
                objDwFix.AcceptText();
            }
        }
        function OnDwFixButtonClick(sender, row, buttonName) {
            if (buttonName == "cb_havetax") {
                var taxStatus = objDwFix.GetItem(1, "havetax_status");
                var haveTax = objDwFix.GetItem(1, "havetax_prefer");
                var deptType = objDwMain.GetItem(1, "depttype_code");
                if (taxStatus == 1) {
                    if (haveTax == 1) {
                        Gcoop.OpenDlg(360, 310, "w_dlg_dp_taxrateduring.aspx", "?deptType=" + deptType);
                    }
                    else if (haveTax == 0 || haveTax == 2) {
                        Gcoop.OpenDlg(360, 310, "w_dlg_dp_taxrateamt.aspx", "?deptType=" + deptType);
                    }
                }
            }
        }

        function OnDwSavingButtonClicked(sender, row, btnName) {
            var deptType = objDwMain.GetItem(1, "depttype_code");
            Gcoop.OpenDlg(350, 300, "w_dlg_dp_uncountwith.aspx", "?deptType=" + deptType);
        }

        function onDwFixOtherButtonClicked(sender, row, buttonName) {
            if (buttonName == "cb_period") {
                var deptType = objDwMain.GetItem(1, "depttype_code");
                Gcoop.OpenDlg(280, 210, "w_dlg_dp_period.aspx", "?deptType=" + deptType);
            }
            else if (buttonName == "cb_extraint") {
                var deptType = objDwMain.GetItem(1, "depttype_code");
                Gcoop.OpenDlg(340, 310, "w_dlg_dp_depttype_extraint.aspx", "?deptType=" + deptType);
            }
        }

        function onDwFixOtherItemChange(sender, row, columnName, newValue) {
            if (columnName == "intarr_flag" || columnName == "f_pdueintrate_meth" || columnName == "dept_period") {
                objDwFixOther.SetItem(row, columnName, newValue);
                objDwFixOther.AcceptText();
                postChange();
                return 0;
            }
        }

        function onDwSavingItemChange(sender, row, columnName, newValue) {
            if (columnName == "withcount_flag" || columnName == "chrgnbook_flag" || columnName == "maxbalance_flag" || columnName == "charge_flag" || columnName == "limitopenac_flag" || columnName == "limitdept_flag" || columnName == "limitwith_flag") { //เช็คข้อกำหนดครั้งการถอน
                objDwSaving.SetItem(row, columnName, newValue);
                objDwSaving.AcceptText();
                postChange();
                return 0;
            }
            else if (columnName == "book_stmbase") {
                objDwSaving.SetItem(row, columnName, newValue);
                objDwSaving.AcceptText();
                FilterBookType();
            }


        }

        function onDwMainItemChange(sender, row, columnName, newValue) {
            if (columnName == "persongrp_code") {
                var deptType;
                var persong;
                var HfDeptType = Gcoop.GetEl("HfDeptType");
                var Hdpersong = Gcoop.GetEl("Hdpersong");

                objDwMain.SetItem(row, columnName, newValue);
                deptType = objDwMain.GetItem(row, "depttype_select");
                persong = objDwMain.GetItem(row, "persongrp_code");
                HfDeptType.value = deptType;
                Hdpersong.value = persong;

                objDwMain.AcceptText();

                if (Gcoop.Trim(persong) != "" && Gcoop.Trim(deptType) != "") {
                    postType();
                }
            }
            // return 0;

            else if (columnName == "depttype_select") {
                sender.SetItem(row, columnName, newValue);
                sender.AcceptText();
                postRate();
            }
        }

        function OnDwInterestBfClicked(s, r, c) {
            Gcoop.CheckDw(s, r, c, "previos_feeflag", 1, 0);
            return 0;
        }

        function onDwInterestBfChange(sender, row, columnName, newValue) {
            if (columnName == "previos_intflag" || columnName == "previos_config") {
                objDwInterestBf.SetItem(row, columnName, newValue);
                objDwInterestBf.AcceptText();
                postChange();
            }
        }

        function OnDwUpIntItemClicked(sender, row, columnName, newValue) {
            if (columnName == "prncbal_max") {
                objDwUpInt.SetItem(row, columnName, newValue);
                objDwUpInt.AcceptText();
                var prncMin = parseInt(objDwUpInt.GetItem(row, "prncbal_min"), 10);
                var prncMax = parseInt(objDwUpInt.GetItem(row, "prncbal_max"), 10);
                if (prncMin >= prncMax) {
                    prncMin = prncMin + "";
                    alert("ค่าสิ้นสุดควรมีค่ามากกว่า : " + prncMin);
                }
            }
            else if (columnName == "prncbal_min") {
                objDwUpInt.SetItem(row, columnName, newValue);
                objDwUpInt.AcceptText();
            }
        }

        function DwInterestBfClicked(sender, row, btnName) {
            if (btnName == "cb_int") {
                var typecode = objDwInterestBf.GetItem(row, "depttype_code");
                var preseq = objDwInterestBf.GetItem(row, "preseq_no");
                var queryStr = "?typecode=" + typecode + "&preseq=" + preseq;
                Gcoop.OpenDlg(570, 500, "w_dlg_dp_interest_prewithdraw.aspx", queryStr);

            }
            else if (btnName == "cb_insert") {
                var typecode = objDwMain.GetItem(1, "depttype_code");
                var preseq = 0;
                objDwInterestBf.InsertRow(objDwInterestBf.RowCount() + 1);
                bfUpdateDw();
                objDwInterestBf.Update();
            }
        }
        function DelClick(sender, row, btnName) {
            if (btnName == "dtndel") {
                Gcoop.GetEl("HdRow").value = row;
                delinterestrate();
            }  
        }

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }
    
    </script>
    <style type="text/css">
        .tabTypeDefault
        {
            width: 100%;
            border-spacing: 2px;
        }
        .tabTypeTdDefault
        {
            width: 20%;
            height: 45px;
            font-family: Tahoma, Sans-Serif, Times;
            font-size: 12px;
            font-weight: bold;
            text-align: center;
            vertical-align: middle;
            color: #777777;
            border: solid 1px #55A9CD;
            background-color: rgb(200,235,255);
            cursor: pointer;
        }
        .tabTypeTdSelected
        {
            width: 20%;
            height: 45px;
            font-family: Tahoma, Sans-Serif, Times;
            font-size: 12px;
            font-weight: bold;
            text-align: center;
            vertical-align: middle;
            color: #660066;
            border: solid 1px #77CBEF;
            background-color: #76EFFF;
            cursor: pointer;
            text-decoration: underline;
        }
        .tabTypeTdDefault:hover
        {
            color: #882288;
            border: solid 1px #77CBEF;
            background-color: #98FFFF;
        }
        .tabTypeTdSelected:hover
        {
            color: #882288;
            border: solid 1px #77CBEF;
            background-color: #98FFFF;
        }
        .tabTableDetail
        {
            width: 99%;
        }
        .tabTableDetail td
        {
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <dw:WebDataWindowControl ID="DwMain" runat="server" DataWindowObject="d_dpdepttypecond"
        LibraryList="~/DataWindow/ap_deposit/dpdepttypecond.pbl" ClientEventItemChanged="onDwMainItemChange"
        ClientScriptable="True" AutoSaveDataCacheAfterRetrieve="True" AutoRestoreContext="False"
        AutoRestoreDataCache="True">
    </dw:WebDataWindowControl>
    <br />
    <table class="tabTypeDefault">
        <tr>
            <td class="tabTypeTdSelected" id="stab_1" onclick="showTabPage2(1);">
                ข้อกำหนดและการปรับ
            </td>
            <td class="tabTypeTdDefault" id="stab_2" onclick="showTabPage2(2);">
                เงื่อนไขการจ่าย Int./Div. ค่ารักษาบัญชี
            </td>
            <td class="tabTypeTdDefault" id="stab_3" onclick="showTabPage2(3);">
                การคิด Int./Div.
            </td>
            <td class="tabTypeTdDefault" id="stab_4" onclick="showTabPage2(4);">
                กำหนดอัตรา Int./Div.
            </td>
            <td class="tabTypeTdDefault" id="stab_5" onclick="showTabPage2(5);">
                ถอนก่อนกำหนด
            </td>
        </tr>
    </table>
    <br />
    <table class="tabTableDetail">
        <tr>
            <td style="height: 200px;" valign="top">
                <div id="tab_1" style="visibility: visible; position: absolute;">
                    <asp:Panel ID="Panel1" runat="server" Height="550px" ScrollBars="Auto">
                        <dw:WebDataWindowControl ID="DwSaving" runat="server" DataWindowObject="d_dp_depttype_saving"
                            LibraryList="~/DataWindow/ap_deposit/dpdepttypecond.pbl" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientEventItemChanged="onDwSavingItemChange"
                            ClientScriptable="True" Height="400px" ClientEventButtonClicked="OnDwSavingButtonClicked"
                            ClientFormatting="True">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </div>
                <div id="tab_2" style="visibility: hidden; position: absolute;">
                    <asp:Panel ID="Panel2" runat="server" Height="550px" ScrollBars="Auto">
                        <dw:WebDataWindowControl ID="DwFix" runat="server" DataWindowObject="d_dp_depttype_fix"
                            LibraryList="~/DataWindow/ap_deposit/dpdepttypecond.pbl" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                            ClientEventItemChanged="onDwFixItemChange" Height="250px" ClientEventButtonClicked="OnDwFixButtonClick"
                            ClientFormatting="True">
                        </dw:WebDataWindowControl>
                        <dw:WebDataWindowControl ID="DwUpInt" runat="server" DataWindowObject="d_dp_depttype_upintdate"
                            LibraryList="~/DataWindow/ap_deposit/dpdepttypecond.pbl" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                            ClientEventItemChanged="OnDwUpIntItemClicked" ClientFormatting="True">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </div>
                <div id="tab_3" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="DwFixOther" runat="server" DataWindowObject="d_dp_depttype_fix_other"
                        LibraryList="~/DataWindow/ap_deposit/dpdepttypecond.pbl" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                        ClientEventItemChanged="onDwFixOtherItemChange" ClientEventButtonClicked="onDwFixOtherButtonClicked"
                        ClientFormatting="True">
                    </dw:WebDataWindowControl>
                </div>
                <div id="tab_4" style="visibility: hidden; position: absolute;">
                    <asp:Panel ID="Panel3" runat="server" Width="750" Height="400" ScrollBars="Auto">
                        <dw:WebDataWindowControl ID="DwInterestNew" runat="server" DataWindowObject="d_dp_depttype_showinterest"
                            LibraryList="~/DataWindow/ap_deposit/dpdepttypecond.pbl" AutoRestoreContext="False"
                            AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientEventButtonClicked = "DelClick" ClientScriptable="True"
                            ClientFormatting="True">
                        </dw:WebDataWindowControl>
                    </asp:Panel>
                </div>
                <div id="tab_5" style="visibility: hidden; position: absolute;">
                    <dw:WebDataWindowControl ID="DwInterestBf" runat="server" DataWindowObject="d_dp_withdraw_bf"
                        LibraryList="~/DataWindow/ap_deposit/dpdepttypecond.pbl" AutoRestoreContext="False"
                        AutoRestoreDataCache="True" AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True"
                        ClientEventItemChanged="onDwInterestBfChange" ClientEventButtonClicked="DwInterestBfClicked"
                        ClientFormatting="True" ClientEventClicked="OnDwInterestBfClicked">
                    </dw:WebDataWindowControl>
                </div>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HSelect" runat="server" Value="02" />
    <asp:HiddenField ID="HDCurrentTab" runat="server" Value="01" />
    <asp:HiddenField ID="HfDeptType" runat="server" />
    <asp:HiddenField ID="Hdpersong" runat="server" />
    <asp:HiddenField ID="HdRow" runat="server" />
</asp:Content>
